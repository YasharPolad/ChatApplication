using Slacker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Application.Interfaces.RepositoryInterfaces;
public interface IEmployeeRepository : IGenericRepository<Employee> //TODO: Actually you shouldn't be able to create or delete employees. Employees are create/deleted automatically when users are.
{
}
