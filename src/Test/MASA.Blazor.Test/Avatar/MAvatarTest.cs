using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor.Test.Avatar
{
    [TestClass]
    public class MAvatarTest:TestBase
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
        //}
        //[TestMethod]
        //public void RenderNormal()
        //{
        //    // Arrange & Act
        //    var cut = RenderComponent<MAvatar>();
        //    var inputDiv = cut.Find("div");

        //    // Assert
        //    Assert.AreEqual(2, inputDiv.ClassList.Length);
        //    Assert.IsTrue(inputDiv.ClassList.Contains("m-input"));
        //    Assert.IsTrue(inputDiv.ClassList.Contains("theme--light"));
        //}

        //[TestMethod]
        //public void RenderWithHeight()
        //{
        //    // Act
        //    var cut = RenderComponent<MAvatar>(props =>
        //    {
        //        props.Add(p => p.Height, 100);
        //    });
        //    var avatarDiv = cut.Find(".m-avatar");
        //    var style = avatarDiv.GetAttribute("style");

        //    // Assert
        //    Assert.AreEqual("height: 48px", style);
        //}
    }
}
