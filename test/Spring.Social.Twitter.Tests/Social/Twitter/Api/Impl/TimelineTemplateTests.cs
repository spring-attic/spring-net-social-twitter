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
using System.Net;
using System.Collections.Generic;

using NUnit.Framework;
using Spring.Rest.Client.Testing;

using Spring.Http;
using Spring.IO;

namespace Spring.Social.Twitter.Api.Impl
{
    /// <summary>
    /// Unit tests for the TimelineTemplate class.
    /// </summary>
    /// <author>Craig Walls</author>
    /// <author>Bruno Baia (.NET)</author>
    [TestFixture]
    public class TimelineTemplateTests : AbstractTwitterOperationsTests 
    {    
	    [Test]
	    public void GetHomeTimelineAsync() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/statuses/home_timeline.json")
				.AndExpectMethod(HttpMethod.GET)
				.AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> timeline = twitter.TimelineOperations.GetHomeTimelineAsync().Result;
#else
            IList<Tweet> timeline = twitter.TimelineOperations.GetHomeTimeline();
#endif
            AssertTimelineTweets(timeline);
	    }

	    [Test]
	    public void GetHomeTimeline_WithCount() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/statuses/home_timeline.json?count=100")
				.AndExpectMethod(HttpMethod.GET)
				.AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> timeline = twitter.TimelineOperations.GetHomeTimelineAsync(100).Result;
#else
            IList<Tweet> timeline = twitter.TimelineOperations.GetHomeTimeline(100);
#endif
            AssertTimelineTweets(timeline);
	    }

	    [Test]
	    public void GetHomeTimeline_WithCountAndSinceIdAndMaxId() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/statuses/home_timeline.json?count=100&since_id=1234567&max_id=7654321")
				.AndExpectMethod(HttpMethod.GET)
				.AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> timeline = twitter.TimelineOperations.GetHomeTimelineAsync(100, 1234567, 7654321).Result;
#else
            IList<Tweet> timeline = twitter.TimelineOperations.GetHomeTimeline(100, 1234567, 7654321);
#endif
            AssertTimelineTweets(timeline);
	    }

	    [Test]
	    public void GetUserTimelineAsync() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/statuses/user_timeline.json")
				.AndExpectMethod(HttpMethod.GET)
				.AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> timeline = twitter.TimelineOperations.GetUserTimelineAsync().Result;
#else
            IList<Tweet> timeline = twitter.TimelineOperations.GetUserTimeline();
#endif
            AssertTimelineTweets(timeline);
	    }

	    [Test]
	    public void GetUserTimeline_WithCount() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/statuses/user_timeline.json?count=15")
				.AndExpectMethod(HttpMethod.GET)
				.AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> timeline = twitter.TimelineOperations.GetUserTimelineAsync(15).Result;
#else
            IList<Tweet> timeline = twitter.TimelineOperations.GetUserTimeline(15);
#endif
            AssertTimelineTweets(timeline);
	    }

	    [Test]
	    public void GetUserTimeline_WithCountAndSinceIdAndMaxId() {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/statuses/user_timeline.json?count=15&since_id=123456&max_id=654321")
				.AndExpectMethod(HttpMethod.GET)
				.AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> timeline = twitter.TimelineOperations.GetUserTimelineAsync(15, 123456, 654321).Result;
#else
            IList<Tweet> timeline = twitter.TimelineOperations.GetUserTimeline(15, 123456, 654321);
#endif
            AssertTimelineTweets(timeline);
	    }
	
	    [Test]
	    public void GetUserTimeline_ForScreenName() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/statuses/user_timeline.json?screen_name=habuma")
				.AndExpectMethod(HttpMethod.GET)
				.AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> timeline = twitter.TimelineOperations.GetUserTimelineAsync("habuma").Result;
#else
            IList<Tweet> timeline = twitter.TimelineOperations.GetUserTimeline("habuma");
#endif
            AssertTimelineTweets(timeline);
	    }

	    [Test]
	    public void GetUserTimeline_ForScreenName_WithCount() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/statuses/user_timeline.json?count=24&screen_name=habuma")
				.AndExpectMethod(HttpMethod.GET)
				.AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> timeline = twitter.TimelineOperations.GetUserTimelineAsync("habuma", 24).Result;
