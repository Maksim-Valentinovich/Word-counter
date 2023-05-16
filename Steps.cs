using Npgsql;
using System;
using System.Collections.Generic;
using System.IO;
using Word_counter.Enums;
using Word_counter.Enums2;

namespace New_Structure
{
    //interface A { void Test(); }
    //class B : A { public void Test() { } }
    //class C : A { public void Test() { } }

    //public static class Fabric
    //{
    //    public static A GetA(int num)
    //    {
    //        return num switch
    //        {
    //            1 => new B(),
    //            2 => new C(),
    //        };
    //    }
    //}


    class Steps
    {
        //private A a = new C();

        private string text = string.Empty;

        private string path = string.Empty;

        private int count = 0;

        private List<char> textAfterCheck = new();


        public void Move(NextStep step)
        {

            //a = Fabric.GetA(1);

            while (true)
            {
                switch (step)
                {
                    case NextStep.Start:
                        step = Start();
                        count = 0;
                        break;

                    case NextStep.InputDataBase:
                        step = InputData();
                        break;

                    case NextStep.WriteText:
                        step = WriteText();
                        break;

                    case NextStep.CheckText:
                        step = CheckText();
                        break;

                    case NextStep.ChoiseAction:
                        step = ChoiseAction();
                        break;

                    //case NextStep.MakeNewString:
                    //    step = MakeNewString();
                    //    break;

                    case NextStep.CountWords:
                        step = CountWords();
                        break;

                    case NextStep.PrintText:
                        step = PrintText();
                        break;

                    case NextStep.ChoiseSaveResult:
                        step = ChoiseSaveResult();
                        break;

                    case NextStep.ChoiseSaveInputData:
                        step = ChoiseSaveInputData();
                        break;

                    case NextStep.ChoiseRepeatOperation:
                        step = ChoiseRepeatOperation();
                        break;

                    case NextStep.ChoisePrintText:
                        step = ChoosePrintText();
                        break;

                    case NextStep.Saving:
                        step = Saving();
                        break;

                    case NextStep.ReadFile:
                        step = ReadFile();
                        break;

                    case NextStep.CreateFile:
                        step = CreateFile();
                        break;

                    case NextStep.Exit:
                        Console.WriteLine("Goodbye!");
                        Console.ReadLine();
                        return;
                }
            }
        }

        /// <summary>
        /// Выбор характера поступления исходных данных
        /// </summary>
        /// <returns></returns>
        private NextStep Start()
        {

            while (true)
            {
                Console.WriteLine("Выберите способ ввода текста: \n1 - Ввод в консоль \n2 - Считывание из файла \n3 - Загрузка из базы данных");

                string Choise = Console.ReadLine();

                SourceInputText sourceinputtext;

                /// Валидация выбора пользователя 
                try
                {
                    sourceinputtext = (SourceInputText)Enum.Parse(typeof(SourceInputText), Choise);
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("\nНеверный ввод!");

                    break;
                }

                switch (sourceinputtext)
                {
                    case SourceInputText.ConsoleInput:

                        return NextStep.WriteText;

                    case SourceInputText.FileInput:

                        return NextStep.ReadFile;

                    case SourceInputText.DataInput:

                        return NextStep.InputDataBase;

                    default:
                        Console.WriteLine("\nВыбор не соответствует заданному диапазону!");
                        break;
                }
            }
            return NextStep.Start;
        }

        private NextStep InputData()
        {
            DataBase dataBase = new DataBase();

            string connectionString = "Host=localhost;Username=postgres;Password=31127;Database=One";

            using NpgsqlDataSource dataSource = NpgsqlDataSource.Create(connectionString);

            text = dataBase.OutputDB(dataSource);

            return NextStep.CheckText;
        }

        /// <summary>
        /// Чтение файла
        /// </summary>
        /// <param name="text"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        private NextStep ReadFile()
        {
            ActionsFile actions = new ActionsFile();

            while (true)
            {
                Console.Write("\nВведите путь к файлу: ");

                path = Console.ReadLine();

                try
                {
                    actions.CheckFile(path);
                }
                catch (FormatException)
                {
                    Console.WriteLine("\nТип файла не соответствует требуемому 'txt', \n Повторите ввод!");

                    break;
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("\nФайла по указанному пути не существует! \n1-Ввести новое имя файла \n2-Завершить программу");

                    string Choise = Console.ReadLine();

                    ContinueOperation continueoperation;

                    /// Валидация пользовательского выбора
                    try
                    {
                        continueoperation = (ContinueOperation)Enum.Parse(typeof(ContinueOperation), Choise);
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine("\nНеверный ввод!");

                        return NextStep.ChoiseRepeatOperation; ;
                    }

                    switch (continueoperation)
                    {
                        case ContinueOperation.WriteNewFileName:
                            break;

                        case ContinueOperation.Exit:
                            return NextStep.Exit;

                        default:
                            Console.WriteLine("\nВыбор не соответствует заданному диапазону!");
                            return NextStep.ChoiseRepeatOperation;
                    }
                }
            }

            if (actions.ReadFile(ref text, path))
            {
                Console.WriteLine(text);
            }

            return NextStep.CheckText;
        }

