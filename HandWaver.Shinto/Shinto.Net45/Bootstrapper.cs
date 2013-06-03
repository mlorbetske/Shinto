using Shinto.Cache.Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Shinto.Net45
{
    /// <summary>
    /// Bootstrap composition as well as some non-portable items from Shinto core
    /// </summary>
    public class Bootstrapper
    {
        public Bootstrapper()
        {
            _isConfigured = false;
            Configure();            
        }

        bool _isConfigured;
        MEFCompositionProvider _provider;

        public Bootstrapper Configure()
        {
            if (!_isConfigured)
            {
                _provider = new MEFCompositionProvider();
                Initializer.CompositionProvider = _provider;
                _isConfigured = true;
            }
            return this;
        }

        public Bootstrapper WithAssembly(Assembly a)
        {
            var assCat = new AssemblyCatalog(a);
            _provider.AddCatalog(assCat);
            return this;
        }


        public Bootstrapper WithCacheDefaults()
        {
            ConcurrentDictionaryProvider.Creator = new ConcurrentDictionaryCreator();            
            return this;
        }

        public Bootstrapper WithLogger()
        {
            return WithLogger(new ConsoleLogger());
        }

        public Bootstrapper WithLogger(ILogger logger)
        {
            Initializer.SatisfyImports(logger);
            TimeBasedExpirationPolicy.Log = logger;
            return this;
        }

        public Bootstrapper WithAggregateExceptionPolicy()
        {
            ExceptionPolicy.AddHandler(typeof(AggregateException), ex =>
            {
                var agg = ex as AggregateException;
                var logger = Initializer.GetSingleExport<ILogger>();

                logger.Error("Task/Thread Exception", agg);
                logger.Error("Aggregate Details may follow based on individual policies");
                if (null != agg.InnerExceptions)
                {
                    foreach (var iex in agg.InnerExceptions)
                    {
                        ExceptionPolicy.Handle(iex);
                    }
                }

            });

            return this;
        }
    }
}
