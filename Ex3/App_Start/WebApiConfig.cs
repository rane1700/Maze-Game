using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Ex3
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            
            // Web API routes
            config.MapHttpAttributeRoutes();
            //Route for generating the maze
            config.Routes.MapHttpRoute(
                            name: "GenerateMaze",
                            routeTemplate: "api/{controller}/{mazeName}/{MazeCols}/{mazeRows}"
                        //defaults: new { id = RouteParameter.Optional }
               );
            //Route for solving the maze
            config.Routes.MapHttpRoute(
                            name: "SolveApi",
                            routeTemplate: "api/{controller}/{mazeName}/{algorithm}"
               );
            //Route for login to website
            config.Routes.MapHttpRoute(
                            name: "LoginApi",
                            routeTemplate: "api/{controller}/{id}/{password}"
               );
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

        }
    }
}
