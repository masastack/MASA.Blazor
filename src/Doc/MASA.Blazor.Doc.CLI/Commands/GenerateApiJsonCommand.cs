using MASA.Blazor.Doc.CLI.Interfaces;
using Microsoft.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components;

namespace MASA.Blazor.Doc.CLI.Commands
{
    public class GenerateApiJsonCommand : IAppCommand
    {
        private static readonly Dictionary<string, string[]> _dic = new()
        {
            { "Avatar", new string[] { "Avatars" } },
            { "Badge", new string[] { "Badges" } },
            { "Banner", new string[] { "Banners" } },
            { "Border", new string[] { "Borders" } },
            { "Breadcrumbs", new string[] { "Breadcrumbs" } },
            { "BreadcrumbsDivider", new string[] { "Breadcrumbs" } },
            { "BreadcrumbsItem", new string[] { "Breadcrumbs" } },
            { "Button", new string[] { "Buttons", "Button Groups" } },
            { "ButtonGroup", new string[] { "Button groups" } },
            { "Calendar", new string[] { "Calendars" } },
            { "CalendarDaily", new string[] { "Calendars" } },
            { "CalendarMonthly", new string[] { "Calendars" } },
            { "CalendarWeekly", new string[] { "Calendars" } },
            { "Card", new string[] { "Cards" } },
            { "CardActions", new string[] { "Cards" } },
            { "CardSubtitle", new string[] { "Cards" } },
            { "CardText", new string[] { "Cards" } },
            { "CardTitle", new string[] { "Cards" } },
            { "Cascader", new string[] { "Cascaders" } },
            { "CascaderMenu", new string[] { "Cascaders" } },
            { "CascaderSelectList", new string[] { "Cascaders" } },
            { "CascaderSelectOption", new string[] { "Cascaders" } },
            { "Checkbox", new string[] { "Checkboxes" } },
            { "Chip", new string[] { "Chips" } },
            { "ChipGroup", new string[] { "Chip groups" } },
            { "Col", new string[] { "Grid system" } },
            { "ColorPicker", new string[] { "Color pickers" } },
            { "ColorPickerCanvas", new string[] { "Color pickers" } },
            { "ColorPickerEdit", new string[] { "Color pickers" } },
            { "ColorPickerPreview", new string[] { "Color pickers" } },
            { "Container", new string[] { "Grid system" } },
            { "DataFooter", new string[] { "Data iterators" } },
            { "DataIterator", new string[] { "Data iterators" } },
            { "DataTable", new string[] { "Data tables" } },
            { "DataTableHeader", new string[] { "Data tables" } },
            { "DataTableRow", new string[] { "Data tables" } },
            { "DataTableRowGroup", new string[] { "Data tables" } },
            { "DatePicker", new string[] { "Date pickers" } },
            { "DatePickerDateTable", new string[] { "Date pickers" } },
            { "DatePickerHeader", new string[] { "Date pickers" } },
            { "DatePickerMonthTable", new string[] { "Date pickers" } },
            { "DatePickerTable", new string[] { "Date pickers" } },
            { "DatePickerTitle", new string[] { "Date pickers" } },
            { "DatePickerYears", new string[] { "Date pickers" } },
            { "Dialog", new string[] { "Dialogs" } },
            { "Divider", new string[] { "Dividers" } },
            { "ECharts", new string[] { "ECharts" } },
            { "Message", new string[] { "Presets" } },
            { "ExpansionPanel", new string[] { "Expansion panels" } },
            { "ExpansionPanelContent", new string[] { "Expansion panels" } },
            { "ExpansionPanelHeader", new string[] { "Expansion panels" } },
            { "ExpansionPanels", new string[] { "Expansion panels" } },
            { "FileInput", new string[] { "File inputs" } },
            { "Footer", new string[] { "Footers" } },
            { "Form", new string[] { "Forms" } },
            { "Hover", new string[] { "Hover" } },
            { "Icon", new string[] { "Icons" } },
            { "Image", new string[] { "Images" } },
            { "Input", new string[] { "Text fields" } },
            { "Item", new string[] { "Item groups" } },
            { "ItemGroup", new string[] { "Item groups" } },
            { "Label", new string[] { "Text fields" } },
            { "List", new string[] { "Lists" } },
            { "ListGroup", new string[] { "Lists", "List item groups" } },
            { "ListItem", new string[] { "Lists", "List item groups" } },
            { "ListItemAction", new string[] { "Lists", "List item groups" } },
            { "ListItemActionText", new string[] { "Lists", "List item groups" } },
            { "ListItemAvatar", new string[] { "Lists", "List item groups" } },
            { "ListItemContent", new string[] { "Lists", "List item groups" } },
            { "ListItemGroup", new string[] { "Lists", "List item groups" } },
            { "ListItemIcon", new string[] { "Lists" } },
            { "ListItemSubtitle", new string[] { "Lists", "List item groups" } },
            { "ListItemTitle", new string[] { "Lists", "List item groups" } },
            { "Main", new string[] { "Grid system" } },
            { "Menu", new string[] { "Menus" } },
            { "Messages", new string[] { "Messages" } },
            { "NavigationDrawer", new string[] { "Navigation drawers" } },
            { "Modal", new string[] { "Presets" } },
            { "Overlay", new string[] { "Overlay" } },
            { "Pagination", new string[] { "Pagination" } },
            { "ProgressCircular", new string[] { "Progress circulars" } },
            { "ProgressLinear", new string[] { "Progress linears" } },
            { "Radio", new string[] { "Radio" } },
            { "RadioGroup", new string[] { "Radio groups" } },
            { "RangeSlider", new string[] { "Range sliders" } },
            { "Rating", new string[] { "Ratings" } },
            { "Responsive", new string[] { "Grid system" } },
            { "Row", new string[] { "Grid system" } },
            { "Select", new string[] { "Selects" } },
            { "SelectList", new string[] { "Selects" } },
            { "SelectOption", new string[] { "Selects" } },
            { "Sheet", new string[] { "Sheets" } },
            { "SimpleCheckbox", new string[] { "Checkboxes" } },
            { "SimpleTable", new string[] { "Simple tables" } },
            { "SkeletonLoader", new string[] { "Skeleton loaders" } },
            { "SlideGroup", new string[] { "Slide groups" } },
            { "SlideItem", new string[] { "Slide groups" } },
            { "Slider", new string[] { "Sliders" } },
            { "Snackbar", new string[] { "Snackbars" } },
            { "Spacer", new string[] { "Grid system" } },
            { "Stepper", new string[] { "Steppers" } },
            { "StepperContent", new string[] { "Steppers" } },
            { "StepperHeader", new string[] { "Steppers" } },
            { "StepperItems", new string[] { "Steppers" } },
            { "StepperStep", new string[] { "Steppers" } },
            { "Subheader", new string[] { "Subheaders" } },
            { "Switch", new string[] { "Switches" } },
            { "SystemBar", new string[] { "System bars" } },
            { "Tab", new string[] { "Tabs" } },
            { "TabItem", new string[] { "Tabs" } },
            { "Table", new string[] { "Data tables" } },
            { "TableCol", new string[] { "Data tables" } },
            { "TableFooter", new string[] { "Data tables" } },
            { "TableFooterButton", new string[] { "Data tables" } },
            { "TableHeader", new string[] { "Data tables" } },
            { "TableLoading", new string[] { "Data tables" } },
            { "Tabs", new string[] { "Tabs" } },
            { "TabsBar", new string[] { "Tabs" } },
            { "TabsItems", new string[] { "Tabs" } },
            { "TabsSlider", new string[] { "Tabs" } },
            { "Textarea", new string[] { "Textareas" } },
            { "TextField", new string[] { "Text fields" } },
            { "Timeline", new string[] { "Timelines" } },
            { "TimelineItem", new string[] { "Timelines" } },
            { "TimePicker", new string[] { "Timelines" } },
            { "TimePickerClock", new string[] { "Timelines" } },
            { "TimePickerTitle", new string[] { "Timelines" } },
            { "Toolbar", new string[] { "Toolbars" } },
            { "ToolbarItems", new string[] { "Toolbars" } },
            { "ToolbarTitle", new string[] { "Toolbars" } },
            { "Tooltip", new string[] { "Tooltips" } },
            { "Treeview", new string[] { "Treeview" } },
            { "TreeviewNode", new string[] { "Treeview" } },
            { "Upload", new string[] { "Uploads" } },
            { "VirtualScroll", new string[] { "Virtual scroller" } },
            { "Window", new string[] { "Windows" } },
            { "WindowItem", new string[] { "Windows" } },
            { "Alert", new string[] { "Alerts" } },
            { "AlertDismissButton", new string[] { "Alerts" } },
            { "AlertIcon", new string[] { "Alerts" } },
            { "App", new string[] { "Application" } },
            { "AppBar", new string[] { "App bars" } },
            { "AppBarNavIcon", new string[] { "App bars" } },
            { "AppBarTitle", new string[] { "App bars" } },
            { "Autocomplete", new string[] { "Autocompletes" } },
        };
        public string Name => "api2json";

