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
    public class MAutocomplete<TItem, TItemValue, TValue> : MSelect<TItem, TItemValue, TValue>
    {
        private Func<TItem, string, bool> _filter;

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
            set { _filter = value; }
        }

        [Parameter]
        public EventCallback<string> OnSearchInputUpdate { get; set; }

        public override Dictionary<string, object> InputAttrs => new()
        {
            { "type", Type },
            //TODO:this can be more simple
            { "value", (Multiple || Chips) ? QueryText : (QueryText ?? string.Join(',', FormatText(Value))) },
            { "autocomplete", "off" }
        };

        public override List<string> Text
        {
            get
            {
                if (Multiple || Chips)
                {
                    return base.Text;
                }

                //By default,we use value instead
                return new List<string>();
            }
        }

        protected override BMenuProps GetDefaultMenuProps()
        {
            var props = base.GetDefaultMenuProps();
            props.OffsetY = true;

            // props.OffsetOverflow = true;
            // props.Transition = false;

            return props;
        }

        public override IReadOnlyList<TItem> ComputedItems => Items.Where(r => QueryText == null || Filter(r, QueryText)).ToList();

        public override bool IsDirty => !string.IsNullOrEmpty(QueryText) || base.IsDirty;

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            AbstractProvider
                .Merge<BMenu, MMenu>();

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

        public override async Task HandleOnBlurAsync(FocusEventArgs args)
        {
            QueryText = default;
            HighlightIndex = -1;
            await base.HandleOnBlurAsync(args);
        }

        public override async Task HandleOnInputAsync(ChangeEventArgs args)
        {
            QueryText = args.Value.ToString();
            HighlightIndex = -1;

            if (OnSearchInputUpdate.HasDelegate)
            {
                await OnSearchInputUpdate.InvokeAsync(QueryText);
            }

            await base.HandleOnInputAsync(args);
        }

        public override async Task HandleOnKeyDownAsync(KeyboardEventArgs args)
        {
            await base.HandleOnKeyDownAsync(args);

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
                        var values = Values;
                        if (values.Count > 0 && string.IsNullOrEmpty(QueryText))
                        {
                            values.RemoveAt(values.Count - 1);

                            //Since EditContext validate model,we should update outside value of model first
                            if (OnChange.HasDelegate)
                            {
                                await OnChange.InvokeAsync((TValue)values);
                            }
                            else
                            {
                                //We don't want render twice
                                if (ValueChanged.HasDelegate)
                                {
                                    await ValueChanged.InvokeAsync((TValue)values);
                                }
                            }

                            InternalValue = (TValue)values;
                        }
                    }
                    else
                    {
                        if (Chips && !EqualityComparer<TValue>.Default.Equals(InternalValue, default) && string.IsNullOrEmpty(QueryText))
                        {
                            //Since EditContext validate model,we should update outside value of model first
                            if (OnChange.HasDelegate)
                            {
                                await OnChange.InvokeAsync(default);
                            }
                            else
                            {
                                //We don't want render twice
                                if (ValueChanged.HasDelegate)
                                {
                                    await ValueChanged.InvokeAsync(default);
                                }
                            }

                            InternalValue = default;
                        }
                    }

                    break;
                default:
                    break;
            }
        }

        public override async Task HandleOnClearClickAsync(MouseEventArgs args)
        {
            if (!string.IsNullOrEmpty(QueryText))
            {
                QueryText = null;
            }

            await base.HandleOnClearClickAsync(args);
        }
    }
}