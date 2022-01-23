﻿using AnonTesting.BLL.Interfaces;
using AnonTesting.BLL.Model;
using AnonTesting.BLL.Services.Abstract;
using AnonTesting.DAL.Interfaces;
using AnonTesting.DAL.Model;
using AutoMapper;

namespace AnonTesting.BLL.Services
{
    public class TestService : GenericService<Test, TestDto>, ITestService
    {
        public TestService(IEntityRepository<Test> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
