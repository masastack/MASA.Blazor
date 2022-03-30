using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Masa.Blazor.Test.RangeSlider
{
    [TestClass]
    public class MRangeSliderTests : TestBase
    {
        //[TestMethod]
        //public void RenderRangeSliderWithDark()
        //{
        //    //Act
        //    var cut = RenderComponent<MRangeSlider<string>>(props =>
        //    {
        //        props.Add(rangeslider => rangeslider.Dark, true);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasDarkClass = classes.Contains("theme--dark");
        //    //Assert
        //    Assert.IsTrue(hasDarkClass);
        //}

        //[TestMethod]
        //public void RenderRangeSliderWithLight()
        //{
        //    //Act
        //    var cut = RenderComponent<MRangeSlider<string>>(props =>
        //    {
        //        props.Add(rangeslider => rangeslider.Light, true);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasLightClass = classes.Contains("m-range-slider");
        //    // Assert
        //    Assert.IsTrue(hasLightClass);
        //}

        //[TestMethod]
        //public void RenderRangeSliderWithDense()
        //{
        //    //Act
        //    var cut = RenderComponent<MRangeSlider<string>>(props =>
        //    {
        //        props.Add(rangeslider => rangeslider.Dense, true);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasDenseClass = classes.Contains("m-range-slider");
        //    // Assert
        //    Assert.IsTrue(hasDenseClass);
        //}

        //[TestMethod]
        //public void RenderRangeSliderWithDisabled()
        //{
        //    //Act
        //    var cut = RenderComponent<MRangeSlider<string>>(props =>
        //    {
        //        props.Add(rangeslider => rangeslider.Disabled, true);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasDisabledClass = classes.Contains("m-range-slider");
        //    // Assert
        //    Assert.IsTrue(hasDisabledClass);
        //}

        //[TestMethod]
        //public void RenderRangeSliderWithError()
        //{
        //    //Act
        //    var cut = RenderComponent<MRangeSlider<string>>(props =>
        //    {
        //        props.Add(rangeslider => rangeslider.Error, true);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasErrorClass = classes.Contains("m-range-slider");
        //    // Assert
        //    Assert.IsTrue(hasErrorClass);
        //}

        //[TestMethod]
        //public void RenderRangeSliderWithErrorCount()
        //{
        //    //Act
        //    var cut = RenderComponent<MRangeSlider<string>>(props =>
        //    {
        //        props.Add(alert => alert.ErrorCount, 1);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasErrorCountClass = classes.Contains("m-range-slider");

        //    // Assert
        //    Assert.IsTrue(hasErrorCountClass);
        //}

        //[TestMethod]
        //public void RenderWithHeight()
        //{
        //    // Act
        //    var cut = RenderComponent<MRangeSlider<string>>(props =>
        //    {
        //        props.Add(p => p.Height, 100);
        //    });
        //    var inputSlotDiv = cut.Find(".m-range-slider");
        //    var style = inputSlotDiv.GetAttribute("style");

        //    // Assert
        //    Assert.AreEqual("height: 100px", style);
        //}

        //[TestMethod]
        //public void RenderRangeSliderWithHideDetails()
        //{
        //    //Act
        //    var cut = RenderComponent<MRangeSlider<string>>(props =>
        //    {
        //        props.Add(rangeslider => rangeslider.HideDetails, true);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasHideDetailsClass = classes.Contains("m-range-slider");
        //    // Assert
        //    Assert.IsTrue(hasHideDetailsClass);
        //}

        //[TestMethod]
        //public void RenderRangeSliderWithInverseLabel()
        //{
        //    //Act
        //    var cut = RenderComponent<MRangeSlider<string>>(props =>
        //    {
        //        props.Add(rangeslider => rangeslider.InverseLabel, true);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasInverseLabelClass = classes.Contains("m-range-slider");
        //    // Assert
        //    Assert.IsTrue(hasInverseLabelClass);
        //}

        //[TestMethod]
        //public void RenderRangeSliderWithLoaderHeight()
        //{
        //    //Act
        //    var cut = RenderComponent<MRangeSlider<string>>(props =>
        //    {
        //        props.Add(alert => alert.LoaderHeight, 2);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasLoaderHeightClass = classes.Contains("m-range-slider");

        //    // Assert
        //    Assert.IsTrue(hasLoaderHeightClass);
        //}

        //[TestMethod]
        //public void RenderRangeSliderWithLoading()
        //{
        //    //Act
        //    var cut = RenderComponent<MRangeSlider<string>>(props =>
        //    {
        //        props.Add(rangeslider => rangeslider.Loading, true);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasLoadingClass = classes.Contains("m-range-slider");
        //    // Assert
        //    Assert.IsTrue(hasLoadingClass);
        //}

        //[TestMethod]
        //public void RenderRangeSliderWithMax()
        //{
        //    //Act
        //    var cut = RenderComponent<MRangeSlider<string>>(props =>
        //    {
        //        props.Add(alert => alert.Max, 100);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasMaxClass = classes.Contains("m-range-slider");

        //    // Assert
        //    Assert.IsTrue(hasMaxClass);
        //}

        //[TestMethod]
        //public void RenderRangeSliderWithMin()
        //{
        //    //Act
        //    var cut = RenderComponent<MRangeSlider<string>>(props =>
        //    {
        //        props.Add(alert => alert.Min, 0);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasMinClass = classes.Contains("m-range-slider");

        //    // Assert
        //    Assert.IsTrue(hasMinClass);
        //}

        //[TestMethod]
        //public void RenderRangeSliderWithPersistentHint()
        //{
        //    //Act
        //    var cut = RenderComponent<MRangeSlider<string>>(props =>
        //    {
        //        props.Add(rangeslider => rangeslider.PersistentHint, true);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasPersistentHintClass = classes.Contains("m-range-slider");
        //    // Assert
        //    Assert.IsTrue(hasPersistentHintClass);
        //}

        //[TestMethod]
        //public void RenderRangeSliderWithReadonly()
        //{
        //    //Act
        //    var cut = RenderComponent<MRangeSlider<string>>(props =>
        //    {
        //        props.Add(rangeslider => rangeslider.Readonly, true);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasReadonlyClass = classes.Contains("m-range-slider");
        //    // Assert
        //    Assert.IsTrue(hasReadonlyClass);
        //}

        //[TestMethod]
        //public void RenderRangeSliderWithStep()
        //{
        //    //Act
        //    var cut = RenderComponent<MRangeSlider<string>>(props =>
        //    {
        //        props.Add(alert => alert.Step, 0);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasStepClass = classes.Contains("m-range-slider");

        //    // Assert
        //    Assert.IsTrue(hasStepClass);
        //}

        //[TestMethod]
        //public void RenderRangeSliderWithSuccess()
        //{
        //    //Act
        //    var cut = RenderComponent<MRangeSlider<string>>(props =>
        //    {
        //        props.Add(rangeslider => rangeslider.Success, true);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasSuccessClass = classes.Contains("m-range-slider");
        //    // Assert
        //    Assert.IsTrue(hasSuccessClass);
        //}

        //[TestMethod]
        //public void RenderRangeSliderWithThumbLabel()
        //{
        //    //Act
        //    var cut = RenderComponent<MRangeSlider<string>>(props =>
        //    {
        //        props.Add(rangeslider => rangeslider.ThumbLabel, true);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasThumbLabelClass = classes.Contains("m-range-slider");
        //    // Assert
        //    Assert.IsTrue(hasThumbLabelClass);
        //}

        //[TestMethod]
        //public void RenderRangeSliderWithThumbSize()
        //{
        //    //Act
        //    var cut = RenderComponent<MRangeSlider<string>>(props =>
        //    {
        //        props.Add(alert => alert.ThumbSize, 32);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasThumbSizeClass = classes.Contains("m-range-slider");

        //    // Assert
        //    Assert.IsTrue(hasThumbSizeClass);
        //}

        //[TestMethod]
        //public void RenderRangeSliderWithTickSize()
        //{
        //    //Act
        //    var cut = RenderComponent<MRangeSlider<string>>(props =>
        //    {
        //        props.Add(alert => alert.TickSize, 2);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasTickSizeClass = classes.Contains("m-range-slider");

        //    // Assert
        //    Assert.IsTrue(hasTickSizeClass);
        //}

        //[TestMethod]
        //public void RenderRangeSliderWithTicks()
        //{
        //    //Act
        //    var cut = RenderComponent<MRangeSlider<string>>(props =>
        //    {
        //        props.Add(rangeslider => rangeslider.Ticks, true);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasTicksClass = classes.Contains("m-range-slider");
        //    // Assert
        //    Assert.IsTrue(hasTicksClass);
        //}

        //[TestMethod]
        //public void RenderRangeSliderWithValidateOnBlur()
        //{
        //    //Act
        //    var cut = RenderComponent<MRangeSlider<string>>(props =>
        //    {
        //        props.Add(rangeslider => rangeslider.ValidateOnBlur, true);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasValidateOnBlurClass = classes.Contains("m-range-slider");
        //    // Assert
        //    Assert.IsTrue(hasValidateOnBlurClass);
        //}

        //[TestMethod]
        //public void RenderRangeSliderWithVertical()
        //{
        //    //Act
        //    var cut = RenderComponent<MRangeSlider<string>>(props =>
        //    {
        //        props.Add(rangeslider => rangeslider.Vertical, true);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasVerticalClass = classes.Contains("m-range-slider");
        //    // Assert
        //    Assert.IsTrue(hasVerticalClass);
        //}
    }
}
