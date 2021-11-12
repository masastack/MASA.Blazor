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
        //等待进一步优化
        //[TestMethod]
        //public void RendeDatePickerWithMultiple()
        //{
        //    //Act
        //    var cut = RenderComponent<MDatePicker<string>>(props =>
        //    {
        //        props.Add(datepicker => datepicker.Multiple, true);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasDenseClass = classes.Contains("m-date-picker");

        //    // Assert
        //    Assert.IsTrue(hasDenseClass);
        //}

        //[TestMethod]
        //public void RendeDatePickerWithRange()
        //{
        //    //Act
        //    var cut = RenderComponent<MDatePicker<string>>(props =>
        //    {
        //        props.Add(datepicker => datepicker.Range, true);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasDenseClass = classes.Contains("m-date-picker");

        //    // Assert
        //    Assert.IsTrue(hasDenseClass);
        //}
    }
}
