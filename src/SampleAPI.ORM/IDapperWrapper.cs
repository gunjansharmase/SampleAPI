using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using SampleAPI.DataTransferObjects;



namespace SampleAPI.ORM
{
    public interface IDapperWrapper
    {
        Task<int> ExecuteAsync(IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?));
        Task<IEnumerable<T>> QueryAsync<T>(IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?));
        Task<SqlMapper.GridReader> QueryMultipleAsync(IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?));
        Task<IEnumerable<T>> Query<Q, S, T>(IDbConnection cnn, Func<Q, S, T> map, string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?), string spliton = "Id");
        Task<IEnumerable<T>> Query<Q, R, S, T>(IDbConnection cnn, Func<Q, R, S, T> map, string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?), string spliton = "Id");
        IEnumerable<T> Query<T>(IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?));
        dynamic ExecuteQueryMultiple(IDbConnection cnn, string spName, object param = null, IEnumerable<MapItem> mapItems = null, CommandType commandType = CommandType.StoredProcedure);
    }
}
