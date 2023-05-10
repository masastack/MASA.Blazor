namespace Masa.Blazor
{
    public class SorttableEventArgs
    {
        public string? ParentId { get; set; }

        public string? NewParentId { get; set; }

        public string? ItemId { get; set; }

        public int OldIndex { get; set; }

        public int NewIndex { get; set; }

        public bool IsClone { get; set; }

        public string? CloneId { get; set; }
    }
}
