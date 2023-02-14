using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Masa.Blazor.Test.DatePicker
{
    [TestClass]
    public class MDatePickerTests : TestBase
    {
        [TestMethod]
        public void RenderDatePickerWithDark()
        {
            //Act
            var cut = RenderComponent<MDatePicker<string>>(props =>
            {
                props.Add(datepicker => datepicker.Dark, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("m-picker--date");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderDatePickerWithDisabled()
        {
            //Act
            var cut = RenderComponent<MDatePicker<string>>(props =>
            {
                props.Add(datepicker => datepicker.Disabled, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDisabledClass = classes.Contains("m-picker--date");

            // Assert
            Assert.IsTrue(hasDisabledClass);
        }

        [TestMethod]
        public void RenderDatePickerWithElevation()
        {
            //Act
            var cut = RenderComponent<MDatePicker<string>>(props =>
            {
                props.Add(datepicker => datepicker.Elevation, 24);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasElevationClass = classes.Contains("m-picker--date");

            // Assert
            Assert.IsTrue(hasElevationClass);
        }

        [TestMethod]
        public void RenderDatePickerWithFirstDayOfWeek()
        {
            //Act
            var cut = RenderComponent<MDatePicker<string>>(props =>
            {
                props.Add(datepicker => datepicker.FirstDayOfWeek, 0);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFirstDayOfWeekClass = classes.Contains("m-picker--date");

            // Assert
            Assert.IsTrue(hasFirstDayOfWeekClass);
        }

        [TestMethod]
        public void RenderDatePickerWithFlat()
        {
            //Act
            var cut = RenderComponent<MDatePicker<string>>(props =>
            {
                props.Add(datepicker => datepicker.Flat, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFlatClass = classes.Contains("m-picker--date");

            // Assert
            Assert.IsTrue(hasFlatClass);
        }

        [TestMethod]
        public void RenderDatePickerWithFullWidth()
        {
            //Act
            var cut = RenderComponent<MDatePicker<string>>(props =>
            {
                props.Add(datepicker => datepicker.FullWidth, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFullWidthClass = classes.Contains("m-picker--date");

            // Assert
            Assert.IsTrue(hasFullWidthClass);
        }

        [TestMethod]
        public void RenderDatePickerWithLandscape()
        {
            //Act
            var cut = RenderComponent<MDatePicker<string>>(props =>
            {
                props.Add(datepicker => datepicker.Landscape, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLandscapeClass = classes.Contains("m-picker--date");

            // Assert
            Assert.IsTrue(hasLandscapeClass);
        }

        [TestMethod]
        public void RenderDatePickerWithLight()
        {
            //Act
            var cut = RenderComponent<MDatePicker<string>>(props =>
            {
                props.Add(datepicker => datepicker.Light, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("m-picker--date");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderDatePickerWithNoTitle()
        {
            //Act
            var cut = RenderComponent<MDatePicker<string>>(props =>
            {
                props.Add(datepicker => datepicker.NoTitle, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasNoTitleClass = classes.Contains("m-picker--date");

            // Assert
            Assert.IsTrue(hasNoTitleClass);
        }

        [TestMethod]
        public void RenderDatePickerWithReactive()
        {
            //Act
            var cut = RenderComponent<MDatePicker<string>>(props =>
            {
                props.Add(datepicker => datepicker.Reactive, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasReactiveClass = classes.Contains("m-picker--date");

            // Assert
            Assert.IsTrue(hasReactiveClass);
        }

        [TestMethod]
        public void RenderDatePickerWithReadonly()
        {
            //Act
            var cut = RenderComponent<MDatePicker<string>>(props =>
            {
                props.Add(datepicker => datepicker.Readonly, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasReadonlyClass = classes.Contains("m-picker--date");

            // Assert
            Assert.IsTrue(hasReadonlyClass);
        }

        [TestMethod]
        public void RenderDatePickerWithScrollable()
        {
            //Act
            var cut = RenderComponent<MDatePicker<string>>(props =>
            {
                props.Add(datepicker => datepicker.Scrollable, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasScrollableClass = classes.Contains("m-picker--date");

            // Assert
            Assert.IsTrue(hasScrollableClass);
        }

        [TestMethod]
        public void RenderDatePickerWithShowAdjacentMonths()
        {
            //Act
            var cut = RenderComponent<MDatePicker<string>>(props =>
            {
                props.Add(datepicker => datepicker.ShowAdjacentMonths, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasShowAdjacentMonthsClass = classes.Contains("m-picker--date");

            // Assert
            Assert.IsTrue(hasShowAdjacentMonthsClass);
        }

        [TestMethod]
        public void RenderDatePickerWithShowCurrent()
        {
            //Act
            var cut = RenderComponent<MDatePicker<string>>(props =>
            {
                props.Add(datepicker => datepicker.ShowCurrent, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasShowCurrentClass = classes.Contains("m-picker--date");

            // Assert
            Assert.IsTrue(hasShowCurrentClass);
        }

        [TestMethod]
        public void RenderDatePickerWithShowWeek()
        {
            //Act
            var cut = RenderComponent<MDatePicker<string>>(props =>
            {
                props.Add(datepicker => datepicker.ShowWeek, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasShowWeekClass = classes.Contains("m-picker--date");

            // Assert
            Assert.IsTrue(hasShowWeekClass);
        }

        [TestMethod]
        public void RenderDatePickerWithWidth()
        {
            //Act
            var cut = RenderComponent<MDatePicker<string>>(props =>
            {
                props.Add(datepicker => datepicker.Width, 290);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasWidthClass = classes.Contains("m-picker--date");

            // Assert
            Assert.IsTrue(hasWidthClass);
        }
    }
}
