using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TrainingSystem;

public class Student
{
    public int Id { get; set; }
    public string Fname { get; set; } =string.Empty;
    public string Mname { get; set; } = string.Empty;
    public string Lname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    [StringLength(11,MinimumLength = 11)]
    public string Phone {  get; set; } = string.Empty;
    [StringLength(14,MinimumLength = 14)]
    public string NationalId { get; set; } = string.Empty;

    public string? Image {  get; set; }
    public DateTime DateOfBirth { get; set; }
  
    public int TrackId { get; set; }
    public Track Track { get; set; } = null!;
}
