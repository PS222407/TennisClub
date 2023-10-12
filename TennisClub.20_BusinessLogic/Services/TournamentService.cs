using BusinessLogicLayer.Interfaces.Repositories;
using BusinessLogicLayer.Interfaces.Services;
using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Services;

public class TournamentService : ITournamentService
{
    private readonly ITournamentRepository _tournamentRepository;

    private readonly IUserRepository _userRepository;

    public TournamentService(ITournamentRepository tournamentRepository, IUserRepository userRepository)
    {
        _tournamentRepository = tournamentRepository;
        _userRepository = userRepository;
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
        return tournament.IsValid() && _tournamentRepository.Create(tournament);
    }

    public bool Edit(int id, Tournament tournament)
    {
        return tournament.IsValid() && _tournamentRepository.Exists(id) && _tournamentRepository.Edit(id, tournament);
    }

    public bool Delete(int id)
    {
        return _tournamentRepository.Delete(id);
    }

    public StatusMessage AddUser(int tournamentId, string userId)
    {
        if (!_userRepository.Exists(userId))
        {
            return new StatusMessage
            {
                Success = false,
                Reason = "User does not exist",
            };
        }

        if (!_tournamentRepository.Exists(tournamentId))
        {
            return new StatusMessage
            {
                Success = false,
                Reason = "Tournament does not exist",
            };
        }

        return _tournamentRepository.AddUser(tournamentId, userId);
    }
}