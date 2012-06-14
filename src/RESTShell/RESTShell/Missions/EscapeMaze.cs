using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Maze;
using RESTAgent;
using Tavis;
using RESTAgent.Maze;

namespace RESTShell.Missions {
    public interface IMission {
        void Go(RestAgent restAgent);
    }

    public class EscapeMaze : IMission {

        public void Go(RestAgent restAgent) {

            // Find start link and go
            var startlink = restAgent.CurrentContent.GetLink<StartLink>();
            restAgent.NavigateTo(startlink);

            // Pick first available door
            var firstlink = (from lk in restAgent.CurrentContent.GetLinks() where "north|south|east|west|exit".Contains(lk.Relation) select lk).First();
            restAgent.NavigateTo((Link)firstlink);

            Link linkfrom = firstlink;

            while (restAgent.CurrentContent.GetLink<ExitLink>() == null) {

                Link chosenLink = null;
				var availablelinks = (from lk in restAgent.CurrentContent.GetLinks() where "north|south|east|west|exit".Contains(lk.Relation) select lk).ToDictionary(lk => lk.Relation);
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
                restAgent.NavigateTo((Link)chosenLink);
                linkfrom = chosenLink;
            }
            restAgent.NavigateTo(restAgent.CurrentContent.GetLink<ExitLink>());
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
