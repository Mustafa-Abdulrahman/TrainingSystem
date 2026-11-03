using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TrainingSystem.Controllers;

public class TrackController: Controller
{
    private readonly ITrackManager trackManager;

    public TrackController(ITrackManager _trackManager)
    {
        trackManager = _trackManager;
    }


    public async Task<IActionResult> index()
    {
        var tracks =await trackManager.GetAllAsync();
        return View(tracks);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
       var result = await trackManager.DeleteAsync(id);
        if (result.Succeeded)
        {
            TempData["SuccessMessageDeleted"] = "Track deleted successfully.";
        }
        else
        {
            TempData["ErrorMessage"] = result.Error;
        }
        return RedirectToAction("index");
    }


    public async Task<IActionResult> Details(int id)
    {
        var trackDetails = await trackManager.GetDetailsAsync(id);
        return View(trackDetails);
    }


    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var intakes = await trackManager.GetIntakesLookupAsync();
        ViewBag.Intakes = new SelectList(intakes, "Id", "Name");
        var branches = await trackManager.GetBranchesLookupAsync();
        ViewBag.Branches = new SelectList(branches, "Id", "Name");
        //var courses = await trackManager.GetCoursesLookupAsync();
        //ViewBag.Courses = courses;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TrackSaveDto model)
    {
        //model.Courses = model.Courses.Where(c => c.isSelected).ToList();
        var intakes = await trackManager.GetIntakesLookupAsync();
        ViewBag.Intakes = new SelectList(intakes, "Id", "Name");
        var branches = await trackManager.GetBranchesLookupAsync();
        ViewBag.Branches = new SelectList(branches, "Id", "Name");
        //var courses = await trackManager.GetCoursesLookupAsync();

        if (ModelState.IsValid)
        {
            var result =  await trackManager.CreateAsync(model);
            if (result.Succeeded) {
            TempData["SuccessMessage"] = "Track created successfully.";
             return RedirectToAction("index");
            }
            else
            ModelState.AddModelError("", result.Error);
            return View(model);
        }
        TempData["FailedToAdd"] ="Error occurred while adding the track.";

       
        //ViewBag.Courses = courses;
        return View(model);

    }


    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var intakes = await trackManager.GetIntakesLookupAsync();
        ViewBag.Intakes = new SelectList(intakes, "Id", "Name");
        var branches = await trackManager.GetBranchesLookupAsync();
        ViewBag.Branches = new SelectList(branches, "Id", "Name");
        var Othercourses = await trackManager.GetCoursesNotInTrackAsync(id);
        ViewBag.OtherCourses = new SelectList(Othercourses, "Id", "Name");
        var Trackcourses = await trackManager.GetCoursesInTrackAsync(id);
        ViewBag.TrackCourses = Trackcourses;

        var model = await trackManager.GetForEditAsync(id);
        if(model is null)
        {
            TempData["ErrorMessageGetForEdit"] = $"Track with Id {id} not found.";
            return RedirectToAction("index");
        }
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(TrackSaveDto model)
    {
        var intakes = await trackManager.GetIntakesLookupAsync();
        ViewBag.Intakes = new SelectList(intakes, "Id", "Name");
        var branches = await trackManager.GetBranchesLookupAsync();
        ViewBag.Branches = new SelectList(branches, "Id", "Name");
        var Othercourses = await trackManager.GetCoursesNotInTrackAsync(model.Id);
        ViewBag.OtherCourses = new SelectList(Othercourses, "Id", "Name");
        var Trackcourses = await trackManager.GetCoursesInTrackAsync(model.Id);
        ViewBag.TrackCourses = Trackcourses;
        if(ModelState.IsValid)
        {
            var result = await trackManager.UpdateAsync(model);
            if (result.Succeeded)
            {
                TempData["SuccessMessageUpdated"] = "Track updated successfully.";
                return RedirectToAction("index");
            }
            else
            {
                ModelState.AddModelError("", result.Error);
                return View(model);
            }
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddTrackCourse(int Id, List<int> courseIds)
    {
        if (courseIds.Any())
        {
            var intakes = await trackManager.GetIntakesLookupAsync();
            ViewBag.Intakes = new SelectList(intakes, "Id", "Name");
            var branches = await trackManager.GetBranchesLookupAsync();
            ViewBag.Branches = new SelectList(branches, "Id", "Name");
            var Othercourses = await trackManager.GetCoursesNotInTrackAsync(Id);
            ViewBag.OtherCourses = new SelectList(Othercourses, "Id", "Name");
            var Trackcourses = await trackManager.GetCoursesInTrackAsync(Id);
            ViewBag.TrackCourses = Trackcourses;
            for (int i=0;i< courseIds.Count; i++)
            {
                var trackCourseDto = new TrackSaveDto
                {
                    Id = Id,
                    Courses = new List<TrackCourseSaveDto>
                    {
                        new TrackCourseSaveDto
                        {
                            CourseId = courseIds[i],
                            Hours = 3 // Set the hours as needed
                        }
                    }
                };
                var result =await trackManager.CreateNewTrackCourse(trackCourseDto);
                if (!result.Succeeded)
                {

                    TempData["ErrorMessage"] = result.Error;
                    return RedirectToAction("Update", new { id = Id });
                }
            }
        }
        
        return RedirectToAction("Update", new { id = Id });
    }
}
