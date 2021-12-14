using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bunit;
using BlazorComponent;
using Moq;

namespace MASA.Blazor.Test.DatePicker
{
    [TestClass]
    public class MDatePickerTests:TestBase
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

        [TestMethod]
        public void RenderDatePickerWithColor()
        {
            //Act
            var cut = RenderComponent<MDatePicker<string>>(props =>
            {
                string color = "m-picker--date";
                props.Add(datepicker => datepicker.Color, color);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasColorClass = classes.Contains("m-picker--date");

            // Assert
            Assert.IsTrue(hasColorClass);
        }

        [TestMethod]
        public void RenderDatePickerWithHeaderColor()
        {
            //Act
            var cut = RenderComponent<MDatePicker<string>>(props =>
            {
                string color = "m-picker--date";
                props.Add(datepicker => datepicker.HeaderColor, color);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasHeaderColorClass = classes.Contains("m-picker--date");

            // Assert
            Assert.IsTrue(hasHeaderColorClass);
        }

        [TestMethod]
        public void RenderDatePickerWithLocale()
        {
            //Act
            var cut = RenderComponent<MDatePicker<string>>(props =>
            {
                string locale = "en-US";
                props.Add(datepicker => datepicker.Locale, locale);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLocaleClass = classes.Contains("m-picker--date");

            // Assert
            Assert.IsTrue(hasLocaleClass);
        }

        [TestMethod]
        public void RenderDatePickerWithNextIcon()
        {
            //Act
            var cut = RenderComponent<MDatePicker<string>>(props =>
            {
                string icon = "mdi-star";
                props.Add(datepicker => datepicker.NextIcon, icon);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasNextIconClass = classes.Contains("m-picker--date");

            // Assert
            Assert.IsTrue(hasNextIconClass);
        }

        [TestMethod]
        public void RenderDatePickerWithPrevIcon()
        {
            //Act
            var cut = RenderComponent<MDatePicker<string>>(props =>
            {
                string icon = "mdi-star";
                props.Add(datepicker => datepicker.PrevIcon, icon);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasPrevIconClass = classes.Contains("m-picker--date");

            // Assert
            Assert.IsTrue(hasPrevIconClass);
        }

        //[TestMethod]
        //public void RenderDatePickerWithRange()
        //{
        //    //Act
        //    var cut = RenderComponent<MDatePicker<string>>(props =>
        //    {
        //        props.Add(datepicker => datepicker.Range, true);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasRangeClass = classes.Contains("m-picker--date");

        //    // Assert
        //    Assert.IsTrue(hasRangeClass);
        //}

        [TestMethod]
        public void RenderDatePickerWithValue()
        {
            //Act
            var cut = RenderComponent<MDatePicker<string>>(props =>
            {
                string icon = "mdi-star";
                props.Add(datepicker => datepicker.Value, icon);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasValueClass = classes.Contains("m-picker--date");

            // Assert
            Assert.IsTrue(hasValueClass);
        }

        [TestMethod]
        public void RenderDatePickerWithYearIcon()
        {
            //Act
            var cut = RenderComponent<MDatePicker<string>>(props =>
            {
                string icon = "mdi-star";
                props.Add(datepicker => datepicker.YearIcon, icon);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasYearIconClass = classes.Contains("m-picker--date");

            // Assert
            Assert.IsTrue(hasYearIconClass);
        }
    }
}
