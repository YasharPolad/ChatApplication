using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Slacker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Infrastructure.DbConfigs;
public class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
{
    public void Configure(EntityTypeBuilder<Attachment> builder)
    {
        //Property
        builder.Property(a => a.FileName).HasMaxLength(250);

        //Entity
        builder.HasKey(a => a.Id);
        builder.HasOne(a => a.Post).WithMany(p => p.Attachments).HasForeignKey(a => a.PostId);
    }
}
