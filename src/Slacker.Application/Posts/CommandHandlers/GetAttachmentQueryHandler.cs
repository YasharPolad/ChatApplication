using MediatR;
using Slacker.Application.Interfaces;
using Slacker.Application.Interfaces.RepositoryInterfaces;
using Slacker.Application.Models;
using Slacker.Application.Models.DTOs;
using Slacker.Application.Posts.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Application.Posts.CommandHandlers;
internal class GetAttachmentQueryHandler : IRequestHandler<GetAttachmentQuery, MediatrResult<FileDto>>
{
    private readonly IAttachmentRepository _attachmentRepository;
    private readonly IFileHandlerService _fileHandlerService;

    public GetAttachmentQueryHandler(IAttachmentRepository attachmentRepository, IFileHandlerService fileHandlerService)
    {
        _attachmentRepository = attachmentRepository;
        _fileHandlerService = fileHandlerService;
    }

    public async Task<MediatrResult<FileDto>> Handle(GetAttachmentQuery request, CancellationToken cancellationToken)
    {
        var result = new MediatrResult<FileDto>();

        var attachment = await _attachmentRepository.GetAsync(a => a.Id == request.AttachmentId);
        if(attachment is null)
        {
            result.IsSuccess = false;
            result.Errors.Add("File not found");
            return result;
        }

        var filePath = attachment.FilePath;
        var fileResult = _fileHandlerService.GetFile(filePath);

        if(fileResult is null)
        {
            result.IsSuccess = false;
            result.Errors.Add("File not found");
            return result;
        }

        result.IsSuccess = true;
        result.Payload = fileResult;
        return result;
    }
}
