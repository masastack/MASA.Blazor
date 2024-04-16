namespace Masa.Blazor
{
    public partial class MTreeview<TItem, TKey> : MasaComponentBase, ITreeview<TItem, TKey>, IThemeable
        where TKey : notnull
    {
        [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

        [Parameter, EditorRequired] public List<TItem> Items { get; set; } = null!;

        [Parameter, EditorRequired] public Func<TItem, string> ItemText { get; set; } = null!;

        [Parameter, EditorRequired] public Func<TItem, TKey> ItemKey { get; set; } = null!;

        [Parameter, EditorRequired] public Func<TItem, List<TItem>> ItemChildren { get; set; } = null!;

        [Parameter] public RenderFragment? ChildContent { get; set; }

        [Parameter] public string? Search { get; set; }

        [Parameter] public RenderFragment<TreeviewItem<TItem>>? PrependContent { get; set; }

        [Parameter] public RenderFragment<TreeviewItem<TItem>>? LabelContent { get; set; }

        [Parameter] public RenderFragment<TreeviewItem<TItem>>? AppendContent { get; set; }

        [Parameter] public Func<TItem, bool>? ItemDisabled { get; set; }

        [Parameter] public bool Selectable { get; set; }

        [Parameter] public bool OpenAll { get; set; }

        [Parameter] public Func<TItem, string?, Func<TItem, string>, bool>? Filter { get; set; }

        [Parameter] public SelectionType SelectionType { get; set; }

        [Parameter] public List<TKey>? Value { get; set; }

        [Parameter] public EventCallback<List<TKey>> ValueChanged { get; set; }

        [Parameter] public bool MultipleActive { get; set; }

        [Parameter] public bool MandatoryActive { get; set; }

        [Parameter] public List<TKey>? Active { get; set; }

        [Parameter] public EventCallback<List<TKey>> ActiveChanged { get; set; }

        [Parameter] public List<TKey>? Open { get; set; }

        [Parameter] public EventCallback<List<TKey>> OpenChanged { get; set; }

        [Parameter, MasaApiParameter("$loading")]
        public string LoadingIcon { get; set; } = "$loading";

        [Parameter, MasaApiParameter("$subgroup")]
        public string ExpandIcon { get; set; } = "$subgroup";

        [Parameter]
        [Obsolete("Use OnSelectUpdate instead.")]
        public EventCallback<List<TItem>> OnInput { get; set; }

        [Parameter] public EventCallback<List<TItem>> OnSelectUpdate { get; set; }

        [Parameter] public EventCallback<List<TItem>> OnActiveUpdate { get; set; }

        [Parameter] public EventCallback<List<TItem>> OnOpenUpdate { get; set; }

        [Parameter] public bool Dark { get; set; }

        [Parameter] public bool Light { get; set; }

        [Parameter] public bool Hoverable { get; set; }

        [Parameter] public bool Dense { get; set; }

        [Parameter] public bool Rounded { get; set; }

        [Parameter] public bool Shaped { get; set; }

        [Parameter] public string? ActiveClass { get; set; }

        [Parameter]
        [MasaApiParameter("accent")]
        public string? SelectedColor { get; set; } = "accent";

        [Parameter]
        [MasaApiParameter("primary")]
        public string? Color { get; set; } = "primary";

        [Parameter] public EventCallback<TItem> LoadChildren { get; set; }

        [Parameter] public bool Activatable { get; set; }

        [Parameter]
        [MasaApiParameter("$checkboxIndeterminate")]
        public string? IndeterminateIcon { get; set; } = "$checkboxIndeterminate";

        [Parameter]
        [MasaApiParameter("$checkboxOn")]
        public string? OnIcon { get; set; } = "$checkboxOn";

        [Parameter]
        [MasaApiParameter("$checkboxOff")]
        public string? OffIcon { get; set; } = "$checkboxOff";

        [Parameter] public bool OpenOnClick { get; set; }

        [CascadingParameter(Name = "IsDark")] public bool CascadingIsDark { get; set; }

        private List<TItem>? _oldItems;
        private string? _oldItemsKeys;
        private List<TKey> _oldValue = new();
        private List<TKey> _oldActive = new();
        private List<TKey> _oldOpen = new();
        private string? _prevSearch;
        private CancellationTokenSource? _searchUpdateCts;

        public bool IsDark
        {
            get
            {
                if (Dark)
                {
                    return true;
                }

                if (Light)
                {
                    return false;
                }

                return CascadingIsDark;
            }
        }

        public List<TItem> ComputedItems
        {
            get
            {
                if (string.IsNullOrEmpty(Search))
                {
                    return Items;
                }

                return Items.Where(r => !IsExcluded(ItemKey.Invoke(r))).ToList();
            }
        }

        public Dictionary<TKey, NodeState<TItem, TKey>> Nodes { get; private set; } = new();

        public List<TKey> ExcludedItems
        {
            get
            {
                var excluded = new List<TKey>();

                if (string.IsNullOrEmpty(Search))
                {
                    return excluded;
                }

                foreach (var item in Items)
                {
                    FilterTreeItems(item, ref excluded);
                }

                return excluded;
            }
        }

        private bool IndependentTheme =>
            (IsDirtyParameter(nameof(Dark)) && Dark) || (IsDirtyParameter(nameof(Light)) && Light);

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters);

            Items.ThrowIfNull(ComponentName);
            ItemText.ThrowIfNull(ComponentName);
            ItemKey.ThrowIfNull(ComponentName);
            ItemChildren.ThrowIfNull(ComponentName);
        }

#if NET8_0_OR_GREATER
        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (MasaBlazor.IsSsr && !IndependentTheme)
            {
                CascadingIsDark = MasaBlazor.Theme.Dark;
            }
        }
