using System.Globalization;

namespace CoreDdd.Tests.Helpers.Extensions
{
    public static class CultureHelper
    {
        public static CultureInfo English 
        { 
            get
            {
                return new CultureInfo("en");
            } 
        }

        public static CultureInfo Japanese
        {
            get
            {
                return new CultureInfo("ja");
            }
        }
    }
}
