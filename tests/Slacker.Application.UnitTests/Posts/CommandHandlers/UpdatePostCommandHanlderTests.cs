using Moq;
using Slacker.Application.Interfaces.RepositoryInterfaces;
using Slacker.Application.Posts.CommandHandlers;
using Slacker.Application.Posts.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Application.UnitTests.Posts.CommandHandlers;
public class UpdatePostCommandHanlderTests
{
    private readonly Mock<IPostRepository> _postRepositoryMock;
	UpdatePostCommand command;
	public UpdatePostCommandHanlderTests()
	{
		_postRepositoryMock = new();
		command = new UpdatePostCommand
		{
			postId = 15,
			updatedMessage = "Updated test message"
		};
	}
	
	[Fact]
	public async Task Handle_Should_ReturnFail_IfPostIsntFound()
	{
		//Arrange
		_postRepositoryMock
			.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Post, bool>>>()))
			.ReturnsAsync((Post)null);

		var handler = new UpdatePostCommandHandler(_postRepositoryMock.Object);

		//Act
		var result = await handler.Handle(command, default);

		//Assert
		result.IsSuccess.ShouldBeFalse();
		result.Errors.ShouldContain("This post doesn't exist");
	}

	[Fact]
	public async Task Handle_Should_ReturnSucess_IfPostIsFound()
	{
		//Arrange
		var postFake = new Post();
        _postRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Post, bool>>>()))
            .ReturnsAsync(postFake);
		var handler = new UpdatePostCommandHandler(_postRepositoryMock.Object);

		//Act
		var result = await handler.Handle(command, default);

        //Assert
		result.IsSuccess.ShouldBeTrue();
        result.Errors.ShouldNotContain("This post doesn't exist");
    }

    [Fact]
    public async Task Handle_Should_Return_UpdatedPost_IfPostIsFound()
    {
        //Arrange
        var postFake = new Post();
        _postRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Post, bool>>>()))
            .ReturnsAsync(postFake);
        var handler = new UpdatePostCommandHandler(_postRepositoryMock.Object);

        //Act
        var result = await handler.Handle(command, default);

        //Assert
        postFake.IsEdited.ShouldBeTrue();
		postFake.Message.ShouldBeSameAs(command.updatedMessage);
    }
}
