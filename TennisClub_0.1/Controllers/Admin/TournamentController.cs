using DataLayer.Dtos;
using DataLayer.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TennisClub_0._1.Models;
using TennisClub_0._1.Requests;

namespace TennisClub_0._1.Controllers.Admin;

[Authorize(Roles = "Admin")]
[Route("Admin/[controller]")]
public class TournamentsController : Controller
{
    private readonly ITournamentRepository _tournamentRepository;
    
    public TournamentsController(ITournamentRepository tournamentRepository)
    {
        _tournamentRepository = tournamentRepository;
    }
    
    // GET: Tournaments
    [HttpGet]
    public ActionResult Index()
    {
        List<TournamentDto>? tournamentDtos = Task.Run(async () => await _tournamentRepository.GetAll()).GetAwaiter().GetResult();
        List<TournamentViewModel> tournamentViewModels = new List<TournamentViewModel>();
        foreach (TournamentDto tournamentDto in tournamentDtos)
        {
            tournamentViewModels.Add(new TournamentViewModel()
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
    [HttpGet("Details/{id:int}")]
    public ActionResult Details(int id)
    {
        TournamentDto? tournamentDto = Task.Run(async () => await _tournamentRepository.FindById(id)).GetAwaiter().GetResult();
        TournamentViewModel tournamentViewModel = new TournamentViewModel()
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
    [HttpGet("Create")]
    public ActionResult Create()
    {
        return View();
    }

    // POST: Tournaments/Create
    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public ActionResult Create(TournamentRequest tournamentRequest)
    {
        try
        {
            TournamentDto tournamentDto = new TournamentDto()
            {
                Name = tournamentRequest.Name,
                Description = tournamentRequest.Description,
                Price = tournamentRequest.Price,
                MaxMembers = tournamentRequest.MaxMembers,
                StartDateTime = tournamentRequest.StartDateTime,
            };
            bool success = Task.Run(async () => await _tournamentRepository.Create(tournamentDto)).GetAwaiter().GetResult();
            // TODO: Add logic here
            if (!success)
            {
                return View();
            }

            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // GET: Tournaments/Edit/5
    [HttpGet("Edit/{id:int}")]
    public ActionResult Edit(int id)
    {
        TournamentDto? tournamentDto = Task.Run(async () => await _tournamentRepository.FindById(id)).GetAwaiter().GetResult();
        TournamentViewModel tournamentViewModel = new TournamentViewModel()
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

    // POST: Tournaments/Edit/5
    [HttpPost("Edit/{id:int}")]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, TournamentRequest tournamentRequest)
    {
        try
        {
            TournamentDto tournamentDto = new TournamentDto()
            {
                Name = tournamentRequest.Name,
                Description = tournamentRequest.Description,
                Price = tournamentRequest.Price,
                MaxMembers = tournamentRequest.MaxMembers,
                StartDateTime = tournamentRequest.StartDateTime,
            };
            bool success = Task.Run(async () => await _tournamentRepository.Edit(id, tournamentDto)).GetAwaiter().GetResult();
            // TODO: Add logic here
            if (!success)
            {
                return View();
            }

            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // GET: Tournaments/Delete/5
    [HttpGet("Delete{id:int}")]
    public ActionResult Delete(int id)
    {
        TournamentDto? tournamentDto = Task.Run(async () => await _tournamentRepository.FindById(id)).GetAwaiter().GetResult();
        TournamentViewModel tournamentViewModel = new TournamentViewModel()
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
    [HttpPost("Delete/{id:int}")]
    [ValidateAntiForgeryToken]
    public ActionResult Destroy(int id)
    {
        try
        {
            bool success = Task.Run(async () => await _tournamentRepository.Delete(id)).GetAwaiter().GetResult();
            // TODO: Add logic here
            if (!success)
            {
                return View("Delete");
            }

            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View("Delete");
        }
    }
}