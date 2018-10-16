using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// DFS class - solves a Search able problem using a stack by DFS searcher.
    /// </summary>
    /// <typeparam name="T"> the type of the state- the element of the Search able.</typeparam>
    public class DFS<T> : StackSearcher<T>
    {
        /// <summary>
        /// The DFS class constructor, calls the StackSearcher constructor.
        /// </summary>
        public DFS() : base() {}
        /// <summary>
        /// Search for a solution for the given Search able problem.
        /// </summary>
        /// <param name="searchable">the Search able problem to solve  </param>
        /// <returns> a solution- the path of the solution, if there isn't one returns null</returns>
        public override Solution<T> Search(ISearchable<T> searchable)
        {
            State<T> goal = searchable.GetGoalState();
            State<T> current = searchable.GetInitialState();

            AddToSearcher(current);
            while (StackSize() > 0)
            {

                current = PopFromSearcher();
                if (CheckIfVisited(current))
                {
                    continue;
                }
                AddToVisited(current);

                if (current.Equals(goal))
                {
                    // found a solution
                    return BackTraceSolution(current);
                }
                //returns a list of states with n as a parent
                List<State<T>> succerssors = searchable.GetAllPossibleStates(current);
                foreach (State<T> s in succerssors)
                {
                    if (UpdateCameFrom(current, s))
                    {
                        AddToSearcher(s);
                    }
                }
            }
            //no solution was fund
            return null;  
        }

    }
}
