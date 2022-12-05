
using Library.Entities;
using System.Collections.Generic;

namespace Library.Interfaces.Service
{
    public interface IUserService
    {
        bool LoginUser(User user);
        User GetUser(string login);
        List<User> GetUsers();
        int UpdateUserRole(User user);
    }
}
