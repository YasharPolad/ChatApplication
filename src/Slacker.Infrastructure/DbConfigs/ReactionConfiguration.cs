using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Slacker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Infrastructure.DbConfigs;
public class ReactionConfiguration : IEntityTypeConfiguration<Reaction>
{
    public void Configure(EntityTypeBuilder<Reaction> builder)
    {
        //Entity
        builder.HasKey(r => r.Id);
        builder.HasOne(r => r.Post).WithMany(p => p.Reactions).HasForeignKey(r => r.PostId);
    }
}
