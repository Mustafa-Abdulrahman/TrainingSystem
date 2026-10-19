using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingSystem
{
    internal class TrackConfig : IEntityTypeConfiguration<Track>
    {
        public void Configure(EntityTypeBuilder<Track> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasColumnType("varchar(250)");
            builder.Property(x => x.Description).IsRequired().HasColumnType("varchar(500)");
            builder.Property(x => x.Description).IsRequired().HasColumnType("varchar(500)");
            builder.HasOne(x => x.Intake).WithMany(i => i.Tracks).HasForeignKey(x => x.IntakeId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Branch).WithMany(i => i.Tracks).HasForeignKey(x => x.BranchId).OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new { x.IntakeId, x.BranchId, x.Name }).IsUnique(); ;
        }
    }
}
