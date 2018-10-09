using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automaton
{
    public class StateMatrix<T, V>
    {
        public V[] Q { get; private set; }
        public T[] Sigma { get; private set; }
   
        private readonly Dictionary<Tuple<V, T>, V> tupleMap = new Dictionary<Tuple<V, T>, V>();

        public V this[T a, V b]
        {
            get { return tupleMap[new Tuple<V, T>(b, a)]; }
        }


        public StateMatrix(T[] sigma, V[] q)
        {
            Sigma = sigma;
            Q = q;
        }

        public void CartesianProductSxQ(Func<T, V, V> mapper)
        {
            for (int i = 0; i < Q.Length; i++)
            {
                for (int j = 0; j < Sigma.Length; j++)
                    tupleMap.Add(new Tuple<V, T>(Q[i], Sigma[j]), mapper(Sigma[j], Q[i]));
            }
        }
    }
}
