using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactAnalyser.Contracts.Models;
using System.IO;
using ContactAnalyser.Contracts;
using ContactAnalyser.Domain.Data;
using ContactAnalyser.Domain.Services;

namespace ContactAnalyser
{
    class Program
    {
        private static string _inputFileName;
        private static string _namesOuputFileName;
        private static string _addressOutputFileName;

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Use sample file? Y/N");
                if (Console.ReadLine() == "Y")
                {
                    _inputFileName = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..", "Input", "data.csv"));
                    _namesOuputFileName = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..", "Output", "name" + DateTime.Now.ToFileTime() + ".txt"));
                    _addressOutputFileName = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..", "Output", "address" + DateTime.Now.ToFileTime() + ".txt"));

                }
                else
                {
                    Console.WriteLine("Please input the source csv file:");
                    var tempInput = Console.ReadLine();
                    if (!File.Exists(tempInput)) { throw new FileNotFoundException("Selected input file does not exist"); }
                    if(Path.GetExtension(tempInput) != ".csv") { throw new ArgumentException("Input file is in the incorrect format, please ensure it is a csv file."); }
                    _inputFileName = tempInput;

                    Console.WriteLine("Please input the names file:");
                    var tempName = Console.ReadLine();
                    if (File.Exists(tempName)) { throw new FileNotFoundException("Selected file already exists"); }
                    if (Path.GetExtension(tempName) != ".txt") { throw new ArgumentException("Output file is in the incorrect format, please ensure it is a txt file."); }
                    _namesOuputFileName = tempName;

                    Console.WriteLine("Please input the addresses file:");
                    var tempAddress = Console.ReadLine();
                    if (File.Exists(tempAddress)) { throw new FileNotFoundException("Selected file already exists"); }
                    if (Path.GetExtension(tempAddress) != ".txt") { throw new ArgumentException("Output file is in the incorrect format, please ensure it is a txt file."); }
                    _addressOutputFileName = tempAddress;
                }
                Console.WriteLine("Using the folowing parmaters:");
                Console.WriteLine("-----------------------------");
                Console.WriteLine("Input File: " + _inputFileName);
                Console.WriteLine("Address Output File: " + _addressOutputFileName);
                Console.WriteLine("Name Output File: " + _namesOuputFileName);

                ProccessContacts();

                Console.WriteLine("Proccessing Complete!");
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            Console.WriteLine("Hit any key to exit.");
            Console.Read();
            
        }

        //this method wires up and orchestrates all required steps
        static void ProccessContacts()
        {
            //get all required instances
            var dataProvider = new CsvContactDataProvider(new FileProvider());
            var addressSortService = new AddressSortService(new AddressParseService());
            var nameSortService = new NameSortService();

            //read the csv data 
            Console.WriteLine("Reading CSV file...");
            var contacts = dataProvider.ReadContacts(_inputFileName);
            //do the sort
            Console.WriteLine("Sorting names and addresses...");
            var sortedName = nameSortService.Sort(contacts);
            var sortedAddress = addressSortService.Sort(contacts);

            //write the results to a file
            Console.WriteLine("Writing result files...");
            dataProvider.WriteFile(_namesOuputFileName, sortedName);
            dataProvider.WriteFile(_addressOutputFileName, sortedAddress);


        }
    }
}
