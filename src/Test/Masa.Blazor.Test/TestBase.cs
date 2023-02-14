using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Masa.Blazor.Test
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