#endif

        private Block _block = new("m-treeview");

        protected override IEnumerable<string> BuildComponentClass()
        {
            return _block
                .Modifier(Hoverable)
                .And(Dense)
                .AddTheme(IsDark, IndependentTheme)
                .GenerateCssClasses();
        }

        public void AddNode(ITreeviewNode<TItem, TKey> node)
        {
            if (Nodes.TryGetValue(node.Key, out var nodeState))
            {
                nodeState.Node = node;
            }
        }

        public bool IsActive(TKey key)
        {
            if (Nodes.TryGetValue(key, out var nodeState))
            {
                return nodeState.IsActive;
            }

            return false;
        }

        public bool IsIndeterminate(TKey key)
        {
            if (Nodes.TryGetValue(key, out var nodeState))
            {
                return nodeState.IsIndeterminate;
            }

            return false;
        }

        public bool IsOpen(TKey key)
        {
            if (Nodes.TryGetValue(key, out var nodeState))
            {
                return nodeState.IsOpen;
            }

            return false;
        }

        public bool IsSelected(TKey key)
        {
            if (Nodes.TryGetValue(key, out var nodeState))
            {
                return nodeState.IsSelected;
            }

            return false;
        }

        public void UpdateActive(TKey key, bool isActive)
        {
            if (!Nodes.TryGetValue(key, out var nodeState)) return;

            if (MandatoryActive && !isActive)
            {
                return;
            }

            nodeState.IsActive = isActive;

            if (MultipleActive) return;

            foreach (var ns in Nodes.Values.Where(r => r != nodeState && r.IsActive))
            {
                ns.IsActive = false;
            }
        }

        public async Task EmitActiveAsync()
        {
            var active = Nodes.Values.Where(r => r.IsActive).Select(r => r.Item!).ToList();

            if (OnActiveUpdate.HasDelegate)
            {
                _ = OnActiveUpdate.InvokeAsync(active);
            }

            if (ActiveChanged.HasDelegate)
            {
                await ActiveChanged.InvokeAsync(active.Select(ItemKey).ToList());
            }

            if (!ActiveChanged.HasDelegate && !OnActiveUpdate.HasDelegate)
            {
                StateHasChanged();
            }
        }

        public void UpdateOpen(TKey key)
        {
            if (Nodes.TryGetValue(key, out var nodeState))
            {
                nodeState.IsOpen = !nodeState.IsOpen;
            }
        }

        public async Task EmitOpenAsync()
        {
            var open = Nodes.Values.Where(r => r.IsOpen).Select(r => r.Item!).ToList();

            if (OnOpenUpdate.HasDelegate)
            {
                _ = OnOpenUpdate.InvokeAsync(open);
            }

            if (OpenChanged.HasDelegate)
            {
                await OpenChanged.InvokeAsync(open.Select(ItemKey).ToList());
            }

            if (!OpenChanged.HasDelegate && !OnOpenUpdate.HasDelegate)
            {
                StateHasChanged();
            }
        }

        public void UpdateSelected(TKey key, bool isSelected)
            => UpdateSelected(key, isSelected, false);

        private void UpdateSelectedByValue(TKey key, bool isSelected)
            => UpdateSelected(key, isSelected, true);

        private void UpdateSelected(TKey key, bool isSelected, bool updateByValue)
        {
            if (!Nodes.TryGetValue(key, out var nodeState)) return;

            nodeState.IsSelected = isSelected;
            nodeState.IsIndeterminate = false;

            if (updateByValue && SelectionType == SelectionType.LeafButIndependentParent)
            {
                UpdateParentSelected(nodeState);
            }
            else if (SelectionType is SelectionType.Leaf or SelectionType.LeafButIndependentParent)
            {
                UpdateChildrenSelected(nodeState.Children, nodeState.IsSelected);
                UpdateParentSelected(nodeState);
            }
        }

        public async Task EmitSelectedAsync()
        {
            var selected = Nodes.Values.Where(r =>
            {
                if (SelectionType is SelectionType.Independent or SelectionType.LeafButIndependentParent)
                {
                    return r.IsSelected;
                }

                return r.IsSelected && !r.Children.Any();
            }).Select(r => r.Item).ToList();

            var onSelectUpdate = OnSelectUpdate.HasDelegate ? OnSelectUpdate : OnInput;
            if (onSelectUpdate.HasDelegate)
            {
                _ = onSelectUpdate.InvokeAsync(selected);
            }

            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(selected.Select(ItemKey).ToList());
            }

            if (!ValueChanged.HasDelegate && !onSelectUpdate.HasDelegate)
            {
                StateHasChanged();
            }
        }

        /// <summary>
        /// Rebuilds the tree with current Items.
        /// </summary>
        public void RebuildTree()
        {
            BuildTree(Items, default);
        }

        private NodeState<TItem, TKey>[] GetParents(NodeState<TItem, TKey> node)
        {
            var parents = new List<NodeState<TItem, TKey>>();
            var parent = node.Parent;
            var parentKeys = new List<TKey>();
            while (parent != null && !parentKeys.Contains(parent))
            {
                parentKeys.Add(parent);
                if (Nodes.TryGetValue(parent, out var parentNode))
                {
                    parents.Add(parentNode);
                    parent = parentNode.Parent;
                }
                else
                {
                    break;
                }
            }

            return parents.ToArray();
        }

        private void UpdateParentSelected(NodeState<TItem, TKey> node, bool isIndeterminate = false)
        {
            var parents = GetParents(node);
            foreach (var nodeState in parents)
            {
                if (SelectionType == SelectionType.LeafButIndependentParent)
                {
                    nodeState.IsSelected = true;
                    nodeState.IsIndeterminate = false;
                }
                else if (isIndeterminate)
                {
                    nodeState.IsSelected = false;
                    nodeState.IsIndeterminate = true;
                }
                else
                {
                    var children = Nodes
                        .Where(r => nodeState.Children.Contains(r.Key))
                        .Select(r => r.Value)
                        .ToList();

                    if (children.All(r => r.IsSelected))
                    {
                        nodeState.IsSelected = true;
                        nodeState.IsIndeterminate = false;
                    }
                    else if (children.All(r => !r.IsSelected))
                    {
                        nodeState.IsSelected = false;
                        nodeState.IsIndeterminate = false;
                    }
                    else
                    {
                        nodeState.IsSelected = false;
                        nodeState.IsIndeterminate = true;
                    }
                }
            }
        }

        private void UpdateChildrenSelected(IEnumerable<TKey>? children, bool isSelected)
        {
            if (children == null) return;

            foreach (var child in children)
            {
                if (Nodes.TryGetValue(child, out var nodeState))
                {
                    //control by parent
                    nodeState.IsSelected = isSelected;
                    UpdateChildrenSelected(nodeState.Children, isSelected);
                }
            }
        }

        private bool FilterTreeItems(TItem item, ref List<TKey> excluded)
        {
            if (FilterTreeItem(item, Search, ItemText))
            {
                if (Nodes.TryGetValue(ItemKey(item), out var nodeState) && nodeState.Parent != null)
                {
                    UpdateOpen(nodeState.Parent, true);
                }

                return true;
            }

            var children = ItemChildren(item);

            if (children != null)
            {
                var match = false;

                foreach (var child in children)
                {
                    if (FilterTreeItems(child, ref excluded))
                    {
                        if (Nodes.TryGetValue(ItemKey(child), out var nodeState) && nodeState.Parent != null)
                        {
                            UpdateOpen(nodeState.Parent, true);
                        }

                        match = true;
                    }
                }

                if (match)
                {
                    return true;
                }
            }

            excluded.Add(ItemKey(item));
            return false;
        }

        private bool FilterTreeItem(TItem item, string? search, Func<TItem, string> itemText)
        {
            if (Filter is not null)
            {
                return Filter.Invoke(item, search, itemText);
            }

            if (string.IsNullOrEmpty(search))
            {
                return true;
            }

            var text = itemText(item);
            return text.ToLower().IndexOf(search.ToLower(), StringComparison.InvariantCulture) > -1;
        }

        public bool IsExcluded(TKey key)
        {
            return !string.IsNullOrEmpty(Search) && ExcludedItems.Contains(key);
        }

        private string CombineItemKeys(IList<TItem> list)
        {
            string keys = string.Empty;

            if (list == null || list.Count == 0)
            {
                return keys;
            }

            foreach (var item in list)
            {
                var key = ItemKey(item);
                keys += key.ToString();

                var children = ItemChildren(item);
                if (children is not null)
                {
                    keys += CombineItemKeys(children);
                }
            }

            return keys;
        }

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();

            if (_oldItems != Items)
            {
                _oldItems = Items;
                _oldItemsKeys = CombineItemKeys(Items);

                BuildTree(Items, default);
            }
            else
            {
                var itemKeys = CombineItemKeys(Items);
                if (_oldItemsKeys != itemKeys)
                {
                    _oldItemsKeys = itemKeys;

                    BuildTree(Items, default);
                }
            }

            var value = Value ?? new List<TKey>();
            if (!ListComparer.Equals(_oldValue, value))
            {
                await HandleUpdate(_oldValue, Value, UpdateSelectedByValue, EmitSelectedAsync);
                _oldValue = value;
            }

            var active = Active ?? new List<TKey>();
            if (!ListComparer.Equals(_oldActive, active))
            {
                await HandleUpdate(_oldActive, Active, UpdateActive, EmitActiveAsync);
                _oldActive = active;
            }

            var open = Open ?? new List<TKey>();
            if (!ListComparer.Equals(_oldOpen, open))
            {
                await HandleUpdate(_oldOpen, Open, UpdateOpen, EmitOpenAsync);
                _oldOpen = open;
            }

            if (_prevSearch != Search)
            {
                _prevSearch = Search;

                _searchUpdateCts?.Cancel();
                _searchUpdateCts = new();
                await RunTaskInMicrosecondsAsync(EmitOpenAsync, 300, _searchUpdateCts.Token);
            }
        }

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {
                if (OpenAll)
                {
                    UpdateAll(true);
                }

                StateHasChanged();
            }
        }

        public void UpdateAll(bool val)
        {
            Nodes.Values.ForEach(nodeState => { nodeState.IsOpen = val; });
        }

        private void UpdateOpen(TKey key, bool isOpen)
        {
            if (!Nodes.TryGetValue(key, out var nodeState)) return;

            nodeState.IsOpen = isOpen;
        }

        private async Task HandleUpdate(List<TKey> old, List<TKey>? value, Action<TKey, bool> updateFn,
            Func<Task> emitFn)
        {
            if (value == null) return;

            old.ForEach(k => updateFn(k, false));
            value.ForEach(k => updateFn(k, true));

            await emitFn.Invoke();
        }

        private void BuildTree(List<TItem> items, TKey? parent)
        {
            foreach (var item in items)
            {
                var key = ItemKey(item);
                var children = ItemChildren(item) ?? new List<TItem>();

                Nodes.TryGetValue(key, out var oldNode);

                var newNode = new NodeState<TItem, TKey>(item, children.Select(ItemKey), parent);

                BuildTree(children, key);

                if (SelectionType == SelectionType.Leaf
                    && parent != null
                    && !Nodes.ContainsKey(key)
                    && Nodes.TryGetValue(parent, out var node))
                {
                    newNode.IsSelected = node.IsSelected;
                }
                else if (oldNode is not null)
                {
                    newNode.IsSelected = oldNode.IsSelected;
                    newNode.IsIndeterminate = oldNode.IsIndeterminate;
                }

                if (oldNode is not null)
                {
                    newNode.Node = oldNode.Node;
                    newNode.IsActive = oldNode.IsActive;
                    newNode.IsOpen = oldNode.IsOpen;
                }

                Nodes[key] = newNode;

                // TODO: there is still some logic in Vuetify but no implementation here, it's necessarily?

                if (newNode.IsSelected && (SelectionType != SelectionType.Leaf || !newNode.Children.Any()))
                {
                    if (Value == null)
                    {
                        Value = new List<TKey> { key };
                    }
                    else
                    {
                        Value.Add(key);
                    }
                }

                if (newNode.IsActive)
                {
                    if (Active == null)
                    {
                        Active = new List<TKey> { key };
                    }
                    else
                    {
                        Active.Add(key);
                    }
                }

                if (newNode.IsOpen)
                {
                    if (Open == null)
                    {
                        Open = new List<TKey> { key };
                    }
                    else
                    {
                        Open.Add(key);
                    }
                }
            }
        }
    }
}