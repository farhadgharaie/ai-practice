namespace CompanyApp.Domain.Entities;

public class Company(string id, string name)
{
    public string Id { get; set; } = id;
    public string Name { get; set; } = name;
}