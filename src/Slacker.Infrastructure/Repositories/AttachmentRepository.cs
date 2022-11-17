using SendGrid.Helpers.Mail;
using Slacker.Application.Interfaces.RepositoryInterfaces;
using Slacker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Attachment = Slacker.Domain.Entities.Attachment;

namespace Slacker.Infrastructure.Repositories;
public class AttachmentRepository : GenericRepository<Attachment, SlackerDbContext>, IAttachmentRepository
{
    public AttachmentRepository(SlackerDbContext context) : base(context)
    {
    }
}
