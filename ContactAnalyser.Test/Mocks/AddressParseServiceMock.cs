using ContactAnalyser.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactAnalyser.Contracts.Models;

namespace ContactAnalyser.Test.Mocks
{
    public class AddressParseServiceMock : IAddressParseService
    {
        /// <summary>
        /// this mock implementation will always return the consistant result
        /// </summary>
        /// <param name="address"></param>
        /// <returns>will always return 1 Street Road</returns>
        public AddressEntry Parse(string address)
        {
            return new AddressEntry() { StreetName = "Street Road", StreetNumber = "1" };
        }

        /// <summary>
        /// this mock implementation will always return a consistant result
        /// </summary>
        /// <param name="address"></param>
        /// <returns>will always return a predefined list of addresses</returns>
        public IEnumerable<AddressEntry> Parse(IEnumerable<string> addressList)
        {
            return new List<AddressEntry>()
            {
                new AddressEntry() { StreetName = "G Road", StreetNumber = "1" },
                new AddressEntry() { StreetName = "F Road", StreetNumber = "2" },
                new AddressEntry() { StreetName = "E Road", StreetNumber = "3" },
                new AddressEntry() { StreetName = "D Road", StreetNumber = "4" },
                new AddressEntry() { StreetName = "C Road", StreetNumber = "5" },
                new AddressEntry() { StreetName = "B Road", StreetNumber = "6" },
                new AddressEntry() { StreetName = "A Road", StreetNumber = "7" },
            };
        }
    }
}
