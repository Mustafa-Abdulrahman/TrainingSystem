using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingSystem;

public class TrainingProgram
{
    public int Id {  get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int DurationInMonth { get; set; }
    public ICollection<Intake> Intakes { get; set; } = new List<Intake>();
}
