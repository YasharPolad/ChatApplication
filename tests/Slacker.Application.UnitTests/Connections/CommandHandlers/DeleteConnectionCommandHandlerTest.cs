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
public class DeleteConnectionCommandHandlerTest : IDisposable
{
    private readonly Mock<IConnectionRepository> _connectionRepositoryMock;
    DeleteConnectionCommand command;
    public DeleteConnectionCommandHandlerTest()
    {
        _connectionRepositoryMock = new();
        _connectionRepositoryMock
            .Setup(repo => repo.DeleteAsync(It.IsAny<Connection>()))
            .Returns(Task.CompletedTask);

        command = new DeleteConnectionCommand { Id = 10 }; // Some random connection Id to delete

    }
    public void Dispose()
    {
        command = null;
    }

    [Fact]
    public async Task Handle_Should_ReturnFail_WhenConnectionIsntFound()
    {
        //Arrange
        _connectionRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Connection, bool>>>()))
            .ReturnsAsync((Connection)null);
        
        var handler = new DeleteConnectionCommandHandler(_connectionRepositoryMock.Object);
        //Act
        var result = await handler.Handle(command, default);

        //Assert
        result.IsSuccess.ShouldBe(false);
        result.Errors.ShouldContain("The connection to delete wasn't found!");
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenConnectionIsntFound()
    {
        //Arrange
        var connectionFake = new Mock<Connection>();
        _connectionRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Connection, bool>>>()))
            .ReturnsAsync(connectionFake.Object);

        var handler = new DeleteConnectionCommandHandler(_connectionRepositoryMock.Object);
        //Act
        var result = await handler.Handle(command, default);

        //Assert
        result.IsSuccess.ShouldBe(true);
    }
}
