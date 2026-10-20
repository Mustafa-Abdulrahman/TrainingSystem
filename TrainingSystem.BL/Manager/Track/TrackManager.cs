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
            TrackCourses = dto.Courses.Select(x => new TrackCourse
            {
                CourseId = x.CourseId,
                Hours = x.Hours,
            }).ToList()
        };
  

        _unitOfWork.TrackRepo.Add(track);
        await _unitOfWork.SaveChangesAsync();
       

        return new OperationResult { Succeeded = true, };

    }

    public async Task<OperationResult> DeleteAsync(int id)
    {
        var trackToDelete = await _unitOfWork.TrackRepo.GetByIdAsync(id);
        var hasStudents = await _unitOfWork.TrackRepo.HasStudentsAsync(id);
        if (trackToDelete is null)
        {
            return new OperationResult
            {
                Succeeded = false,
                Error = $"Track With Id {id} Not Found"
            };
        }
        if (hasStudents)
        {
            return new OperationResult
            {
                Succeeded = false,
                Error = $"Track With Id {id} Has Students, Cannot Delete It"
            };
        }

        _unitOfWork.TrackRepo.Delete(trackToDelete);
        await _unitOfWork.SaveChangesAsync();

        return new OperationResult { Succeeded = true };
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

   

    public async Task<List<LookupDto>> GetIntakesLookupAsync()
    {
        var intakes = await _unitOfWork.IntakeRepo.GetAllAsync();

        var result = intakes.Select(x => new LookupDto { Id = x.Id, Name = x.Name}).ToList();
        return result;
    }

    public async Task<TrackSaveDto?> GetForEditAsync(int id)
    {
        var track = await _unitOfWork.TrackRepo.GetByIdAsync(id);
        if (track is null)
            return null;

        var trackCourses = await _unitOfWork.TrackRepo.GetTrackCoursesAsync(id);

        var result = new TrackSaveDto
        {
            Id = track.Id,
            Name = track.Name,
            Description = track.Description,
            IntakeId = track.IntakeId,
            BranchId = track.BranchId,
            Courses = trackCourses.Select(x => new TrackCourseSaveDto
            {
                CourseId = x.CourseId,
                Hours = x.Hours
            }).ToList()
        };

        return result;

    }

        public async Task<OperationResult> UpdateAsync(TrackSaveDto dto)
        {
            var track = await _unitOfWork.TrackRepo.GetByIdAsync(dto.Id);

            if(track is null)
            {
                return new OperationResult
                {
                    Succeeded = false,
                    Error = $"Track With Id {dto.Id} Not Exists"
                };
            }
            var checkIfExist = await _unitOfWork.TrackRepo.ExistsAsync(dto.IntakeId, dto.BranchId, dto.Name, dto.Id);
            if (checkIfExist)
            {
                return new OperationResult
                {
                    Succeeded = false,
                    Error = $"Track With Name {dto.Name} Already Exists"
                };
            }
            track.Name = dto.Name;
            track.Description = dto.Description;
            track.IntakeId = dto.IntakeId;
            track.BranchId = dto.BranchId;

            var exitingCourse = await _unitOfWork.TrackRepo.GetTrackCoursesAsync(track.Id);
            var incomingCourse = dto.Courses;

            Dictionary<int, TrackCourse> exitingCourseDict = exitingCourse.ToDictionary(x => x.CourseId, x => x);
            foreach (var course in incomingCourse)
            {
                if (exitingCourseDict.ContainsKey(course.CourseId))
                {
                    // Update existing course
                    exitingCourseDict[course.CourseId].Hours = course.Hours;
                    exitingCourseDict.Remove(course.CourseId);
                }
                else
                {
                    // Add new course
                    track.TrackCourses.Add(new TrackCourse
                    {
                        CourseId = course.CourseId,
                        Hours = course.Hours
                    });
                }
            }
            foreach (var course in exitingCourseDict.Values)
                {
                    // Remove courses that are not in the incoming list
                    track.TrackCourses.Remove(course);
                    _unitOfWork.TrackCourseRepo.Delete(course);
            }

            _unitOfWork.TrackRepo.Update(track);
            await _unitOfWork.SaveChangesAsync();
            return new OperationResult { Succeeded = true };
        }
}
