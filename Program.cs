using System;
using System.IO;
using Word_counter.Enums;
using Word_counter.Enums2;

namespace New_Structure
{
    static class Variables 
    {
        static string text = string.Empty;
        static int count = 0;
    }

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
                        step = WriteText(ref text);
                        break;

                    case NextStep.CheckText:
                        step = CheckText(text);
                        break;

                    case NextStep.CountWords:
                        step = CountWords(text, ref count);
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

                    case NextStep.ChooseRepeatOperation:
                        step = ChooseRepeatOperation();
                        break;

                    case NextStep.ChoisePrintText:
                        step = ChoosePrintText();
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
        /// Выбор характера поступления исходных данных
        /// </summary>
        /// <returns></returns>
        private NextStep Start()
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
            WorkFile file = new WorkFile();
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
        private NextStep WriteText(ref string text)
        {
            WriteInputText write = new WriteInputText();

            return write.WriteText(ref text);
        }

        /// <summary>
        /// Проверка совпадения символов текста
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private NextStep CheckText(string text) 
        {
            ValidateText validate = new ValidateText();

            return validate.CheckSymbols(text);
        }

        private NextStep CountWords(string text, ref int count) 
        {
            Estimation estimation = new Estimation();


            return estimation.CountWords(text, ref count);
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

            return print.PrintTextOnConsole(text, ref count);
        }

        /// <summary>
        /// Сохранение исходных данных - результатов
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private NextStep Saving(ref string path)
        {
            Save save = new Save();

            return save.Saving(ref path);
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
        private NextStep ChooseRepeatOperation()
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
        private NextStep CreateFile(string path, string text, int count, bool PrintIsWork)
        {
            MakeNewFile file = new MakeNewFile();

            return file.CreateFile(path,text,count,PrintIsWork);
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
