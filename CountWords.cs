using System;
using Word_counter.Enums;

namespace New_Structure
{
    class CountWords
    {
        private int count;

        public CountWords(ref int count)
        {
            this.count = count;
        }

        /// <summary>
        /// Вывод слов на консоль
        /// </summary>
        /// <param name="text"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public NextStep Estimation(string text, ref int count)
        {
            bool startWord = false;

            bool finishWord = false;

            string textlower = text.ToLower();

            string sumbolsPunctuation = ".,:;!?";

            foreach (var item in textlower)
            {
                if ((item < 'a' || item > 'z') && item != ' ')
                {
                    foreach (var prov in sumbolsPunctuation)
                    {
                        if (prov == item && startWord == true && finishWord == false)
                        {
                            Console.Write(item);

                            finishWord = true;

                            startWord = false;
                        }

                        else
                            continue;
                    }
                }

                else if (item == ' ' && startWord == true)
                {
                    finishWord = true;

                    startWord = false;
                }

                else if (item >= 'a' || item <= 'z' && item != ' ')
                {
                    if (startWord == false)
                    {
                        count++;

                        Console.Write("\nСлово {0}: ", count);
                    }

                    startWord = true;

                    finishWord = false;

                    Console.Write(item);
                }

                else
                    continue;
            }

            Console.WriteLine("\nКоличество слов в тексте = {0}", count);
           
            return NextStep.ChoiseSaveResult;
        }
    }
}
