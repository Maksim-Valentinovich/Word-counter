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

            try
            {
                savethetext = (SaveText)Enum.Parse(typeof(SaveText), Choise);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Неверный ввод!");

                Choising choising = new Choising();

                return choising.ChoiseSaveResult();

                //return NextStep.ChoiseSaveResult;
            }

            switch (savethetext)
            {
                case SaveText.saveText:

                    return NextStep.Saving;

                case SaveText.ChooseRepeatOperation:

                    Choising choising1 = new Choising();

                    return choising1.ChooseRepeatOperation();

                //return NextStep.ChooseRepeatOperation;

                default:
                    Console.WriteLine("Выбор не соответствует заданному диапазону!");

                    Choising choising2 = new Choising();

                    return choising2.ChoiseSaveResult();

                    //return NextStep.ChoiseSaveResult;
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

            ///Проверка адекватности выбора пользователя
            try
            {
                repeatoperation = (RepeatOperation)Enum.Parse(typeof(RepeatOperation), Choise);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Неверный ввод!");

                Choising choising1 = new Choising();

                return choising1.ChooseRepeatOperation();
            }

            switch (repeatoperation)
            {
                case RepeatOperation.Repeat:
                    return NextStep.Start;

                case RepeatOperation.Exit:
                    return NextStep.Exit;

                default:
                    Console.WriteLine("Выбор не соответствует заданному диапазону!");

                    Choising choising = new Choising();

                    return choising.ChooseRepeatOperation();

                    //return NextStep.ChooseRepeatOperation;
            }

        }
    }
}
