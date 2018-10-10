using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automaton
{
    public class Automaton<T, V>
    {
        private readonly StateMatrix<T, V> internalMatrix;

        public V[] F { get; private set; }

        public V Q0 { get; private set; }
        public V CurrentState { get; private set; }

        public Automaton(T[] sigma, V[] q, V q0, V[] f)
        {
            internalMatrix = new StateMatrix<T, V>(sigma, q);
            F = f;

            Q0 = q0;
            CurrentState = Q0;
        }

        public void CreateStates(Func<T, V, V> mapper)
        {
            internalMatrix.CartesianProductSxQ(mapper);
        }

        public bool Evaluate(T[] sequence)
        {
            bool isAccepting = false;
            for (int i = 0; i < sequence.Length; i++)
            {
                isAccepting = false;
                CurrentState = internalMatrix[sequence[i], CurrentState];
                for (int j = 0; j < F.Length; j++)
                {
                    if (CurrentState.Equals(F[j]))
                        isAccepting = true;
                }
            }

            // RETURN WHETHER AUTOMATON IS IN AN ACCEPTING STATE (IF TRUE then CurrentState ∈ F)
            return isAccepting;
        }
        
        public void Reset()
        {
            CurrentState = Q0;
        }
        
    }
}
