using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE.VectorData.Domain.Extensions
{
    public static class StringExtensions
    {

        public static double[] ToVector(this string value)
        {
            var arry = value.Split(' ');
            var result = new double[arry.Length];
            for (int i = 0; i < arry.Length; i++)
            {
                result[i] = Convert.ToDouble(arry[i]);
            }
            return result;
        }
    }
}
