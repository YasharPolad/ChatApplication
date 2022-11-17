using Microsoft.AspNetCore.Http;
using Slacker.Application.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Application.Interfaces;
public interface IFileHandlerService
{
    Task<string> SaveFile(IFormFile file); //Save file to the root, return file path
    FileDto GetFile(string path);
}
