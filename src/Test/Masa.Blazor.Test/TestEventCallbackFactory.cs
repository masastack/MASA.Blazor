using BlazorComponent;
using Bunit;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Masa.Blazor.Test
{
    public class TestEventCallbackFactory : IDisposable
    {
        public TestEventCallbackFactory()
        {
            TestContext = new TestContext();
            Reciever = TestContext.RenderComponent<EmptyComponent>();
        }

        public TestContext TestContext { get; }

        public IRenderedComponent<EmptyComponent> Reciever { get; set; }

        public EventCallback<TArgs> CreateEventCallback<TArgs>(Action callback)
        {
            return EventCallback.Factory.Create<TArgs>(Reciever.Instance, callback);
        }

        public EventCallback<TArgs> CreateEventCallback<TArgs>(Action<TArgs> callback)
        {
            return EventCallback.Factory.Create<TArgs>(Reciever.Instance, callback);
        }

        public EventCallback<TArgs> CreateEventCallback<TArgs>(Func<Task> callback)
        {
            return EventCallback.Factory.Create<TArgs>(Reciever.Instance, callback);
        }

        public void Dispose()
        {
            TestContext?.Dispose();
        }
    }
}
