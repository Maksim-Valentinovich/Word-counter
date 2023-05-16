using New_Structure;
using System;
using Word_counter.Enums;

namespace Word_counter
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Старт программы");
            Steps steps = new Steps();
            steps.Move(NextStep.Start);
            Console.ReadLine();
        }

    }
}
