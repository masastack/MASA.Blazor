using AngleSharp.Css.Dom;
using Bunit;

namespace Masa.Blazor.Test.Main
{
    [TestClass]
    public class MMainTests : TestBase<MMain>
    {
        [TestMethod]
        public void Structure()
        {
            var cut = Render();
            var root = cut.Find(".m-main");
            Assert.IsTrue(root.FirstElementChild.ClassList.Contains("m-main__wrap"));
        }

        [TestMethod]
        public void ChildContent()
        {
            var cut = Render(props => { props.Add(u => u.ChildContent, "<span>Hello world</span>"); });

            var wrap = cut.Find(".m-main__wrap");
            wrap.Children.MarkupMatches("<span>Hello world</span>");
        }

        [TestMethod]
        public void Application()
        {
            // assume application position was set by JSInterop
            var masaBlazor = Services.GetService<MasaBlazor>();
            masaBlazor.Application.Bar = 64;
            masaBlazor.Application.Footer = 128;
            masaBlazor.Application.Right = 256;
            masaBlazor.Application.Left = 256;

            var cut = Render();
            var styles = cut.Find(".m-main").GetStyle();
            Assert.AreEqual("64px", styles["padding-top"]);
            Assert.AreEqual("256px", styles["padding-left"]);
            Assert.AreEqual("256px", styles["padding-right"]);
            Assert.AreEqual("128px", styles["padding-bottom"]);
        }
    }
}