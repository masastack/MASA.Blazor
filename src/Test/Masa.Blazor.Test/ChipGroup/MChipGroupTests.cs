using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Masa.Blazor.Test.ChipGroup
{
    [TestClass]
    public class MChipGroupTests : TestBase
    {
        [TestMethod]
        public void RenderChipGroupWithCenterActive()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MChipGroup>(props =>
            {
                props.Add(chipgroup => chipgroup.CenterActive, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasCenterActiveClass = classes.Contains("m-chip-group");

            // Assert
            Assert.IsTrue(hasCenterActiveClass);
        }

        [TestMethod]
        public void RenderChipGroupWithColumn()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MChipGroup>(props =>
            {
                props.Add(chipgroup => chipgroup.Column, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasColumnClass = classes.Contains("m-chip-group");

            // Assert
            Assert.IsTrue(hasColumnClass);
        }

        [TestMethod]
        public void RenderChipGroupWithMandatory()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MChipGroup>(props =>
            {
                props.Add(chipgroup => chipgroup.Mandatory, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasMandatoryClass = classes.Contains("m-chip-group");

            // Assert
            Assert.IsTrue(hasMandatoryClass);
        }

        [TestMethod]
        public void RenderChipGroupWithMax()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MChipGroup>(props =>
            {
                props.Add(chipgroup => chipgroup.Max, 1);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasMaxClass = classes.Contains("m-chip-group");

            // Assert
            Assert.IsTrue(hasMaxClass);
        }

        [TestMethod]
        public void RenderChipGroupWithMultiple()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MChipGroup>(props =>
            {
                props.Add(chipgroup => chipgroup.Multiple, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasMultipleClass = classes.Contains("m-chip-group");

            // Assert
            Assert.IsTrue(hasMultipleClass);
        }

        [TestMethod]
        public void RenderChipGroupWithShowArrows()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MChipGroup>(props =>
            {
                props.Add(chipgroup => chipgroup.ShowArrows, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasShowArrowsClass = classes.Contains("m-chip-group");

            // Assert
            Assert.IsTrue(hasShowArrowsClass);
        }
    }
}
