﻿using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net.Http.Formatting.Parsers;

namespace System.Net.Http.Formatting
{    
    /// <summary>
    /// Represent the form data.
    /// - This has 100% fidelity (including ordering, which is important for deserializing ordered array). 
    /// - using interfaces allows us to optimize the implementation. Eg, we can avoid eagerly string-splitting a 10gb file. 
    /// - This also provides a convenient place to put extension methods. 
    /// </summary>
    public class FormDataCollection : IEnumerable<KeyValuePair<string, string>>
    {
        private readonly IEnumerable<KeyValuePair<string, string>> _pairs;
        private NameValueCollection _nameValueCollection;
                
        /// <summary>
        /// Initialize a form collection around incoming data. 
        /// The key value enumeration should be immutable. 
        /// </summary>
        /// <param name="pairs">incoming set of key value pairs. Ordering is preserved.</param>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is the convention for representing FormData")]        
        public FormDataCollection(IEnumerable<KeyValuePair<string, string>> pairs)
        {
            if (pairs == null)
            {
                throw new ArgumentNullException("pairs");
            }
            _pairs = pairs;
        }
        
        /// <summary>
        /// Initialize a form collection from a query string. 
        /// Uri and FormURl body have the same schema. 
        /// </summary>
        /// <param name="uri"></param>
        public FormDataCollection(Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException("uri");
            }

            string query = uri.Query;
            if (query.Length > 0 && query[0] == '?')
            {
                query = query.Substring(1);
            }

            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(query);
            
            List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();
            FormUrlEncodedParser parser = new FormUrlEncodedParser(result, Int64.MaxValue);

            int bytesConsumed = 0;
            ParserState state = parser.ParseBuffer(bytes, bytes.Length, ref bytesConsumed, isFinal: true);

            if (state != ParserState.Done)
            {
                throw new InvalidOperationException(RS.Format(Properties.Resources.FormUrlEncodedParseError, bytesConsumed));
            }

            _pairs = result;
        }
        
        /// <summary>
        /// Get the collection as a NameValueCollection.
        /// Beware this loses some ordering. Values are ordered within a key,
        /// but keys are no longer ordered against each other.         
        /// </summary>
        public NameValueCollection ReadAsNameValueCollection()
        {            
            if (_nameValueCollection == null)
            {
                // Ordering example:
                //   k=A&j=B&k=C --> k:[A,C];j=[B].
                NameValueCollection nvc = new NameValueCollection();
                foreach (KeyValuePair<string, string> kv in this)
                {
                    string key = kv.Key;
                    nvc.Add(key, kv.Value);
                }

                // Initialize in a private collection to be thread-safe, and swap the finished object.
                // Ok to double initialize this. 
                _nameValueCollection = nvc;
            }
            return _nameValueCollection;
        }

        /// <summary>
        /// Get values associated with a given key. If there are multiple values, they're concatenated. 
        /// </summary>
        public string Get(string key)
        {
            return ReadAsNameValueCollection().Get(key);
        }

        /// <summary>
        /// Get a value associated with a given key. 
        /// </summary>
        public string[] GetValues(string key)
        {
            return ReadAsNameValueCollection().GetValues(key);
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return _pairs.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            IEnumerable ie = _pairs;
            return ie.GetEnumerator();
        }
    }
}
