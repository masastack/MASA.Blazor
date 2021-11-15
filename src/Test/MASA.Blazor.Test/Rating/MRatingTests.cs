using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bunit;

namespace MASA.Blazor.Test.Rating
{
    [TestClass]
    public class MRatingTests:TestBase
    {
        [TestMethod]
        public void RendeRatingWithReadonly()
        {
            //Act
            var cut = RenderComponent<MRating>(props =>
            {
                props.Add(rating => rating.Readonly, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasReadonlyClass = classes.Contains("m-rating--readonly");
            // Assert
            Assert.IsTrue(hasReadonlyClass);
        }

        [TestMethod]
        public void RendeRatingWithDense()
        {
            //Act
            var cut = RenderComponent<MRating>(props =>
            {
                props.Add(rating => rating.Dense, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDenseClass = classes.Contains("m-rating--dense");
            // Assert
            Assert.IsTrue(hasDenseClass);
        }

        [TestMethod]
        public void RenderRatingWithDark()
        {
            //Act
            var cut = RenderComponent<MRating>(props =>
            {
                props.Add(rating => rating.Dark, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("theme--dark");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderRatingWithLight()
        {
            //Act
            var cut = RenderComponent<MRating>(props =>
            {
                props.Add(rating => rating.Light, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("theme--light");

            // Assert
            Assert.IsTrue(hasLightClass);
        }
    }
}
