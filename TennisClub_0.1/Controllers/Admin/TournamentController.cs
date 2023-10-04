using BusinessLogicLayer.Interfaces;
using DataLayer.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TennisClub_0._1.Models;
using TennisClub_0._1.Requests;
using TennisClub_0._1.Services;

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
    public async Task<ActionResult> Index()
    {
        List<TournamentDto>? tournamentDtos = await _tournamentService.GetAll();
        if (tournamentDtos == null)
        {
            TempData["Message"] = "Fout tijdens het ophalen van data.";
            TempData["MessageType"] = "danger";

            return View(new List<TournamentViewModel>());
        }

        return View(_tournamentTransformer.DtosToViews(tournamentDtos));
    }

    // GET: Tournaments/Details/5
    public async Task<ActionResult> Details(int id)
    {
        TournamentDto? tournamentDto = await _tournamentService.FindById(id);
        if (tournamentDto == null)
        {
            TempData["Message"] = "Fout tijdens het ophalen van data.";
            TempData["MessageType"] = "danger";

            return RedirectToAction(nameof(Index));
        }

        return View(_tournamentTransformer.DtoToView(tournamentDto));
    }

    // GET: Tournaments/Create
    public async Task<ActionResult> Create()
    {
        List<CourtDto>? courtDtos = await _courtService.GetAll();

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
        
        TournamentDto tournamentDto = _tournamentTransformer.RequestToDto(tournamentRequest);
        tournamentDto.ImageUrl = await _fileService.SaveImageAsync(tournamentRequest.Image, _webHostEnvironment);
            
        if (!await _tournamentService.Create(tournamentDto))
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
    public async Task<ActionResult> Edit(int id)
    {
        TournamentDto? tournamentDto = await _tournamentService.FindById(id);
        if (tournamentDto == null)
        {
            TempData["Message"] = "Fout tijdens het ophalen van data.";
            TempData["MessageType"] = "danger";

            return RedirectToAction(nameof(Index));
        }

        List<CourtDto>? courtDtos = await _courtService.GetAll();

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

        TournamentDto tournamentDto = _tournamentTransformer.RequestToDto(tournamentRequest);
        tournamentDto.ImageUrl = await _fileService.SaveImageAsync(tournamentRequest.Image, _webHostEnvironment);
        
        if (!await _tournamentService.Edit(id, tournamentDto))
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
    public async Task<ActionResult> Delete(int id)
    {
        TournamentDto? tournamentDto = await _tournamentService.FindById(id);
        if (tournamentDto == null)
        {
            TempData["Message"] = "Fout tijdens het ophalen van de data.";
            TempData["MessageType"] = "danger";

            return View();
        }

        return View(_tournamentTransformer.DtoToView(tournamentDto));
    }

    // POST: Tournaments/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Destroy(int id)
    {
        if (!await _tournamentService.Delete(id))
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