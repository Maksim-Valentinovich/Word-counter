﻿using System;
using System.IO;
using Word_counter.Enums;
using Word_counter.Enums2;

namespace New_Structure
{
    class File
    {
        /// <summary>
        /// Проверка файла
        /// </summary>
        /// <param name="path"></param>    
        public void CheckFile(string path)
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

        /// <summary>
        /// Чтение файла
        /// </summary>
        /// <param name="text"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public NextStep ReadFile(ref string text, ref string path)
        {
            Console.Write("Введите путь к файлу: ");

            path = Console.ReadLine();

            try
            {
                CheckFile(path);
            }
            catch (FormatException)
            {
                File file = new File();

                file.ReadFile(ref text, ref path);

                //return NextStep.ReadFile;
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файла по указанному пути не существует! \n1-Ввести новое имя файла \n2-Завершить программу");

                string Choise = Console.ReadLine();

                ContinueOperation continueoperation;

                /// проверка адекватности выбора пользователя
                try
                {
                    continueoperation = (ContinueOperation)Enum.Parse(typeof(ContinueOperation), Choise);
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Неверный ввод!");

                    Choising choising = new Choising();
                    
                    return choising.ChooseRepeatOperation();

                    //return NextStep.ChooseRepeatOperation;
                }

                switch (continueoperation)
                {
                    case ContinueOperation.WriteNewFileName:

                        File file = new File();

                        return file.ReadFile(ref text, ref path);

                        //return NextStep.ReadFile;

                    case ContinueOperation.Exit:
                        return NextStep.Exit;

                    default:
                        Console.WriteLine("Выбор не соответствует заданному диапазону!");
                        
                        Choising choising = new Choising();

                        return choising.ChooseRepeatOperation();

                        //return NextStep.ChooseRepeatOperation;
                }
            }

            //text = File.ReadAllText(path);

            Console.WriteLine(text);

            WorkText workText = new WorkText(ref count);

            return workText.CheckText(text);

            //return NextStep.CheckText;
        }
    }
}
