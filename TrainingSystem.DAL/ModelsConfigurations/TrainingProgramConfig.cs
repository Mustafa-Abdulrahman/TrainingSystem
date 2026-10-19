using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingSystem;

public class TrainingProgramConfig : IEntityTypeConfiguration<TrainingProgram>
{
    public void Configure(EntityTypeBuilder<TrainingProgram> builder)
    {
        builder.HasIndex(x => x.Name).IsUnique();
        builder.Property(x => x.Description).HasColumnType("varchar(100)");
        
    }
}