#else
            IList<Tweet> timeline = twitter.TimelineOperations.GetUserTimeline("habuma", 24);
#endif
            AssertTimelineTweets(timeline);
	    }

	    [Test]
	    public void GetUserTimeline_ForScreenName_WithCountAndSinceIdAndMaxId() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/statuses/user_timeline.json?count=24&since_id=112233&max_id=332211&screen_name=habuma")
				.AndExpectMethod(HttpMethod.GET)
				.AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> timeline = twitter.TimelineOperations.GetUserTimelineAsync("habuma", 24, 112233, 332211).Result;
#else
            IList<Tweet> timeline = twitter.TimelineOperations.GetUserTimeline("habuma", 24, 112233, 332211);
#endif
            AssertTimelineTweets(timeline);
	    }

	    [Test]
	    public void GetUserTimeline_ForUserId() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/statuses/user_timeline.json?user_id=12345")
				.AndExpectMethod(HttpMethod.GET)
				.AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> timeline = twitter.TimelineOperations.GetUserTimelineAsync(12345L).Result;
#else
            IList<Tweet> timeline = twitter.TimelineOperations.GetUserTimeline(12345L);
#endif
            AssertTimelineTweets(timeline);
	    }

	    [Test]
	    public void GetUserTimeline_ForUserId_WithCount() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/statuses/user_timeline.json?count=24&user_id=12345")
				.AndExpectMethod(HttpMethod.GET)
				.AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> timeline = twitter.TimelineOperations.GetUserTimelineAsync(12345L, 24).Result;
#else
            IList<Tweet> timeline = twitter.TimelineOperations.GetUserTimeline(12345L, 24);
#endif
            AssertTimelineTweets(timeline);
	    }

	    [Test]
	    public void GetUserTimeline_ForUserId_WithCountAndSinceIdAndMaxId() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/statuses/user_timeline.json?count=24&since_id=112233&max_id=332211&user_id=12345")
				.AndExpectMethod(HttpMethod.GET)
				.AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> timeline = twitter.TimelineOperations.GetUserTimelineAsync(12345L, 24, 112233, 332211).Result;
#else
            IList<Tweet> timeline = twitter.TimelineOperations.GetUserTimeline(12345L, 24, 112233, 332211);
#endif
            AssertTimelineTweets(timeline);
	    }

	    [Test]
	    public void GetMentions() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/statuses/mentions_timeline.json")
				.AndExpectMethod(HttpMethod.GET)
				.AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> mentions = twitter.TimelineOperations.GetMentionsAsync().Result;
#else
            IList<Tweet> mentions = twitter.TimelineOperations.GetMentions();
#endif
            AssertTimelineTweets(mentions);
	    }

	    [Test]
	    public void GetMentions_WithCount() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/statuses/mentions_timeline.json?count=50")
				.AndExpectMethod(HttpMethod.GET)
				.AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> mentions = twitter.TimelineOperations.GetMentionsAsync(50).Result;
#else
            IList<Tweet> mentions = twitter.TimelineOperations.GetMentions(50);
#endif
            AssertTimelineTweets(mentions);
	    }

	    [Test]
	    public void GetMentions_WithCountAndSinceIdAndMaxId() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/statuses/mentions_timeline.json?count=50&since_id=112233&max_id=332211")
				.AndExpectMethod(HttpMethod.GET)
				.AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> mentions = twitter.TimelineOperations.GetMentionsAsync(50, 112233, 332211).Result;
#else
            IList<Tweet> mentions = twitter.TimelineOperations.GetMentions(50, 112233, 332211);
#endif
            AssertTimelineTweets(mentions);
	    }
	
	    [Test]
	    public void GetRetweetsOfMe() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/statuses/retweets_of_me.json")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> timeline = twitter.TimelineOperations.GetRetweetsOfMeAsync().Result;
#else
            IList<Tweet> timeline = twitter.TimelineOperations.GetRetweetsOfMe();
