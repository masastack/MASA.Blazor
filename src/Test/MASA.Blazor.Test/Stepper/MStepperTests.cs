using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bunit;
using BlazorComponent;
using Moq;
using Microsoft.AspNetCore.Components;


namespace MASA.Blazor.Test.Stepper
{
    [TestClass]
    public class MStepperTests:TestBase
    {
        [TestMethod]
        public void RendeMStepperWithFlat()
        {
            //Act
            var cut = RenderComponent<MStepper>(props =>
            {
                props.Add(stepper => stepper.Flat, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFlatClass = classes.Contains("m-stepper--flat");
            // Assert
            Assert.IsTrue(hasFlatClass);
        }

        [TestMethod]
        public void RendeMStepperWithVertical()
        {
            //Act
            var cut = RenderComponent<MStepper>(props =>
            {
                props.Add(stepper => stepper.Vertical, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasVerticalClass = classes.Contains("m-stepper--vertical");
            // Assert
            Assert.IsTrue(hasVerticalClass);
        }

        [TestMethod]
        public void RendeMStepperWithAltLabels()
        {
            //Act
            var cut = RenderComponent<MStepper>(props =>
            {
                props.Add(stepper => stepper.AltLabels, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasAltLabelsClass = classes.Contains("m-stepper--alt-labels");
            // Assert
            Assert.IsTrue(hasAltLabelsClass);
        }

        [TestMethod]
        public void RendeMStepperWithNonLinear()
        {
            //Act
            var cut = RenderComponent<MStepper>(props =>
            {
                props.Add(stepper => stepper.NonLinear, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasNonLinearClass = classes.Contains("m-stepper--non-linear");
            // Assert
            Assert.IsTrue(hasNonLinearClass);
        }

        [TestMethod]
        public void RendeMStepperNoWithFlat()
        {
            //Act
            var cut = RenderComponent<MStepper>(props =>
            {
                props.Add(stepper => stepper.Flat, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFlatClass = classes.Contains("m-stepper");
            // Assert
            Assert.IsTrue(hasFlatClass);
        }

        [TestMethod]
        public void RendeMStepperNoWithVertical()
        {
            //Act
            var cut = RenderComponent<MStepper>(props =>
            {
                props.Add(stepper => stepper.Vertical, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasVerticalClass = classes.Contains("m-stepper");
            // Assert
            Assert.IsTrue(hasVerticalClass);
        }

        [TestMethod]
        public void RendeMStepperNoWithAltLabels()
        {
            //Act
            var cut = RenderComponent<MStepper>(props =>
            {
                props.Add(stepper => stepper.AltLabels, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasAltLabelsClass = classes.Contains("m-stepper");
            // Assert
            Assert.IsTrue(hasAltLabelsClass);
        }

        [TestMethod]
        public void RendeMStepperNoWithNonLinear()
        {
            //Act
            var cut = RenderComponent<MStepper>(props =>
            {
                props.Add(stepper => stepper.NonLinear, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasNonLinearClass = classes.Contains("m-stepper");
            // Assert
            Assert.IsTrue(hasNonLinearClass);
        }

        [TestMethod]
        public void RenderWithChildContentt()
        {
            // Arrange & Act
            var cut = RenderComponent<MStepper>(props =>
            {
                props.Add(stepper => stepper.ChildContent, "<span>Hello world</span>");
            });
            var contentDiv = cut.Find(".m-stepper");

            // Assert
            contentDiv.Children.MarkupMatches("<span>Hello world</span>");
        }

        //[TestMethod]
        //public void RenderWithSize()
        //{
        //    // Act
        //    var cut = RenderComponent<MStepper>(props =>
        //    {
        //        props.Add(p => p.Value, 1);
        //    });
        //    var inputSlotDiv = cut.Find(".m-avatar");
        //    var style = inputSlotDiv.GetAttribute("style");

        //    // Assert
        //    Assert.AreEqual("", style);
        //}

        //[TestMethod]
        //public void RendeMStepperWithComplete()
        //{
        //    //Act
        //    var cut = RenderComponent<MStepper>(props =>
        //    {
        //        props.Add(stepper => stepper.Complete, true);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasCompleteClass = classes.Contains("m-stepper__step--complete");
        //    // Assert
        //    Assert.IsTrue(hasCompleteClass);
        //}
    }
}
