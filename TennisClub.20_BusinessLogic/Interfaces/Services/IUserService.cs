namespace BusinessLogicLayer.Interfaces.Services;

public interface IUserService
{
    public bool Exists(string userId);
}