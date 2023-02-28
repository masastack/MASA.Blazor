using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Masa.Blazor.Test.Picker
{
    [TestClass]
    public class MPickerTests : TestBase
    {
        [TestMethod]
        public void RendeMPickerWithFlat()
        {
            //Act
            var cut = RenderComponent<MPicker>(props =>
            {
                props.Add(picker => picker.Flat, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFlatClass = classes.Contains("m-picker--flat");

            // Assert
            Assert.IsTrue(hasFlatClass);
        }

        [TestMethod]
        public void RendeMPickerWithLandscape()
        {
            //Act
            var cut = RenderComponent<MPicker>(props =>
            {
                props.Add(picker => picker.Landscape, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLandscapeClass = classes.Contains("m-picker--landscape");

            // Assert
            Assert.IsTrue(hasLandscapeClass);
        }

        [TestMethod]
        public void RendeMPickerWithFullWidth()
        {
            //Act
            var cut = RenderComponent<MPicker>(props =>
            {
                props.Add(picker => picker.FullWidth, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFullWidthClass = classes.Contains("m-picker--full-width");

            // Assert
            Assert.IsTrue(hasFullWidthClass);
        }

        [TestMethod]
        public void RendeMPickerWithDark()
        {
            //Act
            var cut = RenderComponent<MPicker>(props =>
            {
                props.Add(picker => picker.Dark, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("theme--dark");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RendeMPickerWithLight()
        {
            //Act
            var cut = RenderComponent<MPicker>(props =>
            {
                props.Add(picker => picker.Light, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("theme--light");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderPickerWithElevation()
        {
            //Act
            var cut = RenderComponent<MPicker>(props =>
            {
                props.Add(picker => picker.Elevation, 2);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasElevationClass = classes.Contains("elevation-2");

            // Assert
            Assert.IsTrue(hasElevationClass);
        }

        //[TestMethod]
        //public void RendeMPickerWithNoTitle()
        //{
        //    //Act
        //    var cut = RenderComponent<MPicker>(props =>
        //    {
        //        props.Add(picker => picker.NoTitle, true);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasLightClass = classes.Contains("m-picker__actions--no-title");

        //    // Assert
        //    Assert.IsTrue(hasLightClass);
        //}

        //[TestMethod]
        //public void RenderWithSize()
        //{
        //    // Act
        //    var cut = RenderComponent<MPicker>(props =>
        //    {
        //        props.Add(p => p.Width, 290);
        //    });
        //    var inputSlotDiv = cut.Find(".m-picker__body");
        //    var style = inputSlotDiv.GetAttribute("style");

        //    // Assert
        //    Assert.AreEqual("width: 290px", style);
        //}
    }
}
