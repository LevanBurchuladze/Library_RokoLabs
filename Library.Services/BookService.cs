
using AutoMapper;
using Library.Interfaces.Repository;
using Microsoft.Extensions.Logging;
using Library.Interfaces.Service;
using Library.Entities;
using System.Collections.Generic;
using System.Linq;
using System;
using Library.Dto;

namespace Library.Services
{
    public class BookService : IBookService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<BookService> _logger;
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;

        public BookService(ILogger<BookService> logger,IMapper mapper, 
            IAuthorRepository authorRepository,IBookRepository bookRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }

        public Book GetBookById(int id)
        {
            List<Book> books = new List<Book>();
            books.Add(_mapper.Map<Book>(_bookRepository.GetBookById(id)));
            var authorsBooks = _authorRepository.GetAuthorByBookIds(books.Select(p => p.BookId));
            foreach (var book in books)
            {
                book.Authors = _mapper.Map<List<Author>>(authorsBooks.Where(b => b.BookId == book.BookId));
            }
            return books[0];
        }

        public List<Book> Getbooks()
        {
            List<Book> books = _mapper.Map<List<Book>>(_bookRepository.Getbooks());
            var authorsBooks = _authorRepository.GetAuthorByBookIds(books.Select(p => p.BookId));
            foreach (var book in books)
            {
                book.Authors = _mapper.Map<List<Author>>(authorsBooks.Where(b => b.BookId == book.BookId));
            }
            return books;
        }

        public List<BookDto> GetBooksByAuthor(Author newAuthor)
        {
            throw new NotImplementedException();
        }

        public List<BookDto> GetbooksByName(string title)
        {
            return _bookRepository.GetbooksByName(title);
        }

        public List<BookDto> GetBooksByPubHouse(string pubHouse)
        {
            return _bookRepository.GetBooksByPubHouse(pubHouse);
        }

        public int GetEqualsBook(BookDto bookDto)
        {
            return _bookRepository.GetEqualsBook(bookDto);
        }

        public int InsertBook(BookDto bookDto)
        {
            return _bookRepository.InsertBook(bookDto);
        }

        public int UpdateBook(BookDto bookDto)
        {
            return _bookRepository.UpdateBook(bookDto);
        }
    }
}
