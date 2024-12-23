namespace Masa.Blazor.Test.Radio
{
    [TestClass]
    public class MRadioTests : TestBase
    {
        [TestMethod]
        public void RenderRadioWithIsDisabled()
        {
            //Act
            var cut = RenderComponent<MRadio<string>>(props =>
            {
                props.Add(counter => counter.Disabled, true);
            });
            var classes = cut.Instance.GetClass();
            var hasIsDisabledClass = classes.Contains("m-radio--is-disabled");

            // Assert
            Assert.IsTrue(hasIsDisabledClass);
        }

        [TestMethod]
        public void RenderRadioWithIsReadonly()
        {
            //Act
            var cut = RenderComponent<MRadio<string>>(props =>
            {
                props.Add(counter => counter.Readonly, true);
            });
            var classes = cut.Instance.GetClass();
            var hasIsDisabledClass = classes.Contains("m-radio");

            // Assert
            Assert.IsTrue(hasIsDisabledClass);
        }

        [TestMethod]
        public void RenderRadioWithRipple()
        {
            //Act
            var cut = RenderComponent<MRadio<string>>(props =>
            {
                props.Add(counter => counter.Ripple, true);
            });
            var classes = cut.Instance.GetClass();
            var hasIsDisabledClass = classes.Contains("m-radio");

            // Assert
            Assert.IsTrue(hasIsDisabledClass);
        }
    }
}
