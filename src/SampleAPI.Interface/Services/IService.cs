using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleAPI.Interface.Services
{
    public interface IService<U, T>
        where U : class
        where T : class
    {

        Task<IServiceResponse<IEnumerable<U>>> GetAsync();

        Task<IServiceResponse<U>> GetByIdAsync(int id);
        Task<IServiceResponse<IEnumerable<U>>> GetRangeAsync(IEnumerable<int> ids);

        Task<IServiceResponse<U>> AddAsync(U dto);

        Task<IServiceResponse<int>> AddRangeAsync(IEnumerable<U> dto);

        Task<IServiceResponse<int>> UpdateAsync(U dto);

        Task<IServiceResponse<int>> UpdateRangeAsync(IEnumerable<U> dto);

        Task<IServiceResponse<int>> DeleteAsync(int id);

        Task<IServiceResponse<int>> DeleteRangeAsync(IEnumerable<int> ids);

        Task<T> ExecuteWithoutTransactionAsync<T>(Func<Task<T>> action);
        Task<T> ExecuteWithTransactionAsync<T>(Func<Task<T>> action);
    }
}
