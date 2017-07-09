using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContactAnalyser.Contracts;
using ContactAnalyser.Domain.Data;
using ContactAnalyser.Test.Mocks;
using System.Collections.Generic;
using System.Linq;

namespace ContactAnalyser.Test
{
    [TestClass]
    public class CsvContactDataProviderTest
    {
        IContactDataProvider _dataProvider;
        FileProviderMock _mockFileProvider;

        [TestInitialize]
        public void Setup()
        {
            _mockFileProvider = new FileProviderMock();
            _dataProvider = new CsvContactDataProvider(_mockFileProvider);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _dataProvider = null;
        }

        [TestMethod]
        public void CsvContactDataProviderWriteTest()
        {
            var toWrite = new List<string>()
            {
                "line 1",
                "line 2"
            };

            int linesWritten = _dataProvider.WriteFile("test.txt", toWrite);
            Assert.IsTrue(linesWritten == 2);

            var writtenText = _mockFileProvider.StreamContents;
            Assert.IsTrue(writtenText == "line 1\r\nline 2\r\n");
        }

        [TestMethod]
        public void CsvContactDataProviderReadTest()
        {
            var contacts = _dataProvider.ReadContacts("test.txt").ToList();
            Assert.IsTrue(contacts.Count() == 3);
            Assert.IsTrue(contacts[0].FirstName == "Test");
            Assert.IsTrue(contacts[0].LastName == "Smith");
            Assert.IsTrue(contacts[0].Address == "1 B Lane");
            Assert.IsTrue(contacts[0].PhoneNumber == "123456");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CsvContactDataProviderWriteRaiseArgumentExceptionEmptyCollection()
        {
            var res = _dataProvider.WriteFile("test.txt",new List<string>());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CsvContactDataProviderWriteRaiseArgumentExceptionNullCollection()
        {
            List<string> param = null; 
            var res = _dataProvider.WriteFile("test.txt",param);
        }
    }
}
