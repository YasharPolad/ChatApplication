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
public class AddEmployeeToConnectionCommandHandlerTests
{
    private readonly Mock<IConnectionRepository> _connectionRepositoryMock;
    private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
    Mock<Employee> _employeeMock;
    AddEmployeeToConnectionCommand command;
    List<Connection> _connections;
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

        _connections = new List<Connection>
        {
            new Connection  //Direct message that has one employee
            {
                IsChannel = false,
                Employees = new List<Employee> {_employeeMock.Object}
            },
            new Connection //Direct message that has two employees already
            {
                IsChannel = false,
                Employees = new List<Employee>{ _employeeMock.Object, _employeeMock.Object}
            }
        };
        
    }


    [Fact]
    public async Task Handle_Should_ReturnError_WhenConnectionIsntFound()
    {
        //Arrange
        _connectionRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Connection, bool>>>(),
                                         It.IsAny<Expression<Func<Connection, object>>>()))
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
        //Arrange           
        _connectionRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Connection, bool>>>(),
                                         It.IsAny<Expression<Func<Connection, object>>>()))
            .ReturnsAsync(_connections[1]);

        var handler = new AddEmployeeToConnectionCommandHandler(_connectionRepositoryMock.Object, _employeeRepositoryMock.Object);

        //Act
        var result = await handler.Handle(command, default);

        //Assert
        result.IsSuccess.ShouldBe(false);
        result.Errors.ShouldContain("Can't add a third employee to a direct message");
    }

    [Fact]
    public async Task Handle_Should_ReturnError_WhenEmployeeIsntFound()
    {
        //Arrange
        _connectionRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Connection, bool>>>(),
                                         It.IsAny<Expression<Func<Connection, object>>>()))
            .ReturnsAsync(_connections[0]);

        _employeeRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Employee, bool>>>()))
            .ReturnsAsync((Employee)null);

        var handler = new AddEmployeeToConnectionCommandHandler(_connectionRepositoryMock.Object, _employeeRepositoryMock.Object);

        //Act
        var result = await handler.Handle(command, default);

        //Assert
        result.IsSuccess.ShouldBe(false);
        result.Errors.ShouldContain("The employee wasn't found");
    }

    [Fact]  
    public async Task Handle_Should_ReturnError_IfEmployeeIsAlreadyInTheConnection()
    {
        //Arrange
        _connectionRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Connection, bool>>>(),
                                         It.IsAny<Expression<Func<Connection, object>>>()))
            .ReturnsAsync(_connections[0]);

        _employeeRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Employee, bool>>>()))
            .ReturnsAsync(_employeeMock.Object);
        
        var handler = new AddEmployeeToConnectionCommandHandler(_connectionRepositoryMock.Object, _employeeRepositoryMock.Object);
        
        //Act
        var result = await handler.Handle(command, default);

        //Assert
        result.IsSuccess.ShouldBe(false);
        result.Errors.ShouldContain($"Employee {command.EmployeeId} is already in connection {command.ConnectionId}");


    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_IfAllOtherTestsPass()
    {
        //Arrange
        _connectionRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Connection, bool>>>(),
                                         It.IsAny<Expression<Func<Connection, object>>>()))
            .ReturnsAsync(_connections[0]);
        
        var newEmployeeMock = new Mock<Employee>();

        _employeeRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Employee, bool>>>()))
            .ReturnsAsync(newEmployeeMock.Object);

        var handler = new AddEmployeeToConnectionCommandHandler(_connectionRepositoryMock.Object, _employeeRepositoryMock.Object);

        //Act
        var result = await handler.Handle(command, default);

        //Assert
        result.IsSuccess.ShouldBe(true);
        _connections[0].Employees.ShouldContain(newEmployeeMock.Object);
    }
}
