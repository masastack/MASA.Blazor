using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor.Test.Card
{
    [TestClass]
    public class MCardSubtitleTests:TestBase
    {
        [TestMethod]
        public void RenderWithChildContentt()
        {
            // Arrange & Act
            var cut = RenderComponent<MCardSubtitle>(props =>
            {
                props.Add(cardsubtitle => cardsubtitle.ChildContent, "<span>Hello world</span>");
            });
            var contentDiv = cut.Find(".m-card__subtitle");

            // Assert
            contentDiv.Children.MarkupMatches("<span>Hello world</span>");
        }
    }
}
