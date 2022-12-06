namespace Nabs.BusinessObjectBuilderCli.Models;

public class IntrovertPerson : IPerson
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public int Dependents { get; set; }

    public Address StreetAddress { get; set; }

    public List<Relationship> Relationships { get; set; }
}