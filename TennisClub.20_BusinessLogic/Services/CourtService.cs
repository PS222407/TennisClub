using BusinessLogicLayer.Interfaces.Repositories;
using BusinessLogicLayer.Interfaces.Services;
using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Services;

public class CourtService : ICourtService
{
    private readonly ICourtRepository _courtRepository;

    public CourtService(ICourtRepository courtRepository)
    {
        _courtRepository = courtRepository;
    }

    public List<Court>? GetAll()
    {
        return _courtRepository.GetAll();
    }

    public Court? FindById(int id)
    {
        return _courtRepository.FindById(id);
    }

    public bool Create(Court court)
    {
        return court.IsValid() && _courtRepository.Create(court);
    }

    public bool Edit(int id, Court court)
    {
        return court.IsValid() && _courtRepository.Exists(id) && _courtRepository.Edit(id, court);
    }

    public bool Delete(int id)
    {
        return _courtRepository.Delete(id);
    }
}