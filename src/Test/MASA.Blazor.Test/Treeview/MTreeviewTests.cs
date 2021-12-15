using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bunit;
using BlazorComponent;
using Moq;

namespace MASA.Blazor.Test.Treeview
{
    [TestClass]
    public class MTreeviewTests:TestBase
    {
        [TestMethod]
        public void RenderTreeviewWithHoverable()
        {
            //Act
            var cut = RenderComponent<MTreeview<string, string>>(props =>
             {
                 props.Add(treeview => treeview.Hoverable, true);
                 props.Add(treeview => treeview.ItemKey, item => item);
             });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasHoverableClass = classes.Contains("m-treeview--hoverable");

            // Assert
            Assert.IsTrue(hasHoverableClass);
        }

        [TestMethod]
        public void RenderTreeviewWithActivatable()
        {
            //Act
            var cut = RenderComponent<MTreeview<string, string>>(props =>
            {
                props.Add(treeview => treeview.Activatable, true);
                props.Add(treeview => treeview.ItemKey, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasActivatableClass = classes.Contains("m-treeview");

            // Assert
            Assert.IsTrue(hasActivatableClass);
        }

        [TestMethod]
        public void RenderTreeviewWithDark()
        {
            //Act
            var cut = RenderComponent<MTreeview<string, string>>(props =>
            {
                props.Add(treeview => treeview.Dark, true);
                props.Add(treeview => treeview.ItemKey, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("theme--dark");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderTreeviewWithDense()
        {
            //Act
            var cut = RenderComponent<MTreeview<string, string>>(props =>
            {
                props.Add(treeview => treeview.Dense, true);
                props.Add(treeview => treeview.ItemKey, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDenseClass = classes.Contains("m-treeview");

            // Assert
            Assert.IsTrue(hasDenseClass);
        }

        [TestMethod]
        public void RenderTreeviewWithLight()
        {
            //Act
            var cut = RenderComponent<MTreeview<string, string>>(props =>
            {
                props.Add(treeview => treeview.Light, true);
                props.Add(treeview => treeview.ItemKey, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("theme--light");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderTreeviewWithMultipleActive()
        {
            //Act
            var cut = RenderComponent<MTreeview<string, string>>(props =>
            {
                props.Add(treeview => treeview.MultipleActive, true);
                props.Add(treeview => treeview.ItemKey, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasMultipleActiveClass = classes.Contains("m-treeview");

            // Assert
            Assert.IsTrue(hasMultipleActiveClass);
        }

        [TestMethod]
        public void RenderTreeviewWithOpenAll()
        {
            //Act
            var cut = RenderComponent<MTreeview<string, string>>(props =>
            {
                props.Add(treeview => treeview.OpenAll, true);
                props.Add(treeview => treeview.ItemKey, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasOpenAllClass = classes.Contains("m-treeview");

            // Assert
            Assert.IsTrue(hasOpenAllClass);
        }

        [TestMethod]
        public void RenderTreeviewWithOpenOnClick()
        {
            //Act
            var cut = RenderComponent<MTreeview<string, string>>(props =>
            {
                props.Add(treeview => treeview.OpenOnClick, true);
                props.Add(treeview => treeview.ItemKey, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasOpenOnClickClass = classes.Contains("m-treeview");

            // Assert
            Assert.IsTrue(hasOpenOnClickClass);
        }

        [TestMethod]
        public void RenderTreeviewWithRounded()
        {
            //Act
            var cut = RenderComponent<MTreeview<string, string>>(props =>
            {
                props.Add(treeview => treeview.Rounded, true);
                props.Add(treeview => treeview.ItemKey, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasRoundedClass = classes.Contains("m-treeview");

            // Assert
            Assert.IsTrue(hasRoundedClass);
        }

        [TestMethod]
        public void RenderTreeviewWithSelectable()
        {
            //Act
            var cut = RenderComponent<MTreeview<string, string>>(props =>
            {
                props.Add(treeview => treeview.Selectable, true);
                props.Add(treeview => treeview.ItemKey, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSelectableClass = classes.Contains("m-treeview");

            // Assert
            Assert.IsTrue(hasSelectableClass);
        }

        [TestMethod]
        public void RenderTreeviewWithShaped()
        {
            //Act
            var cut = RenderComponent<MTreeview<string, string>>(props =>
            {
                props.Add(treeview => treeview.Shaped, true);
                props.Add(treeview => treeview.ItemKey, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasShapedClass = classes.Contains("m-treeview");

            // Assert
            Assert.IsTrue(hasShapedClass);
        }

        [TestMethod]
        public void RenderTreeviewWithActiveClass()
        {
            //Act
            var cut = RenderComponent<MTreeview<string, string>>(props =>
            {
                string active = "m-treeview-node--active";
                props.Add(treeview => treeview.ActiveClass, active);
                props.Add(treeview => treeview.ItemKey, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasActiveClassClass = classes.Contains("m-treeview");

            // Assert
            Assert.IsTrue(hasActiveClassClass);
        }

        [TestMethod]
        public void RenderTreeviewWithColor()
        {
            //Act
            var cut = RenderComponent<MTreeview<string, string>>(props =>
            {
                string color = "primary";
                props.Add(treeview => treeview.Color, color);
                props.Add(treeview => treeview.ItemKey, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasColorClass = classes.Contains("m-treeview");

            // Assert
            Assert.IsTrue(hasColorClass);
        }

        [TestMethod]
        public void RenderTreeviewWithExpandIcon()
        {
            //Act
            var cut = RenderComponent<MTreeview<string, string>>(props =>
            {
                string icon = "mdi-menu-down";
                props.Add(treeview => treeview.ExpandIcon, icon);
                props.Add(treeview => treeview.ItemKey, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasExpandIconClass = classes.Contains("m-treeview");

            // Assert
            Assert.IsTrue(hasExpandIconClass);
        }

        [TestMethod]
        public void RenderTreeviewWithIndeterminateIcon()
        {
            //Act
            var cut = RenderComponent<MTreeview<string, string>>(props =>
            {
                string icon = "mdi-minus-box";
                props.Add(treeview => treeview.IndeterminateIcon, icon);
                props.Add(treeview => treeview.ItemKey, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasIndeterminateIconClass = classes.Contains("m-treeview");

            // Assert
            Assert.IsTrue(hasIndeterminateIconClass);
        }

        [TestMethod]
        public void RenderTreeviewWithLoadingIcon()
        {
            //Act
            var cut = RenderComponent<MTreeview<string, string>>(props =>
            {
                string icon = "mdi-cached";
                props.Add(treeview => treeview.LoadingIcon, icon);
                props.Add(treeview => treeview.ItemKey, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLoadingIconClass = classes.Contains("m-treeview");

            // Assert
            Assert.IsTrue(hasLoadingIconClass);
        }

        [TestMethod]
        public void RenderTreeviewWithOffIcon()
        {
            //Act
            var cut = RenderComponent<MTreeview<string, string>>(props =>
            {
                string icon = "mdi-checkbox-blank-outline";
                props.Add(treeview => treeview.OffIcon, icon);
                props.Add(treeview => treeview.ItemKey, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasOffIconClass = classes.Contains("m-treeview");

            // Assert
            Assert.IsTrue(hasOffIconClass);
        }

        [TestMethod]
        public void RenderTreeviewWithOnIcon()
        {
            //Act
            var cut = RenderComponent<MTreeview<string, string>>(props =>
            {
                string icon = "mdi-checkbox-marked";
                props.Add(treeview => treeview.OnIcon, icon);
                props.Add(treeview => treeview.ItemKey, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasOnIconClass = classes.Contains("m-treeview");

            // Assert
            Assert.IsTrue(hasOnIconClass);
        }

        [TestMethod]
        public void RenderTreeviewWithSelectedColor()
        {
            //Act
            var cut = RenderComponent<MTreeview<string, string>>(props =>
            {
                string color = "accent";
                props.Add(treeview => treeview.SelectedColor, color);
                props.Add(treeview => treeview.ItemKey, item => item);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSelectedColorClass = classes.Contains("m-treeview");

            // Assert
            Assert.IsTrue(hasSelectedColorClass);
        }
    }
}
