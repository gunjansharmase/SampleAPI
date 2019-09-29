using System;
using System.Dynamic;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using SampleAPI.DataTransferObjects.Enum;
using SampleAPI.DataTransferObjects;


namespace SampleAPI.ORM
{
    public class DapperWrapper : IDapperWrapper
    {
        public async Task<int> ExecuteAsync(IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            return await cnn.ExecuteAsync(sql, param, transaction, commandTimeout);
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            return await cnn.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType).ConfigureAwait(false);
        }

        public async Task<SqlMapper.GridReader> QueryMultipleAsync(IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            return await cnn.QueryMultipleAsync(sql, param, transaction, commandTimeout, commandType);
        }

        public async Task<IEnumerable<T>> Query<Q, S, T>(IDbConnection cnn, Func<Q, S, T> map, string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?), string spliton = "Id")
        {
            return await cnn.QueryAsync<Q, S, T>(sql, map, param, transaction, buffered, spliton, commandTimeout, commandType);
        }

        public async Task<IEnumerable<T>> Query<Q, R, S, T>(IDbConnection cnn, Func<Q, R, S, T> map, string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?), string spliton = "Id")
        {
            return await cnn.QueryAsync<Q, R, S, T>(sql, map, param, transaction, buffered, spliton, commandTimeout, commandType);
        }

        public IEnumerable<T> Query<T>(IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            return cnn.Query<T>(sql, param, transaction, buffered, commandTimeout, commandType);
        }

        public dynamic ExecuteQueryMultiple(IDbConnection cnn, string spName, object param = null, IEnumerable<MapItem> mapItems = null, CommandType commandType = CommandType.StoredProcedure)
        {
            var data = new ExpandoObject();
            using (var multi = cnn.QueryMultipleAsync(spName, param, commandType: commandType))
            {
                if (mapItems == null) return data;

                foreach (var item in mapItems)
                {
                    if (item.DataRetrieveType == DataRetrieveTypeEnum.FirstOrDefault)
                    {
                        var singleItem = multi.Result.ReadFirstOrDefault(item.Type);
                        ((IDictionary<string, object>)data).Add(item.PropertyName, singleItem);
                    }

                    if (item.DataRetrieveType == DataRetrieveTypeEnum.List)
                    {
                        var listItem = multi.Result.Read(item.Type);
                        ((IDictionary<string, object>)data).Add(item.PropertyName, listItem);
                    }
                }

                return data;
            }
        }

    }
}
