using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static Dictionary<string, object> pedigree = new Dictionary<string, object>();
    static Dictionary<string, Dictionary<string, object>> ancestors = new Dictionary<string, Dictionary<string, object>>();
    static Dictionary<string, object> names = new Dictionary<string, object>();
    static Dictionary<int, Dictionary<string, object>> generations = new Dictionary<int, Dictionary<string, object>>();
    static Dictionary<string, double> ancestorCache = new Dictionary<string, double>();

    static void Main()
    {
        const int MAX_GENS = 12;

        // Khởi tạo dữ liệu cho các thế hệx
        for (int i = 0; i <= MAX_GENS; i++)
        {
            generations[i] = new Dictionary<string, object>();
        }

        generations[0][""] = pedigree;
        FillPedigree();
        Calculate();
    }

    static void FillPedigree()
    {
        AddInd("", "X");
        AddInd("s", "B");
        AddInd("ss", "C");
        AddInd("sss", "A");
        AddInd("ssd", "E");
        AddInd("ssds", "A");


        AddInd("d", "D");
        AddInd("ds", "F");
        AddInd("dss", "E");
        AddInd("dsss", "A");


        AddInd("dd", "C");
        AddInd("dds", "A");
        AddInd("ddd", "E");
        AddInd("ddds", "A");
    }

    static void AddInd(string code, string name)
    {
        Dictionary<string, object> node = pedigree;
        bool isNewName = false;

        if (code.Length > 12)
        {
            return;
        }

        for (int i = 0; i < code.Length; i++)
        {
            if (!node.ContainsKey(code[i].ToString()))
            {
                node[code[i].ToString()] = new Dictionary<string, object>();
                generations[i + 1][code.Substring(0, i + 1)] = node[code[i].ToString()];
            }
            node = (Dictionary<string, object>)node[code[i].ToString()];
        }
        node["name"] = name;

        if (!ancestors.ContainsKey(name))
        {
            ancestors[name] = new Dictionary<string, object>();
            isNewName = true;
        }
        ancestors[name][code] = null;

        if (isNewName)
        {
            node = names;
            for (int i = 0; i < name.Length; i++)
            {
                if (!node.ContainsKey(name[i].ToString()))
                {
                    node[name[i].ToString()] = new Dictionary<string, object>();
                }
                node = (Dictionary<string, object>)node[name[i].ToString()];
            }
            node["end"] = null;
        }
    }

    static void Calculate()
    {
        ancestorCache.Clear(); 
        DoCalculation();
    }

    static void DoCalculation()
    {
        List<object> common = CalculateForCode("", true);
        common = common.Cast<object>().ToList();

        // Group by name
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

        // Sort by name
        groupedCommon.Sort((a, b) => a.Name.CompareTo(b.Name));

        // Display result and breakdown
        double totalInbreeding = groupedCommon.Sum(a => a.Inbreeding);
        Console.WriteLine($"Inbreeding: F = {(totalInbreeding * 100.0).ToString("F2")}%");

        foreach (var anc in groupedCommon)
        {
            string ancestorInfo = $"{(anc.Inbreeding * 100.0).ToString("F2")}% through {anc.Name} ({anc.Paths.Count} path{(anc.Paths.Count > 1 ? "s" : "")})";
            Console.WriteLine(ancestorInfo);

            if (anc.Paths.Count > 1)
            {
                var breakdownInfo = anc.Paths.Select(path =>
                    $"{(path.Inbreeding * 100.0).ToString("F2")}% × {path.NumPaths}"
                ).ToList();

                Console.WriteLine($"Breakdown for {anc.Name}: {string.Join(" + ", breakdownInfo)}");
            }
        }
    }


    static double calculateForName(string name)
    {
        string code;
        Dictionary<string, object> node;
        List<double> results = new List<double>();

        foreach (var codeEntry in ancestors[name].Keys)
        {
            code = codeEntry;
            node = getNodeFromCode(code);
            if (node.ContainsKey("s") && node.ContainsKey("d"))
            {
                results.AddRange(CalculateForCode(code, true).Select(item => Convert.ToDouble(((Dictionary<string, object>)item)["inbreeding"])));
            }
        }

        if (results.Count > 0)
        {
            return results.Max();
        }
        return 0.0;
    }




    static List<object> CalculateForCode(string baseCode, bool list)
    {
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

        foreach (var entry in ancestors)
        {
            ancs[entry.Key] = new Dictionary<string, object>(entry.Value);
        }

        if (baseCode.Length > 0)
        {
            foreach (var entry in ancs.ToList())
            {
                name = entry.Key;
                foreach (var codeEntry in entry.Value.Keys.ToList())
                {
                    if (codeEntry.IndexOf(baseCode) == 0)
                    {
                        ancs[name][codeEntry.Substring(baseCode.Length)] = null;
                    }
                    ancs[name].Remove(codeEntry);
                }
                if (ancs[name].Count == 0)
                {
                    ancs.Remove(name);
                }
            }
        }

        foreach (var entry in ancs)
        {
            name = entry.Key;
            foreach (var code1Entry in entry.Value.Keys)
            {
                foreach (var code2Entry in entry.Value.Keys)
                {
                    code1 = code1Entry;
                    code2 = code2Entry;

                    if (code2 != code1)
                    {
                        path = true;
                        interm.Clear();

                        for (i = 1; i < code1.Length; i++)
                        {
                            interm[getNodeFromCode(baseCode + code1.Substring(0, i))["name"].ToString()] = null;
                        }

                        for (i = 1; i < code2.Length; i++)
                        {
                            if (interm.ContainsKey(getNodeFromCode(baseCode + code2.Substring(0, i))["name"].ToString()))
                            {
                                // Found an intermediate ancestor that is in both codes.
                                // This is not an inbreeding path.
                                path = false;
                                break;
                            }
                        }

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

                            // 2^(-(length1 + length2 - 1))*(1 - FA) using shift operator
                            inbreeding = 1.0 / (1 << (code1.Length + code2.Length - 1)) * (1.0 + anc_inbreeding);

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
                    ancs[name].Remove(code1);

                }

            }

        }

        return common;
    }



    static Dictionary<string, object> getNodeFromCode(string code)
    {
        Dictionary<string, object> node = pedigree;
        for (int i = 0; i < code.Length; i++)
        {
            if (node.ContainsKey(code[i].ToString()))
            {
                node = (Dictionary<string, object>)node[code[i].ToString()];
            }
            else
            {
                throw new Exception($"Node matching code {code} does not exist");
            }
        }
        return node;
    }
}
