using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// state class - represent a node of a search able object.
    /// </summary>
    /// <typeparam name="T">the type of the state- the element of the Search able.</typeparam>
    public class State<T>
    {
        /// <summary>
        /// state - the element of the Search able.
        /// </summary>
        private T state;
        /// <summary>
        /// cost property - the cost to reach this state
        /// </summary>
        public double Cost {
            get; set;
        }
        /// <summary>
        /// privet copy constructor for the state class
        /// </summary>
        /// <param name="state">the state to copy</param>
        private State(T state)
        {
            this.state = state;
        }
        /// <summary>
        /// returns the state value
        /// </summary>
        /// <returns>returns the state value</returns>
        public T GetState()
        {
            return state;
        }
        /// <summary>
        /// checks if the cornet state is Equal to a given state
        /// </summary>
        /// <param name="s">the state to compare to</param>
        /// <returns>true if equals, otherwise false</returns>
        public bool Equals(State<T> s) 
        {
            return state.Equals(s.state);
        }
        /// <summary>
        /// CameFrom property - the state witch we came from to the cornet state
        /// </summary>
        public State<T> CameFrom { get; set; }
        /// <summary>
        /// override the object equals function
        /// </summary>
        /// <param name="obj">the state to compare to</param>
        /// <returns>true if equals, otherwise false</returns>
        public override bool Equals(object obj)
        {
            return obj != null && state.Equals((obj as State<T>).state);
        }
        /// <summary>
        /// override the GetHashCode function
        /// </summary>
        /// <returns>the Hash Code for the state</returns>
        public override int GetHashCode()
        {
            return state.GetHashCode();
        }
        /// <summary>
        /// override the ToString function
        /// </summary>
        /// <returns>a string representation for the state</returns>
        public override String ToString()
        {
            return state.ToString();
        }
        /// <summary>
        /// static StatePool class
        /// </summary>
        /// <typeparam name="T">the type of the state</typeparam>
        public static class StatePool<T>
        {
            /// <summary>
            /// a Dictionary contains all the states  
            /// </summary>
            private static Dictionary<string, State<T>> pool = new Dictionary<string, State<T>>();
            /// <summary>
            /// returns an instant of the wonted state
            /// </summary>
            /// <param name="state">the wonted state</param>
            /// <returns>a state with same given state otherwise create one</returns>
            public static State<T> GetStateInstant(T state)
            {
                if (pool.ContainsKey(state.ToString()))
                {
                    return pool[state.ToString()];
                }
                
                State<T> x = new State<T>(state);
                pool.Add(state.ToString(), x);
                return x;
            }
        }
    }
}
