using System.Globalization;

namespace CoreDdd.Tests.Helpers.Extensions
{
    public static class CultureHelper
    {
        public static CultureInfo English => new CultureInfo("en");

        public static CultureInfo Japanese => new CultureInfo("ja");
    }
}
