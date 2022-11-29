using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using Shouldly;
using Slacker.Application.Connections.CommandHandlers;
using Slacker.Application.Connections.Commands;
using Slacker.Application.Interfaces.RepositoryInterfaces;
using Slacker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Application.UnitTests.Connections.CommandHandlers;
public class AddEmployeeToConnectionCommandHandlerTests : IDisposable
{
    private readonly Mock<IConnectionRepository> _connectionRepositoryMock;
    private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
    Mock<Employee> _employeeMock;
    AddEmployeeToConnectionCommand command;
    public AddEmployeeToConnectionCommandHandlerTests()
    {
        _connectionRepositoryMock = new();
        _employeeRepositoryMock = new();
        _employeeMock = new();

        command = new AddEmployeeToConnectionCommand  //Random command
        {
            EmployeeId = 1,
            ConnectionId = 2,
        };
        
    }
    public void Dispose()
    {
        command = null;
    }

    [Fact]
    public async Task Handle_Should_ReturnError_WhenConnectionIsntFound()
    {
        //Arrange
        _connectionRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Connection, bool>>>()))
            .ReturnsAsync((Connection)null);

        var handler = new AddEmployeeToConnectionCommandHandler(_connectionRepositoryMock.Object, _employeeRepositoryMock.Object);

        //Act
        var result = await handler.Handle(command, default);

        //Assert
        result.IsSuccess.ShouldBe(false);
        result.Errors.ShouldContain("The connection wasn't found");
    }

    [Fact]
    public async Task Handle_Should_ReturnError_WhenConnectionIsDirectMessage_WithTwoEmployees()
    {
        var connection = new Connection();
        connection.IsChannel = false;
        connection.Employees = new List<Employee>
        {
            _employeeMock.Object,
            _employeeMock.Object
        };
        Console.WriteLine(connection is null);
        //Arrange           
        _connectionRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Connection, bool>>>()))
            .ReturnsAsync(connection);

        var handler = new AddEmployeeToConnectionCommandHandler(_connectionRepositoryMock.Object, _employeeRepositoryMock.Object);

        //Act
        var result = await handler.Handle(command, default);

        //Assert
        result.IsSuccess.ShouldBe(false);
        result.Errors.ShouldContain("Can't add a third employee to a direct message");
    }

    //[Fact]
    //public async Task Handle_Should_ReturnError_WhenConnectionIsntFound()
    //{
    //    //Arrange
    //    _connectionRepositoryMock
    //        .Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Connection, bool>>>()))
    //        .ReturnsAsync((Connection)null);

    //    var handler = new AddEmployeeToConnectionCommandHandler(_connectionRepositoryMock.Object, _employeeRepositoryMock.Object);

    //    //Act
    //    var result = await handler.Handle(command, default);

    //    //Assert
    //    result.IsSuccess.ShouldBe(false);
    //    result.Errors.ShouldContain("The connection wasn't found");
    //}
}
