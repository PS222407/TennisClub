using DataLayer;
using DataLayer.Dtos;

namespace BusinessLogicLayer.Interfaces;

public interface ITournamentService
{
    public Task<List<TournamentDto>?> GetAll();

    public Task<TournamentDto?> FindById(int id);

    public Task<bool> Create(TournamentDto tournamentDto);

    public Task<bool> Edit(int id, TournamentDto tournamentDto);

    public Task<bool> Delete(int id);

    public Task<StatusMessage> AddUser(int tournamentId, string userId);
}