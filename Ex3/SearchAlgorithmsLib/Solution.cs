using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// solution class - represent a solution of a problem as a list of states 
    /// </summary>
    /// <typeparam name="T">the type of the state</typeparam>
    public class Solution<T>
    {
        /// <summary>
        /// list of states that represent the path of a solution
        /// </summary>
        private List<State<T>> result;
        /// <summary>
        /// The Solution class constructor
        /// </summary>
        /// <param name="statesInput"> list of states represent the path of the solution</param>
        public Solution(List<State<T>> statesInput)
        {
            result = statesInput;
        }
        /// <summary>
        /// gets the solution
        /// </summary>
        /// <returns>the solution - list of states</returns>
        public List<State<T>> GetStates()
        {
            return result;
        }

    }
}
