using System;
using System.IO;
using Word_counter.Enums;
using Word_counter.Enums2;

namespace New_Structure
{
    class Save
    {
        /// <summary>
        /// Сохранение
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public NextStep Saving(ref string path)  // --> Это явно надо переписать
        {
            Console.WriteLine("Введите путь сохранения файла");

            path = Console.ReadLine();

            ///Валидация формата 'txt' в имени файла и его существования в указанном месте
            try
            {
                WorkFile checkTxt = new WorkFile();
                checkTxt.CheckFile(path);
            }
            catch (FormatException)
            {
                return NextStep.Saving;
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
