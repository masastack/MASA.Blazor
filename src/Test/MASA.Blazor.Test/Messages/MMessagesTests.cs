using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor.Test.Messages
{
    [TestClass]
    public class MMessagesTests:TestBase
    {
        [TestMethod]
        public void RenderMessagesWithDark()
        {
            //Act
            var cut = RenderComponent<MMessages>(props =>
            {
                props.Add(messages => messages.Dark, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("theme--dark");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderMessagesWithLight()
        {
            //Act
            var cut = RenderComponent<MMessages>(props =>
            {
                props.Add(messages => messages.Light, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("theme--light");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        //[TestMethod]
        //public void RenderWithChildContentt()
        //{
        //    // Arrange & Act
        //    var cut = RenderComponent<MMessages>(props =>
        //    {
        //        props.Add(messages => messages.ChildContent, context => "<span>Hello world</span>");
        //    });
        //    var contentDiv = cut.Find(".m-messages");

        //    // Assert
        //    contentDiv.Children.MarkupMatches("<span>Hello world</span>");
        //}
    }
}
