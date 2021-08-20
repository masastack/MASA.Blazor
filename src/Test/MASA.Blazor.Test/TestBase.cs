using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor.Test
{
    public abstract class TestBase : TestContextWrapper
    {
        [TestInitialize]
        public void Setup()
        {
            TestContext = new Bunit.TestContext();
            TestContext.Services.AddMasaBlazor();
        }

        [TestCleanup]
        public void TearDown() => TestContext?.Dispose();
    }
}
