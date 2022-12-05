
using Library.Entities;
using Library.Interfaces.Service;
using Library.UI.WebApi.infrastructures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Library.UI.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EditionController : ControllerBase
    {
        private readonly IEditionService _editionService;
        private readonly ILogger<EditionController> _logger;

        public EditionController(ILogger<EditionController> logger, IEditionService editionService)
        {
            _logger = logger;
            _editionService = editionService;
        }

/*        [HttpGet]
        public ActionResult<List<Edition>> Get()
        {
            List<Edition> editions = _editionService.GetEditions();
            if (editions == null)
            {
                return BadRequest("Error! Editions is null");
            }
            return editions;
        }*/

        [HttpGet("{pg}")]
        public ActionResult<List<Edition>> Get(int pg = 1)
        {
            List<Edition> editions = _editionService.GetEditionsInPage(pg);
            if (editions == null)
            {
                return BadRequest("Error! Editions is null");
            }
            return editions;
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        [TypeFilter(typeof(AuditLogActionFilter))]
        public ActionResult<int> Delete(int id)
        {
            int res = _editionService.DeleteEdition(id);
            if (res == 0)
            {
                return BadRequest("Error! Edition is not deleted");
            }
            return Ok();
        }

        [HttpGet]
        [TypeFilter(typeof(AuditLogActionFilter))]
        public ActionResult<int> GetTotalEditions()
        {
            return Ok(_editionService.GetEditionCount());
        }
    }
}
