﻿using AnonTesting.DAL.Model;
using AnonTesting.DAL.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace AnonTesting.DAL.Repositories
{
    public class QuestionRepository : EntityRepository<Question>
    {
        public QuestionRepository(ApplicationContext context) : base(context) 
        { 
        }
    }
}
