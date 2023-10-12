using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Interfaces.Repositories;

public interface ICourtRepository
{
    public List<Court>? GetAll();

    public Court? FindById(int id);

    public bool Create(Court court);

    public bool Edit(int id, Court court);

    public bool Delete(int id);
    
    bool Exists(int id);
}