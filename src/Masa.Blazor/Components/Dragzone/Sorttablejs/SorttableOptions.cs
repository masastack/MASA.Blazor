namespace Masa.Blazor
{
    /// <summary>
    /// detail to http://www.sortablejs.com/options.html
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
        public bool Sort { get; set; } = true;
        public int Delay { get; set; }
        public int TouchStartThreshold { get; set; }
        public bool Disabled { get; set; }
        public int Animation { get; set; } = 100;
        public string Handle { get; set; }
        public string Filter { get; set; } = "ignore-elements";
        public bool PreventOnFilter { get; set; } = true;
        public string Draggable { get; set; }
        public string GhostClass { get; set; } = "sortable-ghost";
        public string ChosenClass { get; set; } = "sortable-chosen";
        public string DragClass { get; set; } = "sortable-drag";
        public string DataIdAttr { get; set; } = "id";
        public int SwapThreshold { get; set; } = 1;
        public bool InvertSwap { get; set; }
        public int InvertedSwapThreshold { get; set; } = 1;
        public DirectionTypes? Direction { get; set; }
        public bool ForceFallback { get; set; }
        public string FallbackClass { get; set; } = "sortable-fallback";
        public bool FallbackOnBody { get; set; }
        public int FallbackTolerance { get; set; }
        public bool Scroll { get; set; }
        public int ScrollSensitivity { get; set; }
        public int ScrollSpeed { get; set; }
        public bool BubbleScroll { get; set; }
        public bool DragoverBubble { get; set; }
        public bool RemoveCloneOnHide { get; set; }
        public int EmptyInsertThreshold { get; set; }

        public object ToParameters()
        {
            var dic = new Dictionary<string, object>();
            SetGroup(dic);
            dic.Add("sort", Sort);
            dic.Add("delay", Delay);
            dic.Add("touchStartThreshold", TouchStartThreshold);
            dic.Add("disabled", Disabled);
            dic.Add("animation", Animation);
            if (!string.IsNullOrEmpty(Handle))
                dic.Add("handle", Handle);
            dic.Add("filter", Filter);
            dic.Add("preventOnFilter", PreventOnFilter);

            if (!string.IsNullOrEmpty(Draggable))
                dic.Add("draggable", Draggable);
            dic.Add("ghostClass", GhostClass);
            dic.Add("chosenClass", ChosenClass);
            dic.Add("dragClass", DragClass);
            dic.Add("dataIdAttr", DataIdAttr);

            dic.Add("swapThreshold", SwapThreshold);
            dic.Add("invertSwap", InvertSwap);
            dic.Add("invertedSwapThreshold", InvertedSwapThreshold);

            if (Direction.HasValue && Direction - DirectionTypes.Default > 0)
                dic.Add("direction", Direction.Value.ToString("G").ToLower());

            dic.Add("forceFallback", ForceFallback);
            dic.Add("fallbackClass", FallbackClass);
            dic.Add("fallbackOnBody", FallbackOnBody);
            dic.Add("fallbackTolerance", FallbackTolerance);
            dic.Add("scroll", Scroll);

            dic.Add("scrollSensitivity", ScrollSensitivity);
            dic.Add("scrollSpeed", ScrollSpeed);
            dic.Add("bubbleScroll", BubbleScroll);
            dic.Add("dragoverBubble", DragoverBubble);
            dic.Add("removeCloneOnHide", RemoveCloneOnHide);
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