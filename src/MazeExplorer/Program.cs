using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using Maze;
using RESTAgent;
using RESTAgent.Maze;
using Tavis;
using Tavis.Tools;

namespace MazeExplorer {
    class Program {
        static void Main(string[] args) {

            
            var restagent = new RestAgent();
            MazeSemanticsProvider.RegisterSemantics(restagent.SemanticsRegistry);
			restagent.SetAcceptedMediaTypes( new[] { new MediaTypeWithQualityHeaderValue("application/vnd.amundsen.maze+xml") });

            var link = new Link() {Target = new Uri("http://amundsen.com/examples/mazes/2d/five-by-five/")};
            
            restagent.NavigateTo(link);

            var startlink = restagent.CurrentContent.GetLink<StartLink>();
            restagent.NavigateTo(startlink);

            // Pick first available door
            var firstlink = (from lk in restagent.CurrentContent.GetLinks() where !(lk is CurrentLink || lk is StartLink) select lk).First();
            restagent.NavigateTo((Link)firstlink);

            var linkfrom = firstlink;

            while (restagent.CurrentContent.GetLink<ExitLink>() == null) {

                Link chosenLink = null;
                var availablelinks = (from lk in restagent.CurrentContent.GetLinks() select lk).ToDictionary(lk => lk.Relation);

                switch(linkfrom.Relation) {
                    case "east":
                        chosenLink = ChooseDoor(availablelinks, "south", "east", "north", "west");
                        break;
                    case "west":
                        chosenLink = ChooseDoor(availablelinks, "north", "west", "south", "east");
                        break;
                    case "south":
                        chosenLink = ChooseDoor(availablelinks, "west", "south", "east", "north");
                        break;
                    case "north":
                        chosenLink = ChooseDoor(availablelinks, "east", "north", "west", "south");
                        break;

                }
                Console.WriteLine("Going : " + chosenLink.Relation);
                restagent.NavigateTo((Link)chosenLink);
                linkfrom = chosenLink;
            }
            Console.WriteLine("Complete"); 
            Console.ReadLine();
        }
        private static Link ChooseDoor(IDictionary<string, Link> links, string option1, string option2, string option3, string option4) {
            
            if (links.ContainsKey(option1)) return links[option1];
            if (links.ContainsKey(option2)) return links[option2];
            if (links.ContainsKey(option3)) return links[option3];
            if (links.ContainsKey(option4)) return links[option4];
            return null;
        }

    }
}
