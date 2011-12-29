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

using System;
using System.Collections.Generic;

using Spring.Json;
using Spring.Rest.Client;
using Spring.Social.OAuth1;
using Spring.Http.Converters;
using Spring.Http.Converters.Json;

using Spring.Social.Twitter.Api.Impl.Json;

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
    /// will result in <see cref="NotAuthorizedException"/> being thrown.
    /// </para>
    /// </remarks>
    /// <author>Craig Walls</author>
    /// <author>Bruno Baia (.NET)</author>
    public class TwitterTemplate : AbstractOAuth1ApiBinding, ITwitter 
    {
        private static readonly Uri API_URI_BASE = new Uri("https://api.twitter.com/1/");

        private ITimelineOperations timelineOperations;

        /// <summary>
        /// Create a new instance of <see cref="TwitterTemplate"/> able to perform unauthenticated operations against Twitter's API.
        /// </summary>
        /// <remarks>
        /// Some operations, such as search, do not require OAuth authentication. 
        /// A TwitterTemplate created with this constructor will support those operations. 
        /// Any operations requiring authentication will throw an <see cref="NotAuthorizedException"/>.
        /// </remarks>
	    public TwitterTemplate() 
            : base()
        {
            this.InitSubApis();
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
            this.InitSubApis();
	    }

        #region ITwitter Membres

        /// <summary>
        /// Gets the portion of the Twitter API containing the tweet and timeline operations.
        /// </summary>
        public ITimelineOperations TimelineOperations
        {
            get { return this.timelineOperations; }
        }

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

        /// <summary>
        /// Enables customization of the RestTemplate used to consume provider API resources.
        /// </summary>
        /// <remarks>
        /// An example use case might be to configure a custom error handler. 
        /// Note that this method is called after the RestTemplate has been configured with the message converters returned from GetMessageConverters().
        /// </remarks>
        /// <param name="restTemplate">The RestTemplate to configure.</param>
        protected override void ConfigureRestTemplate(RestTemplate restTemplate)
        {
            restTemplate.BaseAddress = API_URI_BASE;
            restTemplate.ErrorHandler = new TwitterErrorHandler();
        }

        /// <summary>
        /// Returns a list of <see cref="IHttpMessageConverter"/>s to be used by the internal <see cref="RestTemplate"/>.
        /// </summary>
        /// <remarks>
        /// This implementation adds a <see cref="SpringJsonHttpMessageConverter"/> to the default list.
        /// </remarks>
        /// <returns>
        /// The list of <see cref="IHttpMessageConverter"/>s to be used by the internal <see cref="RestTemplate"/>.
        /// </returns>
        protected override IList<IHttpMessageConverter> GetMessageConverters()
        {
            JsonMapper jsonMapper = new JsonMapper();
            jsonMapper.RegisterDeserializer(typeof(Tweet), new TweetDeserializer());
            jsonMapper.RegisterDeserializer(typeof(IList<Tweet>), new TweetListDeserializer());
            jsonMapper.RegisterDeserializer(typeof(TwitterProfile), new TwitterProfileDeserializer());
            jsonMapper.RegisterDeserializer(typeof(IList<TwitterProfile>), new TwitterProfileListDeserializer());
            jsonMapper.RegisterDeserializer(typeof(IList<long>), new LongListDeserializer());

            IList<IHttpMessageConverter> converters = base.GetMessageConverters();
            converters.Add(new SpringJsonHttpMessageConverter(jsonMapper));
            return converters;
        }

        private void InitSubApis()
        {
            this.timelineOperations = new TimelineTemplate(this.RestTemplate, this.IsAuthorized);
        }
    }
}