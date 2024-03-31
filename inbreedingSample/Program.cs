using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    public static Dictionary<string, object> pedigree = new Dictionary<string, object>();
    static Dictionary<string, Dictionary<string, object>> ancestors = new Dictionary<string, Dictionary<string, object>>();
    static Dictionary<int, Dictionary<string, object>> generations = new Dictionary<int, Dictionary<string, object>>();
    static Dictionary<string, double> ancestorCache = new Dictionary<string, double>();

    public static void Main()
    {
        const int MAX_GENS = 12;

        // Khởi tạo dữ liệu cho mỗi thế hệ.
        for (int i = 0; i <= MAX_GENS; i++)
        {
            generations[i] = new Dictionary<string, object>();
        }

        // Khởi tạo thế hệ đầu tiên với khóa chuỗi trống và từ điển danh phả.
        generations[0][""] = pedigree;

        // Fill the pedigree data with example values.
        FillPedigree();

        // Calculate inbreeding coefficients.
        Calculate();
    }

    // Function to fill the pedigree with example data.
    public static void FillPedigree()
    {
        // Add individuals to the pedigree with their codes and birdIds.
        AddInd("s", 1);
        AddInd("ss", 2);
        AddInd("sss", 3);
        AddInd("ssd", 4);
        AddInd("ssds", 3);
        AddInd("d", 5);
        AddInd("ds", 6);
        AddInd("dss", 4);
        AddInd("dsss", 3);
        AddInd("dd", 2);
        AddInd("dds", 3);
        AddInd("ddd", 4);
        AddInd("ddds", 3);
    }

    // Function to add an individual to the pedigree.
    public static void AddInd(string code, int birdId)
    {
        // Initialize variables.
        Dictionary<string, object> node = pedigree;


        // Duyệt qua các ký tự của mã để thêm nút vào danh phả.
        for (int i = 0; i < code.Length; i++)
        {
            if (!node.ContainsKey(code[i].ToString()))
            {
                // Nếu node does not exist, create a new dictionary for it.
                node[code[i].ToString()] = new Dictionary<string, object>();
                // Thêm nút mới vào thế hệ tương ứng.
            }
            node = (Dictionary<string, object>)node[code[i].ToString()];
        }

        // Đặt thuộc tính "birdId" của nút cuối cùng trong mã.
        node["birdId"] = birdId;

        // Check if the birdId is already in the ancestors dictionary.
        if (!ancestors.ContainsKey(birdId.ToString()))
        {
            // If not, create a new entry for the birdId in the ancestors dictionary.
            ancestors[birdId.ToString()] = new Dictionary<string, object>();
        }

        // Add the code to the ancestors dictionary for the given birdId.
        ancestors[birdId.ToString()][code] = null;
    }

    //calculate inbreeding coefficients.
    public static void Calculate()
    {
        // Clear the ancestor cache before calculations.
        ancestorCache.Clear();
        // Perform the calculation.
        DoCalculation();
    }

    // Function to perform the inbreeding calculation.
    public static void DoCalculation()
    {
        // Calculate inbreeding coefficients for each code.
        List<object> common = CalculateForCode("", true);
        common = common.Cast<object>().ToList();

        // Group the common ancestors by birdId.
        var groupedCommon = common
            .GroupBy(a => Convert.ToInt32(((Dictionary<string, object>)a)["birdId"]))
            .Select(group => new
            {
                BirdId = group.Key,
                Inbreeding = group.Sum(a => Convert.ToDouble(((Dictionary<string, object>)a)["inbreeding"])),
                Paths = group.Select(a => new
                {
                    Inbreeding = Convert.ToDouble(((Dictionary<string, object>)a)["inbreeding"]),
                    NumPaths = 1
                }).ToList()
            })
            .ToList();

        // Sort the grouped common ancestors by birdId.
        groupedCommon.Sort((a, b) => a.BirdId.CompareTo(b.BirdId));

        // Display the total inbreeding and breakdown for each ancestor.
        double totalInbreeding = groupedCommon.Sum(a => a.Inbreeding);
        Console.WriteLine($"Inbreeding: F = {(totalInbreeding * 100.0).ToString("F2")}%");

    }

    // Function to calculate inbreeding for a specific birdId.
    static double calculateForBirdId(int birdId)
    {
        // Initialize variables.
        string code;
        Dictionary<string, object> node;
        List<double> results = new List<double>();

        // Iterate through codes of the given birdId in the ancestors dictionary.
        foreach (var codeEntry in ancestors[birdId.ToString()].Keys)
        {
            code = codeEntry;
            node = getNodeFromCode(code);

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
    static List<object> CalculateForCode(string baseCode, bool list)
    {
        // Initialize variables.
        List<object> common = new List<object>();
        Dictionary<string, Dictionary<string, object>> ancs = new Dictionary<string, Dictionary<string, object>>();
        int birdId;
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
                birdId = Convert.ToInt32(entry.Key);
                foreach (var codeEntry in entry.Value.Keys.ToList())
                {
                    if (codeEntry.IndexOf(baseCode) == 0)
                    {
                        // Remove the baseCode from other codes.
                        ancs[birdId.ToString()][codeEntry.Substring(baseCode.Length)] = null;
                    }
                    ancs[birdId.ToString()].Remove(codeEntry);
                }
                if (ancs[birdId.ToString()].Count == 0)
                {
                    // Remove all codes for this birdId.
                    ancs.Remove(birdId.ToString());
                }
            }
        }

        // Iterate through each pair of codes in the ancestors dictionary.
        foreach (var entry in ancs)
        {
            birdId = Convert.ToInt32(entry.Key);
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
                            interm[getNodeFromCode(baseCode + code1.Substring(0, i))["birdId"].ToString()] = null;
                        }

                        for (i = 1; i < code2.Length; i++)
                        {
                            if (interm.ContainsKey(getNodeFromCode(baseCode + code2.Substring(0, i))["birdId"].ToString()))
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
                            if (ancestorCache.ContainsKey(birdId.ToString()))
                            {
                                anc_inbreeding = Convert.ToDouble(ancestorCache[birdId.ToString()]);
                            }
                            else
                            {
                                anc_inbreeding = calculateForBirdId(birdId);
                                ancestorCache[birdId.ToString()] = anc_inbreeding;
                            }

                            // Calculate inbreeding coefficient using the formula.
                            inbreeding = 1.0 / (1 << (code1.Length + code2.Length - 1)) * (1.0 + anc_inbreeding);

                            // Add the result to the common list.
                            if (list)
                            {
                                common.Add(new Dictionary<string, object>
                                {
                                    { "birdId", birdId },
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
                    ancs[birdId.ToString()].Remove(code1);
                }
            }
        }

        // Return the list of inbreeding coefficients.
        return common;
    }

    // Function to get a node from the pedigree using a given code.
    static Dictionary<string, object> getNodeFromCode(string code)
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
