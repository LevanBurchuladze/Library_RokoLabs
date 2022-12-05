
using AutoMapper;
using Library.Dto;
using Library.Entities;
using Library.Interfaces.Repository;
using Library.Interfaces.Service;
using Library.UI.WEB.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.UI.WEB.Controllers
{
    public class BookController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorService _authorService;
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookService _bookService;
        private readonly IEditionService _editionService;
        private readonly ILogger<BookController> _logger;

        public BookController(IEditionService editionService, IBookRepository bookRepository, 
            IMapper mapper, IAuthorRepository authorRepository, 
            IBookService bookService, IAuthorService authorService,
            ILogger<BookController> logger)
        {
            _logger = logger;
            _authorService = authorService;
            _mapper = mapper;
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _editionService = editionService;
            _bookService = bookService;
        }

        // GET: BookController
        public ActionResult Book(int id)
        {
            Book book = _bookService.GetBookById(id);
            if(book == null)
            {
                return View();
            }
            return View(book);
        }

        // GET: BookController/BookAdd
        [Authorize(Roles = "admin")]
        [TypeFilter(typeof(AuditLogActionFilter))]
        public ActionResult BookAdd()
        {         
            ViewBag.Authors = _mapper.Map<List<Author>>(_authorRepository.GetAuthors());
            return View();
        }

        [HttpPost]
        public ActionResult AuthorAdd(string firName, string secName)
        {
            _authorRepository.InsertAuthor(new AuthorDto() {FirstName = firName, SecondName = secName});
            ViewBag.Authors = _mapper.Map<List<Author>>(_authorRepository.GetAuthors());
            return View("BookAdd");
        }

        public ActionResult WrongBook()
        {
            return View();
        }

        // POST: BookController/BookAdd
        [HttpPost]
        [Authorize(Roles = "admin")]
        [TypeFilter(typeof(AuditLogActionFilter))]
        [ValidateAntiForgeryToken]
        public ActionResult BookAdd(IFormCollection collection)
        {
            try
            {
                List<Author> lstAuthors = new List<Author>();
                string[] authors = Convert.ToString(collection["Authors"]).Split(',',' ');
                for(int i = 0; i < authors.Length - 1; i += 2)
                {
                    lstAuthors.Add(new Author { FirstName = authors[i], SecondName = authors[i+1] });
                }
                Book book = new Book()
                {
                    Title = collection["Title"],
                    Type = 1,
                    PublicationPlace = collection["PublicationPlace"],
                    PublicationHouse = collection["PublicationHouse"],
                    PublicationYear = Convert.ToInt32(collection["PublicationYear"]),
                    CountPages = Convert.ToInt32(collection["CountPages"]),
                    Description = collection["Description"],
                    ISBN = collection["ISBN"],
                    Authors = lstAuthors
                };
                int editionId = _editionService.InsertEdition(book);
                if (editionId == -1)
                {
                    return RedirectToAction(nameof(WrongBook));
                }
                else
                {
                    return RedirectToAction(nameof(BookAdd));
                }
            }
            catch
            {
                try
                {
                    string[] authors = Convert.ToString(collection["Authors"]).Split(',', ' ');
                    if(authors.Length == 2)
                    {
                        _authorRepository.InsertAuthor(new AuthorDto() {FirstName = authors[0], SecondName = authors[1]} );
                    }
                }
                catch (WrongData)
                {
                    return RedirectToAction(nameof(WrongBook));
                }
                return RedirectToAction(nameof(WrongBook));
            }
        }

        // GET: BookController/Edit/5
        [Authorize(Roles = "admin")]
        [TypeFilter(typeof(AuditLogActionFilter))]
        public ActionResult BookEdit(int editionId)
        {
            ViewBag.Authors = _mapper.Map<List<Author>>(_authorRepository.GetAuthors());
            return View(_bookService.GetBookById(editionId));
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        [TypeFilter(typeof(AuditLogActionFilter))]
        public ActionResult BookEdit(IFormCollection collection)
        {
            try
            {
                List<Author> lstAuthors = new List<Author>();
                string[] authors;

                try
                {
                    authors = Convert.ToString(collection["selAuthors"]).Split(',', ' ');
                }
                catch
                {
                    authors = new string[0];
                }

                for (int i = 0; i < authors.Length - 1; i += 3)
                {
                    lstAuthors.Add(new Author { AuthorId = Convert.ToInt32(authors[i]), FirstName = authors[i + 1], SecondName = authors[i + 2] });
                }

                Book updateBook = new Book()
                {
                    EditionId = Convert.ToInt32(collection["EditionId"]),
                    Title = collection["Title"],
                    Type = 1,
                    PublicationPlace = collection["PublicationPlace"],
                    PublicationHouse = collection["PublicationHouse"],
                    PublicationYear = Convert.ToInt32(collection["PublicationYear"]),
                    CountPages = Convert.ToInt32(collection["CountPages"]),
                    Description = collection["Description"],
                    ISBN = collection["ISBN"],
                    Authors = lstAuthors
                };
                EditionDto editionDto = _mapper.Map<EditionDto>(updateBook);
                _editionService.UpdateEdition(editionDto);

                BookDto bookDto = _mapper.Map<BookDto>(updateBook);
                _bookRepository.UpdateBook(bookDto);

                Book book = _bookService.GetBookById(updateBook.EditionId);

                List<Author> newAuthors = updateBook.Authors.Except<Author>(book.Authors).ToList();
                List<Author> deleteAuthors = book.Authors.Except<Author>(updateBook.Authors).ToList();

                _authorService.UpdateAuthorsBook(book.BookId, newAuthors, deleteAuthors);
                return RedirectToRoute(new { controller = "Edition", action = "Index" });
            }
            catch
            {
                return RedirectToAction(nameof(WrongBook));
            }
        }

        // GET: BookController/Delete/5
        [Authorize(Roles = "admin")]
        [TypeFilter(typeof(AuditLogActionFilter))]
        public ActionResult BookDelete(int editionId)
        {
            return View(_bookService.GetBookById(editionId));
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        [TypeFilter(typeof(AuditLogActionFilter))]
        public ActionResult BookDelete(IFormCollection collection)
        {
            int res = _editionService.DeleteEdition(Convert.ToInt32(collection["EditionId"]));
            return RedirectToRoute(new { controller = "Edition", action = "Index" });
        }
    }
}
