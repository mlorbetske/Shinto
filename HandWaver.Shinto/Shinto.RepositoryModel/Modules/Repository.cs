using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shinto.RepositoryModel.Modules
{
    public abstract class Repository
    {
        public IUnitOfWork Context { get; set; }


        protected TRepository GetAssistant<TRepository>() where TRepository : IRepository
        {
            if (null != Context)
            {
                return Context.GetRepository<TRepository>();
            }
            else
            {
                return Initializer.GetSingleExport<TRepository>();
            }
        }

    }
}
