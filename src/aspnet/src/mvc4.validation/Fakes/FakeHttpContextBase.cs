using System.Collections;
using System.Web;

namespace Cobweb.Testing.Mvc.Fakes {
    public abstract class FakeHttpContextBase : HttpContextBase {
        protected internal FakeHttpContextBase() {}

        public override sealed IDictionary Items {
            get { return InternalItems; }
        }

        protected internal abstract IDictionary InternalItems { get; }

        public override HttpRequestBase Request {
            get { return InternalRequest; }
        }

        protected internal abstract HttpRequestBase InternalRequest { get; }

        public override HttpResponseBase Response {
            get { return InternalResponse; }
        }

        protected internal abstract HttpResponseBase InternalResponse { get; }

        public override HttpSessionStateBase Session {
            get { return InternalSession; }
        }

        protected internal abstract HttpSessionStateBase InternalSession { get; }
    }
}
