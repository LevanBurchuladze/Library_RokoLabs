
using AutoMapper;
using Library.Dto;
using Library.Entities;
using Library.Interfaces.Repository;
using Library.Interfaces.Service;
using Library.UI.WebApi.infrastructures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Library.UI.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;
        private readonly ILogger<BookController> _logger;
        private readonly IEditionService _editionService;

        public BookController(IEditionService editionService, IMapper mapper,
            IBookService bookService, IAuthorService authorService, ILogger<BookController> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _bookService = bookService;
            _authorService = authorService;
            _editionService = editionService;
        }

        [HttpGet("{id}")]
        public ActionResult<Book> Get(int id)
        {
            Book book = _bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound("Book is not found");
            }
            return book;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [TypeFilter(typeof(AuditLogActionFilter))]
        public ActionResult<int> Post(Book book)
        {
            int editionId = _editionService.InsertEdition(book);
            if (editionId == -1)
            {
                return BadRequest("This book is already added");
            }
            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        [TypeFilter(typeof(AuditLogActionFilter))]
        public ActionResult<int> Put(Book updatebook)
        {
            try
            {
                EditionDto editionDto = _mapper.Map<EditionDto>(updatebook);
                _editionService.UpdateEdition(editionDto);

                BookDto bookDto = _mapper.Map<BookDto>(updatebook);
                _bookService.UpdateBook(bookDto);

                Book book = _bookService.GetBookById(updatebook.EditionId);

                List<Author> newAuthors = updatebook.Authors.Except<Author>(book.Authors).ToList();
                List<Author> deleteAuthors = book.Authors.Except<Author>(updatebook.Authors).ToList();

                _authorService.UpdateAuthorsBook(book.BookId, newAuthors, deleteAuthors);
                
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        /*[Authorize(Roles = "admin")]*/
        [TypeFilter(typeof(AuditLogActionFilter))]
        public ActionResult<int> Delete(int id)
        {
            int res = _editionService.DeleteEdition(id);
            if (res == 0)
            {
                return BadRequest("Error! Book is not deleted");
            }
            return Ok();
        }
    }
}
