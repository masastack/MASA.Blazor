using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor.Test.Button
{
    [TestClass]
    public class MButtonTests: TestBase
    {
        [TestMethod]
        public void RenderButtonWithBlock() 
        {
             //Act
             var cut = RenderComponent<MButton>(props=>
             {
                 props.Add(button => button.Block, true);
             });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasBlockClass = classes.Contains("m-btn--block");

            // Assert
            Assert.IsTrue(hasBlockClass);
        }
        [TestMethod]
        public void RenderButtonWithDepressed()
        {
            //Act
            var cut = RenderComponent<MButton>(props =>
            {
                props.Add(button => button.Depressed, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var noElevatedClass = !classes.Contains("m-bin--is-elevated");

            // Assert
            Assert.IsTrue(noElevatedClass);
        }
        [TestMethod]
        public void RenderButtonWithFab()
        {
            //Act
            var cut = RenderComponent<MButton>(props =>
            {
                props.Add(button => button.Fab, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFabClass = classes.Contains("m-btn--fab");

            // Assert
            Assert.IsTrue(hasFabClass);
        }
        [TestMethod]
        public void RenderButtonWithRound()
        {
            //Act
            var cut = RenderComponent<MButton>(props =>
            {
                props.Add(button => button.Rounded, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasRoundClass = classes.Contains("m-btn--round");

            // Assert
            Assert.IsTrue(hasRoundClass);
        }
        [TestMethod]
        public void RenderButtonWithDefault()
        {
            //Act
            var cut = RenderComponent<MButton>(props =>
            {
                props.Add(button => button.Default, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDefaultClass = classes.Contains("m-size--default");

            // Assert
            Assert.IsTrue(hasDefaultClass);
        }
        [TestMethod]
        public void RenderButtonWithOutlined()
        {
            //Act
            var cut = RenderComponent<MButton>(props =>
            {
                props.Add(button => button.Outlined, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasoutlinedClass = classes.Contains("m-btn--outlined");

            // Assert
            Assert.IsTrue(hasoutlinedClass);
        }
        [TestMethod]
        public void RenderButtonWithPlain()
        {
            //Act
            var cut = RenderComponent<MButton>(props =>
            {
                props.Add(button => button.Plain, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasPlainClass = classes.Contains("m-btn--plain");

            // Assert
            Assert.IsTrue(hasPlainClass);
        }
        [TestMethod]
        public void RenderButtonWithRounded()
        {
            //Act
            var cut = RenderComponent<MButton>(props =>
            {
                props.Add(button => button.Rounded, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasRoundedClass = classes.Contains("m-btn--rounded");

            // Assert
            Assert.IsTrue(hasRoundedClass);

        }
        [TestMethod]
        public void RenderButtonWithLight()
        {
            //Act
            var cut = RenderComponent<MButton>(props =>
            {
                props.Add(button => button.Light, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("theme--light");

            // Assert
            Assert.IsTrue(hasLightClass);
        
        }
        [TestMethod]
        public void RenderButtonWithText()
        {
            //Act
            var cut = RenderComponent<MButton>(props =>
            {
                props.Add(button => button.Text, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasTextClass = classes.Contains("m-btn--text");

            // Assert
            Assert.IsTrue(hasTextClass);
        }
        [TestMethod]
        public void RenderButtonWithTile()
        {
            //Act
            var cut = RenderComponent<MButton>(props =>
            {
                props.Add(button => button.Tile, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasTileClass = classes.Contains("m-btn--tile");

            // Assert
            Assert.IsTrue(hasTileClass);
        }
        [TestMethod]
        public void RenderButtonWithIcon()
        {
            //Act
            var cut = RenderComponent<MButton>(props =>
            {
                props.Add(button => button.Icon, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasIconClass = classes.Contains("m-btn--icon");

            // Assert
            Assert.IsTrue(hasIconClass);
        }
        [TestMethod]
        public void RenderButtonWithAbsolute()
        {
            //Act
            var cut = RenderComponent<MButton>(props =>
            {
                props.Add(button => button.Absolute, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasAbsoluteClass = classes.Contains("m-btn--absolute");

            // Assert
            Assert.IsTrue(hasAbsoluteClass);
        }
        [TestMethod]
        public void RenderButtonWithBottom()
        {
            //Act
            var cut = RenderComponent<MButton>(props =>
            {
                props.Add(button => button.Bottom, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasBottomClass = classes.Contains("m-btn--bottom");

            // Assert
            Assert.IsTrue(hasBottomClass);
        }
        [TestMethod]
        public void RenderButtonWithDisabled()
        {
            //Act
            var cut = RenderComponent<MButton>(props =>
            {
                props.Add(button => button.Disabled, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDisabledClass = classes.Contains("m-btn--disabled");

            // Assert
            Assert.IsTrue(hasDisabledClass);
        }
        [TestMethod]
        public void RenderButtonWithFixed()
        {
            //Act
            var cut = RenderComponent<MButton>(props =>
            {
                props.Add(button => button.Fixed, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFixedClass = classes.Contains("m-btn--fixed");

            // Assert
            Assert.IsTrue(hasFixedClass);
        }
    }
}
