
using Library.Dto;
using Library.Entities;
using System.Collections.Generic;

namespace Library.Interfaces.Service
{
    public interface IPatentService
    {
        List<Patent> GetPatents();
        int InsertPatent(PatentDto patentDto);
        Patent GetPatentById(int EditionId);
        int UpdatePatent(PatentDto patentDto);
        int GetEqualsPatent(Patent patentDto);
        List<Patent> GetPatentsByName(string title);
        List<Patent> GetPatentsByAuthor(Author newAuthor);
    }
}
