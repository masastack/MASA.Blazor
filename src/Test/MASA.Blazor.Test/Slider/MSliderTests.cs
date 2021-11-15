using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace MASA.Blazor.Test.Slider
{
    [TestClass]
    public class MSliderTests:TestBase
    {
        [TestMethod]
        public void RenderWithIconContent()
        {
            // Arrange & Act
            var cut = RenderComponent<MSlider<double>>(props =>
            {
                props.Add(slider => slider.ThumbLabelContent, context => "<span>Hello world</span>");
            });
            var sliderDiv = cut.Find(".m-slider__thumb-label>div");

            // Assert
            sliderDiv.Children.MarkupMatches("<span>Hello world</span>");
        }

        [TestMethod]
        public void RenderSliderWithVertical()
        {
            //Act
            var cut = RenderComponent<MSlider<double>>(props =>
            {
                props.Add(slider => slider.Vertical, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasVerticalClass = classes.Contains("m-input__slider--vertical");

            // Assert
            Assert.IsTrue(hasVerticalClass);
        }

        [TestMethod]
        public void RenderSliderWithInverseLabel()
        {
            //Act
            var cut = RenderComponent<MSlider<double>>(props =>
            {
                props.Add(slider => slider.InverseLabel, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasInverseLabelClass = classes.Contains("m-input__slider--inverse-label");

            // Assert
            Assert.IsTrue(hasInverseLabelClass);
        }

        [TestMethod]
        public void RenderSliderWithnoVertical()
        {
            //Act
            var cut = RenderComponent<MSlider<double>>(props =>
            {
                props.Add(slider => slider.Vertical, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var noVerticalClass = !classes.Contains("m-slider--horizontal");

            // Assert
            Assert.IsTrue(noVerticalClass);
        }

        //[TestMethod]
        //public void RenderWithSize()
        //{
        //    // Act
        //    var cut = RenderComponent<MSlider<double>>(props =>
        //    {
        //        props.Add(p => p.ThumbSize, 32);
        //    });
        //    var inputSlotDiv = cut.Find(".m-slider");
        //    var style = inputSlotDiv.GetAttribute("style");

        //    // Assert
        //    Assert.AreEqual("height: 32px; width: 32px", style);
        //}
    }
}
