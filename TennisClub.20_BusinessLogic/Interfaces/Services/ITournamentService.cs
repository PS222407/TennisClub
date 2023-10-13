using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Interfaces.Services;

public interface ITournamentService
{
    public List<Tournament>? GetAll();

    public Tournament? FindById(int id);

    public bool Create(Tournament tournament);

    public bool Edit(int id, Tournament tournament);

    public bool Delete(int id);

    public StatusMessage AddUser(int tournamentId, string userId);
}