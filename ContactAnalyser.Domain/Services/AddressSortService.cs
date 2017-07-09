using ContactAnalyser.Contracts;
using ContactAnalyser.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactAnalyser.Domain.Services
{
    public class AddressSortService : ISortService<ContactEntry, AddressEntry>
    {
        IAddressParseService parser;

        public AddressSortService(IAddressParseService addressParser)
        {
            parser = addressParser;
        }

        public IEnumerable<AddressEntry> Sort(IEnumerable<ContactEntry> contacts)
        {
            if(contacts == null || contacts.Count() == 0)
                throw new ArgumentException("List of contacts cannot be null or empty");
            
            var parsedAddressList = parser.Parse(contacts.Select(x => x.Address).ToList());

            return parsedAddressList.OrderBy(x => x.StreetName);
        }
    }
}
