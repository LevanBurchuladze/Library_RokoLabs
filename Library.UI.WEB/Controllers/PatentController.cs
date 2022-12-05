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
    public class PatentController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPatentRepository _patentRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IEditionService _editionService;
        private readonly IPatentService _patentService;
        private readonly IAuthorService _authorService;
        private readonly ILogger<PatentController> _logger;

        public PatentController(IEditionService editionService, IPatentRepository patentRepository, 
            IMapper mapper, IAuthorRepository authorRepository, 
            IPatentService patentService, IAuthorService authorService,
            ILogger<PatentController> logger)
        {
            _logger = logger;
            _authorService = authorService;
            _editionService = editionService; 
            _mapper = mapper;
            _patentRepository = patentRepository;
            _authorRepository = authorRepository;
            _patentService = patentService;
        }

        // GET: PatentController
        public ActionResult Patent(int id)
        {
            Patent patent = _patentService.GetPatentById(id);
            if (patent == null)
            {
                return View();
            }
            return View(_patentService.GetPatentById(id));
        }

        // GET: PatentController/PatentAdd
        [Authorize(Roles = "admin")]
        [TypeFilter(typeof(AuditLogActionFilter))]
        public ActionResult PatentAdd()
        {
            ViewBag.Authors = _mapper.Map<List<Author>>(_authorRepository.GetAuthors());
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [TypeFilter(typeof(AuditLogActionFilter))]
        public ActionResult AuthorAdd(string firName, string secName)
        {
            _authorRepository.InsertAuthor(new AuthorDto() { FirstName = firName, SecondName = secName });
            ViewBag.Authors = _mapper.Map<List<Author>>(_authorRepository.GetAuthors());
            return View("PatentAdd");
        }

        public ActionResult WrongPatent()
        {
            return View();
        }

        // POST: PatentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        [TypeFilter(typeof(AuditLogActionFilter))]
        public ActionResult PatentAdd(IFormCollection collection)
        {
            try
            {
                List<Author> lstAuthors = new List<Author>();
                string[] authors = Convert.ToString(collection["Authors"]).Split(',', ' ');
                for (int i = 0; i < authors.Length - 1; i += 2)
                {
                    lstAuthors.Add(new Author { FirstName = authors[i], SecondName = authors[i + 1] });
                }
                Patent patent = new Patent()
                {
                    Title = collection["Title"],
                    Type = 3,
                    PublicationPlace = collection["PublicationPlace"],
                    RegNumber = Convert.ToInt32(collection["RegNumber"]),
                    AppDate = Convert.ToDateTime(collection["AppDate"]),
                    PublicationDate = Convert.ToDateTime(collection["PublicationDate"]),
                    PublicationYear = Convert.ToInt32(collection["PublicationYear"]),
                    CountPages = Convert.ToInt32(collection["CountPages"]),
                    Description = collection["Description"],
                    Authors = lstAuthors
                };
                int editionId = _editionService.InsertEdition(patent);
                if (editionId == -1)
                {
                    return RedirectToAction(nameof(WrongPatent));
                }
                else
                {
                    return RedirectToAction(nameof(PatentAdd));
                }
            }
            catch
            {
                try
                {
                    string[] authors = Convert.ToString(collection["Authors"]).Split(',', ' ');
                    if (authors.Length == 2)
                    {
                        _authorRepository.InsertAuthor(new AuthorDto() { FirstName = authors[0], SecondName = authors[1] });
                    }
                }
                catch (WrongData)
                {
                    return RedirectToAction(nameof(WrongPatent));
                }
                return RedirectToAction(nameof(WrongPatent));
            }
        }

        // GET: PatentController/Edit/5
        [Authorize(Roles = "admin")]
        [TypeFilter(typeof(AuditLogActionFilter))]
        public ActionResult PatentEdit(int editionId)
        {
            ViewBag.Authors = _mapper.Map<List<Author>>(_authorRepository.GetAuthors());
            return View(_patentService.GetPatentById(editionId));
        }

        // POST: PatentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        [TypeFilter(typeof(AuditLogActionFilter))]
        public ActionResult PatentEdit(IFormCollection collection)
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

                Patent updatePatent = new Patent()
                {
                    EditionId = Convert.ToInt32(collection["EditionId"]),
                    Title = collection["Title"],
                    Type = 3,
                    PublicationPlace = collection["PublicationPlace"],
                    RegNumber = Convert.ToInt32(collection["RegNumber"]),
                    AppDate = Convert.ToDateTime(collection["AppDate"]),
                    PublicationDate = Convert.ToDateTime(collection["PublicationDate"]),
                    PublicationYear = Convert.ToInt32(collection["PublicationYear"]),
                    CountPages = Convert.ToInt32(collection["CountPages"]),
                    Description = collection["Description"],
                    Authors = lstAuthors
                };

                EditionDto editionDto = _mapper.Map<EditionDto>(updatePatent);
                _editionService.UpdateEdition(editionDto);
                
                PatentDto patentDto = _mapper.Map<PatentDto>(updatePatent);
                _patentRepository.UpdatePatent(patentDto);

                Patent patent = _patentService.GetPatentById(updatePatent.EditionId);


                List<Author> newAuthors = updatePatent.Authors.Except<Author>(patent.Authors).ToList();
                List<Author> deleteAuthors = patent.Authors.Except<Author>(updatePatent.Authors).ToList();

                _authorService.UpdateAuthorsPatent(patent.PatentId, newAuthors, deleteAuthors);

                return RedirectToRoute(new { controller = "Edition", action = "Index" });
            }
            catch
            {
                return RedirectToAction(nameof(WrongPatent));
            }
        }

        // GET: PatentController/Delete/5
        [Authorize(Roles = "admin")]
        [TypeFilter(typeof(AuditLogActionFilter))]
        public ActionResult PatentDelete(int editionId)
        {
            return View(_patentService.GetPatentById(editionId));
        }

        // POST: PatentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        [TypeFilter(typeof(AuditLogActionFilter))]
        public ActionResult PatentDelete(int id, IFormCollection collection)
        {
            int res = _editionService.DeleteEdition(Convert.ToInt32(collection["EditionId"]));
            if (res != -1)
            {
                return RedirectToRoute(new { controller = "Edition", action = "Index" });
            }
            else
            {
                return RedirectToAction(nameof(WrongPatent));
            }
        }
    }
}
