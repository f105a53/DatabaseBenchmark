﻿using System;
using System.Collections.Generic;

namespace DatabaseBenchmark.Model.EFCore
{
    public partial class DeptLocations
    {
        public int Dnumber { get; set; }
        public string Dlocation { get; set; }

        public virtual Department DnumberNavigation { get; set; }
    }
}
