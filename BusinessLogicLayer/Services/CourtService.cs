using BusinessLogicLayer.Interfaces;
using DataLayer.Dtos;
using DataLayer.Repositories;

namespace BusinessLogicLayer.Services;

public class CourtService : ICourtService
{
    private readonly ICourtRepository _courtRepository;
    
    public CourtService(ICourtRepository courtRepository)
    {
        _courtRepository = courtRepository;
    }
    
    public async Task<List<CourtDto>?> GetAll()
    {
        return await _courtRepository.GetAll();
    }

    public async Task<CourtDto?> FindById(int id)
    {
        return await _courtRepository.FindById(id);
    }

    public async Task<bool> Create(CourtDto courtDto)
    {
        return await _courtRepository.Create(courtDto);
    }

    public async Task<bool> Edit(int id, CourtDto courtDto)
    {
        return await _courtRepository.Edit(id, courtDto);
    }

    public async Task<bool> Delete(int id)
    {
        return await _courtRepository.Delete(id);
    }
}

