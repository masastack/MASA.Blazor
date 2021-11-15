using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace MASA.Blazor.Test.Radio
{
    [TestClass]
    public class MRadioTests:TestBase
    {
        [TestMethod]
        public void RenderRadioWithDark()
        {
            //Act
            var cut = RenderComponent<MRadio<string>>(props =>
            {
                props.Add(counter => counter.Dark, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("theme--dark");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderRadioWithLight()
        {
            //Act
            var cut = RenderComponent<MRadio<string>>(props =>
            {
                props.Add(counter => counter.Light, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("theme--light");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderRadioWithIsDisabled()
        {
            //Act
            var cut = RenderComponent<MRadio<string>>(props =>
            {
                props.Add(counter => counter.IsDisabled, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasIsDisabledClass = classes.Contains("m-radio--is-disabled");

            // Assert
            Assert.IsTrue(hasIsDisabledClass);
        }

        [TestMethod]
        public void RenderRadioNoWithDark()
        {
            //Act
            var cut = RenderComponent<MRadio<string>>(props =>
            {
                props.Add(counter => counter.Dark, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("m-radio");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderRadioNoWithLight()
        {
            //Act
            var cut = RenderComponent<MRadio<string>>(props =>
            {
                props.Add(counter => counter.Light, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("m-radio");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderRadioNoWithIsDisabled()
        {
            //Act
            var cut = RenderComponent<MRadio<string>>(props =>
            {
                props.Add(counter => counter.IsDisabled, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasIsDisabledClass = classes.Contains("m-radio");

            // Assert
            Assert.IsTrue(hasIsDisabledClass);
        }
    }
}
