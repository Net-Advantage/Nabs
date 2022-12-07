using Nabs.BusinessObjectBuilderCli.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NJsonSchema;
using NJsonSchema.Generation;

namespace Nabs.BusinessObjectBuilderCli;

public class JsonSchemaGenerator
{
	private const string _folderName = "GeneratedSchemas";
	private readonly string _rootPath;
	private readonly string _fileName;

	public JsonSchemaGenerator(string rootPath, string fileName)
	{
		_rootPath = rootPath;
		_fileName = fileName;
	}

	public void Generate<T>()
		where T : class
	{
		var serializerSettings = new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() };
		var settings = new JsonSchemaGeneratorSettings { SerializerSettings = serializerSettings };
		
		//Generate and validate the schema
		var schema = JsonSchema.FromType<T>(settings);
		var schemaJson = schema.ToJson(Formatting.Indented);

		var schemaPath = Path.Combine(_rootPath, _folderName, _fileName);
		File.WriteAllText(schemaPath, schemaJson);

		//break it up into small parts.

		foreach (var jsonSchemaProperty in schema.Properties)
		{
			var definitionPath = Path.Combine(_rootPath, _folderName, $"{jsonSchemaProperty.Key}.json");
			schema.Definitions.TryGetValue(jsonSchemaProperty.Key, out var definition);

			

			//var definitionJson = definition.ActualSchema.ToJson();
			//File.WriteAllText(definitionPath, definitionJson);
		}

		var allDefinitionKeys = schema.Definitions.Keys.ToList();

		while (allDefinitionKeys.Any())
		{
			foreach (var schemaDefinition in schema.Definitions)
			{
				var schemaDefinitionPath = Path.Combine(_rootPath, _folderName, $"{schemaDefinition.Key}.schema.json");

				var hasReferences = schemaDefinition.Value.HasReference
				                    || schemaDefinition.Value.Properties.Any(_ => _.Value.HasReference);

				if (!hasReferences)
				{
					var schemaDefinitionJson = schemaDefinition.Value.ActualSchema.ToJson();
					File.WriteAllText(schemaDefinitionPath, schemaDefinitionJson);
				}
				
				allDefinitionKeys.Remove(schemaDefinition.Key);
			}
		}
	}
}