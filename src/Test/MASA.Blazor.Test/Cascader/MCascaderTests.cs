using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject.Selection;

namespace MASA.Blazor.Test.Cascader
{
    [TestClass]
    public class MCascaderTests:TestBase
    {
        [TestMethod]
        public void RenderCascaderWithDense()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCascader<string,string>>(props =>
            {
                props.Add(cascader => cascader.Dense, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDenseClass = classes.Contains("m-cascader");

            // Assert
            Assert.IsTrue(hasDenseClass);
        }

        [TestMethod]
        public void RenderCascaderWithAutofocus()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCascader<string, string>>(props =>
            {
                props.Add(cascader => cascader.Autofocus, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasAutofocusClass = classes.Contains("m-cascader");

            // Assert
            Assert.IsTrue(hasAutofocusClass);
        }

        [TestMethod]
        public void RenderCascaderWithChips()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCascader<string, string>>(props =>
            {
                props.Add(cascader => cascader.Chips, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasChipsClass = classes.Contains("m-cascader");

            // Assert
            Assert.IsTrue(hasChipsClass);
        }

        [TestMethod]
        public void RenderCascaderWithClearable()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCascader<string, string>>(props =>
            {
                props.Add(cascader => cascader.Clearable, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasClearableClass = classes.Contains("m-cascader");

            // Assert
            Assert.IsTrue(hasClearableClass);
        }

        [TestMethod]
        public void RenderCascaderWithCounter()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCascader<string, string>>(props =>
            {
                props.Add(cascader => cascader.Counter, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasCounterClass = classes.Contains("m-cascader");

            // Assert
            Assert.IsTrue(hasCounterClass);
        }

        [TestMethod]
        public void RenderCascaderWithDark()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCascader<string, string>>(props =>
            {
                props.Add(cascader => cascader.Dark, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("theme--dark");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderCascaderWithDeletableChips()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCascader<string, string>>(props =>
            {
                props.Add(cascader => cascader.DeletableChips, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDeletableChipsClass = classes.Contains("m-cascader");

            // Assert
            Assert.IsTrue(hasDeletableChipsClass);
        }

        [TestMethod]
        public void RenderCascaderWithDisabled()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCascader<string, string>>(props =>
            {
                props.Add(cascader => cascader.Disabled, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDisabledClass = classes.Contains("m-cascader");

            // Assert
            Assert.IsTrue(hasDisabledClass);
        }

        [TestMethod]
        public void RenderCascaderWithError()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCascader<string, string>>(props =>
            {
                props.Add(cascader => cascader.Error, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasErrorClass = classes.Contains("m-cascader");

            // Assert
            Assert.IsTrue(hasErrorClass);
        }

        [TestMethod]
        public void RenderCascaderWithErrorCount()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCascader<string, string>>(props =>
            {
                props.Add(cascader => cascader.ErrorCount, 1);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasErrorCountClass = classes.Contains("m-cascader");

            // Assert
            Assert.IsTrue(hasErrorCountClass);
        }

        [TestMethod]
        public void RenderCascaderWithFilled()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCascader<string, string>>(props =>
            {
                props.Add(cascader => cascader.Filled, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFilledClass = classes.Contains("m-cascader");

            // Assert
            Assert.IsTrue(hasFilledClass);
        }

        [TestMethod]
        public void RenderCascaderWithFlat()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCascader<string, string>>(props =>
            {
                props.Add(cascader => cascader.Flat, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFlatClass = classes.Contains("m-cascader");

            // Assert
            Assert.IsTrue(hasFlatClass);
        }

        [TestMethod]
        public void RenderCascaderWithFullWidth()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCascader<string, string>>(props =>
            {
                props.Add(cascader => cascader.FullWidth, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFullWidthClass = classes.Contains("m-cascader");

            // Assert
            Assert.IsTrue(hasFullWidthClass);
        }

        [TestMethod]
        public void RenderCascaderWithHeight()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCascader<string, string>>(props =>
            {
                props.Add(cascader => cascader.Height, 24);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasHeightClass = classes.Contains("m-cascader");

             // Assert
            Assert.IsTrue(hasHeightClass);
        }

        [TestMethod]
        public void RenderCascaderWithHideDetails()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCascader<string, string>>(props =>
            {
                props.Add(cascader => cascader.HideDetails, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasHideDetailsClass = classes.Contains("m-cascader");

            // Assert
            Assert.IsTrue(hasHideDetailsClass);
        }

        [TestMethod]
        public void RenderCascaderWithHideNoData()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCascader<string, string>>(props =>
            {
                props.Add(cascader => cascader.HideNoData, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasHideNoDataClass = classes.Contains("m-cascader");

            // Assert
            Assert.IsTrue(hasHideNoDataClass);
        }

        [TestMethod]
        public void RenderCascaderWithHideSelected()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCascader<string, string>>(props =>
            {
                props.Add(cascader => cascader.HideSelected, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasHideSelectedClass = classes.Contains("m-cascader");

            // Assert
            Assert.IsTrue(hasHideSelectedClass);
        }

        [TestMethod]
        public void RenderCascaderWithLight()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCascader<string, string>>(props =>
            {
                props.Add(cascader => cascader.Light, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("theme--light");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderCascaderWithLoaderHeight()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCascader<string, string>>(props =>
            {
                props.Add(cascader => cascader.LoaderHeight, 1);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLoaderHeightClass = classes.Contains("m-cascader");

            // Assert
            Assert.IsTrue(hasLoaderHeightClass);
        }

        [TestMethod]
        public void RenderCascaderWithLoading()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCascader<string, string>>(props =>
            {
                props.Add(cascader => cascader.Loading, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLoadingClass = classes.Contains("m-cascader");

            // Assert
            Assert.IsTrue(hasLoadingClass);
        }

        [TestMethod]
        public void RenderCascaderWithMinWidth()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCascader<string, string>>(props =>
            {
                props.Add(cascader => cascader.MinWidth, 1);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasMinWidthClass = classes.Contains("m-cascader");

            // Assert
            Assert.IsTrue(hasMinWidthClass);
        }

        [TestMethod]
        public void RenderCascaderWithOutlined()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCascader<string, string>>(props =>
            {
                props.Add(cascader => cascader.Outlined, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasOutlinedClass = classes.Contains("m-cascader");

            // Assert
            Assert.IsTrue(hasOutlinedClass);
        }

        [TestMethod]
        public void RenderCascaderWithPersistentHint()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCascader<string, string>>(props =>
            {
                props.Add(cascader => cascader.PersistentHint, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasPersistentHintClass = classes.Contains("m-cascader");

            // Assert
            Assert.IsTrue(hasPersistentHintClass);
        }

        [TestMethod]
        public void RenderCascaderWithPersistentPlaceholder()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCascader<string, string>>(props =>
            {
                props.Add(cascader => cascader.PersistentPlaceholder, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasPersistentPlaceholderClass = classes.Contains("m-cascader");

            // Assert
            Assert.IsTrue(hasPersistentPlaceholderClass);
        }

        [TestMethod]
        public void RenderCascaderWithReadonly()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCascader<string, string>>(props =>
            {
                props.Add(cascader => cascader.Readonly, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasReadonlyClass = classes.Contains("m-cascader");

            // Assert
            Assert.IsTrue(hasReadonlyClass);
        }

        [TestMethod]
        public void RenderCascaderWithReverse()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCascader<string, string>>(props =>
            {
                props.Add(cascader => cascader.Reverse, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasReverseClass = classes.Contains("m-cascader");

            // Assert
            Assert.IsTrue(hasReverseClass);
        }

        [TestMethod]
        public void RenderCascaderWithRounded()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCascader<string, string>>(props =>
            {
                props.Add(cascader => cascader.Rounded, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasRoundedClass = classes.Contains("m-cascader");

            // Assert
            Assert.IsTrue(hasRoundedClass);
        }

        [TestMethod]
        public void RenderCascaderWithShaped()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCascader<string, string>>(props =>
            {
                props.Add(cascader => cascader.Shaped, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasShapedClass = classes.Contains("m-cascader");

            // Assert
            Assert.IsTrue(hasShapedClass);
        }

        [TestMethod]
        public void RenderCascaderWithShowAllLevels()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCascader<string, string>>(props =>
            {
                props.Add(cascader => cascader.ShowAllLevels, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasShowAllLevelsClass = classes.Contains("m-cascader");

            // Assert
            Assert.IsTrue(hasShowAllLevelsClass);
        }

        [TestMethod]
        public void RenderCascaderWithSingleLine()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCascader<string, string>>(props =>
            {
                props.Add(cascader => cascader.SingleLine, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSingleLineClass = classes.Contains("m-cascader");

            // Assert
            Assert.IsTrue(hasSingleLineClass);
        }

        [TestMethod]
        public void RenderCascaderWithSmallChips()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCascader<string, string>>(props =>
            {
                props.Add(cascader => cascader.SmallChips, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSmallChipsClass = classes.Contains("m-cascader");

            // Assert
            Assert.IsTrue(hasSmallChipsClass);
        }

        [TestMethod]
        public void RenderCascaderWithSolo()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCascader<string, string>>(props =>
            {
                props.Add(cascader => cascader.Solo, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSoloClass = classes.Contains("m-cascader");

            // Assert
            Assert.IsTrue(hasSoloClass);
        }

        [TestMethod]
        public void RenderCascaderWithSoloInverted()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCascader<string, string>>(props =>
            {
                props.Add(cascader => cascader.SoloInverted, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSoloInvertedClass = classes.Contains("m-cascader");

            // Assert
            Assert.IsTrue(hasSoloInvertedClass);
        }

        [TestMethod]
        public void RenderCascaderWithSuccess()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCascader<string, string>>(props =>
            {
                props.Add(cascader => cascader.Success, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSuccessClass = classes.Contains("m-cascader");

            // Assert
            Assert.IsTrue(hasSuccessClass);
        }

        [TestMethod]
        public void RenderCascaderWithValidateOnBlur()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCascader<string, string>>(props =>
            {
                props.Add(cascader => cascader.ValidateOnBlur, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasValidateOnBlurClass = classes.Contains("m-cascader");

            // Assert
            Assert.IsTrue(hasValidateOnBlurClass);
        }
    }
}
