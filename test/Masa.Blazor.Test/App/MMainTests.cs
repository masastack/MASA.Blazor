using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Masa.Blazor.Test.Main
{
    [TestClass]
    public class MMainTests : TestBase
    {
        [TestMethod]
        public void RenderMain()
        {
            //Act
            var cut = RenderComponent<MMain>();
            var main = cut.Find("main");

            // Assert
            Assert.IsNotNull(main);
        }
    }
}
