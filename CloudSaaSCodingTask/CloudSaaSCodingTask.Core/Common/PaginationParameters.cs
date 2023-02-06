﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSaaSCodingTask.Core.Common
{
    public class PaginationParameters
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string Search { get; set; } = string.Empty;
        public bool GoToLastPage { get; set; }
    }
}
