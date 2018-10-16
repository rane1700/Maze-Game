using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// Solution interface - represent the fields a solvable object need to have.
    /// </summary>
    /// <typeparam name="T"> the type of the solution wanted </typeparam>
    /// <typeparam name="W">the type of state in the Search able object </typeparam>
    public interface ISolution<T,W>
    {
        /// <summary>
        /// get a solution for the problem.
        /// </summary>
        /// <returns> a list of a given type represents the solution</returns>
        List<T> GetSolution();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fState"></param>
        void UpdateFinalState(List<State<W>> fState);
    }
}
