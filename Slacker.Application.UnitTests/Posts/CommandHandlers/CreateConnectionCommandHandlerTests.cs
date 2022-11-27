using Moq;
using Shouldly;
using Slacker.Application.Connections.CommandHandlers;
using Slacker.Application.Connections.Commands;
using Slacker.Application.Interfaces.RepositoryInterfaces;
using Slacker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Application.UnitTests.Posts.CommandHandlers;
public class CreateConnectionCommandHandlerTests
{
    private readonly Mock<IConnectionRepository> _connectionRepositoryMock;
    private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;

    public CreateConnectionCommandHandlerTests()
    {
        _connectionRepositoryMock = new();
        _employeeRepositoryMock = new();
    }
    [Fact]
    public async Task Handle_ShouldReturnConnection_With_OneEmployee()
    {

        //Arrange
        var command = new CreateConnectionCommand
        {
            CreatingUserId = Guid.NewGuid().ToString(),
            IsChannel= true,
            IsPrivate= false,
            Name= "Mock Channel"
        };

        var employee = new Employee
        {
            IdentityId = command.CreatingUserId,
        };

        var connection = new Connection
        {
            Name = command.Name,
            IsChannel = command.IsChannel,
            IsPrivate = command.IsPrivate,
            Employees = new List<Employee>()
        };


        _employeeRepositoryMock
            .Setup(repo => repo.GetAsync(e => e.IdentityId == command.CreatingUserId))
            .ReturnsAsync(employee);
        _connectionRepositoryMock
            .Setup(repo => repo.CreateAsync(connection))
            .Returns(Task.CompletedTask);

        var handler = new CreateConnectionCommandHandler(_connectionRepositoryMock.Object, _employeeRepositoryMock.Object);

        //Act

        var result = await handler.Handle(command, default);

        //Assert
        result.IsSuccess.ShouldBe(true);
        result.Payload.ShouldNotBeNull().ShouldBeOfType(typeof(Connection));
        result.Payload.Employees.Count.ShouldBe(1);
        result.Payload.Employees.FirstOrDefault().IdentityId.ShouldBeSameAs(command.CreatingUserId);

    }
}
