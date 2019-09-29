using System;
using System.Linq;
using System.Collections.Generic;
using SampleAPI.ORM.Model.Packaging;
using SampleAPI.Interface.DataAccess;
using SampleAPI.Interface.DataAccess.Packaging;
using SampleAPI.ORM;

namespace SampleAPI.DataAcccess.Repositories.Packaging
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository<Customer>
    {
        protected new static readonly string _schema = "dbo.";

        public CustomerRepository(IDbProvider idbProvider, IUnitOfWork unitOfWork, IRulesEngine<Customer> rulesengine, IDbHelper helper) : base(idbProvider, rulesengine, helper, _schema)
        {
            unitOfWork.Register(this);
        }

    }
}
