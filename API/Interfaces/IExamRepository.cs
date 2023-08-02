using API.DTO;
using API.Entities;

namespace API.Interfaces
{
    public interface IExamRepository
    {

        void Update(AppExams exam);

        Task <bool> SaveAllAsync();

        Task <IEnumerable<AppExams>> GetExamsAsync();

        Task <AppExams> GetExamByIdAsync (int examid);

        Task <AppExams> GetExamByExamnameAsync(string examname);

        Task <IEnumerable<ExamsDto>> GerExamsDtoAsync();

        Task <ExamsDto> GetExamDtoAsync (string examname);

        
    }
}