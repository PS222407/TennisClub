using BusinessLogicLayer.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TennisClub_0._1.Models;
using TennisClub_0._1.Requests;
using TennisClub_0._1.Services;
using Court = BusinessLogicLayer.Models.Court;
using Tournament = BusinessLogicLayer.Models.Tournament;

namespace TennisClub_0._1.Controllers.Admin;

[Authorize(Roles = "Admin")]
[Area("Admin")]
public class TournamentController : Controller
{
    private readonly ITournamentService _tournamentService;

    private readonly ICourtService _courtService;

    private readonly TournamentTransformer _tournamentTransformer = new();
    
    private readonly FileService _fileService = new();
    
    private readonly IWebHostEnvironment _webHostEnvironment;

    public TournamentController(ITournamentService tournamentService, ICourtService courtService, IWebHostEnvironment webHostEnvironment)
    {
        _tournamentService = tournamentService;
        _courtService = courtService;
        _webHostEnvironment = webHostEnvironment;
    }

    // GET: Tournaments
    public ActionResult Index()
    {
        List<Tournament>? tournamentDtos = _tournamentService.GetAll();
        if (tournamentDtos == null)
        {
            TempData["Message"] = "Fout tijdens het ophalen van data.";
            TempData["MessageType"] = "danger";

            return View(new List<TournamentViewModel>());
        }

        return View(_tournamentTransformer.ModelsToViews(tournamentDtos));
    }

    // GET: Tournaments/Details/5
    public ActionResult Details(int id)
    {
        Tournament? tournamentDto = _tournamentService.FindById(id);
        if (tournamentDto == null)
        {
            TempData["Message"] = "Fout tijdens het ophalen van data.";
            TempData["MessageType"] = "danger";

            return RedirectToAction(nameof(Index));
        }

        return View(_tournamentTransformer.ModelToView(tournamentDto));
    }

    // GET: Tournaments/Create
    public ActionResult Create()
    {
        List<Court>? courtDtos = _courtService.GetAll();

        TournamentRequest tournamentRequest = new()
        {
            CourtOptions = courtDtos?.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Number.ToString(),
            }).ToList(),
        };

        return View(tournamentRequest);
    }

    // POST: Tournaments/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(TournamentRequest tournamentRequest)
    {
        if (!ModelState.IsValid)
        {
            return View(tournamentRequest);
        }
        
        Tournament tournament = _tournamentTransformer.RequestToDto(tournamentRequest);
        tournament.ImageUrl = await _fileService.SaveImageAsync(tournamentRequest.Image, _webHostEnvironment);
            
        if (!_tournamentService.Create(tournament))
        {
            TempData["Message"] = "Fout tijdens het aanmaken.";
            TempData["MessageType"] = "danger";
            return View(tournamentRequest);
        }

        TempData["Message"] = "Item succesvol aangemaakt";
        TempData["MessageType"] = "success";

         return RedirectToAction(nameof(Index));
    }

    // GET: Tournaments/Edit/5
    public ActionResult Edit(int id)
    {
        Tournament? tournamentDto = _tournamentService.FindById(id);
        if (tournamentDto == null)
        {
            TempData["Message"] = "Fout tijdens het ophalen van data.";
            TempData["MessageType"] = "danger";

            return RedirectToAction(nameof(Index));
        }

        List<Court>? courtDtos = _courtService.GetAll();

        TournamentRequest tournamentRequest = new()
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

        return View(tournamentRequest);
    }

    // POST: Tournaments/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(int id, TournamentRequest tournamentRequest)
    {
        if (!ModelState.IsValid)
        {
            return View(tournamentRequest);
        }

        Tournament tournament = _tournamentTransformer.RequestToDto(tournamentRequest);
        tournament.ImageUrl = await _fileService.SaveImageAsync(tournamentRequest.Image, _webHostEnvironment);
        
        if (!_tournamentService.Edit(id, tournament))
        {
            TempData["Message"] = "Fout tijdens het opslaan van de data.";
            TempData["MessageType"] = "danger";

            return RedirectToAction(nameof(Index));
        }

        TempData["Message"] = "Item succesvol gewijzigd";
        TempData["MessageType"] = "success";

        return RedirectToAction(nameof(Index));
    }

    // GET: Tournaments/Delete/5
    public ActionResult Delete(int id)
    {
        Tournament? tournamentDto = _tournamentService.FindById(id);
        if (tournamentDto == null)
        {
            TempData["Message"] = "Fout tijdens het ophalen van de data.";
            TempData["MessageType"] = "danger";

            return View();
        }

        return View(_tournamentTransformer.ModelToView(tournamentDto));
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