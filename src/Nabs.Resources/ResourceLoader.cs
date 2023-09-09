using System.Reflection;

namespace Nabs.Resources
{
	public class ResourceLoader
	{
		private readonly string[] _manifestResourceNames;

		public ResourceLoader(params Type[] relativeAssemblyTypes)
		{
			var warnings = new List<string>();
			var manifestResourceNames = new List<string>();
			foreach (var relativeAssemblyType in relativeAssemblyTypes)
			{
				var assembly = Assembly.GetAssembly(relativeAssemblyType);
				if (assembly != null)
				{
					manifestResourceNames.AddRange(assembly.GetManifestResourceNames());
				}
				else
				{
					warnings.Add($"'{relativeAssemblyType}' was not loaded.");
				}
			}

			_manifestResourceNames = manifestResourceNames.ToArray();
			Warnings = warnings.ToArray();
		}

		public string[] Warnings { get; }

		private string GetResourcePath(Func<string, bool> predicate)
		{
			var resourcePath = _manifestResourceNames.Where(predicate).FirstOrDefault();

			return string.IsNullOrWhiteSpace(resourcePath) ? string.Empty : resourcePath;
		}
	}
}