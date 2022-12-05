using AutoMapper;
using Library.Entities;
using Library.Interfaces.Service;
using Library.UI.WebApi.infrastructures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Library.UI.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAuthorService _authorService;
        private readonly ILogger<BookController> _logger;

        public AuthorController(IMapper mapper, ILogger<BookController> logger,
            IAuthorService authorService)
        {
            _mapper = mapper;
            _logger = logger;
            _authorService = authorService;
        }

        [HttpGet]
        public ActionResult<List<Author>> Get()
        {
            List<Author> authors = _mapper.Map<List<Author>>(_authorService.GetAuthors());
            if (authors != null)
            {
                return authors;
            }
            else
            {
                authors = new List<Author>();
                return authors;
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [TypeFilter(typeof(AuditLogActionFilter))]
        public ActionResult<int> Post(Author author)
        {
            int res = _authorService.InsertAuthor(author);
            if (res != 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Author is not added");
            }
        }
    }
}