#endif
            AssertTimelineTweets(timeline);				
	    }

	    [Test]
	    public void GetRetweetsOfMe_WithCount() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/statuses/retweets_of_me.json?count=25")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> timeline = twitter.TimelineOperations.GetRetweetsOfMeAsync(25).Result;
#else
            IList<Tweet> timeline = twitter.TimelineOperations.GetRetweetsOfMe(25);
#endif
            AssertTimelineTweets(timeline);				
	    }

	    [Test]
	    public void GetRetweetsOfMe_WithCountAndSinceIdAndMaxId() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/statuses/retweets_of_me.json?count=25&since_id=2345&max_id=3456")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> timeline = twitter.TimelineOperations.GetRetweetsOfMeAsync(25, 2345, 3456).Result;
#else
            IList<Tweet> timeline = twitter.TimelineOperations.GetRetweetsOfMe(25, 2345, 3456);
#endif
            AssertTimelineTweets(timeline);				
	    }
	
	    [Test]
	    public void GetStatus() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/statuses/show/12345.json")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Status"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            Tweet tweet = twitter.TimelineOperations.GetStatusAsync(12345).Result;
#else
            Tweet tweet = twitter.TimelineOperations.GetStatus(12345);
#endif
            AssertSingleTweet(tweet);
	    }
	
	    [Test]
	    public void UpdateStatus() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/statuses/update.json")
				.AndExpectMethod(HttpMethod.POST)
				.AndExpectBody("status=Test+Message")
				.AndRespondWith(JsonResource("Status"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            Tweet tweet = twitter.TimelineOperations.UpdateStatusAsync("Test Message").Result;
#else
            Tweet tweet = twitter.TimelineOperations.UpdateStatus("Test Message");
#endif
            AssertSingleTweet(tweet);
	    }

	    [Test]
	    public void UpdateStatus_WithImage() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/statuses/update_with_media.json")
				.AndExpectMethod(HttpMethod.POST)
                .AndExpectHeaderContains("Content-Type", MediaType.MULTIPART_FORM_DATA.ToString() + ";boundary=")
                .AndExpectBodyContains("Content-Disposition: form-data; name=\"status\"")
                .AndExpectBodyContains("Content-Type: text/plain;charset=ISO-8859-1")
                .AndExpectBodyContains("Test Message")
                .AndExpectBodyContains("Content-Disposition: form-data; name=\"media\"; filename=\"Logo.png\"")
                .AndExpectBodyContains("Content-Type: image/png")
				.AndRespondWith(JsonResource("Status"), responseHeaders);

            IResource photo = new AssemblyResource("assembly://Spring.Social.Twitter.Tests/Spring.Social.Twitter.Api.Impl/Logo.png");
#if NET_4_0 || SILVERLIGHT_5
            Tweet tweet = twitter.TimelineOperations.UpdateStatusAsync("Test Message", photo).Result;
#else
            Tweet tweet = twitter.TimelineOperations.UpdateStatus("Test Message", photo);
#endif
            AssertSingleTweet(tweet);
	    }

	    [Test]
	    public void UpdateStatus_WithLocation() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/statuses/update.json")
				.AndExpectMethod(HttpMethod.POST)
				.AndExpectBody("status=Test+Message&lat=123.1&long=-111.2")
				.AndRespondWith(JsonResource("Status"), responseHeaders);

		    StatusDetails details = new StatusDetails();
		    details.Latitude = 123.1f;
            details.Longitude = -111.2f;
#if NET_4_0 || SILVERLIGHT_5
            Tweet tweet = twitter.TimelineOperations.UpdateStatusAsync("Test Message", details).Result;
#else
            Tweet tweet = twitter.TimelineOperations.UpdateStatus("Test Message", details);
#endif
            AssertSingleTweet(tweet);
	    }

	    [Test]
	    public void UpdateStatus_WithLocationAndDisplayCoordinates() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/statuses/update.json")
				.AndExpectMethod(HttpMethod.POST)
				.AndExpectBody("status=Test+Message&lat=123.1&long=-111.2&display_coordinates=true")
				.AndRespondWith(JsonResource("Status"), responseHeaders);

		    StatusDetails details = new StatusDetails();
            details.Latitude = 123.1f;
            details.Longitude = -111.2f;
		    details.DisplayCoordinates = true;
