using System;
using System.Collections.Generic;
using SearchAlgorithmsLib;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Sockets;
using Newtonsoft.Json;
using MazeGeneratorLib;
using MazeLib;
using System.Configuration;
using System.Threading;

namespace Models
{
    /// <summary>
    /// Class represents model of the project in form of a Maze object.
    /// </summary>
    public class MazeModel : IModel
    {
        /// <summary>
        /// object generating the maze.
        /// </summary>
        private DFSMazeGenerator mazeGen;
        /// <summary>
        /// Dictionary connecting between maze name and it's solution.
        /// </summary>
        private Dictionary<string, JObject> solutionDict;
        /// <summary>
        /// Dictionary connecting between maze name and maze object.
        /// </summary>
        private Dictionary<string, Maze> mazeDictionary;
        /// <summary>
        /// Array containing all available multi-player games
        /// </summary>
        private List<string> availableGamesToJoin;
        /// <summary>
        /// Dictionary connecting between maze name and list of clients connecting to
        /// that game.
        /// </summary>
        private Dictionary<string, List<TcpClient>> activeMultiplayerGames;
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="ctrlInput">Controller object</param>
        public MazeModel()
        {
            solutionDict = new Dictionary<string, JObject>();
            mazeDictionary = new Dictionary<string, MazeLib.Maze>();
            availableGamesToJoin = new List<string>();
            mazeGen = new DFSMazeGenerator();
            activeMultiplayerGames = new Dictionary<string, List<TcpClient>>();
        }
        /// <summary>
        /// Method to give a maze solution according to the solve command from the client.
        /// </summary>
        /// <param name="name">name of the maze</param>
        /// <param name="algorithm">type of searcher for the maze(BFS or DFS)</param>
        /// <returns>string representing maze solution</returns>
        public string Solve(string name, ISearcher<Position> algorithm)
        {
            JObject sol = new JObject();
            //Check if maze was already solved previously.
            if (solutionDict.ContainsKey(name))
            {
                return solutionDict[name].ToString();
            }
            sol.Add("Name", name);
            MazeAdapter mazeAdapter = new MazeAdapter(mazeDictionary[name]);
            Solution<Position> stateSol = algorithm.Search(mazeAdapter);
            mazeAdapter.UpdateFinalState(stateSol.GetStates());
            sol.Add("Solution",
             JsonConvert.SerializeObject(mazeAdapter.GetSolution()));
            sol.Add("NodesEvaluated", algorithm.GetNumberOfNodesEvaluated());
            //remove maze from unsolved mazes list.
            mazeDictionary.Remove(name);
            solutionDict.Add(name, sol);
            return solutionDict[name].ToString();
        }
        /// <summary>
        /// Method to generate a maze according to the generate command.
        /// </summary>
        /// <param name="name">name of the maze</param>
        /// <param name="row">number of rows in maze</param>
        /// <param name="col">number of columns in maze</param>
        /// <returns>Returns a maze object</returns>
        public void AddMaze(string name, Maze maze)
        {
            if (!mazeDictionary.ContainsKey(name))
            {
                mazeDictionary.Add(name, maze);
            }
        }
        /// <summary>
        /// Method to add a client to a specific game
        /// </summary>
        /// <param name="nameOfGame">name of maze</param>
        /// <param name="client">client to be added</param>
        public void AddToMultiPlayerGame(string name, Maze maze)
        {
            if (!mazeDictionary.ContainsValue(maze))
            {
                mazeDictionary.Add(name, maze);
                availableGamesToJoin.Add(name);
            }
        }
        /// <summary>
        /// Method to Join a game according to the Join command.
        /// </summary>
        /// <param name="nameOfGame">name of maze</param>
        /// <param name="client">client who wants to join game</param>
        /// <returns>Result object with relevant information</returns>
        public JObject Join(string nameOfGame)
        {
            //availableGamesToJoin[nameOfGame].Remove();
            availableGamesToJoin.Remove(nameOfGame);
            //activeMultiplayerGames[nameOfGame].Add(client);
            //Result result = new Result(false);
            //result.SetMultiPlayerClients(activeMultiplayerGames[nameOfGame]);
            JObject obj = JObject.Parse(mazeDictionary[nameOfGame].ToJSON());
            return obj;
        }
        /// <summary>
        /// Method to return all available multi-player games according to the List command
        /// </summary>
        /// <returns>Available multi-player games</returns>
        public string GetAllPossibleGames()
        {
            return JsonConvert.SerializeObject(availableGamesToJoin);
        }
        /// <summary>
        /// Method to update relevant clients according to the Play command
        /// </summary>
        /// <param name="play">type of play</param>
        /// <param name="client">client who performed the play</param>
        /// <returns>Result object with relevant information</returns>
        public Result PlayGame(string play)
        {
            JObject sol = new JObject();
            //sol.Add("Name:", nameOfMaze);
            sol.Add("Direction:", play);
            Result result = new Result(false);
            //List<TcpClient> clientsToNotify = activeMultiplayerGames[nameOfMaze];
            //result.SetMultiPlayerClients(clientsToNotify);
            result.SetStringResult(sol.ToString());
            return result;
        }
        /// <summary>
        /// Method to close relevant clients connection to a game according to the Close command.
        /// </summary>
        /// <param name="nameOfGame">name of game to close</param>
        /// <returns>List of all clients who played this game and their connection
       ///  needs to be closed</returns>
        public List<TcpClient> CloseMultiPlayerGame(string nameOfGame)
        {
            List<TcpClient> clientsToNotify = activeMultiplayerGames[nameOfGame];
            mazeDictionary.Remove(nameOfGame);
            activeMultiplayerGames.Remove(nameOfGame);
            foreach (TcpClient x in clientsToNotify)
            {
                //clientToGame.Remove(x);
            }
            return clientsToNotify;
        }

    }
}
