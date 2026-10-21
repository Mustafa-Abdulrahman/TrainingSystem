using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingSystem;

public class StudentConfig : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.Property(x => x.Fname).IsRequired().HasColumnType("varchar(100)");
        builder.Property(x => x.Mname).IsRequired().HasColumnType("varchar(100)");
        builder.Property(x => x.Lname).IsRequired().HasColumnType("varchar(100)");
        builder.Property(x => x.Email).IsRequired();
        builder.Property(x => x.Phone).IsRequired();
        builder.Property(x => x.NationalId).IsRequired();
        builder.HasOne(x => x.Track).WithMany(y => y.Students).HasForeignKey(x => x.TrackId).OnDelete(DeleteBehavior.Restrict);
    }
}
