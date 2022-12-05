
using Library.Dto;
using Library.Entities;
using System.Collections.Generic;

namespace Library.Interfaces.Service
{
    public interface IEditionService
    {
        List<Edition> GetEditions();
        int InsertEdition(Edition edition);
        int DeleteEdition(int editionId);
        List<Edition> GetEditionsByName(string title);
        List<Book> GetBooksByPubHouse(string pubHouse);
        List<Edition> GetEditionsByDate();
        List<Book> GetBooksByAuthor(Author newAuthor);
        List<Edition> GetEditionsByDateDesc();
        List<Patent> GetPatentsByAuthor(Author newAuthor);
        int UpdateEdition(EditionDto editionDto);
        List<Edition> GetEditionsInPage(int pg);
        int GetEditionCount();
    }
}
