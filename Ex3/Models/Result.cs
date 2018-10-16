using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [System.Serializable]
    /// <summary>
    /// Class which contains all appropriate result attributes.
    /// </summary>
    public class Result
    {
        /// <summary>
        /// result to a given client command in a form of a string
        /// </summary>
        private string stringResult;
        /// <summary>
        /// symbolizes if connection between client and server should be closed.
        /// </summary>
        private bool closeConnection;
        /// <summary>
        /// List of all clients playing the same game.
        /// </summary>
        private List<TcpClient> multiplayerClients;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="closeConnection">variable - should the connection with server be closed</param>
        public Result(bool closeConnection)
        {
            this.closeConnection = closeConnection;
            multiplayerClients = null;
        }
        /// <summary>
        /// Setting the result string
        /// </summary>
        /// <param name="stringResult">result string</param>
        public void SetStringResult(string stringResult)
        {
            this.stringResult = stringResult;
        }
        /// <summary>
        /// setting the wanted connection status
        /// </summary>
        /// <param name="closeConnection">connection status</param>
        public void SetCloseConnection(bool closeConnection)
        {
            this.closeConnection = closeConnection;
        }
        /// <summary>
        /// get result in form of a string
        /// </summary>
        /// <returns>result for a command</returns>
        public string GetStringResult()
        {
            return stringResult;
        }
        /// <summary>
        /// get the current connection status
        /// </summary>
        /// <returns>connection status</returns>
        public bool GetCloseConnection()
        {
            return closeConnection;
        }
        /// <summary>
        /// Updating the list of multi-player clients
        /// </summary>
        /// <param name="clientsInput">multi-player client list</param>
        public void SetMultiPlayerClients(List<TcpClient> clientsInput)
        {
            multiplayerClients = clientsInput;
        }
        /// <summary>
        /// return a list of multi-player clients playing the same game.
        /// </summary>
        /// <returns>clients playing the same game</returns>
        public List<TcpClient> GetMultiPlayerClients()
        {
            return multiplayerClients;
        }
        public void EmptyList()
        {
            multiplayerClients = null;
        }
    }
}
