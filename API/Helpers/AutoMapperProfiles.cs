using API.DTO;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MemberDto>().ForMember(
                dest => dest.ProfilePictureUrl, opt=> opt.MapFrom(src => src.ProfilePicture.PhotoUrl)
                );
            CreateMap<Photo, PhotoDto>();
            CreateMap<AppExams, ExamsDto>().ForMember(
                dest => dest.ExamPictureUrl, opt => opt.MapFrom(src => src.ExamPicture.ExamPhotoUrl)
            );
            CreateMap<ExamPhoto, ExamPhotoDto>();
            CreateMap<Question, QuestionDto>();
            CreateMap<Answers, AnswersDto>();
            CreateMap<UserExam, UserExamDto>();
        }
    }
}