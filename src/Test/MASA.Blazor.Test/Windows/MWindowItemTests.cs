using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Bunit;

namespace MASA.Blazor.Test.Windows
{
    [TestClass]
    public class MWindowItemTests:TestBase
    {
        [TestMethod]
        public void RenderWindowItemWithDisabled()
        {
            //Act
            var cut = RenderComponent<MWindowItem>(props =>
            {
                props.Add(windowitem => windowitem.Disabled, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDisabledClass = classes.Contains("m-window-item");

            // Assert
            Assert.IsTrue(hasDisabledClass);
        }

        [TestMethod]
        public void RenderWindowItemWithActiveClass()
        {
            //Act
            var cut = RenderComponent<MWindowItem>(props =>
            {
                string active = "active";
                props.Add(windowitem => windowitem.ActiveClass, active);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasActiveClassClass = classes.Contains("m-window-item");

            // Assert
            Assert.IsTrue(hasActiveClassClass);
        }

        [TestMethod]
        public void RenderWindowItemWithReverseTransition()
        {
            //Act
            var cut = RenderComponent<MWindowItem>(props =>
            {
                string active = "active";
                props.Add(windowitem => windowitem.ReverseTransition, active);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasReverseTransitionClass = classes.Contains("m-window-item");

            // Assert
            Assert.IsTrue(hasReverseTransitionClass);
        }

        [TestMethod]
        public void RenderWindowItemWithTransition()
        {
            //Act
            var cut = RenderComponent<MWindowItem>(props =>
            {
                string active = "active";
                props.Add(windowitem => windowitem.Transition, active);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasTransitionClass = classes.Contains("m-window-item");

            // Assert
            Assert.IsTrue(hasTransitionClass);
        }

        [TestMethod]
        public void RenderWindowItemWithValue()
        {
            //Act
            var cut = RenderComponent<MWindowItem>(props =>
            {
                string active = "active";
                props.Add(windowitem => windowitem.Value, active);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasValueClass = classes.Contains("m-window-item");

            // Assert
            Assert.IsTrue(hasValueClass);
        }

        [TestMethod]
        public void RenderWithChildContent()
        {
            // Arrange & Act
            var cut = RenderComponent<MWindowItem>(props =>
            {
                props.Add(windowitem => windowitem.ChildContent,  "<span>Hello world</span>");
            });
            var contentDiv = cut.Find(".m-window-item");
            //row fill-height  
            // Assert
            contentDiv.Children.MarkupMatches("<span>Hello world</span>");
        }
    }
}
