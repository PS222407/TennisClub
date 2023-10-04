using DataLayer.Dtos;

namespace BusinessLogicLayer.Interfaces;

public interface ICourtService
{
    public Task<List<CourtDto>?> GetAll();

    public Task<CourtDto?> FindById(int id);

    public Task<bool> Create(CourtDto courtDto);

    public Task<bool> Edit(int id, CourtDto courtDto);

    public Task<bool> Delete(int id);
}