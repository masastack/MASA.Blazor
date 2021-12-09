using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bunit;
using BlazorComponent;
using Moq;

namespace MASA.Blazor.Test.Autocompletes
{
    [TestClass]
    public class MAutocompleteTests:TestBase
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

        [TestMethod]
        public void RenderAutocompleteWithAppendIcon()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MAutocomplete<string, string, string>>(props =>
            {
                string appendicon="mdi-star";
                props.Add(autocomplete => autocomplete.AppendIcon, appendicon);
                props.Add(autocomplete => autocomplete.ItemText, item => item);
                props.Add(autocomplete => autocomplete.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasAppendIconClass = classes.Contains("m-autocomplete");

            // Assert
            Assert.IsTrue(hasAppendIconClass);
        }

        [TestMethod]
        public void RenderAutocompleteWithAppendOuterIcon()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MAutocomplete<string, string, string>>(props =>
            {
                string appendoutericon = "mdi-star";
                props.Add(autocomplete => autocomplete.AppendOuterIcon, appendoutericon);
                props.Add(autocomplete => autocomplete.ItemText, item => item);
                props.Add(autocomplete => autocomplete.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasAppendOuterIconClass = classes.Contains("m-autocomplete");

            // Assert
            Assert.IsTrue(hasAppendOuterIconClass);
        }

        [TestMethod]
        public void RenderAutocompleteWithBackgroundColor()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MAutocomplete<string, string, string>>(props =>
            {
                string backgroundcolor = "mdi-star";
                props.Add(autocomplete => autocomplete.BackgroundColor, backgroundcolor);
                props.Add(autocomplete => autocomplete.ItemText, item => item);
                props.Add(autocomplete => autocomplete.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasBackgroundColorClass = classes.Contains("m-autocomplete");

            // Assert
            Assert.IsTrue(hasBackgroundColorClass);
        }

        [TestMethod]
        public void RenderAutocompleteWithClearIcon()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MAutocomplete<string, string, string>>(props =>
            {
                string clearicon = "mdi-star";
                props.Add(autocomplete => autocomplete.ClearIcon, clearicon);
                props.Add(autocomplete => autocomplete.ItemText, item => item);
                props.Add(autocomplete => autocomplete.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasClearIconClass = classes.Contains("m-autocomplete");

            // Assert
            Assert.IsTrue(hasClearIconClass);
        }

        [TestMethod]
        public void RenderAutocompleteWithColor()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MAutocomplete<string, string, string>>(props =>
            {
                string color = "mdi-star";
                props.Add(autocomplete => autocomplete.Color, color);
                props.Add(autocomplete => autocomplete.ItemText, item => item);
                props.Add(autocomplete => autocomplete.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasColorClass = classes.Contains("m-autocomplete");

            // Assert
            Assert.IsTrue(hasColorClass);
        }

        [TestMethod]
        public void RenderAutocompleteWithErrorCount()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MAutocomplete<string, string, string>>(props =>
            {
                props.Add(autocomplete => autocomplete.ErrorCount, 1);
                props.Add(autocomplete => autocomplete.ItemText, item => item);
                props.Add(autocomplete => autocomplete.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasErrorCountClass = classes.Contains("m-autocomplete");

            // Assert
            Assert.IsTrue(hasErrorCountClass);
        }

        [TestMethod]
        public void RenderAutocompleteWithHideNoData()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MAutocomplete<string, string, string>>(props =>
            {
                props.Add(autocomplete => autocomplete.HideNoData, true);
                props.Add(autocomplete => autocomplete.ItemText, item => item);
                props.Add(autocomplete => autocomplete.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasHideNoDataClass = classes.Contains("m-autocomplete");

            // Assert
            Assert.IsTrue(hasHideNoDataClass);
        }

        [TestMethod]
        public void RenderAutocompleteWithHideSelected()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MAutocomplete<string, string, string>>(props =>
            {
                props.Add(autocomplete => autocomplete.HideSelected, true);
                props.Add(autocomplete => autocomplete.ItemText, item => item);
                props.Add(autocomplete => autocomplete.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasHideSelectedClass = classes.Contains("m-autocomplete");

            // Assert
            Assert.IsTrue(hasHideSelectedClass);
        }

        [TestMethod]
        public void RenderAutocompleteWithHint()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MAutocomplete<string, string, string>>(props =>
            {
                string hint = "hint";
                props.Add(autocomplete => autocomplete.Hint, hint);
                props.Add(autocomplete => autocomplete.ItemText, item => item);
                props.Add(autocomplete => autocomplete.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasHintClass = classes.Contains("m-autocomplete");

            // Assert
            Assert.IsTrue(hasHintClass);
        }

        [TestMethod]
        public void RenderAutocompleteWithId()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MAutocomplete<string, string, string>>(props =>
            {
                string id = "id";
                props.Add(autocomplete => autocomplete.Id, id);
                props.Add(autocomplete => autocomplete.ItemText, item => item);
                props.Add(autocomplete => autocomplete.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasIdClass = classes.Contains("m-autocomplete");

            // Assert
            Assert.IsTrue(hasIdClass);
        }

        [TestMethod]
        public void RenderAutocompleteWithLabel()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MAutocomplete<string, string, string>>(props =>
            {
                string label = "label";
                props.Add(autocomplete => autocomplete.Label, label);
                props.Add(autocomplete => autocomplete.ItemText, item => item);
                props.Add(autocomplete => autocomplete.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLabelClass = classes.Contains("m-autocomplete");

            // Assert
            Assert.IsTrue(hasLabelClass);
        }

        [TestMethod]
        public void RenderAutocompleteWithPlaceholder()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MAutocomplete<string, string, string>>(props =>
            {
                string p = "autocomplete";
                props.Add(autocomplete => autocomplete.Placeholder, p);
                props.Add(autocomplete => autocomplete.ItemText, item => item);
                props.Add(autocomplete => autocomplete.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasPlaceholderClass = classes.Contains("m-autocomplete");

            // Assert
            Assert.IsTrue(hasPlaceholderClass);
        }

        [TestMethod]
        public void RenderAutocompleteWithPrefix()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MAutocomplete<string, string, string>>(props =>
            {
                string p = "autocomplete";
                props.Add(autocomplete => autocomplete.Prefix, p);
                props.Add(autocomplete => autocomplete.ItemText, item => item);
                props.Add(autocomplete => autocomplete.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasPrefixClass = classes.Contains("m-autocomplete");

            // Assert
            Assert.IsTrue(hasPrefixClass);
        }

        [TestMethod]
        public void RenderAutocompleteWithPrependIcon()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MAutocomplete<string, string, string>>(props =>
            {
                string p = "mdi-star";
                props.Add(autocomplete => autocomplete.PrependIcon, p);
                props.Add(autocomplete => autocomplete.ItemText, item => item);
                props.Add(autocomplete => autocomplete.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasPrependIconClass = classes.Contains("m-autocomplete");

            // Assert
            Assert.IsTrue(hasPrependIconClass);
        }

        [TestMethod]
        public void RenderAutocompleteWithPrependInnerIcon()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MAutocomplete<string, string, string>>(props =>
            {
                string p = "mdi-star";
                props.Add(autocomplete => autocomplete.PrependInnerIcon, p);
                props.Add(autocomplete => autocomplete.ItemText, item => item);
                props.Add(autocomplete => autocomplete.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasPrependInnerIconClass = classes.Contains("m-autocomplete");

            // Assert
            Assert.IsTrue(hasPrependInnerIconClass);
        }

        [TestMethod]
        public void RenderAutocompleteWithSuffix()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MAutocomplete<string, string, string>>(props =>
            {
                string p = "mdi-star";
                props.Add(autocomplete => autocomplete.Suffix, p);
                props.Add(autocomplete => autocomplete.ItemText, item => item);
                props.Add(autocomplete => autocomplete.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSuffixClass = classes.Contains("m-autocomplete");

            // Assert
            Assert.IsTrue(hasSuffixClass);
        }

        [TestMethod]
        public void RenderAutocompleteWithType()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MAutocomplete<string, string, string>>(props =>
            {
                string p = "mdi-star";
                props.Add(autocomplete => autocomplete.Type, p);
                props.Add(autocomplete => autocomplete.ItemText, item => item);
                props.Add(autocomplete => autocomplete.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasTypeClass = classes.Contains("m-autocomplete");

            // Assert
            Assert.IsTrue(hasTypeClass);
        }

        [TestMethod]
        public void RenderAutocompleteWithValue()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MAutocomplete<string, string, string>>(props =>
            {
                string p = "mdi-star";
                props.Add(autocomplete => autocomplete.Value, p);
                props.Add(autocomplete => autocomplete.ItemText, item => item);
                props.Add(autocomplete => autocomplete.ItemValue, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasValueClass = classes.Contains("m-autocomplete");

            // Assert
            Assert.IsTrue(hasValueClass);
        }
    }
}
