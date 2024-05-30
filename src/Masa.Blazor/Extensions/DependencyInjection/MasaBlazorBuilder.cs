namespace Microsoft.Extensions.DependencyInjection;

public class MasaBlazorBuilder(IServiceCollection services) : IMasaBlazorBuilder
{
    public IServiceCollection Services { get; set; } = services;
}