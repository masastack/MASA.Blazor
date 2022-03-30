using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Masa.Blazor.Test.Avatar
{
    [TestClass]
    public class MAvatarTest : TestBase
    {
        [TestMethod]
        public void RenderAvatarWithLeft()
        {
            //Act
            var cut = RenderComponent<MAvatar>(props =>
            {
                props.Add(avatar => avatar.Left, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLeftClass = classes.Contains("m-avatar--left");

            // Assert
            Assert.IsTrue(hasLeftClass);
        }

        [TestMethod]
        public void RenderAvatarWithRight()
        {
            //Act
            var cut = RenderComponent<MAvatar>(props =>
            {
                props.Add(avatar => avatar.Right, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasRightClass = classes.Contains("m-avatar--right");

            // Assert
            Assert.IsTrue(hasRightClass);
        }

        [TestMethod]
        public void RenderWithChildContentt()
        {
            // Arrange & Act
            var cut = RenderComponent<MAvatar>(props =>
            {
                props.Add(avatar => avatar.ChildContent, "<span>Hello world</span>");
            });
            var contentDiv = cut.Find(".m-avatar");

            // Assert
            contentDiv.Children.MarkupMatches("<span>Hello world</span>");
        }

        [TestMethod]
        public void RenderWithSize()
        {
            // Act
            var cut = RenderComponent<MAvatar>(props =>
            {
                props.Add(p => p.Size, 48);
            });
            var inputSlotDiv = cut.Find(".m-avatar");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("height: 48px;min-width: 48px;width: 48px", style);
        }

        [TestMethod]
        public void RenderAvatarWithRounded()
        {
            //Act
            var cut = RenderComponent<MAvatar>(props =>
            {
                props.Add(avatar => avatar.Rounded, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasRoundedClass = classes.Contains("rounded");

            // Assert
            Assert.IsTrue(hasRoundedClass);
        }

        [TestMethod]
        public void RenderWithHeight()
        {
            // Act
            var cut = RenderComponent<MAvatar>(props =>
            {
                props.Add(p => p.Height, 48);
            });
            var inputSlotDiv = cut.Find(".m-avatar");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("height: 48px;min-width: 48px;width: 48px;height: 48px", style);
        }

        [TestMethod]
        public void RenderWithWidth()
        {
            // Act
            var cut = RenderComponent<MAvatar>(props =>
            {
                props.Add(p => p.Width, 48);
            });
            var inputSlotDiv = cut.Find(".m-avatar");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("height: 48px;min-width: 48px;width: 48px;width: 48px", style);
        }

        [TestMethod]
        public void RenderWithMaxHeight()
        {
            // Act
            var cut = RenderComponent<MAvatar>(props =>
            {
                props.Add(p => p.MaxHeight, 48);
            });
            var inputSlotDiv = cut.Find(".m-avatar");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("height: 48px;min-width: 48px;width: 48px;max-height: 48px", style);
        }

        [TestMethod]
        public void RenderWithMaxWidth()
        {
            // Act
            var cut = RenderComponent<MAvatar>(props =>
            {
                props.Add(p => p.MaxWidth, 48);
            });
            var inputSlotDiv = cut.Find(".m-avatar");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("height: 48px;min-width: 48px;width: 48px;max-width: 48px", style);
        }

        [TestMethod]
        public void RenderWithMinHeight()
        {
            // Act
            var cut = RenderComponent<MAvatar>(props =>
            {
                props.Add(p => p.MinHeight, 48);
            });
            var inputSlotDiv = cut.Find(".m-avatar");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("height: 48px;min-width: 48px;width: 48px;min-height: 48px", style);
        }

        [TestMethod]
        public void RenderWithMinWidth()
        {
            // Act
            var cut = RenderComponent<MAvatar>(props =>
            {
                props.Add(p => p.MinWidth, 48);
            });
            var inputSlotDiv = cut.Find(".m-avatar");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("height: 48px;min-width: 48px;width: 48px;min-width: 48px", style);
        }
    }
}
