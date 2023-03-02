using System;
using System.IO;
using Word_counter.Enums;
using Word_counter.Enums2;

namespace New_Structure
{
    class WriteInputText
    {
        public string WriteText()
        {
            Console.WriteLine("Введите текст на английском языке");

            string text = Console.ReadLine();

            return text;

            //return NextStep.CheckText;
        }
    }
}
