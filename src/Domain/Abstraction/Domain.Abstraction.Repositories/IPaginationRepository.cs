using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Domain.Abstraction.Repositories
{
    public interface IPaginationRepository<T> : IRepository<T>
    {
        List<T> GetAll(int skip, int take);
        List<T> GetAll(Func<T, bool> predicate, int skip, int take);
    }
}
