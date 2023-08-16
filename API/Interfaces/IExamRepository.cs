using API.DTO;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Interfaces
{
    public interface IExamRepository
    {

        void Update(AppExams exam);

        Task <bool> SaveAllAsync();

        Task <IEnumerable<AppExams>> GetExamsAsync();

        Task <AppExams> GetExamByIdAsync (int examid);

        Task <AppExams> GetExamByExamnameAsync(string examname);

        Task <IEnumerable<ExamsDto>> GetExamsDtoAsync();

        Task <ExamsDto> GetExamDtoAsync (string examname);

       /* Task <string> GetExamDtoByIdAsync (int examid);*/
    }
}