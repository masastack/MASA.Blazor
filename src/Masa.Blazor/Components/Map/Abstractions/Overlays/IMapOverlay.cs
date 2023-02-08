namespace Masa.Blazor
{
    public interface IMapOverlay<TMap> where TMap : IMap
    {
        public IJSObjectReference OverlayRef { get; set; }

        public TMap MapRef { get; set; }

        public async Task ShowAsync() => await OverlayRef.InvokeVoidAsync("show");

        public async Task HideAsync() => await OverlayRef.InvokeVoidAsync("hide");

    }
}
