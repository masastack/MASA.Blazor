namespace Microsoft.Extensions.DependencyInjection;

public interface IMasaBlazorBuilder
{
    IServiceCollection Services { get; set; }
}