using Bunit;

namespace Masa.Blazor.Test.Alert
{
    [TestClass]
    public class MAlertTests : TestBase<MAlert>
    {
        [TestMethod]
        public void Border()
        {
            var cut = Render(props =>
            {
                props.Add(alert => alert.Border, Borders.Left);
            });

            cut.Find(".m-alert__border");
        }

        [TestMethod]
        public void RenderAlertWithDense()
        {
            //Act
            var cut = RenderAndGetRootElement(props =>
            {
                props.Add(alert => alert.Dense, true);
            });
            var hasDenseClass = cut.ClassList.Contains("m-alert--dense");

            // Assert
            Assert.IsTrue(hasDenseClass);
        }

        [TestMethod]
        public void RenderAlertWithDark()
        {
            //Act
            var cut = RenderAndGetRootElement(props =>
            {
                props.Add(alert => alert.Dark, true);
            });
            var hasDarkClass = cut.ClassList.Contains("theme--dark");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderAlertWithDismissible()
        {
            //Act
            var cut = Render(props =>
            {
                props.Add(alert => alert.Dismissible, true);
            });

            cut.Find(".m-alert__dismissible");
        }

        [TestMethod]
        public void RenderAlertWithElevation()
        {
            //Act
            var cut = RenderAndGetRootElement(props =>
            {
                props.Add(alert => alert.Elevation, 2);
            });
            var hasElevationClass = cut.ClassList.Contains("elevation-2");

            // Assert
            Assert.IsTrue(hasElevationClass);
        }

        [TestMethod]
        public void RenderWithHeight()
        {
            // Act
            var cut = RenderAndGetRootElement(props =>
            {
                props.Add(p => p.Height, 100);
            });
            var style = cut.GetAttribute("style");

            // Assert
            Assert.AreEqual("height: 100px;", style);
        }

        [TestMethod]
        public void RenderAlertWithIcon()
        {
            //Act
            var cut = RenderAndGetRootElement(props =>
            {
                props.Add(alert => alert.Icon, true);
            });
            var hasIconClass = cut.ClassList.Contains("m-alert");

            // Assert
            Assert.IsTrue(hasIconClass);
        }

        [TestMethod]
        public void RenderAlertWithWithLight()
        {
            //Act
            var cut = RenderAndGetRootElement(props =>
            {
                props.Add(alert => alert.Light, true);
            });
            var hasLightClass = cut.ClassList.Contains("theme--light");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderWithMaxHeight()
        {
            // Act
            var cut = RenderAndGetRootElement(props =>
            {
                props.Add(p => p.MaxHeight, 100);
            });
            var style = cut.GetAttribute("style");

            // Assert
            Assert.AreEqual("max-height: 100px;", style);
        }

        [TestMethod]
        public void RenderWithMaxWidth()
        {
            // Act
            var cut = RenderAndGetRootElement(props =>
            {
                props.Add(p => p.MaxWidth, 100);
            });
            var style = cut.GetAttribute("style");

            // Assert
            Assert.AreEqual("max-width: 100px;", style);
        }

        [TestMethod]
        public void RenderWithMinWidth()
        {
            // Act
            var cut = RenderAndGetRootElement(props =>
            {
                props.Add(p => p.MinWidth, 100);
            });
            var style = cut.GetAttribute("style");

            // Assert
            Assert.AreEqual("min-width: 100px;", style);
        }

        [TestMethod]
        public void RenderWithMinHeight()
        {
            // Act
            var cut = RenderAndGetRootElement(props =>
            {
                props.Add(p => p.MinHeight, 100);
            });
            var style = cut.GetAttribute("style");

            // Assert
            Assert.AreEqual("min-height: 100px;", style);
        }

        [TestMethod]
        public void RenderAlertWithOutlined()
        {
            //Act
            var cut = RenderAndGetRootElement(props =>
            {
                props.Add(alert => alert.Outlined, true);
            });
            var hasOutlinedClass = cut.ClassList.Contains("m-alert--outlined");

            // Assert
            Assert.IsTrue(hasOutlinedClass);
        }

        [TestMethod]
        public void RenderAlertWithProminent()
        {
            //Act
            var cut = RenderAndGetRootElement(props =>
            {
                props.Add(alert => alert.Prominent, true);
            });
            var hasProminentClass = cut.ClassList.Contains("m-alert--prominent");

            // Assert
            Assert.IsTrue(hasProminentClass);
        }

        [TestMethod]
        public void RenderAlertWithRounded()
        {
            //Act
            var cut = RenderAndGetRootElement(props =>
            {
                props.Add(alert => alert.Rounded, true);
            });
            var hasRoundedClass = cut.ClassList.Contains("rounded");

            // Assert
            Assert.IsTrue(hasRoundedClass);
        }

        [TestMethod]
        public void RenderAlertWithShaped()
        {
            //Act
            var cut = RenderAndGetRootElement(props =>
            {
                props.Add(alert => alert.Shaped, true);
            });
            var hasShapedClass = cut.ClassList.Contains("m-sheet--shaped");

            // Assert
            Assert.IsTrue(hasShapedClass);
        }

        [TestMethod]
        public void RenderAlertWithText()
        {
            //Act
            var cut = RenderAndGetRootElement(props =>
            {
                props.Add(alert => alert.Text, true);
            });
            var hasTextClass = cut.ClassList.Contains("m-alert--text");

            // Assert
            Assert.IsTrue(hasTextClass);
        }

        [TestMethod]
        public void RenderAlertWithTile()
        {
            //Act
            var cut = RenderAndGetRootElement(props =>
            {
                props.Add(alert => alert.Tile, true);
            });
            var hasRoundedClass = cut.ClassList.Contains("rounded-0");

            // Assert
            Assert.IsTrue(hasRoundedClass);
        }

        [TestMethod]
        public void RenderAlertWithValue()
        {
            //Act
            var cut = RenderAndGetRootElement(props =>
            {
                props.Add(alert => alert.Value, true);
            });
            var hasValueClass = cut.ClassList.Contains("m-alert");

            // Assert
            Assert.IsTrue(hasValueClass);
        }

        [TestMethod]
        public void RenderWithChildContent()
        {
            // Arrange & Act
            var cut = Render(props =>
            {
                props.Add(alert => alert.ChildContent, "<span>Hello world</span>");
            });
            var contentDiv = cut.Find(".m-alert__content");

            // Assert
            contentDiv.Children.MarkupMatches("<span>Hello world</span>");
        }
    }
}