        /// <summary>
        /// Ввод текста с консоли
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private NextStep WriteText()
        {
            Console.WriteLine("\nВведите текст, на английском языке, состоящий из слов, записанных через пробелы или знаки препинания: ! . , ? : ; ");

            text = Console.ReadLine();

            return NextStep.CheckText;
        }

        private NextStep ChoiseAction()
        {
            Console.WriteLine("\nВыберите действие, реализуемое с текстом: \n1 - Подсчёт количества слов \n2 - Сформировать новую строку, включающаю слова " +
                " не содержащие q, r и u.");

            string Choise = Console.ReadLine();

            SelectionAction action;

            ///Валидация пользовательского выбора
            try
            {
                action = (SelectionAction)Enum.Parse(typeof(SelectionAction), Choise);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("\nНеверный ввод!");

                return NextStep.ChoiseRepeatOperation;
            }

            switch (action)
            {
                case SelectionAction.CountWords:
                    return NextStep.CountWords;

                case SelectionAction.MakeNewString:
                    return NextStep.MakeNewString;

                default:
                    Console.WriteLine("\nВыбор не соответствует заданному диапазону!");
                    return NextStep.ChoiseRepeatOperation;
            }

        }

        //private NextStep MakeNewString()
        //{
        //    ActionsText actions = new ActionsText();

        //    actions.CountWordsАlgorithm(text, textAfterCheck);

        //    try
        //    {
        //        actions.MakeStringАlgorithm(textAfterCheck);
        //    }
        //    catch (Exception)
        //    {
        //        Console.WriteLine("В тексте нет слов, содержащих буквы q r и u");

        //        return NextStep.ChoiseRepeatOperation;
        //    }

        //    Console.WriteLine(textAfterCheck);

        //    return NextStep.ChoisePrintText;
        //}

        /// <summary>
        /// Проверка совпадения символов текста
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private NextStep CheckText()
        {
            ActionsText validate = new ActionsText();

            if (validate.CheckSymbols(text))
            {
                Console.WriteLine("\nПроверка текста прошлоа успешно!");

                return NextStep.ChoiseSaveInputData;
            }

            Console.WriteLine("\nНеверный текст!");

            return NextStep.ChoiseRepeatOperation;
        }

        /// <summary>
        /// Подсчёт количества слов
        /// </summary>
        /// <param name="text"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        private NextStep CountWords()
        {
            ActionsText estimation = new ActionsText();

            Console.WriteLine("\nКоличество слов в тексте = {0}", estimation.CountWordsАlgorithm(text, textAfterCheck));

            return NextStep.ChoisePrintText;
        }

        /// <summary>
        /// Вывод слов на консоль
        /// </summary>
        /// <param name="text"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        private NextStep PrintText()
        {
            bool startWord = false;

            int count = 0;

            foreach (var symbol in textAfterCheck)
            {
                if (symbol >= 'a' && symbol <= 'z')
                {
                    if (!startWord)
                    {
                        count++;

                        Console.Write("\nСлово {0}: ", count);
                    }

                    Console.Write(symbol);

                    startWord = true;
                }
                else
                {
                    Console.WriteLine(symbol);

                    startWord = false;
                }
            }
            return NextStep.ChoiseSaveResult;
        }

        /// <summary>
        /// Сохранение исходных данных - результатов
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private NextStep Saving()
        {
            ActionsFile actions = new ActionsFile();

            while (true)
            {
                Console.WriteLine("\nВведите путь сохранения файла и его имя");

                path = Console.ReadLine();

                ///Валидация формата 'txt' в имени файла и его существования в указанном месте
                try
                {
                    actions.CheckFile(path);
                }
                catch (FormatException)
                {
                    Console.WriteLine("\nТип файла не соответствует требуемому 'txt', \n Повторите ввод!");

                    break;
                }
                catch (FileNotFoundException)
                {
                    return NextStep.CreateFile;
                }

                Console.WriteLine("\nФайл уже существует. Выберите действие: \n1-Перезаписать \n2-Указать новое имя файла");

                string Choise = Console.ReadLine();

                NewNameFile newNameFile;

                ///Валидация выбора пользователя
                try
                {
                    newNameFile = (NewNameFile)Enum.Parse(typeof(NewNameFile), Choise);
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("\nНеверный ввод");

                    break;
                }

                switch (newNameFile)
                {
                    case NewNameFile.Record:
                        if (actions.RecordFile(path))
                        {
                            return NextStep.CreateFile;
                        }
                        break;

                    case NewNameFile.NewName:
                        break;

                    default:
                        Console.WriteLine("\nВыбор не соответствует заданному диапазону!");
                        break;
                }
            }
            return NextStep.ChoiseRepeatOperation;
        }

