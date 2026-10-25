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

    [HttpDelete]
    public IActionResult Delete(int id)
    {
        trackManager.DeleteAsync(id);
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
        var courses = await trackManager.GetCoursesLookupAsync();
        ViewBag.Courses = courses;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(TrackSaveDto model)
    {
        model.Courses = model.Courses.Where(c => c.isSelected).ToList();

        if (ModelState.IsValid)
        {
            TempData["SuccessMessage"] = "Track created successfully.";
            var result =  await trackManager.CreateAsync(model);
             return RedirectToAction("index");
        }
        TempData["FailedToAdd"] ="Error occurred while adding the track.";

        var intakes = await trackManager.GetIntakesLookupAsync();
        ViewBag.Intakes = new SelectList(intakes, "Id", "Name");
        var branches = await trackManager.GetBranchesLookupAsync();
        ViewBag.Branches = new SelectList(branches, "Id", "Name");
        var courses = await trackManager.GetCoursesLookupAsync();
        ViewBag.Courses = courses;
        return View(model);

    }
}
