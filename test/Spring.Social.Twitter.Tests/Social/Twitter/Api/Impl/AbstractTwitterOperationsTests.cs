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
	    protected MockRestServiceServer mockServer;
	    protected HttpHeaders responseHeaders;

	    [SetUp]
	    public void Setup() 
        {
		    twitter = new TwitterTemplate("API_KEY", "API_SECRET", "ACCESS_TOKEN", "ACCESS_TOKEN_SECRET");
		    mockServer = MockRestServiceServer.CreateServer(twitter.RestTemplate);
		    responseHeaders = new HttpHeaders();
		    responseHeaders.ContentType = MediaType.APPLICATION_JSON;
	    }

        [TearDown]
        public void TearDown()
        {
            mockServer.Verify();
        }
	
	    protected IResource JsonResource(string filename) 
        {
		    return new AssemblyResource(filename + ".json", typeof(AbstractTwitterOperationsTests));
	    }

	    protected void AssertSingleTweet(Tweet tweet) 
        {
		    Assert.AreEqual(12345, tweet.ID);
		    Assert.AreEqual("Tweet 1", tweet.Text);
            Assert.IsNotNull(tweet.CreatedAt);
            Assert.AreEqual("13/07/2010 17:38:21", tweet.CreatedAt.Value.ToUniversalTime().ToString("dd/MM/yyyy HH:mm:ss"));
            Assert.AreEqual("habuma", tweet.User.ScreenName);
            Assert.AreEqual(112233, tweet.User.ID);
            Assert.AreEqual("https://a3.twimg.com/profile_images/1205746571/me2_300.jpg", tweet.User.ProfileImageUrl);
		    Assert.AreEqual("habuma", tweet.FromUser); // Deprecated
            Assert.AreEqual(112233, tweet.FromUserId); // Deprecated
            Assert.AreEqual("https://a3.twimg.com/profile_images/1205746571/me2_300.jpg", tweet.ProfileImageUrl); // Deprecated
            Assert.IsNotNull(tweet.InReplyToStatusId);
		    Assert.AreEqual(123123123123, tweet.InReplyToStatusId.Value);
            Assert.IsNotNull(tweet.InReplyToUserId);
            Assert.AreEqual(332211, tweet.InReplyToUserId.Value);
            Assert.IsNotNull(tweet.ToUserId); // Deprecated
            Assert.AreEqual(332211, tweet.ToUserId.Value); // Deprecated
            Assert.AreEqual("brbaia", tweet.InReplyToUserScreenName);
            Assert.AreEqual("Spring Social Showcase", tweet.Source);
            Assert.AreEqual(9, tweet.FavoriteCount);
            Assert.AreEqual(12, tweet.RetweetCount);
            Assert.IsTrue(tweet.IsRetweetedByUser);
            Assert.IsTrue(tweet.IsFavoritedByUser);
	    }

        protected void AssertTweetEntities(TweetEntities entities)
        {
            Assert.IsNotNull(entities);
            // Hashtags
            Assert.IsNotNull(entities.Hashtags);
            Assert.AreEqual(1, entities.Hashtags.Count);
            Assert.AreEqual("Twitterbird", entities.Hashtags[0].Text);
            Assert.AreEqual(19, entities.Hashtags[0].BeginOffset);
            Assert.AreEqual(31, entities.Hashtags[0].EndOffset);
            // User mentions
            Assert.IsNotNull(entities.UserMentions);
            Assert.AreEqual(2, entities.UserMentions.Count);
            Assert.AreEqual(11223344, entities.UserMentions[0].ID);
            Assert.AreEqual("ukuleleman", entities.UserMentions[0].ScreenName);
            Assert.AreEqual("Bucky Greenhorn", entities.UserMentions[0].Name);
            Assert.AreEqual(3, entities.UserMentions[0].BeginOffset);
            Assert.AreEqual(18, entities.UserMentions[0].EndOffset);
            // URLs
            Assert.IsNotNull(entities.Urls);
            Assert.AreEqual(1, entities.Urls.Count);
            Assert.AreEqual(10, entities.Urls[0].BeginOffset);
            Assert.AreEqual(30, entities.Urls[0].EndOffset);
            Assert.AreEqual("https://t.co/t35tur1", entities.Urls[0].Url);
            Assert.AreEqual("fb.me/t35tur1", entities.Urls[0].DisplayUrl);
            Assert.AreEqual("https://fb.me/t35tur1", entities.Urls[0].ExpandedUrl);
            // Media
            Assert.IsNotNull(entities.Media);
            Assert.AreEqual(1, entities.Media.Count);
            Assert.AreEqual(15, entities.Media[0].BeginOffset);
            Assert.AreEqual(35, entities.Media[0].EndOffset);
            Assert.AreEqual(114080493040967680, entities.Media[0].ID);
            Assert.AreEqual("https://t.co/rJC5Pxsu", entities.Media[0].Url);
            Assert.AreEqual("pic.twitter.com/rJC5Pxsu", entities.Media[0].DisplayUrl);
            Assert.AreEqual("https://twitter.com/yunorno/status/114080493036773378/photo/1", entities.Media[0].ExpandedUrl);
            Assert.AreEqual("https://p.twimg.com/AZVLmp-CIAAbkyy.jpg", entities.Media[0].MediaUrl);
            Assert.AreEqual("https://p.twimg.com/AZVLmp-CIAAbkyy.jpg", entities.Media[0].MediaHttpsUrl);
            Assert.AreEqual(205282515685081088, entities.Media[0].SourceStatusId);
        }
	
	    protected void AssertTimelineTweets(IList<Tweet> tweets) 
        {
		    Assert.AreEqual(2, tweets.Count);
		    this.AssertSingleTweet(tweets[0]);
		    Tweet tweet2 = tweets[1];
		    Assert.AreEqual(54321, tweet2.ID);
		    Assert.AreEqual("Tweet 2", tweet2.Text);
            Assert.IsNotNull(tweet2.CreatedAt);
            Assert.AreEqual("20/07/2010 19:38:21", tweet2.CreatedAt.Value.ToUniversalTime().ToString("dd/MM/yyyy HH:mm:ss"));
            Assert.AreEqual("rclarkson", tweet2.User.ScreenName);
            Assert.AreEqual(332211, tweet2.User.ID);
            Assert.AreEqual("https://a3.twimg.com/profile_images/1205746571/me2_300.jpg", tweet2.User.ProfileImageUrl);
            Assert.AreEqual("rclarkson", tweet2.FromUser); // Deprecated
            Assert.AreEqual(332211, tweet2.FromUserId); // Deprecated
            Assert.AreEqual("https://a3.twimg.com/profile_images/1205746571/me2_300.jpg", tweet2.ProfileImageUrl); // Deprecated
		    Assert.AreEqual("Twitter", tweet2.Source);
            Assert.AreEqual(0, tweet2.FavoriteCount);
            Assert.AreEqual(0, tweet2.RetweetCount);
            Assert.IsFalse(tweet2.IsRetweetedByUser);
            Assert.IsFalse(tweet2.IsFavoritedByUser);
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
