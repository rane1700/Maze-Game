using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Priority_Queue;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// abstract QueueSearche class - a searcher implemented by a priority queue 
    /// </summary>
    /// <typeparam name="T">the type of the state- the element of the Search able.</typeparam>
    public abstract class QueueSearcher<T> : Searcher<T>
    {
        /// <summary>
        /// Priority Queue for the states to check 
        /// </summary>
        private SimplePriorityQueue<State<T>> openList;
        /// <summary>
        /// The QueueSearcher class constructor
        /// </summary>
        public QueueSearcher() : base()
        {
            openList = new SimplePriorityQueue<State<T>>();
        }
        /// <summary>
        /// abstract method - Search for a solution for the given Search able problem.
        /// </summary>
        /// <param name="searchable">the given Search able problem</param>
        /// <returns>a solution- the path of the solution, if there isn't one returns null</returns>
        public override abstract Solution<T> Search(ISearchable<T> searchable);
        /// <summary>
        /// returns the next state to check.
        /// </summary>
        /// <returns>the state with the biggest priority</returns>
        protected override State<T> PopFromSearcher()
        {
            Evaluated();
            return openList.Dequeue();
        }
        /// <summary>
        /// adds a given state to the states to check by the searcher
        /// </summary>
        /// <param name="state">the state to check by the searcher</param>
        protected override void AddToSearcher(State<T> state)
        {
            openList.Enqueue(state, (float)state.Cost);
        }
        /// <summary>
        /// get the number of nodes in the Priority Queue
        /// </summary>
        /// <returns>number of nodes in the Priority Queue</returns>
        protected int OpenListSize()
        {
            return openList.Count;
        }
        /// <summary>
        /// checks if the queue contains the given state
        /// </summary>
        /// <param name="state">the state to check</param>
        /// <returns>true if contains otherwise false</returns>
        protected bool OpenContaines(State<T> state)
        {
            foreach (State<T> st in openList)
            {
                if (st.Equals(state))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Adjust the Priority of a given state in the Priority queue
        /// </summary>
        /// <param name="state">the state with the new Priority</param>
        protected void AdjustPriority(State<T> state)
        {
            foreach (State<T> openS in openList)
            {
                if (state.Equals(openS))
                {
                    if (openS.Cost > state.Cost)
                    {
                        openS.Cost = state.Cost;
                        openS.CameFrom = state.CameFrom;
                        openList.UpdatePriority(openS, (float)openS.Cost);
                    }
                }
            }
        }

    }
}
