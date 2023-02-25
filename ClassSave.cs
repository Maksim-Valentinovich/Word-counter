using System;

using System.IO;
using Word_counter.Enums;
using Word_counter.Enums2;

namespace New_Structure
{
    class Save
    {
        /// <summary>
        /// Создание файла исходных данных либо результатов
        /// </summary>
        /// <param name="path"></param>
        /// <param name="text"></param>
        /// <param name="count"></param>
        /// <param name="PrintIsWork"></param>
        /// <returns></returns>
        private NextStep CreateFile(string path, string text, int count, bool PrintIsWork)
        {
            if (PrintIsWork == true)
            {
                string createText = "Lab1.cpp \nЛабораторная работа № 1 \nИспользование языка С# для математических расчетов " +
                                        "\nВычислить количество слов в нем и распечатать эти слова (по одному в строке) " +
                                        "\nСтудент группы 001, Иванов Максим Валентинович, 2022 год" + "\nТекст: " + text + "\nКоличество слов в тексте = " + count;

                //File.WriteAllText(path, createText);

                Console.WriteLine("Сохранение успешно!");

                return NextStep.ChooseRepeatOperation;
            }

            else
            {
                //File.WriteAllText(path, text);

                Console.WriteLine("Сохранение успешно!");

                return NextStep.PrintText;
            }
        }

        /// <summary>
        /// Сохранение
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public NextStep Saving(ref string path)  // --> Это явно надо переписать
        {
            Console.WriteLine("Введите путь сохранения файла результатов");

            path = Console.ReadLine();

            try
            {
                string extension;

                FileInfo fileInf = new FileInfo(path);

                extension = Path.GetExtension(path);

                if (extension != ".txt")
                {
                    Console.WriteLine("Тип файла не соответствует требуемому 'txt', \n Повторите ввод!");

                    throw new FormatException();
                }

                if (fileInf.Exists != true)
                {
                    throw new FileNotFoundException();
                }
            }
            catch (FormatException)
            {
                return Saving(ref path);
            }
            catch (FileNotFoundException)
            {
                return NextStep.CreateFile;  // --> дальше нигде не предлагается его создать, или ввести другое имя
            }

            Console.WriteLine("Файл уже существует. Выберите действие: \n1-Перезаписать \n2-Указать новое имя файла"); // --> а 

            string Choise = Console.ReadLine();

            NewNameFile newnameFile;

            try
            {
                newnameFile = (NewNameFile)Enum.Parse(typeof(NewNameFile), Choise);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Неверный ввод");

                return NextStep.Saving;
            }

            switch (newnameFile)
            {
                case NewNameFile.Record:
                    FileInfo fileSV = new FileInfo(path);
                    fileSV.Create();
                    return NextStep.CreateFile;

                case NewNameFile.NewName:
                    Console.WriteLine("Введите новое имя файла");
                    return NextStep.Saving;

                default:
                    Console.WriteLine("Выбор не соответствует заданному диапазону!");
                    return NextStep.Saving;
            }
        }
    }
}
