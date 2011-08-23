using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Core.TestHelper.Extensions
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
