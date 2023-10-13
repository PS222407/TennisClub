using BusinessLogicLayer.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TennisClub_0._1.Requests;
using TennisClub_0._1.Services;
using TennisClub_0._1.ViewModels;
using Court = BusinessLogicLayer.Models.Court;

namespace TennisClub_0._1.Controllers.Admin;

[Authorize(Roles = "Admin")]
[Area("Admin")]
public class CourtController : Controller
{
    private readonly ICourtService _courtService;
    private readonly CourtTransformer _courtTransformer = new();

    public CourtController(ICourtService courtService)
    {
        _courtService = courtService;
    }

    // GET: Courts
    public ActionResult Index()
    {
        List<Court>? courts = _courtService.GetAll();
        if (courts == null)
        {
            TempData["Message"] = "Fout tijdens het ophalen van data.";
            TempData["MessageType"] = "danger";

            return View(new List<CourtViewModel>());
        }

        return View(_courtTransformer.ModelsToViews(courts));
    }

    // GET: Courts/Details/5
    public ActionResult Details(int id)
    {
        Court? court = _courtService.FindById(id);
        if (court == null)
        {
            TempData["Message"] = "Fout tijdens het ophalen van data.";
            TempData["MessageType"] = "danger";

            return View();
        }

        return View(_courtTransformer.ModelToView(court));
    }

    // GET: Courts/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: Courts/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(CourtRequest courtRequest)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        
        if (!_courtService.Create(_courtTransformer.RequestToModel(courtRequest)))
        {
            TempData["Message"] = "Fout tijdens het aanmaken.";
            TempData["MessageType"] = "danger";
            return View();
        }

        TempData["Message"] = "Item succesvol aangemaakt";
        TempData["MessageType"] = "success";

        return RedirectToAction(nameof(Index));
    }

    // GET: Courts/Edit/5
    public ActionResult Edit(int id)
    {
        Court? court = _courtService.FindById(id);
        if (court == null)
        {
            TempData["Message"] = "Fout tijdens het ophalen van data.";
            TempData["MessageType"] = "danger";

            return View();
        }

        return View(_courtTransformer.ModelToView(court));
    }

    // POST: Courts/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, CourtRequest courtRequest)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        if (!_courtService.Edit(id, _courtTransformer.RequestToModel(courtRequest)))
        {
            TempData["Message"] = "Fout tijdens het opslaan van de data.";
            TempData["MessageType"] = "danger";

            return View();
        }

        TempData["Message"] = "Item succesvol gewijzigd";
        TempData["MessageType"] = "success";

        return RedirectToAction(nameof(Index));
    }

    // GET: Courts/Delete/5
    public ActionResult Delete(int id)
    {
        Court? court = _courtService.FindById(id);
        if (court == null)
        {
            TempData["Message"] = "Fout tijdens het ophalen van de data.";
            TempData["MessageType"] = "danger";

            return View();
        }

        return View(_courtTransformer.ModelToView(court));
    }

    // POST: Courts/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Destroy(int id)
    {
        if (!_courtService.Delete(id))
        {
            TempData["Message"] = "Fout tijdens het verwijderen van de data.";
            TempData["MessageType"] = "danger";

            return View("Delete");
        }

        TempData["Message"] = "Item succesvol verwijderd";
        TempData["MessageType"] = "success";

        return RedirectToAction(nameof(Index));
    }
}