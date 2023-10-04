using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Interfaces.Services;

public interface ITournamentService
{
    public List<Tournament>? GetAll();

    public Tournament? FindById(int id);

    public bool Create(Tournament tournamentDto);

    public bool Edit(int id, Tournament tournamentDto);

    public bool Delete(int id);

    public StatusMessage AddUser(int tournamentId, string userId);
}