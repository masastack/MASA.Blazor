using Masa.Blazor.Core;

namespace Masa.Blazor.Test.Core;

[TestClass]
public class ColorParserTests
{
    [TestMethod]
    [DataRow("rgb(31, 31, 31)")]
    [DataRow("#1f1f1f")]
    [DataRow("hsl(0,0%,12.2%)")]
    public void Test1(string color)
    {
        var parsedColor = ColorParser.ParseColor(color);
        Assert.AreEqual(31, parsedColor.R);
        Assert.AreEqual(31, parsedColor.G);
        Assert.AreEqual(31, parsedColor.B);
    }

    [TestMethod]
    [DataRow("rgba(31, 31, 31, 0.5)")]
    [DataRow("#1f1f1f80")]
    [DataRow("hsla(0,0%,12.2%, 0.5)")]
    public void Test2(string color)
    {
        var parsedColor = ColorParser.ParseColor(color);
        Assert.AreEqual(31, parsedColor.R);
        Assert.AreEqual(31, parsedColor.G);
        Assert.AreEqual(31, parsedColor.B);
    }
}