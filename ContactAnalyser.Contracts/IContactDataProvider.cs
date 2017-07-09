using ContactAnalyser.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactAnalyser.Contracts
{
    public interface IContactDataProvider
    {
        IEnumerable<ContactEntry> ReadContacts(string file);

        int WriteFile<T>(string file, IEnumerable<T> items);
    }
}
