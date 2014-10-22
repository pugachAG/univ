using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPMMathTest.Helpers
{
    public static class AssertHelper
    {
        public static void AreEqual(decimal expected, decimal actual, decimal delta)
        {
            Assert.AreEqual((double)expected, (double)actual, (double)delta);
        }
    }   
}
