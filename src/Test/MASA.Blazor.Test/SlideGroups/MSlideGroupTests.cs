using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Bunit;

namespace MASA.Blazor.Test.SlideGroups
{
    [TestClass]
    public  class MSlideGroupTests:TestBase
    {
        [TestMethod]
        public void RenderSlideGroupWithActiveClass()
        {
            //Act
            JSInterop.Mode = Bunit.JSRuntimeMode.Loose;
            var cut = RenderComponent<MSlideGroup>(props =>
            {
                string active = "m-slide-item--active";
                props.Add(slidegroup => slidegroup.ActiveClass, active);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasActiveClassClass = classes.Contains("m-slide-group");

            // Assert
            Assert.IsTrue(hasActiveClassClass);
        }

        [TestMethod]
        public void RenderSlideGroupWithCenterActive()
        {
            //Act
            JSInterop.Mode = Bunit.JSRuntimeMode.Loose;
            var cut = RenderComponent<MSlideGroup>(props =>
            {
                props.Add(slidegroup => slidegroup.CenterActive, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasCenterActiveClass = classes.Contains("m-slide-group");

            // Assert
            Assert.IsTrue(hasCenterActiveClass);
        }

        [TestMethod]
        public void RenderSlideGroupWithMandatory()
        {
            //Act
            JSInterop.Mode = Bunit.JSRuntimeMode.Loose;
            var cut = RenderComponent<MSlideGroup>(props =>
            {
                props.Add(slidegroup => slidegroup.Mandatory, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasMandatoryClass = classes.Contains("m-slide-group");

            // Assert
            Assert.IsTrue(hasMandatoryClass);
        }

        [TestMethod]
        public void RenderSlideGroupWithMax()
        {
            //Act
            JSInterop.Mode = Bunit.JSRuntimeMode.Loose;
            var cut = RenderComponent<MSlideGroup>(props =>
            {
                props.Add(slidegroup => slidegroup.Max, 1);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasMaxClass = classes.Contains("m-slide-group");

            // Assert
            Assert.IsTrue(hasMaxClass);
        }

        [TestMethod]
        public void RenderSlideGroupWithMultiple()
        {
            //Act
            JSInterop.Mode = Bunit.JSRuntimeMode.Loose;
            var cut = RenderComponent<MSlideGroup>(props =>
            {
                props.Add(slidegroup => slidegroup.Multiple, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasMultipleClass = classes.Contains("m-slide-group");

            // Assert
            Assert.IsTrue(hasMultipleClass);
        }

        [TestMethod]
        public void RenderSlideGroupWithNextIcon()
        {
            //Act
            JSInterop.Mode = Bunit.JSRuntimeMode.Loose;
            var cut = RenderComponent<MSlideGroup>(props =>
            {
                string icon = "mdi-star";
                props.Add(slidegroup => slidegroup.NextIcon, icon);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasNextIconClass = classes.Contains("m-slide-group");

            // Assert
            Assert.IsTrue(hasNextIconClass);
        }

        [TestMethod]
        public void RenderSlideGroupWithPrevIcon()
        {
            //Act
            JSInterop.Mode = Bunit.JSRuntimeMode.Loose;
            var cut = RenderComponent<MSlideGroup>(props =>
            {
                string icon = "mdi-star";
                props.Add(slidegroup => slidegroup.PrevIcon, icon);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasPrevIconClass = classes.Contains("m-slide-group");

            // Assert
            Assert.IsTrue(hasPrevIconClass);
        }

        [TestMethod]
        public void RenderSlideGroupWithShowArrows()
        {
            //Act
            JSInterop.Mode = Bunit.JSRuntimeMode.Loose;
            var cut = RenderComponent<MSlideGroup>(props =>
            {
                props.Add(slidegroup => slidegroup.ShowArrows, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasShowArrowsClass = classes.Contains("m-slide-group");

            // Assert
            Assert.IsTrue(hasShowArrowsClass);
        }

        [TestMethod]
        public void RenderSlideGroupWithValue()
        {
            //Act
            JSInterop.Mode = Bunit.JSRuntimeMode.Loose;
            var cut = RenderComponent<MSlideGroup>(props =>
            {
                string icon = "mdi-star";
                props.Add(slidegroup => slidegroup.Value, icon);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasValueClass = classes.Contains("m-slide-group");

            // Assert
            Assert.IsTrue(hasValueClass);
        }
    }
}
