using System;
using System.ComponentModel.Composition;
using Tavis;

namespace RESTAgent.Maze {
	public class MazeLink : Link
	{
        //public override string Relation {
        //    get { return base.Relation; }
        //    set {
        //        base.Relation = value;
        //        Context = new Uri("#" + Relation, UriKind.Relative);
        //    }
        //}
	}


    public class EastLink : MazeLink {
        public EastLink(): base() {
            Relation = "east";
        }
    }

    public class WestLink : MazeLink {
        public WestLink() {
            Relation = "west";
        }
    }

    public class NorthLink : MazeLink {
        public NorthLink()
             {
            Relation = "north";
        }
    }

    public class SouthLink : MazeLink {
        public SouthLink() {
            Relation = "south";
        }
    }

    public class StartLink: MazeLink {
        public StartLink() {
            Relation = "start";
        }
    }

    public class CurrentLink : MazeLink {
        public CurrentLink() {
            Relation = "current";
        }
    }

    public class ExitLink : MazeLink {
        public ExitLink() {
            Relation = "exit";
        }
    }

}
