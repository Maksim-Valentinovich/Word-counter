using System;
using System.IO;
using Word_counter.Enums;
using Word_counter.Enums2;

namespace New_Structure
{
    class ActionsFile
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
                throw new FormatException();

            if (fileInf.Exists != true)
                throw new FileNotFoundException();
            
        }

        /// <summary>
        /// Чтение файла
        /// </summary>
        /// <param name="text"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool ReadFile(ref string text, string path)
        {
            text = File.ReadAllText(path);

            return true;
        }

        /// <summary>
        /// Сохранение
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool RecordFile(string path)  
        {
            FileInfo fileSV = new FileInfo(path);

            fileSV.Create();

            return true;
        }

        /// <summary>
        /// Создание файла результатов
        /// </summary>
        /// <param name = "path" ></ param >
        /// < param name="text"></param>
        /// <param name = "count" ></ param >
        /// < param name="PrintIsWork"></param>
        /// <returns></returns>
        public void CreateFileResult(string path, string text, int count)
        {
                string createText = "Lab1.cpp \nЛабораторная работа № 1 \nИспользование языка С# для математических расчетов " +
                                        "\nВычислить количество слов в нем и распечатать эти слова (по одному в строке) " +
                                        "\nСтудент группы 001, Иванов Максим Валентинович, 2022 год" + "\nТекст: " + text + "\nКоличество слов в тексте = " + count;

                File.WriteAllText(path, createText);
        }

        /// <summary>
        /// Создание файла исходных данных
        /// </summary>
        /// <returns></returns>
        public void CreateFileInputData(string path, string text) 
        {
            File.WriteAllText(path, text);
        }
    }
}
