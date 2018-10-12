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
            
            
            // KELLER-AUTOMAT
            
            char eps = '\0';
            char[] sigma = new char[] { '(', ')', eps };
            string[] q = new string[] { "yes", "no", "dead" };
            string[] k = new string[] { "q0", "("};

        

            KAutomaton<char, string, string> automaton = new KAutomaton<char, string, string>(sigma, q, k, "yes", "q0", new string[] { "yes" });
            automaton.CreateStates((_in, _q, _k) =>
            {
                switch (_q)
                {
                    case "yes":
                        // ADD TO STACK
                        if (_in.Equals('(') && _k.Equals("q0"))
                            return new Tuple<string, string[]>("no", new string[] { "q0", "(" });
                        // DEAD
                        if (_in.Equals(')') && _k.Equals("q0"))
                            return new Tuple<string, string[]>("dead", new string[] { "q0" });
                        break;
                    case "no":

                        // ADD TO STACK
                        if (_in.Equals('(') && _k.Equals("("))
                            return new Tuple<string, string[]>("no", new string[] { "(", "(" });
                        if (_in.Equals('(') && _k.Equals("q0"))
                            return new Tuple<string, string[]>("no", new string[] { "q0", "(" });

                        // CORRECT
                        if (_in.Equals(')') && _k.Equals("q0"))
                            return new Tuple<string, string[]>("yes", new string[] { "q0" });
                        if (_in.Equals(eps) && _k.Equals("q0"))
                            return new Tuple<string, string[]>("yes", new string[] { "q0" });


                        // DELETE FROM STACK
                        if (_in.Equals(')') && _k.Equals("("))
                            return new Tuple<string, string[]>("no", new string[0]);

                        break;
 
                }


                // DEAD
                return new Tuple<string, string[]>("dead", new string[] { "q0" });
            });

            Console.WriteLine(automaton.Evaluate("(())".ToCharArray(), eps)); // TRUE
            Console.WriteLine(automaton.Evaluate("((())".ToCharArray(), eps)); // false

 
            
            Console.Read();

        }


    }
}
