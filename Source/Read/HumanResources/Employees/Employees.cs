﻿using System.Linq;
using doLittle.Read;

namespace Read.HumanResources.Employees
{
    public class Employees : IQueryFor<Employee> 
    {
        IReadModelRepositoryFor<Employee> _repository;
        public Employees(IReadModelRepositoryFor<Employee> repository)
        {
            _repository = repository;
        }

        public IQueryable<Employee> Query => _repository.Query;
    }
}
