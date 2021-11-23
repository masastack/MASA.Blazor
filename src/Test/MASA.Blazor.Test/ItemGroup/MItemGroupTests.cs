using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bunit;
using BlazorComponent;
using Moq;

namespace MASA.Blazor.Test.ItemGroup
{
    [TestClass]
    public class MItemGroupTests:TestBase
    {


        [TestMethod]
        public void RenderItemGroupWithDark()
        {
            //Act
            var cut = RenderComponent<MItemGroup>(props =>
            {
                props.Add(itemgroup => itemgroup.Dark, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasdarkClass = classes.Contains("theme--dark");

            // Assert
            Assert.IsTrue(hasdarkClass);
        }

        [TestMethod]
        public void RenderItemGroupNoWithDark()
        {
            //Act
            var cut = RenderComponent<MItemGroup>(props =>
            {
                props.Add(itemgroup => itemgroup.Dark, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasdarkClass = classes.Contains("theme--light");

            // Assert
            Assert.IsTrue(hasdarkClass);
        }

        [TestMethod]
        public void RenderItemGroupWithLight()
        {
            //Act
            var cut = RenderComponent<MItemGroup>(props =>
            {
                props.Add(itemgroup => itemgroup.Light, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("theme--light");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderItemGroupNoWithLight()
        {
            //Act
            var cut = RenderComponent<MItemGroup>(props =>
            {
                props.Add(itemgroup => itemgroup.Light, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("m-item-group");

            // Assert
            Assert.IsTrue(hasLightClass);
        }
    }
}
