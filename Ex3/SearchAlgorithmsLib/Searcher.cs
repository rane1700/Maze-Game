using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Priority_Queue;
namespace SearchAlgorithmsLib
{
    /// <summary>
    /// abstract class Searcher - represent the fields a Searcher element need to have.
    /// </summary>
    /// <typeparam name="T">the type of the state- the element of the Search able.</typeparam>
    public abstract class Searcher<T> : ISearcher<T>
    {
        /// <summary>
        ///  the Number Of Nodes Evaluated in the search
        /// </summary>
        private int evaluatedNodes;
        /// <summary>
        /// The Searcher class constructor
        /// </summary>
        public Searcher()
        {
            evaluatedNodes = 0;
        }
        /// <summary>
        /// Gets the Number Of Nodes Evaluated by the search
        /// </summary>
        /// <returns>the Number Of Nodes Evaluated by the search</returns>
        public int GetNumberOfNodesEvaluated()
        {
            return evaluatedNodes;
        }
        /// <summary>
        /// abstract method - Search for a solution for the given Search able problem.
        /// </summary>
        /// <param name="searchable"> the given Search able problem.</param>
        /// <returns>a solution- the path of the solution, if there isn't one returns null</returns>
        public abstract Solution<T> Search(ISearchable<T> searchable);
        /// <summary>
        /// abstract method - returns the next state to check. 
        /// </summary>
        /// <returns> the next state to check.</returns>
        protected abstract State<T> PopFromSearcher();
        /// <summary>
        /// abstract method - adds a given state to the states to check by the searcher
        /// </summary>
        /// <param name="state">the state to check by the searcher</param>
        protected abstract void AddToSearcher(State<T> state);
        /// <summary>
        /// increase the number of evaluated Nodes by one.
        /// </summary>
        protected void Evaluated()
        {
            evaluatedNodes++;
        }
        /// <summary>
        /// Back Trace the states to get the solution upright.
        /// </summary>
        /// <param name="state">the end goal to Back Trace from </param>
        /// <returns> a solution after Back Tracing so it will be upright </returns>
        protected Solution<T> BackTraceSolution(State<T> state)
        {
            List<State<T>> nodesList = new List<State<T>>();
            State<T> nodeToAdd = state;
            do
            {
                nodesList.Add(nodeToAdd);
                nodeToAdd = nodeToAdd.CameFrom;
            } while (nodeToAdd != null);
            Solution<T> solution = new Solution<T>(nodesList);
            return solution;
        }
    }
}
