using BusinessLogicLayer.Interfaces;
using DataLayer;
using DataLayer.Dtos;
using DataLayer.Repositories;

namespace BusinessLogicLayer.Services;

public class TournamentService : ITournamentService
{
    private readonly ITournamentRepository _tournamentRepository;

    public TournamentService(ITournamentRepository tournamentRepository)
    {
        _tournamentRepository = tournamentRepository;
    }

    public async Task<List<TournamentDto>?> GetAll()
    {
        return await _tournamentRepository.GetAll();
    }

    public async Task<TournamentDto?> FindById(int id)
    {
        return await _tournamentRepository.FindById(id);
    }

    public async Task<bool> Create(TournamentDto tournamentDto)
    {
        return await _tournamentRepository.Create(tournamentDto);
    }

    public async Task<bool> Edit(int id, TournamentDto tournamentDto)
    {
        return await _tournamentRepository.Edit(id, tournamentDto);
    }

    public async Task<bool> Delete(int id)
    {
        return await _tournamentRepository.Delete(id);
    }

    public async Task<StatusMessage> AddUser(int tournamentId, string userId)
    {
        return await _tournamentRepository.AddUser(tournamentId, userId);
    }
}