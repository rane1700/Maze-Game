using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// Search able interface - represent the fields a Search able object need to have.
    /// </summary>
    /// <typeparam name="T"> the type of the state in the Search able </typeparam>
    public interface ISearchable <T>
    {
        /// <summary>
        /// gets the initial state of the Search able object
        /// </summary>
        /// <returns> the initial state of the Search able object </returns>
        State<T> GetInitialState();
        /// <summary>
        /// gets the goal state of the Search able object
        /// </summary>
        /// <returns>the goal state of the Search able object</returns>
        State<T> GetGoalState();
        /// <summary>
        /// gets all the possible states for the next move, from a cornet state
        /// </summary>
        /// <param name="s"> the cornet state from were to get the next move </param>
        /// <returns> a list of the possible states for the next move </returns>
        List<State<T>> GetAllPossibleStates(State<T> s);
    }
}
