using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingSystem;

public class Branch
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? City { get; set; }
    public string? Adress { get; set; }
    public ICollection<Student> Students { get; set; } = new List<Student>();
    public ICollection<Track> Tracks { get; set; } = new List<Track>();

}
