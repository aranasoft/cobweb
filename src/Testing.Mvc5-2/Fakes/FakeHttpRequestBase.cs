using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;

namespace Cobweb.Testing.Mvc.Fakes {
    public abstract class FakeHttpRequestBase : HttpRequestBase {
        protected internal FakeHttpRequestBase() {}

        public override sealed string[] AcceptTypes {
            get { return InternalAcceptTypes; }
        }

        protected internal abstract string[] InternalAcceptTypes { get; }

        public override sealed string ApplicationPath {
            get { return InternalApplicationPath; }
        }

        protected internal abstract string InternalApplicationPath { get; }

        public override sealed string AppRelativeCurrentExecutionFilePath {
            get { return InternalAppRelativeCurrentExecutionFilePath; }
        }

        protected internal abstract string InternalAppRelativeCurrentExecutionFilePath { get; }

        public override sealed HttpCookieCollection Cookies {
            get { return InternalCookies; }
        }

        protected internal abstract HttpCookieCollection InternalCookies { get; }

        public override sealed HttpFileCollectionBase Files {
            get { return InternalFiles; }
        }

        protected internal abstract HttpFileCollectionBase InternalFiles { get; }

        public override sealed NameValueCollection Form {
            get { return InternalForm; }
        }

        protected internal abstract NameValueCollection InternalForm { get; }

        public override sealed NameValueCollection Headers {
            get { return InternalHeaders; }
        }

        protected internal abstract NameValueCollection InternalHeaders { get; }

        public override sealed string HttpMethod {
            get { return InternalHttpMethod; }
        }

        protected internal abstract string InternalHttpMethod { get; }
        public abstract HttpVerbs HttpVerb { get; set; }

        public override sealed bool IsAuthenticated {
            get { return InternalIsAuthenticated; }
        }

        protected internal abstract bool InternalIsAuthenticated { get; }

        public override NameValueCollection Params {
            get { return new NameValueCollection {QueryString, Form, ServerVariables}; }
        }

        public override sealed string PathInfo {
            get { return InternalPathInfo; }
        }

        protected internal abstract string InternalPathInfo { get; }

        public override sealed NameValueCollection QueryString {
            get { return InternalQueryString; }
        }

        protected internal abstract NameValueCollection InternalQueryString { get; }

        public override string RawUrl {
            get { return Url.AbsolutePath; }
        }

        public override sealed NameValueCollection ServerVariables {
            get { return InternalServerVariables; }
        }

        protected internal abstract NameValueCollection InternalServerVariables { get; }

        public override sealed Uri Url {
            get { return InternalUrl; }
        }

        protected internal abstract Uri InternalUrl { get; }

        public override sealed Uri UrlReferrer {
            get { return InternalUrlReferrer; }
        }

        protected internal abstract Uri InternalUrlReferrer { get; }
    }
}
