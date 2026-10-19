using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingSystem;

public class BranchConfig : IEntityTypeConfiguration<Branch>
{
    public void Configure(EntityTypeBuilder<Branch> builder)
    {
        builder.HasIndex(x => x.Name).IsUnique();
        builder.Property(x => x.Name).HasColumnType("varchar(100)");
        builder.Property(x => x.City).HasColumnType("varchar(100)");
        builder.Property(x => x.Adress).HasColumnType("varchar(200)");
    }
}
