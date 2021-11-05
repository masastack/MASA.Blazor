using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor.Test.TextArea
{
    [TestClass]
    public class MTextAreaTests:TestBase
    {
        [TestMethod]
        public void RendeMTextAreaWithAutoGrow()
        {
            ////Act
            //var cut = RenderComponent<MTextarea>(props =>
            //{
            //    props.Add(textarea => textarea.AutoGrow, true);
            //});
            //var classes = cut.Instance.CssProvider.GetClass();
            //var hasAutoGrowClass = classes.Contains("m-textarea--auto-grow");
            //// Assert
            //Assert.IsTrue(hasAutoGrowClass);
        }

        [TestMethod]
        public void RendeMTextAreaWithnoAutoGrow()
        {
            ////Act
            //var cut = RenderComponent<MTextarea>(props =>
            //{
            //    props.Add(textarea => textarea.AutoGrow, true);
            //});
            //var classes = cut.Instance.CssProvider.GetClass();
            //var hasAutoGrowClass = classes.Contains("m-textarea--no-resize");
            //// Assert
            //Assert.IsTrue(hasAutoGrowClass);
        }

        [TestMethod]
        public void RendeMTextAreaWithNoResize()
        {
            //Act
            var cut = RenderComponent<MTextarea>(props =>
            {
                props.Add(textarea => textarea.NoResize, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasAutoGrowClass = classes.Contains("m-textarea--no-resize");
            // Assert
            Assert.IsTrue(hasAutoGrowClass);
        }
    }
}
