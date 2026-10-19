using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingSystem;

public interface IUnitOfWork
{
    public ITrackRepo TrackRepo { get; }
    public IIntakeRepo IntakeRepo { get; }
    public ICourseRepo CoursesRepo { get; }
    public IBranchRepo BranchRepo { get; }
    public ITrackCourseRepo TrackCourseRepo { get; }
    public Task<int> SaveChangesAsync();
}
    