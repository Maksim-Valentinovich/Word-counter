using System;
using System.Linq;

namespace New_Structure
{
    public class Estimation
    {

        /// <summary>
        /// Вывод слов на консоль
        /// </summary>
        /// <param name="text"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public int CountWords(string text)
        {
            bool startWord = false;

            bool finishWord = false;

            string textlower = text.ToLower();

            char[] symbolsPunctuation = { '!', '.', ',', '?', ':', ';', ' ' };

            int count = 0;

            foreach (var symbol in textlower)
            {
                if (symbol >= 'a' && symbol <= 'z')
                {
                    if (!startWord)
                    {
                        count++;
                    }

                    startWord = true;

                    finishWord = false;
                }

                else if (symbol < 'a' || symbol > 'z')
                {
                    if (symbolsPunctuation.Contains(symbol) && startWord && !finishWord)
                    {
                        finishWord = true;

                        startWord = false;
                    }
                    else
                        continue;
                }
                else
                    continue;
            }
            return count;
        }

        ///// <summary>
        ///// Вывод слов на консоль
        ///// </summary>
        ///// <param name="text"></param>
        ///// <param name="count"></param>
        ///// <returns></returns>
        //public bool PrintTextOnConsole(string text)
        //{
        //    bool startWord = false;

        //    bool finishWord = false;

        //    string textlower = text.ToLower();

        //    char[] symbolsPunctuation = { '!', '.', ',', '?', ':', ';', ' ' };

        //    int count = 0;

        //    foreach (var symbol in textlower)
        //    {
        //        if (symbol >= 'a' && symbol <= 'z')
        //        {
        //            if (!startWord)
        //            {
        //                count++;

        //                Console.Write("\nСлово {0}: ", count);
        //            }

        //            Console.Write(symbol);

        //            startWord = true;

        //            finishWord = false;
        //        }

        //        else if (symbol < 'a' || symbol > 'z')
        //        {
        //            if (symbolsPunctuation.Contains(symbol) && startWord && !finishWord)
        //            {
        //                Console.WriteLine(symbol);

        //                finishWord = true;

        //                startWord = false;
        //            }
        //            else
        //                continue;
        //        }
        //        else
        //            continue;
        //    }
        //    return true;
        //}
    }
}
