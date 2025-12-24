using CompanyApp.Domain.Entities;
using MediatR;

namespace CompanyApp.Application.Companies;

public record SearchCompaniesQuery(string Name) : IRequest<List<CompanyDto>>;