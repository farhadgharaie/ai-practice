using CompanyApp.Domain.Entities;
using CompanyApp.Domain.Interfaces;
using MediatR;

namespace CompanyApp.Application.Companies;

public class SearchCompaniesHandler 
    : IRequestHandler<SearchCompaniesQuery, List<CompanyDto>>
{
    private readonly ICompanyRepository _repository;
    private readonly IThirdPartyCompanySearchService _thirdParty;

    public SearchCompaniesHandler(ICompanyRepository repository,
        IThirdPartyCompanySearchService thirdParty)
    {
        _repository = repository;
        _thirdParty = thirdParty;
    }

    public async Task<List<CompanyDto>> Handle(SearchCompaniesQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            return new List<CompanyDto>();
        // First search in MongoDB
        var localResults = await _repository.SearchByNameAsync(request.Name);

        if (localResults.Count > 0)
        {
            return localResults.Select(x => new CompanyDto(x.Id, x.Name))
                .ToList();;
        }

        // Fallback to third-party API
        var remoteResults = await _thirdParty.SearchAsync(request.Name);

        return remoteResults
            .Select(x => new CompanyDto(x.Id, x.Name))
            .ToList();
    }
}