/*using API.Date;
using API.DTO;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using AutoMapper;

//editiededitied

namespace API.Data
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public QuestionRepository (DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            
        }

        public async Task<Question> GetQuestionByIdAsync(int questionid)
        {
            return await _context.Questions.FindAsync(questionid);
        }

        public async Task<IEnumerable<Question>> GetQuestionsAsync()
        {
            return await _context.Questions.ToListAsync();
        }

         public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(Question question)
        {
            _context.Entry(question).State = EntityState.Modified; // just inform without changing
        }

        public async Task<IEnumerable<QuestionDto>> GetQuestionsDtoAsync()
        {
                return await _context.Questions
                    .ProjectTo<QuestionDto> (_mapper.ConfigurationProvider)
                    .ToListAsync();
        }

        public async Task<QuestionDto> GetQuestionDtoAsync(int questionid)
        {
            return await _context.Questions.
                Where(x => x.QuestionId == questionid)
                .ProjectTo<QuestionDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

    }
}



        
        */

       




        

