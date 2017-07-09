using ContactAnalyser.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactAnalyser.Contracts
{
    public interface IAddressParseService
    {
        AddressEntry Parse(string address);

        IEnumerable<AddressEntry> Parse(IEnumerable<string> addressList);

    }
}
