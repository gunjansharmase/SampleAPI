using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SampleAPI.Interface.DataAccess
{
    public interface IUnitOfWork
    {
        Dictionary<Type, dynamic> Repos { get; }
        void OpenConnection();
        void CloseConnection();
        IDbTransaction StartTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
        void CompleteTransaction();
        void Error();
        dynamic Repository<T>() where T : class;
        void Register<T>(IRepository<T> repository) where T : class;
    }

}
