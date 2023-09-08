using BusinessLogicLayer.Interfaces;
using DataLayer.Dtos;
using DataLayer.Repositories;

namespace BusinessLogicLayer.Services;

public class CourtService : ICourtService
{
    private readonly ICourtRepository _courtRepository;
    
    public CourtService()
    {
        _courtRepository = new CourtRepository();
    }
    
    public List<CourtDto> GetAll()
    {
        return Task.Run(async () => await _courtRepository.GetAll()).GetAwaiter().GetResult();
    }

    public CourtDto? FindById(int id)
    {
        return Task.Run(async () => await _courtRepository.FindById(id)).GetAwaiter().GetResult();
    }

    public bool Create(CourtDto courtDto)
    {
        return Task.Run(async () => await _courtRepository.Create(courtDto)).GetAwaiter().GetResult();
    }

    public bool Edit(int id, CourtDto courtDto)
    {
        return Task.Run(async () => await _courtRepository.Edit(id, courtDto)).GetAwaiter().GetResult();
    }

    public bool Delete(int id)
    {
        return Task.Run(async () => await _courtRepository.Delete(id)).GetAwaiter().GetResult();
    }
}

