using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// Searcher interface - represent the fields a Searcher element need to have.
    /// </summary>
    /// <typeparam name="T">the type of the state- the element of the Search able.</typeparam>
    public interface ISearcher<T>
    {
        /// <summary>
        /// Search for a solution for the given Search able problem.
        /// </summary>
        /// <param name="searchable">the Search able problem to solve</param>
        /// <returns>a solution- the path of the solution, if there isn't one returns null</returns>
        Solution<T> Search(ISearchable<T> searchable);
        /// <summary>
        /// returns the number of state Evaluated in the Search able problem by the searcher.
        /// </summary>
        /// <returns>the number of state Evaluated in the Search able problem by the searcher</returns>
        int GetNumberOfNodesEvaluated();
    }
}
