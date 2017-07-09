using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContactAnalyser.Contracts;
using ContactAnalyser.Test.Mocks;
using ContactAnalyser.Domain.Services;
using ContactAnalyser.Contracts.Models;
using System.Collections.Generic;
using System.Linq;

namespace ContactAnalyser.Test
{
    [TestClass]
    public class AddressSortServiceTest
    {
        ISortService<ContactEntry, AddressEntry> _addressSort; 

        [TestInitialize]
        public void Setup()
        {
            _addressSort = new AddressSortService(new AddressParseServiceMock());
        }

        [TestCleanup]
        public void Cleanup()
        {
            _addressSort = null;
        }

        [TestMethod]
        public void AddressSortServiceWillSortContactList()
        {
            //we pass in a collection with 1 element to avoid the argument null exception 
            //the address parser mock will always return a predefined list of parsed contact entries
            //this enables is to test only the sort logic as the parse logic is tested elseware
            var res = _addressSort.Sort(new List<ContactEntry>() { new ContactEntry(){ Address = "1 test street"} }).ToList();
            Assert.IsNotNull(res);
            Assert.IsTrue(res.Count() == 7);
            Assert.IsTrue(res[0].StreetName == "A Road" && res[0].StreetNumber == "7");
            Assert.IsTrue(res[6].StreetName == "G Road" && res[6].StreetNumber == "1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddressSortServiceWillRaiseArgumentExceptionEmptyCollection()
        {
            var res = _addressSort.Sort(new List<ContactEntry>());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddressSortServiceWillRaiseArgumentExceptionNullCollection()
        {
            var res = _addressSort.Sort(null);
        }
    }
}
