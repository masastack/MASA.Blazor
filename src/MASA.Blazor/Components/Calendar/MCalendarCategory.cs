using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneOf;

namespace MASA.Blazor
{
    public partial class MCalendarCategory : MCalendarDaily, ICalendarCategory
    {
        [Parameter]
        public OneOf<string, List<OneOf<string, Dictionary<string, object>>>> Categories { get; set; }

        [Parameter]
        public OneOf<string, Func<OneOf<string, Dictionary<string, object>>, string>> CategoryText { get; set; }

        [Parameter]
        public bool CategoryHideDynamic { get; set; }

        [Parameter]
        public bool CategoryShowAll { get; set; }

        [Parameter]
        public string CategoryForInvalid { get; set; } = string.Empty;

        [Parameter]
        public StringNumber CategoryDays { get; set; } = 1;

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssProvider.Merge(cssBuilder =>
            {
                cssBuilder
                    .Add("m-calendar-daily")
                    .Add("m-calendar-category")
                    .AddTheme(IsDark);
            })
            .Merge("dayHeader", cssBuilder =>
            {
                cssBuilder
                    .Add("m-calendar-category__columns");
            })
            .Merge("columnHeader", cssBuilder =>
             {
                 cssBuilder
                    .Add("m-calendar-category__column-header");
             })
            .Merge("category", cssBuilder =>
            {
                cssBuilder
                    .Add("m-calendar-category__category");
            })
            .Merge("day", cssBuilder =>
            {
                var timestamp = cssBuilder.Data as CalendarTimestamp;
                cssBuilder
                    .Add("m-calendar-daily__day")
                    .AddIf("m-present", () => timestamp?.Present ?? false)
                    .AddIf("m-past", () => timestamp?.Past ?? false)
                    .AddIf("m-future", () => timestamp?.Future ?? false);
            })
            .Merge("dayInterval", cssBuilder =>
            {
                cssBuilder
                    .Add("m-calendar-daily__day-interval");
            }, styleBuilder =>
            {
                //TODO ...styler({ ...interval, category }),
                styleBuilder
                    .AddHeight(IntervalHeight);
            })
            .Merge("columns", cssBuilder =>
            {
                cssBuilder
                    .Add("m-calendar-category__columns");
            })
            .Merge("column", cssBuilder =>
            {
                cssBuilder
                    .Add("m-calendar-category__column");
            });

            AbstractProvider
                .ApplyCalendarCategoryDefault();
        }

        public List<OneOf<string, Dictionary<string, object>>> ParsedCategories()
        {
            return CalendarParser.GetParsedCategories(Categories, CategoryText);
        }

        public CategoryContentProps GetCategoryScope(CategoryContentProps scope,
            OneOf<string, Dictionary<string, object>> category)
        {
            var cat = category.IsT1 && category.AsT1.ContainsKey("categoryName") &&
                category.AsT1["categoryName"].ToString() == CategoryForInvalid ? default : category;

            scope.Category = cat;

            return scope;
        }
    }
}
