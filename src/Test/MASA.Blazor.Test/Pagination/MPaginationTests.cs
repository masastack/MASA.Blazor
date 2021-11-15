using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bunit;
using BlazorComponent;
using Moq;

namespace MASA.Blazor.Test.Pagination
{
    [TestClass]
    public class MPaginationTests:TestBase
    {
        [TestMethod]
        public void RendePaginationWithCircle()
        {
            ////Act
            //var cut = RenderComponent<MPagination>(props =>
            //{
            //    props.Add(pagination => pagination.Circle, true);
            //});
            //var classes = cut.Instance.CssProvider.GetClass();
            //var hasCircleClass = classes.Contains("m-pagination--circle");

            //// Assert
            //Assert.IsTrue(hasCircleClass);
        }

        [TestMethod]
        public void RendePaginationWithDisabled()
        {
            //Act
            var cut = RenderComponent<MPagination>(props =>
            {
                props.Add(pagination => pagination.Disabled, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDisabledClass = classes.Contains("m-pagination--disabled");

            // Assert
            Assert.IsTrue(hasDisabledClass);
        }

        [TestMethod]
        public void RenderPaginationWithDark()
        {
            ////Act
            //var cut = RenderComponent<MPagination>(props =>
            //{
            //    props.Add(pagination => pagination.Dark, true);
            //});
            //var classes = cut.Instance.CssProvider.GetClass();
            //var hasDarkClass = classes.Contains("theme--dark");

            //// Assert
            //Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderPaginationWithLight()
        {
            ////Act
            //var cut = RenderComponent<MPagination>(props =>
            //{
            //    props.Add(pagination => pagination.Light, true);
            //});
            //var classes = cut.Instance.CssProvider.GetClass();
            //var hasLightClass = classes.Contains("theme--light");

            //// Assert
            //Assert.IsTrue(hasLightClass);
        }

        
    }
}
