using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingSystem;

public class TrackRepo : GenericRepo<Track>, ITrackRepo
{
    private readonly AppDbContext context;
    public TrackRepo(AppDbContext _context):base(_context)
    {
        context = _context;
    }
    public async Task<bool> ExistsAsync(int intakeId, int branchId, string name, int? excludeId)
    {
        return await context.Tracks.AnyAsync(x => x.IntakeId == intakeId && x.BranchId == branchId && x.Name == name && (!excludeId.HasValue || x.Id != excludeId.Value)
        );
    }

    public async Task<List<Track>> GetAllWithDetailsAsync()
    {
        return await context.Tracks.Include(x=>x.Intake).Include(x=>x.Branch).AsNoTracking().ToListAsync();
    }

    public async Task<Track?> GetByIdWithDetailsAsync(int id)
    {
        return await context.Tracks.Include(x => x.Intake).Include(x => x.Branch).AsNoTracking().FirstOrDefaultAsync(x=>x.Id == id);

    }

    public async Task<List<TrackCourse>> GetTrackCoursesAsync(int trackId)
    {
        return await context.TrackCourses.AsNoTracking().Where(x => x.TrackId == trackId).Include(x=>x.Course).ToListAsync(); 
    }

    public async Task<List<Student>> GetTrackStudentsAsync(int trackId)
    {
      return await context.Students.AsNoTracking().Where(x=>x.TrackId == trackId).ToListAsync(); 
    }

    public async Task<bool> HasStudentsAsync(int id)
    {
        return await context.Students.AnyAsync(x => x.TrackId == id);
    }
}
