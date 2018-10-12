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
            // AUTOMATON WHICH FINDS 3 'B's IN STRING
            Automaton<char, int> b3 = new Automaton<char, int>(new char[] { 'A', 'B' }, new int[] { 0, 1, 2, 3, 4 }, 0, new int[] { 3 });
            b3.CreateStates((x, y) => x == 'B' ? (++y <= 3 ? y : 4) : y);
            // AUTOMATON WHICH FINDS 2 'A's IN STRING
            Automaton<char, int> a2 = new Automaton<char, int>(new char[] { 'A', 'B' }, new int[] { 0, 1, 2, 4 }, 0, new int[] { 2 });
            a2.CreateStates((x, y) => x == 'A' ? (++y <= 2 ? y : 4) : y);

            string input = "AABBB";

            bool a = b3.Evaluate(input.ToCharArray());
            bool b = a2.Evaluate(input.ToCharArray());

            // DEF: Qa = Current state of automaton a2
            // DEF: Qb = Current state of automaton b3
            // DEF: Fa = All accepting states of automaton a2
            // DEF: Fb = All accepting states of automaton b3
            /*
             * Qa ∈ Fa ∧ Qb ∈ Fb
             */

            Console.WriteLine(a && b); // TRUE

            b3.Reset();
            a2.Reset(); 

            input = "BAABBBBB";

            a = b3.Evaluate(input.ToCharArray());
            b = a2.Evaluate(input.ToCharArray());

            Console.WriteLine(a && b); // FALSE (6 "b"s)

            
            // DETECTS AB ABAB ABABAB

            Automaton<char, string> ab = new Automaton<char, string>(new char[] { 'A', 'B' }, new string[] { "", "A", "AB" }, "", new string[] { "AB" });
            ab.CreateStates((x, y) => (y + x).Length > 2 ? x + "" : y + x);

            input = "ABABABABABABAB";
            Console.WriteLine(ab.Evaluate(input.ToCharArray())); // TRUE

             // IS THE FIFTH LETTER AN A
            input = "BBBBAAAABBBBBBBBBB";

            Automaton<char, int> isFifthA = new Automaton<char, int>(new char[] { 'A', 'B' }, new int[] { 0, 1, 2, 3, 4, 5, 6 }, 0, new int[] { 5 });
            isFifthA.CreateStates((x, y) => (x == 'A' || x == 'B') && y + 1 < 5 ? ++y : (x == 'A' && y == 4) ? ++y : (y == 5) ? 5 : 6) ;

            Console.WriteLine(isFifthA.Evaluate(input.ToCharArray())); // TRUE
            
            
            Console.Read();

        }


    }
}
