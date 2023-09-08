using DataLayer.Dtos;
using DataLayer.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TennisClub_0._1.Models;
using TennisClub_0._1.Requests;

namespace TennisClub_0._1.Controllers.Admin;

[Authorize(Roles = "Admin")]
// [Route("Admin/[controller]")]
[Area("Admin")]
public class CourtsController : Controller
{
    private readonly ICourtRepository _courtRepository;

    public CourtsController(ICourtRepository courtRepository)
    {
        _courtRepository = courtRepository;
    }

    // GET: Courts
    [HttpGet]
    public ActionResult Index()
    {
        List<CourtDto>? courtDtos = Task.Run(async () => await _courtRepository.GetAll()).GetAwaiter().GetResult();
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
    [HttpGet("Details/{id:int}")]
    public ActionResult Details(int id)
    {
        CourtDto? courtDto = Task.Run(async () => await _courtRepository.FindById(id)).GetAwaiter().GetResult();
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
    [HttpGet("Create")]
    public ActionResult Create()
    {
        return View();
    }

    // POST: Courts/Create
    [HttpPost("Create")]
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
            bool success = Task.Run(async () => await _courtRepository.Create(courtDto)).GetAwaiter().GetResult();
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
    [HttpGet("Edit/{id:int}")]
    public ActionResult Edit(int id)
    {
        CourtDto? courtDto = Task.Run(async () => await _courtRepository.FindById(id)).GetAwaiter().GetResult();
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
    [HttpPost("Edit/{id:int}")]
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
            bool success = Task.Run(async () => await _courtRepository.Edit(id, courtDto)).GetAwaiter().GetResult();
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
    [HttpGet("Delete/{id:int}")]
    public ActionResult Delete(int id)
    {
        CourtDto? courtDto = Task.Run(async () => await _courtRepository.FindById(id)).GetAwaiter().GetResult();
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
    [HttpPost("Delete/{id:int}")]
    [ValidateAntiForgeryToken]
    public ActionResult Destroy(int id)
    {
        try
        {
            bool success = Task.Run(async () => await _courtRepository.Delete(id)).GetAwaiter().GetResult();
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