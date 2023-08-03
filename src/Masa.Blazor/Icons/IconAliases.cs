namespace Masa.Blazor;

public class IconAliases
{
    /// <summary>
    /// Complete icon used in Chip, Stepper and MobileCascader.
    /// </summary>
    public Icon? Complete { get; set; }
    
    /// <summary>
    /// Cancel icon used in Alert.
    /// </summary>
    public Icon? Cancel { get; set; }

    /// <summary>
    /// Close icon used in TextField, DataTable, Snackbar, Modal/Drawer and PageTabs.
    /// </summary>
    public Icon? Close { get; set; }

    /// <summary>
    /// Delete icon used in Chip.
    /// </summary>
    public Icon? Delete { get; set; }

    /// <summary>
    /// Clear icon used in TextField and ImageCaptcha.
    /// </summary>
    public Icon? Clear { get; set; }

    /// <summary>
    /// Success icon used in Alert, CopyableText.
    /// </summary>
    public Icon? Success { get; set; }

    /// <summary>
    /// Info icon used in Alert.
    /// </summary>
    public Icon? Info { get; set; }

    /// <summary>
    /// Warning icon used in Alert.
    /// </summary>
    public Icon? Warning { get; set; }

    /// <summary>
    /// Error icon used in Alert and Stepper.
    /// </summary>
    public Icon? Error { get; set; }

    /// <summary>
    /// Prev icon used in DataFooter, DatePicker, Pagination, SlideGroup and Window.
    /// </summary>
    public Icon? Prev { get; set; }

    /// <summary>
    /// Next icon used in Cascader, DataFooter, DatePicker, Pagination, SlideGroup and Window.
    /// </summary>
    public Icon? Next { get; set; }

    /// <summary>
    /// CheckboxOn icon used in SimpleCheckbox, Checkbox and Treeview.
    /// </summary>
    public Icon? CheckboxOn { get; set; }

    /// <summary>
    /// CheckboxOff icon used in SimpleCheckbox, Checkbox and Treeview.
    /// </summary>
    public Icon? CheckboxOff { get; set; }

    /// <summary>
    /// CheckboxIndeterminate icon used in Checkbox.
    /// </summary>
    public Icon? CheckboxIndeterminate { get; set; }

    /// <summary>
    /// Delimiter icon used in Carousel.
    /// </summary>
    public Icon? Delimiter { get; set; }

    /// <summary>
    /// Sort icon used in DataTable.
    /// </summary>
    public Icon? Sort { get; set; }

    /// <summary>
    /// Expand icon used in ExpansionPanel, TextField, DataTable and ListGroup.
    /// </summary>
    public Icon? Expand { get; set; }

    /// <summary>
    /// Menu icon used in AppBar.
    /// </summary>
    public Icon? Menu { get; set; }

    /// <summary>
    /// Subgroup icon used in ListGroup, Select and Treeview.
    /// </summary>
    public Icon? Subgroup { get; set; }

    /// <summary>
    /// Dropdown icon used in Select.
    /// </summary>
    public Icon? Dropdown { get; set; }

    /// <summary>
    /// RadioOn icon used in Radio.
    /// </summary>
    public Icon? RadioOn { get; set; }

    /// <summary>
    /// RadioOff icon used in Radio.
    /// </summary>
    public Icon? RadioOff { get; set; }

    /// <summary>
    /// Edit icon used in Stepper.
    /// </summary>
    public Icon? Edit { get; set; }

    /// <summary>
    /// RatingEmpty icon used in Rating.
    /// </summary>
    public Icon? RatingEmpty { get; set; }

    /// <summary>
    /// RatingFull icon used in Rating.
    /// </summary>
    public Icon? RatingFull { get; set; }

    /// <summary>
    /// RatingHalf icon used in Rating.
    /// </summary>
    public Icon? RatingHalf { get; set; }

    /// <summary>
    /// Loading icon used in Treeview.
    /// </summary>
    public Icon? Loading { get; set; }

    /// <summary>
    /// First icon used in Pagination.
    /// </summary>
    public Icon? First { get; set; }

    /// <summary>
    /// Last icon used in Pagination.
    /// </summary>
    public Icon? Last { get; set; }

    /// <summary>
    /// Unfold icon used in ColorPicker.
    /// </summary>
    public Icon? Unfold { get; set; }

    /// <summary>
    /// File icon used in FileInput.
    /// </summary>
    public Icon? File { get; set; }

    /// <summary>
    /// Plus icon used in DataTable.
    /// </summary>
    public Icon? Plus { get; set; }

    /// <summary>
    /// Minus icon used in SimpleCheckbox, DataTable and Treeview.
    /// </summary>
    public Icon? Minus { get; set; }

    /// <summary>
    /// Increase icon used in TextField(number). 
    /// </summary>
    public Icon? Increase { get; set; }

    /// <summary>
    /// Decrease icon used in TextField(number). 
    /// </summary>
    public Icon? Decrease { get; set; }

    /// <summary>
    /// Copy icon used in CopyableText.
    /// </summary>
    public Icon? Copy { get; set; }

    /// <summary>
    /// GoBack icon used in PageHeader.
    /// </summary>
    public Icon? GoBack { get; set; }

    /// <summary>
    /// Search icon used in PageHeader.
    /// </summary>
    public Icon? Search { get; set; }

    /// <summary>
    /// FilterOn icon used in PageHeader.
    /// </summary>
    public Icon? FilterOn { get; set; }

    /// <summary>
    /// FilterOff icon used in PageHeader.
    /// </summary>
    public Icon? FilterOff { get; set; }

    /// <summary>
    ///  Retry icon used in InfiniteScroll.
    /// </summary>
    public Icon? Retry { get; set; }

    /// <summary>
    /// User defined icons.
    /// </summary>
    public Dictionary<string, Icon?> UserDefined { get; set; } = new();

    /// <summary>
    /// Custom the CSS formatter, works for custom icon set only.
    /// </summary>
    public Func<string, string>? Custom { get; set; }

    public IReadOnlyDictionary<string, Icon?> ToDictionary()
    {
        var defaultAliases = this.ToDictionary<Icon>(p => p.PropertyType.IsEquivalentTo(typeof(Icon)));
        return defaultAliases.Concat(UserDefined).ToDictionary(k => k.Key, v => v.Value);
    }

    public Icon? GetIconOrDefault(string alias, Icon? @default = null)
    {
        alias = alias.TrimStart('$');

        var dict = ToDictionary();

        var key = dict.Keys.FirstOrDefault(k => k.Equals(alias, StringComparison.OrdinalIgnoreCase));
        return key is null ? @default : dict[key];
    }
}
