using System.Security.Claims;
using BusinessLogicLayer.Interfaces;
using DataLayer;
using DataLayer.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TennisClub_0._1.Models;
using TennisClub_0._1.Services;

namespace TennisClub_0._1.Controllers;

public class TournamentController : Controller
{
    private readonly ITournamentService _tournamentService;
    private readonly TournamentTransformer _tournamentTransformer = new();

    public TournamentController(ITournamentService tournamentService)
    {
        _tournamentService = tournamentService;
    }

    // GET
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

    // GET: Tournament/Details/5
    public async Task<ActionResult> Details(int id)
    {
        TournamentDto? tournamentDto = await _tournamentService.FindById(id);
        if (tournamentDto == null)
        {
            TempData["Message"] = "Fout tijdens het ophalen van data.";
            TempData["MessageType"] = "danger";

            return View();
        }

        return View(_tournamentTransformer.DtoToView(tournamentDto));
    }

    [HttpGet("Tournament/Join/{id:int}")]
    [Authorize]
    public async Task<ActionResult> Join(int id)
    {
        string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        StatusMessage statusMessage = await _tournamentService.AddUser(id, userId);
        if (!statusMessage.Success)
        {
            TempData["Message"] = statusMessage.Reason;
            TempData["MessageType"] = "danger";

            return RedirectToAction(nameof(Details), new { id });
        }

        TempData["Message"] = "Succesvol ingeschreven!";
        TempData["MessageType"] = "success";

        return RedirectToAction(nameof(Details), new { id = id });
    }
}