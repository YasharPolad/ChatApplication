using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using SendGrid;
using Slacker.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Infrastructure.Services;
public class FileHandlerService : IFileHandlerService
{
    private readonly IWebHostEnvironment _env;
 
    public FileHandlerService(IWebHostEnvironment env)
    {
        _env = env;
    }

    public async Task<string> SaveFile(IFormFile file)
    {
        try
        {
            if (file.Length != 0)
            {
                var baseDirectory = Path.Combine(_env.WebRootPath, "Uploads"); //TODO: Maybe user files should be stored outside of the project
                Directory.CreateDirectory(baseDirectory);

                var subdirectory = DirectoryFromMimeType(file.FileName);
                var fileDirectory = Path.Combine(baseDirectory, subdirectory); 
                Directory.CreateDirectory(fileDirectory);
                
                var filePath = Path.Combine(fileDirectory, file.FileName);
                
                using (var filestream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(filestream);
                }
                return filePath;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Coulnd't save the file", ex);
        }
    }

    //For example if the file is .png, it should be saved under Uploads/image/png 
    private string DirectoryFromMimeType(string fileName)
    {
        var typeProvider = new FileExtensionContentTypeProvider(); //TODO: I shouldn't new this here 
        
        if (!typeProvider.TryGetContentType(fileName, out var contentType))
            return "unknown-type";
        
        var mimeArray = contentType.Split("/");
        return Path.Combine(mimeArray[0], mimeArray[1]);
    }

   
}
