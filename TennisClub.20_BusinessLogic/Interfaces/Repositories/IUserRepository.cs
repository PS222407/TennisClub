namespace BusinessLogicLayer.Interfaces.Repositories;

public interface IUserRepository
{
    public bool Exists(string id);
}