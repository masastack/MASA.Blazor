using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor.Test.Alert
{
    [TestClass]
    public class MButtonGroupTests : TestBase
    {
        [TestMethod]
        public void RenderMButtonGroupWithDark()
        {
            //Act
            var cut = RenderComponent<MAlert>(props=>
            {
                props.Add(buttongrop=>buttongrop.Dark,true);
            
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasBorderClass=classes.Contains("m-bin--is-elevated");
            //Assert
            Assert.IsTrue(hasBorderClass);

        }



    }
}
