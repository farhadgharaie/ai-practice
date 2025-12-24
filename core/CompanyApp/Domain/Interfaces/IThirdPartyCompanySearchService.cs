using CompanyApp.Domain.Entities;

namespace CompanyApp.Domain.Interfaces;

public interface IThirdPartyCompanySearchService
{
    Task<List<Company>> SearchAsync(string name);

}