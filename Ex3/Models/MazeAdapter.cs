using System;
using SearchAlgorithmsLib;
using System.Collections.Generic;
using MazeLib;
/// <summary>
/// Class which adapts maze so it can be search able.
/// </summary>
public class MazeAdapter : ISolution<Direction, Position>, ISearchable<Position>

{
    /// <summary>
    /// maze object.
    /// </summary>
    private Maze maze;
    /// <summary>
    /// List of all final positions of the maze after he was searched for solution.
    /// </summary>
    private List<State<Position>> finalPositionList;
    /// <summary>
    /// List of the direction representing the solution of the maze.
    /// </summary>
    private List<Direction> directions;
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="inputMaze">Maze object</param>
	public MazeAdapter(Maze inputMaze)
	{
        maze = inputMaze; 
        directions = new List<Direction>();
	}
    /// <summary>
    /// Method returns maze's initial position before any advancement is made.
    /// </summary>
    /// <returns>First state of the maze</returns>
    public State<Position> GetInitialState()
    {
         return State<Position>.StatePool<Position>.GetStateInstant(maze.InitialPos);
    }
    /// <summary>
    /// Method returning the exit position which is actually the end point of the solution.
    /// </summary>
    /// <returns>End point of maze solution</returns>
    public State<Position> GetGoalState()
    {
        return State<Position>.StatePool<Position>.GetStateInstant(maze.GoalPos);
    }
    /// <summary>
    /// Method which returns all possible adjacent states from a given state
    /// </summary>
    /// <param name="s">current state</param>
    /// <returns>List of proper adjacent state</returns>
    public List<State<Position>> GetAllPossibleStates(State<Position> s)
    {
        //List of adjacent vertices's.
        List<State<Position>> neighbors  = new List<State<Position>>(); 
        int x = s.GetState().Row;
        int y = s.GetState().Col;
        State <Position> cur;
        //Check if right vertex is available
        if (x+1 < maze.Rows && maze[x + 1,y]==CellType.Free)
        {
            cur = State<Position>.StatePool<Position>.GetStateInstant(new Position(x + 1, y));
            neighbors.Add(cur);
        }
        //Check if left vertex is available
        if ( x-1 >= 0 && maze[x-1,y] == CellType.Free)
        {
            cur = State<Position>.StatePool<Position>.GetStateInstant(new Position(x - 1, y));
            neighbors.Add(cur);
        }
        //Check if upper vertex is available
        if (y+1 < maze.Cols && maze[x,y+1] == CellType.Free)
        {
            cur = State<Position>.StatePool<Position>.GetStateInstant(new Position(x, y + 1));
            neighbors.Add(cur);
        }
        //Check if lower vertex is available
        if (y - 1 >= 0 && maze[x,y-1] == CellType.Free)
        {
            cur = State<Position>.StatePool<Position>.GetStateInstant(new Position(x, y - 1));
            neighbors.Add(cur);
        }
        return neighbors;
    }
    /// <summary>
    ///Updating the list of all final positions of the maze after he was searched for solution.
    /// </summary>
    /// <param name="finalState">Final states list</param>
    public void UpdateFinalState(List<State<Position>> finalState)
    {
        finalPositionList = finalState;
    }
    /// <summary>
    /// Method which converts final maze state objects into direction objects for the complete solution. 
    /// </summary>
    /// <returns>List of direction representing the solution</returns>
    public List<Direction> GetSolution()
    {
        //List of direction representing the solution.
        List<Direction> directions = new List<Direction>();
        //back tracing the position to the initial state position.

        //State<Position> prev = finalPositionList[0].CameFrom;
        finalPositionList.Reverse();
         State < Position > prev = finalPositionList[0];
        //updating directions list.
        for (int i=1;i<finalPositionList.Count;i++)
        {
            if (finalPositionList[i].GetState().Row +1 == prev.GetState().Row)
            {
                directions.Add(Direction.Up);
            }
            else if (finalPositionList[i].GetState().Row - 1 == prev.GetState().Row)
            {
                directions.Add(Direction.Down);
            } else if (finalPositionList[i].GetState().Col + 1 == prev.GetState().Col)
            {
                directions.Add(Direction.Left);
            } else
            {
                directions.Add(Direction.Right);
            }
            prev = finalPositionList[i];
        }
        return directions;
   }
}
