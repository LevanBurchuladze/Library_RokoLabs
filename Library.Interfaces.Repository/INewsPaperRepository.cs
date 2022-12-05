
using Library.Dto;
using Library.Entities;
using System.Collections.Generic;

namespace Library.Interfaces.Repository
{
    public interface INewsPaperRepository
    {
        int InsertNewsPaper(NewsPaperDto newsPaperDto);
        List<NewsPaperDto> GetNewsPapers();
        List<NewsPaperDto> GetNewsPapersByName(string title);
        string CheckISSN(NewsPaperDto newsPaper);
        int GetEqualsNewsPaper(NewsPaperDto newsPaper);
        NewsPaperDto GetNewsPaperById(int Editionid);
        List<NewsPaperDto> GetNewsByTitlePublisher(string title, string publisher);
        int UpdateNewsPaper(NewsPaperDto newsPaperDto);
    }
}
