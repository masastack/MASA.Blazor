using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Masa.Blazor.Test.Autocompletes
{
    [TestClass]
    public class MAutocompleteTests : TestBase
    {
        [TestMethod]
        public void RenderAutocompleteWithAutofocus()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MAutocomplete<string, string, string>>(props =>
              {
                  props.Add(autocomplete => autocomplete.Autofocus, true);
                  props.Add(autocomplete => autocomplete.ItemText, item => item);
                  props.Add(autocomplete => autocomplete.ItemValue, item => item);
              });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasAutofocusClass = classes.Contains("m-autocomplete");

            // Assert
            Assert.IsTrue(hasAutofocusClass);
        }

        [TestMethod]
        public void RenderAutocompleteWithChips()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MAutocomplete<string, string, string>>(props =>
            {
                props.Add(autocomplete => autocomplete.Chips, true);
                props.Add(autocomplete => autocomplete.ItemText, item => item);
                props.Add(autocomplete => autocomplete.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasChipsClass = classes.Contains("m-autocomplete");

            // Assert
            Assert.IsTrue(hasChipsClass);
        }

        [TestMethod]
        public void RenderAutocompleteWithClearable()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MAutocomplete<string, string, string>>(props =>
            {
                props.Add(autocomplete => autocomplete.Clearable, true);
                props.Add(autocomplete => autocomplete.ItemText, item => item);
                props.Add(autocomplete => autocomplete.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasClearableClass = classes.Contains("m-autocomplete");

            // Assert
            Assert.IsTrue(hasClearableClass);
        }

        [TestMethod]
        public void RenderAutocompleteWithCounter()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MAutocomplete<string, string, string>>(props =>
            {
                props.Add(autocomplete => autocomplete.Counter, true);
                props.Add(autocomplete => autocomplete.ItemText, item => item);
                props.Add(autocomplete => autocomplete.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasCounterClass = classes.Contains("m-autocomplete");

            // Assert
            Assert.IsTrue(hasCounterClass);
        }

        [TestMethod]
        public void RenderAutocompleteWithDark()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MAutocomplete<string, string, string>>(props =>
            {
                props.Add(autocomplete => autocomplete.Dark, true);
                props.Add(autocomplete => autocomplete.ItemText, item => item);
                props.Add(autocomplete => autocomplete.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("m-autocomplete");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderAutocompleteWithDeletableChips()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MAutocomplete<string, string, string>>(props =>
            {
                props.Add(autocomplete => autocomplete.DeletableChips, true);
                props.Add(autocomplete => autocomplete.ItemText, item => item);
                props.Add(autocomplete => autocomplete.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDeletableChipsClass = classes.Contains("m-autocomplete");

            // Assert
            Assert.IsTrue(hasDeletableChipsClass);
        }

        [TestMethod]
        public void RenderAutocompleteWithDense()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MAutocomplete<string, string, string>>(props =>
            {
                props.Add(autocomplete => autocomplete.Dense, true);
                props.Add(autocomplete => autocomplete.ItemText, item => item);
                props.Add(autocomplete => autocomplete.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDenseClass = classes.Contains("m-autocomplete");

            // Assert
            Assert.IsTrue(hasDenseClass);
        }

        [TestMethod]
        public void RenderAutocompleteWithDisabled()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MAutocomplete<string, string, string>>(props =>
            {
                props.Add(autocomplete => autocomplete.Disabled, true);
                props.Add(autocomplete => autocomplete.ItemText, item => item);
                props.Add(autocomplete => autocomplete.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDisabledClass = classes.Contains("m-autocomplete");

            // Assert
            Assert.IsTrue(hasDisabledClass);
        }

        [TestMethod]
        public void RenderAutocompleteWithError()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MAutocomplete<string, string, string>>(props =>
            {
                props.Add(autocomplete => autocomplete.Error, true);
                props.Add(autocomplete => autocomplete.ItemText, item => item);
                props.Add(autocomplete => autocomplete.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasErrorClass = classes.Contains("m-autocomplete");

            // Assert
            Assert.IsTrue(hasErrorClass);
        }

        [TestMethod]
        public void RenderAutocompleteWithFilled()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MAutocomplete<string, string, string>>(props =>
            {
                props.Add(autocomplete => autocomplete.Filled, true);
                props.Add(autocomplete => autocomplete.ItemText, item => item);
                props.Add(autocomplete => autocomplete.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFilledClass = classes.Contains("m-autocomplete");

            // Assert
            Assert.IsTrue(hasFilledClass);
        }

        [TestMethod]
        public void RenderAutocompleteWithFlat()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MAutocomplete<string, string, string>>(props =>
            {
                props.Add(autocomplete => autocomplete.Flat, true);
                props.Add(autocomplete => autocomplete.ItemText, item => item);
                props.Add(autocomplete => autocomplete.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFlatClass = classes.Contains("m-autocomplete");

            // Assert
            Assert.IsTrue(hasFlatClass);
        }

        [TestMethod]
        public void RenderAutocompleteWithFullWidth()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MAutocomplete<string, string, string>>(props =>
            {
                props.Add(autocomplete => autocomplete.FullWidth, true);
                props.Add(autocomplete => autocomplete.ItemText, item => item);
                props.Add(autocomplete => autocomplete.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFullWidthClass = classes.Contains("m-autocomplete");

            // Assert
            Assert.IsTrue(hasFullWidthClass);
        }

        [TestMethod]
        public void RenderWithHeight()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MAutocomplete<string, string, string>>(props =>
            {
                props.Add(autocomplete => autocomplete.Height, 100);
                props.Add(autocomplete => autocomplete.ItemText, item => item);
                props.Add(autocomplete => autocomplete.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasHeightClass = classes.Contains("m-autocomplete");

            // Assert
            Assert.IsTrue(hasHeightClass);
        }

        [TestMethod]
        public void RenderAutocompleteWithHideDetails()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MAutocomplete<string, string, string>>(props =>
            {
                props.Add(autocomplete => autocomplete.HideDetails, true);
                props.Add(autocomplete => autocomplete.ItemText, item => item);
                props.Add(autocomplete => autocomplete.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasHideDetailsClass = classes.Contains("m-autocomplete");

            // Assert
            Assert.IsTrue(hasHideDetailsClass);
        }

        [TestMethod]
        public void RenderAutocompleteWithLight()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MAutocomplete<string, string, string>>(props =>
            {
                props.Add(autocomplete => autocomplete.Light, true);
                props.Add(autocomplete => autocomplete.ItemText, item => item);
                props.Add(autocomplete => autocomplete.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("m-autocomplete");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderAutocompleteWithLoaderHeight()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MAutocomplete<string, string, string>>(props =>
            {
                props.Add(autocomplete => autocomplete.LoaderHeight, 2);
                props.Add(autocomplete => autocomplete.ItemText, item => item);
                props.Add(autocomplete => autocomplete.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLoaderHeightClass = classes.Contains("m-autocomplete");

            // Assert
            Assert.IsTrue(hasLoaderHeightClass);
        }

        [TestMethod]
        public void RenderAutocompleteWithLoading()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MAutocomplete<string, string, string>>(props =>
            {
                props.Add(autocomplete => autocomplete.Loading, true);
                props.Add(autocomplete => autocomplete.ItemText, item => item);
                props.Add(autocomplete => autocomplete.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLoadingClass = classes.Contains("m-autocomplete");

            // Assert
            Assert.IsTrue(hasLoadingClass);
        }

        [TestMethod]
        public void RenderAutocompleteWithMultiple()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MAutocomplete<string, string, string>>(props =>
            {
                props.Add(autocomplete => autocomplete.Multiple, true);
                props.Add(autocomplete => autocomplete.ItemText, item => item);
                props.Add(autocomplete => autocomplete.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasMultipleClass = classes.Contains("m-autocomplete");

            // Assert
            Assert.IsTrue(hasMultipleClass);
        }

        [TestMethod]
        public void RenderAutocompleteWithOutlined()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MAutocomplete<string, string, string>>(props =>
            {
                props.Add(autocomplete => autocomplete.Outlined, true);
                props.Add(autocomplete => autocomplete.ItemText, item => item);
                props.Add(autocomplete => autocomplete.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasOutlinedClass = classes.Contains("m-autocomplete");

            // Assert
            Assert.IsTrue(hasOutlinedClass);
        }

        [TestMethod]
        public void RenderAutocompleteWithPersistentHint()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MAutocomplete<string, string, string>>(props =>
            {
                props.Add(autocomplete => autocomplete.PersistentHint, true);
                props.Add(autocomplete => autocomplete.ItemText, item => item);
                props.Add(autocomplete => autocomplete.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasPersistentHintClass = classes.Contains("m-autocomplete");

            // Assert
            Assert.IsTrue(hasPersistentHintClass);
        }

        [TestMethod]
        public void RenderAutocompleteWithPersistentPlaceholder()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MAutocomplete<string, string, string>>(props =>
            {
                props.Add(autocomplete => autocomplete.PersistentPlaceholder, true);
                props.Add(autocomplete => autocomplete.ItemText, item => item);
                props.Add(autocomplete => autocomplete.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasPersistentPlaceholderClass = classes.Contains("m-autocomplete");

            // Assert
            Assert.IsTrue(hasPersistentPlaceholderClass);
        }

        [TestMethod]
        public void RenderAutocompleteWithReadonly()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MAutocomplete<string, string, string>>(props =>
            {
                props.Add(autocomplete => autocomplete.Readonly, true);
                props.Add(autocomplete => autocomplete.ItemText, item => item);
                props.Add(autocomplete => autocomplete.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasReadonlyClass = classes.Contains("m-autocomplete");

            // Assert
            Assert.IsTrue(hasReadonlyClass);
        }

        [TestMethod]
        public void RenderAutocompleteWithReverse()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MAutocomplete<string, string, string>>(props =>
            {
                props.Add(autocomplete => autocomplete.Reverse, true);
                props.Add(autocomplete => autocomplete.ItemText, item => item);
                props.Add(autocomplete => autocomplete.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasReverseClass = classes.Contains("m-autocomplete");

            // Assert
            Assert.IsTrue(hasReverseClass);
        }

        [TestMethod]
        public void RenderAutocompleteWithRounded()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MAutocomplete<string, string, string>>(props =>
            {
                props.Add(autocomplete => autocomplete.Rounded, true);
                props.Add(autocomplete => autocomplete.ItemText, item => item);
                props.Add(autocomplete => autocomplete.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasRoundedClass = classes.Contains("m-autocomplete");

            // Assert
            Assert.IsTrue(hasRoundedClass);
        }

        [TestMethod]
        public void RenderAutocompleteWithShaped()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MAutocomplete<string, string, string>>(props =>
            {
                props.Add(autocomplete => autocomplete.Shaped, true);
                props.Add(autocomplete => autocomplete.ItemText, item => item);
                props.Add(autocomplete => autocomplete.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasShapedClass = classes.Contains("m-autocomplete");

            // Assert
            Assert.IsTrue(hasShapedClass);
        }

        [TestMethod]
        public void RenderAutocompleteWithSingleLine()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MAutocomplete<string, string, string>>(props =>
            {
                props.Add(autocomplete => autocomplete.SingleLine, true);
                props.Add(autocomplete => autocomplete.ItemText, item => item);
                props.Add(autocomplete => autocomplete.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSingleLineClass = classes.Contains("m-autocomplete");

            // Assert
            Assert.IsTrue(hasSingleLineClass);
        }

        [TestMethod]
        public void RenderAutocompleteWithSmallChips()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MAutocomplete<string, string, string>>(props =>
            {
                props.Add(autocomplete => autocomplete.SmallChips, true);
                props.Add(autocomplete => autocomplete.ItemText, item => item);
                props.Add(autocomplete => autocomplete.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSmallChipsClass = classes.Contains("m-autocomplete");

            // Assert
            Assert.IsTrue(hasSmallChipsClass);
        }

        [TestMethod]
        public void RenderAutocompleteWithSolo()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MAutocomplete<string, string, string>>(props =>
            {
                props.Add(autocomplete => autocomplete.Solo, true);
                props.Add(autocomplete => autocomplete.ItemText, item => item);
                props.Add(autocomplete => autocomplete.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSoloClass = classes.Contains("m-autocomplete");

            // Assert
            Assert.IsTrue(hasSoloClass);
        }

        [TestMethod]
        public void RenderAutocompleteWithSoloInverted()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MAutocomplete<string, string, string>>(props =>
            {
                props.Add(autocomplete => autocomplete.SoloInverted, true);
                props.Add(autocomplete => autocomplete.ItemText, item => item);
                props.Add(autocomplete => autocomplete.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSoloInvertedClass = classes.Contains("m-autocomplete");

            // Assert
            Assert.IsTrue(hasSoloInvertedClass);
        }

        [TestMethod]
        public void RenderAutocompleteWithSuccess()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MAutocomplete<string, string, string>>(props =>
            {
                props.Add(autocomplete => autocomplete.Success, true);
                props.Add(autocomplete => autocomplete.ItemText, item => item);
                props.Add(autocomplete => autocomplete.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSuccessClass = classes.Contains("m-autocomplete");

            // Assert
            Assert.IsTrue(hasSuccessClass);
        }

        [TestMethod]
        public void RenderAutocompleteWithValidateOnBlur()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MAutocomplete<string, string, string>>(props =>
            {
                props.Add(autocomplete => autocomplete.ValidateOnBlur, true);
                props.Add(autocomplete => autocomplete.ItemText, item => item);
                props.Add(autocomplete => autocomplete.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasValidateOnBlurClass = classes.Contains("m-autocomplete");

            // Assert
            Assert.IsTrue(hasValidateOnBlurClass);
        }
    }
}
