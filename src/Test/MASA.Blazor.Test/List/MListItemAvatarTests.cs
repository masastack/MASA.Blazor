using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bunit;
using BlazorComponent;
using Moq;

namespace MASA.Blazor.Test.List
{
    [TestClass]
    public class MListItemAvatarTests:TestBase
    {
        [TestMethod]
        public void RendeListItemAvatarWithHorizontal()
        {
            //Act
            var cut = RenderComponent<MListItemAvatar>(props =>
            {
                props.Add(listitemavatar => listitemavatar.Horizontal, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasHorizontalClass = classes.Contains("m-list-item__avatar--horizontal");

            // Assert
            Assert.IsTrue(hasHorizontalClass);
        }

        [TestMethod]
        public void RendeListItemAvatarWithTile()
        {
            //Act
            var cut = RenderComponent<MListItemAvatar>(props =>
            {
                props.Add(listitemavatar => listitemavatar.Tile, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasTileClass = classes.Contains("m-avatar-tile");

            // Assert
            Assert.IsTrue(hasTileClass);
        }

        [TestMethod]
        public void RendeListItemAvatarWithHorizontals()
        {
            //Act
            var cut = RenderComponent<MListItemAvatar>(props =>
            {
                props.Add(listitemavatar => listitemavatar.Horizontal, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasHorizontalClass = classes.Contains("m-avatar-tile");

            // Assert
            Assert.IsTrue(hasHorizontalClass);
        }
    }
}
