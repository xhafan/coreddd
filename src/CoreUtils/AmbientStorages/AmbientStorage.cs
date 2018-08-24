using System.Threading;

#if NETFRAMEWORK
using System;
using System.Web;
#endif

namespace CoreUtils.AmbientStorages
{
    public class AmbientStorage<TData>
    {
#if NETFRAMEWORK
        private readonly string _key = $"{nameof(AmbientStorage<TData>)}-{Guid.NewGuid()}";
#endif
#if NET40
        private readonly ThreadLocal<TData> _threadLocalData = new ThreadLocal<TData>();
#endif
#if !NET40
        private readonly AsyncLocal<TData> _asyncLocalData = new AsyncLocal<TData>();
#endif
        public TData Value
        {
            get
            {
#if NETFRAMEWORK
                if (_isAspNet())
                {
                    return (TData) HttpContext.Current.Items[_key];
                }
#endif
#if NET40
                return _threadLocalData.Value;
#else
                return _asyncLocalData.Value;
#endif
            }
            set
            {
#if NETFRAMEWORK
                if (_isAspNet())
                {
                    HttpContext.Current.Items[_key] = value;
                }
                else
#endif
#if NET40
                {
                    _threadLocalData.Value = value;
                }
#else
                {
                    _asyncLocalData.Value = value;
                }
#endif
            }
        }

#if NETFRAMEWORK
        private bool _isAspNet()
        {
            return HttpContext.Current != null;
        }
#endif
    }
}