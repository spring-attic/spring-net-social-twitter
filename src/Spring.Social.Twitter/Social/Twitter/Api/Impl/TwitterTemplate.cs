#region License

/*
 * Copyright 2002-2011 the original author or authors.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

#endregion

using Spring.Rest.Client;
using Spring.Social.OAuth1;

namespace Spring.Social.Twitter.Api.Impl
{
    /// <summary>
    /// This is the central class for interacting with Twitter.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Most (not all) Twitter operations require OAuth authentication. 
    /// To perform such operations, <see cref="TwitterTemplate"/> must be constructed 
    /// with the minimal amount of information required to sign requests to Twitter's API 
    /// with an OAuth <code>Authorization</code> header.
    /// </para>
    /// <para>
    /// There are some operations, such as searching, that do not require OAuth authentication. 
    /// In those cases, you may use a <see cref="TwitterTemplate"/> that is created through 
    /// the default constructor and without any OAuth details.
    /// Attempts to perform secured operations through such an instance, however, 
    /// will result in NotAuthorizedException being thrown.
    /// </para>
    /// </remarks>
    /// <author>Craig Walls</author>
    /// <author>Bruno Baia (.NET)</author>
    public class TwitterTemplate : AbstractOAuth1ApiBinding, ITwitter 
    {
        /// <summary>
        /// Create a new instance of <see cref="TwitterTemplate"/> able to perform unauthenticated operations against Twitter's API.
        /// </summary>
        /// <remarks>
        /// Some operations, such as search, do not require OAuth authentication. 
        /// A TwitterTemplate created with this constructor will support those operations. 
        /// Any operations requiring authentication will throw an NotAuthorizedException.
        /// </remarks>
	    public TwitterTemplate() 
            : base()
        {
	    }

        /// <summary>
        /// Create a new instance of <see cref="TwitterTemplate"/>.
        /// </summary>
        /// <param name="consumerKey">The application's API key.</param>
        /// <param name="consumerSecret">The application's API secret.</param>
        /// <param name="accessToken">An access token acquired through OAuth authentication with Twitter.</param>
        /// <param name="accessTokenSecret">An access token secret acquired through OAuth authentication with Twitter.</param>
        public TwitterTemplate(string consumerKey, string consumerSecret, string accessToken, string accessTokenSecret) 
            : base(consumerKey, consumerSecret, accessToken, accessTokenSecret)
        {
	    }

        #region ITwitter Membres

        /// <summary>
        /// Gets the underlying <see cref="IRestOperations"/> object allowing for consumption of Twitter endpoints 
        /// that may not be otherwise covered by the API binding. 
        /// </summary>
        /// <remarks>
        /// The <see cref="IRestOperations"/> object returned is configured to include an OAuth "Authorization" header on all requests.
        /// </remarks>
        public IRestOperations RestOperations
        {
            get { return this.RestTemplate; }
        }

        #endregion
    }
}