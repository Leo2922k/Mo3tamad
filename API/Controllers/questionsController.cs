/*using API.DTO;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// editied


namespace API.Controllers
{
    [Authorize]
    public class questionsController : BaseApiController
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;
        public questionsController(IQuestionRepository questionRepository, IMapper mapper)
        {
            _mapper = mapper;
            _questionRepository = questionRepository;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionDto>>> GetQuestions()
        {

            var questions = await _questionRepository.GetQuestionsDtoAsync();

            return Ok(questions);

        }

        [HttpGet("{questionid}")]
        public async Task<ActionResult<QuestionDto>> GetQuestion(int questionid)
        {
            return await _questionRepository.GetQuestionDtoAsync(questionid);

        }

    }
}



    

       
*/

    
