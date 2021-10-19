using BlazorComponent;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor.Test.TextField
{
    [TestClass]
    public class MTextFieldTests : TestBase
    {
        [TestMethod]
        public void RenderNormal()
        {
            // Act
            var cut = RenderComponent<MTextField<string>>();
            var inputDiv = cut.Find("div");

            // Assert
            Assert.AreEqual(4, inputDiv.ClassList.Length);
            Assert.IsTrue(inputDiv.ClassList.Contains("m-input"));
            Assert.IsTrue(inputDiv.ClassList.Contains("theme--light"));
            Assert.IsTrue(inputDiv.ClassList.Contains("m-text-field"));
            Assert.IsTrue(inputDiv.ClassList.Contains("m-text-field--is-booted"));
            Assert.IsTrue(inputDiv.ClassList.Contains("m-input"));
        }

        [DataTestMethod]
        [DataRow("red")]
        [DataRow("blue")]
        public void RenderWithColorAndFocus(string color)
        {
            // Arrange
            var cut = RenderComponent<MTextField<string>>(props =>
            {
                props.Add(r => r.Color, color);
            });
            var inputDiv = cut.Find("div");
            var inputElement = cut.Find("input");

            // Act
            inputElement.Focus();

            // Assert
            Assert.AreEqual(6, inputDiv.ClassList.Length);
            Assert.IsTrue(inputDiv.ClassList.Contains(color + "--text"));
        }
    }
}
