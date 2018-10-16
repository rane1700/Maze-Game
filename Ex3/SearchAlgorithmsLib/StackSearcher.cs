using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// abstract StackSearcher class - a searcher implemented by a stack.
    /// </summary>
    /// <typeparam name="T">the type of the state- the element of the Search able.</typeparam>
    public abstract class StackSearcher <T> : Searcher<T>
    {
        /// <summary>
        /// stack for the states to check 
        /// </summary>
        private Stack<State<T>>  stack;
        /// <summary>
        /// a list contains the states that were checked
        /// </summary>
        private List<State<T>> visited;
        /// <summary>
        /// the StackSearcher class constructor
        /// </summary>
        public StackSearcher()
        {
            stack = new Stack<State<T>>();
            visited = new List<State<T>>();
        }
        /// <summary>
        /// abstract method - Search for a solution for the given Search able problem.
        /// </summary>
        /// <param name="searchable">the given Search able problem</param>
        /// <returns>a solution- the path of the solution, if there isn't one returns null</returns>
        public override abstract Solution<T> Search(ISearchable<T> searchable);
        /// <summary>
        /// adds a given state to the states to check by the searcher
        /// </summary>
        /// <param name="state">the state to check by the searcher</param>
        protected override void AddToSearcher(State<T> state) 
        {
            stack.Push(state);
            
        }
        /// <summary>
        /// returns the next state to check.
        /// </summary>
        /// <returns> the state in the top of the stack</returns>
        protected override State<T> PopFromSearcher()
        {
            Evaluated();
            return stack.Pop();
            
        }
        /// <summary>
        /// get the number of nodes in the stack
        /// </summary>
        /// <returns>the number of nodes in the stack</returns>
        protected int StackSize()
        {
            return stack.Count;
        }
        /// <summary>
        /// changes the property CameFrom of a given state
        /// </summary>
        /// <param name="prev">the new CameFrom property for the current state</param>
        /// <param name="cur">the state to change the CameFrom property</param>
        /// <returns>true if the state was checked and change his CameFrom property otherwise false</returns>
        protected bool UpdateCameFrom(State<T> prev,State<T> cur)
        {
            if (visited.Contains(cur))
            {
                return false;
            } else
            {
                cur.CameFrom = prev;
                return true;
            }
        }
        /// <summary>
        /// adds a given state to the checked state list
        /// </summary>
        /// <param name="state">the state to add</param>
        protected void AddToVisited(State<T> state) {
            visited.Add(state);
        }
        /// <summary>
        /// checks if the given state was already check by the searcher
        /// </summary>
        /// <param name="state">the state to check</param>
        /// <returns>true if the state was checked otherwise false</returns>
        protected bool CheckIfVisited(State<T> state)
        {
            foreach (State <T> s in visited)
            {
                if (state.Equals(s)) 
                {
                    return true;
                }
            }
            return false;
        }

    }
}
