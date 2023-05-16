using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace New_Structure
{
    public class ActionsText
    {

        /// <summary>
        /// Подсчёт количества слов
        /// </summary>
        /// <param name="text"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public int CountWordsАlgorithm(string text, List<char> textAfterCheck)
        {
            bool startWord = false;

            bool finishWord = false;

            int count = 0;

            char[] symbolsPunctuation = { '!', '.', ',', '?', ':', ';', ' ' };

            foreach (var symbol in text)
            {
                if (symbol >= 'a' && symbol <= 'z')
                {
                    if (!startWord)
                    {
                        count++;
                    }

                    textAfterCheck.Add(symbol);

                    startWord = true;

                    finishWord = false;
                }
                else
                {
                    if (symbolsPunctuation.Contains(symbol) && startWord && !finishWord)
                    {
                        textAfterCheck.Add(symbol);

                        finishWord = true;

                        startWord = false;
                    }
                    else
                        continue;
                }
            }
            return count;
        }

        /// <summary>
        /// Проверка корректности текста
        /// </summary>
        /// <param name = "text" ></ param >
        /// < returns ></ returns >
        public bool CheckSymbols(string text)
        {
            string textlower = text.ToLower();

            int nubmerLetterInWord = 0;

            char[] symbolsPunctuation = { '!', '.', ',', '?', ':', ';', ' ' };

            if (text.Length == 0)
                return false;

            foreach (var symbol in textlower)
            {
                if (symbol < 'a' || symbol > 'z')
                {
                    if (symbolsPunctuation.Contains(symbol))
                        continue;

                    else return
                            false;
                }
                else
                    nubmerLetterInWord++;
                continue;
            }

            if (nubmerLetterInWord != 0)
                return true;

            return false;
        }

    }
}
