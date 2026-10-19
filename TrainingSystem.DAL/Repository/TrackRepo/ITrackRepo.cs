using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingSystem;

public interface ITrackRepo:IGenericRepo<Track>
{
    public Task<List<Track>> GetAllWithDetailsAsync();
    public Task<Track?> GetByIdWithDetailsAsync(int id);
    public Task<List<TrackCourse>> GetTrackCoursesAsync(int trackId);
    public Task<List<Student>> GetTrackStudentsAsync(int trackId);

    public Task<bool> ExistsAsync(int intakeId, int branchId, string name, int? excludeId);
    public Task<bool> HasStudentsAsync(int id);

}
