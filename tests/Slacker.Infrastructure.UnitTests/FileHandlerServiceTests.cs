using Microsoft.AspNetCore.Hosting;
using Moq;
using Shouldly;
using Slacker.Application.Models.DTOs;
using Slacker.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Infrastructure.UnitTests;
public class FileHandlerServiceTests : IDisposable
{
    private readonly Mock<IWebHostEnvironment> _envMock;
    string baseDirectory;
    public FileHandlerServiceTests()
    {
        _envMock = new();
        baseDirectory = @"..\..\";

        //Create test file
        var filePath = Path.Combine(baseDirectory, "fakeFile.txt");
        using (new FileStream(filePath, FileMode.Create))
        {
        }

    }

    public void Dispose()
    {
        File.Delete(Path.Combine(baseDirectory, "fakeFile.txt"));
    }

    [Fact]
    public async Task Service_Should_ReturnNull_IfFileDoesntExist()
    {
        //Arrange
        var service = new FileHandlerService(_envMock.Object);
        string path = Path.Combine(baseDirectory, "notexistingfile.png");

        //Act
        var result = service.GetFile(path);

        //Assert
        result.ShouldBeEquivalentTo(new FileDto());
    }

    [Fact]
    public async Task Service_Should_ReturnFileDto_IfFileExists()
    {
        //Arrange
        var service = new FileHandlerService(_envMock.Object);
        string path = Path.Combine(baseDirectory, "fakeFile.txt");

        //Act
        var result = service.GetFile(path);

        //Assert
        result.ShouldBeOfType(typeof(FileDto));
        result.FileName.ShouldBe("fakeFile.txt");
        result.FilePath.ShouldBeSameAs(path);
        result.ContentType.ShouldBe("text/plain");
    }
}
