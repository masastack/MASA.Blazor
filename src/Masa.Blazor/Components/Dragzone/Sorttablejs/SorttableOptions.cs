namespace Masa.Blazor
{
    /// <summary>
    /// detail to https://github.com/SortableJS/Sortable
    /// </summary>
    public class SorttableOptions
    {
        public Func<bool> PullFn { get; set; }
        public Func<bool> PutFn { get; set; }
        public Action<SorttableEventArgs> OnStart;
        public Action<SorttableEventArgs> OnEnd;
        public Action<SorttableEventArgs> OnAdd;
        public Action<SorttableEventArgs> OnUpdate;
        public Action<SorttableEventArgs> OnSort;
        public Action<SorttableEventArgs> OnRemove;
        public Action<SorttableEventArgs> OnClone;
        public Action<SorttableEventArgs> OnChange;
        public Action<SorttableEventArgs> OnChoose;
        public Action<SorttableEventArgs> OnUnchoose;
        public Action<SorttableMoveEventArgs> OnMove;

        public string Pull { get; set; }
        public string Put { get; set; }
        public string Group { get; set; }
        public bool? Sort { get; set; }
        public int? Delay { get; set; }
        public int? TouchStartThreshold { get; set; }
        public bool? Disabled { get; set; }
        public string Easing { get; set; }
        public int? Animation { get; set; } = 150;
        public string Handle { get; set; }
        public string Filter { get; set; }
        public bool? PreventOnFilter { get; set; }
        public string Draggable { get; set; }
        public string GhostClass { get; set; }
        public string ChosenClass { get; set; }
        public string DragClass { get; set; }
        public string DataIdAttr { get; set; } = "id";
        public float? SwapThreshold { get; set; }
        public bool? InvertSwap { get; set; }
        public int? InvertedSwapThreshold { get; set; }
        public DirectionTypes? Direction { get; set; }
        public bool? ForceFallback { get; set; }
        public string FallbackClass { get; set; }
        public bool? FallbackOnBody { get; set; }
        public int? FallbackTolerance { get; set; }
        public bool? Scroll { get; set; }
        public int? ScrollSensitivity { get; set; }
        public int? ScrollSpeed { get; set; }
        public bool? BubbleScroll { get; set; }
        public bool? DragoverBubble { get; set; }
        public bool? RemoveCloneOnHide { get; set; }
        public int? EmptyInsertThreshold { get; set; }

        public object ToParameters()
        {
            var dic = new Dictionary<string, object>();
            SetGroup(dic);
            if (Sort.HasValue)
                dic.Add("sort", Sort);
            if (Delay.HasValue)
                dic.Add("delay", Delay);
            if (TouchStartThreshold.HasValue)
                dic.Add("touchStartThreshold", TouchStartThreshold);
            if (Disabled.HasValue)
                dic.Add("disabled", Disabled);
            if (Animation.HasValue)
                dic.Add("animation", Animation);
            if (!string.IsNullOrEmpty(Easing))
                dic.Add("easing", Easing);
            if (!string.IsNullOrEmpty(Handle))
                dic.Add("handle", Handle);
            if (!string.IsNullOrEmpty(Filter))
                dic.Add("filter", Filter);
            if (PreventOnFilter.HasValue)
                dic.Add("preventOnFilter", PreventOnFilter);

            if (!string.IsNullOrEmpty(Draggable))
                dic.Add("draggable", Draggable);
            if (!string.IsNullOrEmpty(GhostClass))
                dic.Add("ghostClass", GhostClass);
            if (!string.IsNullOrEmpty(ChosenClass))
                dic.Add("chosenClass", ChosenClass);
            if (!string.IsNullOrEmpty(DragClass))
                dic.Add("dragClass", DragClass);
            if (!string.IsNullOrEmpty(DataIdAttr))
                dic.Add("dataIdAttr", DataIdAttr);
            if (SwapThreshold.HasValue)
                dic.Add("swapThreshold", SwapThreshold);
            if (InvertSwap.HasValue)
                dic.Add("invertSwap", InvertSwap);
            if (InvertedSwapThreshold.HasValue)
                dic.Add("invertedSwapThreshold", InvertedSwapThreshold);

            if (Direction.HasValue && Direction - DirectionTypes.Default > 0)
                dic.Add("direction", Direction.Value.ToString("G").ToLower());
            if (ForceFallback.HasValue)
                dic.Add("forceFallback", ForceFallback);
            if (!string.IsNullOrEmpty(FallbackClass))
                dic.Add("fallbackClass", FallbackClass);
            if (FallbackOnBody.HasValue)
                dic.Add("fallbackOnBody", FallbackOnBody);
            if (FallbackTolerance.HasValue)
                dic.Add("fallbackTolerance", FallbackTolerance);
            if (Scroll.HasValue)
                dic.Add("scroll", Scroll);

            if (ScrollSensitivity.HasValue)
                dic.Add("scrollSensitivity", ScrollSensitivity);
            if (ScrollSpeed.HasValue)
                dic.Add("scrollSpeed", ScrollSpeed);
            if (BubbleScroll.HasValue)
                dic.Add("bubbleScroll", BubbleScroll);
            if (DragoverBubble.HasValue)
                dic.Add("dragoverBubble", DragoverBubble);
            if (RemoveCloneOnHide.HasValue)
                dic.Add("removeCloneOnHide", RemoveCloneOnHide);
            if (EmptyInsertThreshold.HasValue)
                dic.Add("emptyInsertThreshold", EmptyInsertThreshold);
            return dic;
        }

        private void SetGroup(Dictionary<string, object> dic)
        {
            if (string.IsNullOrEmpty(Group))
                Group = nameof(Group).ToLower();

            if (Pull == null && Put == null && PullFn == null && PutFn == null)
            {
                if (!string.IsNullOrEmpty(Group))
                    dic.Add("group", Group);
                return;
            }

            var items = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(Group))
                items.Add("name", Group);

            if (Pull != null)
            {
                if (bool.TryParse(Pull, out bool result))
                {
                    items.Add("pull", result);
                }
                else if (Pull == "clone")
                {
                    items.Add("pull", Pull);
                }
            }
            else if (PullFn != null)
            {
                items.Add("pull", "fun");
            }

            if (!string.IsNullOrEmpty(Put))
            {
                if (bool.TryParse(Put, out bool result))
                    items.Add("put", result);
                else if (Put.Contains(','))
                {
                    items.Add("put", Put.Split(','));
                }
                else
                {
                    items.Add("put", new string[] { Put });
                }
            }
            else if (PutFn != null)
            {
                items.Add("put", "fun");
            }

            dic.Add("group", items);
        }
    }
}