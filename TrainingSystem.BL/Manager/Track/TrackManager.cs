using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace TrainingSystem;

public class TrackManager : ITrackManager
{
    private readonly IUnitOfWork _unitOfWork;
    public TrackManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult> CreateAsync(TrackSaveDto dto)
    {
        var checkExist = await _unitOfWork.TrackRepo.ExistsAsync(dto.IntakeId, dto.BranchId, dto.Name, null);
        if (checkExist)
        {
            return new OperationResult
            {
                Succeeded = false,
                Error = "Track Already Exists"
            };
        }

        var track = new Track
        {
            Name = dto.Name,
            Description = dto.Description,
            IntakeId = dto.IntakeId,
            BranchId = dto.BranchId,
        };

        foreach(var course in dto.Courses)
        {
            track.TrackCourses.Add(new TrackCourse
            {
                CourseId = course.CourseId,
                Hours = course.Hours,
            });
        }

        _unitOfWork.TrackRepo.Add(track);
        await _unitOfWork.SaveChangesAsync();
       

        return new OperationResult { Succeeded = true, };

    }

    public Task<OperationResult> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<TrackDto>> GetAllAsync()
    {
        var tracks = await _unitOfWork.TrackRepo.GetAllWithDetailsAsync();
        var result = tracks.Select(x => new TrackDto
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            IntakeId = x.IntakeId,
            IntakeName = x.Intake.Name,
            BranchId = x.BranchId,
            BranchName = x.Branch.Name
        }).ToList();
        return result;
    }

    public async Task<List<LookupDto>> GetBranchesLookupAsync()
    {
        var branchs = await _unitOfWork.BranchRepo.GetAllAsync();

        var result = branchs.Select(x => new LookupDto
        {
            Id = x.Id,
            Name = x.Name,
        }).ToList();

        return result;

    }

    public async Task<List<LookupDto>> GetCoursesLookupAsync()
    {
        var courses = await _unitOfWork.CoursesRepo.GetAllAsync();
        var result = courses.Select(x => new LookupDto
        {
            Id = x.Id,
            Name = x.Name,
        }).ToList();

        return result;
    }

    public async Task<TrackDetailsDto?> GetDetailsAsync(int id)
    {
        var track = await _unitOfWork.TrackRepo.GetByIdWithDetailsAsync(id);
        if(track is null)
        {
            return null; 
        }

        var trackCourses = await _unitOfWork.TrackRepo.GetTrackCoursesAsync(id);
        var trackStudens = await _unitOfWork.TrackRepo.GetTrackStudentsAsync(id);

        var result = new TrackDetailsDto
        {
            Id = track.Id,
            Name = track.Name,
            Description = track.Description,
            IntakeId = track.IntakeId,
            IntakeName = track.Intake.Name,
            BranchId = track.BranchId,
            BranchName = track.Branch.Name,
            Courses = trackCourses.Select(x => new TrackCourseDto { CourseId = x.CourseId,CourseName = x.Course.Name, Hours = x.Hours }).ToList(),
            Students = trackStudens.Select(x=> new TrackStudentDto { FullName = $"{x.Fname} {x.Mname} {x.Lname}", Email=x.Email, StudentId=x.Id}).ToList()
        };

        return result;


    }

    public Task<TrackSaveDto?> GetForEditAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<LookupDto>> GetIntakesLookupAsync()
    {
        var intakes = await _unitOfWork.IntakeRepo.GetAllAsync();

        var result = intakes.Select(x => new LookupDto { Id = x.Id, Name = x.Name}).ToList();
        return result;
    }

    public Task<OperationResult> UpdateAsync(TrackSaveDto dto)
    {
        throw new NotImplementedException();
    }
}