        /// <summary>
        /// Сохраняем исходные данные?
        /// </summary>
        /// <returns></returns>
        private NextStep ChoiseSaveInputData()
        {
            while (true)
            {
                Console.WriteLine("\nСохранить исходные данный в файл? \n1 - Да \n2 - Нет");

                string Choise = Console.ReadLine();

                SaveInputData saveInputData;

                ///Валидация пользовательского выбора
                try
                {
                    saveInputData = (SaveInputData)Enum.Parse(typeof(SaveInputData), Choise);
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("\nНеверный ввод!");

                    break;
                }

                switch (saveInputData)
                {
                    case SaveInputData.Save:
                        return NextStep.Saving;

                    case SaveInputData.NotSave:
                        return NextStep.ChoiseAction;

                    default:
                        Console.WriteLine("\nВыбор не соответствует заданному диапазону!");
                        break;
                }
            }
            return NextStep.ChoiseSaveInputData;
        }

        /// <summary>
        /// Сохраняем результаты?
        /// </summary>
        /// <returns></returns>
        private NextStep ChoiseSaveResult()
        {
            Console.WriteLine("\nCохранить результаты? \n1 - да \n2 - нет");

            string Choise = Console.ReadLine();

            SaveText savethetext;

            ///Валидация пользовательского выбора
            try
            {
                savethetext = (SaveText)Enum.Parse(typeof(SaveText), Choise);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("\nНеверный ввод!");

                return NextStep.ChoiseSaveResult;
            }

            switch (savethetext)
            {
                case SaveText.saveText:
                    return NextStep.Saving;

                case SaveText.ChooseRepeatOperation:
                    return NextStep.ChoiseRepeatOperation;

                default:
                    Console.WriteLine("\nВыбор не соответствует заданному диапазону!");
                    return NextStep.ChoiseSaveResult;
            }
        }

        /// <summary>
        /// Повторяем операцию?
        /// </summary>
        /// <returns></returns>
        private NextStep ChoiseRepeatOperation()
        {
            Console.WriteLine("\nПовторить операцию? \n1 - да \n2 - нет");

            string Choise = Console.ReadLine();

            RepeatOperation repeatoperation;

            ///Валидация пользовательского выбора
            try
            {
                repeatoperation = (RepeatOperation)Enum.Parse(typeof(RepeatOperation), Choise);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("\nНеверный ввод!");

                return NextStep.ChoiseRepeatOperation;
            }

            switch (repeatoperation)
            {
                case RepeatOperation.Repeat:
                    return NextStep.Start;

                case RepeatOperation.Exit:
                    return NextStep.Exit;

                default:
                    Console.WriteLine("\nВыбор не соответствует заданному диапазону!");
                    return NextStep.ChoiseRepeatOperation;
            }
        }

        /// <summary>
        /// Печатаем слова из текста? 
        /// </summary>
        /// <returns></returns>
        private NextStep ChoosePrintText()
        {
            Console.WriteLine("\nВывести слова на консоль? \n1 - да \n2 - нет");

            string Choise = Console.ReadLine();

            PrintTxt printText;

            ////Валидация пользовательского выбора
            try
            {
                printText = (PrintTxt)Enum.Parse(typeof(PrintTxt), Choise);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("\nНеверный ввод!");

                return NextStep.ChoisePrintText;
            }

            switch (printText)
            {
                case PrintTxt.Print:
                    return NextStep.PrintText;

                case PrintTxt.NoPrint:
                    return NextStep.ChoiseSaveResult;

                default:
                    Console.WriteLine("\nВыбор не соответствует заданному диапазону!");
                    return NextStep.ChoisePrintText;
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
        private NextStep CreateFile()
        {
            ActionsFile actions = new ActionsFile();

            if (count != 0) // ФЛАГ !!! 
            {
                actions.CreateFileResult(path, text, count);

                Console.WriteLine("\nСохранение результатов прошло успешно!");

                return NextStep.ChoiseRepeatOperation;
            }
            else
            {
                actions.CreateFileInputData(path, text);

                Console.WriteLine("\nСохранение вводных данных прошло успешно!");

                return NextStep.ChoiseAction;
            }
        }
    }
}
