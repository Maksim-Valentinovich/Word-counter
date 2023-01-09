using System;
using System.IO;
using Word_counter.Enums;
using Word_counter.Enums2;

namespace New_Structure
{
    class Program
    {
        /// <summary>
        /// Шаги выполнения алгоритма
        /// </summary>
        static void Step()
        {
            string text = string.Empty;

            string path = string.Empty;

            bool PrintIsWork = false;

            int count = 0;

            NextStep step = NextStep.Start;

            while (true)
            {
                switch (step)
                {
                    case NextStep.Start:
                        PrintIsWork = false;
                        step = Start(ref text);
                        count = 0;
                        break;

                    case NextStep.CheckText:
                        step = CheckText(text);
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

        /// Проверка текста.
        static NextStep CheckText(string text)
        {
            string textlower = text.ToLower();

            ///проверка наличия символов впринципе.

            foreach (var item in textlower) // --> Не хватает проверки пунктуации
            {
                if (item < 'a' || item > 'z')
                {
                    Console.WriteLine("Неверный текст");

                    return NextStep.ChooseRepeatOperation;
                }

                else
                    continue;
            }

            return NextStep.ChoiseSaveInputData;
        }

        /// Вывод слов на консоль
        static NextStep PrintText(string text, ref int count)
        {
            bool startWord = false;

            bool finishWord = false;

            string textlower = text.ToLower();

            string sumbolsPunctuation = ".,:;!?";

            // Можно сделать короче, но в таком случае не реализуется весь функционал, который я задумал 

            ///короткая реализация счётчика слов , но она очень глупая, я не понимаю как её развивать дальше
            //string[] textMass;

            //textMass = text.Split(new char[] {' '});

            //Console.WriteLine("Количество слов:");

            //Console.WriteLine(textMass.Length);


            foreach (var item in textlower)
            {
                ///проверка знаков препинания 
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

                ///если символ совпадает с пробелом и начало слова == true
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

            Console.WriteLine("\nКоличество слов в тексте = {0}", count); // вывод сообщения о количестве слов в тексте

            return NextStep.ChoiseSaveResult;
        }

        /// Создание текста при сохраннии результатов  // --> Описание странное
        static NextStep CreateFile(string path, string text, int count, bool PrintIsWork)
        {
            if (PrintIsWork == true)
            {
                string createText = "Lab1.cpp \nЛабораторная работа № 1 \nИспользование языка С# для математических расчетов " +
                                        "\nВычислить количество слов в нем и распечатать эти слова (по одному в строке) " +
                                        "\nСтудент группы 001, Иванов Максим Валентинович, 2022 год" + "\nТекст: " + text + "\nКоличество слов в тексте = " + count;

                File.WriteAllText(path, createText);

                Console.WriteLine("Сохранение успешно!");

                return NextStep.ChooseRepeatOperation;
            }

            else
            {
                File.WriteAllText(path, text);

                Console.WriteLine("Сохранение успешно!");

                return NextStep.PrintText;
            }
        }

        /// Сохранение результатов
        static NextStep Saving(ref string path)  // --> Это явно надо переписать
        {
            Console.WriteLine("Введите путь сохранения файла результатов");

            path = Console.ReadLine();

            try
            {
                CheckFile(path);
            }
            catch (FormatException)
            {
                return NextStep.Saving;  // --> понятней будет если сразу рекурсивно вызвать этот же метод
            }
            catch (FileNotFoundException)
            {
                return NextStep.CreateFile;  // --> дальше нигде не предлагается его создать, или ввести другое имя
            }

            Console.WriteLine("Файл уже существует. Выберите действие: \n1-Перезаписать \n2-Указать новое имя файла"); // --> а 

            string Choise = Console.ReadLine();

            try
            {
                CheckSelection(Choise);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return NextStep.Saving;
            }

            NewNameFile newnameFile = (NewNameFile)Enum.Parse(typeof(NewNameFile), Choise);

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
                    Console.WriteLine("Неверный ввод");
                    return NextStep.Saving;
            }
        }

        /// Выбор сохранения исходных данных
        static NextStep ChoiseSaveInputData()
        {
            Console.WriteLine("Сохранить исходные данный в файл? \n1 - Да \n2 - Нет");

            string Choise = Console.ReadLine();

            try
            {
                CheckSelection(Choise);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return NextStep.ChoiseSaveInputData;
            }

            SaveInputData saveInputData = (SaveInputData)Enum.Parse(typeof(SaveInputData), Choise);

            switch (saveInputData)
            {
                case SaveInputData.Save:
                    return NextStep.Saving;

                case SaveInputData.NotSave:
                    return NextStep.PrintText;

                default:
                    Console.WriteLine("Неверный ввод");
                    return NextStep.ChoiseSaveInputData;
            }
        }

        /// Выбор повтора операции
        static NextStep ChooseRepeatOperation()
        {
            while (true)
            {
                Console.WriteLine("Повторить операцию? \n1 - да \n2 - нет");

                string Choise = Console.ReadLine();

                try
                {
                    CheckSelection(Choise);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    return NextStep.ChooseRepeatOperation;
                }

                RepeatOperation repeatoperation = (RepeatOperation)Enum.Parse(typeof(RepeatOperation), Choise);

                switch (repeatoperation)
                {
                    case RepeatOperation.Repeat:
                        return NextStep.Start;

                    case RepeatOperation.Exit:
                        return NextStep.Exit;

                    default:
                        Console.WriteLine("Неверный ввод");
                        return NextStep.ChooseRepeatOperation;
                }
            }
        }

        /// Выбор сохранения результатов
        static NextStep ChoiseSaveResult()
        {
            Console.WriteLine("Cохранить результаты? \n1 - да \n2 - нет");

            string Choise = Console.ReadLine();
            try
            {
                CheckSelection(Choise);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return NextStep.ChoiseSaveResult;
            }

            SaveText savethetext = (SaveText)Enum.Parse(typeof(SaveText), Choise);

            switch (savethetext)
            {
                case SaveText.saveText:
                    return NextStep.Saving;

                case SaveText.ChooseRepeatOperation:
                    return NextStep.ChooseRepeatOperation;

                default:
                    Console.WriteLine("Неверный ввод");
                    return NextStep.ChoiseSaveResult;
            }
        }

        /// Проверка имени файла 
        static void CheckFile(string path)
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

        /// Чтение файла
        static NextStep ReadFile(ref string text, ref string path)
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

                try
                {
                    CheckSelection(Choise);  // --> там нигде не проверяется диапазон для выбора
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    return NextStep.ChooseRepeatOperation;
                }

                ContinueOperation continueoperation = (ContinueOperation)Enum.Parse(typeof(ContinueOperation), Choise);

                switch (continueoperation)
                {
                    case ContinueOperation.WriteNewFileName:
                        return NextStep.ReadFile;

                    case ContinueOperation.Exit:
                        return NextStep.Exit;

                    default:
                        Console.WriteLine("Неверный ввод");
                        return NextStep.ChooseRepeatOperation;
                }
            }

            text = File.ReadAllText(path);

            Console.WriteLine(text);

            return NextStep.CheckText;
        }

        static void CheckSelection(string Choise)  // --> что-то лишнее, по названию непонятно зачем
        {
            int NumberChoise = int.Parse(Choise);
        }

        /// Основное тело программы
        static NextStep Start(ref string text)
        {
            Console.WriteLine("Выберите способ ввода текста: \n1 - Ввод в консоль \n2 - Считывание из файла");

            string Choise = Console.ReadLine();

            try
            {
                CheckSelection(Choise);  // --> не проверяется диапазон выбора
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);  // --> такие вещи пользователь не поймёт, это отладочная информация

                return NextStep.Start;
            }

            SourceInputText sourceinputtext = (SourceInputText)Enum.Parse(typeof(SourceInputText), Choise);

            switch (sourceinputtext)
            {
                case SourceInputText.ConsoleInput:

                    Console.WriteLine("Введите текст на английском языке"); // --> я бы это вынес отдельно чтобы работало аналогично с файловым вводом в плане цепочки методов, а то тут ты сразу прыгаешь на проверку а ниже только на ввод из файла

                    text = Console.ReadLine();

                    return NextStep.CheckText;

                case SourceInputText.FileInput:

                    return NextStep.ReadFile;

                default:
                    Console.WriteLine("Неверный ввод");
                    return NextStep.Start;
            }
        }
        static void Main()
        {
            //[Начало программы 001]

            Step();
        }
    }
}