#if NET_4_0 || SILVERLIGHT_5
            Tweet tweet = twitter.TimelineOperations.UpdateStatusAsync("Test Message", details).Result;
#else
            Tweet tweet = twitter.TimelineOperations.UpdateStatus("Test Message", details);
#endif
            AssertSingleTweet(tweet);
	    }

	    [Test]
	    public void UpdateStatus_WithInReplyToStatus() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/statuses/update.json")
				.AndExpectMethod(HttpMethod.POST)
				.AndExpectBody("status=Test+Message+in+reply+to+%40someone&in_reply_to_status_id=123456")
				.AndRespondWith(JsonResource("Status"), responseHeaders);

		    StatusDetails details = new StatusDetails();
		    details.InReplyToStatusId = 123456;
#if NET_4_0 || SILVERLIGHT_5
            Tweet tweet = twitter.TimelineOperations.UpdateStatusAsync("Test Message in reply to @someone", details).Result;
#else
            Tweet tweet = twitter.TimelineOperations.UpdateStatus("Test Message in reply to @someone", details);
#endif
            AssertSingleTweet(tweet);
	    }

	    [Test]
	    public void UpdateStatus_WithWrapLinks() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/statuses/update.json")
				.AndExpectMethod(HttpMethod.POST)
				.AndExpectBody("status=Test+Message&wrap_links=true")
				.AndRespondWith(JsonResource("Status"), responseHeaders);

		    StatusDetails details = new StatusDetails();
		    details.WrapLinks = true;
#if NET_4_0 || SILVERLIGHT_5
            Tweet tweet = twitter.TimelineOperations.UpdateStatusAsync("Test Message", details).Result;
#else
            Tweet tweet = twitter.TimelineOperations.UpdateStatus("Test Message", details);
#endif
            AssertSingleTweet(tweet);
	    }

	    [Test]
	    public void UpdateStatus_WithImageAndLocation() 
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/statuses/update_with_media.json")
                .AndExpectMethod(HttpMethod.POST)
                .AndExpectHeaderContains("Content-Type", MediaType.MULTIPART_FORM_DATA.ToString() + ";boundary=")
                .AndExpectBodyContains("Content-Disposition: form-data; name=\"status\"")
                .AndExpectBodyContains("Content-Type: text/plain;charset=ISO-8859-1")
                .AndExpectBodyContains("Test Message")
                .AndExpectBodyContains("Content-Disposition: form-data; name=\"lat\"")
                .AndExpectBodyContains("123.1")
                .AndExpectBodyContains("Content-Disposition: form-data; name=\"long\"")
                .AndExpectBodyContains("-111.2")
                .AndExpectBodyContains("Content-Disposition: form-data; name=\"media\"; filename=\"Logo.png\"")
                .AndExpectBodyContains("Content-Type: image/png")
                .AndRespondWith(JsonResource("Status"), responseHeaders);

            IResource photo = new AssemblyResource("assembly://Spring.Social.Twitter.Tests/Spring.Social.Twitter.Api.Impl/Logo.png");
            StatusDetails details = new StatusDetails();
            details.Latitude = 123.1f;
            details.Longitude = -111.2f;
#if NET_4_0 || SILVERLIGHT_5
            Tweet tweet = twitter.TimelineOperations.UpdateStatusAsync("Test Message", photo, details).Result;
#else
            Tweet tweet = twitter.TimelineOperations.UpdateStatus("Test Message", photo, details);
#endif
            AssertSingleTweet(tweet);
	    }

	    [Test]
	    public void UpdateStatus_DuplicateTweet() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/statuses/update.json")
				.AndExpectMethod(HttpMethod.POST)
				.AndExpectBody("status=Test+Message")
				.AndRespondWith("{\"error\":\"You already said that\"}", responseHeaders, HttpStatusCode.Forbidden, "");

#if NET_4_0 || SILVERLIGHT_5
            twitter.TimelineOperations.UpdateStatusAsync("Test Message")
                .ContinueWith(task =>
                {
                    AssertTwitterApiException(task.Exception, "You already said that", TwitterApiError.OperationNotPermitted);
                })
                .Wait();
