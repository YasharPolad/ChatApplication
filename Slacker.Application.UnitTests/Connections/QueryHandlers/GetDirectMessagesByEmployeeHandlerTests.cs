using Moq;
using Shouldly;
using Slacker.Application.Connections.Queries;
using Slacker.Application.Connections.QueryHandlers;
using Slacker.Application.Interfaces.RepositoryInterfaces;
using Slacker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Application.UnitTests.Connections.QueryHandlers;
public class GetDirectMessagesByEmployeeHandlerTests
{
    private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
	GetDirectMessagesByEmployeeQuery query;
    Employee employee;
	public GetDirectMessagesByEmployeeHandlerTests()
	{
		_employeeRepositoryMock = new();
		query = new GetDirectMessagesByEmployeeQuery { EmployeeId = 10 };

        employee = new Employee();
        employee.Connections = new List<Connection> //TODO: refactor, both get channels and get dms have this
        {
            new Connection
            {
                IsChannel = true,
                Name = "Channel Connection 1"
            },
            new Connection
            {
                IsChannel = true,
                Name = "Channel Connection 2"
            },
            new Connection
            {
                IsChannel = false,
                Name = "DM Connection 1"
            },
            new Connection
            {
                IsChannel = true,
                Name = "DM Connection 2"
            },
        };
    }

    [Fact]
    public async Task Handle_Should_ReturnFail_IfEmployeeIsntFound()
    {
        //Arrange
        _employeeRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Employee, bool>>>(),
                                         It.IsAny<Expression<Func<Employee, object>>>()))
            .ReturnsAsync((Employee)null);

        var handler = new GetDirectMessagesByEmployeeQueryHandler(_employeeRepositoryMock.Object);
        //Act
        var result = await handler.Handle(query, default);

        //Assert
        result.IsSuccess.ShouldBeFalse();
        result.Payload.ShouldBeNull();
        result.Errors.ShouldContain("This employee doesn't exist");
    }

    [Fact]
    public async Task Handle_Should_ReturnSucess_IfEmployeeIsFound()
    {
        //Arrange
        _employeeRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Employee, bool>>>(),
                                         It.IsAny<Expression<Func<Employee, object>>>()))
            .ReturnsAsync(employee);

        var handler = new GetDirectMessagesByEmployeeQueryHandler(_employeeRepositoryMock.Object);
       
        //Act
        var result = await handler.Handle(query, default);

        //Assert
        result.IsSuccess.ShouldBeTrue();
        result.Payload.ShouldNotBeNull();
        result.Errors.ShouldNotContain("This employee doesn't exist");

    }

    [Fact]
    public async Task Handle_Should_Return_OnlyDirectMessages()
    {
        //Arrange
        _employeeRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Employee, bool>>>(),
                                         It.IsAny<Expression<Func<Employee, object>>>()))
            .ReturnsAsync(employee);

        var handler = new GetDirectMessagesByEmployeeQueryHandler(_employeeRepositoryMock.Object);

        //Act
        var result = await handler.Handle(query, default);

        //Assert
        result.Payload.ShouldBeOfType<List<Connection>>();
        result.Payload.ShouldNotContain(c => c.IsChannel == true); //should contain only direct messages
    }

}
