using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SharpFEGrasshopper.Utilities
{
    class StringManipulation
    {
        public static string RemoveDigits(string key)
        {
            return Regex.Replace(key, @"\d", "");
        }

    }
}
