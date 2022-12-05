
using AutoMapper;
using Library.Dto;
using Library.Entities;
using Library.Interfaces.Service;
using Library.UI.WebApi.infrastructures;
using Library.UI.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Library.UI.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsPaperController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEditionService _editionService;
        private readonly INewsPaperService _newsPaperService;
        private readonly ILogger<NewsPaperController> _logger;

        public NewsPaperController(IEditionService editionService, INewsPaperService newsPaperService,
            IMapper mapper, ILogger<NewsPaperController> logger)
        {
            _logger = logger;
            _mapper = mapper;
            _editionService = editionService;
            _newsPaperService = newsPaperService;
        }

        [HttpGet("{id}")]
        public ActionResult<FullNewsPaper> Get(int id)
        {
            var newsPaper = _mapper.Map<NewsPaper>(_newsPaperService.GetNewsPaperById(id));
            if (newsPaper == null)
            {
                return NotFound("NewsPaper is not found");
            }

            var listNewsPapers = _mapper.Map<List<NewsPaper>>(_newsPaperService.GetNewsByTitlePublisher(newsPaper.Title, newsPaper.PublicationHouse));
            FullNewsPaper fullNewsPaper = new FullNewsPaper() { newsPaper = newsPaper, listNewsPapers = listNewsPapers };
            
            return fullNewsPaper;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [TypeFilter(typeof(AuditLogActionFilter))]
        public ActionResult<int> Post(NewsPaper newsPaper)
        {
            int editionId = _editionService.InsertEdition(newsPaper);
            if (editionId == -1)
            {
                return BadRequest("This newspaper is already added");
            }
            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        [TypeFilter(typeof(AuditLogActionFilter))]
        public ActionResult<int> Put(NewsPaper updateNewsPaper)
        {
            try
            {
                EditionDto editionDto = _mapper.Map<EditionDto>(updateNewsPaper);
                _editionService.UpdateEdition(editionDto);

                NewsPaperDto newsPaperDto = _mapper.Map<NewsPaperDto>(updateNewsPaper);
                _newsPaperService.UpdateNewsPaper(newsPaperDto);

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        [TypeFilter(typeof(AuditLogActionFilter))]
        public ActionResult<int> Delete(int id)
        {
            int res = _editionService.DeleteEdition(id);
            if (res == 0)
            {
                return BadRequest("Error! NewsPaper is not deleted");
            }
            return Ok();
        }
    }
}
