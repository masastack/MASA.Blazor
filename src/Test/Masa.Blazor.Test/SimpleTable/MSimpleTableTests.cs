using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Masa.Blazor.Test.SimpleTable
{
    [TestClass]
    public class MSimpleTableTests : TestBase
    {
        [TestMethod]
        public void RenderSimpleTableWithDark()
        {
            //Act
            var cut = RenderComponent<MSimpleTable>(props =>
            {
                props.Add(simpletable => simpletable.Dark, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("theme--dark");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderSimpleTableWithLight()
        {
            //Act
            var cut = RenderComponent<MSimpleTable>(props =>
            {
                props.Add(simpletable => simpletable.Light, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("theme--light");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderSimpleTableWithDense()
        {
            //Act
            var cut = RenderComponent<MSimpleTable>(props =>
            {
                props.Add(simpletable => simpletable.Dense, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDenseClass = classes.Contains("m-data-table");

            // Assert
            Assert.IsTrue(hasDenseClass);
        }

        [TestMethod]
        public void RenderSimpleTableWithFixedHeader()
        {
            //Act
            var cut = RenderComponent<MSimpleTable>(props =>
            {
                props.Add(simpletable => simpletable.FixedHeader, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFixedHeaderClass = classes.Contains("m-data-table");

            // Assert
            Assert.IsTrue(hasFixedHeaderClass);
        }

        [TestMethod]
        public void RenderSimpleTableWithHeight()
        {
            //Act
            var cut = RenderComponent<MSimpleTable>(props =>
            {
                props.Add(simpletable => simpletable.Height, 10);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasHeightClass = classes.Contains("m-data-table");

            // Assert
            Assert.IsTrue(hasHeightClass);
        }
    }
}
