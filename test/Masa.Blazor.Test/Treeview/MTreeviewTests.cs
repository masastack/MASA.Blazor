using System;
using System.Collections.Generic;
using System.Diagnostics;
using Bunit;

namespace Masa.Blazor.Test.Treeview
{
    [TestClass]
    public class MTreeviewTests : TestBase
    {
        private record Model(string Name, string Value, List<Model> Children);

        private IRenderedComponent<MTreeview<Model, string>> RenderTreeview(
            Action<ComponentParameterCollectionBuilder<MTreeview<Model, string>>> parameterBuilder = null)
        {
            return RenderComponent<MTreeview<Model, string>>(props =>
            {
                props.Add(t => t.Items, new List<Model>());
                props.Add(t => t.ItemText, u => u.Name);
                props.Add(t => t.ItemKey, u => u.Value);
                props.Add(t => t.ItemChildren, u => u.Children);
                parameterBuilder?.Invoke(props);
            });
        }

        [TestMethod]
        public void RenderTreeviewWithHoverable()
        {
            //Act
            var cut = RenderTreeview(props => { props.Add(treeview => treeview.Hoverable, true); });
            var classes = cut.Instance.GetClass();
            var hasHoverableClass = classes.Contains("m-treeview--hoverable");

            // Assert
            Assert.IsTrue(hasHoverableClass);
        }

        [TestMethod]
        public void RenderTreeviewWithActivatable()
        {
            //Act
            var cut = RenderTreeview(props =>
            {
                props.Add(treeview => treeview.Activatable, true);
            });
            var classes = cut.Instance.GetClass();
            var hasActivatableClass = classes.Contains("m-treeview");

            // Assert
            Assert.IsTrue(hasActivatableClass);
        }

        [TestMethod]
        public void RenderTreeviewWithDark()
        {
            //Act
            var cut = RenderTreeview(props =>
            {
                props.Add(treeview => treeview.Dark, true);
            });
            var classes = cut.Instance.GetClass();
            var hasDarkClass = classes.Contains("theme--dark");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderTreeviewWithDense()
        {
            //Act
            var cut = RenderTreeview(props =>
            {
                props.Add(treeview => treeview.Dense, true);
            });
            var classes = cut.Instance.GetClass();
            var hasDenseClass = classes.Contains("m-treeview");

            // Assert
            Assert.IsTrue(hasDenseClass);
        }

        [TestMethod]
        public void RenderTreeviewWithLight()
        {
            //Act
            var cut = RenderTreeview(props =>
            {
                props.Add(treeview => treeview.Light, true);
            });
            var classes = cut.Instance.GetClass();
            var hasLightClass = classes.Contains("theme--light");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderTreeviewWithMultipleActive()
        {
            //Act
            var cut = RenderTreeview(props =>
            {
                props.Add(treeview => treeview.MultipleActive, true);
            });
            var classes = cut.Instance.GetClass();
            var hasMultipleActiveClass = classes.Contains("m-treeview");

            // Assert
            Assert.IsTrue(hasMultipleActiveClass);
        }

        [TestMethod]
        public void RenderTreeviewWithOpenAll()
        {
            //Act
            var cut = RenderTreeview(props =>
            {
                props.Add(treeview => treeview.OpenAll, true);
            });
            var classes = cut.Instance.GetClass();
            var hasOpenAllClass = classes.Contains("m-treeview");

            // Assert
            Assert.IsTrue(hasOpenAllClass);
        }

        [TestMethod]
        public void RenderTreeviewWithOpenOnClick()
        {
            //Act
            var cut = RenderTreeview(props =>
            {
                props.Add(treeview => treeview.OpenOnClick, true);
            });
            var classes = cut.Instance.GetClass();
            var hasOpenOnClickClass = classes.Contains("m-treeview");

            // Assert
            Assert.IsTrue(hasOpenOnClickClass);
        }

        [TestMethod]
        public void RenderTreeviewWithRounded()
        {
            //Act
            var cut = RenderTreeview(props =>
            {
                props.Add(treeview => treeview.Rounded, true);
            });
            var classes = cut.Instance.GetClass();
            var hasRoundedClass = classes.Contains("m-treeview");

            // Assert
            Assert.IsTrue(hasRoundedClass);
        }

        [TestMethod]
        public void RenderTreeviewWithSelectable()
        {
            //Act
            var cut = RenderTreeview(props =>
            {
                props.Add(treeview => treeview.Selectable, true);
            });
            var classes = cut.Instance.GetClass();
            var hasSelectableClass = classes.Contains("m-treeview");

            // Assert
            Assert.IsTrue(hasSelectableClass);
        }

        [TestMethod]
        public void RenderTreeviewWithShaped()
        {
            //Act
            var cut = RenderTreeview(props =>
            {
                props.Add(treeview => treeview.Shaped, true);
            });
            var classes = cut.Instance.GetClass();
            var hasShapedClass = classes.Contains("m-treeview");

            // Assert
            Assert.IsTrue(hasShapedClass);
        }
    }
}