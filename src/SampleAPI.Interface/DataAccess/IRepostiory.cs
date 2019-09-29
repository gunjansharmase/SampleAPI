using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleAPI.Interface.DataAccess
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAsync();
        Task<IEnumerable<T>> GetByIdRangeAsync(IEnumerable<int> ids);
        Task<T> GetByIdAsync(int id, string keyName);
        Task<IEnumerable<T>> GetByQueryAsync(string query, object param = null);
        Task<int> AddAsync(T entity);
        Task<int> AddAsync(T entity, List<string> ignoreFields);
        Task<int> AddRangeAsync(IEnumerable<T> entities);

        Task<int> AddRangeAsync(IEnumerable<T> entities, List<string> ignoreParameters);
        Task<int> UpdateAsync(T entity);

        Task<int> UpdateAsync(T entity, List<string> ignoreFields);

        Task<int> UpdateRangeAsync(IEnumerable<T> entities);
        Task<int> UpdateRangeAsync(IEnumerable<T> entities, string[] byKeys);
        Task<int> UpdateRangeAsync(IEnumerable<T> entities, string[] byKeys, List<string> ignoreParameters);

        Task<int> RemoveAsync(int id);
        Task<int> RemoveRangeAsync(IEnumerable<T> entities, string[] byKeys, List<string> ignoreParameters);
        Task<IEnumerable<IDataValidationFailure>> CanAddAsync(T entity);
        Task<IEnumerable<IDataValidationFailure>> CanUpdateAsync(T entity);
        Type TableType { get; }
        Task<IEnumerable<T>> GetWithQuery<Q, S, T>(string query, Func<Q, S, T> map, object param, string splitOn);
        Task<IEnumerable<T>> GetWithQuery<Q, R, S, T>(string query, Func<Q, R, S, T> map, object param, string splitOn);
        Task<IDictionary<Type, IEnumerable<object>>> QueryMultiple(string query, object parameters, IEnumerable<Type> returnTypes);
        Task<IDictionary<Type, IEnumerable<object>>> QueryMultiple(string sp, object args, IEnumerable<Type> returnTypes, CommandType type = CommandType.Text);
        IEnumerable<T> Query<T>(string sql, object param = null);
        
    }
}
