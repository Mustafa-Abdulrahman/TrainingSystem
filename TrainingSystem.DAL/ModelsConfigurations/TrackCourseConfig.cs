using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingSystem;

public class TrackCourseConfig : IEntityTypeConfiguration<TrackCourse>
{
    public void Configure(EntityTypeBuilder<TrackCourse> builder)
    {
        builder.HasKey(x => new { x.TrackId, x.CourseId });
        builder.Property(x => x.Hours).IsRequired();
        builder.ToTable(x => x.HasCheckConstraint(
            "HoursBiggerZero",
            "[Hours]>0")
        );

        builder.HasOne(x=>x.Track).WithMany(y=>y.TrackCourses).HasForeignKey(x=>x.TrackId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x=>x.Course).WithMany(y=>y.TrackCourses).HasForeignKey(x=>x.CourseId).OnDelete(DeleteBehavior.Cascade);

    }
}
