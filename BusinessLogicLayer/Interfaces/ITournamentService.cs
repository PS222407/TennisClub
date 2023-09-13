using DataLayer;
using DataLayer.Dtos;

namespace BusinessLogicLayer.Interfaces;

public interface ITournamentService
{
    public List<TournamentDto>? GetAll();

    public TournamentDto? FindById(int id);

    public bool Create(TournamentDto tournamentDto);

    public bool Edit(int id, TournamentDto tournamentDto);

    public bool Delete(int id);

    public StatusMessage AddUser(int tournamentId, string userId);
}