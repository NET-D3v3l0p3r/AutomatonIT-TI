using System;
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
            Automaton<int, int> a = new Automaton<int, int>(new int[] { 50, 100, 200 }, new int[] { 0, 50, 100, 150, 200 }, 0, new int[] { 200 });
            a.CreateStates((x, y) => (x + y) >= 200 ? 200 : (x + y));

            Console.WriteLine(a.Evaluate(new int[] { 200, 50, 100 }));


            Automaton<char, bool> automaton = new Automaton<char, bool>(new char[] { 'A', 'B' }, new bool[] { true, false }, true, new bool[] { true });
            automaton.CreateStates((x, y) => x == 'A' ? (y = !y) : y);

            Console.WriteLine(automaton.Evaluate(new char[] { 'A', 'B', 'B', 'A', 'A', 'A', 'A', 'A' }));
            Console.Read();
        }


    }
}
