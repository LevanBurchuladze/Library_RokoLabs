
using AutoMapper;
using Library.Dto;
using Library.Entities;
using Library.Interfaces.Repository;
using Library.Interfaces.Service;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Library.Services
{
    public class EditionService : IEditionService
    { 
        private readonly IMapper _mapper;
        private readonly IBookService _bookService;
        private readonly IPatentService _patentService;
        private readonly ILogger<EditionService> _logger;
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IPatentRepository _patentRepository;
        private readonly IEditionRepository _editionRepository;
        private readonly INewsPaperRepository _newsPaperRepository;

        public EditionService(ILogger<EditionService> logger,IEditionRepository editionRepository, 
            IMapper mapper, IAuthorRepository authorRepository,
            IBookRepository bookRepository, INewsPaperRepository newsPaperRepository,
            IPatentRepository patentRepository, IBookService bookService, IPatentService patentService)
        {
            _mapper = mapper;
            _logger = logger;
            _bookService = bookService;
            _patentService = patentService;
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _patentRepository = patentRepository;
            _editionRepository = editionRepository;
            _newsPaperRepository = newsPaperRepository;
        }

        public List<Edition> GetEditionsInPage(int pg)
        {
            var editions = _editionRepository.GetEditionsInPage(pg);
            List<Edition> resEditions = null;
            if (editions != null)
            {
                var newsPapersDto = _newsPaperRepository.GetNewsPapers();
                List<NewsPaper> newsPapers = _mapper.Map<List<NewsPaper>>(newsPapersDto);

                List<Book> books = _mapper.Map<List<Book>>(_bookService.Getbooks());

                List<Patent> patents = _mapper.Map<List<Patent>>(_patentService.GetPatents());

                resEditions = new List<Edition>();
                foreach (var item in editions)
                {
                    if (item.Type == 1)
                    {
                        foreach (var book in books)
                        {
                            if (item.EditionId == book.EditionId)
                            {
                                resEditions.Add(book);
                                break;
                            }
                        }
                    }
                    if (item.Type == 2)
                    {
                        foreach (var newsPaper in newsPapers)
                        {
                            if (item.EditionId == newsPaper.EditionId)
                            {
                                resEditions.Add(newsPaper);
                                break;
                            }
                        }
                    }
                    if (item.Type == 3)
                    {
                        foreach (var patent in patents)
                        {
                            if (item.EditionId == patent.EditionId)
                            {
                                resEditions.Add(patent);
                                break;
                            }
                        }
                    }
                }
            }
            return resEditions;
        }

        public List<Edition> GetEditions()
        {
            var editions = _editionRepository.GetEditions();
            List<Edition> resEditions = null;
            if (editions != null)
            {
                var newsPapersDto = _newsPaperRepository.GetNewsPapers();
                List<NewsPaper> newsPapers = _mapper.Map<List<NewsPaper>>(newsPapersDto);

                List<Book> books = _mapper.Map<List<Book>>(_bookService.Getbooks());

                List<Patent> patents = _mapper.Map<List<Patent>>(_patentService.GetPatents());

                resEditions = new List<Edition>();
                foreach (var item in editions)
                {
                    if(item.Type == 1)
                    {
                        foreach (var book in books)
                        {
                            if (item.EditionId == book.EditionId)
                            {
                                resEditions.Add(book);
                                break;
                            }
                        }
                    }
                    if (item.Type == 2)
                    {
                        foreach (var newsPaper in newsPapers)
                        {
                            if (item.EditionId == newsPaper.EditionId)
                            {
                                resEditions.Add(newsPaper);
                                break;
                            }
                        }
                    }
                    if (item.Type == 3)
                    {
                        foreach (var patent in patents)
                        {
                            if (item.EditionId == patent.EditionId)
                            {
                                resEditions.Add(patent);
                                break;
                            }
                        }
                    }
                }
            }
            return resEditions;
        }

        public int InsertEdition(Edition edition)
        {
            switch (edition.Type)
            {
                case 1:
                    return InsertBook((Book)edition);
                case 2:
                    return InsertNewsPaper((NewsPaper)edition);
                case 3:
                    return InsertPatent((Patent)edition);
            }
            return -1;
        }

        private int InsertNewsPaper(NewsPaper newsPaper)
        {
            if (newsPaper.ISSN != "")
            {
                string titleNwsPpr = _newsPaperRepository.CheckISSN(_mapper.Map<NewsPaperDto>(newsPaper));
                if (titleNwsPpr != null && titleNwsPpr != newsPaper.Title)
                {
                    _logger.LogInformation("Данный ISSN есть, но названия разнятся - запрещено");
                    return -1;
                }
                else
                {
                    int findNewsPaper = _newsPaperRepository.GetEqualsNewsPaper(_mapper.Map<NewsPaperDto>(newsPaper));
                    if (findNewsPaper != 0)
                    {
                        _logger.LogInformation("Данная газета уже была добавлена в каталог");
                        return -1;
                    }
                }
            }

            var NewsPaperDto = _mapper.Map<NewsPaperDto>(newsPaper);

            int EditionId = _editionRepository.InsertEdition(NewsPaperDto);
            NewsPaperDto.EditionId = EditionId;

            int newsPaperId = _newsPaperRepository.InsertNewsPaper(NewsPaperDto);

            return newsPaperId;
        }

        private int InsertPatent(Patent patent)
        {
            var patentDto = _mapper.Map<PatentDto>(patent);

            int findPatent = _patentRepository.GetEqualsPatent(patentDto);
            if (findPatent != 0)
            {
                _logger.LogInformation("Данный патент уже был добавлен в каталог");
                return -1;
            }

            int EditionId = _editionRepository.InsertEdition(patentDto);
            patentDto.EditionId = EditionId;

            int patentId = _patentRepository.InsertPatent(patentDto);

            foreach (var authorId in patent.Authors)
            {
                int tempAuthorid = _authorRepository.FindAuthor(_mapper.Map<AuthorDto>(authorId));
                if (tempAuthorid == 0)
                {
                    tempAuthorid = _authorRepository.InsertAuthor(_mapper.Map<AuthorDto>(authorId));
                }
                _authorRepository.InsertAuthorsPatents(patentId, tempAuthorid);
            }
            return patentId;
        }

        private int InsertBook(Book book)
        {
            var bookDto = _mapper.Map<BookDto>(book);

            int findBook = _bookRepository.GetEqualsBook(bookDto);
            if (findBook != 0)
            {
                _logger.LogInformation("Данная книга уже была добавлена в каталог,совпал ISBN");
                return -1;
            }

            List<Book> tmpBooks = _mapper.Map<List<Book>>(_bookRepository.Getbooks());
            var authorsBooks = _authorRepository.GetAuthorByBookIds(tmpBooks.Select(p => p.BookId));
            foreach (var tmpbook in tmpBooks)
            {
                tmpbook.Authors = _mapper.Map<List<Author>>(authorsBooks.Where(b => b.BookId == tmpbook.BookId));
            }

            foreach (var item in tmpBooks)
            {
                IEnumerable<Author> per = item.Authors.Intersect(book.Authors);
                int cnt = per.Count();
                if (item.Title == book.Title && item.PublicationYear == book.PublicationYear && cnt == book.Authors.Count)
                {
                    _logger.LogInformation("Данная книга уже была добавлена в каталог");
                    return -1;
                }
            }


            int EditionId = _editionRepository.InsertEdition(bookDto);
            bookDto.EditionId = EditionId;

            int bookId = _bookRepository.InsertBook(bookDto);

            foreach (var authorId in book.Authors)
            {
                int tempAuthorid = _authorRepository.FindAuthor(_mapper.Map<AuthorDto>(authorId));
                if (tempAuthorid == 0)
                {
                    tempAuthorid = _authorRepository.InsertAuthor(_mapper.Map<AuthorDto>(authorId));
                }
                _authorRepository.InsertAuthorsBooks(bookId, tempAuthorid);
            }
            return bookId;
        }

        public int DeleteEdition(int editionId)
        {
            return _editionRepository.DeleteEdition(editionId);
        }

        public List<Edition> GetEditionsByName(string title)
        {
            List<NewsPaper> newsPapers = _mapper.Map<List<NewsPaper>>(_newsPaperRepository.GetNewsPapersByName(title));

            List<Book> books = _mapper.Map<List<Book>>(_bookRepository.GetbooksByName(title));
            var authorsBooks = _authorRepository.GetAuthorByBookIds(books.Select(p => p.BookId));
            foreach (var book in books)
            {
                book.Authors = _mapper.Map<List<Author>>(authorsBooks.Where(b => b.BookId == book.BookId));
            }

            List<Patent> patents = _mapper.Map<List<Patent>>(_patentRepository.GetPatentsByName(title));
            var authorsPatents = _authorRepository.GetAuthorByPatentIds(patents.Select(p => p.PatentId));
            foreach (var patent in patents)
            {
                patent.Authors = _mapper.Map<List<Author>>(authorsPatents.Where(b => b.BookId == patent.PatentId));
            }

            List<Edition> editions = new List<Edition>();
            editions.AddRange(newsPapers);
            editions.AddRange(books);
            editions.AddRange(patents);

            return editions;
        }

        public List<Book> GetBooksByPubHouse(string pubHouse)
        {
            var books = _mapper.Map<List<Book>>(_bookRepository.GetBooksByPubHouse(pubHouse));
            var authorsBooks = _authorRepository.GetAuthorByBookIds(books.Select(p => p.BookId));
            foreach (var book in books)
            {
                book.Authors = _mapper.Map<List<Author>>(authorsBooks.Where(b => b.BookId == book.BookId));
            }
            return books;
        }

        public List<Edition> GetEditionsByDate()
        {
            return _mapper.Map<List<Edition>>(_editionRepository.GetEditionsByDate());
        }

        public List<Edition> GetEditionsByDateDesc()
        {
            return _mapper.Map<List<Edition>>(_editionRepository.GetEditionsByDateDesc());
        }

        public List<Book> GetBooksByAuthor(Author newAuthor)
        {
            var books = _mapper.Map<List<Book>>(_bookRepository.GetBooksByAuthor(newAuthor));
            var authorsBooks = _authorRepository.GetAuthorByBookIds(books.Select(p => p.BookId));
            foreach (var book in books)
            {
                book.Authors = _mapper.Map<List<Author>>(authorsBooks.Where(b => b.BookId == book.BookId));
            }
            return books;
        }

        public List<Patent> GetPatentsByAuthor(Author newAuthor)
        {
            var patents = _mapper.Map<List<Patent>>(_patentRepository.GetPatentsByAuthor(newAuthor));
            var authorsPatents = _authorRepository.GetAuthorByPatentIds(patents.Select(p => p.PatentId));
            foreach (var patent in patents)
            {
                patent.Authors = _mapper.Map<List<Author>>(authorsPatents.Where(p => p.PatentId == patent.PatentId));
            }
            return patents;
        }

        public int UpdateEdition(EditionDto editionDto)
        {
            return _editionRepository.UpdateEdition(editionDto);
        }

        public int GetEditionCount()
        {
            return _editionRepository.GetEditionCount();
        }
    }
}
