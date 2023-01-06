using System.Runtime.InteropServices.ComTypes;

namespace Nabs.Tests.TestsTests.DataAttributes;


public class CsvDataAttributeUnitTests
{

	[Theory]
	//[CsvDataAttribute<CsvTestDataModel>()]
	[ClassData(typeof(TheoryData<CsvTestDataModel>))]
	public void LoadCsvTestDataWithAttribute(string scenario, CsvTestDataModel item)
	{
		_ = scenario;
		_ = item;
	}
}