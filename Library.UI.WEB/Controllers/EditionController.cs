using AutoMapper;
using Library.Entities;
using Library.Interfaces.Repository;
using Library.Interfaces.Service;
using Library.UI.WEB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Library.UI.WEB.Controllers
{
    public class EditionController : Controller
    {
        private readonly ILogger<EditionController> _logger;
        private readonly IEditionService _editionService;

        public EditionController(ILogger<EditionController> logger, IEditionService editionService)
        {
            _logger = logger;
            _editionService = editionService;
        }

        // GET: EditionController
        public ActionResult Index(int pg = 1)
        {
            var editions = _editionService.GetEditionsInPage(pg);//<>

            const int pageSize = 10;
            if (pg < 1)
            {
                pg = 1;
            }

            int resCount = _editionService.GetEditionCount();//<>
            var pager = new Pager(resCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;

            this.ViewBag.Pager = pager;

            return View(editions);
        }
    }
}
