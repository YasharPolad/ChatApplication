using Microsoft.EntityFrameworkCore.Metadata;
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
public class UpdateConnectionCommandHandlerTests : IDisposable
{
    private readonly Mock<IConnectionRepository> _connectionRepositoryMock;
    UpdateConnectionCommand command;

    public UpdateConnectionCommandHandlerTests() 
    {
        _connectionRepositoryMock = new();
        
        _connectionRepositoryMock
            .Setup(repo => repo.UpdateAsync(It.IsAny<Connection>()))
            .Returns(Task.CompletedTask);

        command = new UpdateConnectionCommand
        {
            ConnectionId = 15,
            IsPrivate = false,
            Name = "Test Connection Updated"
        };

    }

    public void Dispose()
    {
        command = null;
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenConnectionIsntFound()
    {
        //Arrange
        _connectionRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Connection, bool>>>()))
            .ReturnsAsync((Connection)null);

        //Act
        var handler = new UpdateConnectionCommandHandler(_connectionRepositoryMock.Object);
        var result = await handler.Handle(command, default);

        //Assert
        result.IsSuccess.ShouldBeFalse();
        result.Errors.ShouldContain("Connection wasn't found! :(");
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenConnectionIsFound()
    {
        var connectionFake = new Connection();
        //Arrange
        _connectionRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Connection, bool>>>()))
            .ReturnsAsync(connectionFake);

        //Act
        var handler = new UpdateConnectionCommandHandler(_connectionRepositoryMock.Object);
        var result = await handler.Handle(command, default);

        //Assert        
        connectionFake.Name.ShouldBeSameAs(command.Name);
        connectionFake.IsPrivate.Equals(command.IsPrivate);
        result.IsSuccess.ShouldBeTrue();
    }
}
