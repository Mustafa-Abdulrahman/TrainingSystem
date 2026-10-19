using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingSystem;

public class CourseRepo : GenericRepo<Course>, ICourseRepo
{
    private readonly AppDbContext context;
    public CourseRepo(AppDbContext _context) : base(_context)
    {
        context = _context;
    }
}
