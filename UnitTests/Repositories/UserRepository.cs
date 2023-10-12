using BusinessLogicLayer.Interfaces.Repositories;

namespace UnitTests.Repositories;

public class UserRepository : IUserRepository
{
    public bool Exists(string id)
    {
        return true;
    }
}