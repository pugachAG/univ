using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace WPMControls.Drawing
{
    public static class StringFormatHelper
    {
        public const int DefaultNumberLength = 4;

        public static string FormatPoint(Point p)
        {
            string strX = FormatDouble(p.X);
            string strY = FormatDouble(p.Y);
            return string.Format("({0},{1})", strX, strY);
        }

        public static string FormatDouble(double val)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
            int integerPart = (int)val;
            int minLength = integerPart.ToString().Length;
            int len = Math.Max(DefaultNumberLength, minLength);
            string result = val.ToString("0.00000");
            if (result.Length > len)
                result = result.Substring(0, len);
            return result;
        }
    }
}
