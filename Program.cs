using System;
using System.IO;
using Word_counter.Enums;
using Word_counter.Enums2;

namespace New_Structure
{
    class Steps
    {
        private string text = string.Empty;

        private string path = string.Empty;

        private bool PrintIsWork = false;

        private int count = 0;


        public void Move(NextStep step)
        {
            while (true)
            {
                switch (step)
                {
                    case NextStep.Start:
                        PrintIsWork = false;
                        step = Start();
                        count = 0;
                        break;

                    case NextStep.WriteText:
                        step = WriteText(ref text, ref count);
                        break;

                    case NextStep.PrintText:
                        PrintIsWork = true;
                        step = PrintText(text, ref count);
                        break;

                    case NextStep.ChoiseSaveResult:
                        step = ChoiseSaveResult();
                        break;

                    case NextStep.ChoiseSaveInputData:
                        step = ChoiseSaveInputData();
                        break;

                    case NextStep.ChooseRepeatOperation:
                        step = ChooseRepeatOperation();
                        break;

                    case NextStep.Saving:
                        step = Saving(ref path);
                        break;

                    case NextStep.ReadFile:
                        step = ReadFile(ref text, ref path);
                        break;

                    case NextStep.CreateFile:
                        step = CreateFile(path, text, count, PrintIsWork);
                        break;

                    case NextStep.Exit:
                        Console.WriteLine("Goodbye!");
                        Console.ReadLine();
                        return;
                }
            }

        }

        /// <summary>
        /// Чтение и проверка файла исходных данных
        /// </summary>
        private class File
        {
            /// <summary>
            /// Проверка файла
            /// </summary>
            /// <param name="path"></param>    
            private void CheckFile(string path)
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
                    return NextStep.ReadFile;
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("Файла по указанному пути не существует! \n1-Ввести новое имя файла \n2-Завершить программу");

                    string Choise = Console.ReadLine();

                    ContinueOperation continueoperation;

                    try
                    {
                        continueoperation = (ContinueOperation)Enum.Parse(typeof(ContinueOperation), Choise);
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine("Неверный ввод!");

                        return NextStep.ChooseRepeatOperation;
                    }

                    switch (continueoperation)
                    {
                        case ContinueOperation.WriteNewFileName:
                            return NextStep.ReadFile;

                        case ContinueOperation.Exit:
                            return NextStep.Exit;

                        default:
                            Console.WriteLine("Выбор не соответствует заданному диапазону!");
                            return NextStep.ChooseRepeatOperation;
                    }
                }

                //text = File.ReadAllText(path);

                Console.WriteLine(text);

                return NextStep.CheckText;
            }
        }

        /// <summary>
        /// Проверка и вывод текста
        /// </summary>
        private class WorkText
        {
            private string text = string.Empty;

            private int count;

            public WorkText(string text, ref int count)
            {
                this.text = text;
                this.count = count;
            }

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

                        return NextStep.ChooseRepeatOperation;
                    }

                    else
                        continue;
                }

                Print(text, ref count);

                return NextStep.ChoiseSaveResult;
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

        private class SaveAndChoise
        {
            private NextStep Saving(ref string path)  // --> Это явно надо переписать
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

        }

        /// <summary>
        /// Выбор характера поступления исходных данных
        /// </summary>
        /// <returns></returns>
        private NextStep Start()
        {
            Console.WriteLine("Выберите способ ввода текста: \n1 - Ввод в консоль \n2 - Считывание из файла");

            string Choise = Console.ReadLine();

            SourceInputText sourceinputtext;

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

        /// <summary>
        /// Чтение файла
        /// </summary>
        /// <param name="text"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        private NextStep ReadFile(ref string text, ref string path)
        {
            File file = new File();
            return file.ReadFile(ref text, ref path);
        }

        /// <summary>
        /// Проверка файла
        /// </summary>
        /// <param name="path"></param>    
        private void CheckFile(string path)
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
        /// Ввод текста с консоли
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private NextStep WriteText(ref string text, ref int count)
        {
            Console.WriteLine("Введите текст на английском языке");

            text = Console.ReadLine();

            return NextStep.PrintText;
        }

        /// <summary>
        /// Вывод слов на консоль
        /// </summary>
        /// <param name="text"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        private NextStep PrintText(string text, ref int count)
        {
            WorkText workText = new WorkText(text, ref count);

            return workText.CheckText(text);
        }

        /// <summary>
        /// Сохранение исходных данных - результатов
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private NextStep Saving(ref string path)  // --> Это явно надо переписать
        {
            Console.WriteLine("Введите путь сохранения файла результатов");

            path = Console.ReadLine();

            try
            {
                CheckFile(path);
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

        /// <summary>
        /// Сохраняем исходные данные?
        /// </summary>
        /// <returns></returns>
        private NextStep ChoiseSaveInputData()
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
        private NextStep ChoiseSaveResult()
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
        private NextStep ChooseRepeatOperation()
        {
            while (true)
            {
                Console.WriteLine("Повторить операцию? \n1 - да \n2 - нет");

                string Choise = Console.ReadLine();

                RepeatOperation repeatoperation;

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

    }

    class Program
    {
        static void Main()
        {
            Steps steps = new Steps();
            steps.Move(NextStep.Start);
        }
    }
}
