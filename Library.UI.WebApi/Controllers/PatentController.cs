
using AutoMapper;
using Library.Dto;
using Library.Entities;
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
    public class PatentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPatentService _patentService;
        private readonly IEditionService _editionService;
        private readonly ILogger<PatentController> _logger;

        public PatentController(IEditionService editionService,IMapper mapper,IPatentService patentService, 
            ILogger<PatentController> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _patentService = patentService;
            _editionService = editionService;
        }

        [HttpGet("{id}")]
        public ActionResult<Patent> Get(int id)
        {
            Patent patent = _patentService.GetPatentById(id);
            if (patent == null)
            {
                return NotFound("Patent is not found");
            }
            return patent;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [TypeFilter(typeof(AuditLogActionFilter))]
        public ActionResult<int> Post(Patent patent)
        {
            int editionId = _editionService.InsertEdition(patent);
            if (editionId == -1)
            {
                return BadRequest("This patent is already added");
            }
            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        [TypeFilter(typeof(AuditLogActionFilter))]
        public ActionResult<int> Put(Patent updatePatent)
        {
            try
            {
                EditionDto editionDto = _mapper.Map<EditionDto>(updatePatent);
                _editionService.UpdateEdition(editionDto);

                PatentDto patentDto = _mapper.Map<PatentDto>(updatePatent);
                _patentService.UpdatePatent(patentDto);

                Patent patent = _patentService.GetPatentById(updatePatent.EditionId);

                List<Author> newAuthors = updatePatent.Authors.Except<Author>(patent.Authors).ToList();
                List<Author> deleteAuthors = patent.Authors.Except<Author>(updatePatent.Authors).ToList();

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
                return BadRequest("Error! Patent is not deleted");
            }
            return Ok();
        }
    }
}
