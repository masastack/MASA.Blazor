using System;
using System.Linq;
using AngleSharp.Dom;
using Bunit;
using Microsoft.AspNetCore.Components;
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

            JSInterop.Mode = JSRuntimeMode.Loose;
            JSInterop.SetupModule("./_content/Masa.Blazor/js/components/transition/index.js");
        }

        [TestCleanup]
        public void TearDown() => TestContext?.Dispose();
    }

    public abstract class TestBase<TComponent> : TestBase where TComponent : IComponent
    {
        private readonly string _rootTag;

        public TestBase()
        {
            _rootTag = "div";
        }

        public TestBase(string rootTag)
        {
            _rootTag = rootTag;
        }

        protected IRenderedComponent<TComponent> Render(
            Action<ComponentParameterCollectionBuilder<TComponent>> parameterBuilder = null)
        {
            return parameterBuilder is null
                ? TestContext!.RenderComponent<TComponent>()
                : TestContext!.RenderComponent(parameterBuilder);
        }

        protected IElement RenderAndGetRootElement(
            Action<ComponentParameterCollectionBuilder<TComponent>> parameterBuilder = null)
        {
            return Render(parameterBuilder).Find(_rootTag);
        }
    }
}