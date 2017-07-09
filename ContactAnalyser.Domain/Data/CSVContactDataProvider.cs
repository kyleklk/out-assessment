using ContactAnalyser.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactAnalyser.Contracts.Models;
using CsvHelper;
using System.IO;

namespace ContactAnalyser.Domain.Data
{
    public class CsvContactDataProvider : IContactDataProvider
    {
        IFileProvider _fileProvider;

        public CsvContactDataProvider(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }

        public IEnumerable<ContactEntry> ReadContacts(string file)
        {          
            using (TextReader reader = _fileProvider.GetTextReader(file))
            {
                var csv = new CsvReader(reader, new CsvHelper.Configuration.CsvConfiguration() { HasHeaderRecord = true });
                var contacts = csv.GetRecords<ContactEntry>().ToList();
                return contacts;
            }
            
        }

        public int WriteFile<T>(string file, IEnumerable<T> items)
        {

            if (items == null || items.Count() == 0)
                throw new ArgumentException("Cant save null or empty items list");

            using (TextWriter writer = _fileProvider.GetTextWriter(file))
            {
                foreach(var line in items)
                {
                    writer.WriteLine(line.ToString());
                }
                writer.Flush();
                return items.Count();
            }
                
        }
    }
}
