namespace Masa.Blazor
{
    public partial class MDragzone<Item> : BDragzone<Item>, IDisposable
    {
        private DotNetObjectReference<MDragzone<Item>> _dotNetHelper;
        private IJSObjectReference _jsHelper;

        [Parameter]
        public SorttableOptions Options { get; set; }

        [Parameter]
        public Action<SorttableOptions> SetOptions { get; set; }

        /// <summary>
        /// 元素被选中
        /// </summary>
        /// <param name="args"></param>
        [JSInvokable]
        public void OnChoose(dynamic args)
        {
            if (Options?.OnChoose != null)
                Options.OnChoose(args);
        }

        /// <summary>
        /// 元素未被选中的时候（从选中到未选中）
        /// </summary>
        /// <param name="args"></param>
        [JSInvokable]
        public void OnUnchoose(dynamic args)
        {
            if (Options?.OnUnchoose != null)
                Options.OnUnchoose(args);
        }

        /// <summary>
        /// 开始拖拽的时候
        /// </summary>
        /// <param name="args"></param>
        [JSInvokable]
        public void OnStart(SorttableEventArgs args)
        {
            DragDropService.DragItem = Items[args.OldIndex];
            if (Options?.OnStart != null)
                Options.OnStart(args);
        }

        /// <summary>        
        ///  结束拖拽
        /// </summary>
        /// <param name="args"></param>
        [JSInvokable]
        public void OnDropEnd(SorttableEventArgs args)
        {
            if (Options?.OnEnd != null)
                Options.OnEnd(args);
            DragDropService.Reset();
        }

        /// <summary>
        /// 元素从一个列表拖拽到另一个列表
        /// </summary>
        /// <param name="args"></param>
        [JSInvokable]
        public void OnAdd(SorttableEventArgs args)
        {
            if (Options?.OnAdd != null)
                Options.OnAdd(args);

            if (!Items.Contains(DragDropService.DragItem))
                Items.Insert(args.NewIndex, DragDropService.DragItem);
        }

        /// <summary>
        /// 列表内元素顺序更新的时候触发
        /// </summary>
        /// <param name="args"></param>
        [JSInvokable]
        public void OnUpdate(SorttableEventArgs args)
        {
            if (Options?.OnUpdate != null)
                Options.OnUpdate(args);

            Items[args.OldIndex] = Items[args.NewIndex];
            Items[args.NewIndex] = DragDropService.DragItem;
        }

        /// <summary>
        /// 列表的任何更改都会触发
        /// </summary>
        /// <param name="args"></param>
        [JSInvokable]
        public void OnSort(SorttableEventArgs args)
        {
            if (Options?.OnSort != null)
                Options.OnSort(args);
        }

        /// <summary>
        /// 元素从列表中移除进入另一个列表
        /// </summary>
        /// <param name="args"></param>
        [JSInvokable]
        public void OnRemove(SorttableEventArgs args)
        {
            if (Options?.OnRemove != null)
                Options.OnRemove(args);

            var index = Items.IndexOf(DragDropService.DragItem);
            if (args.OldIndex == index)
            {
                Items.RemoveAt(index);
            }
        }

        /// <summary>
        /// moving 
        /// </summary>
        /// <param name="args"></param>
        [JSInvokable]
        public void OnMove(SorttableMoveEventArgs args)
        {
            if (Options?.OnMove != null)
                Options.OnMove(args);
        }

        /// <summary>
        /// clone
        /// </summary>
        /// <param name="args"></param>
        [JSInvokable]
        public void OnClone(SorttableEventArgs args)
        {
            if (Options?.OnClone != null)
                Options.OnClone(args);
        }

        /// <summary>
        /// on changed item position
        /// </summary>
        /// <param name="args"></param>
        [JSInvokable]
        public void OnDrop(SorttableEventArgs args)
        {
            if (Options?.OnChange != null)
                Options.OnChange(args);
        }

        [JSInvokable]
        public bool OnPut()
        {
            if (Options.PutFn != null)
                return Options.PutFn();
            return true;
        }

        [JSInvokable]
        public bool OnPull()
        {
            if (Options.PullFn != null)
                return Options.PullFn();
            return true; ;
        }

        protected override void OnInitialized()
        {
            Options = new SorttableOptions()
            {
                ChosenClass = "dragItem-active"
            };
            base.OnInitialized();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            _dotNetHelper = DotNetObjectReference.Create(this);
            _jsHelper = await Js.InvokeAsync<IJSObjectReference>("import", "./_content/Masa.Blazor/js/draggable/sorttable-helper.js");
            await _jsHelper.InvokeVoidAsync("init", _dotNetHelper, Id, Options.ToParameters());
            await base.OnAfterRenderAsync(firstRender);
        }

        protected override void OnParametersSet()
        {
            if (SetOptions != null)
                SetOptions(Options);
            base.OnParametersSet();
        }

        protected override Task OnParametersSetAsync()
        {
            if (SetOptions != null)
                SetOptions(Options);
            return base.OnParametersSetAsync();
        }

        public override void Dispose()
        {
            _dotNetHelper?.Dispose();
        }
    }
}