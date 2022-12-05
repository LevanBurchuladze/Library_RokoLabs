
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
    public class PatentRepository : IPatentRepository
    {
        private readonly ILogger<PatentRepository> _logger;
        private readonly DbOptions _dbOptions;

        public PatentRepository(ILogger<PatentRepository> logger, DbOptions dbOptions)
        {
            _logger = logger;
            _dbOptions = dbOptions;
        }

        public List<PatentDto> GetPatentsByName(string title)
        {
            using (IDbConnection db = new SqlConnection(_dbOptions.ConnectionString))
            {
                var param = new DynamicParameters();
                param.Add("Title", title);
                var querry = "SELECT * FROM Patents as P INNER JOIN Editions as E ON P.EditionId = E.EditionId WHERE E.Title = @Title;";
                return db.Query<PatentDto>(querry, param).ToList();
            }
        }

        public List<PatentDto> GetPatents()
        {
            using (IDbConnection db = new SqlConnection(_dbOptions.ConnectionString))
            {
                var querry = "SELECT * FROM Patents as P INNER JOIN Editions as E ON P.EditionId = E.EditionId;";
                return db.Query<PatentDto>(querry).ToList();
            }
        }

        public int InsertPatent(PatentDto patentDto)
        {
            using (IDbConnection db = new SqlConnection(_dbOptions.ConnectionString))
            {
                var querry = "INSERT INTO Patents (EditionId, RegNumber, AppDate, PublicationDate) " +
                    "VALUES (@EditionId, @RegNumber, @AppDate, @PublicationDate); SELECT SCOPE_IDENTITY();";
                return (int)(db.Query<decimal>(querry, patentDto).First());
            }
        }

        public List<PatentDto> GetPatentsByAuthor(Author newAuthor)
        {
            using (IDbConnection db = new SqlConnection(_dbOptions.ConnectionString))
            {
                var param = new DynamicParameters();
                param.Add("FirstName", newAuthor.FirstName);
                param.Add("SecondName", newAuthor.SecondName);

                var querry = "SELECT * FROM Patents as P " +
                    "LEFT JOIN AuthorsPatents as AP ON P.PatentId = AP.PatentId " +
                    "LEFT JOIN Authors ON AP.AuthorId = Authors.AuthorId " +
                    "WHERE Authors.FirstName = @FirstName AND Authors.SecondName = @SecondName; ";
                return db.Query<PatentDto>(querry, param).ToList();
            }
        }

        public int GetEqualsPatent(PatentDto patentDto)
        {
            using (IDbConnection db = new SqlConnection(_dbOptions.ConnectionString))
            {
                var param = new DynamicParameters();
                param.Add("RegNumber", patentDto.RegNumber);
                param.Add("PublicationPlace", patentDto.PublicationPlace);

                var querry = "SELECT * FROM Patents as P INNER JOIN Editions as E ON P.EditionId = E.EditionId " +
                    "WHERE PublicationPlace = @PublicationPlace AND RegNumber = @RegNumber; ";
                return (int)db.Query<decimal>(querry,param).FirstOrDefault();
            }
        }

        public PatentDto GetPatentById(int EditionId)
        {
            using (IDbConnection db = new SqlConnection(_dbOptions.ConnectionString))
            {
                var param = new DynamicParameters();
                param.Add("EditionId", EditionId);
                var querry = "SELECT * FROM Patents as P INNER JOIN Editions as E ON P.EditionId = E.EditionId WHERE E.EditionId = @EditionId;";
                return db.Query<PatentDto>(querry, param).First();
            }
        }

        public int UpdatePatent(PatentDto patentDto)
        {
            using (IDbConnection db = new SqlConnection(_dbOptions.ConnectionString))
            {
                var param = new DynamicParameters();
                param.Add("PatentId", patentDto.PatentId);
                param.Add("RegNumber", patentDto.RegNumber);
                param.Add("AppDate", patentDto.AppDate);
                param.Add("PublicationDate", patentDto.PublicationDate);

                var querry = "UPDATE Patents SET RegNumber = @RegNumber,AppDate = @AppDate,PublicationDate = @PublicationDate WHERE PatentId = @PatentId";

                db.Query<decimal>(querry, param);

                return 1;
            }
        }
    }
}
