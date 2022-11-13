using AutoMapper;
using Slacker.Application.Models.DTOs;
using Slacker.Domain.Entities;

namespace Slacker.Api.Mappings;

public class EmployeeMapping : Profile
{
	public EmployeeMapping()
	{
		CreateMap<Employee, EmployeeDto>();
	}
}
