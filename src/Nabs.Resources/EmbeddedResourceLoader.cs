using System.Text;

namespace Nabs.Resources;

public class EmbeddedResourceLoader
{
    private readonly System.Collections.Generic.HashSet<ResourceInfo> _resourceInfos = [];

    public EmbeddedResourceLoader(params Type[] relativeAssemblyTypes)
    {
        var assemblies = GetAssemblies(relativeAssemblyTypes);

        foreach (var assembly in assemblies)
        {
            var manifestResourceNames = assembly.GetManifestResourceNames();
            foreach (var manifestResourceName in manifestResourceNames)
            {
                _ = _resourceInfos.Add(new(manifestResourceName, assembly));
            }
        }
    }

    public List<string> Warnings { get; } = [];

    public IEnumerable<ResourceInfo> GetResourceInfoItems(Func<ResourceInfo, bool> predicate)
    {
        var items = _resourceInfos
            .Where(predicate);

        return items;
    }

    public Result<Stream> GetResourceStreamContent(Func<ResourceInfo, bool> predicate)
    {
        var resourceInfoItems = GetResourceInfoItems(predicate);
        var itemCount = resourceInfoItems.Count();
        if (itemCount == 0)
        {
            return new Result<Stream>(new Exception("No resource info items found."));
        }
        else if (itemCount > 1)
        {
            return new Result<Stream>(new Exception("More than one resource info item found."));
        }

        var resourceInfoItem = resourceInfoItems.First();
        var resourceStream = resourceInfoItem.Assembly
            .GetManifestResourceStream(resourceInfoItem.Path)!;

        return resourceStream;
    }

    public Result<byte[]> GetResourceBytesContent(Func<ResourceInfo, bool> predicate)
    {
        var resourceStreamResult = GetResourceStreamContent(predicate);

        var result = resourceStreamResult.Match(resourceStream =>
        {
            var resourceBytes = new byte[resourceStream.Length];
            resourceStream.Read(resourceBytes, 0, resourceBytes.Length);
            return resourceBytes;

        }, exception =>
        {
            return new Result<byte[]>(new Exception(""));
        });

        return result;
    }

    public Result<string> GetResourceTextContent(Func<ResourceInfo, bool> predicate)
    {
        var resourceStreamResult = GetResourceStreamContent(predicate);

        var result = resourceStreamResult.Match(resourceStream =>
        {
            using var reader = new StreamReader(resourceStream, Encoding.UTF8);
            var text = reader.ReadToEnd();
            return text;

        }, exception =>
        {
            return new Result<string>(new Exception(""));
        });

        return result;
    }

    private static System.Collections.Generic.HashSet<Assembly> GetAssemblies(params Type[] relativeAssemblyTypes)
    {
        var assemblies = new System.Collections.Generic.HashSet<Assembly>();

        foreach (var relativeAssemblyType in relativeAssemblyTypes)
        {
            var assembly = Assembly.GetAssembly(relativeAssemblyType)!;
            _ = assemblies.Add(assembly);
        }

        return assemblies;
    }
}

public record struct ResourceInfo(string Path, Assembly Assembly);