using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bunit;
using BlazorComponent;
using Moq;

namespace MASA.Blazor.Test.ItemGroup
{
    [TestClass]
    public class MItemGroupTests:TestBase
    {
        [TestMethod]
        public void RenderItemGroupWithDark()
        {
            //Act
            var cut = RenderComponent<MItemGroup>(props =>
            {
                props.Add(itemgroup => itemgroup.Dark, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasdarkClass = classes.Contains("theme--dark");

            // Assert
            Assert.IsTrue(hasdarkClass);
        }

        [TestMethod]
        public void RenderItemGroupWithLight()
        {
            //Act
            var cut = RenderComponent<MItemGroup>(props =>
            {
                props.Add(itemgroup => itemgroup.Light, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("theme--light");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderItemGroupWithMandatory()
        {
            //Act
            var cut = RenderComponent<MItemGroup>(props =>
            {
                props.Add(itemgroup => itemgroup.Mandatory, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasMandatoryClass = classes.Contains("m-item-group");

            // Assert
            Assert.IsTrue(hasMandatoryClass);
        }

        [TestMethod]
        public void RenderItemGroupWithElevation()
        {
            //Act
            var cut = RenderComponent<MItemGroup>(props =>
            {
                props.Add(alert => alert.Max, 2);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasElevationClass = classes.Contains("m-item-group");

            // Assert
            Assert.IsTrue(hasElevationClass);
        }

        [TestMethod]
        public void RenderItemGroupWithMultiple()
        {
            //Act
            var cut = RenderComponent<MItemGroup>(props =>
            {
                props.Add(itemgroup => itemgroup.Multiple, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasMandatoryClass = classes.Contains("m-item-group");

            // Assert
            Assert.IsTrue(hasMandatoryClass);
        }

        [TestMethod]
        public void RenderItemGroupWithActiveClass()
        {
            //Act
            var cut = RenderComponent<MItemGroup>(props =>
            {
                string active = "m-item--active";
                props.Add(itemgroup => itemgroup.ActiveClass, active);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasActiveClassClass = classes.Contains("m-item-group");

            // Assert
            Assert.IsTrue(hasActiveClassClass);
        }

        [TestMethod]
        public void RenderItemGroupWithMax()
        {
            //Act
            var cut = RenderComponent<MItemGroup>(props =>
            {
                props.Add(itemgroup => itemgroup.Max, 2);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasMaxClass = classes.Contains("m-item-group");

            // Assert
            Assert.IsTrue(hasMaxClass);
        }

        [TestMethod]
        public void RenderItemGroupWithValue()
        {
            //Act
            var cut = RenderComponent<MItemGroup>(props =>
            {
                string active = "m-item--active";
                props.Add(itemgroup => itemgroup.Value, active);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasValueClass = classes.Contains("m-item-group");

            // Assert
            Assert.IsTrue(hasValueClass);
        }
    }
}
