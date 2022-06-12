namespace Masa.Blazor
{
    public partial class MDragZone : BDragZone, IDisposable, IAsyncDisposable
    {
        private DotNetObjectReference<MDragZone> _dotNetHelper;
        private IJSObjectReference _jsHelper;

        [Parameter]
        public SorttableOptions Options { get; set; }

        [Parameter]
        public Action<SorttableOptions> OnConfigure { get; set; }        

        /// <summary>
        /// 元素被选中
        /// </summary>
        /// <param name="args"></param>
        [JSInvokable]
        public void OnChoose(dynamic args)
        {
            _isRender = false;
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
            _isRender = false;
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
            _isRender = false;
            DragDropService.DragItem = Value.FirstOrDefault(it => it.Id == args.ItemId);
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
            _isRender = false;
            if (Options?.OnEnd != null)
                Options.OnEnd(args);

            DragDropService.Reset();
        }

        /// <summary>
        /// 元素从一个列表拖拽到另一个列表
        /// </summary>
        /// <param name="args"></param>
        [JSInvokable]
        public string OnAdd(SorttableEventArgs args)
        {
            _isRender = true;
            if (Options?.OnAdd != null)
                Options.OnAdd(args);

            string cloneId = string.Empty;
            if (!Contains(DragDropService.DragItem))
            {
                var item = DragDropService.DragItem.Clone();
                item.Id = Guid.NewGuid().ToString();
                Add(item, args.NewIndex);
            }
            return cloneId;
        }

        /// <summary>
        /// 列表内元素顺序更新的时候触发
        /// </summary>
        /// <param name="args"></param>
        [JSInvokable]
        public void OnUpdate(SorttableEventArgs args)
        {
            _isRender = true;
            if (Options?.OnUpdate != null)
                Options.OnUpdate(args);

            Update(DragDropService.DragItem, args.OldIndex, args.NewIndex);
        }

        /// <summary>
        /// 列表的任何更改都会触发
        /// </summary>
        /// <param name="args"></param>
        [JSInvokable]
        public void OnSort(SorttableEventArgs args)
        {
            _isRender = true;
            if (Options?.OnSort != null)
                Options.OnSort(args);

            if (args.NewParentId == Id)
                Update(DragDropService.DragItem, args.OldIndex, args.NewIndex);
        }

        /// <summary>
        /// 元素从列表中移除进入另一个列表
        /// </summary>
        /// <param name="args"></param>
        [JSInvokable]
        public void OnRemove(SorttableEventArgs args)
        {
            _isRender = true;
            if (Options?.OnRemove != null)
                Options.OnRemove(args);

            if (Contains(DragDropService.DragItem))
            {
                Remove(DragDropService.DragItem);
                if (args.IsClone)
                {
                    var item = DragDropService.DragItem.Clone();
                    item.Id = Guid.NewGuid().ToString();
                    Add(item, args.OldIndex);
                }
            }
        }

        /// <summary>
        /// moving 
        /// </summary>
        /// <param name="args"></param>
        [JSInvokable]
        public void OnMove(SorttableMoveEventArgs args)
        {
            _isRender = false;
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
            _isRender = false;
            if (Options?.OnClone != null)
                Options.OnClone(args);
        }

        [JSInvokable]
        public void OnChange(SorttableEventArgs args)
        {
            _isRender = false;
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
            return true;
        }

        protected override void OnInitialized()
        {
            Options = new();
            base.OnInitialized();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            _jsHelper = await Js.InvokeAsync<IJSObjectReference>("import", "./_content/Masa.Blazor/js/Dragzone/sorttable-helper.js");
            if (firstRender)
            {
                _dotNetHelper = DotNetObjectReference.Create(this);
                await _jsHelper.InvokeVoidAsync("init", _dotNetHelper, Id, Options.ToParameters());
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        protected override bool ShouldRender()
        {
            return base.ShouldRender();
        }

        protected override void OnParametersSet()
        {
            OnConfigure?.Invoke(Options);
            base.OnParametersSet();
        }       

        public override void Dispose()
        {
            _dotNetHelper?.Dispose();
        }

        public async ValueTask DisposeAsync()
        {
            if (_jsHelper != null)
                await _jsHelper.DisposeAsync();
        }
    }
}