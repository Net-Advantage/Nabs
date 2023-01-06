using System.Reflection;

namespace Nabs.Resources
{
	public class ResourceLoader
	{
		private readonly Assembly _assembly;

		public ResourceLoader(Type relativeAssemblyType)
		{
			_assembly = Assembly.GetAssembly(relativeAssemblyType);
		}
	}
}