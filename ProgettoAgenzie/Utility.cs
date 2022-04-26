using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgettoAgenzie
{
    public static class Utility
    {
        public static int RandomN()
        {
            Random rnd = new Random();
            int nrand = rnd.Next(10, 99);
            return nrand;
        }
    }
}
