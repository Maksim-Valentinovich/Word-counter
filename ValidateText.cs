using System;
using Word_counter.Enums;
using Word_counter.Enums2;

namespace New_Structure
{
    class ValidateText
    {
        /// <summary>
        /// Проверка корректности текста
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public NextStep CheckSymbols(string text)
        {
            string textlower = text.ToLower();

            foreach (var item in textlower)
            {
                if ((item < 'a' || item > 'z') && (item != ' ' && item != '.' && item != ',' && item != '!' && item != '?' && item != ';' && item != ':'))
                {
                    Console.WriteLine("Неверный текст");

                    return NextStep.ChooseRepeatOperation;
                }

                else
                    continue;
            }

            return NextStep.ChoiseSaveInputData;
        }
    }
}
