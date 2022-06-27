namespace Microsoft.Extensions.DependencyInjection;

public class MasaBlazorBuilder : BlazorComponentBuilder, IMasaBlazorBuilder
{
    public MasaBlazorBuilder(IServiceCollection services) : base(services)
    {
    }
}
