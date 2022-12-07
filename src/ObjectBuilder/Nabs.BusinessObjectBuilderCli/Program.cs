using Nabs.BusinessObjectBuilderCli;
using Nabs.BusinessObjectBuilderCli.Models;
using NJsonSchema;

var rootPath = @"C:\Dev\Nabs\Nabs\src\ObjectBuilder\Nabs.BusinessObjectBuilderCli\";

var generatedSchemaPath = Path.Join(rootPath, "Schemas", "generated.person.schema.json");
var jsonSchemaGenerator = new JsonSchemaGenerator(rootPath, "generated.schema.json");

jsonSchemaGenerator.Generate<People>();

var inputsPath = Path.Join(rootPath, "Inputs", "people.json");
var inputsJson = File.ReadAllText(inputsPath);

//var schemaPath = Path.Join(rootPath, "Schemas", "person.schema.json");
var schemaPath = Path.Join(rootPath, "GeneratedSchemas", "people.schema.json");
var schema = await JsonSchema.FromFileAsync(schemaPath);
var schemaValidationErrors = schema.Validate(inputsJson);

foreach (var schemaValidationError in schemaValidationErrors)
{
	Console.WriteLine(schemaValidationError);
}




//var people = JsonConvert.DeserializeObject<List<Person>>(inputJson);

//foreach (var person in people)
//{
//	Console.WriteLine(person.ToString());
//}

//var pocoService = new PocoService();
//var result = pocoService.Convert(schema);
//Console.Write(result);


//var generator = new JSchemaGenerator();
//var jSchema = generator.Generate(typeof(Person));
//var schemaJson = jSchema.ToString();
//File.WriteAllText(schemaPath, schemaJson);
//Console.Read();
