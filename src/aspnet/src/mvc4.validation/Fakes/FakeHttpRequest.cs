using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;

namespace Cobweb.Testing.Mvc.Fakes {
    public class FakeHttpRequest : FakeHttpRequestBase {
        private string[] _acceptTypes;
        private string _applicationPath;
        private HttpCookieCollection _cookies;
        private HttpFileCollectionBase _files;
        private NameValueCollection _formParams;
        private NameValueCollection _headers;
        private bool _isAuthenticated;
        private string _pathInfo;
        private NameValueCollection _queryStringParams;
        private string _relativeUrl;
        private NameValueCollection _serverVariables;
        private Uri _url;
        private Uri _urlReferrer;

        public FakeHttpRequest() {
            _applicationPath = "/";
            _isAuthenticated = false;
            _pathInfo = string.Empty;
            _relativeUrl = "~/";
        }

        public new virtual string[] AcceptTypes {
            get { return _acceptTypes ?? (_acceptTypes = new string[] {}); }
            set { _acceptTypes = value; }
        }

        protected internal override sealed string[] InternalAcceptTypes {
            get { return _acceptTypes ?? (_acceptTypes = new string[] {}); }
        }

        public new virtual string ApplicationPath {
            get { return _applicationPath; }
            set { _applicationPath = value; }
        }

        protected internal override sealed string InternalApplicationPath {
            get { return _applicationPath; }
        }

        public new virtual string AppRelativeCurrentExecutionFilePath {
            set { _relativeUrl = value; }
            get { return _relativeUrl; }
        }

        protected internal override sealed string InternalAppRelativeCurrentExecutionFilePath {
            get { return _relativeUrl; }
        }

        public new virtual HttpCookieCollection Cookies {
            set { _cookies = value; }
            get { return _cookies ?? (_cookies = new HttpCookieCollection()); }
        }

        protected internal override sealed HttpCookieCollection InternalCookies {
            get { return _cookies ?? (_cookies = new HttpCookieCollection()); }
        }

        public new virtual HttpFileCollectionBase Files {
            get { return _files ?? (_files = new FakeHttpFileCollection()); }
            set { _files = value; }
        }

        protected internal override sealed HttpFileCollectionBase InternalFiles {
            get { return _files ?? (_files = new FakeHttpFileCollection()); }
        }

        public new virtual NameValueCollection Form {
            get { return _formParams ?? (_formParams = new NameValueCollection()); }
            set { _formParams = value; }
        }

        protected internal override sealed NameValueCollection InternalForm {
            get { return _formParams ?? (_formParams = new NameValueCollection()); }
        }

        public new virtual NameValueCollection Headers {
            get { return _headers ?? (_headers = new NameValueCollection()); }
            set { _headers = value; }
        }

        protected internal override sealed NameValueCollection InternalHeaders {
            get { return _headers ?? (_headers = new NameValueCollection()); }
        }

        public new virtual string HttpMethod {
            get { return HttpVerb.ToString("g"); }
            set { HttpVerb = (HttpVerbs) Enum.Parse(typeof(HttpVerbs), value); }
        }

        protected internal override sealed string InternalHttpMethod {
            get { return HttpVerb.ToString("g"); }
        }

        public override HttpVerbs HttpVerb { get; set; }

        public new virtual bool IsAuthenticated {
            get { return _isAuthenticated; }
            set { _isAuthenticated = value; }
        }

        protected internal override sealed bool InternalIsAuthenticated {
            get { return _isAuthenticated; }
        }

        public new virtual string PathInfo {
            get { return _pathInfo; }
            set { _pathInfo = value; }
        }

        protected internal override sealed string InternalPathInfo {
            get { return _pathInfo; }
        }

        public new virtual NameValueCollection QueryString {
            get { return _queryStringParams ?? (_queryStringParams = new NameValueCollection()); }
            set { _queryStringParams = value; }
        }

        protected internal override sealed NameValueCollection InternalQueryString {
            get { return _queryStringParams ?? (_queryStringParams = new NameValueCollection()); }
        }

        public new virtual NameValueCollection ServerVariables {
            get { return _serverVariables ?? (_serverVariables = new NameValueCollection()); }
            set { _serverVariables = value; }
        }

        protected internal override sealed NameValueCollection InternalServerVariables {
            get { return _serverVariables ?? (_serverVariables = new NameValueCollection()); }
        }

        public new virtual Uri Url {
            get { return _url; }
            set { _url = value; }
        }

        protected internal override sealed Uri InternalUrl {
            get { return _url; }
        }

        public new virtual Uri UrlReferrer {
            get { return _urlReferrer; }
            set { _urlReferrer = value; }
        }

        protected internal override sealed Uri InternalUrlReferrer {
            get { return _urlReferrer; }
        }
    }
}
