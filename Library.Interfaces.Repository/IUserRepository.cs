
using Library.Entities;
using System.Collections.Generic;

namespace Library.Interfaces.Repository
{
    public interface IUserRepository
    {
        User GetUser(string login);
        List<User> GetUsers();
        int UpdateUserRole(User user);
    }
}