#else
            try
            {
                twitter.TimelineOperations.UpdateStatus("Test Message");
                Assert.Fail("Exception expected");
            }
            catch (Exception ex)
            {
                AssertTwitterApiException(ex, "You already said that", TwitterApiError.OperationNotPermitted);
            }
#endif
        }
	
	    [Test]
	    public void UpdateStatus_TweetTooLong() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/statuses/update.json")
			    .AndExpectMethod(HttpMethod.POST)
			    .AndExpectBody("status=Really+long+message")
			    .AndRespondWith("{\"error\":\"Status is over 140 characters.\"}", responseHeaders, HttpStatusCode.Forbidden, "");

#if NET_4_0 || SILVERLIGHT_5
            twitter.TimelineOperations.UpdateStatusAsync("Really long message")
                .ContinueWith(task =>
                {
                    AssertTwitterApiException(task.Exception, "Status is over 140 characters.", TwitterApiError.OperationNotPermitted);
                })
                .Wait();
#else
            try
            {
                twitter.TimelineOperations.UpdateStatus("Really long message");
                Assert.Fail("Exception expected");
            }
            catch (Exception ex)
            {
                AssertTwitterApiException(ex, "Status is over 140 characters.", TwitterApiError.OperationNotPermitted);
            }
#endif
        }
	
	    [Test]
	    public void UpdateStatus_Forbidden() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/statuses/update.json")
				.AndExpectMethod(HttpMethod.POST)
				.AndExpectBody("status=Test+Message")
				.AndRespondWith("{\"error\":\"Forbidden\"}", responseHeaders, HttpStatusCode.Forbidden, "");

#if NET_4_0 || SILVERLIGHT_5
            twitter.TimelineOperations.UpdateStatusAsync("Test Message")
                .ContinueWith(task =>
                {
                    AssertTwitterApiException(task.Exception, "Forbidden", TwitterApiError.OperationNotPermitted);
                })
                .Wait();
#else
            try
            {
                twitter.TimelineOperations.UpdateStatus("Test Message");
                Assert.Fail("Exception expected");
            }
            catch (Exception ex)
            {
                AssertTwitterApiException(ex, "Forbidden", TwitterApiError.OperationNotPermitted);
            }
#endif
        }

	    [Test]
	    public void DeleteStatus() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/statuses/destroy/12345.json")
			    .AndExpectMethod(HttpMethod.POST)
                .AndRespondWith(JsonResource("Status"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            Tweet deletedTweet = twitter.TimelineOperations.DeleteStatusAsync(12345L).Result;
#else
            Tweet deletedTweet = twitter.TimelineOperations.DeleteStatus(12345L);
#endif
            AssertSingleTweet(deletedTweet);
        }
	
	    [Test]
	    public void Retweet() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/statuses/retweet/12345.json")
				.AndExpectMethod(HttpMethod.POST)
				.AndRespondWith("{}", responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            twitter.TimelineOperations.RetweetAsync(12345).Wait();
#else
            twitter.TimelineOperations.Retweet(12345);
#endif
        }

	    [Test]
	    public void Retweet_DuplicateTweet() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/statuses/retweet/12345.json")
				.AndExpectMethod(HttpMethod.POST)
				.AndRespondWith("{\"error\":\"You already said that\"}", responseHeaders, HttpStatusCode.Forbidden, "");

#if NET_4_0 || SILVERLIGHT_5
            twitter.TimelineOperations.RetweetAsync(12345)
                .ContinueWith(task =>
                {
                    AssertTwitterApiException(task.Exception, "You already said that", TwitterApiError.OperationNotPermitted);
                })
                .Wait();
#else
            try
            {
                twitter.TimelineOperations.Retweet(12345);
                Assert.Fail("Exception expected");
            }
            catch (Exception ex)
            {
                AssertTwitterApiException(ex, "You already said that", TwitterApiError.OperationNotPermitted);
            }
#endif
        }

	    [Test]
	    public void Retweet_Forbidden() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/statuses/retweet/12345.json")
				.AndExpectMethod(HttpMethod.POST)
				.AndRespondWith("{\"error\":\"Forbidden\"}", responseHeaders, HttpStatusCode.Forbidden, "");

