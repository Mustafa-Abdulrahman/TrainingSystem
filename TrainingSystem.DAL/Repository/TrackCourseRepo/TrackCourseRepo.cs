using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingSystem;

public class TrackCourseRepo : GenericRepo<TrackCourse>, ITrackCourseRepo
{
    private readonly AppDbContext context;
    public TrackCourseRepo(AppDbContext _context):base(_context)
    {
        context = _context;
    }
   
}
