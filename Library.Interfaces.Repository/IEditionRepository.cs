
using Library.Dto;
using Library.Entities;
using System.Collections.Generic;

namespace Library.Interfaces.Repository
{
    public interface IEditionRepository
    {
        int InsertEdition(EditionDto editionDto);
        int DeleteEdition(int editionId);
        List<EditionDto> GetEditionsByDateDesc();
        List<EditionDto> GetEditionsByDate();
        int UpdateEdition(EditionDto editionDto);
        List<EditionDto> GetEditions();
        List<EditionDto> GetEditionsInPage(int pg);
        int GetEditionCount();
    }
}
