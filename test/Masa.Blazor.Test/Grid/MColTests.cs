using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Masa.Blazor.Test.Grid
{
    [TestClass]
    public class MColTests : TestBase
    {
        [TestMethod]
        public void RenderColWithCols()
        {
            //Act
            var cut = RenderComponent<MCol>(props =>
            {
                props.Add(col => col.Cols, 12);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasColsClass = classes.Contains("col");

            // Assert
            Assert.IsTrue(hasColsClass);
        }

        [TestMethod]
        public void RenderColWithLg()
        {
            //Act
            var cut = RenderComponent<MCol>(props =>
            {
                props.Add(col => col.Lg, 10);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLgClass = classes.Contains("col");

            // Assert
            Assert.IsTrue(hasLgClass);
        }

        [TestMethod]
        public void RenderColWithMd()
        {
            //Act
            var cut = RenderComponent<MCol>(props =>
            {
                props.Add(col => col.Md, 10);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasMdClass = classes.Contains("col");

            // Assert
            Assert.IsTrue(hasMdClass);
        }

        [TestMethod]
        public void RenderColWithOffset()
        {
            //Act
            var cut = RenderComponent<MCol>(props =>
            {
                props.Add(col => col.Offset, 10);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasOffsetClass = classes.Contains("col");

            // Assert
            Assert.IsTrue(hasOffsetClass);
        }

        [TestMethod]
        public void RenderColWithOffsetLg()
        {
            //Act
            var cut = RenderComponent<MCol>(props =>
            {
                props.Add(col => col.OffsetLg, 10);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasOffsetLgClass = classes.Contains("col");

            // Assert
            Assert.IsTrue(hasOffsetLgClass);
        }

        [TestMethod]
        public void RenderColWithOffsetMd()
        {
            //Act
            var cut = RenderComponent<MCol>(props =>
            {
                props.Add(col => col.OffsetMd, 10);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasOffsetMdClass = classes.Contains("col");

            // Assert
            Assert.IsTrue(hasOffsetMdClass);
        }

        [TestMethod]
        public void RenderColWithOffsetSm()
        {
            //Act
            var cut = RenderComponent<MCol>(props =>
            {
                props.Add(col => col.OffsetSm, 10);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasOffsetSmClass = classes.Contains("col");

            // Assert
            Assert.IsTrue(hasOffsetSmClass);
        }

        [TestMethod]
        public void RenderColWithOffsetXl()
        {
            //Act
            var cut = RenderComponent<MCol>(props =>
            {
                props.Add(col => col.OffsetXl, 10);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasOffsetXlClass = classes.Contains("col");

            // Assert
            Assert.IsTrue(hasOffsetXlClass);
        }

        [TestMethod]
        public void RenderColWithOrder()
        {
            //Act
            var cut = RenderComponent<MCol>(props =>
            {
                props.Add(col => col.Order, 10);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasOrderClass = classes.Contains("col");

            // Assert
            Assert.IsTrue(hasOrderClass);
        }

        [TestMethod]
        public void RenderColWithOrderLg()
        {
            //Act
            var cut = RenderComponent<MCol>(props =>
            {
                props.Add(col => col.OrderLg, 10);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasOrderLgClass = classes.Contains("col");

            // Assert
            Assert.IsTrue(hasOrderLgClass);
        }

        [TestMethod]
        public void RenderColWithOrderMd()
        {
            //Act
            var cut = RenderComponent<MCol>(props =>
            {
                props.Add(col => col.OrderMd, 10);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasOrderMdClass = classes.Contains("col");

            // Assert
            Assert.IsTrue(hasOrderMdClass);
        }

        [TestMethod]
        public void RenderColWithOrderSm()
        {
            //Act
            var cut = RenderComponent<MCol>(props =>
            {
                props.Add(col => col.OrderSm, 10);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasOrderSmMdClass = classes.Contains("col");

            // Assert
            Assert.IsTrue(hasOrderSmMdClass);
        }

        [TestMethod]
        public void RenderColWithOrderXl()
        {
            //Act
            var cut = RenderComponent<MCol>(props =>
            {
                props.Add(col => col.OrderXl, 10);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasOrderXlClass = classes.Contains("col");

            // Assert
            Assert.IsTrue(hasOrderXlClass);
        }

        [TestMethod]
        public void RenderColWithSm()
        {
            //Act
            var cut = RenderComponent<MCol>(props =>
            {
                props.Add(col => col.Sm, 3);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSmClass = classes.Contains("col");

            // Assert
            Assert.IsTrue(hasSmClass);
        }

        [TestMethod]
        public void RenderColWithXl()
        {
            //Act
            var cut = RenderComponent<MCol>(props =>
            {
                props.Add(col => col.Xl, 3);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasXlClass = classes.Contains("col");

            // Assert
            Assert.IsTrue(hasXlClass);
        }

        [TestMethod]
        public void RenderWithChildContentt()
        {
            // Arrange & Act
            var cut = RenderComponent<MCol>(props =>
            {
                props.Add(col => col.ChildContent, "<span>Hello world</span>");
            });
            var contentDiv = cut.Find(".col");

            // Assert
            contentDiv.Children.MarkupMatches("<span>Hello world</span>");
        }
    }
}
