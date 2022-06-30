using Microsoft.Extensions.Logging;

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
        /// element is choosing
        /// </summary>
        /// <param name="args"></param>
        [JSInvokable]
        public async Task OnChoose(dynamic args)
        {
            _isRender = false;
            if (_options?.OnChoose != null)
                _options.OnChoose(args);

            await Task.CompletedTask;
        }

        /// <summary>
        /// element state is from choosing change to unchoose
        /// </summary>
        /// <param name="args"></param>
        [JSInvokable]
        public async Task OnUnchoose(dynamic args)
        {
            _isRender = false;
            if (_options?.OnUnchoose != null)
                _options.OnUnchoose(args);

            await Task.CompletedTask;
        }

        /// <summary>
        /// drag start
        /// </summary>
        /// <param name="args"></param>
        [JSInvokable]
        public async Task OnStart(SorttableEventArgs args)
        {
            _isRender = false;
            var find = Value.FirstOrDefault(it => it.Id == args.ItemId);
            if (find == null)
            {
                return;
            }
            DragDropService.DragItem = find;
            if (_options?.OnStart != null)
                _options.OnStart(args);

            await Task.CompletedTask;
        }

        /// <summary>        
        /// drop end
        /// </summary>
        /// <param name="args"></param>
        [JSInvokable]
        public async Task OnDropEnd(SorttableEventArgs args)
        {
            _isRender = false;
            if (_options?.OnEnd != null)
                _options.OnEnd(args);

            DragDropService.Reset();
            await Task.CompletedTask;
        }

        /// <summary>
        /// element from other container add to a new different container
        /// </summary>
        /// <param name="args"></param>
        [JSInvokable]
        public async Task<string> OnAdd(SorttableEventArgs args)
        {
            _isRender = true;
            if (_options?.OnAdd != null)
                _options.OnAdd(args);

            string cloneId = string.Empty;
            //DragDropService.DropZone = this;
            //if (args.IsClone)
            //{
            //    cloneId= Guid.NewGuid().ToString();
            //    DragDropService.CloneId = cloneId;
            //}
            if (!Contains(DragDropService.DragItem))
            {
                var item = DragDropService.DragItem.Clone();
                item.Id = Guid.NewGuid().ToString();
                Add(item, args.NewIndex);
            }
            await Task.CompletedTask;
            return cloneId;
        }

        /// <summary>
        /// element sort is updating
        /// </summary>
        /// <param name="args"></param>
        [JSInvokable]
        public async Task OnUpdate(SorttableEventArgs args)
        {
            _isRender = true;
            if (_options?.OnUpdate != null)
                _options.OnUpdate(args);

            if (args.NewParentId == Id)
                Update(DragDropService.DragItem, args.OldIndex, args.NewIndex);

            await Task.CompletedTask;
        }

        /// <summary>
        /// element sort is updated
        /// </summary>
        /// <param name="args"></param>
        [JSInvokable]
        public async Task OnSort(SorttableEventArgs args)
        {
            _isRender = true;
            if (_options?.OnSort != null)
                _options.OnSort(args);

            await Task.CompletedTask;
        }

        /// <summary>
        /// element is removed from parent container
        /// </summary>
        /// <param name="args"></param>
        [JSInvokable]
        public async Task<string> OnRemove(SorttableEventArgs args)
        {
            _isRender = true;
            if (_options?.OnRemove != null)
                _options.OnRemove(args);

            string cloneId = string.Empty;
            if (DragDropService.DragItem != null && Contains(DragDropService.DragItem))
            {
                Remove(DragDropService.DragItem);
                if (args.IsClone)
                {
                    var item = DragDropService.DragItem.Clone();
                    item.Id = Guid.NewGuid().ToString();
                    Add(item, args.NewIndex);
                }
            }

            await Task.CompletedTask;
            return cloneId;
        }

        /// <summary>
        /// moving 
        /// </summary>
        /// <param name="args"></param>
        [JSInvokable]
        public async Task OnMove(SorttableMoveEventArgs args)
        {
            _isRender = false;
            if (_options?.OnMove != null)
                _options.OnMove(args);

            await Task.CompletedTask;
        }

        /// <summary>
        /// clone
        /// </summary>
        /// <param name="args"></param>
        [JSInvokable]
        public async Task OnClone(SorttableEventArgs args)
        {
            _isRender = false;
            if (_options?.OnClone != null)
                _options.OnClone(args);

            await Task.CompletedTask;
        }

        [JSInvokable]
        public async Task OnChange(SorttableEventArgs args)
        {
            _isRender = false;
            if (_options?.OnChange != null)
                _options.OnChange(args);

            await Task.CompletedTask;
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