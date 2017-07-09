using ContactAnalyser.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactAnalyser.Contracts.Models;
using System.Text.RegularExpressions;

namespace ContactAnalyser.Domain.Services
{
    public class AddressParseService : IAddressParseService
    {
        private const string pattern = "^(\\d*[a-zA-Z]?)\\s(.+)$";

        public AddressEntry Parse(string address)
        {
            if (string.IsNullOrEmpty(address))
                throw new ArgumentNullException("Address cannot be null or empty");

            var match = Regex.Match(address, pattern);
            if (match.Success && match.Groups.Count > 1)
            {
                return new AddressEntry() { StreetNumber = match.Groups[1].Value, StreetName = match.Groups[2].Value };
            }
            else
            {
                throw new ArgumentException(address + " does not match the required format for an valid address");
            }
        }

        public IEnumerable<AddressEntry> Parse(IEnumerable<string> addressList)
        {
            var ret = new List<AddressEntry>();
            foreach (var address in addressList)
            {
                var itm = Parse(address);
                ret.Add(itm);
            }
            return ret;
        }
    }
}
