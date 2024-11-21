using System;
using System.Collections.Generic;
using AngleSharp.Dom;
using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Masa.Blazor.Test.Autocompletes
{
    [TestClass]
    public class MAutocompleteTests : TestBase
    {
        private record Model(string Name, string Value);

        private IRenderedComponent<MAutocomplete<Model, string, string>> Render(
            Action<ComponentParameterCollectionBuilder<MAutocomplete<Model, string, string>>> parameterBuilder = null)
        {
            return RenderComponent<MAutocomplete<Model, string, string>>(props =>
            {
                props.Add(a => a.Value, "Value1");
                props.Add(a => a.Items, new List<Model>()
                {
                    new Model("Name1", "Value1"),
                    new Model("Name2", "Value2"),
                    new Model("Name3", "Value3"),
                });
                props.Add(a => a.ItemText, item => item.Name);
                props.Add(a => a.ItemValue, item => item.Value);
                parameterBuilder?.Invoke(props);
            });
        }

        private IElement RenderAndGetRootElement(
            Action<ComponentParameterCollectionBuilder<MAutocomplete<Model, string, string>>> parameterBuilder = null,
            string tag = "div")
        {
            return Render(parameterBuilder).Find(tag);
        }

        [TestMethod]
        public void RenderAutocompleteWithChips()
        {
            //Act
            var cut = RenderAndGetRootElement(props =>
            {
                props.Add(autocomplete => autocomplete.Chips, true);
            });
            var hasChipsClass = cut.ClassList.Contains("m-select--chips");

            // Assert
            Assert.IsTrue(hasChipsClass);
        }

        [TestMethod]
        public void RenderAutocompleteWithClearable()
        {
            //Act
            var cut = Render(props =>
            {
                props.Add(autocomplete => autocomplete.Clearable, true);
            });

            cut.Render();

            cut.Find(".m-input__icon--clear");
        }

        [TestMethod]
        public void RenderAutocompleteWithDeletableChips()
        {
            //Act
            var cut = Render(props =>
            {
                props.Add(a => a.Chips, true);
                props.Add(a => a.DeletableChips, true);
            });

            cut.Find(".m-chip__close");
        }

        [TestMethod]
        public void RenderAutocompleteWithMultiple()
        {
            //Act
            var cut = RenderAndGetRootElement(props =>
            {
                props.Add(autocomplete => autocomplete.Multiple, true);
            });

            // Assert
            Assert.IsTrue(cut.ClassList.Contains("m-select--is-multi"));
        }

        [TestMethod]
        public void RenderAutocompleteWithSmallChips()
        {
            //Act
            var cut = Render(props =>
            {
                props.Add(a => a.Chips, true);
                props.Add(a => a.SmallChips, true);
            });
            
            // Assert
            cut.Find(".m-chip.m-size--small");
        }
    }
}