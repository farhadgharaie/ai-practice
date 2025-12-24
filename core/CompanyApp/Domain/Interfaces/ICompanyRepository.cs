using CompanyApp.Domain.Entities;

namespace CompanyApp.Domain.Interfaces;

public interface ICompanyRepository
{
   Task<List<Company>> SearchByNameAsync(string name);
}