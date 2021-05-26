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
    public partial class MDatePickerBody : BDatePickerBody<MDatePicker>
    {
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
                    props[nameof(MDatePickerYears.Color)] = Component.Color;
                    props[nameof(MDatePickerYears.Min)] = Component.MinYear;
                    props[nameof(MDatePickerYears.Max)] = Component.MaxYear;
                    props[nameof(MDatePickerYears.Value)] = Component.TableYear;
                    props[nameof(MDatePickerYears.OnYearSelected)] = OnYearSelected;
                })
                .Apply<BDatePickerHeader, MDatePickerHeader>(props =>
                {
                    props[nameof(MDatePickerHeader.PrevIcon)] = Component.PrevIcon;
                    props[nameof(MDatePickerHeader.NextIcon)] = Component.NextIcon;
                    props[nameof(MDatePickerHeader.Color)] = Component.Color;
                    props[nameof(MDatePickerHeader.Dark)] = Component.Dark;
                    props[nameof(MDatePickerHeader.Disabled)] = Component.Disabled;
                    props[nameof(MDatePickerHeader.Min)] = Component.ActivePicker == "DATE" ? Component.MinMonth : Component.MinYear;
                    props[nameof(MDatePickerHeader.Max)] = Component.ActivePicker == "DATE" ? Component.MaxMonth : Component.MaxYear;
                    props[nameof(MDatePickerHeader.Readonly)] = Component.Readonly;
                    props[nameof(MDatePickerHeader.Value)] = (StringNumber)(Component.ActivePicker == "DATE" ? $"{Component.TableYear.ToString().PadLeft(4, '0')}-{(Component.TableMonth.ToInt32()).ToString().PadLeft(2, '0')}" : Component.TableYear.ToString().PadLeft(4, '0'));
                    props[nameof(MDatePickerHeader.PrevClick)] = EventCallback.Factory.Create<MouseEventArgs>(this, () =>
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
                    props[nameof(MDatePickerHeader.NextClick)] = EventCallback.Factory.Create<MouseEventArgs>(this, () =>
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
                    props[nameof(MDatePickerHeader.DateClick)] = DateClick;
                })
                .Apply<IDatePickerDateTable, MDatePickerDateTable>(props =>
                {
                    props[nameof(MDatePickerDateTable.Value)] = Component.Value;
                    props[nameof(MDatePickerDateTable.TableDate)] = Component.TableDate;
                    props[nameof(MDatePickerDateTable.OnDaySelected)] = OnDaySelected;
                })
                .Apply<IDatePickerMonthTable, MDatePickerMonthTable>(props =>
                {
                    props[nameof(MDatePickerDateTable.Value)] = Component.Value;
                    props[nameof(MDatePickerMonthTable.TableDate)] = Component.TableDate;
                    props[nameof(MDatePickerMonthTable.OnMonthSelected)] = OnMonthSelected;
                });
        }
    }
}
