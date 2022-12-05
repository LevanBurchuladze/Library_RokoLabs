
using AutoMapper;
using Library.Interfaces.Repository;
using Microsoft.Extensions.Logging;
using Library.Interfaces.Service;
using Library.Entities;
using System.Collections.Generic;
using System.Linq;
using Library.Dto;

namespace Library.Services
{
    public class PatentService : IPatentService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<PatentService> _logger;
        private readonly IPatentRepository _patentRepository;
        private readonly IAuthorRepository _authorRepository;

        public PatentService(ILogger<PatentService> logger, IMapper mapper,
            IAuthorRepository authorRepository, IPatentRepository patentRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _patentRepository = patentRepository;
            _authorRepository = authorRepository;
        }

        public int GetEqualsPatent(Patent patentDto)
        {
            throw new System.NotImplementedException();
        }

        public Patent GetPatentById(int id)
        {
            List<Patent> patents = new List<Patent>();
            patents.Add(_mapper.Map<Patent>(_patentRepository.GetPatentById(id)));
            var authorsPatents = _authorRepository.GetAuthorByPatentIds(patents.Select(p => p.PatentId));
            foreach (var patent in patents)
            {
                patent.Authors = _mapper.Map<List<Author>>(authorsPatents.Where(b => b.PatentId == patent.PatentId));
            }

            return patents[0];
        }

        public List<Patent> GetPatents()
        {
            List<Patent> patents = _mapper.Map<List<Patent>>(_patentRepository.GetPatents());
            if(patents != null)
            {
                var authorsPatents = _authorRepository.GetAuthorByPatentIds(patents.Select(p => p.PatentId));
                foreach (var patent in patents)
                {
                    patent.Authors = _mapper.Map<List<Author>>(authorsPatents.Where(b => b.BookId == patent.PatentId));
                }
            }
            return patents;
        }

        public List<Patent> GetPatentsByAuthor(Author newAuthor)
        {
            throw new System.NotImplementedException();
        }

        public List<Patent> GetPatentsByName(string title)
        {
            throw new System.NotImplementedException();
        }

        public int InsertPatent(PatentDto patentDto)
        {
            return _patentRepository.InsertPatent(patentDto);
        }

        public int UpdatePatent(PatentDto patentDto)
        {
            return _patentRepository.UpdatePatent(patentDto);
        }
    }
}
