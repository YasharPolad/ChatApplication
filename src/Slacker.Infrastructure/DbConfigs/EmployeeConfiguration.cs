﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Slacker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Infrastructure.DbConfigs;
public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        //Properties
        builder.Property(b => b.FullName).HasMaxLength(50);
        builder.Property(b => b.SigalRId).HasMaxLength(50);

        //Entity
        builder.HasKey(b => b.Id);
        builder.HasMany(b => b.Connections).WithMany(c => c.Employees);
        builder.HasMany(b => b.Posts).WithOne(p => p.Employee).HasForeignKey(p => p.EmployeeId);
    }
}
