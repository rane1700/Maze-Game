using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Models;
using MazeGeneratorLib;
using MazeLib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SearchAlgorithmsLib;
namespace Ex3.Controllers
{
    public class MazeController : ApiController
    {
        //static maze model to process controller requests.
        private static IModel mazeModel = new MazeModel();
        //maze generator to generate mazes
        private DFSMazeGenerator mazeGen = new DFSMazeGenerator();
        // GET: api/Generate
        /// <summary>
        /// Method to generate and retrieve maze according to given parameters
        /// </summary>
        /// <param name="mazeName">name of maze</param>
        /// <param name="mazeCols">maze columns</param>
        /// <param name="mazeRows">maze rows</param>
        /// <returns>Json object of the retrieved maze</returns>
        [HttpGet()]
        public JObject GetMazeParameters(string mazeName, int mazeCols, int mazeRows)
        {
            JObject obj = new JObject();
            Maze maze = mazeGen.Generate(mazeRows, mazeCols);
            maze.Name = mazeName;
            mazeModel.AddMaze(mazeName, maze);
            obj = JObject.Parse(maze.ToJSON());
            return obj;

        }
        /// <summary>
        /// Method to retrieve maze solution
        /// </summary>
        /// <param name="mazeName">name of the maze</param>
        /// <param name="algorithm">algorithm to solve maze by</param>
        /// <returns>Json object of the solution</returns>
        [HttpGet()]
        public JObject GetMazeSolution(string mazeName, string algorithm)
        {
            ISearcher<Position> dfsSearcher = new DFS<Position>();
            ISearcher<MazeLib.Position> bfsSearcher = new BFS<MazeLib.Position>();
            string solution;
            JObject obj = null;
            if (algorithm.Equals("BFS"))
            {
                solution = mazeModel.Solve(mazeName, bfsSearcher);
                obj = JObject.Parse(solution);
            }
            else if (algorithm.Equals("DFS"))
            {
                solution = mazeModel.Solve(mazeName, dfsSearcher);
                obj = JObject.Parse(solution);
            }

            return obj;

        }
    }
}
