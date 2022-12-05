
using Dapper;
using Library.Dto;
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
    public class BookRepository : IBookRepository
    {
        private readonly ILogger<BookRepository> _logger;
        private readonly DbOptions _dbOptions;

        public BookRepository(ILogger<BookRepository> logger, DbOptions dbOptions)
        {
            _logger = logger;
            _dbOptions = dbOptions;
        }

        public BookDto GetBookById(int Editionid)
        {
            using (IDbConnection db = new SqlConnection(_dbOptions.ConnectionString))
            {
                var param = new DynamicParameters();
                param.Add("EditionId", Editionid);
                var querry = "SELECT * FROM Books as B INNER JOIN Editions as E ON B.EditionId = E.EditionId WHERE E.EditionId = @EditionId;";
                return db.Query<BookDto>(querry,param).First();
            }
        }

        public List<BookDto> Getbooks()
        {
            using (IDbConnection db = new SqlConnection(_dbOptions.ConnectionString))
            {
                var querry = "SELECT * FROM Books as B INNER JOIN Editions as E ON B.EditionId = E.EditionId;";
                return db.Query<BookDto>(querry).ToList();
            }
        }

        public List<BookDto> GetBooksByAuthor(Author newAuthor)
        {
            using (IDbConnection db = new SqlConnection(_dbOptions.ConnectionString))
            {
                var param = new DynamicParameters();
                param.Add("FirstName", newAuthor.FirstName);
                param.Add("SecondName", newAuthor.SecondName);

                var querry = "SELECT * FROM Books as B " +
                    "LEFT JOIN AuthorsBooks as AB ON B.BookId = AB.BookId " +
                    "LEFT JOIN Authors ON AB.AuthorId = Authors.AuthorId " +
                    "WHERE Authors.FirstName = @FirstName AND Authors.SecondName = @SecondName; ";
                return db.Query<BookDto>(querry,param).ToList();
            }
        }

        public List<BookDto> GetbooksByName(string title)
        {
            using (IDbConnection db = new SqlConnection(_dbOptions.ConnectionString))
            {
                var param = new DynamicParameters();
                param.Add("Title", title);
                var querry = "SELECT * FROM Books as B INNER JOIN Editions as E ON B.EditionId = E.EditionId WHERE E.Title = @Title;";
                return db.Query<BookDto>(querry, param).ToList();
            }
        }

        public List<BookDto> GetBooksByPubHouse(string pubHouse)
        {
            using (IDbConnection db = new SqlConnection(_dbOptions.ConnectionString))
            {
                var param = new DynamicParameters();
                param.Add("PublicationHouse", pubHouse);
                var querry = "SELECT * FROM Books as B INNER JOIN Editions as E ON B.EditionId = E.EditionId WHERE PublicationHouse LIKE @PublicationHouse ORDER BY PublicationHouse";
                return db.Query<BookDto>(querry, param).ToList();
            }
        }

        public int GetEqualsBook(BookDto bookDto)
        {
            using (IDbConnection db = new SqlConnection(_dbOptions.ConnectionString))
            {
                var param = new DynamicParameters();
                param.Add("ISBN", bookDto.ISBN);

                var querry = "SELECT * FROM Books as B INNER JOIN Editions as E ON B.EditionId = E.EditionId " +
                    "WHERE ISBN = @ISBN;";
                return (int)db.Query<decimal>(querry, param).FirstOrDefault();
            }
        }

        public int InsertBook(BookDto bookDto)
        {
            using (IDbConnection db = new SqlConnection(_dbOptions.ConnectionString))
            {
                var querry = "INSERT INTO Books (EditionId, PublicationHouse, ISBN) VALUES (@EditionId, @PublicationHouse, @ISBN); SELECT SCOPE_IDENTITY();";
                return (int)(db.Query<decimal>(querry, bookDto).First());
            }
        }

        public int UpdateBook(BookDto bookDto)
        {
            using (IDbConnection db = new SqlConnection(_dbOptions.ConnectionString))
            {
                var param = new DynamicParameters();
                param.Add("BookId", bookDto.BookId);
                param.Add("PublicationHouse", bookDto.PublicationHouse);
                param.Add("ISBN",bookDto.ISBN);

                var querry = "UPDATE Books SET PublicationHouse = @PublicationHouse,ISBN = @ISBN WHERE BookId = @BookId";

                db.Query<decimal>(querry, param);

                return 1;
            }
        }
    }
}
