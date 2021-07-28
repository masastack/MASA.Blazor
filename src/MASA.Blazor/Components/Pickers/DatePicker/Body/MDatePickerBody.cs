using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    internal partial class MDatePickerBody : BDatePickerBody<MDatePicker>
    {
        [Parameter]
        public string Color { get; set; }

        [Parameter]
        public EventCallback<int> OnDaySelected { get; set; }

        [Parameter]
        public EventCallback<int> OnMonthSelected { get; set; }

        [Parameter]
        public EventCallback<int> OnYearSelected { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> DateClick { get; set; }

        protected override void SetComponentClass()
        {
            AbstractProvider
                .Apply<BDatePickerYears, MDatePickerYears>(props =>
                {
                    props[nameof(MDatePickerYears.Color)] = Color;
                    props[nameof(MDatePickerYears.Min)] = Component.MinYear;
                    props[nameof(MDatePickerYears.Max)] = Component.MaxYear;
                    props[nameof(MDatePickerYears.Value)] = Component.TableYear;
                    props[nameof(MDatePickerYears.OnYearSelected)] = OnYearSelected;
                })
                .Apply<BDatePickerHeader, MDatePickerHeader>(props =>
                {
                    props[nameof(MDatePickerHeader.PrevIcon)] = Component.PrevIcon;
                    props[nameof(MDatePickerHeader.NextIcon)] = Component.NextIcon;
                    props[nameof(MDatePickerHeader.Color)] = Color;
                    props[nameof(MDatePickerHeader.Dark)] = Component.Dark;
                    props[nameof(MDatePickerHeader.Disabled)] = Component.Disabled;
                    props[nameof(MDatePickerHeader.Min)] = Component.Min;
                    props[nameof(MDatePickerHeader.Max)] = Component.Max;
                    props[nameof(MDatePickerHeader.Readonly)] = Component.Readonly;
                    props[nameof(MDatePickerHeader.ActivePicker)] = Component.ActivePicker;
                    props[nameof(MDatePickerHeader.Value)] = Component.TableDate;
                    props[nameof(MDatePickerHeader.OnPrevClick)] = EventCallback.Factory.Create<MouseEventArgs>(this, () =>
                    {
                        if (Component.ActivePicker == "DATE")
                        {
                            Component.AddMonths(-1);
                        }
                        else
                        {
                            Component.AddYears(-1);
                        }
                    });
                    props[nameof(MDatePickerHeader.OnNextClick)] = EventCallback.Factory.Create<MouseEventArgs>(this, () =>
                    {
                        if (Component.ActivePicker == "DATE")
                        {
                            Component.AddMonths(1);
                        }
                        else
                        {
                            Component.AddYears(1);
                        }
                    });
                    props[nameof(MDatePickerHeader.OnDateClick)] = DateClick;
                })
                .Apply<IDatePickerDateTable, MDatePickerDateTable>(props =>
                {
                    props[nameof(MDatePickerDateTable.Color)] = Color;
                    props[nameof(MDatePickerDateTable.Value)] = Component.Value;
                    props[nameof(MDatePickerDateTable.TableDate)] = Component.TableDate;
                    props[nameof(MDatePickerDateTable.OnDaySelected)] = OnDaySelected;
                    props[nameof(MDatePickerDateTable.Min)] = Component.Min;
                    props[nameof(MDatePickerDateTable.Max)] = Component.Max;
                })
                .Apply<IDatePickerMonthTable, MDatePickerMonthTable>(props =>
                {
                    props[nameof(MDatePickerMonthTable.Color)] = Color;
                    props[nameof(MDatePickerMonthTable.Value)] = Component.Value;
                    props[nameof(MDatePickerMonthTable.TableDate)] = Component.TableDate;
                    props[nameof(MDatePickerMonthTable.OnMonthSelected)] = OnMonthSelected;
                    props[nameof(MDatePickerMonthTable.Min)] = Component.Min;
                    props[nameof(MDatePickerMonthTable.Max)] = Component.Max;
                });
        }
    }
}
