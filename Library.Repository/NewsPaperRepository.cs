
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
    public class NewsPaperRepository : INewsPaperRepository
    {
        private readonly ILogger<NewsPaperRepository> _logger;
        private readonly DbOptions _dbOptions;

        public NewsPaperRepository(ILogger<NewsPaperRepository> logger, DbOptions dbOptions)
        {
            _logger = logger;
            _dbOptions = dbOptions;
        }

        public string CheckISSN(NewsPaperDto newsPaper)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_dbOptions.ConnectionString))
                {
                    var param = new DynamicParameters();
                    param.Add("ISSN", newsPaper.ISSN);

                    var querry = "SELECT Title FROM NewsPapers as N INNER JOIN Editions as E ON N.EditionId = E.EditionId WHERE ISSN = @ISSN;";
                    return db.Query<string>(querry,param).ToList().FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message);
                return null;
            }
        }

        public int GetEqualsNewsPaper(NewsPaperDto newsPaper)
        {
            using (IDbConnection db = new SqlConnection(_dbOptions.ConnectionString))
            {
                var param = new DynamicParameters();
                param.Add("Title", newsPaper.Title);
                param.Add("PublicationHouse", newsPaper.PublicationHouse);
                param.Add("Date", newsPaper.Date);

                var querry = "SELECT * FROM NewsPapers as N " +
                    "INNER JOIN Editions as E ON n.EditionId = E.EditionId " +
                    "WHERE Title = @Title AND PublicationHouse = @PublicationHouse AND Date = @Date; ";
                return (int)db.Query<decimal>(querry, param).FirstOrDefault();
            }
        }

        public List<NewsPaperDto> GetNewsPapers()
        {
            using (IDbConnection db = new SqlConnection(_dbOptions.ConnectionString))
            {
                var querry = "SELECT * FROM Editions as N INNER JOIN NewsPapers as E ON N.EditionId = E.EditionId;";
                return db.Query<NewsPaperDto>(querry).ToList();
            }
        }

        public List<NewsPaperDto> GetNewsPapersByName(string title)
        {
            using (IDbConnection db = new SqlConnection(_dbOptions.ConnectionString))
            {
                var param = new DynamicParameters();
                param.Add("Title", title);
                var querry = "SELECT * FROM NewsPapers as N INNER JOIN Editions as E ON N.EditionId = E.EditionId WHERE E.Title = @Title;";
                return db.Query<NewsPaperDto>(querry,param).ToList();
            }
        }

        public NewsPaperDto GetNewsPaperById(int Editionid)
        {
            using (IDbConnection db = new SqlConnection(_dbOptions.ConnectionString))
            {
                var param = new DynamicParameters();
                param.Add("EditionId", Editionid);
                var querry = "SELECT * FROM NewsPapers as N INNER JOIN Editions as E ON N.EditionId = E.EditionId WHERE E.EditionId = @EditionId;";
                return db.Query<NewsPaperDto>(querry, param).First();
            }
        }

        public int InsertNewsPaper(NewsPaperDto newsPaperDto)
        {
            using (IDbConnection db = new SqlConnection(_dbOptions.ConnectionString))
            {
                var querry = "INSERT INTO NewsPapers (EditionId, PublicationHouse, Number, Date, ISSN) " +
                    "VALUES (@EditionId, @PublicationHouse, @Number, @Date, @ISSN); SELECT SCOPE_IDENTITY();";
                return (int)(db.Query<decimal>(querry, newsPaperDto).First());
            }
        }

        public List<NewsPaperDto> GetNewsByTitlePublisher(string title, string publisher)
        {
            using (IDbConnection db = new SqlConnection(_dbOptions.ConnectionString))
            {
                var param = new DynamicParameters();
                param.Add("Title", title);
                param.Add("PublicationHouse", publisher);
                var querry = "SELECT * FROM NewsPapers as N INNER JOIN Editions as E ON N.EditionId = E.EditionId " +
                    "WHERE Title = @Title AND PublicationHouse = @PublicationHouse " +
                    "ORDER BY PublicationYear ;";
                return db.Query<NewsPaperDto>(querry, param).ToList();
            }
        }

        public int UpdateNewsPaper(NewsPaperDto newsPaperDto)
        {
            using (IDbConnection db = new SqlConnection(_dbOptions.ConnectionString))
            {
                var param = new DynamicParameters();
                param.Add("NewsPaperId", newsPaperDto.NewsPaperId);
                param.Add("PublicationHouse", newsPaperDto.PublicationHouse);
                param.Add("Number", newsPaperDto.Number);
                param.Add("Date", newsPaperDto.Date);
                param.Add("ISSN", newsPaperDto.ISSN);

                var querry = "UPDATE NewsPapers SET PublicationHouse = @PublicationHouse,Number = @Number,Date = @Date,ISSN = @ISSN WHERE NewsPaperId = @NewsPaperId";

                db.Query<decimal>(querry, param);

                return 1;
            }
        }
    }
}
