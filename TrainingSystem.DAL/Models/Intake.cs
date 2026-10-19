using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingSystem;

public class Intake
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int TrainingProgramId { get; set; }
    public TrainingProgram TrainingProgram { get; set; } = null!;
    public ICollection<Student> Students { get; set; } = new List<Student>();
    public ICollection<Track> Tracks { get; set; } = new List<Track>();

}
