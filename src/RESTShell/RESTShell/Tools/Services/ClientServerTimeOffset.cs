using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RESTShell.Tools.Services {
    public class ClientServerTimeOffset {

        private ClientServerTimeOffset() {

        }

        private static ClientServerTimeOffset _Instance;
        public static ClientServerTimeOffset Instance {
            get {
                if (_Instance == null) {
                    _Instance = new ClientServerTimeOffset();
                }
                return _Instance;
            }
        }

        private Double _Offset = 0;
        public Double OffsetInSeconds {
            get {
                return _Offset;
            }
            set {
                _Offset = value;
            }
        }
    }
}
