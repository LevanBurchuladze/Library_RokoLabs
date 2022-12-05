
using Library.Dto;
using Microsoft.Extensions.Logging;
using Dapper;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using Library.Interfaces.Repository;

namespace Library.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ILogger<AuthorRepository> _logger;
        private readonly DbOptions _dbOptions;

        public AuthorRepository(ILogger<AuthorRepository> logger,DbOptions dbOptions)
        {
            _logger = logger;
            _dbOptions = dbOptions;
        }

        public void DeleteAuthorsBook(int bookId, int authorId)
        {
            using (IDbConnection db = new SqlConnection(_dbOptions.ConnectionString))
            {
                var param = new DynamicParameters();
                param.Add("BookId", bookId);
                param.Add("AuthorId", authorId);

                var querry = "DELETE FROM AuthorsBooks WHERE @AuthorId=AuthorId AND @BookId=BookId";
                db.Query(querry, param);
            }
        }

        public void DeleteAuthorsPatent(int patentId, int authorId)
        {
            using (IDbConnection db = new SqlConnection(_dbOptions.ConnectionString))
            {
                var param = new DynamicParameters();
                param.Add("PatentId", patentId);
                param.Add("AuthorId", authorId);

                var querry = "DELETE FROM AuthorsPatents WHERE @AuthorId=AuthorId AND @PatentId=PatentId";
                db.Query(querry, param);
            }
        }

        public int FindAuthor(AuthorDto authorDto)
        {
            using (IDbConnection db = new SqlConnection(_dbOptions.ConnectionString))
            {
                var param = new DynamicParameters();
                param.Add("FirstName", authorDto.FirstName);
                param.Add("SecondName", authorDto.SecondName);
                var querry = "SELECT AuthorId FROM Authors WHERE FirstName = @FirstName AND SecondName = @SecondName; SELECT SCOPE_IDENTITY();";
                return (int)(db.Query<decimal>(querry, authorDto).FirstOrDefault());
            }
        }

        public List<AuthorDto> GetAuthorByBookIds(IEnumerable<int> ids)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("EntityID");
            foreach (var id in ids)
            {
                dt.Rows.Add(id);
            }

            using(IDbConnection db = new SqlConnection(_dbOptions.ConnectionString))
            {
                return db.Query<AuthorDto>("GetAuthorsByBookIds",
                    new { ids = dt.AsTableValuedParameter("dtIntEntity") },
                    commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public List<AuthorDto> GetAuthorByPatentIds(IEnumerable<int> ids)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("EntityID");
            foreach (var id in ids)
            {
                dt.Rows.Add(id);
            }

            using (IDbConnection db = new SqlConnection(_dbOptions.ConnectionString))
            {
                return db.Query<AuthorDto>("GetAuthorsByPatentIds",
                    new { ids = dt.AsTableValuedParameter("dtIntEntity") },
                    commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public List<AuthorDto> GetAuthors()
        {
            using (IDbConnection db = new SqlConnection(_dbOptions.ConnectionString))
            {
                var querry = "SELECT * FROM Authors";
                return db.Query<AuthorDto>(querry).ToList();
            }
        }

        public int InsertAuthor(AuthorDto authorDto)
        {
            using (IDbConnection db = new SqlConnection(_dbOptions.ConnectionString))
            {
                var querry = "INSERT INTO Authors (FirstName, SecondName) VALUES (@FirstName, @SecondName); SELECT SCOPE_IDENTITY();";
                return (int)(db.Query<decimal>(querry, authorDto).First());
            }
        }

        public void InsertAuthorsBooks(int bookId, int tempAuthorid)
        {
            using (IDbConnection db = new SqlConnection(_dbOptions.ConnectionString))
            {
                var param = new DynamicParameters();
                param.Add("AuthorId", tempAuthorid);
                param.Add("BookId", bookId);

                var querry = "INSERT INTO AuthorsBooks (AuthorId, BookId) VALUES (@AuthorId, @BookId); SELECT SCOPE_IDENTITY();";
                db.Query(querry, param);
            }
        }

        public void InsertAuthorsPatents(int patentId, int tempAuthorid)
        {
            using (IDbConnection db = new SqlConnection(_dbOptions.ConnectionString))
            {
                var param = new DynamicParameters();
                param.Add("AuthorId", tempAuthorid);
                param.Add("PatentId", patentId);

                var querry = "INSERT INTO AuthorsPatents (AuthorId, PatentId) VALUES (@AuthorId, @PatentId); SELECT SCOPE_IDENTITY();";
                db.Query(querry, param);
            }
        }

        public void UpdateAuthorsBook(int bookId, int authorId)
        {
            using (IDbConnection db = new SqlConnection(_dbOptions.ConnectionString))
            {
                var param = new DynamicParameters();
                param.Add("BookId", bookId);
                param.Add("AuthorId", authorId);

                var querry = "INSERT INTO AuthorsBooks (AuthorId, BookId) VALUES (@AuthorId, @BookId);";
                db.Query(querry, param);
            }
        }

        public void UpdateAuthorsPatent(int patentId, int authorId)
        {
            using (IDbConnection db = new SqlConnection(_dbOptions.ConnectionString))
            {
                var param = new DynamicParameters();
                param.Add("PatentId", patentId);
                param.Add("AuthorId", authorId);

                var querry = "INSERT INTO AuthorsPatents (AuthorId, PatentId) VALUES (@AuthorId, @PatentId);";
                db.Query(querry, param);
            }
        }
    }
}
