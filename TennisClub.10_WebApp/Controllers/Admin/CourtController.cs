using BusinessLogicLayer.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TennisClub_0._1.Models;
using TennisClub_0._1.Requests;
using Court = BusinessLogicLayer.Models.Court;

namespace TennisClub_0._1.Controllers.Admin;

[Authorize(Roles = "Admin")]
[Area("Admin")]
public class CourtController : Controller
{
    private readonly ICourtService _courtService;

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

        List<CourtViewModel> courtViewModels = new();
        foreach (Court court in courts)
        {
            courtViewModels.Add(new CourtViewModel
            {
                Id = court.Id,
                Double = court.Double,
                Indoor = court.Indoor,
                Number = court.Number,
            });
        }

        return View(courtViewModels);
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

        CourtViewModel courtViewModel = new()
        {
            Id = court.Id,
            Double = court.Double,
            Indoor = court.Indoor,
            Number = court.Number,
        };

        return View(courtViewModel);
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

        Court court = new()
        {
            Double = courtRequest.Double,
            Indoor = courtRequest.Indoor,
            Number = courtRequest.Number,
        };
        if (!_courtService.Create(court))
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

        CourtViewModel courtViewModel = new()
        {
            Id = court.Id,
            Double = court.Double,
            Indoor = court.Indoor,
            Number = court.Number,
        };

        return View(courtViewModel);
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

        Court court = new()
        {
            Double = courtRequest.Double,
            Indoor = courtRequest.Indoor,
            Number = courtRequest.Number,
        };
        if (!_courtService.Edit(id, court))
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

        CourtViewModel courtViewModel = new()
        {
            Id = court.Id,
            Double = court.Double,
            Indoor = court.Indoor,
            Number = court.Number,
        };

        return View(courtViewModel);
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