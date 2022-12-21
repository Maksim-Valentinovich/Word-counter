using Word_counter.Enums;
using Word_counter.Enums2;
using System;
using System.IO;

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

            string savingElement = string.Empty;

            int count = 0;

            NextStep step = NextStep.Start;

            while (true)
            {
                switch (step)
                {
                    case NextStep.Start:
                        step = Start(ref text);
                        count = 0;
                        break;

                    case NextStep.CheckText:
                        step = CheckText(text);
                        break;

                    case NextStep.PrintText:
                        step = PrintText(text, ref count);
                        break;

                    case NextStep.ChoiseSaveInputData:
                        step = ChoiseSaveInputData();
                        break;

                    case NextStep.ChoiseSaveResult:
                        step = ChoiseSaveResult();
                        break;

                    case NextStep.ChooseRepeatOperation:
                        step = ChooseRepeatOperation();
                        break;

                    case NextStep.SavingInputData:
                        step = SavingInputData(ref path, ref savingElement);
                        break;

                    case NextStep.SavingResult:
                        step = SavingResult(ref path, ref savingElement);
                        break;

                    case NextStep.ReadFile:
                        step = ReadFile(ref text, ref path);
                        break;

                    case NextStep.Create:
                        step = Create(path, text, count, savingElement);
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
            if (textlower.Length < 2)
            {
                Console.WriteLine("Текст не введён или в тексте нет слов!");

                return NextStep.ChooseRepeatOperation;
            }

            else
            {
                foreach (var item in textlower)
                {
                    if ((item > 'a' && item < 'z') && item != ' ')
                        return NextStep.PrintText;

                    else
                        continue;
                }

                Console.WriteLine("Неверный текст");

                return NextStep.ChooseRepeatOperation;
            }
        }

        /// Вывод слов на консоль
        static NextStep PrintText(string text, ref int count)
        {
            char[] Word = new char[text.Length];

            bool startWord = false;

            bool finishWord = false;

            int numberLetterInlWord = 0;

            string textLower = text.ToLower();

            string sumbolsPunctuation = ".,:;!?";

            foreach (var item in textLower)
            {
                if ((item > 'a' && item < 'z') && item != ' ')
                {
                    //startWord = true;

                    Word[numberLetterInlWord] = item;

                    numberLetterInlWord++;

                    //if (item == textLower.Length)
                    //{
                    //    count++;

                    //    finishWord = true;
                    //}
                }

                else if ((item == '.' || item > 122) && item != ' ')
                {
                    foreach (var prov in sumbolsPunctuation)
                    {
                        if (prov == item && startWord == true && finishWord == false)
                        {
                            Word[numberLetterInlWord] = item;

                            numberLetterInlWord++;

                            count++;

                            finishWord = true;
                        }

                        else
                        {
                            continue;
                        }
                    }
                }


                //else if ((item < 97 || item > 122) && item != ' ')
                //{
                //    foreach (var prov in sumbolsPunctuation)
                //    {
                //        if (prov == item && startWord == true && finishWord == false)
                //        {
                //            Word[numberLetterInlWord] = item;

                //            numberLetterInlWord++;

                //            count++;

                //            finishWord = true;
                //        }

                //        else
                //        {
                //            continue;
                //        }
                //    }
                //}

                    /// если символ совпадает с пробелом и начало слова == true


                else if (item == ' ' && startWord == true)
                {
                    count++;

                    finishWord = true;
                }

                else
                {
                    continue;
                }

                ///вывод слова на консоль
                if (startWord && finishWord)
                {
                    Console.Write("\nСлово {0}: ", count);

                    for (int a = 0; a < Word.Length; a++)
                    {
                        if (Word[a] != '\0')
                        {
                            Console.Write(Word[a]);

                            Word[a] = '\0';
                        }

                        else
                        {
                            break;
                        }
                    }

                    numberLetterInlWord = 0;

                    startWord = false;

                    finishWord = false;
                }
            }

            //for (int i = 0; i < text.Length; i++)
            //{
            //    char ch = textLower[i];

            //    int charnum = (int)ch;

            //    ///пропуск первого пробела в тексте
            //    if ((i == 0 && text[i] == ' ' && text.Length != 1) | (text[i] == ' ' && startWord == false && finishWord == false))
            //    {
            //        continue;
            //    }

            //    ///проверка знаков препинания 
            //    else if ((charnum < 97 || charnum > 122) && charnum != 32)
            //    {
            //        foreach (var prov in sumbolsPunctuation)
            //        {
            //            if (prov == ch && startWord == true && finishWord == false)
            //            {
            //                Word[numberLetterInlWord] = ch;

            //                numberLetterInlWord++;

            //                count++;

            //                finishWord = true;
            //            }

            //            else
            //            {
            //                continue;
            //            }
            //        }

            //    }

            //    ///если символ совпадает с пробелом и начало слова == true
            //    else if (text[i] == ' ' && startWord == true)
            //    {
            //        count++;

            //        finishWord = true;

            //    }

            //    else
            //    {
            //        Word[numberLetterInlWord] = ch;

            //        numberLetterInlWord++;

            //        startWord = true;

            //        if (i == (text.Length - 1))
            //        {
            //            count++;

            //            finishWord = true;
            //        }
            //    }

            //    ///вывод слова на консоль
            //    if (startWord && finishWord)
            //    {
            //        Console.Write("\nСлово {0}: ", count);

            //        for (int a = 0; a < Word.Length; a++)
            //        {
            //            if (Word[a] != '\0')
            //            {
            //                Console.Write(Word[a]);

            //                Word[a] = '\0';
            //            }
            //            else
            //            {
            //                break;
            //            }
            //        }

            //        numberLetterInlWord = 0;

            //        startWord = false;

            //        finishWord = false;
            //    }
            //}
            Console.WriteLine("\nКоличество слов в тексте = {0}", count); // вывод сообщения о количестве слов в тексте

            return NextStep.ChoiseSaveResult;
        }

        /// <summary>
        /// Создание текста при сохраннии результатов
        /// </summary>
        /// <param name="path"></param>
        /// <param name="text"></param>
        /// <param name="count"></param>
        static NextStep Create(string path, string text, int count, string savingElement)
        {
            string createText;

            ///Сохраняем результаты работы
            if (savingElement == "Result")
            {
                createText = "Lab1.cpp \nЛабораторная работа № 1 \nИспользование языка С# для математических расчетов " +
                            "\nВычислить количество слов в нем и распечатать эти слова (по одному в строке) " +
                            "\nСтудент группы 001, Иванов Максим Валентинович, 2022 год" + "\nТекст: " + text + "\nКоличество слов в тексте = " + count;

                File.WriteAllText(path, createText);

                Console.WriteLine("Сохранение успешно!");

                return NextStep.ChooseRepeatOperation;
            }

            ///Сохраняем вводные данные
            else if (savingElement == "InputData")
            {
                createText = text;

                File.WriteAllText(path, createText);

                Console.WriteLine("Сохранение успешно!");

                return NextStep.CheckText;
            }

            return NextStep.Exit;
        }

        /// Сохранение результатов
        static NextStep SavingResult(ref string path, ref string savingElement)
        {
            savingElement = "Result";

            while (true)
            {
                Console.WriteLine("Введите путь сохранения файла результатов");

                path = @"C:\Users\Учебный\Desktop\Программирование\Инорматика\Лаба 1\" + Console.ReadLine();

                CheckFile(ref path);

                FileInfo fileSV = new FileInfo(path);

                ///Файл по введённому пути существует
                if (fileSV.Exists == true)
                {
                    Console.WriteLine("Файл уже существует. Выберите действие: \n1-Перезаписать \n2-Указать новое имя файла");

                    string Choise = Console.ReadLine();

                    NewNameFile newnameFile = (NewNameFile)Enum.Parse(typeof(NewNameFile), Choise);

                    switch (newnameFile)
                    {
                        case NewNameFile.Record:

                            fileSV.Create();

                            return NextStep.Create;

                        case NewNameFile.NewName:

                            Console.WriteLine("Введите новое имя файла");

                            break;

                        default:
                            Console.WriteLine("Неверный ввод");
                            break;
                    }
                }

                ///Файл по введённому НЕ пути существует
                else
                {
                    return NextStep.Create;
                }
            }

        }

        /// Сохранение исходных данных
        static NextStep SavingInputData(ref string path, ref string savingElement)
        {
            savingElement = "InputData";

            while (true)
            {
                Console.WriteLine("Введите путь сохранения файла результатов");

                path = @"C:\Users\Учебный\Desktop\Программирование\Инорматика\Лаба 1\" + Console.ReadLine();

                CheckFile(ref path);

                FileInfo fileSV = new FileInfo(path);

                ///Файл по введённому пути существует
                if (fileSV.Exists == true)
                {
                    Console.WriteLine("Файл уже существует. Выберите действие: \n1-Перезаписать \n2-Указать новое имя файла: ");

                    string Choise = Console.ReadLine();

                    NewNameFile newnameFile = (NewNameFile)Enum.Parse(typeof(NewNameFile), Choise);

                    switch (newnameFile)
                    {
                        case NewNameFile.Record:

                            fileSV.Create();

                            return NextStep.Create;

                        case NewNameFile.NewName:
                            Console.WriteLine("Введите новое имя файла");
                            break;

                        default:
                            Console.WriteLine("Неверный ввод");
                            break;
                    }
                }

                ///Файл по введённому НЕ пути существует
                else
                {
                    return NextStep.Create;
                }

            }
        }

        /// Выбор сохранения исходных данных
        static NextStep ChoiseSaveInputData()
        {
            Console.WriteLine("Сохранить исходные данный в файл? \n1 - Да \n2 - Нет");

            string Choise = Console.ReadLine();

            SaveInputData saveInputData = (SaveInputData)Enum.Parse(typeof(SaveInputData), Choise);

            switch (saveInputData)
            {
                case SaveInputData.Save:
                    return NextStep.SavingInputData;

                case SaveInputData.NotSave:
                    return NextStep.CheckText;

                default:
                    Console.WriteLine("Неверный ввод");
                    break;
            }
            return NextStep.CheckText;
        }

        /// Выбор повтора операции
        static NextStep ChooseRepeatOperation()
        {
            while (true)
            {
                Console.WriteLine("Повторить операцию? \n1 - да \n2 - нет");

                string Choise = Console.ReadLine();

                RepeatOperation repeatoperation = (RepeatOperation)Enum.Parse(typeof(RepeatOperation), Choise);

                switch (repeatoperation)
                {
                    case RepeatOperation.Repeat:
                        return NextStep.Start;

                    case RepeatOperation.Exit:
                        return NextStep.Exit;

                    default:
                        Console.WriteLine("Неверный ввод");
                        break;
                }
            }
        }

        /// Выбор сохранения результатов
        static NextStep ChoiseSaveResult()
        {
            while (true)
            {
                Console.WriteLine("Cохранить результаты? \n1 - да \n2 - нет");

                string Choise = Console.ReadLine();

                SaveText savethetext = (SaveText)Enum.Parse(typeof(SaveText), Choise);

                switch (savethetext)
                {
                    case SaveText.saveText:
                        return NextStep.SavingResult;

                    case SaveText.ChooseRepeatOperation:
                        return NextStep.ChooseRepeatOperation;

                    default:
                        Console.WriteLine("Неверный ввод");
                        break;
                }
            }
        }

        /// Проверка имени файла 
        static void CheckFile(ref string path)
        {
            while (true)
            {
                string extension;

                extension = Path.GetExtension(path);

                if (extension != ".txt")
                {
                    Console.WriteLine("Тип файла не соответствует требуемому 'txt', \n Поворите ввод: ");

                    path = @"C:\Users\Учебный\Desktop\Программирование\Инорматика\Лаба 1\" + Console.ReadLine();
                }

                else
                    return;
            }
        }

        /// Чтение файла
        static NextStep ReadFile(ref string text, ref string path)
        {
            while (true)
            {
                Console.Write("Введите путь к файлу: ");

                path = @"C:\Users\Учебный\Desktop\Программирование\Инорматика\Лаба 1\" + Console.ReadLine();

                CheckFile(ref path);

                FileInfo fileInf = new FileInfo(path);

                ///Файл по введённому пути существует
                if (fileInf.Exists == true)
                {
                    text = File.ReadAllText(path);

                    Console.WriteLine(text);

                    return NextStep.CheckText;
                }

                ///Файл по введённому НЕ пути существует
                else
                {
                    Console.WriteLine("Файла по указанному пути не существует! \n1-Ввести новое имя файла \n2-Завершить программу");

                    string Choise = Console.ReadLine();

                    ContinueOperation continueoperation = (ContinueOperation)Enum.Parse(typeof(ContinueOperation), Choise);

                    switch (continueoperation)
                    {
                        case ContinueOperation.WriteNewFileName:
                            break;

                        case ContinueOperation.Exit:
                            return NextStep.Exit;

                        default:
                            Console.WriteLine("Неверный ввод");
                            break;
                    }
                }
            }
        }

        /// Основное тело программы
        static NextStep Start(ref string text)
        {
            while (true)
            {
                Console.WriteLine("Выберите способ ввода текста: \n1 - Ввод в консоль \n2 - Считывание из файла");

                string ChoiseA = Console.ReadLine();

                SourceInputText sourceinputtext = (SourceInputText)Enum.Parse(typeof(SourceInputText), ChoiseA);

                switch (sourceinputtext)
                {
                    case SourceInputText.ConsoleInput:

                        Console.WriteLine("Введите текст на английском языке");

                        text = Console.ReadLine();

                        return NextStep.ChoiseSaveInputData;

                    case SourceInputText.FileInput:

                        return NextStep.ReadFile;

                    default:
                        Console.WriteLine("Неверный ввод");
                        break;
                }
            }
        }
        static void Main()
        {
            //[Начало программы 001]

            Step();
        }
    }
}
