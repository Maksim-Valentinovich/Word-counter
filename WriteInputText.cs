using System;
using System.IO;
using Word_counter.Enums;
using Word_counter.Enums2;

namespace New_Structure
{
    class WriteInputText
    {
        public NextStep WriteText(ref string text)
        {
            Console.WriteLine("Введите текст на английском языке");

            text = Console.ReadLine();

            return NextStep.CheckText;
        }
    }
}
