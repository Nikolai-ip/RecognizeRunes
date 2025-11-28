using System;
using System.Collections.Generic;

namespace _Game.Source
{
    public interface IRepository<T>: IEnumerable<T>
    {
        void Add(T pin);
        void Remove(T item);
    }

    public interface IRepositoryNotifier<out T>
    {
        event Action<T> OnElementAppend;
        event Action<T> OnElementRemove;
    }

}