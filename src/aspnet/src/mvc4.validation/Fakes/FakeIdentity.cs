using System;
using System.Security.Principal;

namespace Cobweb.Testing.Mvc.Fakes {
    public class FakeIdentity : IIdentity {
        private readonly string _name;

        public FakeIdentity(string userName) {
            _name = userName;
        }

        public string AuthenticationType {
            get { return "Fake"; }
        }

        public bool IsAuthenticated {
            get { return !String.IsNullOrEmpty(_name); }
        }

        public string Name {
            get { return _name; }
        }
    }
}
