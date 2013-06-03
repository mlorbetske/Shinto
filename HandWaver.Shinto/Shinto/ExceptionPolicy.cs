using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shinto
{
    /// <summary>
    /// Centralize error handling
    /// </summary>
    public class ExceptionPolicy
    {
        static ExceptionPolicy()
        {
            _handlers = new Dictionary<Type, List<ExceptionFilterTuple>>();
        }

        static Func<Exception, bool> DefaultFilter = e => true;

        static Action<Exception> _defaultHandler;

        static Dictionary<Type, List<ExceptionFilterTuple>> _handlers;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handler"></param>
        public static void SetDefaultHandler(Action<Exception> handler)
        {
            _defaultHandler = handler;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exceptionType"></param>
        public static void AddHandler(Type exceptionType, Action<Exception> handler)
        {
            AddHandler(exceptionType, DefaultFilter, handler);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="exceptionType"></param>
        /// <param name="filter"></param>
        /// <param name="handler"></param>
        public static void AddHandler(Type exceptionType, Func<Exception, bool> filter, Action<Exception> handler)
        {
            if (!_handlers.ContainsKey(exceptionType))
            {
                _handlers[exceptionType] = new List<ExceptionFilterTuple>();
            }

            _handlers[exceptionType].Add(new ExceptionFilterTuple(exceptionType, filter));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        public static void Handle(Exception ex)
        {
            var exType = ex.GetType();

            if (_handlers.ContainsKey(exType))
            {
                var handler = _handlers[exType].Where(e => e.Filter(ex)).FirstOrDefault();
                if (null != handler)
                {
                    handler.Handler(ex);
                }
                return;
            }

            _defaultHandler(ex);
        }


        class ExceptionFilterTuple
        {
            public ExceptionFilterTuple(Type ex)
                : this(ex, ExceptionPolicy.DefaultFilter)
            {
            }

            public ExceptionFilterTuple(Type ex, Func<Exception, bool> filter)
            {
                Exception = ex;
                Filter = filter;
            }

            public Type Exception { get; private set; }

            public Action<Exception> Handler { get; private set; }

            public Func<Exception, bool> Filter { get; private set; }
        }
    }
}
