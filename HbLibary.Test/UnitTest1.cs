namespace HbLibary.Test;

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using HamRadio.Models; // Adjust namespace to your project

namespace HamRadio.Tests
{
    [TestClass]
    public class QsoTests
    {
        [TestMethod]
        public void Qso_CanStoreBasicFields()
        {
            var qso = new Qso
            {
                Call = "WX5ZZZ",
                QsoDateTimeUtc = new DateTime(2025, 6, 6, 14, 30, 0, DateTimeKind.Utc),
                Band = "40M",
                Mode = "SSB",
                RstSent = "59",
                RstRcvd = "59",
                Frequency = 7.2f,
                Grid = "EM36",
                LotwSent = true,
                LotwRcvd = false
            };

            Assert.AreEqual("WX5ZZZ", qso.Call);
            Assert.AreEqual("40M", qso.Band);
            Assert.AreEqual(7.2f, qso.Frequency);
        }

        [TestMethod]
        public void TryAddDetail_AddsNonCoreField()
        {
            var qso = new Qso();
            var success = qso.TryAddDetail("PotaRef", "K-1234");

            Assert.IsTrue(success);
            Assert.AreEqual(1, qso.Details.Count);
            Assert.AreEqual("K-1234", qso.Details.First().FieldValue);
        }

        [TestMethod]
        public void TryAddDetail_FailsForCoreField()
        {
            var qso = new Qso();
            var success = qso.TryAddDetail("Call", "WX5ZZZ");

            Assert.IsFalse(success);
            Assert.AreEqual(0, qso.Details.Count);
        }

        [TestMethod]
        public void TryAddDetail_FailsForDuplicate()
        {
            var qso = new Qso();
            var success1 = qso.TryAddDetail("SotaRef", "W1/NL-001");
            var success2 = qso.TryAddDetail("SotaRef", "W1/NL-002"); // duplicate key

            Assert.IsTrue(success1);
            Assert.IsFalse(success2);
            Assert.AreEqual(1, qso.Details.Count);
            Assert.AreEqual("W1/NL-001", qso.Details.First().FieldValue);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TryAddDetail_ThrowsForEmptyFieldName()
        {
            var qso = new Qso();
            qso.TryAddDetail("", "SomeValue");
        }
    }
}
