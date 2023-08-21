using API.Date;
using API.DTO;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace API.Data
{
    public class ExamRepository : IExamRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ExamRepository (DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            
        }

        public async Task<AppExams> GetExamByIdAsync(int examid)
        {
            return await _context.Exams.FindAsync(examid);
        }

        public async Task<AppExams> GetExamByExamnameAsync(string examname)
        {
            return await _context.Exams.Include(p => p.ExamPicture).Include(p => p.UserExams).SingleOrDefaultAsync(x => x.ExamName == examname);
        }

        public async Task<IEnumerable<AppExams>> GetExamsAsync()
        {
            return await _context.Exams.Include(p => p.ExamPicture).Include(p => p.UserExams).ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(AppExams exam)
        {
            _context.Entry(exam).State = EntityState.Modified; // just inform without changing
        }

        public async Task<IEnumerable<ExamsDto>> GetExamsDtoAsync()
        {
                return await _context.Exams
                    .ProjectTo<ExamsDto> (_mapper.ConfigurationProvider)
                    .ToListAsync();
        }
        
        public async Task<ExamsDto> GetExamDtoAsync(string examname)
        {
            return await _context.Exams
                .Where(x => x.ExamName == examname)
                .ProjectTo<ExamsDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public void Delete(AppExams ExamToDelete)
        {
            _context.Remove(ExamToDelete);
        }
    }
}