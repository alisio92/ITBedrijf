using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITBedrijf.Extra
{
    public class LimitList
    {
        public static List<int> GetNumberList(int aantal)
        {
            List<int> numbers = new List<int>();
            for (int i = 0; i < aantal; i++)
            {
                numbers.Add(i);
            }
            return numbers;
        }

        public static int GetAantal(int count, int aantal)
        {
            if (count <= aantal) return 0;
            if (count == aantal * (count / aantal)) return count / aantal;
            else return (count / aantal) + 1;
        }
    }
}