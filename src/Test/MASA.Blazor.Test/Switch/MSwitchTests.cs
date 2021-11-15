using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Bunit;

namespace MASA.Blazor.Test.Switch
{
    [TestClass]
    public class MSwitchTests:TestBase
    {
        [TestMethod]
        public void RendeMSwitchWithFlat()
        {
            //Act
            var cut = RenderComponent<MSwitch>(props =>
            {
                props.Add(mswitch => mswitch.Flat, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFlatClass = classes.Contains("m-input--switch--flat");
            // Assert
            Assert.IsTrue(hasFlatClass);
        }

        [TestMethod]
        public void RendeMSwitchWithInset()
        {
            //Act
            var cut = RenderComponent<MSwitch>(props =>
            {
                props.Add(mswitch => mswitch.Inset, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasInsetClass = classes.Contains("m-input--switch--inset");
            // Assert
            Assert.IsTrue(hasInsetClass);
        }

        [TestMethod]
        public void RendeMSwitchNoWithFlat()
        {
            //Act
            var cut = RenderComponent<MSwitch>(props =>
            {
                props.Add(mswitch => mswitch.Flat, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFlatClass = classes.Contains("m-input--switch");
            // Assert
            Assert.IsTrue(hasFlatClass);
        }

        [TestMethod]
        public void RendeMSwitchNoWithInset()
        {
            //Act
            var cut = RenderComponent<MSwitch>(props =>
            {
                props.Add(mswitch => mswitch.Inset, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasInsetClass = classes.Contains("m-input--switch");
            // Assert
            Assert.IsTrue(hasInsetClass);
        }
    }
}
