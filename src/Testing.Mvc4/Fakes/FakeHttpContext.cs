using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web;

namespace Cobweb.Testing.Mvc.Fakes {
    public class FakeHttpContext : FakeHttpContextBase {
        private IDictionary _items;
        private HttpRequestBase _request;
        private HttpResponseBase _response;
        private HttpSessionStateBase _sessionState;

        protected internal override sealed IDictionary InternalItems {
            get { return _items ?? (_items = new Dictionary<object, object>()); }
        }

        public new virtual IDictionary Items {
            get { return _items ?? (_items = new Dictionary<object, object>()); }
            set { _items = value; }
        }


        protected internal override sealed HttpRequestBase InternalRequest {
            get { return _request ?? (_request = new FakeHttpRequest()); }
        }

        public new virtual HttpRequestBase Request {
            get { return _request ?? (_request = new FakeHttpRequest()); }
            set { _request = value; }
        }

        protected internal override sealed HttpResponseBase InternalResponse {
            get { return _response ?? (_response = new FakeHttpResponse()); }
        }

        public new virtual HttpResponseBase Response {
            get { return _response ?? (_response = new FakeHttpResponse()); }
            set { _response = value; }
        }

        protected internal override sealed HttpSessionStateBase InternalSession {
            get { return _sessionState ?? (_sessionState = new FakeHttpSessionState()); }
        }

        public new virtual HttpSessionStateBase Session {
            get { return _sessionState ?? (_sessionState = new FakeHttpSessionState()); }
            set { _sessionState = value; }
        }

        public override IPrincipal User { get; set; }

        public override object GetService(Type serviceType) {
            return null;
        }
    }
}
