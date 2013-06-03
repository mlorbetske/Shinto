using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Shinto.Net45
{
    public class MEFCompositionProvider : ICompositionProvider
    {
        AggregateCatalog _catalog;
        CompositionContainer _container;

        public MEFCompositionProvider()
        {
            _catalog = new AggregateCatalog();
            _container = new CompositionContainer(_catalog, true);//thread safe
        }

        public void AddCatalog(ComposablePartCatalog cat)
        {
            _catalog.Catalogs.Add(cat);
        }

        public void SatisfyImports(object part)
        {
            _container.ComposeParts(part);
        }

        public void SatisfyImports<TStaticType>()
        {
            var targetType = typeof(TStaticType);
            var importType = typeof(ImportAttribute);
            var importManyType = typeof(ImportManyAttribute);
            var propInfos = targetType.GetProperties(BindingFlags.Public | BindingFlags.Static);
            var getExportMethod = typeof(Initializer).GetMethod("GetSingleExport");
            var getAllExportsMethod = typeof(Initializer).GetMethod("GetAllExports");
            foreach (var propInfo in propInfos)
            {
                object[] importAttrs = propInfo.GetCustomAttributes(importType, false);
                if (null != importAttrs && importAttrs.Length > 0)
                {
                    Type propType = propInfo.PropertyType;
                    MethodInfo genericOfT = getExportMethod.MakeGenericMethod(propType);
                    object value = genericOfT.Invoke(null, null);
                    propInfo.SetValue(null, value, null);
                }

                object[] importManyAttrs = propInfo.GetCustomAttributes(importManyType, false);
                if (null != importManyType && importManyAttrs.Length > 0)
                {
                    Type propType = propInfo.PropertyType;
                    Type[] genericArgs = propType.GetGenericArguments();
                    MethodInfo genericOfT = getAllExportsMethod.MakeGenericMethod(genericArgs[0]);
                    object value = genericOfT.Invoke(null, null);
                    propInfo.SetValue(null, value, null);
                }
            }
        }

        public T GetSingleExport<T>()
        {
            return _container.GetExport<T>().Value;
        }

        public IEnumerable<T> GetAllExports<T>()
        {
            return _container.GetExportedValues<T>();
        }
    }
}
