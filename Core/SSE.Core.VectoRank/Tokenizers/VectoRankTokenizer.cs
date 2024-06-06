using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SSE.Core.VectoRank.Tokenizers
{
    public class VectoRankTokenizer
    {
        public static List<string> Tokenize(string text)
        {
            // Define regex pattern to split text by whitespace and punctuation
            string pattern = @"[\p{P}\p{Z}\s]+";

            // Split text into tokens using regex pattern
            string[] rawTokens = Regex.Split(text, pattern);

            // Filter out empty tokens
            List<string> tokens = new List<string>();
            foreach (string rawToken in rawTokens)
            {
                if (!string.IsNullOrWhiteSpace(rawToken))
                {
                    tokens.Add(rawToken);
                }
            }

            return tokens;
        }
    }
}
