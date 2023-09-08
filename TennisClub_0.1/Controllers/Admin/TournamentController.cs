using BusinessLogicLayer.Interfaces;
using DataLayer.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TennisClub_0._1.Models;
using TennisClub_0._1.Requests;

namespace TennisClub_0._1.Controllers.Admin;

[Authorize(Roles = "Admin")]
[Area("Admin")]
public class TournamentController : Controller
{
    private readonly ITournamentService _tournamentService;
    
    public TournamentController(ITournamentService tournamentService)
    {
        _tournamentService = tournamentService;
    }
    
    // GET: Tournaments
    public ActionResult Index()
    {
        List<TournamentDto> tournamentDtos = _tournamentService.GetAll();
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
    public ActionResult Details(int id)
    {
        TournamentDto? tournamentDto = _tournamentService.FindById(id);
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
    public ActionResult Create()
    {
        return View();
    }
    
    // POST: Tournaments/Create
    [HttpPost]
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
            bool success = _tournamentService.Create(tournamentDto);
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
    public ActionResult Edit(int id)
    {
        TournamentDto? tournamentDto = _tournamentService.FindById(id);
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
    [HttpPost]
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
            bool success = _tournamentService.Edit(id, tournamentDto);
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
    public ActionResult Delete(int id)
    {
        TournamentDto? tournamentDto =  _tournamentService.FindById(id);
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
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Destroy(int id)
    {
        try
        {
            bool success =  _tournamentService.Delete(id);
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