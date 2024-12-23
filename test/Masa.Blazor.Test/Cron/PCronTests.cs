using Bunit;

namespace Masa.Blazor.Test.Cron;

[TestClass]
public class PCronTests : TestBase
{
    [TestMethod]
    public void Render()
    {
        var cut = RenderComponent<PCron>();
        cut.Find(".m-cron");
    }
}