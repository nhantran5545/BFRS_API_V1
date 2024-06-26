﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.ResponseModels.ChecklistResModels
{
    public class CheckListResponse
    {
        public string? CheckListName { get; set; }
        public int Phase { get; set; }
        public string? QuestionName { get; set; }
        public bool? Compulsory { get; set; }
        public bool? Positive { get; set; }
        public int? Priority { get; set; }
        public string? Status { get; set; }
    }
}

