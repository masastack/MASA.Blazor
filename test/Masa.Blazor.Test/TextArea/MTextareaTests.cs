using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Masa.Blazor.Test.Textarea
{
    [TestClass]
    public class MTextAreaTests : TestBase
    {
        [TestMethod]
        public void RenderTextareaWithAutoGrow()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MTextarea>(props =>
            {
                props.Add(textarea => textarea.AutoGrow, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasAutoGrowClass = classes.Contains("m-textarea--auto-grow");
            // Assert
            Assert.IsTrue(hasAutoGrowClass);
        }

        [TestMethod]
        public void RenderTextareaWithAutofocus()
        {
            //Act
            var cut = RenderComponent<MTextarea>(props =>
            {
                props.Add(textarea => textarea.Autofocus, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasAutofocusClass = classes.Contains("m-textarea");
            // Assert
            Assert.IsTrue(hasAutofocusClass);
        }

        [TestMethod]
        public void RenderTextareaWithClearable()
        {
            //Act
            var cut = RenderComponent<MTextarea>(props =>
            {
                props.Add(textarea => textarea.Clearable, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasClearableClass = classes.Contains("m-textarea");
            // Assert
            Assert.IsTrue(hasClearableClass);
        }

        [TestMethod]
        public void RenderTextareaWithCounter()
        {
            //Act
            var cut = RenderComponent<MTextarea>(props =>
            {
                props.Add(textarea => textarea.Counter, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasCounterClass = classes.Contains("m-textarea");
            // Assert
            Assert.IsTrue(hasCounterClass);
        }

        [TestMethod]
        public void RenderTextareaWithDark()
        {
            //Act
            var cut = RenderComponent<MTextarea>(props =>
            {
                props.Add(textarea => textarea.Dark, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("theme--dark");
            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderTextareaWithDense()
        {
            //Act
            var cut = RenderComponent<MTextarea>(props =>
            {
                props.Add(textarea => textarea.Dense, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDenseClass = classes.Contains("m-textarea");
            // Assert
            Assert.IsTrue(hasDenseClass);
        }

        [TestMethod]
        public void RenderTextareaWithDisabled()
        {
            //Act
            var cut = RenderComponent<MTextarea>(props =>
            {
                props.Add(textarea => textarea.Disabled, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDisabledClass = classes.Contains("m-textarea");
            // Assert
            Assert.IsTrue(hasDisabledClass);
        }

        [TestMethod]
        public void RenderTextareaWithError()
        {
            //Act
            var cut = RenderComponent<MTextarea>(props =>
            {
                props.Add(textarea => textarea.Error, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasErrorClass = classes.Contains("m-textarea");
            // Assert
            Assert.IsTrue(hasErrorClass);
        }

        [TestMethod]
        public void RenderTextareaWithErrorCount()
        {
            //Act
            var cut = RenderComponent<MTextarea>(props =>
            {
                props.Add(textarea => textarea.ErrorCount, 1);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasErrorCountClass = classes.Contains("m-textarea");

            // Assert
            Assert.IsTrue(hasErrorCountClass);
        }

        [TestMethod]
        public void RenderTextareaWithFilled()
        {
            //Act
            var cut = RenderComponent<MTextarea>(props =>
            {
                props.Add(textarea => textarea.Filled, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFilledClass = classes.Contains("m-textarea");
            // Assert
            Assert.IsTrue(hasFilledClass);
        }

        [TestMethod]
        public void RenderTextareaWithFlat()
        {
            //Act
            var cut = RenderComponent<MTextarea>(props =>
            {
                props.Add(textarea => textarea.Flat, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFlatClass = classes.Contains("m-textarea");
            // Assert
            Assert.IsTrue(hasFlatClass);
        }

        [TestMethod]
        public void RenderTextareaWithFullWidth()
        {
            //Act
            var cut = RenderComponent<MTextarea>(props =>
            {
                props.Add(textarea => textarea.FullWidth, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFullWidthClass = classes.Contains("m-textarea");
            // Assert
            Assert.IsTrue(hasFullWidthClass);
        }

        [TestMethod]
        public void RenderTextareaWithHeight()
        {
            //Act
            var cut = RenderComponent<MTextarea>(props =>
            {
                props.Add(textarea => textarea.Height, 1);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasErrorCountClass = classes.Contains("m-textarea");

            // Assert
            Assert.IsTrue(hasErrorCountClass);
        }

        [TestMethod]
        public void RenderTextareaWithHideDetails()
        {
            //Act
            var cut = RenderComponent<MTextarea>(props =>
            {
                props.Add(textarea => textarea.HideDetails, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasHideDetailsClass = classes.Contains("m-textarea");
            // Assert
            Assert.IsTrue(hasHideDetailsClass);
        }

        [TestMethod]
        public void RenderTextareaWithLight()
        {
            //Act
            var cut = RenderComponent<MTextarea>(props =>
            {
                props.Add(textarea => textarea.Light, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("theme--light");
            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderTextareaWithLoaderHeight()
        {
            //Act
            var cut = RenderComponent<MTextarea>(props =>
            {
                props.Add(textarea => textarea.LoaderHeight, 2);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLoaderHeightClass = classes.Contains("m-textarea");

            // Assert
            Assert.IsTrue(hasLoaderHeightClass);
        }

        [TestMethod]
        public void RenderTextareaWithLoading()
        {
            //Act
            var cut = RenderComponent<MTextarea>(props =>
            {
                props.Add(textarea => textarea.Loading, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLoadingClass = classes.Contains("m-textarea");
            // Assert
            Assert.IsTrue(hasLoadingClass);
        }

        [TestMethod]
        public void RenderTextareaWithNoResize()
        {
            //Act
            var cut = RenderComponent<MTextarea>(props =>
            {
                props.Add(textarea => textarea.NoResize, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasNoResizeClass = classes.Contains("m-textarea--no-resize");
            // Assert
            Assert.IsTrue(hasNoResizeClass);
        }

        [TestMethod]
        public void RenderTextareaWithOutlined()
        {
            //Act
            var cut = RenderComponent<MTextarea>(props =>
            {
                props.Add(textarea => textarea.Outlined, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasOutlinedClass = classes.Contains("m-textarea");
            // Assert
            Assert.IsTrue(hasOutlinedClass);
        }

        [TestMethod]
        public void RenderTextareaWithPersistentHint()
        {
            //Act
            var cut = RenderComponent<MTextarea>(props =>
            {
                props.Add(textarea => textarea.PersistentHint, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasPersistentHintClass = classes.Contains("m-textarea");
            // Assert
            Assert.IsTrue(hasPersistentHintClass);
        }

        [TestMethod]
        public void RenderTextareaWithPersistentPlaceholder()
        {
            //Act
            var cut = RenderComponent<MTextarea>(props =>
            {
                props.Add(textarea => textarea.PersistentPlaceholder, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasPersistentPlaceholderClass = classes.Contains("m-textarea");
            // Assert
            Assert.IsTrue(hasPersistentPlaceholderClass);
        }

        [TestMethod]
        public void RenderTextareaWithReadonly()
        {
            //Act
            var cut = RenderComponent<MTextarea>(props =>
            {
                props.Add(textarea => textarea.Readonly, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasReadonlyClass = classes.Contains("m-textarea");
            // Assert
            Assert.IsTrue(hasReadonlyClass);
        }

        [TestMethod]
        public void RenderTextareaWithReverse()
        {
            //Act
            var cut = RenderComponent<MTextarea>(props =>
            {
                props.Add(textarea => textarea.Reverse, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasReverseClass = classes.Contains("m-textarea");
            // Assert
            Assert.IsTrue(hasReverseClass);
        }

        [TestMethod]
        public void RenderTextareaWithRounded()
        {
            //Act
            var cut = RenderComponent<MTextarea>(props =>
            {
                props.Add(textarea => textarea.Rounded, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasRoundedClass = classes.Contains("m-textarea");
            // Assert
            Assert.IsTrue(hasRoundedClass);
        }

        [TestMethod]
        public void RenderTextareaWithRowHeight()
        {
            //Act
            var cut = RenderComponent<MTextarea>(props =>
            {
                props.Add(textarea => textarea.RowHeight, 24);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasRowHeightClass = classes.Contains("m-textarea");

            // Assert
            Assert.IsTrue(hasRowHeightClass);
        }

        [TestMethod]
        public void RenderTextareaWithRows()
        {
            //Act
            var cut = RenderComponent<MTextarea>(props =>
            {
                props.Add(textarea => textarea.Rows, 5);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasRowsClass = classes.Contains("m-textarea");

            // Assert
            Assert.IsTrue(hasRowsClass);
        }

        [TestMethod]
        public void RenderTextareaWithShaped()
        {
            //Act
            var cut = RenderComponent<MTextarea>(props =>
            {
                props.Add(textarea => textarea.Shaped, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasShapedClass = classes.Contains("m-textarea");
            // Assert
            Assert.IsTrue(hasShapedClass);
        }

        [TestMethod]
        public void RenderTextareaWithSingleLine()
        {
            //Act
            var cut = RenderComponent<MTextarea>(props =>
            {
                props.Add(textarea => textarea.SingleLine, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSingleLineClass = classes.Contains("m-textarea");
            // Assert
            Assert.IsTrue(hasSingleLineClass);
        }

        [TestMethod]
        public void RenderTextareaWithSolo()
        {
            //Act
            var cut = RenderComponent<MTextarea>(props =>
            {
                props.Add(textarea => textarea.Solo, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSoloClass = classes.Contains("m-textarea");
            // Assert
            Assert.IsTrue(hasSoloClass);
        }

        [TestMethod]
        public void RenderTextareaWithSoloInverted()
        {
            //Act
            var cut = RenderComponent<MTextarea>(props =>
            {
                props.Add(textarea => textarea.SoloInverted, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSoloInvertedClass = classes.Contains("m-textarea");
            // Assert
            Assert.IsTrue(hasSoloInvertedClass);
        }

        [TestMethod]
        public void RenderTextareaWithSuccess()
        {
            //Act
            var cut = RenderComponent<MTextarea>(props =>
            {
                props.Add(textarea => textarea.Success, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSuccessClass = classes.Contains("m-textarea");
            // Assert
            Assert.IsTrue(hasSuccessClass);
        }

        [TestMethod]
        public void RenderTextareaWithValidateOnBlur()
        {
            //Act
            var cut = RenderComponent<MTextarea>(props =>
            {
                props.Add(textarea => textarea.ValidateOnBlur, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasValidateOnBlurClass = classes.Contains("m-textarea");
            // Assert
            Assert.IsTrue(hasValidateOnBlurClass);
        }
    }
}
