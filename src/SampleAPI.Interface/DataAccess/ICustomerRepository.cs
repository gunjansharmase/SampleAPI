using System;
using System.Collections.Generic;
using System.Text;

namespace SampleAPI.Interface.DataAccess.Packaging
{
    public interface ICustomerRepository<T> : IRepository<T> where T : class
    {
    }
}
