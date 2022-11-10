using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Slacker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Infrastructure.DbConfigs;
public class ConnectionConfiguration : IEntityTypeConfiguration<Connection>
{
    public void Configure(EntityTypeBuilder<Connection> builder)
    {
        //Property Config

        builder.Property(p => p.Name).HasMaxLength(50);

        //Entity Config

        builder.HasKey(p => p.Id);
        builder.HasMany(c => c.Employees).WithMany(e => e.Connections);
        builder.HasMany(c => c.Posts).WithOne(p => p.Connection).HasForeignKey(p => p.ConnectionId);

    }
}
