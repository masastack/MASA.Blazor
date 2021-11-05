using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace MASA.Blazor.Test.Radio
{
    [TestClass]
    public class MRadioGroupTests:TestBase
    {
        [TestMethod]
        public void RendeMRadioGroupWithRow()
        {
            //Act
            var cut = RenderComponent<MRadioGroup<string>>(props =>
            {
                props.Add(radiogroup => radiogroup.Row, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasRowClass = classes.Contains("m-input--radio-group--row");
            // Assert
            Assert.IsTrue(hasRowClass);
        }

        [TestMethod]
        public void RendeMRadioGroupWithColumn()
        {
            //Act
            var cut = RenderComponent<MRadioGroup<string>>(props =>
            {
                props.Add(radiogroup => radiogroup.Column, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasColumnClass = classes.Contains("m-input--radio-group--column");
            // Assert
            Assert.IsTrue(hasColumnClass);
        }

        [TestMethod]
        public void RenderProgressLinearWithDark()
        {
            //Act
            var cut = RenderComponent<MRadioGroup<string>>(props =>
            {
                props.Add(counter => counter.Dark, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("theme--dark");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderProgressLinearWithLight()
        {
            //Act
            var cut = RenderComponent<MRadioGroup<string>>(props =>
            {
                props.Add(counter => counter.Light, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("theme--light");

            // Assert
            Assert.IsTrue(hasLightClass);
        }
    }
}
