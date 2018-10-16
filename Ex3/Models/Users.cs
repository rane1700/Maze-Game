using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Ex3.Models
{
    /// <summary>
    /// data base attributes
    /// </summary>
    public class Users
    {
        [Key]
        //Name of user
        public string Name { get; set; }
        //Email address of the user
        [Required]
        public string Email { get; set; }
        //Password of the user
        [Required]
        public string Password { get; set; }
        //Number of wins of the user
        public int Wins { get; set; }
        //Number of losses of the user
        public int Losses { get; set; }
    }
}