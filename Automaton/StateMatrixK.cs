using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KellerAutomat
{
    public class StateMatrixK<Q, S, K>
    {
        public S[] Sigma { get; private set; }
        public Q[] Quants { get; private set; }
        public K[] KDict { get; private set; }

        private readonly Dictionary<Tuple<Q, S, K>, Tuple<Q, K[]>> deltaMatrix = new Dictionary<Tuple<Q, S, K>, Tuple<Q, K[]>>();

        public StateMatrixK(S[] sigma, Q[] quants, K[] dict)
        {
            Sigma = sigma;
            Quants = quants;
            KDict = dict;
        }

        public Tuple<Q, K[]> this[Q q, S s, K k]
        {
            get { return deltaMatrix[new Tuple<Q, S, K>(q, s, k)]; }
        }

        public void CartesianProduct(Func<S, Q, K, Tuple<Q, K[]>> f)
        {
            for (int i = 0; i < Quants.Length; i++)
            {
                for (int j = 0; j < Sigma.Length; j++)
                {
                    for (int k = 0; k < KDict.Length; k++)
                    {
                        deltaMatrix.Add(new Tuple<Q, S, K>(Quants[i], Sigma[j], KDict[k]), f(Sigma[j], Quants[i], KDict[k]));
                    }
                }
            }
        }



    }
}
