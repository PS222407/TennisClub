using System.Security.Claims;
using BusinessLogicLayer.Interfaces;
using DataLayer;
using DataLayer.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TennisClub_0._1.Models;
using TennisClub_0._1.Services;

namespace TennisClub_0._1.Controllers;

public class TournamentController : Controller
{
    // private readonly UserManager<User> _userManager;
    
    private readonly ITournamentService _tournamentService;
    
    private readonly IViewModelTransformerService _viewModelTransformer;
    
    public TournamentController(ITournamentService tournamentService, IViewModelTransformerService viewModelTransformer)
    {
        _tournamentService = tournamentService;
        _viewModelTransformer = viewModelTransformer;
    }

    // GET
    public IActionResult Index()
    {
        List<TournamentDto>? tournamentDtos = _tournamentService.GetAll();
        if (tournamentDtos == null)
        {
            TempData["Message"] = "Fout tijdens het ophalen van data.";
            TempData["MessageType"] = "danger";

            return View(new List<TournamentViewModel>());
        }

        return View(_viewModelTransformer.TransformTournaments(tournamentDtos));
    }
    
    // GET: Tournament/Details/5
    public ActionResult Details(int id)
    {
        TournamentDto? tournamentDto = _tournamentService.FindById(id);
        if (tournamentDto == null)
        {
            TempData["Message"] = "Fout tijdens het ophalen van data.";
            TempData["MessageType"] = "danger";

            return View();
        }

        return View(_viewModelTransformer.TransformTournament(tournamentDto));
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

            return RedirectToAction(nameof(Details), new { id = id });
        }

        TempData["Message"] = "Succesvol ingeschreven!";
        TempData["MessageType"] = "success";

        return RedirectToAction(nameof(Details), new { id = id });
    }
}