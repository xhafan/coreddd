namespace CoreDdd.Utilities
{
    public static class Guard
    {
        static public void Hope(bool condition, string message)
        {
            if (!condition)
            {
                throw new CoreException(message);
            }
        }
    }
}