        public void Execute(CommandLineApplication command)
        {
            command.Description = "Generate json file for api";
            command.HelpOption();

            var assemblyDirArgument = command.Argument(
                "assembly Path", "[Required] The Path of assembly file.");

            var outputArgument = command.Argument(
                "output", "[Required] The directory where the json file to output");        
          
            command.OnExecute(() =>
            {
                string assemblyPath = assemblyDirArgument.Value;
                string output = outputArgument.Value;

                if (string.IsNullOrEmpty(assemblyPath) || !File.Exists(assemblyPath))
                {
                    Console.WriteLine("Invalid assemblyPath.");
                    return 1;
                }

                if (string.IsNullOrEmpty(output))
                {
                    output = "./";
                }
                GenerateApiFiles(output,new[] { "zh-CN", "en-US" });

                return 0;
            });         
        }

        void GenerateApiFiles(string output,string[] languages)
        {
            var assembly = typeof(MApp).Assembly;//Assembly.LoadFile(assemblyPath);
            var componentBaseType = typeof(ComponentBase);
            var componentTypes = assembly.GetTypes().Where(type => componentBaseType.IsAssignableFrom(type) && type.Name.StartsWith("M"));
            var apis = new List<Api>();

            foreach (var componentType in componentTypes)
            {
                var paramterProps = componentType.GetProperties().Where(prop => prop.CustomAttributes.Any(attribute => attribute.AttributeType == typeof(ParameterAttribute)));
                var contentProps = paramterProps.Where(prop => prop.PropertyType == typeof(RenderFragment) || (prop.PropertyType.IsGenericType && prop.PropertyType == typeof(RenderFragment<>).MakeGenericType(prop.PropertyType.GenericTypeArguments[0])));
                var eventProps = paramterProps.Where(prop => prop.PropertyType == typeof(EventCallback) || (prop.PropertyType.IsGenericType && prop.PropertyType == typeof(EventCallback<>).MakeGenericType(prop.PropertyType.GenericTypeArguments[0])));
                var defaultProps = paramterProps.Where(props => contentProps.Any(cprops => cprops == props) is false && eventProps.Any(eprops => eprops == props) is false);

                var title = GetComponentName(componentType);

                if (!_dic.ContainsKey(title))
                {
                    continue;
                }

                _dic.TryGetValue(title, out var components);

                var api = new Api
                {
                    Title = title,
                    Components = components,
                    Props = defaultProps.Where(prop => IgnorePrpps(prop.Name)).Select(prop => new Prop
                    {
                        Name = prop.Name,
                        Type = prop.PropertyType.Name,
                        Default = GetDefaultValue(prop.PropertyType),
                        Description = "",
                    }).OrderBy(p => p.Name).ToArray(),
                    Contents = contentProps.Select(prop => new Content
                    {
                        Name = prop.Name,
                        Description = ""
                    }).OrderBy(content => content.Name).ToArray(),
                    Events = eventProps.Select(prop => new Event
                    {
                        Name = prop.Name,
                        Description = ""
                    }).OrderBy(e => e.Name).ToArray(),
                };

                apis.Add(api);
            }

            foreach (var language in languages)
            {
                var files = Directory.GetFiles(output).Where(file => file.EndsWith($".{language}.json"));
                var jsonOption = new JsonSerializerOptions()
                {
                    WriteIndented = true,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                };

                var basepath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var propDescriptionMap = JsonSerializer.Deserialize<string[][]>(File.ReadAllText($"{basepath}/ApiSettings/propDefaultDescription.{language}.json"))
                                                        .ToDictionary(prop => prop[0], prop => prop[1]);

                var contentDescriptionMap = JsonSerializer.Deserialize<string[][]>(File.ReadAllText($"{basepath}/ApiSettings/contentDefaultDescription.{language}.json"))
                                                        .ToDictionary(prop => prop[0], prop => prop[1]);

                foreach (var api in apis)
                {
                    var file = files.FirstOrDefault(f => f.Contains($"M{api.Title}.{language}.json"));
                    if (file is not null)
                    {
                        var oldApi = JsonSerializer.Deserialize<Api>(File.ReadAllText(file));
                        foreach (var prop in api.Props)
                        {
                            var oldProp = oldApi.Props.FirstOrDefault(p => p.Name == prop.Name);
                            if (oldProp is not null)
                            {
                                prop.Description = oldProp.Description;
                                prop.Default = oldProp.Default;
                            }
                        }
                    }
                    else file = $"{output}/M{api.Title}.{language}.json";

                    foreach (var prop in api.Props)
                    {
                        if(propDescriptionMap.ContainsKey(prop.Name))
                        {
                            prop.Description = propDescriptionMap[prop.Name];
                        }
                    }
                    foreach (var content in api.Contents)
                    {
                        if(contentDescriptionMap.ContainsKey(content.Name))
                        {
                            content.Description = contentDescriptionMap[content.Name];
                        }
                    }

                    File.WriteAllText(file, JsonSerializer.Serialize(api, jsonOption), Encoding.UTF8);

                    //md
                    var mdFile = $"{output}/M{api.Title}.{language}.md";
                    var mdContent = JsonSerializer.Deserialize<List<string>>(File.ReadAllText($"{basepath}/ApiSettings/mdContent.{language}.json"));
                    mdContent[mdContent.IndexOf("[title]")] = $"title: {api.Title}";
                    File.WriteAllLines(mdFile, mdContent);
                }
            }
        }

        string GetComponentName(Type componentType)
        {
            if (componentType.IsGenericType) return componentType.Name.Remove(componentType.Name.IndexOf('`')).Remove(0, 1);
            else return componentType.Name.Remove(0, 1);
        }

        bool IgnorePrpps(string name)
        {
            return name != "Attributes" && name != "RefBack";
        }

        string GetDefaultValue(Type type)
        {
            if (type.IsValueType)
            {
                if (type == typeof(bool)) return "false";
                if (type == typeof(int)) return "0";
                else return "";
            }
            else return "null";
        }
    }

    public class Api
    {
        public string Title { get; set; }

        public string[] Components { get; set; }

        public Prop[] Props { get; set; }

        public Content[] Contents { get; set; }

        public Event[] Events { get; set; }
    }

    public class Prop
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public string Default { get; set; }

        public string Description { get; set; }
    }

    public class Content
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class Event
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}


