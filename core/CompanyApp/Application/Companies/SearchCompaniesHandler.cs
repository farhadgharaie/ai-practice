using CompanyApp.Domain.Entities;
using CompanyApp.Domain.Interfaces;
using MediatR;

namespace CompanyApp.Application.Companies;

public class SearchCompaniesHandler 
    : IRequestHandler<SearchCompaniesQuery, List<Company>>
{
    private readonly ICompanyRepository _repository;

    public SearchCompaniesHandler(ICompanyRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Company>> Handle(SearchCompaniesQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            return new List<Company>();

        return await _repository.SearchByNameAsync(request.Name);
    }
}