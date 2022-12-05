
using AutoMapper;
using Library.Dto;
using Library.Entities;
using Library.Interfaces.Repository;
using Library.Interfaces.Service;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Library.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<AuthorService> _logger;
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(ILogger<AuthorService> logger, IMapper mapper,IAuthorRepository authorRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _authorRepository = authorRepository;
        }

        public List<AuthorDto> GetAuthors()
        {
            return _authorRepository.GetAuthors();
        }

        public int InsertAuthor(Author authorDto)
        {
            return _authorRepository.InsertAuthor(_mapper.Map<AuthorDto>(authorDto));
        }

        public void UpdateAuthorsBook(int bookId, List<Author> newAuthors, List<Author> deleteAuthors)
        {
            foreach (var author in newAuthors)
            {
                _authorRepository.UpdateAuthorsBook(bookId, author.AuthorId);
            }

            foreach (var author in deleteAuthors)
            {
                _authorRepository.DeleteAuthorsBook(bookId, author.AuthorId);
            }
        }

        public void UpdateAuthorsPatent(int patentId, List<Author> newAuthors, List<Author> deleteAuthors)
        {
            foreach (var author in newAuthors)
            {
                _authorRepository.UpdateAuthorsPatent(patentId, author.AuthorId);
            }

            foreach (var author in deleteAuthors)
            {
                _authorRepository.DeleteAuthorsPatent(patentId, author.AuthorId);
            }
        }
    }
}
