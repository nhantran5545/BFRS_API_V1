﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.ResponseModels
{
    public class CheckListDetailResponse
    {
        public string? QuestionName { get; set; }
        public bool? Compulsory { get; set; }
        public bool? Positive { get; set; }
        public int? Priority { get; set; }
        public string? Status { get; set; }
    }
}
