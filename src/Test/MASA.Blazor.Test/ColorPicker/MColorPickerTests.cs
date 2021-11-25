using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor.Test.ColorPicker
{
    [TestClass]
    public class MColorPickerTests:TestBase
    {
        [TestMethod]
        public void RenderColorPickerWithCanvasHeight()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MColorPicker>(props =>
            {
                props.Add(colorpicker => colorpicker.CanvasHeight, 150);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasCanvasHeightClass = classes.Contains("m-color-picker");

            // Assert
            Assert.IsTrue(hasCanvasHeightClass);
        }

        [TestMethod]
        public void RenderColorPickerWithDisabled()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MColorPicker>(props =>
            {
                props.Add(colorpicker => colorpicker.Disabled, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDisabledClass = classes.Contains("m-color-picker");

            // Assert
            Assert.IsTrue(hasDisabledClass);
        }

        [TestMethod]
        public void RenderWithDotSize()
        {
            // Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MColorPicker>(props =>
            {
                props.Add(p => p.DotSize, 10);
            });
            var inputSlotDiv = cut.Find(".m-color-picker");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("max-width: 300px", style);
        }

        [TestMethod]
        public void RenderWithElevation()
        {
            // Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MColorPicker>(props =>
            {
                props.Add(p => p.Elevation, 24);
            });
            var inputSlotDiv = cut.Find(".m-color-picker");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("max-width: 300px", style);
        }

        [TestMethod]
        public void RenderColorPickerWithFlat()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MColorPicker>(props =>
            {
                props.Add(colorpicker => colorpicker.Flat, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFlatClass = classes.Contains("m-color-picker");

            // Assert
            Assert.IsTrue(hasFlatClass);
        }

        [TestMethod]
        public void RenderColorPickerWithHideCanvas()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MColorPicker>(props =>
            {
                props.Add(colorpicker => colorpicker.HideCanvas, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasHideCanvasClass = classes.Contains("m-color-picker");

            // Assert
            Assert.IsTrue(hasHideCanvasClass);
        }

        [TestMethod]
        public void RenderColorPickerWithHideInputs()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MColorPicker>(props =>
            {
                props.Add(colorpicker => colorpicker.HideInputs, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasHideInputsClass = classes.Contains("m-color-picker");

            // Assert
            Assert.IsTrue(hasHideInputsClass);
        }

        [TestMethod]
        public void RenderColorPickerWithHideModeSwitch()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MColorPicker>(props =>
            {
                props.Add(colorpicker => colorpicker.HideModeSwitch, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasHideModeSwitchClass = classes.Contains("m-color-picker");

            // Assert
            Assert.IsTrue(hasHideModeSwitchClass);
        }

        [TestMethod]
        public void RenderColorPickerWithHideSliders()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MColorPicker>(props =>
            {
                props.Add(colorpicker => colorpicker.HideSliders, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasHideSlidersClass = classes.Contains("m-color-picker");

            // Assert
            Assert.IsTrue(hasHideSlidersClass);
        }

        [TestMethod]
        public void RenderColorPickerWithShowSwatches()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MColorPicker>(props =>
            {
                props.Add(colorpicker => colorpicker.ShowSwatches, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasShowSwatchesClass = classes.Contains("m-color-picker");

            // Assert
            Assert.IsTrue(hasShowSwatchesClass);
        }

        [TestMethod]
        public void RenderWithWidth()
        {
            // Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MColorPicker>(props =>
            {
                props.Add(p => p.Width, 300);
            });
            var inputSlotDiv = cut.Find(".m-color-picker");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("max-width: 300px", style);
        }
    }
}
