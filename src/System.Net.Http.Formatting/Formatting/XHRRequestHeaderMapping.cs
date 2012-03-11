﻿using System.Linq;
using System.Net.Http.Headers;

namespace System.Net.Http.Formatting
{
    /// <summary>
    /// A RequestHeaderMapping for the x-requested-with http header set by ajax XHR's
    /// </summary>
    internal sealed class XHRRequestHeaderMapping : RequestHeaderMapping
    {
        /// <summary>
        /// Initializes a new instance of <see cref="XHRRequestHeaderMapping" /> class
        /// </summary>
        public XHRRequestHeaderMapping() :
            base(FormattingUtilities.HttpRequestedWithHeader, FormattingUtilities.HttpRequestedWithHeaderValue, StringComparison.OrdinalIgnoreCase, true, MediaTypeConstants.ApplicationJsonMediaType)
        {
        }

        /// <summary>
        /// Returns a value indicating whether the current <see cref="RequestHeaderMapping"/>
        /// instance can return a <see cref="MediaTypeHeaderValue"/> from <paramref name="request"/>.
        /// </summary>
        /// <param name="request">The <see cref="HttpRequestMessage"/> to check.</param>
        /// <returns>
        /// The quality of the match.
        /// A value of <c>0.0</c> signifies no match.
        /// A value of <c>1.0</c> signifies a complete match.
        /// </returns>
        public override double TryMatchMediaType(HttpRequestMessage request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            // Accept header trumps XHR mapping.
            // Accept: */* is equivalent to passing no Accept header.
            if (request.Headers.Accept == null
                || (request.Headers.Accept.Count == 1 && request.Headers.Accept.First().MediaType.Equals("*/*", StringComparison.Ordinal)))
            {
                return base.TryMatchMediaType(request);
            }
            else
            {
                return MediaTypeMatch.NoMatch;
            }
        }
    }
}
