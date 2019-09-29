using System;
using System.Collections.Generic;
using System.Data;
using SampleAPI.Interface.DataAccess;
using SampleAPI.ORM;

namespace SampleAPI.DataAcccess
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private bool disposedValue = false;
        private IDbProvider _idbProvider;
        public Dictionary<Type, dynamic> Repos { get; private set; }       

        public UnitOfWork(IDbProvider idbProvider)
        {
            _idbProvider = idbProvider;
            Repos = new Dictionary<Type, dynamic>();
        }

        public void OpenConnection()
        {
            _idbProvider.OpenConnection();
        }

        public void CloseConnection()
        {
           _idbProvider.CloseConnection();
        }

        public IDbTransaction StartTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadUncommitted)
        {
           return _idbProvider.InitTransaction(isolationLevel);
        }

        public void CompleteTransaction()
        {
            _idbProvider.CommitTransaction();
        }

        public void Error()
        {
            _idbProvider.RollbackTransaction();
        }

        public dynamic Repository<T>() where T : class
        {
            dynamic repository;
            if (Repos.TryGetValue(typeof(T), out repository))
            {
                return repository;
            }
            return null;
        }

        public void Register<T>(IRepository<T> repository) where T : class
        {
            dynamic repositoryValue;
            if (!Repos.TryGetValue(typeof(T), out repositoryValue))
            {
                Repos.Add(typeof(T), repository);
            }
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _idbProvider.Dispose();
                    _idbProvider = null;
                }

                disposedValue = true;
            }
        }
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }

}
