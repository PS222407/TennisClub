using System.Security.Claims;
using BusinessLogicLayer;
using BusinessLogicLayer.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TennisClub_0._1.Services;
using TennisClub_0._1.ViewModels;
using Tournament = BusinessLogicLayer.Models.Tournament;

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
    public ActionResult Index()
    {
        List<Tournament>? tournaments = _tournamentService.GetAll();
        if (tournaments == null)
        {
            TempData["Message"] = "Fout tijdens het ophalen van data.";
            TempData["MessageType"] = "danger";

            return View(new List<TournamentViewModel>());
        }

        return View(_tournamentTransformer.ModelsToViews(tournaments));
    }

    // GET: Tournament/Details/5
    public ActionResult Details(int id)
    {
        Tournament? tournament = _tournamentService.FindById(id);
        if (tournament == null)
        {
            TempData["Message"] = "Fout tijdens het ophalen van data.";
            TempData["MessageType"] = "danger";

            return View();
        }

        return View(_tournamentTransformer.ModelToView(tournament));
    }

    [HttpGet("Tournament/Join/{id:int}")]
    [Authorize]
    public ActionResult Join(int id)
    {
        string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        StatusMessage statusMessage = _tournamentService.AddUser(id, userId);
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