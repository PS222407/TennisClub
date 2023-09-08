using DataLayer.Dtos;

namespace DataLayer.Repositories;

public interface ITournamentRepository
{
    public Task<List<TournamentDto>> GetAll();

    public Task<TournamentDto?> FindById(int id);

    public Task<bool> Create(TournamentDto tournamentDto);

    public Task<bool> Edit(int id, TournamentDto tournamentDto);

    public Task<bool> Delete(int id);
}