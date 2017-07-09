﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactAnalyser.Contracts.Models
{
    public class NameEntry
    {
        public string Name { get; set; }

        public int Count { get; set; }

        public override string ToString()
        {
            return string.Format("{0}, {1}", Name, Count);
        }
    }
}
