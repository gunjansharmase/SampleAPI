using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using SampleAPI.DataTransferObjects;


namespace SampleAPI.ORM

{
    public interface IDbProvider : IDisposable
    {
        void OpenConnection();
        void CloseConnection();
        IDbTransaction InitTransaction(IsolationLevel isolationLevel);
        void CommitTransaction();
        void RollbackTransaction();
        Task<int> AddAsync<T>(T entity, string schema);
        Task<int> AddAsync<T>(T entity, string schema, string[] ignoreParameters);
        Task<int> AddRangeAsync<T>(IEnumerable<T> entity, string schema);
        Task<int> AddRangeAsync<T>(IEnumerable<T> entities, string schema, string[] ignoreParameters);
        Task<int> AddRangeWithQueryAsync<T>(string query, IEnumerable<T> entities, string[] ignoreParameters = null);
        Task<int> UpdateRangeAsync<T>(IEnumerable<T> entities, string[] ByIds, string schema, string[] ignoreParameters = null);
        Task<int> UpdateAsync<T>(T entity, string schema, string[] ignoreParameters = null);
        Task<IEnumerable<T>> GetAsync<T>(string schema, bool enabled = true);
        Task<T> GetByIdAsync<T>(string id, string keyName, string schema, bool enabled = true);
        Task<IEnumerable<T>> GetAsync<T>(string query, object param);
        Task<int> DeleteAsync<T>(int entityId, string schema, string[] ignoreParameters = null);
        Task<IDictionary<Type, IEnumerable<object>>> QueryMultiple(string query, object parameters, IEnumerable<Type> returnTypes);
        Task<IDictionary<Type, IEnumerable<object>>> QueryMultiple(string sp, object args, CommandType type, IEnumerable<Type> returnTypes);
        Task<IEnumerable<T>> GetByQuery<Q, S, T>(string query, Func<Q, S, T> map, object param = null, string splitOn = "Id");
        Task<IEnumerable<T>> GetByQuery<Q, R, S, T>(string query, Func<Q, R, S, T> map, object param = null, string splitOn = "Id");
        IEnumerable<T> Query<T>(string sql, object param = null);
        dynamic ExecuteQueryMultiple(string spName, object param = null, IEnumerable<MapItem> mapItems = null, CommandType commandType = CommandType.StoredProcedure);


    }
}