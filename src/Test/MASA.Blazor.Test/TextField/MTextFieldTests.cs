using BlazorComponent;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor.Test.TextField
{
    [TestClass]
    public class MTextFieldTests : TestBase
    {
        [TestMethod]
        public void RenderNormal()
        {
            // Act
            var cut = RenderComponent<MTextField<string>>();
            var inputDiv = cut.Find("div");

            // Assert
            Assert.AreEqual(4, inputDiv.ClassList.Length);
            Assert.IsTrue(inputDiv.ClassList.Contains("m-input"));
            Assert.IsTrue(inputDiv.ClassList.Contains("theme--light"));
            Assert.IsTrue(inputDiv.ClassList.Contains("m-text-field"));
            Assert.IsTrue(inputDiv.ClassList.Contains("m-text-field--is-booted"));
            Assert.IsTrue(inputDiv.ClassList.Contains("m-input"));

        }

        [DataTestMethod]
        [DataRow("red")]
        [DataRow("blue")]
        public void RenderWithColorAndFocus(string color)
        {
            // Arrange
            var cut = RenderComponent<MTextField<string>>(props =>
            {
                props.Add(r => r.Color, color);
            });
            var inputDiv = cut.Find("div");
            var inputElement = cut.Find("input");

            // Act
            inputElement.Focus();

            // Assert
            Assert.AreEqual(6, inputDiv.ClassList.Length);
            Assert.IsTrue(inputDiv.ClassList.Contains(color + "--text"));
        }

        [TestMethod]
        public void RenderTextFieldWithFullWidth()
        {
            //Act
            var cut = RenderComponent<MTextField<string>>(props =>
            {
                props.Add(textfield => textfield.FullWidth, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFullWidthClass = classes.Contains("m-text-field--full-width");

            // Assert
            Assert.IsTrue(hasFullWidthClass);
        }

        [TestMethod]
        public void RenderTextFieldWithSoloInverted()
        {
            //Act
            var cut = RenderComponent<MTextField<string>>(props =>
            {
                props.Add(textfield => textfield.SoloInverted, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSoloInvertedClass = classes.Contains("m-text-field--solo-inverted");

            // Assert
            Assert.IsTrue(hasSoloInvertedClass);
        }

        [TestMethod]
        public void RenderTextFieldWithFlat()
        {
            //Act
            var cut = RenderComponent<MTextField<string>>(props =>
            {
                props.Add(textfield => textfield.Flat, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFlatClass = classes.Contains("m-text-field--solo-flat");

            // Assert
            Assert.IsTrue(hasFlatClass);
        }

        [TestMethod]
        public void RenderTextFieldWithFilled()
        {
            //Act
            var cut = RenderComponent<MTextField<string>>(props =>
            {
                props.Add(textfield => textfield.Filled, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFilledClass = classes.Contains("m-text-field--filled");

            // Assert
            Assert.IsTrue(hasFilledClass);
        }

        [TestMethod]
        public void RenderTextFieldWithReverse()
        {
            //Act
            var cut = RenderComponent<MTextField<string>>(props =>
            {
                props.Add(textfield => textfield.Reverse, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasReverseClass = classes.Contains("m-text-field--reverse");

            // Assert
            Assert.IsTrue(hasReverseClass);
        }

        [TestMethod]
        public void RenderTextFieldWithOutlined()
        {
            //Act
            var cut = RenderComponent<MTextField<string>>(props =>
            {
                props.Add(textfield => textfield.Outlined, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasOutlinedClass = classes.Contains("m-text-field--outlined");

            // Assert
            Assert.IsTrue(hasOutlinedClass);
        }

        [TestMethod]
        public void RenderTextFieldWithRounded()
        {
            //Act
            var cut = RenderComponent<MTextField<string>>(props =>
            {
                props.Add(textfield => textfield.Rounded, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasRoundedClass = classes.Contains("m-text-field--rounded");

            // Assert
            Assert.IsTrue(hasRoundedClass);
        }

        [TestMethod]
        public void RenderTextFieldWithShaped()
        {
            //Act
            var cut = RenderComponent<MTextField<string>>(props =>
            {
                props.Add(textfield => textfield.Shaped, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasShapedClass = classes.Contains("m-text-field--shaped");

            // Assert
            Assert.IsTrue(hasShapedClass);
        }

        [TestMethod]
        public void RenderTextFieldWithIsSolo()
        {
            //Act
            var cut = RenderComponent<MTextField<string>>(props =>
            {
                props.Add(textfield => textfield.Solo, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hassoloClass = classes.Contains("m-text-field--solo");

            // Assert
            Assert.IsTrue(hassoloClass);
        }

        [TestMethod]
        public void RenderTextFieldWithSingleLine()
        {
            //Act
            var cut = RenderComponent<MTextField<string>>(props =>
            {
                props.Add(textfield => textfield.SingleLine, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSingleLineClass = classes.Contains("m-text-field--single-line");

            // Assert
            Assert.IsTrue(hasSingleLineClass);
        }

        [TestMethod]
        public void RenderTextFieldNoWithFullWidth()
        {
            //Act
            var cut = RenderComponent<MTextField<string>>(props =>
            {
                props.Add(textfield => textfield.FullWidth, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFullWidthClass = classes.Contains("m-text-field");

            // Assert
            Assert.IsTrue(hasFullWidthClass);
        }

        [TestMethod]
        public void RenderTextFieldNoWithSoloInverted()
        {
            //Act
            var cut = RenderComponent<MTextField<string>>(props =>
            {
                props.Add(textfield => textfield.SoloInverted, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSoloInvertedClass = classes.Contains("m-text-field");

            // Assert
            Assert.IsTrue(hasSoloInvertedClass);
        }

        [TestMethod]
        public void RenderTextFieldNoWithFlat()
        {
            //Act
            var cut = RenderComponent<MTextField<string>>(props =>
            {
                props.Add(textfield => textfield.Flat, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFlatClass = classes.Contains("m-text-field");

            // Assert
            Assert.IsTrue(hasFlatClass);
        }

        [TestMethod]
        public void RenderTextFieldNoWithFilled()
        {
            //Act
            var cut = RenderComponent<MTextField<string>>(props =>
            {
                props.Add(textfield => textfield.Filled, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFilledClass = classes.Contains("m-text-field");

            // Assert
            Assert.IsTrue(hasFilledClass);
        }

        [TestMethod]
        public void RenderTextFieldNoWithReverse()
        {
            //Act
            var cut = RenderComponent<MTextField<string>>(props =>
            {
                props.Add(textfield => textfield.Reverse, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasReverseClass = classes.Contains("m-text-field");

            // Assert
            Assert.IsTrue(hasReverseClass);
        }

        [TestMethod]
        public void RenderTextFieldNoWithOutlined()
        {
            //Act
            var cut = RenderComponent<MTextField<string>>(props =>
            {
                props.Add(textfield => textfield.Outlined, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasOutlinedClass = classes.Contains("m-text-field");

            // Assert
            Assert.IsTrue(hasOutlinedClass);
        }

        [TestMethod]
        public void RenderTextFieldNoWithRounded()
        {
            //Act
            var cut = RenderComponent<MTextField<string>>(props =>
            {
                props.Add(textfield => textfield.Rounded, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasRoundedClass = classes.Contains("m-text-field");

            // Assert
            Assert.IsTrue(hasRoundedClass);
        }

        [TestMethod]
        public void RenderTextFieldNoWithShaped()
        {
            //Act
            var cut = RenderComponent<MTextField<string>>(props =>
            {
                props.Add(textfield => textfield.Shaped, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasShapedClass = classes.Contains("m-text-field");

            // Assert
            Assert.IsTrue(hasShapedClass);
        }

        [TestMethod]
        public void RenderTextFieldNoWithIsSolo()
        {
            //Act
            var cut = RenderComponent<MTextField<string>>(props =>
            {
                props.Add(textfield => textfield.Solo, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hassoloClass = classes.Contains("m-text-field");

            // Assert
            Assert.IsTrue(hassoloClass);
        }

        [TestMethod]
        public void RenderTextFieldNoWithSingleLine()
        {
            //Act
            var cut = RenderComponent<MTextField<string>>(props =>
            {
                props.Add(textfield => textfield.SingleLine, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSingleLineClass = classes.Contains("m-text-field");

            // Assert
            Assert.IsTrue(hasSingleLineClass);
        }

        //[TestMethod]
        //public void RenderWithChildContentt()
        //{
        //    // Arrange & Act
        //    var cut = RenderComponent<MTextField<string>>(props =>
        //    {
        //        props.Add(textfield => textfield.ChildContent, "<span>Hello world</span>");
        //    });
        //    var contentDiv = cut.Find(".m-text-field");

        //    // Assert
        //    contentDiv.Children.MarkupMatches("<span>Hello world</span>");
        //}

        //[TestMethod]
        //public void RenderWithPrependInnerContent()
        //{
        //    // Arrange & Act
        //    var cut = RenderComponent<MTextField<string>>(props =>
        //    {
        //        props.Add(textfield => textfield.PrependInnerContent, "<span>Hello world</span>");
        //    });
        //    var textfieldDiv = cut.Find("");

        //    // Assert
        //    textfieldDiv.Children.MarkupMatches("<span>Hello world</span>");
        //}

        //[TestMethod]
        //public void RenderWithAppendOuterContent()
        //{
        //    // Arrange & Act
        //    var cut = RenderComponent<MTextField<string>>(props =>
        //    {
        //        props.Add(textfield => textfield.AppendOuterContent, "<span>Hello world</span>");
        //    });
        //    var textfieldDiv = cut.Find("");

        //    // Assert
        //    textfieldDiv.Children.MarkupMatches("<span>Hello world</span>");
        //}

        //[TestMethod]
        //public void RenderWithProgressContent()
        //{
        //    // Arrange & Act
        //    var cut = RenderComponent<MTextField<string>>(props =>
        //    {
        //        props.Add(textfield => textfield.ProgressContent, "<span>Hello world</span>");
        //    });
        //    var textfieldDiv = cut.Find("");

        //    // Assert
        //    textfieldDiv.Children.MarkupMatches("<span>Hello world</span>");
        //}

        //[TestMethod]
        //public void RenderWithCounterContent()
        //{
        //    // Arrange & Act
        //    var cut = RenderComponent<MTextField<string>>(props =>
        //    {
        //        props.Add(textfield => textfield.CounterContent, "<span>Hello world</span>");
        //    });
        //    var textfieldDiv = cut.Find("");

        //    // Assert
        //    textfieldDiv.Children.MarkupMatches("<span>Hello world</span>");
        //}
    }
}
