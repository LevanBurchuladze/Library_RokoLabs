
using AutoMapper;
using Library.Dto;
using Library.Entities;
using Library.Interfaces.Repository;
using Library.Interfaces.Service;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Library.Services
{
    public class NewsPaperService : INewsPaperService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<NewsPaperService> _logger;
        private readonly INewsPaperRepository _newsPaperRepository;

        public NewsPaperService(ILogger<NewsPaperService> logger, IMapper mapper,INewsPaperRepository newsPaperRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _newsPaperRepository = newsPaperRepository;
        }

        public string CheckISSN(NewsPaperDto newsPaper)
        {
            return _newsPaperRepository.CheckISSN(newsPaper);
        }

        public int GetEqualsNewsPaper(NewsPaperDto newsPaper)
        {
            return _newsPaperRepository.GetEqualsNewsPaper(newsPaper);
        }

        public List<NewsPaper> GetNewsByTitlePublisher(string title, string publisher)
        {
            return _mapper.Map<List<NewsPaper>>(_newsPaperRepository.GetNewsByTitlePublisher(title,publisher));
        }

        public NewsPaper GetNewsPaperById(int Editionid)
        {
            return _mapper.Map<NewsPaper>(_newsPaperRepository.GetNewsPaperById(Editionid));
        }

        public List<NewsPaper> GetNewsPapers()
        {
            return _mapper.Map<List<NewsPaper>>(_newsPaperRepository.GetNewsPapers());
        }

        public List<NewsPaper> GetNewsPapersByName(string title)
        {
            return _mapper.Map<List<NewsPaper>>(_newsPaperRepository.GetNewsPapersByName(title));
        }

        public int InsertNewsPaper(NewsPaperDto newsPaperDto)
        {
            return _newsPaperRepository.InsertNewsPaper(newsPaperDto);
        }

        public int UpdateNewsPaper(NewsPaperDto newsPaperDto)
        {
            return _newsPaperRepository.UpdateNewsPaper(newsPaperDto);
        }
    }
}
