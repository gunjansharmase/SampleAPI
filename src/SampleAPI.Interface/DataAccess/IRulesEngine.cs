using System.Collections.Generic;

namespace SampleAPI.Interface.DataAccess
{
    public interface IRulesEngine<T> where T : class
    {
        T AddEntity(T entity);

        IEnumerable<T> AddEntities(IEnumerable<T> entities);

        T UpdateEntity(T entity);

        IEnumerable<T> UpdateEntities(IEnumerable<T> entities);

        T RemoveEntity(T entity);

    }
}
