using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingSystem;

public class IntakeConfig : IEntityTypeConfiguration<Intake>
{
    public void Configure(EntityTypeBuilder<Intake> builder)
    {
        builder.Property(x => x.Name).IsRequired();
        builder.HasIndex(x => new {x.Name ,x.TrainingProgramId}).IsUnique();
        builder.HasOne(x=>x.TrainingProgram).WithMany(p=>p.Intakes).HasForeignKey(x=>x.TrainingProgramId).OnDelete(DeleteBehavior.Restrict);
    }
}
