using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KellerAutomat
{
    public class KAutomaton<S, Q, K>
    {
        public S[] Sigma { get; private set; }

        public Q[] Quants { get; private set; }
        public Q[] F { get; private set; }

        public K[] KDict { get; private set; }

        public Stack<K> Memory = new Stack<K>();
        public Q CurrentState;

        public StateMatrixK<Q, S, K> Delta { get; private set; }

        public KAutomaton(S[] sigma, Q[] quants, K[] dict, Q q0, K bottom, Q[] f)
        {
            Sigma = sigma;

            Quants = quants;
            F = f;

            KDict = dict;

            Memory.Push(bottom);
            CurrentState = q0;


            Delta = new StateMatrixK<Q, S, K>(Sigma, Quants, KDict);
        }

        public void CreateStates(Func<S, Q, K, Tuple<Q, K[]>> f)
        {
            Delta.CartesianProduct(f);
        }


        public bool Evaluate(S[] sequence, S @default)
        {
            bool result = false;
            for (int i = 0; i < sequence.Length + 1; i++)
            {
                result = false;
                S currentChar = @default;
                if (i < sequence.Length)
                    currentChar = sequence[i];
                K stack = Memory.Pop();

                var tuple = Delta[CurrentState, currentChar, stack];
                
                CurrentState = tuple.Item1;
                for (int j = 0; j < tuple.Item2.Length; j++)
                    Memory.Push(tuple.Item2[j]);


                for (int j = 0; j < F.Length; j++)
                {
                    if (CurrentState.Equals(F[j]))
                        result = true;
                }

            }
            return result;
        }



    }
}
