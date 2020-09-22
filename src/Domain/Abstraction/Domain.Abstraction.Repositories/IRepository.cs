using System;
using System.Collections.Generic;

namespace Domain.Abstraction.Repositories
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        List<T> GetAll(Func<T, bool> predicate);

        T Get(Func<T, bool> predicate);

        void Add(T item);

        void Update(T item);

        void Delete(string id);
    }
}