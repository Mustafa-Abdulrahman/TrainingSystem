using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingSystem;

public class UnitOfWork : IUnitOfWork
{
    public ITrackRepo TrackRepo { get; }

    public IIntakeRepo IntakeRepo { get; }

    public ICourseRepo CoursesRepo { get; }

    public IBranchRepo BranchRepo { get; }

    public ITrackCourseRepo TrackCourseRepo { get; }

    private readonly AppDbContext _context;

    public UnitOfWork(ITrackRepo _TrackRepo, IIntakeRepo _IntakeRepo , ICourseRepo _CoursesRepo, IBranchRepo _BranchRepo, ITrackCourseRepo _TrackCourseRepo, AppDbContext context)
    {
        TrackRepo = _TrackRepo;
        IntakeRepo = _IntakeRepo;
        CoursesRepo = _CoursesRepo;
        BranchRepo = _BranchRepo;
        TrackCourseRepo = _TrackCourseRepo;
        _context = context;
    }
    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
