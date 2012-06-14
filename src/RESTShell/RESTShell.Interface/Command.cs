using System;

namespace RESTShell.Interface {

    public enum CommandStatus {
        Unavailable = 1,
        Enabled = 2
    }

    public class Command {

        public EventHandler OnStatusChange;
        private string _Name;
        private Action<Command> _Handler;
        private CommandStatus _Status;

        public Command(string name) {
            _Name = name;
        }

        public Command(string name, Action<Command> handler) {
            if (handler == null) throw new ArgumentNullException("Handler is required for the command to do anything");
            _Name = name;
            _Handler = handler;
        }

        public CommandStatus Status {
            get { return _Status; }
            set { _Status = value;
                if (OnStatusChange != null) OnStatusChange(this, EventArgs.Empty);
            }
        }

        public string Name {
            get { return _Name; }
        }

        public Action<Command> Handler {
            get { return _Handler; }
            set { _Handler = value; }
        }

        public void Execute() {
            _Handler(this);    
        }

        
        public void ClickHandler(object sender, EventArgs args) {
            Execute();
        }
    }
}
