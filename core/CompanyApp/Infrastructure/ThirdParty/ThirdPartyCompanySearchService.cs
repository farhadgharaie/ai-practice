using System.Net.Http;
using System.Net.Http.Json;
using CompanyApp.Domain.Entities;
using CompanyApp.Domain.Interfaces;

namespace CompanyApp.Infrastructure.ThirdParty;

public class ThirdPartyCompanySearchService : IThirdPartyCompanySearchService
{
    private readonly HttpClient _httpClient;

    public ThirdPartyCompanySearchService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Company>> SearchAsync(string name)
    {
        var url = $"api/company/search?name={Uri.EscapeDataString(name)}";

        var result = await _httpClient.GetFromJsonAsync<List<Company>>(url);

        return result ?? new List<Company>();
    }
}