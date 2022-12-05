using AutoMapper;
using Library.Dto;
using Library.Entities;
using Library.Interfaces.Repository;
using Library.Interfaces.Service;
using Library.UI.WEB.Infrastructure;
using Library.UI.WEB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Library.UI.WEB.Controllers
{
    public class NewsPaperController : Controller
    {
        private readonly ILogger<NewsPaperController> _logger;
        private readonly IMapper _mapper;
        private readonly IEditionService _editionService;
        private readonly INewsPaperRepository _newsPaperRepository;

        public NewsPaperController(IEditionService editionService, INewsPaperRepository newsPaperRepository, 
            IMapper mapper,ILogger<NewsPaperController> logger)
        {
            _logger = logger;
            _mapper = mapper;
            _editionService = editionService;
            _newsPaperRepository = newsPaperRepository;
        }

        // GET: NewsPaperController
        public ActionResult NewsPaper(int id, string title, string publisher)
        {
            var newsPaper = _mapper.Map<NewsPaper>(_newsPaperRepository.GetNewsPaperById(id));
            var listNewsPapers = _mapper.Map<List<NewsPaper>>(_newsPaperRepository.GetNewsByTitlePublisher(title, publisher));
            FullNewsPaper fullNewsPaper = new FullNewsPaper() { newsPaper = newsPaper, listNewsPapers = listNewsPapers };
            return View(fullNewsPaper);
        }

        // GET: NewsPaperController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult WrongNewsPaper()
        {
            return View();
        }

        // GET: NewsPaperController/NewsPaperAdd
        [Authorize(Roles = "admin")]
        [TypeFilter(typeof(AuditLogActionFilter))]
        public ActionResult NewsPaperAdd()
        {
            ViewBag.News = _mapper.Map<List<NewsPaper>>(_newsPaperRepository.GetNewsPapers());
            return View();
        }

        // POST: NewsPaperController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        [TypeFilter(typeof(AuditLogActionFilter))]
        public ActionResult NewsPaperAdd(IFormCollection collection)
        {
            try
            {
                NewsPaper newsPaper = new NewsPaper()
                {
                    Title = collection["Title"],
                    Type = 2,
                    PublicationPlace = collection["PublicationPlace"],
                    PublicationHouse = collection["PublicationHouse"],
                    PublicationYear = Convert.ToInt32(collection["PublicationYear"]),
                    CountPages = Convert.ToInt32(collection["CountPages"]),
                    Description = collection["Description"],
                    Number = Convert.ToInt32(collection["Number"]),
                    ISSN = collection["ISSN"],
                    Date = Convert.ToDateTime(collection["Date"])
                };
                int editionId = _editionService.InsertEdition(newsPaper);
                if (editionId == -1)
                {
                    ViewBag.TypeWrong = "This newspaper is added";
                    return RedirectToAction(nameof(WrongNewsPaper));
                }
                else
                {
                    return RedirectToAction(nameof(NewsPaperAdd));
                }
            }
            catch
            {
                return RedirectToAction(nameof(WrongNewsPaper));
            }
        }

        // GET: NewsPaperController/Edit/5
        [Authorize(Roles = "admin")]
        [TypeFilter(typeof(AuditLogActionFilter))]
        public ActionResult NewsPaperEdit(int editionId)
        {
            return View(_mapper.Map<NewsPaper>(_newsPaperRepository.GetNewsPaperById(editionId)));
        }

        // POST: NewsPaperController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        [TypeFilter(typeof(AuditLogActionFilter))]
        public ActionResult NewsPaperEdit(IFormCollection collection)
        {
            try
            {
                NewsPaper updateNewsPaper = new NewsPaper()
                {
                    Title = collection["Title"],
                    Type = 2,
                    PublicationPlace = collection["PublicationPlace"],
                    PublicationHouse = collection["PublicationHouse"],
                    PublicationYear = Convert.ToInt32(collection["PublicationYear"]),
                    CountPages = Convert.ToInt32(collection["CountPages"]),
                    Description = collection["Description"],
                    ISSN = collection["ISSN"],
                    Number = Convert.ToInt32(collection["Number"]),
                    Date = Convert.ToDateTime(collection["Date"])
                };

                EditionDto editionDto = _mapper.Map<EditionDto>(updateNewsPaper);
                _editionService.UpdateEdition(editionDto);

                NewsPaperDto newsPaperDto = _mapper.Map<NewsPaperDto>(updateNewsPaper);
                _newsPaperRepository.UpdateNewsPaper(newsPaperDto);

                return RedirectToRoute(new { controller = "Edition", action = "Index" });
            }
            catch
            {
                return RedirectToAction(nameof(WrongNewsPaper));
            }
        }

        // GET: NewsPaperController/Delete/5
        [Authorize(Roles = "admin")]
        [TypeFilter(typeof(AuditLogActionFilter))]
        public ActionResult NewsPaperDelete(int editionId)
        {
            return View(_mapper.Map<NewsPaper>(_newsPaperRepository.GetNewsPaperById(editionId)));
        }

        // POST: NewsPaperController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        [TypeFilter(typeof(AuditLogActionFilter))]
        public ActionResult NewsPaperDelete(IFormCollection collection)
        {
            int res = _editionService.DeleteEdition(Convert.ToInt32(collection["EditionId"]));
            if (res != 1)
            {
                return RedirectToRoute(new { controller = "Edition", action = "Index" });
            }
            {
                return RedirectToAction(nameof(WrongNewsPaper));
            }
        }
    }
}
