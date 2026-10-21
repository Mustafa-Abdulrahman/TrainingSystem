using Microsoft.AspNetCore.Mvc;

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


    public IActionResult Delete(int id)
    {
        trackManager.DeleteAsync(id);
        return RedirectToAction("index");
    }


    public async Task<IActionResult> Details(int id)
    {
        var trackDetails =await trackManager.GetDetailsAsync(id);
        return View(trackDetails);

    }



    public IActionResult Create()
    {
        return View();
    }
}
