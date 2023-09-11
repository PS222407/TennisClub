using BusinessLogicLayer.Interfaces;
using DataLayer.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TennisClub_0._1.Models;
using TennisClub_0._1.Requests;

namespace TennisClub_0._1.Controllers.Admin;

[Authorize(Roles = "Admin")]
[Area("Admin")]
public class TournamentController : Controller
{
    private readonly ITournamentService _tournamentService;

    private readonly ICourtService _courtService;

    public TournamentController(ITournamentService tournamentService, ICourtService courtService)
    {
        _tournamentService = tournamentService;
        _courtService = courtService;
    }

    // GET: Tournaments
    public ActionResult Index()
    {
        List<TournamentDto>? tournamentDtos = _tournamentService.GetAll();
        if (tournamentDtos == null)
        {
            TempData["Message"] = "Fout tijdens het ophalen van data.";
            TempData["MessageType"] = "danger";

            return View(new List<TournamentViewModel>());
        }

        List<TournamentViewModel> tournamentViewModels = new List<TournamentViewModel>();
        foreach (TournamentDto tournamentDto in tournamentDtos)
        {
            tournamentViewModels.Add(new TournamentViewModel
            {
                Id = tournamentDto.Id,
                Name = tournamentDto.Name,
                Description = tournamentDto.Description,
                Price = tournamentDto.Price,
                MaxMembers = tournamentDto.MaxMembers,
                StartDateTime = tournamentDto.StartDateTime,
            });
        }

        return View(tournamentViewModels);
    }

    // GET: Tournaments/Details/5
    public ActionResult Details(int id)
    {
        TournamentDto? tournamentDto = _tournamentService.FindById(id);
        if (tournamentDto == null)
        {
            TempData["Message"] = "Fout tijdens het ophalen van data.";
            TempData["MessageType"] = "danger";

            return View();
        }

        TournamentViewModel tournamentViewModel = new TournamentViewModel
        {
            Id = tournamentDto.Id,
            Name = tournamentDto.Name,
            Description = tournamentDto.Description,
            Price = tournamentDto.Price,
            MaxMembers = tournamentDto.MaxMembers,
            StartDateTime = tournamentDto.StartDateTime,
        };

        return View(tournamentViewModel);
    }

    // GET: Tournaments/Create
    public ActionResult Create()
    {
        List<CourtDto>? courtDtos = _courtService.GetAll();

        TournamentViewModel tournamentViewModel = new TournamentViewModel
        {
            CourtOptions = courtDtos?.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Number.ToString(),
            }).ToList(),
        };

        return View(tournamentViewModel);
    }

    // POST: Tournaments/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(TournamentRequest tournamentRequest)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        TournamentDto tournamentDto = new TournamentDto
        {
            Name = tournamentRequest.Name,
            Description = tournamentRequest.Description,
            Price = tournamentRequest.Price,
            MaxMembers = tournamentRequest.MaxMembers,
            StartDateTime = tournamentRequest.StartDateTime,
            CourtIds = tournamentRequest.SelectedCourtIds,
        };
        if (!_tournamentService.Create(tournamentDto))
        {
            TempData["Message"] = "Fout tijdens het aanmaken.";
            TempData["MessageType"] = "danger";
            return View();
        }

        TempData["Message"] = "Item succesvol aangemaakt";
        TempData["MessageType"] = "success";

        return RedirectToAction(nameof(Index));
    }

    // GET: Tournaments/Edit/5
    public ActionResult Edit(int id)
    {
        TournamentDto? tournamentDto = _tournamentService.FindById(id);
        if (tournamentDto == null)
        {
            TempData["Message"] = "Fout tijdens het ophalen van data.";
            TempData["MessageType"] = "danger";

            return View();
        }

        List<CourtDto>? courtDtos = _courtService.GetAll();

        TournamentViewModel tournamentViewModel = new TournamentViewModel()
        {
            Id = tournamentDto.Id,
            Name = tournamentDto.Name,
            Description = tournamentDto.Description,
            Price = tournamentDto.Price,
            MaxMembers = tournamentDto.MaxMembers,
            StartDateTime = tournamentDto.StartDateTime,
            CourtOptions = courtDtos?.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Number.ToString(),
            }).ToList(),
            SelectedCourtIds = tournamentDto.CourtIds,
        };

        return View(tournamentViewModel);
    }

    // POST: Tournaments/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, TournamentRequest tournamentRequest)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        TournamentDto tournamentDto = new TournamentDto
        {
            Name = tournamentRequest.Name,
            Description = tournamentRequest.Description,
            Price = tournamentRequest.Price,
            MaxMembers = tournamentRequest.MaxMembers,
            StartDateTime = tournamentRequest.StartDateTime,
        };
        if (!_tournamentService.Edit(id, tournamentDto))
        {
            TempData["Message"] = "Fout tijdens het opslaan van de data.";
            TempData["MessageType"] = "danger";

            return View();
        }

        TempData["Message"] = "Item succesvol gewijzigd";
        TempData["MessageType"] = "success";

        return RedirectToAction(nameof(Index));
    }

    // GET: Tournaments/Delete/5
    public ActionResult Delete(int id)
    {
        TournamentDto? tournamentDto = _tournamentService.FindById(id);
        if (tournamentDto == null)
        {
            TempData["Message"] = "Fout tijdens het ophalen van de data.";
            TempData["MessageType"] = "danger";

            return View();
        }

        TournamentViewModel tournamentViewModel = new TournamentViewModel
        {
            Id = tournamentDto.Id,
            Name = tournamentDto.Name,
            Description = tournamentDto.Description,
            Price = tournamentDto.Price,
            MaxMembers = tournamentDto.MaxMembers,
            StartDateTime = tournamentDto.StartDateTime,
        };

        return View(tournamentViewModel);
    }

    // POST: Tournaments/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Destroy(int id)
    {
        if (!_tournamentService.Delete(id))
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