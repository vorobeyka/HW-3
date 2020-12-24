using System;
using System.Collections.Generic;
using System.Linq;

namespace task_4
{
    class Program
    {
        static char getClosedBracket(char c)
        {
            switch (c)
            {
                case '{': return '}';
                case '(': return ')';
                case '[': return ']';
                case '<': return '>';
                default: return '\0';
            }
        }

        static bool IsOpenedBracket(char c)
        {
            return c == '(' || c == '{' || c == '[' || c == '<';
        }

        static bool IsClosedBracket(char c)
        {
            return c == ')' || c == '}' || c == ']' || c == '>';
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Task 1.4. Pairing parentheses by Andrey Basystyi.");
            Console.WriteLine("Rules: you need to enter expression, like '(a+b) *{[s + 4]}.");
            Console.WriteLine("Program will output errors, or success pairs of brackets.");
            Console.WriteLine("Enter expression:");
            string line;
            while ((line = Console.ReadLine()) == "")
            {
                Console.WriteLine("Line is empty. Try again:");
            }
            var brackets = new Stack<char>();
            for (int i = line.Length - 1; i >= 0; i--)
            {
                char c = line[i];
                if (IsClosedBracket(c))
                {
                    brackets.Push(c);
                }
                else if (IsOpenedBracket(c))
                {
                    if (brackets.Count == 0)
                    {
                        Console.WriteLine($"Error in position '{i}' - bracket {c} does not have a closable pair.");
                        Environment.Exit(-1);
                    }
                    else if (getClosedBracket(c) != brackets.Pop())
                    {
                        Console.WriteLine($"Error in position '{i}' - bracket {c} does not have an openable pair.");
                        Environment.Exit(-1);
                    }
                }
            }
            Console.WriteLine("Expression is correct. There are no erros.");
        }
    }
}
