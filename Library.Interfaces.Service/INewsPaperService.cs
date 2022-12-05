
using Library.Dto;
using Library.Entities;
using System.Collections.Generic;

namespace Library.Interfaces.Service
{
    public interface INewsPaperService
    {
        int InsertNewsPaper(NewsPaperDto newsPaperDto);
        List<NewsPaper> GetNewsPapers();
        List<NewsPaper> GetNewsPapersByName(string title);
        string CheckISSN(NewsPaperDto newsPaper);
        int GetEqualsNewsPaper(NewsPaperDto newsPaper);
        NewsPaper GetNewsPaperById(int Editionid);
        List<NewsPaper> GetNewsByTitlePublisher(string title, string publisher);
        int UpdateNewsPaper(NewsPaperDto newsPaperDto);
    }
}
