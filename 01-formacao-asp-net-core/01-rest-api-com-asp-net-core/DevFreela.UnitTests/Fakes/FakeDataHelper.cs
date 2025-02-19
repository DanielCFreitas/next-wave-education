using Bogus;
using DevFreela.Application.Commands.InsertProject;
using DevFreela.Core.Entities;

namespace DevFreela.UnitTests.Fakes;

public class FakeDataHelper
{
    
    // Forma 1 de criar o faker
    private static readonly Faker _faker = new();

    public static Project CreateFakeProjectV1()
    {
        return new Project(
            _faker.Commerce.ProductName(),
            _faker.Lorem.Sentence(),
            _faker.Random.Decimal(1000, 10000),
            _faker.Random.Int(1, 100),
            _faker.Random.Int(1, 100));
    }

    // Forma 2 de criar o faker
    private static readonly Faker<Project> _projectFaker = new Faker<Project>()
        .CustomInstantiator(faker => new(
            faker.Commerce.ProductName(),
            faker.Lorem.Sentence(),
            faker.Random.Decimal(1000, 10000),
            faker.Random.Int(1, 100),
            faker.Random.Int(1, 100)));
    
    public static Project CreateFakeProject() => _projectFaker.Generate();
    public static List<Project> CreateFakeProjectList(int count) => _projectFaker.Generate(count);

    // Forma 3 de criar o faker (apenas quando possui construtor vazio)
    private static readonly Faker<InsertProjectCommand> _insertProjectCommandFaker =
        new Faker<InsertProjectCommand>()
            .RuleFor(project => project.Title, f => f.Commerce.ProductName())
            .RuleFor(project => project.Description, f => f.Lorem.Sentence())
            .RuleFor(project => project.IdFreelancer, f => f.Random.Int(1, 100))
            .RuleFor(project => project.IdClient, f => f.Random.Int(1, 100))
            .RuleFor(project => project.TotalCost, f => f.Random.Decimal(1000, 10000));
    
    public static InsertProjectCommand CreateFakeInsertProjectCommand() =>
        _insertProjectCommandFaker.Generate();
}