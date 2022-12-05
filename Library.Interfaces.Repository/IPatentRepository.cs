
using Library.Dto;
using Library.Entities;
using System.Collections.Generic;

namespace Library.Interfaces.Repository
{
    public interface IPatentRepository
    {
        List<PatentDto> GetPatents();
        int InsertPatent(PatentDto patentDto);
        PatentDto GetPatentById(int EditionId);
        int UpdatePatent(PatentDto patentDto);
        int GetEqualsPatent(PatentDto patentDto);
        List<PatentDto> GetPatentsByName(string title);
        List<PatentDto> GetPatentsByAuthor(Author newAuthor);
    }
}
