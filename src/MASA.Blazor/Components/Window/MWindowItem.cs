using BlazorComponent;
using BlazorComponent.Web;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public class MWindowItem : BWindowItem
    {
        [Parameter]
        public string Transition { get; set; }

        [Parameter]
        public string ReverseTransition { get; set; }

        [CascadingParameter]
        public MWindow WindowGroup { get; set; }

        [Inject]
        public Document Document { get; set; }

        protected bool InTransition { get; set; }

        protected override string ComputedTransition
        {
            get
            {
                if (WindowGroup != null && !WindowGroup.InternalReverse)
                {
                    return Transition ?? WindowGroup.ComputedTransition;
                }

                return ReverseTransition ?? WindowGroup?.ComputedTransition;
            }
        }

        protected override async Task OnBeforeTransition()
        {
            if (InTransition)
            {
                return;
            }

            InTransition = true;
            if (WindowGroup.TransitionCount == 0)
            {
                WindowGroup.TransitionCount++;

                var el = Document.QuerySelector(WindowGroup.Ref);
                await el.UpdateWindowTransitionAsync(true);
            }
            else
            {
                WindowGroup.TransitionCount++;
            }
        }

        protected override async Task OnAfterTransition()
        {
            if (!InTransition)
            {
                return;
            }

            InTransition = false;
            if (WindowGroup.TransitionCount > 0)
            {
                WindowGroup.TransitionCount--;

                if (WindowGroup.TransitionCount == 0)
                {
                    var el = Document.QuerySelector(WindowGroup.Ref);
                    await el.UpdateWindowTransitionAsync(false);
                }
            }
        }

        protected override async Task OnEnterTo()
        {
            var el = Document.QuerySelector(WindowGroup.Ref);
            await el.UpdateWindowTransitionAsync(true, Ref);
        }

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-window-item")
                        .AddIf("m-window-item--active", () => IsActive);
                });
        }
    }
}