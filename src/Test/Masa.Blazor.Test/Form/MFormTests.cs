using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Masa.Blazor.Test.Form
{
    [TestClass]
    public class MFormTests : TestBase
    {
        [TestMethod]
        public void RenderWithChildContent()
        {
            // Arrange & Act
            var cut = RenderComponent<MForm>(props =>
            {
                props.Add(form => form.ChildContent, Counter => "<span>Hello world</span>");
            });
            var contentDiv = cut.Find(".m-form");

            // Assert
            contentDiv.Children.MarkupMatches("<span>Hello world</span>");
        }

        [TestMethod]
        public void RenderFormWithDisabled()
        {
            //Act
            var cut = RenderComponent<MForm>(props =>
            {
                props.Add(form => form.Disabled, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDisabledClass = classes.Contains("m-form");

            // Assert
            Assert.IsTrue(hasDisabledClass);
        }

        [TestMethod]
        public void RenderFormWithReadonly()
        {
            //Act
            var cut = RenderComponent<MForm>(props =>
            {
                props.Add(form => form.Readonly, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasReadonlyClass = classes.Contains("m-form");

            // Assert
            Assert.IsTrue(hasReadonlyClass);
        }
    }
}
