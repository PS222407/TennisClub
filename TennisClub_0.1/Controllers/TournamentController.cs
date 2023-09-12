using BusinessLogicLayer.Interfaces;
using DataLayer.Dtos;
using Microsoft.AspNetCore.Mvc;
using TennisClub_0._1.Models;

namespace TennisClub_0._1.Controllers;

public class TournamentController : Controller
{
    private readonly ITournamentService _tournamentService;
    
    public TournamentController(ITournamentService tournamentService)
    {
        _tournamentService = tournamentService;
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
        List<CourtViewModel> courtViewModels = new List<CourtViewModel>();
        if (tournamentDto.Courts != null)
        {
            foreach (CourtDto courtDto in tournamentDto.Courts)
            {
                courtViewModels.Add(new CourtViewModel
                {
                    Id = courtDto.Id,
                    Number = courtDto.Number,
                    Indoor = courtDto.Indoor,
                    Double = courtDto.Double,
                });
            }
        }

        tournamentViewModel.Courts = courtViewModels;

        return View(tournamentViewModel);
    }
}