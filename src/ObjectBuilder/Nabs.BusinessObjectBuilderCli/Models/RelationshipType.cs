using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nabs.BusinessObjectBuilderCli.Models
{
	[JsonConverter(typeof(StringEnumConverter))]
    public enum RelationshipType
    {
        Unknown,
        Spouse,
        Child,
        Friend
    }
}