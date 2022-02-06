using AnonTesting.BLL.Commands.Test;
using AnonTesting.BLL.Commands.User;
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

            CreateMap<Answer, AnswerDto>().ReverseMap();

            CreateMap<TestResult, TestResultDto>().ReverseMap();

            CreateMap<CreateUserCommand, User>()
                .ForMember(command => command.UserName, options => options.MapFrom(user => user.Email));

            CreateMap<ReviewTestCommand, TestResultDto>();
        }
    }
}
