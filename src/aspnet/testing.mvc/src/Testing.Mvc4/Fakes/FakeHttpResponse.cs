using System.Collections.Specialized;
using System.Web;

namespace Cobweb.Testing.Mvc.Fakes {
    public class FakeHttpResponse : FakeHttpResponseBase {
        private HttpCookieCollection _cookies;
        private NameValueCollection _headers;

        public new virtual HttpCookieCollection Cookies {
            get { return _cookies ?? (_cookies = new HttpCookieCollection()); }
            set { _cookies = value; }
        }

        protected internal override sealed HttpCookieCollection InternalCookies {
            get { return _cookies ?? (_cookies = new HttpCookieCollection()); }
        }

        public new virtual NameValueCollection Headers {
            get { return _headers ?? (_headers = new NameValueCollection()); }
            set { _headers = value; }
        }

        protected internal override sealed NameValueCollection InternalHeaders {
            get { return _headers ?? (_headers = new NameValueCollection()); }
        }

        public override string ApplyAppPathModifier(string virtualPath) {
            return virtualPath;
        }
    }
}
