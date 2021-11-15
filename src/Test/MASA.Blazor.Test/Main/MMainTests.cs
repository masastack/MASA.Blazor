using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bunit;

namespace MASA.Blazor.Test.Main
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
