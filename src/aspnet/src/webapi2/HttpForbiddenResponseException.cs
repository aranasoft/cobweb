﻿using System.Net;
using System.Net.Http;
using System.Web.Http;
using Aranasoft.Cobweb.Http.Formatting;

namespace Aranasoft.Cobweb.Http {
    public class HttpForbiddenResponseException : HttpResponseException {
        public HttpForbiddenResponseException(string reason, string errorMessage)
            : base(
                new HttpResponseMessage(HttpStatusCode.Forbidden) {
                    ReasonPhrase = reason,
                    Content = new JsonErrorObjectContent(errorMessage)
                }) {}
    }
}
