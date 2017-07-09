using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;
using ContactAnalyser.Contracts;
using ContactAnalyser.Domain.Services;
using System.Collections.Generic;
using System.Linq;

namespace ContactAnalyser.Test
{
    [TestClass]
    public class AddressParseServiceTest
    {
        IAddressParseService _parseService;

        [TestInitialize]
        public void Setup()
        {
            _parseService = new AddressParseService();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _parseService = null;
        }

        [TestMethod]
        public void ParserWillParseAddress()
        {
            var addressFormat1 = "1 street road";
            var resFormat1 = _parseService.Parse(addressFormat1);
            Assert.IsNotNull(resFormat1);
            Assert.IsTrue(resFormat1.StreetNumber == "1");
            Assert.IsTrue(resFormat1.StreetName == "street road");

            var addressFormat2 = "1b road street";
            var resFormat2 = _parseService.Parse(addressFormat2);
            Assert.IsNotNull(resFormat2);
            Assert.IsTrue(resFormat2.StreetNumber == "1b");
            Assert.IsTrue(resFormat2.StreetName == "road street");
        }

        [TestMethod]
        public void ParserWillParseMultipleAddreses()
        {
            var addresses = new List<string>()
            {
                "1 street street",
                "2 road street",
                "1b street avenue"
            };
            var res = _parseService.Parse(addresses).ToList();
            Assert.IsNotNull(res);
            Assert.AreEqual(res.Count(), 3);
            Assert.IsTrue(res[0].StreetNumber == "1");
            Assert.IsTrue(res[0].StreetName == "street street");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParserWillThrowAddressFormatError()
        {
            var invalidAddress = "street lane";
            _parseService.Parse(invalidAddress);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ParserWillThrowNullException()
        {
            var emptyAddress = "";
            _parseService.Parse(emptyAddress);
        }
    }
}
