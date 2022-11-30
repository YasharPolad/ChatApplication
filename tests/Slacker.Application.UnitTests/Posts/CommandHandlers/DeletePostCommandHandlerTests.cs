using Moq;
using Shouldly;
using Slacker.Application.Interfaces.RepositoryInterfaces;
using Slacker.Application.Posts.CommandHandlers;
using Slacker.Application.Posts.Commands;
using Slacker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Application.UnitTests.Posts.CommandHandlers;
public class DeletePostCommandHandlerTests
{
    private readonly Mock<IPostRepository> _postRepositoryMock;
    private readonly Mock<Post> _postMock;

    public DeletePostCommandHandlerTests()
    {
        _postRepositoryMock = new();
        _postMock = new();
    }

    [Fact]
    public async Task Handle_Shouldnt_ReturnSuccess_WhenPostDoesntExist()
    {
        //Arrange
        var command = new DeletePostCommand { Id = It.IsAny<int>() };

        //_postRepositoryMock.Setup(repo => repo.GetAsync(p => p.ChildPosts.Count > 4)).ReturnsAsync((Post)null);

        _postRepositoryMock.Setup(repo => repo.GetAsync(
            It.IsAny<Expression<Func<Post, bool>>>(), default)).ReturnsAsync((Post)null);
        var handler = new DeletePostCommandHandler(_postRepositoryMock.Object);

        //Act
        var result = await handler.Handle(command, default);

        //Assert
        result.IsSuccess.ShouldBe(false);
        result.Errors.ShouldContain("This post doesn't exist");
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenPostExists()
    {
        //Arrange
        var command = new DeletePostCommand { Id = It.IsAny<int>() };

        _postRepositoryMock.Setup(repo => repo.GetAsync(
            It.IsAny<Expression<Func<Post, bool>>>())).ReturnsAsync(_postMock.Object); //TODO: Why can I not use It.IsAny<Post>()?
        var handler = new DeletePostCommandHandler(_postRepositoryMock.Object);

        //Act
        var result = await handler.Handle(command, default);

        //Assert
        result.IsSuccess.ShouldBe(true);
        result.Errors.ShouldBeEmpty();
    }
}
