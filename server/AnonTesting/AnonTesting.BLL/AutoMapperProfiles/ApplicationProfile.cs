using AnonTesting.BLL.Model;
using AnonTesting.DAL.Model;
using AutoMapper;

namespace AnonTesting.BLL.AutoMapperProfiles
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<Test, TestDto>().ReverseMap();

            CreateMap<Question, QuestionDto>().ReverseMap();

            CreateMap<Answer, AnswerDto>()
                .ForMember(dest => dest.TestId, options => options.MapFrom(source => source.Question.TestId))
                .ReverseMap();

            CreateMap<TestResult, TestResultDto>().ReverseMap();
        }
    }
}
