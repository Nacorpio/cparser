using System;
using System.IO;
using TokenInterpreter.Classes;

namespace TokenInterpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            var tr = new TokenReader(new StringReader(File.ReadAllText(@"D:\Programming\CodeBlocks\C\Example01\main.c")));
            tr.ReadAll();

            var interpreter = new Classes.Interpreter(tr.Tokens);
            var tree = interpreter.Build();

            Console.ReadLine();
        }
    }
}
