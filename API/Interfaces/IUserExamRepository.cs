using API.DTO;
using API.Entities;

namespace API.Interfaces
{
    public interface IUserExamRepository
    {
        void Update(UserExam userexam);

        Task <bool> SaveAllAsync();

        Task <IEnumerable<UserExam>> GetUsersExamsAsync();

        Task <UserExam> GetUserExamByIdsAsync (int userid, int examid);

        Task <IEnumerable<UserExamDto>> GerUserExamDtosAsync();

        Task <UserExamDto> GetUserExamDtoAsync (int id, int examid);
        void Delete(UserExam userExamToDelete);
    }
}




