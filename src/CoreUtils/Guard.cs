namespace CoreUtils
{
    public static class Guard
    {
        public static void Hope(bool condition, string message)
        {
            if (!condition)
            {
                throw new CoreException(message);
            }
        }
    }
}
