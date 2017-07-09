using ContactAnalyser.Contracts;
using ContactAnalyser.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactAnalyser.Domain.Services
{
    public class NameSortService : ISortService<ContactEntry, NameEntry>
    {
        public IEnumerable<NameEntry> Sort(IEnumerable<ContactEntry> contacts)
        {
            if (contacts == null || contacts.Count() == 0)
                throw new ArgumentException("List of contacts cannot be null or empty");

            Dictionary<string, int> countCache = new Dictionary<string, int>();

            foreach (var name in contacts.SelectMany(x => new List<string>() { x.FirstName, x.LastName }))
            {
                if (!countCache.ContainsKey(name))
                {
                    countCache.Add(name, 0);
                }
                countCache[name] = countCache[name] + 1;
            }

            return countCache.Select(x => new NameEntry() { Name = x.Key, Count = x.Value }).OrderByDescending(x => x.Count).ThenBy(x => x.Name);
        }
    }
}
