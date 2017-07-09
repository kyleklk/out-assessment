using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactAnalyser.Contracts.Models
{
    public class AddressEntry
    {
        //this is a string as I am catering for a case where the street address is 1b street road
        public string StreetNumber { get; set; }

        public string StreetName { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}", StreetNumber, StreetName);
        }
    }
}
