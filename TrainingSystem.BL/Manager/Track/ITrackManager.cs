using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingSystem;

public interface ITrackManager
{
    public Task<List<TrackDto>> GetAllAsync();
    public Task<TrackDetailsDto?> GetDetailsAsync(int id);
    public Task<TrackSaveDto?> GetForEditAsync(int id);
    public Task<OperationResult> CreateAsync(TrackSaveDto dto);
    public Task<OperationResult> UpdateAsync(TrackSaveDto dto);
    public Task<OperationResult> DeleteAsync(int id);
    public Task<List<LookupDto>> GetBranchesLookupAsync();
    public Task<List<LookupDto>> GetIntakesLookupAsync();
    public Task<List<LookupDto>> GetCoursesLookupAsync();
}
