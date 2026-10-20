using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingSystem;

public class Track
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int IntakeId { get; set; }
    public Intake Intake { get; set; } = null!;
    public int BranchId { get; set; }
    public Branch Branch { get; set; } = null!;
    public ICollection<TrackCourse> TrackCourses { get; set; } = new List<TrackCourse>();
    public ICollection<Student> Students { get; set; } = new List<Student>();

}
