
using Library.Entities;
using Library.Interfaces.Repository;
using Library.Interfaces.Service;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Library.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IUserRepository _userRepository;

        public UserService(ILogger<UserService> logger,IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public User GetUser(string login)
        {
            User user = _userRepository.GetUser(login);
            if (user == null)
            {
                _logger.LogError("User not found");
                return null;
            }
            return user;
        }

        public List<User> GetUsers()
        {
            List<User> users = _userRepository.GetUsers();
            if (users == null)
            {
                _logger.LogError("Users not found");
                return null;
            }
            return _userRepository.GetUsers();
        }

        public bool LoginUser(User user)
        {
            if(user == null || string.IsNullOrEmpty(user.Login))
            {
                return false;
            }

            var CurUser = GetUser(user.Login);
            if (CurUser == null)
            {
                return false;
            }

            return (CurUser.Password == user.Password);
        }

        public int UpdateUserRole(User user)
        {
            return _userRepository.UpdateUserRole(user);
        }
    }
}
