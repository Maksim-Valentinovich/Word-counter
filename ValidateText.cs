using System;
using System.Linq;
using Word_counter.Enums;
using Word_counter.Enums2;

namespace New_Structure
{
    public class ValidateText
    {

        /// <summary>
        /// Проверка корректности текста
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public bool CheckSymbols(string text)
        {
            string textlower = text.ToLower();

            int countOfLetter = 0;

            char[] symbolsPunctuation = { '!', '.', ',', '?', ':', ';', ' ' };

            if (textlower.Length == 0) return false;

            foreach (var symbol in textlower)
            {
                if (symbol < 'a' || symbol > 'z')
                {
                    if (symbolsPunctuation.Contains(symbol))
                        continue;
                   
                    else return false;
                }
                else
                    countOfLetter++;
                    continue;
            }

            if (countOfLetter != 0) return true;

            return false;
        }
    }
}
