using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE.Core.VectoRank.Text
{
    public class SentenceHelper
    {

        public static string[] GetSentences(string text)
        {
            return text.Split(new string[] { ".", "!", "?" }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
