using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Masa.Blazor.Test.Timeline
{
    [TestClass]
    public class MTimelineTests : TestBase
    {
        [TestMethod]
        public void RenderTimelineWithAlignTop()
        {
            //Act
            var cut = RenderComponent<MTimeline>(props =>
            {
                props.Add(timeline => timeline.AlignTop, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasAlignTopClass = classes.Contains("m-timeline--align-top");
            // Assert
            Assert.IsTrue(hasAlignTopClass);
        }

        [TestMethod]
        public void RenderTimelineWithDense()
        {
            //Act
            var cut = RenderComponent<MTimeline>(props =>
            {
                props.Add(timeline => timeline.Dense, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDenseClass = classes.Contains("m-timeline--dense");
            // Assert
            Assert.IsTrue(hasDenseClass);
        }

        [TestMethod]
        public void RenderTimelineWithReverse()
        {
            //Act
            var cut = RenderComponent<MTimeline>(props =>
            {
                props.Add(timeline => timeline.Reverse, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasReverseClass = classes.Contains("m-timeline--reverse");
            // Assert
            Assert.IsTrue(hasReverseClass);
        }

        [TestMethod]
        public void RenderTimelineWithDark()
        {
            //Act
            var cut = RenderComponent<MTimeline>(props =>
            {
                props.Add(timeline => timeline.Dark, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("theme--dark");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderTimelineWithLight()
        {
            //Act
            var cut = RenderComponent<MTimeline>(props =>
            {
                props.Add(timeline => timeline.Light, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("theme--light");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderWithChildContent()
        {
            // Arrange & Act
            var cut = RenderComponent<MTimeline>(props =>
            {
                props.Add(timeline => timeline.ChildContent, "<span>Hello world</span>");
            });
            var contentDiv = cut.Find(".m-timeline");

            // Assert
            contentDiv.Children.MarkupMatches("<span>Hello world</span>");
        }
    }
}