#if NET_4_0 || SILVERLIGHT_5
            twitter.TimelineOperations.RetweetAsync(12345)
                .ContinueWith(task =>
                {
                    AssertTwitterApiException(task.Exception, "Forbidden", TwitterApiError.OperationNotPermitted);
                })
                .Wait();
#else
            try
            {
                twitter.TimelineOperations.Retweet(12345);
                Assert.Fail("Exception expected");
            }
            catch (Exception ex)
            {
                AssertTwitterApiException(ex, "Forbidden", TwitterApiError.OperationNotPermitted);
            }
#endif
        }

	    [Test]
	    public void Retweet_SharingNotAllowed() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/statuses/retweet/12345.json")
				.AndExpectMethod(HttpMethod.POST)
                .AndRespondWith(JsonResource("Error_Sharing_NotAllowed"), responseHeaders, HttpStatusCode.Forbidden, "");

#if NET_4_0 || SILVERLIGHT_5
            twitter.TimelineOperations.RetweetAsync(12345)
                .ContinueWith(task =>
                {
                    AssertTwitterApiException(task.Exception, "sharing is not permissable for this status (Share validations failed)\nsharing is not permissable for this status (Share validations failed)\nsharing is not permissable for this status (Share validations failed)", TwitterApiError.OperationNotPermitted);
                })
                .Wait();
#else
            try
            {
                twitter.TimelineOperations.Retweet(12345);
                Assert.Fail("Exception expected");
            }
            catch (Exception ex)
            {
                AssertTwitterApiException(ex, "sharing is not permissable for this status (Share validations failed)\nsharing is not permissable for this status (Share validations failed)\nsharing is not permissable for this status (Share validations failed)", TwitterApiError.OperationNotPermitted);
            }
#endif
        }
	
	    [Test]
	    public void GetRetweets() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/statuses/retweets/42.json?count=100")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> timeline = twitter.TimelineOperations.GetRetweetsAsync(42L).Result;
#else
            IList<Tweet> timeline = twitter.TimelineOperations.GetRetweets(42L);
#endif
            AssertTimelineTweets(timeline);						
	    }

	    [Test]
	    public void GetRetweets_WithCount() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/statuses/retweets/42.json?count=12")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> timeline = twitter.TimelineOperations.GetRetweetsAsync(42L, 12).Result;
#else
            IList<Tweet> timeline = twitter.TimelineOperations.GetRetweets(42L, 12);
#endif
            AssertTimelineTweets(timeline);						
	    }
	
	    [Test]
	    public void GetFavorites() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/favorites/list.json")
				.AndExpectMethod(HttpMethod.GET)
				.AndRespondWith(JsonResource("Favorite"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> timeline = twitter.TimelineOperations.GetFavoritesAsync().Result;
#else
            IList<Tweet> timeline = twitter.TimelineOperations.GetFavorites();
#endif
            AssertTimelineTweets(timeline);
	    }

	    [Test]
	    public void GetFavorites_WithCount() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/favorites/list.json?count=50")
				.AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("Favorite"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> timeline = twitter.TimelineOperations.GetFavoritesAsync(50).Result;
#else
            IList<Tweet> timeline = twitter.TimelineOperations.GetFavorites(50);
#endif
            AssertTimelineTweets(timeline);
	    }

	    [Test]
	    public void AddToFavorites() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/favorites/create.json")
			    .AndExpectMethod(HttpMethod.POST)
                .AndExpectBody("id=42")
			    .AndRespondWith("{}", responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            twitter.TimelineOperations.AddToFavoritesAsync(42L).Wait();
#else
            twitter.TimelineOperations.AddToFavorites(42L);
#endif
        }

	    [Test]
	    public void RemoveFromFavorites() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/favorites/destroy.json")
			    .AndExpectMethod(HttpMethod.POST)
                .AndExpectBody("id=71")
			    .AndRespondWith("{}", responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            twitter.TimelineOperations.RemoveFromFavoritesAsync(71L).Wait();
#else
            twitter.TimelineOperations.RemoveFromFavorites(71L);
#endif
        }
    }
}
