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
public class GetChannelsByEmployeeHandlerTests
{
    private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
    Employee employee;
    GetChannelsByEmployeeQuery query;
    public GetChannelsByEmployeeHandlerTests()
    {
        _employeeRepositoryMock = new();
        query = new GetChannelsByEmployeeQuery { EmployeeId = 12 }; //Random query

        employee = new Employee();
        employee.Connections = new List<Connection>
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
    public async Task Handle_Should_ReturnFail_WhenEmployeIsntFound()
    {
        //Arrange
        _employeeRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Employee, bool>>>(),
                                        It.IsAny<Expression<Func<Employee, object>>>()))
            .ReturnsAsync((Employee)null);

        var handler = new GetChannelsByEmployeeQueryHandler(_employeeRepositoryMock.Object);
        //Act
        var result = await handler.Handle(query, default);

        //Assert
        result.IsSuccess.ShouldBe(false);
        result.Payload.ShouldBeNull();
        result.Errors.ShouldContain("This employee doesn't exist");

    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenEmployeIsFound()
    {
        //Arrange
        _employeeRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Employee, bool>>>(),
                                         It.IsAny<Expression<Func<Employee, object>>>()))
            .ReturnsAsync(employee);

        var handler = new GetChannelsByEmployeeQueryHandler(_employeeRepositoryMock.Object);
        //Act
        var result = await handler.Handle(query, default);

        //Assert
        result.IsSuccess.ShouldBe(true);
        result.Errors.ShouldNotContain("This employee doesn't exist");

    }

    [Fact]
    public async Task Handle_Should_ReturnOnlyChannels()
    {
        //Arrange
        _employeeRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Employee, bool>>>(), 
                                        It.IsAny<Expression<Func<Employee, object>>>()))
            .ReturnsAsync(employee);

        var handler = new GetChannelsByEmployeeQueryHandler(_employeeRepositoryMock.Object);
        //Act
        var result = await handler.Handle(query, default);

        //Assert
        result.Payload.ShouldBeOfType(typeof(List<Connection>));
        result.Payload.ShouldNotContain(c => c.IsChannel == false); //All of the should be connections
    }
}
