using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingSystem;

public class TrackCourse
{
    public int CourseId { get; set; }
    public Course Course { get; set; } = null!;
    public int TrackId { get; set; }
    public Track Track { get; set; } = null!;
    public int Hours { get; set; }
}
