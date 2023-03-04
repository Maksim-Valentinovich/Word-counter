using System;
using System.IO;
using Word_counter;
using Word_counter.Enums;
using Word_counter.Enums2;

namespace New_Structure
{
     class Variables 
    {
        private static string text = string.Empty;

        private static int count = 0;

        public static int Count
        {
            get { return count; }
            set { count = value; }
        }

    }

    class Steps
    {
        private static string text = string.Empty;

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
                        step = WriteText();
                        break;

                    case NextStep.CheckText:
                        step = CheckText();
                        break;

                    case NextStep.CountWords:
                        step = CountWords();
                        break;

                    case NextStep.PrintText:
                        PrintIsWork = true;
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

                    break;
                }

                switch (sourceinputtext)
                {
                    case SourceInputText.ConsoleInput:

                        return NextStep.WriteText;

                    case SourceInputText.FileInput:

                        return NextStep.ReadFile;

                    default:
                        Console.WriteLine("Выбор не соответствует заданному диапазону!");
                        break;
                }
            }
            return NextStep.Start;
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
                Console.Write("Введите путь к файлу: ");

                path = Console.ReadLine();

                try
                {
                    actions.CheckFile(path);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Тип файла не соответствует требуемому 'txt', \n Повторите ввод!");

                    break;
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("Файла по указанному пути не существует! \n1-Ввести новое имя файла \n2-Завершить программу");

                    string Choise = Console.ReadLine();

                    ContinueOperation continueoperation;

                    /// Валидация выбора пользователя
                    try
                    {
                        continueoperation = (ContinueOperation)Enum.Parse(typeof(ContinueOperation), Choise);
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine("Неверный ввод!");

                        return NextStep.ChoiseRepeatOperation;
                    }

                    switch (continueoperation)
                    {
                        case ContinueOperation.WriteNewFileName:
                            break;

                        case ContinueOperation.Exit:
                            return NextStep.Exit;

                        default:
                            Console.WriteLine("Выбор не соответствует заданному диапазону!");
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
            WriteInputText write = new WriteInputText();

            text = write.WriteText();

            return NextStep.CheckText;
        }

        /// <summary>
        /// Проверка совпадения символов текста
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private NextStep CheckText() 
        {
            ValidateText validate = new ValidateText();

            if (validate.CheckSymbols(text)) 
            {
                return NextStep.CountWords;
            }

            Console.WriteLine("Неверный текст!");

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
            Estimation estimation = new Estimation();

            Console.WriteLine("\nКоличество слов в тексте = {0}", estimation.CountWords(text));

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
            Print print = new Print();

            if (print.PrintTextOnConsole(text))
            {
                return NextStep.ChoiseSaveResult;
            }
            return NextStep.ChoiseRepeatOperation;
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
                Console.WriteLine("Введите путь сохранения файла и его имя");

                path = Console.ReadLine();

                ///Валидация формата 'txt' в имени файла и его существования в указанном месте
                try
                {
                    actions.CheckFile(path);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Тип файла не соответствует требуемому 'txt', \n Повторите ввод!");

                    break;
                }
                catch (FileNotFoundException)
                {
                    return NextStep.CreateFile;
                }

                Console.WriteLine("Файл уже существует. Выберите действие: \n1-Перезаписать \n2-Указать новое имя файла");

                string Choise = Console.ReadLine();

                NewNameFile newNameFile;

                ///Валидация выбора пользователя
                try
                {
                    newNameFile = (NewNameFile)Enum.Parse(typeof(NewNameFile), Choise);
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Неверный ввод");

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
                        Console.WriteLine("Введите новое имя файла");
                        break;

                    default:
                        Console.WriteLine("Выбор не соответствует заданному диапазону!");
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
            Choising choising = new Choising();
            return choising.ChoiseSaveInputData();
        }

        /// <summary>
        /// Сохраняем результаты?
        /// </summary>
        /// <returns></returns>
        private NextStep ChoiseSaveResult()
        {
            Choising choising = new Choising();
            return choising.ChoiseSaveResult();
        }

        /// <summary>
        /// Повторяем операцию?
        /// </summary>
        /// <returns></returns>
        private NextStep ChoiseRepeatOperation()
        {
            Choising choising = new Choising();
            return choising.ChooseRepeatOperation();
        }

        /// <summary>
        /// Печатаем слова из текста? 
        /// </summary>
        /// <returns></returns>
        private NextStep ChoosePrintText() 
        {
            Choising choising = new Choising();
            return choising.ChoisePrintText();
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

            if (PrintIsWork)
            {
                actions.CreateFileResult(path, text, count);

                Console.WriteLine("Сохранение успешно!");

                return NextStep.ChoiseRepeatOperation;
            }
            else
            {
                actions.CreateFileInputData(path, text);

                Console.WriteLine("Сохранение успешно!");

                return NextStep.ChoisePrintText;
            }
        }
    }

    class Program
    {
        static void Main()
        {
            Steps steps = new Steps();
            steps.Move(NextStep.Start);
            Console.ReadLine();
        }
    }
}
