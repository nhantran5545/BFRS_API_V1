using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Enums
{
    internal enum EggStatus
    {
        Unknown_Unknown,
        Unknown_Broken,
        Empty_Empty,
        Empty_Broken,
        Empty_Eliminated,
        Empty_Abandoned,
        Fertilized_InDevelopment,
        Fertilized_Hatched,
        Fertilized_DeadInShell,
        Fertilized_DeadEmbryo,
        Fertilized_Broken,
        Fertilized_Missing,
        Fertilized_Eliminated,
        Fertilized_Abandoned,
        Deleted = -1
    }
}
