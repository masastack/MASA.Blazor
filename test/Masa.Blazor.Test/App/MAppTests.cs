using System.Linq;
using Bunit;

namespace Masa.Blazor.Test.App
{
    [TestClass]
    public class MAppTests : TestBase
    {
        [TestMethod]
        public void Structure()
        {
            var cut = RenderComponent<MApp>();
            var app = cut.Find(".m-application.m-application--is-ltr.theme--light");
            Assert.IsTrue(app.FirstElementChild.ClassList.Contains("m-application__wrap"));
        }

        [TestMethod]
        public void ChildContent()
        {
            var cut = RenderComponent<MApp>(props =>
            {
                props.Add(app => app.ChildContent, "<span>Hello world</span>");
            });

            var wrap = cut.Find(".m-application__wrap");
            wrap.Children.MarkupMatches("<span>Hello world</span>");
        }

        [TestMethod]
        public void ToggleTheme()
        {
            var masaBlazor = Services.GetService<MasaBlazor>();
            masaBlazor.ToggleTheme();
            var cut = RenderComponent<MApp>();
            cut.Find(".m-application.theme--dark");
        }

        [TestMethod]
        public void Rtl()
        {
            var masaBlazor = Services.GetService<MasaBlazor>();
            masaBlazor.RTL = true;
            var cut = RenderComponent<MApp>();
            cut.Find(".m-application.m-application--is-rtl");
        }
    }
}