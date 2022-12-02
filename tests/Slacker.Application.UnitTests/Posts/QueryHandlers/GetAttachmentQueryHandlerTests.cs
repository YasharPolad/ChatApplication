using Moq;
using Slacker.Application.Interfaces;
using Slacker.Application.Interfaces.RepositoryInterfaces;
using Slacker.Application.Models.DTOs;
using Slacker.Application.Posts.CommandHandlers;
using Slacker.Application.Posts.Queries;
using Slacker.Application.UnitTests.Posts.CommandHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Application.UnitTests.Posts.QueryHandlers;
public class GetAttachmentQueryHandlerTests
{
    private readonly Mock<IAttachmentRepository> _attachmentRepositoryMock;
    private readonly Mock<IFileHandlerService> _fileHandlerServiceMock;
    GetAttachmentQuery query;
    public GetAttachmentQueryHandlerTests()
    {
        _attachmentRepositoryMock = new();
        _fileHandlerServiceMock = new();
        query = new GetAttachmentQuery { AttachmentId = 3 }; //Random attachment query
    }

    [Fact]
    public async Task Handle_Should_ReturnError_IfAttachmentIsntFound()
    {
        //Arrange
        _attachmentRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Attachment, bool>>>()))
            .ReturnsAsync((Attachment)null);

        var handler = new GetAttachmentQueryHandler(_attachmentRepositoryMock.Object, _fileHandlerServiceMock.Object);
        //Act
        var result = await handler.Handle(query, default);

        //Assert
        result.IsSuccess.ShouldBeFalse();
        result.Errors.ShouldContain("Attachment not found");
        result.Payload.ShouldBeNull();
    }

    [Fact]
    public async Task Handle_Should_ReturnError_IfFileIsntFound()
    {
        //Arrange
        var attachmentFake = new Attachment();

        _attachmentRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Attachment, bool>>>()))
            .ReturnsAsync(attachmentFake);

        _fileHandlerServiceMock
            .Setup(service => service.GetFile(It.IsAny<string>()))
            .Returns(new FileDto());

        var handler = new GetAttachmentQueryHandler(_attachmentRepositoryMock.Object, _fileHandlerServiceMock.Object);
        //Act
        var result = await handler.Handle(query, default);

        //Assert
        result.IsSuccess.ShouldBeFalse();
        result.Errors.ShouldContain("File not found");
        result.Payload.ShouldBeNull();
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_IfTheOtherTestsPass()
    {
        //Arrange
        var attachmentFake = new Attachment();

        var fileDtoFake = new FileDto
        {
            FileName = "fake file"
        };
        _attachmentRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Attachment, bool>>>()))
            .ReturnsAsync(attachmentFake);

        _fileHandlerServiceMock
            .Setup(service => service.GetFile(It.IsAny<string>()))
            .Returns(fileDtoFake);

        var handler = new GetAttachmentQueryHandler(_attachmentRepositoryMock.Object, _fileHandlerServiceMock.Object);
        //Act
        var result = await handler.Handle(query, default);

        //Assert
        result.IsSuccess.ShouldBeTrue();
        result.Payload.ShouldNotBeNull();
        result.Payload.ShouldBeOfType<FileDto>();
    }

}
