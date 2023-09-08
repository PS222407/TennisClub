using BusinessLogicLayer.Interfaces;
using DataLayer.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TennisClub_0._1.Models;
using TennisClub_0._1.Requests;

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
        List<CourtDto> courtDtos = _courtService.GetAll();
        List<CourtViewModel> courtViewModels = new List<CourtViewModel>();
        foreach (CourtDto courtDto in courtDtos)
        {
            courtViewModels.Add(new CourtViewModel()
            {
                Id = courtDto.Id,
                Double = courtDto.Double,
                Indoor = courtDto.Indoor,
                Number = courtDto.Number,
            });
        }

        return View(courtViewModels);
    }

    // GET: Courts/Details/5
    public ActionResult Details(int id)
    {
        CourtDto? courtDto = _courtService.FindById(id);
        CourtViewModel courtViewModel = new CourtViewModel()
        {
            Id = courtDto.Id,
            Double = courtDto.Double,
            Indoor = courtDto.Indoor,
            Number = courtDto.Number,
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
        try
        {
            CourtDto courtDto = new CourtDto()
            {
                Double = courtRequest.Double,
                Indoor = courtRequest.Indoor,
                Number = courtRequest.Number,
            };
            bool success = _courtService.Create(courtDto);
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
    
    // GET: Courts/Edit/5
    public ActionResult Edit(int id)
    {
        CourtDto? courtDto = _courtService.FindById(id);
        CourtViewModel courtViewModel = new CourtViewModel()
        {
            Id = courtDto.Id,
            Double = courtDto.Double,
            Indoor = courtDto.Indoor,
            Number = courtDto.Number,
        };
        
        return View(courtViewModel);
    }
    
    // POST: Courts/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, CourtRequest courtRequest)
    {
        try
        {
            CourtDto courtDto = new CourtDto()
            {
                Double = courtRequest.Double,
                Indoor = courtRequest.Indoor,
                Number = courtRequest.Number,
            };
            bool success = _courtService.Edit(id, courtDto);
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
    
    // GET: Courts/Delete/5
    public ActionResult Delete(int id)
    {
        CourtDto? courtDto = _courtService.FindById(id);
        CourtViewModel courtViewModel = new CourtViewModel()
        {
            Id = courtDto.Id,
            Double = courtDto.Double,
            Indoor = courtDto.Indoor,
            Number = courtDto.Number,
        };
        
        return View(courtViewModel);
    }
    
    // POST: Courts/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Destroy(int id)
    {
        try
        {
            bool success = _courtService.Delete(id);
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