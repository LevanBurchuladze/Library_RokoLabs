
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
    public class EditionRepository : IEditionRepository
    {
        private ILogger<EditionRepository> _logger;
        private DbOptions _dbOptions;

        public EditionRepository(ILogger<EditionRepository> logger, DbOptions dbOptions)
        {
            _logger = logger;
            _dbOptions = dbOptions;
        }

        public int DeleteEdition(int editionId)
        {
            using (SqlConnection db = new SqlConnection(_dbOptions.ConnectionString))
            {
                var comand = new SqlCommand($"DELETE FROM Editions WHERE EditionId = {editionId}",db);
                db.Open();
                return (int)comand.ExecuteNonQuery();
            }
        }

        public int GetEditionCount()
        {
            using (IDbConnection db = new SqlConnection(_dbOptions.ConnectionString))
            {
                var querry = "SELECT COUNT(*) FROM Editions";
                return (int)db.Query<decimal>(querry).First();
            }
        }

        public List<EditionDto> GetEditions()
        {
            using (IDbConnection db = new SqlConnection(_dbOptions.ConnectionString))
            {
                var querry = "SELECT * FROM Editions";
                return db.Query<EditionDto>(querry).ToList();
            }
        }

        public List<EditionDto> GetEditionsByDate()
        {
            using (IDbConnection db = new SqlConnection(_dbOptions.ConnectionString))
            {
                var querry = "SELECT * FROM Editions ORDER BY PublicationYear";
                return db.Query<EditionDto>(querry).ToList();
            }
        }

        public List<EditionDto> GetEditionsByDateDesc()
        {
            using (IDbConnection db = new SqlConnection(_dbOptions.ConnectionString))
            {
                var querry = "SELECT * FROM Editions ORDER BY PublicationYear DESC";
                return db.Query<EditionDto>(querry).ToList();
            }
        }

        public List<EditionDto> GetEditionsInPage(int pg)
        {
            using (IDbConnection db = new SqlConnection(_dbOptions.ConnectionString))
            {
                var param = new DynamicParameters();
                param.Add("pg", pg);
                var querry = "SELECT * " +
                    "FROM Editions " +
                    "ORDER BY EditionId " +
                    "OFFSET (@pg-1)*10 ROWS " +
                    "FETCH NEXT 10 ROWS ONLY";
                return db.Query<EditionDto>(querry,param).ToList();
            }
        }

        public int InsertEdition(EditionDto editionDto)
        {
            using (IDbConnection db = new SqlConnection(_dbOptions.ConnectionString))
            {
                var querry = "INSERT INTO Editions (Type, Title, PublicationPlace, PublicationYear, CountPages, Description) " +
                    "VALUES (@Type, @Title, @PublicationPlace, @PublicationYear, @CountPages, @Description); SELECT SCOPE_IDENTITY();";
                return (int)(db.Query<decimal>(querry, editionDto).First());
            }
        }

        public int UpdateEdition(EditionDto editionDto)
        {
            using (IDbConnection db = new SqlConnection(_dbOptions.ConnectionString))
            {
                var param = new DynamicParameters();
                param.Add("EditionId", editionDto.EditionId);
                param.Add("Title", editionDto.Title);
                param.Add("PublicationPlace", editionDto.PublicationPlace);
                param.Add("CountPages", editionDto.CountPages);
                param.Add("Description", editionDto.Description);

                var querry = "UPDATE Editions SET Title = @Title,PublicationPlace = @PublicationPlace,CountPages = @CountPages,Description = @Description WHERE EditionId = @EditionId";
                
                db.Query<decimal>(querry, param);

                return 1;
            }
        }
    }
}
