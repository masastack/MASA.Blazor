namespace Masa.Blazor
{
    public partial class MDragZone : BDragZone, IDisposable, IAsyncDisposable
    {
        [Parameter]
        public Action<SorttableOptions> Options { get; set; }

        [Parameter]
        public bool Disabled
        {
            get { return _options.Disabled ?? false; }
            set { _options.Disabled = value; }
        }

        [Parameter]
        public string Group
        {
            get
            {
                return _options.Group;
            }
            set
            {
                _options.Group = value;
            }
        }

        [Parameter]
        public string Pull
        {
            get { return _options.Pull; }
            set { _options.Pull = value; }
        }

        [Parameter]
        public string Put
        {
            get { return _options.Put; }
            set { _options.Put = value; }
        }

        [Parameter]
        public bool Sort
        {
            get { return _options.Sort ?? true; }
            set { _options.Sort = value; }
        }

        private DotNetObjectReference<MDragZone> _dotNetHelper;
        private IJSObjectReference _jsHelper;
        private SorttableOptions _options = new();

        /// <summary>
        /// 元素被选中
        /// </summary>
        /// <param name="args"></param>
        [JSInvokable]
        public void OnChoose(dynamic args)
        {
            _isRender = false;
            if (_options?.OnChoose != null)
                _options.OnChoose(args);
        }

        /// <summary>
        /// 元素未被选中的时候（从选中到未选中）
        /// </summary>
        /// <param name="args"></param>
        [JSInvokable]
        public void OnUnchoose(dynamic args)
        {
            _isRender = false;
            if (_options?.OnUnchoose != null)
                _options.OnUnchoose(args);
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
            if (DragDropService.DragItem.Value != args.OldIndex)
            { 
            
            }
                if (_options?.OnStart != null)
                    _options.OnStart(args);
        }

        /// <summary>        
        ///  结束拖拽
        /// </summary>
        /// <param name="args"></param>
        [JSInvokable]
        public void OnDropEnd(SorttableEventArgs args)
        {
            _isRender = false;
            if (_options?.OnEnd != null)
                _options.OnEnd(args);

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
            if (_options?.OnAdd != null)
                _options.OnAdd(args);

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
            if (_options?.OnUpdate != null)
                _options.OnUpdate(args);

            //Update(DragDropService.DragItem, args.OldIndex, args.NewIndex);
        }

        /// <summary>
        /// 列表的任何更改都会触发
        /// </summary>
        /// <param name="args"></param>
        [JSInvokable]
        public void OnSort(SorttableEventArgs args)
        {
            _isRender = true;
            if (_options?.OnSort != null)
                _options.OnSort(args);

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
            if (_options?.OnRemove != null)
                _options.OnRemove(args);

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
            if (_options?.OnMove != null)
                _options.OnMove(args);
        }

        /// <summary>
        /// clone
        /// </summary>
        /// <param name="args"></param>
        [JSInvokable]
        public void OnClone(SorttableEventArgs args)
        {
            _isRender = false;
            if (_options?.OnClone != null)
                _options.OnClone(args);
        }

        [JSInvokable]
        public void OnChange(SorttableEventArgs args)
        {
            _isRender = false;
            if (_options?.OnChange != null)
                _options.OnChange(args);
        }

        [JSInvokable]
        public bool OnPut()
        {
            if (_options.PutFn != null)
                return _options.PutFn();
            return true;
        }

        [JSInvokable]
        public bool OnPull()
        {
            if (_options.PullFn != null)
                return _options.PullFn();
            return true;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            _jsHelper = await Js.InvokeAsync<IJSObjectReference>("import", "./_content/Masa.Blazor/js/Dragzone/sorttable-helper.js");
            if (firstRender)
            {
                _dotNetHelper = DotNetObjectReference.Create(this);
                await _jsHelper.InvokeVoidAsync("init", _dotNetHelper, Id, _options.ToParameters());
            }           
            await base.OnAfterRenderAsync(firstRender);           
        }

        protected override bool ShouldRender()
        {
            return base.ShouldRender();
        }

        protected override void OnParametersSet()
        {
            Options?.Invoke(_options);
            base.OnParametersSet();
        }

        public override void Dispose()
        {
            _dotNetHelper?.Dispose();
        }

        public async ValueTask DisposeAsync()
        {
            try
            {
                if (_jsHelper != null)
                    await _jsHelper.DisposeAsync();
            }
            catch
            {

            }
        }
    }
}