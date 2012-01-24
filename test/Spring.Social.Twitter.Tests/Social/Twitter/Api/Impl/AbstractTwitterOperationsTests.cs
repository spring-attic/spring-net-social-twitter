#region License

/*
 * Copyright 2002-2012 the original author or authors.
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

using NUnit.Framework;
using Spring.Rest.Client.Testing;

using Spring.IO;
using Spring.Http;
using Spring.Http.Client;

namespace Spring.Social.Twitter.Api.Impl
{
    /// <summary>
    /// Base class for all AbstractTwitterOperations subclasses unit tests.
    /// </summary>
    /// <author>Craig Walls</author>
    /// <author>Bruno Baia (.NET)</author>
    public abstract class AbstractTwitterOperationsTests
    {
	    protected TwitterTemplate twitter;	
	    protected TwitterTemplate unauthorizedTwitter;
	    protected MockRestServiceServer mockServer;
	    protected HttpHeaders responseHeaders;

	    [SetUp]
	    public void Setup() 
        {
		    twitter = new TwitterTemplate("API_KEY", "API_SECRET", "ACCESS_TOKEN", "ACCESS_TOKEN_SECRET");
		    mockServer = MockRestServiceServer.CreateServer(twitter.RestTemplate);
		    responseHeaders = new HttpHeaders();
		    responseHeaders.ContentType = MediaType.APPLICATION_JSON;

            unauthorizedTwitter = new TwitterTemplate();
            // TODO: create a mock server just to avoid hitting real twitter if something gets past the authorization check
            //MockRestServiceServer.CreateServer(unauthorizedTwitter.RestTemplate);
	    }

        [TearDown]
        public void TearDown()
        {
            mockServer.Verify();
        }
	
	    protected IResource JsonResource(string filename) 
        {
		    return new AssemblyResource("assembly://Spring.Social.Twitter.Tests/Spring.Social.Twitter.Api.Impl/" + filename + ".json");
	    }

	    protected void AssertSingleTweet(Tweet tweet) 
        {
		    Assert.AreEqual(12345, tweet.ID);
		    Assert.AreEqual("Tweet 1", tweet.Text);
		    Assert.AreEqual("habuma", tweet.FromUser);
		    Assert.AreEqual(112233, tweet.FromUserId);
		    Assert.AreEqual("http://a3.twimg.com/profile_images/1205746571/me2_300.jpg", tweet.ProfileImageUrl);
		    Assert.AreEqual("Spring Social Showcase", tweet.Source);
            Assert.IsNotNull(tweet.CreatedAt);
            Assert.AreEqual("13/07/2010 17:38:21", tweet.CreatedAt.Value.ToUniversalTime().ToString("dd/MM/yyyy HH:mm:ss"));
            Assert.IsNotNull(tweet.InReplyToStatusId);
		    Assert.AreEqual(123123123123, tweet.InReplyToStatusId.Value);
	    }
	
	    protected void AssertTimelineTweets(IList<Tweet> tweets) 
        {
		    Assert.AreEqual(2, tweets.Count);
		    this.AssertSingleTweet(tweets[0]);
		    Tweet tweet2 = tweets[1];
		    Assert.AreEqual(54321, tweet2.ID);
		    Assert.AreEqual("Tweet 2", tweet2.Text);
		    Assert.AreEqual("rclarkson", tweet2.FromUser);
		    Assert.AreEqual(332211, tweet2.FromUserId);
		    Assert.AreEqual("http://a3.twimg.com/profile_images/1205746571/me2_300.jpg", tweet2.ProfileImageUrl);
		    Assert.AreEqual("Twitter", tweet2.Source);
            Assert.IsNotNull(tweet2.CreatedAt);
            Assert.AreEqual("20/07/2010 19:38:21", tweet2.CreatedAt.Value.ToUniversalTime().ToString("dd/MM/yyyy HH:mm:ss"));
	    }

#if NET_4_0 || SILVERLIGHT_5
        protected void AssertTwitterApiException(AggregateException ae, string expectedMessage, TwitterApiError error)
        {
            ae.Handle(ex =>
            {
                if (ex is TwitterApiException)
                {
                    Assert.AreEqual(expectedMessage, ex.Message);
                    Assert.AreEqual(error, ((TwitterApiException)ex).Error);
                    return true;
                }
                return false;
            });
        }
#else
        protected void AssertTwitterApiException(Exception ex, string expectedMessage, TwitterApiError error)
        {
            if (ex is TwitterApiException)
            {
                Assert.AreEqual(expectedMessage, ex.Message);
                Assert.AreEqual(error, ((TwitterApiException)ex).Error);
            }
            else
            {
                Assert.Fail("TwitterApiException expected");
            }
        }
#endif
    }
}
