using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;



namespace Masa.Blazor.Test.Select
{
    [TestClass]
    public class MSelectTests : TestBase
    {
        [TestMethod]
        public void RenderSelectWithAutofocus()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MSelect<string, string, string>>(props =>
            {
                props.Add(select => select.Autofocus, true);
                props.Add(select => select.ItemText, item => item);
                props.Add(select => select.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasAutofocusClass = classes.Contains("m-select");

            //Assert
            Assert.IsTrue(hasAutofocusClass);
        }

        [TestMethod]
        public void RenderSelectWithChips()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MSelect<string, string, string>>(props =>
            {
                props.Add(select => select.Chips, true);
                props.Add(select => select.ItemText, item => item);
                props.Add(select => select.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasChipsClass = classes.Contains("m-select");

            //Assert
            Assert.IsTrue(hasChipsClass);
        }

        [TestMethod]
        public void RenderSelectWithClearable()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MSelect<string, string, string>>(props =>
            {
                props.Add(select => select.Clearable, true);
                props.Add(select => select.ItemText, item => item);
                props.Add(select => select.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasClearableClass = classes.Contains("m-select");

            //Assert
            Assert.IsTrue(hasClearableClass);
        }

        [TestMethod]
        public void RenderSelectWithCounter()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MSelect<string, string, string>>(props =>
            {
                props.Add(select => select.Counter, true);
                props.Add(select => select.ItemText, item => item);
                props.Add(select => select.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasCounterClass = classes.Contains("m-select");

            //Assert
            Assert.IsTrue(hasCounterClass);
        }

        [TestMethod]
        public void RenderSelectWithDark()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MSelect<string, string, string>>(props =>
            {
                props.Add(select => select.Dark, true);
                props.Add(select => select.ItemText, item => item);
                props.Add(select => select.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("m-select");

            //Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderSelectWithDeletableChips()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MSelect<string, string, string>>(props =>
            {
                props.Add(select => select.DeletableChips, true);
                props.Add(select => select.ItemText, item => item);
                props.Add(select => select.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDeletableChipsClass = classes.Contains("m-select");

            //Assert
            Assert.IsTrue(hasDeletableChipsClass);
        }

        [TestMethod]
        public void RenderSelectWithDense()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MSelect<string, string, string>>(props =>
            {
                props.Add(select => select.Dense, true);
                props.Add(select => select.ItemText, item => item);
                props.Add(select => select.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDenseClass = classes.Contains("m-select");

            //Assert
            Assert.IsTrue(hasDenseClass);
        }

        [TestMethod]
        public void RenderSelectWithDisabled()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MSelect<string, string, string>>(props =>
            {
                props.Add(select => select.Disabled, true);
                props.Add(select => select.ItemText, item => item);
                props.Add(select => select.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDisabledClass = classes.Contains("m-select");

            //Assert
            Assert.IsTrue(hasDisabledClass);
        }

        [TestMethod]
        public void RenderSelectWithError()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MSelect<string, string, string>>(props =>
            {
                props.Add(select => select.Error, true);
                props.Add(select => select.ItemText, item => item);
                props.Add(select => select.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasErrorClass = classes.Contains("m-select");

            //Assert
            Assert.IsTrue(hasErrorClass);
        }

        [TestMethod]
        public void RenderSelectWithErrorCount()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MSelect<string, string, string>>(props =>
            {
                props.Add(select => select.ErrorCount, 1);
                props.Add(select => select.ItemText, item => item);
                props.Add(select => select.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasErrorCountClass = classes.Contains("m-select");

            // Assert
            Assert.IsTrue(hasErrorCountClass);
        }

        [TestMethod]
        public void RenderSelectWithFilled()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MSelect<string, string, string>>(props =>
            {
                props.Add(select => select.Filled, true);
                props.Add(select => select.ItemText, item => item);
                props.Add(select => select.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFilledClass = classes.Contains("m-select");

            //Assert
            Assert.IsTrue(hasFilledClass);
        }

        [TestMethod]
        public void RenderSelectWithFlat()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MSelect<string, string, string>>(props =>
            {
                props.Add(select => select.Flat, true);
                props.Add(select => select.ItemText, item => item);
                props.Add(select => select.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFlatClass = classes.Contains("m-select");

            //Assert
            Assert.IsTrue(hasFlatClass);
        }

        [TestMethod]
        public void RenderSelectWithFullWidth()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MSelect<string, string, string>>(props =>
            {
                props.Add(select => select.FullWidth, true);
                props.Add(select => select.ItemText, item => item);
                props.Add(select => select.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFullWidthClass = classes.Contains("m-select");

            //Assert
            Assert.IsTrue(hasFullWidthClass);
        }

        [TestMethod]
        public void RenderSelectWithHeight()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MSelect<string, string, string>>(props =>
            {
                props.Add(select => select.Height, 1);
                props.Add(select => select.ItemText, item => item);
                props.Add(select => select.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasHeightClass = classes.Contains("m-select");

            // Assert
            Assert.IsTrue(hasHeightClass);
        }

        [TestMethod]
        public void RenderSelectWithHideDetails()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MSelect<string, string, string>>(props =>
            {
                props.Add(select => select.HideDetails, true);
                props.Add(select => select.ItemText, item => item);
                props.Add(select => select.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasHideDetailsClass = classes.Contains("m-select");

            //Assert
            Assert.IsTrue(hasHideDetailsClass);
        }

        [TestMethod]
        public void RenderSelectWithLight()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MSelect<string, string, string>>(props =>
            {
                props.Add(select => select.Light, true);
                props.Add(select => select.ItemText, item => item);
                props.Add(select => select.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("m-select");

            //Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderSelectWithLoaderHeight()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MSelect<string, string, string>>(props =>
            {
                props.Add(select => select.LoaderHeight, 2);
                props.Add(select => select.ItemText, item => item);
                props.Add(select => select.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLoaderHeightClass = classes.Contains("m-select");

            // Assert
            Assert.IsTrue(hasLoaderHeightClass);
        }

        [TestMethod]
        public void RenderSelectWithLoading()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MSelect<string, string, string>>(props =>
            {
                props.Add(select => select.Loading, true);
                props.Add(select => select.ItemText, item => item);
                props.Add(select => select.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLoadingClass = classes.Contains("m-select");

            //Assert
            Assert.IsTrue(hasLoadingClass);
        }

        [TestMethod]
        public void RenderSelectWithMultiple()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MSelect<string, string, string>>(props =>
            {
                props.Add(select => select.Multiple, true);
                props.Add(select => select.ItemText, item => item);
                props.Add(select => select.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasMultipleClass = classes.Contains("m-select");

            //Assert
            Assert.IsTrue(hasMultipleClass);
        }

        [TestMethod]
        public void RenderSelectWithOutlined()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MSelect<string, string, string>>(props =>
            {
                props.Add(select => select.Outlined, true);
                props.Add(select => select.ItemText, item => item);
                props.Add(select => select.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasOutlinedClass = classes.Contains("m-select");

            //Assert
            Assert.IsTrue(hasOutlinedClass);
        }

        [TestMethod]
        public void RenderSelectWithPersistentHint()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MSelect<string, string, string>>(props =>
            {
                props.Add(select => select.PersistentHint, true);
                props.Add(select => select.ItemText, item => item);
                props.Add(select => select.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasPersistentHintClass = classes.Contains("m-select");

            //Assert
            Assert.IsTrue(hasPersistentHintClass);
        }

        [TestMethod]
        public void RenderSelectWithPersistentPlaceholder()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MSelect<string, string, string>>(props =>
            {
                props.Add(select => select.PersistentPlaceholder, true);
                props.Add(select => select.ItemText, item => item);
                props.Add(select => select.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasPersistentPlaceholderClass = classes.Contains("m-select");

            //Assert
            Assert.IsTrue(hasPersistentPlaceholderClass);
        }

        [TestMethod]
        public void RenderSelectWithReadonly()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MSelect<string, string, string>>(props =>
            {
                props.Add(select => select.Readonly, true);
                props.Add(select => select.ItemText, item => item);
                props.Add(select => select.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasReadonlyClass = classes.Contains("m-select");

            //Assert
            Assert.IsTrue(hasReadonlyClass);
        }

        [TestMethod]
        public void RenderSelectWithReverse()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MSelect<string, string, string>>(props =>
            {
                props.Add(select => select.Reverse, true);
                props.Add(select => select.ItemText, item => item);
                props.Add(select => select.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasReverseClass = classes.Contains("m-select");

            //Assert
            Assert.IsTrue(hasReverseClass);
        }

        [TestMethod]
        public void RenderSelectWithRounded()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MSelect<string, string, string>>(props =>
            {
                props.Add(select => select.Rounded, true);
                props.Add(select => select.ItemText, item => item);
                props.Add(select => select.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasRoundedClass = classes.Contains("m-select");

            //Assert
            Assert.IsTrue(hasRoundedClass);
        }

        [TestMethod]
        public void RenderSelectWithShaped()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MSelect<string, string, string>>(props =>
            {
                props.Add(select => select.Shaped, true);
                props.Add(select => select.ItemText, item => item);
                props.Add(select => select.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasShapedClass = classes.Contains("m-select");

            //Assert
            Assert.IsTrue(hasShapedClass);
        }

        [TestMethod]
        public void RenderSelectWithSingleLine()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MSelect<string, string, string>>(props =>
            {
                props.Add(select => select.SingleLine, true);
                props.Add(select => select.ItemText, item => item);
                props.Add(select => select.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSingleLineClass = classes.Contains("m-select");

            //Assert
            Assert.IsTrue(hasSingleLineClass);
        }

        [TestMethod]
        public void RenderSelectWithSmallChips()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MSelect<string, string, string>>(props =>
            {
                props.Add(select => select.SmallChips, true);
                props.Add(select => select.ItemText, item => item);
                props.Add(select => select.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSmallChipsClass = classes.Contains("m-select");

            //Assert
            Assert.IsTrue(hasSmallChipsClass);
        }

        [TestMethod]
        public void RenderSelectWithSolo()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MSelect<string, string, string>>(props =>
            {
                props.Add(select => select.Solo, true);
                props.Add(select => select.ItemText, item => item);
                props.Add(select => select.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSoloClass = classes.Contains("m-select");

            //Assert
            Assert.IsTrue(hasSoloClass);
        }

        [TestMethod]
        public void RenderSelectWithSoloInverted()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MSelect<string, string, string>>(props =>
            {
                props.Add(select => select.SoloInverted, true);
                props.Add(select => select.ItemText, item => item);
                props.Add(select => select.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSoloInvertedClass = classes.Contains("m-select");

            //Assert
            Assert.IsTrue(hasSoloInvertedClass);
        }

        [TestMethod]
        public void RenderSelectWithSuccess()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MSelect<string, string, string>>(props =>
            {
                props.Add(select => select.Success, true);
                props.Add(select => select.ItemText, item => item);
                props.Add(select => select.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSuccessClass = classes.Contains("m-select");

            //Assert
            Assert.IsTrue(hasSuccessClass);
        }

        [TestMethod]
        public void RenderSelectWithValidateOnBlur()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MSelect<string, string, string>>(props =>
            {
                props.Add(select => select.ValidateOnBlur, true);
                props.Add(select => select.ItemText, item => item);
                props.Add(select => select.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasValidateOnBlurClass = classes.Contains("m-select");

            //Assert
            Assert.IsTrue(hasValidateOnBlurClass);
        }
    }
}
