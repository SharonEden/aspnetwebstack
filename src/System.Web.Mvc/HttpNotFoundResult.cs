﻿using System.Net;

namespace System.Web.Mvc
{
    public class HttpNotFoundResult : HttpStatusCodeResult
    {
        public HttpNotFoundResult()
            : this(null)
        {
        }

        // NotFound is equivalent to HTTP status 404.
        public HttpNotFoundResult(string statusDescription)
            : base(HttpStatusCode.NotFound, statusDescription)
        {
        }
    }
}
