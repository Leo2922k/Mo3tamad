using API.Date;
using API.DTO;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace API.Data
{
    public class UserExamRepository : IUserExamRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserExamRepository (DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            
        }

        public void Update(UserExam userexam)
        {
            _context.Entry(userexam).State = EntityState.Modified; // just inform without changing
        }

        public void Delete(UserExam userExamToDelete)
        {
            _context.Remove(userExamToDelete);
        }


         public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<UserExam>> GetUsersExamsAsync()
        {
            return await _context.UserExam.ToListAsync();
        }

        public async Task<UserExam> GetUserExamByIdsAsync(int userid, int examid)
        {
            return await _context.UserExam
                .SingleOrDefaultAsync(ue => ue.Id == userid && ue.ExamId == examid);
        }

        public async Task<IEnumerable<UserExamDto>> GerUserExamDtosAsync()
        {

                return await _context.UserExam.ProjectTo<UserExamDto>
                       (_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<UserExamDto> GetUserExamDtoAsync(int id, int examid)
        {
            var userExamDto = await _context.UserExam
            .Where(x => x.Id == id && x.ExamId == examid)
            .Select(x => new UserExamDto
            {
                UserExamGrade = x.UserExamGrade

            })
            .FirstOrDefaultAsync();

            return userExamDto;
        }






    }
}