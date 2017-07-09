using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContactAnalyser.Contracts.Models;
using ContactAnalyser.Contracts;
using ContactAnalyser.Domain.Services;
using System.Collections.Generic;
using System.Linq;

namespace ContactAnalyser.Test
{
    [TestClass]
    public class NameSortServiceTest
    {
        ISortService<ContactEntry, NameEntry> _nameSort;

        [TestInitialize]
        public void Setup()
        {
            _nameSort = new NameSortService();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _nameSort = null;
        }

        [TestMethod]
        public void NameSortServiceWillSortContactList()
        {
            var contactList = new List<ContactEntry>
            {
                new ContactEntry{ FirstName = "Jimmy", LastName="Smith"},
                new ContactEntry { FirstName = "Clive", LastName = "Owen"},
                new ContactEntry { FirstName = "James", LastName = "Brown"},
                new ContactEntry { FirstName = "Graham", LastName = "Howe"},
                new ContactEntry { FirstName = "John", LastName = "Howe"},
                new ContactEntry { FirstName = "Clive", LastName = "Smith"},
                new ContactEntry { FirstName = "James", LastName = "Owen"},
                new ContactEntry { FirstName = "Graham", LastName = "Brown"}
            };

            var res = _nameSort.Sort(contactList).ToList();
            Assert.IsNotNull(res);
            Assert.IsTrue(res.Count() == 9);
            Assert.IsTrue(res[0].Name == "Brown" && res[0].Count == 2);
            Assert.IsTrue(res[8].Name == "John" && res[8].Count == 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NameSortServiceWillRaiseArgumentExceptionEmptyCollection()
        {
            var res = _nameSort.Sort(new List<ContactEntry>());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NameSortServiceWillRaiseArgumentExceptionNullCollection()
        {
            var res = _nameSort.Sort(null);
        }
    }
}
