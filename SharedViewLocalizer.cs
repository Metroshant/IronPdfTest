using IronPdfTest;
using Microsoft.Extensions.Localization;
using System.Reflection;


public class SharedViewLocalizer
{
    private readonly IStringLocalizer _localizer;

    public SharedViewLocalizer(IStringLocalizerFactory factory)
    {
        var type = typeof(SharedResources);
        var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
        _localizer = factory.Create("resource", assemblyName.Name);
    }

    public LocalizedString this[string key] => _localizer[key];

    public LocalizedString GetLocalizedString(string key)
    {
        return _localizer[key];
    }
}
