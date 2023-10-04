using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Interfaces.Services;

public interface ICourtService
{
    public List<Court>? GetAll();

    public Court? FindById(int id);

    public bool Create(Court court);

    public bool Edit(int id, Court court);

    public bool Delete(int id);
}