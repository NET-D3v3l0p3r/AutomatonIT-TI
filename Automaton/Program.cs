﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automaton
{
    class Program
    {
        static void Main(string[] args)
        {
            Automaton<char, bool> automaton = new Automaton<char, bool>(new char[] { 'A', 'B' }, new bool[] { true, false }, true, new bool[] { true });
            automaton.CreateStates((x, y) => x == 'A' ? (y = !y) : y);

            Console.WriteLine(automaton.Evaluate(new char[] { 'A', 'B', 'B', 'A', 'A', 'A', 'A', 'A' }));
            Console.Read();
        }


    }
}
