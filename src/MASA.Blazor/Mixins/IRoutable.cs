namespace MASA.Blazor
{
    public interface IRoutable
    {
        string ActiveClass { get; }

        bool Append { get; }

        bool Disabled { get; }

        bool? Exact { get; }

        bool ExactPath { get; }

        string ExactActiveClass { get; }

        bool Link { get; }

        object Href { get; }

        object To { get; }

        bool Nuxt { get; }

        bool Replace { get; }

        string Tag { get; }

        string Target { get; }
    }
}
