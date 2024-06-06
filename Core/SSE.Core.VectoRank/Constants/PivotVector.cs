using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE.Core.VectoRank.Constants
{
    public class PivotVector
    {
        private static double[] _value;
        public static double[] VALUE
        {
            get
            {
                if (_value == null)
                {
                    _value = new double[400];
                    for (int i = 0; i < _value.Length; i++)
                    {
                        _value[i] = 0.0001;
                    }
                }
                return _value;
            }
        }
    }
}
