#if NETFRAMEWORK
using System;
using System.Web;
#endif

#if !NET40 && !NET45
using System.Threading;
#endif

#if NET40 || NET45 
using System.Runtime.Remoting.Messaging;
#endif

namespace CoreUtils.AmbientStorages
{
    /// <summary>
    /// An ambient storage for various different execution contexts:
    /// 1) For full .NET framework and ASP.NET, the data are stored in HttpContext.Current.Items.
    /// 2) For .NET 4, .NET 4.5.x and non-ASP.NET, the data are stored in the execution context via CallContext.LogicalGet/SetData.
    /// 3) For .NET 4.6 and higher and for .NET Standard, the data are stored in the execution context via async local storage.
    /// More info: https://stackoverflow.com/questions/31707362/how-do-the-semantics-of-asynclocal-differ-from-the-logical-call-context
    /// </summary>
    /// <typeparam name="TData">A data type</typeparam>
    public class AmbientStorage<TData>
    {
#if NETFRAMEWORK
        private readonly string _key = $"{nameof(AmbientStorage<TData>)}-{Guid.NewGuid()}";
#endif

#if !NET40 && !NET45
        private readonly AsyncLocal<TData?> _asyncLocalData = new();
#endif
        /// <summary>
        /// Data value.
        /// </summary>
        public TData? Value
        {
            get
            {
#if NETFRAMEWORK
                if (_isAspNet())
                {
                    return (TData) HttpContext.Current.Items[_key];
                }
#endif
#if NET40 || NET45
                return (TData)CallContext.LogicalGetData(_key);
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
#if NET40 || NET45
                {
                    CallContext.LogicalSetData(_key, value);
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