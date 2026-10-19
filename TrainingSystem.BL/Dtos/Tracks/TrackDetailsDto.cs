using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingSystem;

public class TrackDetailsDto:TrackDto
{
    public List<TrackCourseDto> Courses { get; set; } = new List<TrackCourseDto>();
    public List<TrackStudentDto> Students { get; set; } = new List<TrackStudentDto>();
}
