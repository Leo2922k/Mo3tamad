using API.DTO;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class ExamsController : BaseApiController 
    {
        private readonly IExamRepository _examRepository;
        private readonly IMapper _mapper;
        public ExamsController(IExamRepository examRepository, IMapper mapper) 
        {
            _mapper = mapper;
            _examRepository = examRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExamsDto>>> GetExams()
        {

            var exams = await _examRepository.GetExamsDtoAsync();
            //var exams = await _examRepository.GetExamsDtoAsync();
            // .Include(o => o.Questions)

            return Ok(exams);

        }

        [HttpGet("{examname}")]
        public async Task<ActionResult<ExamsDto>> GetExam(string examname)
        {
            return await _examRepository.GetExamDtoAsync(examname);

        }
    }
}