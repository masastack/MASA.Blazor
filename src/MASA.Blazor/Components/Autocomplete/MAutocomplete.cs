using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Timer = System.Timers.Timer;

namespace MASA.Blazor
{
    public class MAutocomplete<TItem, TValue> : MSelect<TItem, TValue>
    {
        private Func<TItem, string, bool> _filter;

        protected Timer Timer { get; set; }

        [Parameter]
        public Func<TItem, string, bool> Filter
        {
            get
            {
                if (_filter == null)
                {
                    _filter = (item, query) => ItemText(item).ToLower().IndexOf(query.ToLower()) > -1;
                }

                return _filter;
            }
            set
            {
                _filter = value;
            }
        }

        [Parameter]
        public double Interval { get; set; } = 500;

        public override Dictionary<string, object> InputAttrs => new()
        {
            { "type", Type },
            //TODO:this can be more simple
            { "value", (Multiple || Chips) ? QueryText : (QueryText ?? string.Join(',', FormatText(Value))) },
            { "autocomplete", "off" }
        };

        public override IReadOnlyList<TItem> ComputedItems => Items.Where(r => QueryText == null || Filter(r, QueryText)).ToList();

        protected override void OnInitialized()
        {
            if (Timer == null && Interval > 0)
            {
                Timer = new Timer();
                Timer.Interval = Interval;
                Timer.Elapsed += Timer_Elapsed;
            }
        }

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            AbstractProvider
                .Merge<BMenu>(props =>
                {
                    props[nameof(MMenu.OffsetY)] = true;
                });

            CssProvider
                .Merge(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-autocomplete");
                })
                .Apply("mask", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-list-item__mask");
                });
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            InvokeAsync(async () =>
            {
                var args = new ChangeEventArgs()
                {
                    Value = QueryText
                };
                await base.HandleOnInput(args);

                Timer.Stop();
            });
        }

        public override async Task HandleOnBlur(FocusEventArgs args)
        {
            QueryText = default;
            HighlightIndex = -1;
            await base.HandleOnBlur(args);
        }

        public override Task HandleOnChange(ChangeEventArgs args)
        {
            QueryText = args.Value.ToString();
            return Task.CompletedTask;
        }

        public override async Task HandleOnInput(ChangeEventArgs args)
        {
            QueryText = args.Value.ToString();
            HighlightIndex = -1;
            Visible = true;

            if (OnInput.HasDelegate)
            {
                if (Timer != null && Interval > 0)
                {
                    if (!Timer.Enabled)
                    {
                        Timer.Start();
                    }
                    else
                    {
                        //restart
                        Timer.Stop();
                        Timer.Start();
                    }
                }
                else
                {
                    await base.HandleOnInput(args);
                }
            }
        }

        public override async Task HandleOnKeyDown(KeyboardEventArgs args)
        {
            await base.HandleOnKeyDown(args);

            switch (args.Code)
            {
                case "Enter":
                    if (!Multiple)
                    {
                        QueryText = null;
                    }
                    break;
                case "Backspace":
                    Visible = true;
                    if (Multiple)
                    {
                        if (Values.Count > 0 && string.IsNullOrEmpty(QueryText))
                        {
                            Values.RemoveAt(Values.Count - 1);
                            if (ValuesChanged.HasDelegate)
                            {
                                await ValuesChanged.InvokeAsync(Values);
                            }
                        }
                    }
                    else
                    {
                        if (Chips && !EqualityComparer<TValue>.Default.Equals(Value, default) && string.IsNullOrEmpty(QueryText))
                        {
                            Value = default;
                            if (ValueChanged.HasDelegate)
                            {
                                await ValueChanged.InvokeAsync(Value);
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        protected override void Dispose(bool disposing)
        {
            Timer?.Dispose();
            base.Dispose(disposing);
        }
    }
}
