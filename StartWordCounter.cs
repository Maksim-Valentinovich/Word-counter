using System;
using Word_counter.Enums;
using Word_counter.Enums2;

namespace Word_counter
{
     class StartWordCounter
    {
        /// <summary>
        /// Выбор характера поступления исходных данных
        /// </summary>
        /// <returns></returns>
        public NextStep Start()
        {
            Console.WriteLine("Выберите способ ввода текста: \n1 - Ввод в консоль \n2 - Считывание из файла");

            string Choise = Console.ReadLine();

            SourceInputText sourceinputtext;

            /// Валидация выбора пользователя 
            try
            {
                sourceinputtext = (SourceInputText)Enum.Parse(typeof(SourceInputText), Choise);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Неверный ввод!");

                return NextStep.Start;
            }

            switch (sourceinputtext)
            {
                case SourceInputText.ConsoleInput:

                    return NextStep.WriteText;

                case SourceInputText.FileInput:

                    return NextStep.ReadFile;

                default:
                    Console.WriteLine("Выбор не соответствует заданному диапазону!");
                    return NextStep.Start;
            }
        }



    }
}
