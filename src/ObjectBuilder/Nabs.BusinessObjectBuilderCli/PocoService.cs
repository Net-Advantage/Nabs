using Newtonsoft.Json.Schema;
using System.Text.RegularExpressions;
using System.Text;

namespace Nabs.BusinessObjectBuilderCli;

public class PocoService
{
	public static string Convert(JSchema schema)
	{
		return ConvertJsonSchemaToPoco(schema).ToString();
	}

	private static StringBuilder ConvertJsonSchemaToPoco(JSchema schema)
	{
		if(schema.Type == null)
			throw new Exception("Schema does not specify a type.");

		var sb = new StringBuilder();

		switch (schema.Type)
		{
			case JSchemaType.Object:
				sb.Append(ConvertJsonSchemaObjectToPoco(schema));
				break;

			case JSchemaType.Array:
				foreach (var item in schema.Items.Where(x => x.Type.HasValue && x.Type == JSchemaType.Object))
				{
					sb.Append(ConvertJsonSchemaObjectToPoco(item));
				}
				break;
		}

		return sb;
	}

	private static StringBuilder ConvertJsonSchemaObjectToPoco(JSchema schema)
	{
		string className;
		return ConvertJsonSchemaObjectToPoco(schema, out className);
	}

	private static StringBuilder ConvertJsonSchemaObjectToPoco(JSchema schema, out string className)
	{
		var sb = new StringBuilder();
		sb.Append("public class ");

		//if(schema.Title != null)
		//    className = GenerateSlug(schema.Title);
		//else
			className = String.Format("Poco_{0}",Guid.NewGuid().ToString().Replace("-", string.Empty));

		sb.Append(className);
		sb.AppendLine(" {");

		foreach (var item in schema.Properties)
		{
			sb.Append("public ");
			sb.Append(GetClrType(item.Value, sb));
			sb.Append(" ");
			sb.Append(item.Key.Trim());
			sb.AppendLine(" { get; set; }");
		}

		sb.AppendLine("}");
		return sb;
	}

	private static string GenerateSlug(string phrase)
	{
		var str = RemoveAccent(phrase);
		str = Regex.Replace(str, @"[^a-zA-Z\s-]", ""); // invalid chars
		str = Regex.Replace(str, @"\s+", " ").Trim(); // convert multiple spaces into one space, trim
		str = Regex.Replace(str, @"\s", "_"); // convert spaces to underscores
		return str;
	}

	private static string RemoveAccent(string txt)
	{
		var bytes = Encoding.GetEncoding("Cyrillic").GetBytes(txt);
		return Encoding.ASCII.GetString(bytes);
	}

	private static string GetClrType(JSchema jsonSchema, StringBuilder sb)
	{
		switch (jsonSchema.Type)
		{
			case JSchemaType.Array:
				if(jsonSchema.Items.Count == 0)
					return "IEnumerable<object>";
				if (jsonSchema.Items.Count == 1)
					return String.Format("IEnumerable<{0}>", GetClrType(jsonSchema.Items.First(), sb));
				throw new Exception("Not sure what type this will be.");

			case JSchemaType.Boolean:
				return "bool";

			case JSchemaType.Number:
				return "float";

			case JSchemaType.Integer:
				return "int";

			case JSchemaType.String:
				return "string";

			case JSchemaType.Object:
					string className;
					sb.Insert(0, ConvertJsonSchemaObjectToPoco(jsonSchema, out className));
				return className;

			case JSchemaType.None:
			case JSchemaType.Null:
			default:
				return "object";
		}
	}
}