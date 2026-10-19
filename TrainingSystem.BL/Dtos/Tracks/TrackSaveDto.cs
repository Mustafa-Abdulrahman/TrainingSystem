using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingSystem;

public class TrackSaveDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int IntakeId { get; set; }

    public int BranchId { get; set; }
    public List<TrackCourseSaveDto> Courses { get; set; }
}
