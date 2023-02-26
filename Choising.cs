using System;
using Word_counter.Enums;
using Word_counter.Enums2;

namespace New_Structure
{
    class Choising
    {
        /// <summary>
        /// Сохраняем исходные данные?
        /// </summary>
        /// <returns></returns>
        public NextStep ChoiseSaveInputData()
        {
            Console.WriteLine("Сохранить исходные данный в файл? \n1 - Да \n2 - Нет");

            string Choise = Console.ReadLine();

            SaveInputData saveInputData;

            ///Валидация выбора пользователя
            try
            {
                saveInputData = (SaveInputData)Enum.Parse(typeof(SaveInputData), Choise);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Неверный ввод!");

                return NextStep.ChoiseSaveInputData;
            }

            switch (saveInputData)
            {
                case SaveInputData.Save:
                    return NextStep.Saving;

                case SaveInputData.NotSave:
                    return NextStep.PrintText;

                default:
                    Console.WriteLine("Выбор не соответствует заданному диапазону!");
                    return NextStep.ChoiseSaveInputData;
            }
        }

        /// <summary>
        /// Сохраняем результаты?
        /// </summary>
        /// <returns></returns>
        public NextStep ChoiseSaveResult()
        {
            Console.WriteLine("Cохранить результаты? \n1 - да \n2 - нет");

            string Choise = Console.ReadLine();

            SaveText savethetext;

            ///валидация выбора пользователя
            try
            {
                savethetext = (SaveText)Enum.Parse(typeof(SaveText), Choise);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Неверный ввод!");

                return NextStep.ChoiseSaveResult;
            }

            switch (savethetext)
            {
                case SaveText.saveText:
                    return NextStep.Saving;

                case SaveText.ChooseRepeatOperation:
                    return NextStep.ChooseRepeatOperation;

                default:
                    Console.WriteLine("Выбор не соответствует заданному диапазону!");
                    return NextStep.ChoiseSaveResult;
            }
        }

        /// <summary>
        /// Повторяем операцию?
        /// </summary>
        /// <returns></returns>
        public NextStep ChooseRepeatOperation()
        {
            Console.WriteLine("Повторить операцию? \n1 - да \n2 - нет");

            string Choise = Console.ReadLine();

            RepeatOperation repeatoperation;

            ///Валидация выбора пользователя
            try
            {
                repeatoperation = (RepeatOperation)Enum.Parse(typeof(RepeatOperation), Choise);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Неверный ввод!");

                return NextStep.ChooseRepeatOperation;
            }

            switch (repeatoperation)
            {
                case RepeatOperation.Repeat:
                    return NextStep.Start;

                case RepeatOperation.Exit:
                    return NextStep.Exit;

                default:
                    Console.WriteLine("Выбор не соответствует заданному диапазону!");
                    return NextStep.ChooseRepeatOperation;
            }

        }
    }
}
