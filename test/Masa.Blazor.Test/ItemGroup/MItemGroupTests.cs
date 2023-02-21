using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Masa.Blazor.Test.ItemGroup
{
    [TestClass]
    public class MItemGroupTests : TestBase
    {


        [TestMethod]
        public void RenderItemGroupWithDark()
        {
            //Act
            var cut = RenderComponent<MItemGroup>(props =>
            {
                props.Add(itemgroup => itemgroup.Dark, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasdarkClass = classes.Contains("theme--dark");

            // Assert
            Assert.IsTrue(hasdarkClass);
        }

        [TestMethod]
        public void RenderItemGroupWithLight()
        {
            //Act
            var cut = RenderComponent<MItemGroup>(props =>
            {
                props.Add(itemgroup => itemgroup.Light, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("theme--light");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderItemGroupWithMandatory()
        {
            //Act
            var cut = RenderComponent<MItemGroup>(props =>
            {
                props.Add(itemgroup => itemgroup.Mandatory, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasMandatoryClass = classes.Contains("m-item-group");

            // Assert
            Assert.IsTrue(hasMandatoryClass);
        }

        [TestMethod]
        public void RenderAlertWithElevation()
        {
            //Act
            var cut = RenderComponent<MItemGroup>(props =>
            {
                props.Add(alert => alert.Max, 2);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasElevationClass = classes.Contains("m-item-group");

            // Assert
            Assert.IsTrue(hasElevationClass);
        }

        [TestMethod]
        public void RenderItemGroupWithMultiple()
        {
            //Act
            var cut = RenderComponent<MItemGroup>(props =>
            {
                props.Add(itemgroup => itemgroup.Multiple, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasMandatoryClass = classes.Contains("m-item-group");

            // Assert
            Assert.IsTrue(hasMandatoryClass);
        }
    }
}
