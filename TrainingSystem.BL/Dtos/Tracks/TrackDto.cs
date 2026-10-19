using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingSystem;

public class TrackDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int IntakeId { get; set; } 
    public string IntakeName { get; set; } = string.Empty;
    public int BranchId { get; set; }
    public string BranchName { get; set; } = string.Empty;
}
