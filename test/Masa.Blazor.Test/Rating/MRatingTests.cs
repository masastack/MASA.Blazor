using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Masa.Blazor.Test.Rating
{
    [TestClass]
    public class MRatingTests : TestBase
    {
        [TestMethod]
        public void RenderRatingWithClearable()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MRating>(props =>
            {
                props.Add(rating => rating.Clearable, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasClearableClass = classes.Contains("m-rating");

            // Assert
            Assert.IsTrue(hasClearableClass);
        }

        [TestMethod]
        public void RenderRatingWithDark()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
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
        public void RenderRatingWithDense()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MRating>(props =>
            {
                props.Add(rating => rating.Dense, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDenseClass = classes.Contains("m-rating");

            // Assert
            Assert.IsTrue(hasDenseClass);
        }

        [TestMethod]
        public void RenderRatingWithHalfIncrements()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MRating>(props =>
            {
                props.Add(rating => rating.HalfIncrements, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasHalfIncrementsClass = classes.Contains("m-rating");

            // Assert
            Assert.IsTrue(hasHalfIncrementsClass);
        }

        [TestMethod]
        public void RenderRatingWithHover()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MRating>(props =>
            {
                props.Add(rating => rating.Hover, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasHoverClass = classes.Contains("m-rating");

            // Assert
            Assert.IsTrue(hasHoverClass);
        }

        [TestMethod]
        public void RenderRatingWithLarge()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MRating>(props =>
            {
                props.Add(rating => rating.Large, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLargeClass = classes.Contains("m-rating");

            // Assert
            Assert.IsTrue(hasLargeClass);
        }

        [TestMethod]
        public void RenderRatingWithLength()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MRating>(props =>
            {
                props.Add(rating => rating.Length, 5);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLengthClass = classes.Contains("m-rating");

            // Assert
            Assert.IsTrue(hasLengthClass);
        }

        [TestMethod]
        public void RenderRatingWithLight()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MRating>(props =>
            {
                props.Add(rating => rating.Light, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("theme--light");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderRatingWithReadonly()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MRating>(props =>
            {
                props.Add(rating => rating.Readonly, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasReadonlyClass = classes.Contains("m-rating");

            // Assert
            Assert.IsTrue(hasReadonlyClass);
        }

        [TestMethod]
        public void RenderRatingWithSize()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MRating>(props =>
            {
                props.Add(rating => rating.Size, 10);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSizeClass = classes.Contains("m-rating");

            // Assert
            Assert.IsTrue(hasSizeClass);
        }

        [TestMethod]
        public void RenderRatingWithSmall()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MRating>(props =>
            {
                props.Add(rating => rating.Small, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSmallClass = classes.Contains("m-rating");

            // Assert
            Assert.IsTrue(hasSmallClass);
        }

        [TestMethod]
        public void RenderRatingWithValue()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MRating>(props =>
            {
                props.Add(rating => rating.Value, 0);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasValueClass = classes.Contains("m-rating");

            // Assert
            Assert.IsTrue(hasValueClass);
        }

        [TestMethod]
        public void RenderRatingWithXLarge()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MRating>(props =>
            {
                props.Add(rating => rating.XLarge, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasXLargeClass = classes.Contains("m-rating");

            // Assert
            Assert.IsTrue(hasXLargeClass);
        }

        [TestMethod]
        public void RenderRatingWithXSmall()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MRating>(props =>
            {
                props.Add(rating => rating.XSmall, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasXSmallClass = classes.Contains("m-rating");

            // Assert
            Assert.IsTrue(hasXSmallClass);
        }
    }
}
