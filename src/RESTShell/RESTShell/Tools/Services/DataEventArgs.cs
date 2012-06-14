using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RESTShell.Tools.Services {
    public class DataEventArgs<T> : EventArgs {

        public T Data { get; private set; }
        public DataEventArgs(T value) {
            Data = value;
        }
    }
}
