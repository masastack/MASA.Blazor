using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bunit;
using BlazorComponent;
using Moq;

namespace MASA.Blazor.Test.List
{
    [TestClass]
    public class MListGroupTests:TestBase
    {
        [TestMethod]
        public void RenderListGroupWithDisabled()
        {
            //Act
            var cut = RenderComponent<MListGroup>(props =>
            {
                props.Add(listgroup => listgroup.Disabled, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDisabledClass = classes.Contains("m-list-group--disabled");

            // Assert
            Assert.IsTrue(hasDisabledClass);
        }

        [TestMethod]
        public void RenderListGroupWithNoAction()
        {
            //Act
            var cut = RenderComponent<MListGroup>(props =>
            {
                props.Add(listgroup => listgroup.NoAction, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasNoActionClass = classes.Contains("m-list-group--no-action");

            // Assert
            Assert.IsTrue(hasNoActionClass);
        }

        [TestMethod]
        public void RenderListGroupWithSubGroup()
        {
            //Act
            var cut = RenderComponent<MListGroup>(props =>
            {
                props.Add(listgroup => listgroup.SubGroup, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSubGroupClass = classes.Contains("m-list-group--sub-group");

            // Assert
            Assert.IsTrue(hasSubGroupClass);
        }

        [TestMethod]
        public void RenderListGroupWithActiveClass()
        {
            //Act
            var cut = RenderComponent<MListGroup>(props =>
            {
                string active = "active-class";
                props.Add(listgroup => listgroup.ActiveClass, active);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasActiveClassClass = classes.Contains("m-list-group");

            // Assert
            Assert.IsTrue(hasActiveClassClass);
        }

        [TestMethod]
        public void RenderListGroupWithAppendIcon()
        {
            //Act
            var cut = RenderComponent<MListGroup>(props =>
            {
                string icon = "mdi-star";
                props.Add(listgroup => listgroup.AppendIcon, icon);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasAppendIconClass = classes.Contains("m-list-group");

            // Assert
            Assert.IsTrue(hasAppendIconClass);
        }

        [TestMethod]
        public void RenderListGroupWithColor()
        {
            //Act
            var cut = RenderComponent<MListGroup>(props =>
            {
                string icon = "mdi-star";
                props.Add(listgroup => listgroup.Color, icon);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasColorClass = classes.Contains("m-list-group");

            // Assert
            Assert.IsTrue(hasColorClass);
        }

        [TestMethod]
        public void RenderListGroupWithGroup()
        {
            //Act
            var cut = RenderComponent<MListGroup>(props =>
            {
                string icon = "mdi-star";
                props.Add(listgroup => listgroup.Group, icon);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasGroupClass = classes.Contains("m-list-group");

            // Assert
            Assert.IsTrue(hasGroupClass);
        }

        [TestMethod]
        public void RenderListGroupWithPrependIcon()
        {
            //Act
            var cut = RenderComponent<MListGroup>(props =>
            {
                string icon = "mdi-star";
                props.Add(listgroup => listgroup.PrependIcon, icon);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasPrependIconClass = classes.Contains("m-list-group");

            // Assert
            Assert.IsTrue(hasPrependIconClass);
        }
    }
}
