namespace Nabs.Tests.PubSubTests;

internal class KafkaPubSubTestBase
{
}

public abstract class KafkaPubSubTestBase<TKafkaPubSubFixture>(
    ITestOutputHelper testOutputHelper,
    TKafkaPubSubFixture testFixture)
    : FixtureTestBase<TKafkaPubSubFixture>(testOutputHelper, testFixture)
    where TKafkaPubSubFixture : KafkaPubSubFixtureBase
{

}