using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Slacker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Infrastructure.DbConfigs;
public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        //Properties
        builder.Property(p => p.Message).HasColumnType("ntext").HasMaxLength(1500);
        builder.Property(p => p.CreatedAt).HasColumnType("datetime2");

        //Entity
        builder.HasQueryFilter(p => p.isActive == true);
        builder.HasKey(p => p.Id);
        builder.HasOne(p => p.Connection).WithMany(c => c.Posts).HasForeignKey(p => p.ConnectionId);
        builder.HasOne(p => p.Employee).WithMany(p => p.Posts).HasForeignKey(p => p.EmployeeId);
        builder.HasMany(p => p.Attachments).WithOne(a => a.Post).HasForeignKey(a => a.PostId);
        builder.HasMany(p => p.Reactions).WithOne(r => r.Post).HasForeignKey(r => r.PostId);
        builder.HasMany(p => p.ChildPosts).WithOne(p => p.ParentPost).HasForeignKey(p => p.ParentPostId).OnDelete(DeleteBehavior.NoAction);
    }
}
