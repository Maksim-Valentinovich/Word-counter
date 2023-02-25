using System;
using Word_counter.Enums;

namespace New_Structure
{
    class WorkText
    {


        /// <summary>
        /// Проверка корректности текста
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public NextStep CheckText(string text)
        {
            string textlower = text.ToLower();

            foreach (var item in textlower)
            {
                if ((item < 'a' || item > 'z') && (item != ' ' && item != '.' && item != ',' && item != '!' && item != '?' && item != ';' && item != ':'))
                {
                    Console.WriteLine("Неверный текст");

                    Choising choising = new Choising();

                    return choising.ChooseRepeatOperation();

                    //return NextStep.ChooseRepeatOperation;
                }

                else
                    continue;
            }
            
            Print(text, ref count);

            Choising choise = new Choising();

            return choise.ChoiseSaveResult();

            //return NextStep.ChoiseSaveResult;
        }

        /// <summary>
        /// Вывод слов на консоль
        /// </summary>
        /// <param name="text"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        private void Print(string text, ref int count)
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
        }
    }
}
