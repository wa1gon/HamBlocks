// CmpTests.cs
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;

namespace StringExtensions.Tests;

[TestClass]
public class CmpTests
{
    [TestMethod]
    public void Ordinal_IgnoreCase_Equal_WhenCaseDiffers()
    {
        Assert.IsTrue("Hello".Cmp("hello", ignoreCase: true, culture: null));
    }

    [TestMethod]
    public void Ordinal_CaseSensitive_NotEqual_WhenCaseDiffers()
    {
        Assert.IsFalse("Hello".Cmp("hello", ignoreCase: false, culture: null));
    }

    [TestMethod]
    public void Culture_enUS_IgnoreCase_Diacritics_NotIgnored_ByDefault()
    {
        var enUS = new CultureInfo("en-US");
        // Your method ignores case but NOT accents by default.
        Assert.IsFalse("café".Cmp("CAFE", ignoreCase: true, culture: enUS));
    }

    [TestMethod]
    public void Culture_trTR_IgnoreCase_TurkishI_Equals()
    {
        var trTR = new CultureInfo("tr-TR");
        Assert.IsTrue("I".Cmp("ı", ignoreCase: true, culture: trTR));
        Assert.IsTrue("İ".Cmp("i", ignoreCase: true, culture: trTR));
    }

    [TestMethod]
    public void Culture_enUS_IgnoreCase_TurkishI_NotEqual()
    {
        var enUS = new CultureInfo("en-US");
        Assert.IsFalse("I".Cmp("ı", ignoreCase: true, culture: enUS));
    }

    [TestMethod]
    public void Nulls_AreHandled()
    {
        string? s1 = null;
        string? s2 = null;
        Assert.IsTrue(s1.Cmp(s2));          // both null
        Assert.IsFalse(s1.Cmp("a"));        // null vs non-null
        Assert.IsFalse("a".Cmp(s2));        // non-null vs null
    }

    [TestMethod]
    public void SameReference_ReturnsTrue()
    {
        var s = "same";
        var t = s;
        Assert.IsTrue(s.Cmp(t, ignoreCase: false));
    }
}
