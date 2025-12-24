using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using CompanyApp.Application.Companies;
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
       var url = $"company/search/{Uri.EscapeDataString(name)}";
        var result = new List<Company>();

        try
        {
            // 1. Get raw content
            var json = await _httpClient.GetStringAsync(url);

            Console.WriteLine("RAW API RESPONSE:");
            Console.WriteLine(json);

            // 2. Convert to List<CompanyDto> AFTER inspection
            var apiResult = JsonSerializer.Deserialize<List<CompanyDto>>(json);

            // 2. Map DTO → Domain Entity
            if (apiResult != null)
            {
                result = apiResult
                    .Select(x => new Company(x.id, x.name))
                    .ToList();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error calling API or deserializing: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
            return new List<Company>();
        }

        return result;
    }
}