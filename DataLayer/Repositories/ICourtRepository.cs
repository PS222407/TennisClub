using DataLayer.Dtos;

namespace DataLayer.Repositories;

public interface ICourtRepository
{
    public Task<List<CourtDto>?> GetAll();

    public Task<CourtDto?> FindById(int id);

    public Task<bool> Create(CourtDto courtDto);

    public Task<bool> Edit(int id, CourtDto courtDto);

    public Task<bool> Delete(int id);
}