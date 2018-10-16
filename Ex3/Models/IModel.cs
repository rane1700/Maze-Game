using MazeLib;
using Newtonsoft.Json.Linq;
using SearchAlgorithmsLib;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Models
{
    /// <summary>
    /// Interface for model class which defines common ground for maze problems.
    /// </summary>
    interface IModel
    {
        /// <summary>
        /// Method to give a maze solution according to the solve command from the client.
        /// </summary>
        /// <param name="name">name of the maze</param>
        /// <param name="algorithm">type of searcher for the maze(BFS or DFS)</param>
        /// <returns>string representing maze solution</returns>
        string Solve(string name, ISearcher<Position> algorithm);
        /// <summary>
        /// Method to generate a maze according to the generate command.
        /// </summary>
        /// <param name="name">name of the maze</param>
        /// <param name="row">number of rows in maze</param>
        /// <param name="col">number of columns in maze</param>
        /// <returns>Returns a maze object</returns>
        void AddMaze(string name, Maze maze);
        /// <summary>
        /// Method to add a client to a specific game
        /// </summary>
        /// <param name="nameOfGame">name of maze</param>
        /// <param name="client">client to be added</param>
         void AddToMultiPlayerGame(string name, Maze maze);
        /// <summary>
        /// Method to Join a game according to the Join command.
        /// </summary>
        /// <param name="nameOfGame">name of maze</param>
        /// <param name="client">client who wants to join game</param>
        /// <returns>Result object with relevant information</returns>
        JObject Join(string nameOfGame);
        /// <summary>
        /// Method to return all available multi-player games according to the List command
        /// </summary>
        /// <returns>Available multi-player games</returns>
        string GetAllPossibleGames();
        /// <summary>
        /// Method to update relevant clients according to the Play command
        /// </summary>
        /// <param name="play">type of play</param>
        /// <param name="client">client who performed the play</param>
        /// <returns>Result object with relevant information</returns>
        Models.Result PlayGame(string play);
        /// <summary>
        /// Method to close relevant clients connection to a game according to the Close command.
        /// </summary>
        /// <param name="nameOfGame">name of game to close</param>
        /// <returns>List of all clients who played this game and their connection
        ///  needs to be closed</returns>
        List<TcpClient> CloseMultiPlayerGame(string nameOfGame);
    }
}
