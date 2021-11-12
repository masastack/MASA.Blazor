using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Bunit;

namespace MASA.Blazor.Test.Sheet
{
    [TestClass]
    public class MSheetTests:TestBase
    {
        [TestMethod]
        public void RendeSheetWithOutlined()
        {
            //Act
            var cut = RenderComponent<MSheet>(props =>
            {
                props.Add(sheet => sheet.Outlined, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasOutlinedClass = classes.Contains("m-sheet--outlined");
            // Assert
            Assert.IsTrue(hasOutlinedClass);
        }

        [TestMethod]
        public void RendeSheetWithShaped()
        {
            //Act
            var cut = RenderComponent<MSheet>(props =>
            {
                props.Add(sheet => sheet.Shaped, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasShapedClass = classes.Contains("m-sheet--shaped");
            // Assert
            Assert.IsTrue(hasShapedClass);
        }

        [TestMethod]
        public void RendeSheetWithDark()
        {
            //Act
            var cut = RenderComponent<MSheet>(props =>
            {
                props.Add(sheet => sheet.Dark, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("theme--dark");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RendeMSheetWithLight()
        {
            //Act
            var cut = RenderComponent<MSheet>(props =>
            {
                props.Add(sheet => sheet.Light, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("theme--light");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderWithChildContentt()
        {
            // Arrange & Act
            var cut = RenderComponent<MSheet>(props =>
            {
                props.Add(sheet => sheet.ChildContent, "<span>Hello world</span>");
            });
            var contentDiv = cut.Find(".m-sheet");

            // Assert
            contentDiv.Children.MarkupMatches("<span>Hello world</span>");
        }

        [TestMethod]
        public void RenderSheetWithElevation()
        {
            //Act
            var cut = RenderComponent<MSheet>(props =>
            {
                props.Add(sheet => sheet.Elevation, 24);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasElevationClass = classes.Contains("elevation-2");

            // Assert
            Assert.IsTrue(hasElevationClass);
        }
    }
}
