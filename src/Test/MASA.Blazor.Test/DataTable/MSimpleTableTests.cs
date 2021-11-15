using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bunit;
using BlazorComponent;
using Moq;

namespace MASA.Blazor.Test.DataTable
{
    [TestClass]
    public class MSimpleTableTests : TestBase
    {
        [TestMethod]
        public void RendeDataTableWithDense()
        {
            //Act
            var cut = RenderComponent<MSimpleTable>(props =>
            {
                props.Add(simpletable => simpletable.Dense, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDenseClass = classes.Contains("m-data-table--dense");

            // Assert
            Assert.IsTrue(hasDenseClass);
        }

        [TestMethod]
        public void RendeDataTableNoWithDense()
        {
            //Act
            var cut = RenderComponent<MSimpleTable>(props =>
            {
                props.Add(simpletable => simpletable.Dense, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDenseClass = classes.Contains("m-data-table");

            // Assert
            Assert.IsTrue(hasDenseClass);
        }

        [TestMethod]
        public void RendeDataTableWithFixedHeader()
        {
            //Act
            var cut = RenderComponent<MSimpleTable>(props =>
            {
                props.Add(simpletable => simpletable.FixedHeader, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFixedHeaderClass = classes.Contains("m-data-table--fixed-header");

            // Assert
            Assert.IsTrue(hasFixedHeaderClass);
        }

        [TestMethod]
        public void RendeDataTableNoWithFixedHeader()
        {
            //Act
            var cut = RenderComponent<MSimpleTable>(props =>
            {
                props.Add(simpletable => simpletable.FixedHeader, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFixedHeaderClass = classes.Contains("m-data-table");

            // Assert
            Assert.IsTrue(hasFixedHeaderClass);
        }

        [TestMethod]
        public void RenderWithHeight()
        {
            // Act
            var cut = RenderComponent<MSimpleTable>(props =>
            {
                props.Add(p => p.Height, 300);
            });
            var inputSlotDiv = cut.Find(".m-data-table__wrapper");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("height: 300px", style);
        }

        [TestMethod]
        public void RenderWithWrapperContent()
        {
            // Arrange & Act
            var cut = RenderComponent<MSimpleTable>(props =>
            {
                props.Add(simpletable => simpletable.WrapperContent, "<span>Hello world</span>");
            });
            var contentDiv = cut.Find(".m-data-table");

            // Assert
            contentDiv.Children.MarkupMatches("<span>Hello world</span>");
        }

        [TestMethod]
        public void RendeSimpleTableWithDark()
        {
            //Act
            var cut = RenderComponent<MSimpleTable>(props =>
            {
                props.Add(simpletable => simpletable.Dark, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("theme--dark");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RendeSimpleTableNoWithDark()
        {
            //Act
            var cut = RenderComponent<MSimpleTable>(props =>
            {
                props.Add(simpletable => simpletable.Dark, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("m-data-table");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RendeSimpleTableWithLight()
        {
            //Act
            var cut = RenderComponent<MSimpleTable>(props =>
            {
                props.Add(simpletable => simpletable.Light, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("theme--light");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RendeSimpleTableNoWithLight()
        {
            //Act
            var cut = RenderComponent<MSimpleTable>(props =>
            {
                props.Add(simpletable => simpletable.Light, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("m-data-table");

            // Assert
            Assert.IsTrue(hasLightClass);
        }
    }
}
