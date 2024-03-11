using DataAccess.IRepositories;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.IService.Implements
{
    internal class BirdAlgorithmService
    {
        // Khai báo các Dictionary để lưu trữ dữ liệu danh phả, tổ tiên, tên, thế hệ và bộ nhớ cache tổ tiên.
        private Dictionary<string, object> pedigree = new Dictionary<string, object>();
        private Dictionary<string, Dictionary<string, object>> ancestors = new Dictionary<string, Dictionary<string, object>>();
        private Dictionary<string, object> names = new Dictionary<string, object>();
        private Dictionary<int, Dictionary<string, object>> generations = new Dictionary<int, Dictionary<string, object>>();
        private Dictionary<string, double> ancestorCache = new Dictionary<string, double>();
        private readonly IBirdRepository _birdRepository;
        //private Dictionary<string, Guid> Ancestors = new Dictionary<string, Guid>();

        public BirdAlgorithmService(IBirdRepository birdRepository)
        {
            _birdRepository = birdRepository;
        }

        public async Task<Dictionary<string, object>> GetPedigree(Guid birdId)
        {
            pedigree.Add("", birdId);
            await TrackAncestorsAsync("", birdId);
            return pedigree;
        }

        public async Task<double> GetInbreedingCoefficientAsync(Guid birdId)
        {
            await GetPedigree(birdId);
            var InbreedingCoefficientPercentage = DoCalculation();
            return InbreedingCoefficientPercentage;
        }
        private async Task TrackAncestorsAsync(string ancestor, Guid birdId)
        {
            Bird? bird = await _birdRepository.GetByIdAsync(birdId);

            if (bird == null) { return; }

            if (bird.FatherBirdId != null)
            {
                string fatherAncestor = ancestor + "s";
                Guid FatherBirdId = bird.FatherBirdId.Value;
                pedigree.Add(fatherAncestor, FatherBirdId);
                await TrackAncestorsAsync(fatherAncestor, FatherBirdId);
            }

            if (bird.MotherBirdId != null)
            {
                string motherAncestor = ancestor + "d";
                Guid MotherBirdId = bird.MotherBirdId.Value;
                pedigree.Add(motherAncestor, MotherBirdId);
                await TrackAncestorsAsync(motherAncestor, MotherBirdId);
            }

            return;
        }
        private double DoCalculation()
        {
            // Calculate inbreeding coefficients for each code.
            List<object> common = CalculateForCode("", true);
            common = common.Cast<object>().ToList();

            // Group the common ancestors by name.
            var groupedCommon = common
                .GroupBy(a => ((Dictionary<string, object>)a)["name"].ToString())
                .Select(group => new
                {
                    Name = group.Key,
                    Inbreeding = group.Sum(a => Convert.ToDouble(((Dictionary<string, object>)a)["inbreeding"])),
                    Paths = group.Select(a => new
                    {
                        Inbreeding = Convert.ToDouble(((Dictionary<string, object>)a)["inbreeding"]),
                        NumPaths = 1
                    }).ToList()
                })
                .ToList();

            // Sort the grouped common ancestors by name.
            groupedCommon.Sort((a, b) => a.Name.CompareTo(b.Name));

            // Display the total inbreeding and breakdown for each ancestor.
            double totalInbreeding = groupedCommon.Sum(a => a.Inbreeding);
            return totalInbreeding;
            // Console.WriteLine($"Inbreeding: F = {(totalInbreeding * 100.0).ToString("F2")}%");

            // foreach (var anc in groupedCommon)
            // {
            //     // Display information for each ancestor.
            //     string ancestorInfo = $"{(anc.Inbreeding * 100.0).ToString("F2")}% through {anc.Name} ({anc.Paths.Count} path{(anc.Paths.Count > 1 ? "s" : "")})";
            //     Console.WriteLine(ancestorInfo);

            //     // Display breakdown if there are multiple paths.
            //     if (anc.Paths.Count > 1)
            //     {
            //         var breakdownInfo = anc.Paths.Select(path =>
            //             $"{(path.Inbreeding * 100.0).ToString("F2")}% × {path.NumPaths}"
            //         ).ToList();

            //         Console.WriteLine($"Breakdown for {anc.Name}: {string.Join(" + ", breakdownInfo)}");
            //     }
            // }
        }

        // Function to calculate inbreeding for a specific name.
        private double calculateForName(string name)
        {
            // Initialize variables.
            string code;
            Dictionary<string, object> node;
            List<double> results = new List<double>();

            // Iterate through codes of the given name in the ancestors dictionary.
            foreach (var codeEntry in ancestors[name].Keys)
            {
                code = codeEntry;
                node = getNodeFromCode(code);

                // Check if the node has both "s" and "d" children.
                if (node.ContainsKey("s") && node.ContainsKey("d"))
                {
                    // Calculate inbreeding for all cases with both parents.
                    results.AddRange(CalculateForCode(code, true).Select(item => Convert.ToDouble(((Dictionary<string, object>)item)["inbreeding"])));
                }
            }

            // Return the maximum inbreeding coefficient among the results.
            if (results.Count > 0)
            {
                return results.Max();
            }
            return 0.0;
        }

        //  calculate inbreeding coefficients for a given base code.
        private List<object> CalculateForCode(string baseCode, bool list)
        {
            // Initialize variables.
            List<object> common = new List<object>();
            Dictionary<string, Dictionary<string, object>> ancs = new Dictionary<string, Dictionary<string, object>>();
            string name;
            string code1;
            string code2;
            int i;
            Dictionary<string, object> interm = new Dictionary<string, object>();
            bool path;
            double inbreeding;
            double anc_inbreeding;

            // Copy the ancestors dictionary.
            foreach (var entry in ancestors)
            {
                ancs[entry.Key] = new Dictionary<string, object>(entry.Value);
            }

            // Adjust the ancestor codes based on the baseCode.
            if (baseCode.Length > 0)
            {
                foreach (var entry in ancs.ToList())
                {
                    name = entry.Key;
                    foreach (var codeEntry in entry.Value.Keys.ToList())
                    {
                        if (codeEntry.IndexOf(baseCode) == 0)
                        {
                            // Remove the baseCode from other codes.
                            ancs[name][codeEntry.Substring(baseCode.Length)] = null;
                        }
                        ancs[name].Remove(codeEntry);
                    }
                    if (ancs[name].Count == 0)
                    {
                        // Remove all codes for this name.
                        ancs.Remove(name);
                    }
                }
            }

            // Iterate through each pair of codes in the ancestors dictionary.
            foreach (var entry in ancs)
            {
                name = entry.Key;
                foreach (var code1Entry in entry.Value.Keys)
                {
                    foreach (var code2Entry in entry.Value.Keys)
                    {
                        code1 = code1Entry;
                        code2 = code2Entry;

                        // Check if the codes are different.
                        if (code2 != code1)
                        {
                            path = true;
                            interm.Clear();

                            // Check for an intermediate ancestor that exists in both codes.
                            for (i = 1; i < code1.Length; i++)
                            {
                                interm[getNodeFromCode(baseCode + code1.Substring(0, i))["name"].ToString()] = null;
                            }

                            for (i = 1; i < code2.Length; i++)
                            {
                                if (interm.ContainsKey(getNodeFromCode(baseCode + code2.Substring(0, i))["name"].ToString()))
                                {
                                    // Found an intermediate ancestor that exists in both codes.
                                    // This is not an inbreeding path.
                                    path = false;
                                    break;
                                }
                            }

                            // If the path is unique, calculate inbreeding.
                            if (path)
                            {
                                if (ancestorCache.ContainsKey(name))
                                {
                                    anc_inbreeding = Convert.ToDouble(ancestorCache[name]);
                                }
                                else
                                {
                                    anc_inbreeding = calculateForName(name);
                                    ancestorCache[name] = anc_inbreeding;
                                }

                                // Calculate inbreeding coefficient using the formula.
                                inbreeding = 1.0 / (1 << (code1.Length + code2.Length - 1)) * (1.0 + anc_inbreeding);

                                // Add the result to the common list.
                                if (list)
                                {
                                    common.Add(new Dictionary<string, object>
                                {
                                    { "name", name },
                                    { "inbreeding", inbreeding }
                                });
                                }
                                else
                                {
                                    common.Add(inbreeding);
                                }
                            }
                        }
                        // Remove the processed code1 from the ancestor list.
                        ancs[name].Remove(code1);
                    }
                }
            }

            // Return the list of inbreeding coefficients.
            return common;
        }

        // Function to get a node from the pedigree using a given code.
        private  Dictionary<string, object> getNodeFromCode(string code)
        {
            // Initialize the node from the pedigree.
            Dictionary<string, object> node = pedigree;

            // Iterate through the characters of the code.
            for (int i = 0; i < code.Length; i++)
            {
                if (node.ContainsKey(code[i].ToString()))
                {
                    // Move to the next node in the pedigree.
                    node = (Dictionary<string, object>)node[code[i].ToString()];
                }
                else
                {
                    // Throw an exception if the node matching the code does not exist.
                    throw new Exception($"Node matching code {code} does not exist");
                }
            }

            // Return the final node.
            return node;
        }
    }
}
