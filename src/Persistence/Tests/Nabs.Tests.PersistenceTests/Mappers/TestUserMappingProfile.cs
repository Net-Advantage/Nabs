namespace Nabs.Tests.PersistenceTests.Mappers;

public class TestUserMappingProfile : Profile
{
    public TestUserMappingProfile()
    {
        CreateProjection<TestUser, TestUserDto>()
            .ForMember(_ => _.FullName, _ => _.MapFrom(src => $"{src.FirstName} {src.LastName}"));
    }
}