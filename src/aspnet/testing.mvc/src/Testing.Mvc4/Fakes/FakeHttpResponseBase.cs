using System.Collections.Specialized;
using System.Web;

namespace Cobweb.Testing.Mvc.Fakes {
    public abstract class FakeHttpResponseBase : HttpResponseBase {
        protected internal FakeHttpResponseBase() {}

        public override sealed HttpCookieCollection Cookies {
            get { return InternalCookies; }
        }

        protected internal abstract HttpCookieCollection InternalCookies { get; }

        public override sealed NameValueCollection Headers {
            get { return InternalHeaders; }
        }

        protected internal abstract NameValueCollection InternalHeaders { get; }
    }
}
