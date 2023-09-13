﻿using BusinessLogicLayer.Interfaces;
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

    public List<TournamentDto>? GetAll()
    {
        return Task.Run(async () => await _tournamentRepository.GetAll()).GetAwaiter().GetResult();
    }

    public TournamentDto? FindById(int id)
    {
        return Task.Run(async () => await _tournamentRepository.FindById(id)).GetAwaiter().GetResult();
    }

    public bool Create(TournamentDto tournamentDto)
    {
        return Task.Run(async () => await _tournamentRepository.Create(tournamentDto)).GetAwaiter().GetResult();
    }

    public bool Edit(int id, TournamentDto tournamentDto)
    {
        return Task.Run(async () => await _tournamentRepository.Edit(id, tournamentDto)).GetAwaiter().GetResult();
    }

    public bool Delete(int id)
    {
        return Task.Run(async () => await _tournamentRepository.Delete(id)).GetAwaiter().GetResult();
    }

    public StatusMessage AddUser(int tournamentId, string userId)
    {
        return Task.Run(async () => await _tournamentRepository.AddUser(tournamentId, userId)).GetAwaiter().GetResult();
    }
}