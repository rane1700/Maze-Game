using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Collections.Concurrent;
using MazeLib;
using Models;
using MazeGeneratorLib;
using Newtonsoft.Json.Linq;

namespace Ex3
{
    public class MazeHub : Hub
    {
        //dictionary connecting maze name to list of clients.
        private static ConcurrentDictionary<string, List<string>> connectedUsers =
             new ConcurrentDictionary<string, List<string>>();
        //model to process hub request.
        private static IModel mazeModel = new MazeModel();
        private DFSMazeGenerator mazeGen = new DFSMazeGenerator();
        //Dictionary connecting between client to a maze name.
        private static Dictionary<string, string> clientToGame =
            new Dictionary<string, string>();
        /// <summary>
        /// Method to start the multi-player game
        /// </summary>
        /// <param name="mazeName">name of maze</param>
        /// <param name="row">maze rows</param>
        /// <param name="col">maze columns</param>
        public void StartGame(string mazeName, int row, int col)
        {
            connectedUsers[mazeName] = new List<string>();
            connectedUsers[mazeName].Add(Context.ConnectionId);
            Maze maze = mazeGen.Generate(row, col);
            mazeModel.AddToMultiPlayerGame(mazeName, maze);
            clientToGame.Add(Context.ConnectionId, mazeName);
        }
        /// <summary>
        /// Method to take care of client joining an existing game
        /// </summary>
        /// <param name="mazeName">name of maze to join</param>
        public void JoinGame(string mazeName)
        {
            connectedUsers[mazeName].Add(Context.ConnectionId);
            clientToGame.Add(Context.ConnectionId, mazeName);
            JObject obj = mazeModel.Join(mazeName);
            Clients.Client(connectedUsers[mazeName][0]).drowoncanvas(obj);
            Clients.Client(connectedUsers[mazeName][1]).drowoncanvas(obj);
        }
        /// <summary>
        /// Method to perform list command, showing available games to join
        /// </summary>
        public void GameToJoin()
        {
            JArray arr = JArray.Parse(mazeModel.GetAllPossibleGames());
            Clients.Client(Context.ConnectionId).getListGame(arr); 
        }
        /// <summary>
        /// Method to move player in both canvases
        /// </summary>
        /// <param name="move">the moving direction</param>
        public void PlayMove(int move)
        {
            string mazeN = clientToGame[Context.ConnectionId];
            string player1 = connectedUsers[mazeN][0];
            string player2 = connectedUsers[mazeN][1];

            if (Context.ConnectionId == player1)
            {
                Clients.Client(player2).opponentMove(move);
                Clients.Client(player1).myMove(move);
            }
            else
            {
                Clients.Client(player1).opponentMove(move);
                Clients.Client(player2).myMove(move);
            }
        }
        public void NotifyWinner()
        {
            string mazeN = clientToGame[Context.ConnectionId];
            string player1 = connectedUsers[mazeN][0];
            string player2 = connectedUsers[mazeN][1];

            if (Context.ConnectionId == player1)
            {
                Clients.Client(player2).opponentLoss();
            }
            else
            {
                Clients.Client(player1).opponentLoss();
            }
        }
    }
}