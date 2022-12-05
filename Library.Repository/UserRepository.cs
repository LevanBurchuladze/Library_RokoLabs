
using Dapper;
using Library.Entities;
using Library.Interfaces.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Library.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;
        private readonly DbOptions _dbOptions;

        public UserRepository(ILogger<UserRepository> logger, DbOptions dbOptions)
        {
            _logger = logger;
            _dbOptions = dbOptions;
        }

        public User GetUser(string login)
        {
            using (IDbConnection db = new SqlConnection(_dbOptions.ConnectionString))
            {
                var param = new DynamicParameters();
                param.Add("Login", login);
                var querry = "SELECT * FROM Users WHERE Login = @Login; SELECT SCOPE_IDENTITY();";
                List<User> user = db.Query<User>(querry, param).ToList();
                return user[0];
            }
        }

        public List<User> GetUsers()
        {
            using (IDbConnection db = new SqlConnection(_dbOptions.ConnectionString))
            {
                var querry = "SELECT * FROM Users";
                return db.Query<User>(querry).ToList();
            }
        }

        public int UpdateUserRole(User user)
        {
            using (IDbConnection db = new SqlConnection(_dbOptions.ConnectionString))
            {
                var param = new DynamicParameters();
                param.Add("Login", user.Login);
                param.Add("Role", user.Role);

                var querry = "UPDATE Users SET Role = @Role WHERE Login = @Login";

                db.Query<decimal>(querry, param);

                return 1;
            }
        }
    }
}
