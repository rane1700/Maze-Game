using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// BFS class - solves a Search able problem using a priority queue BFS searcher.
    /// </summary>
    /// <typeparam name="T"> the type of the state- the element of the Search able. </typeparam>
    public class BFS<T> : QueueSearcher<T>
    {
        /// <summary>
        /// The BFS class constructor, calls the QueueSearcher constructor.
        /// </summary>
        public BFS() : base() {}
        /// <summary>
        /// Search for a solution for the given Search able problem.
        /// </summary>
        /// <param name="searchable"> the Search able problem to solve </param>
        /// <returns> a solution- the path of the solution, if there isn't one returns null</returns>
        public override Solution<T> Search(ISearchable<T> searchable)
        { 
            // Searcher's abstract method overriding
            AddToSearcher(searchable.GetInitialState()); // inherited from Searcher
            HashSet<State<T>> closed = new HashSet<State<T>>(); // all the states already checked
            while (OpenListSize() > 0)
            {
                State<T> n = PopFromSearcher();
                closed.Add(n);
                if (n.Equals(searchable.GetGoalState()))
                    return BackTraceSolution(n); // found a solution
                //returns a list of states with n as a parent
                List<State<T>> succerssors = searchable.GetAllPossibleStates(n);
                foreach (State<T> s in succerssors)
                {
                    s.Cost = n.Cost + 1; // set the priority for the next move
                    // checks if the state wasn't already marked
                    if (!closed.Contains(s) && !OpenContaines(s))
                    {
                        s.CameFrom = n;
                        AddToSearcher(s);
                    }
                    // checks if the state is still relevant
                    else
                    {
                        if (closed.Contains(s) && !OpenContaines(s))
                        {
                            foreach (State<T> closedState in closed)
                            {
                                if (closedState.Equals(s))
                                {
                                    if (closedState.Cost > s.Cost)
                                    {
                                        AddToSearcher(s);
                                    }
                                }
                            }
                        } else
                        {
                            AdjustPriority(s);
                        }
                    }
                }
            }
            //no solution was fund
            return null;
        }
    }
}
