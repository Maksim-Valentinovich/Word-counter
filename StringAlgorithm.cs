using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace New_Structure
{
    public class StringBuilderAlghorinm
    {
        public string MakeStringАlgorithm(List<char> textAfterCheck)
        {

            string rightWords = string.Empty;

            string [] words = (new string(textAfterCheck.ToArray())).Split(new char[] { '!', '.', ',', '?', ':', ';', ' ' });

            foreach (string w in words)
            {
                if (!w.Contains("q") && !w.Contains("r") && !w.Contains("u"))
                {
                    rightWords = rightWords + w + " ";
                }
            }

            if (rightWords == string.Empty)
            {
                throw new Exception();
            }

            return rightWords;
        }    
    }
}
