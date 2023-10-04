using BusinessLogicLayer.Interfaces.Repositories;
using BusinessLogicLayer.Interfaces.Services;
using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Services;

public class TournamentService : ITournamentService
{
    private readonly ITournamentRepository _tournamentRepository;

    public TournamentService(ITournamentRepository tournamentRepository)
    {
        _tournamentRepository = tournamentRepository;
    }

    public List<Tournament>? GetAll()
    {
        return _tournamentRepository.GetAll();
    }

    public Tournament? FindById(int id)
    {
        return _tournamentRepository.FindById(id);
    }

    public bool Create(Tournament tournament)
    {
        return _tournamentRepository.Create(tournament);
    }

    public bool Edit(int id, Tournament tournament)
    {
        return _tournamentRepository.Edit(id, tournament);
    }

    public bool Delete(int id)
    {
        return _tournamentRepository.Delete(id);
    }

    public StatusMessage AddUser(int tournamentId, string userId)
    {
        return _tournamentRepository.AddUser(tournamentId, userId);
    }
}