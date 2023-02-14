using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Masa.Blazor.Test.List
{
    [TestClass]
    public class MListGroupTests : TestBase
    {
        [TestMethod]
        public void RenderListGroupWithDisabled()
        {
            //Act
            var cut = RenderComponent<MListGroup>(props =>
            {
                props.Add(listgroup => listgroup.Disabled, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDisabledClass = classes.Contains("m-list-group--disabled");

            // Assert
            Assert.IsTrue(hasDisabledClass);
        }

        [TestMethod]
        public void RenderListGroupWithNoAction()
        {
            //Act
            var cut = RenderComponent<MListGroup>(props =>
            {
                props.Add(listgroup => listgroup.NoAction, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasNoActionClass = classes.Contains("m-list-group--no-action");

            // Assert
            Assert.IsTrue(hasNoActionClass);
        }

        [TestMethod]
        public void RenderListGroupWithSubGroup()
        {
            //Act
            var cut = RenderComponent<MListGroup>(props =>
            {
                props.Add(listgroup => listgroup.SubGroup, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSubGroupClass = classes.Contains("m-list-group--sub-group");

            // Assert
            Assert.IsTrue(hasSubGroupClass);
        }
    }
}
