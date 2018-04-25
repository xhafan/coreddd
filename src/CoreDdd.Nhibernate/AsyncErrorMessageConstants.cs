namespace CoreDdd.Nhibernate
{
    internal static class AsyncErrorMessageConstants
    {
        public const string AsyncMethodNotSupportedExceptionMessage =
            "Async methods are supported only for .NET 4.6.1+ (NHibernate 5+). For lower .NET and NHibernate versions, please use sync method instead.";
    }
}