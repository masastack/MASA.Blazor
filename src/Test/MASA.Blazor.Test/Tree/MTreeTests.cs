using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Bunit;

namespace MASA.Blazor.Test.Tree
{
    [TestClass]
    public class MTreeTests:TestBase
    {
        [TestMethod]
        public void RenderTreeWithDark()
        {
            //Act
            var cut = RenderComponent<MTree<string>>(props =>
            {
                props.Add(tree => tree.Dark, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("theme--dark");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderTreeWithLight()
        {
            //Act
            var cut = RenderComponent<MTree<string>>(props =>
            {
                props.Add(tree => tree.Light, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("theme--light");

            // Assert
            Assert.IsTrue(hasLightClass);
        }
    }
}
