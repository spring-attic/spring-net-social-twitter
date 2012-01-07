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
	    public void GetPublicTimelineAsync() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/statuses/public_timeline.json")
				.AndExpectMethod(HttpMethod.GET)
				.AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    IList<Tweet> timeline = twitter.TimelineOperations.GetPublicTimelineAsync().Result;
#else
            IList<Tweet> timeline = twitter.TimelineOperations.GetPublicTimeline();
#endif
            AssertTimelineTweets(timeline);
	    }

	    [Test]
	    public void GetHomeTimelineAsync() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/statuses/home_timeline.json?page=1&count=20")
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
	    public void GetHomeTimeline_Paged() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/statuses/home_timeline.json?page=3&count=100")
				.AndExpectMethod(HttpMethod.GET)
				.AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> timeline = twitter.TimelineOperations.GetHomeTimelineAsync(3, 100).Result;
#else
            IList<Tweet> timeline = twitter.TimelineOperations.GetHomeTimeline(3, 100);
#endif
            AssertTimelineTweets(timeline);
	    }

	    [Test]
	    public void GetHomeTimeline_Paged_WithSinceIdAndMaxId() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/statuses/home_timeline.json?page=3&count=100&since_id=1234567&max_id=7654321")
				.AndExpectMethod(HttpMethod.GET)
				.AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> timeline = twitter.TimelineOperations.GetHomeTimelineAsync(3, 100, 1234567, 7654321).Result;
#else
            IList<Tweet> timeline = twitter.TimelineOperations.GetHomeTimeline(3, 100, 1234567, 7654321);
#endif
            AssertTimelineTweets(timeline);
	    }

	    [Test]
        [ExpectedException(typeof(NotAuthorizedException), 
            ExpectedMessage = "Authorization is required for the operation, but the API binding was created without authorization.")]
	    public void GetHomeTimeline_Unauthorized()
        {
#if NET_4_0 || SILVERLIGHT_5
            unauthorizedTwitter.TimelineOperations.GetHomeTimelineAsync().Wait();
#else
            unauthorizedTwitter.TimelineOperations.GetHomeTimeline();
#endif
        }

	    [Test]
	    public void GetUserTimelineAsync() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/statuses/user_timeline.json?page=1&count=20")
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
	    public void GetUserTimeline_Paged() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/statuses/user_timeline.json?page=2&count=15")
				.AndExpectMethod(HttpMethod.GET)
				.AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> timeline = twitter.TimelineOperations.GetUserTimelineAsync(2, 15).Result;
#else
            IList<Tweet> timeline = twitter.TimelineOperations.GetUserTimeline(2, 15);
#endif
            AssertTimelineTweets(timeline);
	    }

	    [Test]
	    public void GetUserTimeline_Paged_WithSinceIdAndMaxId() {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/statuses/user_timeline.json?page=2&count=15&since_id=123456&max_id=654321")
				.AndExpectMethod(HttpMethod.GET)
				.AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> timeline = twitter.TimelineOperations.GetUserTimelineAsync(2, 15, 123456, 654321).Result;
#else
            IList<Tweet> timeline = twitter.TimelineOperations.GetUserTimeline(2, 15, 123456, 654321);
#endif
            AssertTimelineTweets(timeline);
	    }

	    [Test]
        [ExpectedException(typeof(NotAuthorizedException),
            ExpectedMessage = "Authorization is required for the operation, but the API binding was created without authorization.")]
	    public void GetUserTimeline_Unauthorized()
        {
#if NET_4_0 || SILVERLIGHT_5
            unauthorizedTwitter.TimelineOperations.GetUserTimelineAsync().Wait();
#else
            unauthorizedTwitter.TimelineOperations.GetUserTimeline();
#endif
        }
	
	    [Test]
	    public void GetUserTimeline_ForScreenName() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/statuses/user_timeline.json?page=1&count=20&screen_name=habuma")
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
	    public void GetUserTimeline_ForScreenName_Paged() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/statuses/user_timeline.json?page=6&count=24&screen_name=habuma")
				.AndExpectMethod(HttpMethod.GET)
				.AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> timeline = twitter.TimelineOperations.GetUserTimelineAsync("habuma", 6, 24).Result;
#else
            IList<Tweet> timeline = twitter.TimelineOperations.GetUserTimeline("habuma", 6, 24);
#endif
            AssertTimelineTweets(timeline);
	    }

	    [Test]
	    public void GetUserTimeline_ForScreenName_Paged_WithSinceIdAndMaxId() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/statuses/user_timeline.json?page=6&count=24&since_id=112233&max_id=332211&screen_name=habuma")
				.AndExpectMethod(HttpMethod.GET)
				.AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> timeline = twitter.TimelineOperations.GetUserTimelineAsync("habuma", 6, 24, 112233, 332211).Result;
#else
            IList<Tweet> timeline = twitter.TimelineOperations.GetUserTimeline("habuma", 6, 24, 112233, 332211);
#endif
            AssertTimelineTweets(timeline);
	    }

	    [Test]
	    public void GetUserTimeline_ForUserId() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/statuses/user_timeline.json?page=1&count=20&user_id=12345")
				.AndExpectMethod(HttpMethod.GET)
				.AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> timeline = twitter.TimelineOperations.GetUserTimelineAsync(12345).Result;
#else
            IList<Tweet> timeline = twitter.TimelineOperations.GetUserTimeline(12345);
#endif
            AssertTimelineTweets(timeline);
	    }

	    [Test]
	    public void GetUserTimeline_ForUserId_Paged() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/statuses/user_timeline.json?page=6&count=24&user_id=12345")
				.AndExpectMethod(HttpMethod.GET)
				.AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> timeline = twitter.TimelineOperations.GetUserTimelineAsync(12345, 6, 24).Result;
#else
            IList<Tweet> timeline = twitter.TimelineOperations.GetUserTimeline(12345, 6, 24);
#endif
            AssertTimelineTweets(timeline);
	    }

	    [Test]
	    public void GetUserTimeline_ForUserId_Paged_WithSinceIdAndMaxId() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/statuses/user_timeline.json?page=6&count=24&since_id=112233&max_id=332211&user_id=12345")
				.AndExpectMethod(HttpMethod.GET)
				.AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> timeline = twitter.TimelineOperations.GetUserTimelineAsync(12345, 6, 24, 112233, 332211).Result;
#else
            IList<Tweet> timeline = twitter.TimelineOperations.GetUserTimeline(12345, 6, 24, 112233, 332211);
#endif
            AssertTimelineTweets(timeline);
	    }

	    [Test]
	    public void GetMentions() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/statuses/mentions.json?page=1&count=20")
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
	    public void GetMentions_Paged() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/statuses/mentions.json?page=3&count=50")
				.AndExpectMethod(HttpMethod.GET)
				.AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> mentions = twitter.TimelineOperations.GetMentionsAsync(3, 50).Result;
#else
            IList<Tweet> mentions = twitter.TimelineOperations.GetMentions(3, 50);
#endif
            AssertTimelineTweets(mentions);
	    }

	    [Test]
	    public void GetMentions_Paged_WithSinceIdAndMaxId() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/statuses/mentions.json?page=3&count=50&since_id=112233&max_id=332211")
				.AndExpectMethod(HttpMethod.GET)
				.AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> mentions = twitter.TimelineOperations.GetMentionsAsync(3, 50, 112233, 332211).Result;
#else
            IList<Tweet> mentions = twitter.TimelineOperations.GetMentions(3, 50, 112233, 332211);
#endif
            AssertTimelineTweets(mentions);
	    }

	    [Test]
        [ExpectedException(typeof(NotAuthorizedException),
            ExpectedMessage = "Authorization is required for the operation, but the API binding was created without authorization.")]
	    public void GetMentions_Unauthorized()
        {
#if NET_4_0 || SILVERLIGHT_5
            unauthorizedTwitter.TimelineOperations.GetMentionsAsync().Wait();
#else
            unauthorizedTwitter.TimelineOperations.GetMentions();
#endif
        }

	    [Test]
	    public void GetRetweetedByMe() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/statuses/retweeted_by_me.json?page=1&count=20")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> timeline = twitter.TimelineOperations.GetRetweetedByMeAsync().Result;
#else
            IList<Tweet> timeline = twitter.TimelineOperations.GetRetweetedByMe();
#endif
            AssertTimelineTweets(timeline);		
	    }

	    [Test]
	    public void GetRetweetedByMe_Paged() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/statuses/retweeted_by_me.json?page=5&count=42")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> timeline = twitter.TimelineOperations.GetRetweetedByMeAsync(5, 42).Result;
#else
            IList<Tweet> timeline = twitter.TimelineOperations.GetRetweetedByMe(5, 42);
#endif
            AssertTimelineTweets(timeline);		
	    }

	    [Test]
	    public void GetRetweetedByMe_Paged_WithSinceIdAndMaxId() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/statuses/retweeted_by_me.json?page=5&count=42&since_id=24680&max_id=86420")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> timeline = twitter.TimelineOperations.GetRetweetedByMeAsync(5, 42, 24680, 86420).Result;
#else
            IList<Tweet> timeline = twitter.TimelineOperations.GetRetweetedByMe(5, 42, 24680, 86420);
#endif
            AssertTimelineTweets(timeline);		
	    }

	    [Test]
	    public void GetRetweetedByUser_UserId() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/statuses/retweeted_by_user.json?page=1&count=20&user_id=12345678")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> timeline = twitter.TimelineOperations.GetRetweetedByUserAsync(12345678).Result;
#else
            IList<Tweet> timeline = twitter.TimelineOperations.GetRetweetedByUser(12345678);
#endif
            AssertTimelineTweets(timeline);		
	    }

	    [Test]
	    public void GetRetweetedByUser_UserId_Paged() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/statuses/retweeted_by_user.json?page=5&count=42&user_id=12345678")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> timeline = twitter.TimelineOperations.GetRetweetedByUserAsync(12345678, 5, 42).Result;
#else
            IList<Tweet> timeline = twitter.TimelineOperations.GetRetweetedByUser(12345678, 5, 42);
#endif
            AssertTimelineTweets(timeline);		
	    }

	    [Test]
	    public void GetRetweetedByUser_UserId_WithSinceIdAndMaxId() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/statuses/retweeted_by_user.json?page=5&count=42&since_id=24680&max_id=86420&user_id=12345678")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> timeline = twitter.TimelineOperations.GetRetweetedByUserAsync(12345678, 5, 42, 24680, 86420).Result;
#else
            IList<Tweet> timeline = twitter.TimelineOperations.GetRetweetedByUser(12345678, 5, 42, 24680, 86420);
#endif
            AssertTimelineTweets(timeline);		
	    }

	    [Test]
	    public void GetRetweetedByUser_ScreenName() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/statuses/retweeted_by_user.json?page=1&count=20&screen_name=habuma")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> timeline = twitter.TimelineOperations.GetRetweetedByUserAsync("habuma").Result;
#else
            IList<Tweet> timeline = twitter.TimelineOperations.GetRetweetedByUser("habuma");
#endif
            AssertTimelineTweets(timeline);		
	    }

	    [Test]
	    public void GetRetweetedByUser_ScreenName_Paged() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/statuses/retweeted_by_user.json?page=5&count=42&screen_name=habuma")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> timeline = twitter.TimelineOperations.GetRetweetedByUserAsync("habuma", 5, 42).Result;
#else
            IList<Tweet> timeline = twitter.TimelineOperations.GetRetweetedByUser("habuma", 5, 42);
#endif
            AssertTimelineTweets(timeline);		
	    }

	    [Test]
	    public void GetRetweetedByUser_ScreenName_WithSinceIdAndMaxId()
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/statuses/retweeted_by_user.json?page=5&count=42&since_id=24680&max_id=86420&screen_name=habuma")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> timeline = twitter.TimelineOperations.GetRetweetedByUserAsync("habuma", 5, 42, 24680, 86420).Result;
#else
            IList<Tweet> timeline = twitter.TimelineOperations.GetRetweetedByUser("habuma", 5, 42, 24680, 86420);
#endif
            AssertTimelineTweets(timeline);		
	    }
	
	    [Test]
        [ExpectedException(typeof(NotAuthorizedException),
            ExpectedMessage = "Authorization is required for the operation, but the API binding was created without authorization.")]
	    public void GetRetweetedByMe_Unauthorized()
        {
#if NET_4_0 || SILVERLIGHT_5
            unauthorizedTwitter.TimelineOperations.GetRetweetedByMeAsync().Wait();
#else
            unauthorizedTwitter.TimelineOperations.GetRetweetedByMe();
#endif
        }
	
	    [Test]
	    public void GetRetweetedToMe() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/statuses/retweeted_to_me.json?page=1&count=20")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> timeline = twitter.TimelineOperations.GetRetweetedToMeAsync().Result;
#else
            IList<Tweet> timeline = twitter.TimelineOperations.GetRetweetedToMe();
#endif
            AssertTimelineTweets(timeline);				
	    }

	    [Test]
	    public void GetRetweetedToMe_Paged() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/statuses/retweeted_to_me.json?page=4&count=40")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> timeline = twitter.TimelineOperations.GetRetweetedToMeAsync(4, 40).Result;
#else
            IList<Tweet> timeline = twitter.TimelineOperations.GetRetweetedToMe(4, 40);
#endif
            AssertTimelineTweets(timeline);				
	    }

	    [Test]
	    public void GetRetweetedToMe_Paged_WithSinceIdAndMaxId() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/statuses/retweeted_to_me.json?page=4&count=40&since_id=12345&max_id=54321")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> timeline = twitter.TimelineOperations.GetRetweetedToMeAsync(4, 40, 12345, 54321).Result;
#else
            IList<Tweet> timeline = twitter.TimelineOperations.GetRetweetedToMe(4, 40, 12345, 54321);
#endif
            AssertTimelineTweets(timeline);				
	    }

	    [Test]
        [ExpectedException(typeof(NotAuthorizedException),
            ExpectedMessage = "Authorization is required for the operation, but the API binding was created without authorization.")]
	    public void GetRetweetedToMe_Unauthorized()
        {
#if NET_4_0 || SILVERLIGHT_5
            unauthorizedTwitter.TimelineOperations.GetRetweetedToMeAsync().Wait();
#else
            unauthorizedTwitter.TimelineOperations.GetRetweetedToMe();
#endif
        }

	    [Test]
	    public void GetRetweetedToUser_UserId() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/statuses/retweeted_to_user.json?page=1&count=20&user_id=12345678")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> timeline = twitter.TimelineOperations.GetRetweetedToUserAsync(12345678).Result;
#else
            IList<Tweet> timeline = twitter.TimelineOperations.GetRetweetedToUser(12345678);
#endif
            AssertTimelineTweets(timeline);		
	    }

	    [Test]
	    public void GetRetweetedToUser_UserId_Paged() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/statuses/retweeted_to_user.json?page=5&count=42&user_id=12345678")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> timeline = twitter.TimelineOperations.GetRetweetedToUserAsync(12345678, 5, 42).Result;
#else
            IList<Tweet> timeline = twitter.TimelineOperations.GetRetweetedToUser(12345678, 5, 42);
#endif
            AssertTimelineTweets(timeline);		
	    }

	    [Test]
	    public void GetRetweetedToUser_UserId_WithSinceIdAndMaxId() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/statuses/retweeted_to_user.json?page=5&count=42&since_id=24680&max_id=86420&user_id=12345678")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> timeline = twitter.TimelineOperations.GetRetweetedToUserAsync(12345678, 5, 42, 24680, 86420).Result;
#else
            IList<Tweet> timeline = twitter.TimelineOperations.GetRetweetedToUser(12345678, 5, 42, 24680, 86420);
#endif
            AssertTimelineTweets(timeline);		
	    }

	    [Test]
	    public void GetRetweetedToUser_ScreenName() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/statuses/retweeted_to_user.json?page=1&count=20&screen_name=habuma")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> timeline = twitter.TimelineOperations.GetRetweetedToUserAsync("habuma").Result;
#else
            IList<Tweet> timeline = twitter.TimelineOperations.GetRetweetedToUser("habuma");
#endif
            AssertTimelineTweets(timeline);		
	    }

	    [Test]
	    public void GetRetweetedToUser_ScreenName_Paged() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/statuses/retweeted_to_user.json?page=5&count=42&screen_name=habuma")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> timeline = twitter.TimelineOperations.GetRetweetedToUserAsync("habuma", 5, 42).Result;
#else
            IList<Tweet> timeline = twitter.TimelineOperations.GetRetweetedToUser("habuma", 5, 42);
#endif
            AssertTimelineTweets(timeline);		
	    }

	    [Test]
	    public void GetRetweetedToUser_ScreenName_WithSinceIdAndMaxId() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/statuses/retweeted_to_user.json?page=5&count=42&since_id=24680&max_id=86420&screen_name=habuma")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> timeline = twitter.TimelineOperations.GetRetweetedToUserAsync("habuma", 5, 42, 24680, 86420).Result;
#else
            IList<Tweet> timeline = twitter.TimelineOperations.GetRetweetedToUser("habuma", 5, 42, 24680, 86420);
#endif
            AssertTimelineTweets(timeline);		
	    }
	
	    [Test]
	    public void GetRetweetsOfMe() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/statuses/retweets_of_me.json?page=1&count=20")
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
	    public void GetRetweetsOfMe_Paged() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/statuses/retweets_of_me.json?page=7&count=25")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> timeline = twitter.TimelineOperations.GetRetweetsOfMeAsync(7, 25).Result;
#else
            IList<Tweet> timeline = twitter.TimelineOperations.GetRetweetsOfMe(7, 25);
#endif
            AssertTimelineTweets(timeline);				
	    }

	    [Test]
	    public void GetRetweetsOfMe_Paged_WithSinceIdAndMaxId() 
        {
		    mockServer.ExpectNewRequest().AndExpectUri("https://api.twitter.com/1/statuses/retweets_of_me.json?page=7&count=25&since_id=2345&max_id=3456")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> timeline = twitter.TimelineOperations.GetRetweetsOfMeAsync(7, 25, 2345, 3456).Result;
#else
            IList<Tweet> timeline = twitter.TimelineOperations.GetRetweetsOfMe(7, 25, 2345, 3456);
#endif
            AssertTimelineTweets(timeline);				
	    }

	    [Test]
        [ExpectedException(typeof(NotAuthorizedException), 
            ExpectedMessage = "Authorization is required for the operation, but the API binding was created without authorization.")]
	    public void GetRetweetsOfMe_Unauthorized()
        {
#if NET_4_0 || SILVERLIGHT_5
            unauthorizedTwitter.TimelineOperations.GetRetweetsOfMeAsync().Wait();
#else
            unauthorizedTwitter.TimelineOperations.GetRetweetsOfMe();
#endif
        }
	
	    [Test]
	    public void GetStatus() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/statuses/show/12345.json")
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
                .AndExpectUri("https://api.twitter.com/1/statuses/update.json")
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
        [ExpectedException(typeof(NotAuthorizedException),
            ExpectedMessage = "Authorization is required for the operation, but the API binding was created without authorization.")]
	    public void UpdateStatus_Unauthorized()
        {
#if NET_4_0 || SILVERLIGHT_5
            unauthorizedTwitter.TimelineOperations.UpdateStatusAsync("Shouldn't work").Wait();
#else
            unauthorizedTwitter.TimelineOperations.UpdateStatus("Shouldn't work");
#endif
        }

	    [Test]
	    public void UpdateStatus_WithImage() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://upload.twitter.com/1/statuses/update_with_media.json")
				.AndExpectMethod(HttpMethod.POST)
                .AndExpectHeaderContains("Content-Type", MediaType.MULTIPART_FORM_DATA.ToString() + ";boundary=")
                .AndExpect(BodyContains("Content-Disposition: form-data; name=\"status\""))
                .AndExpect(BodyContains("Content-Type: text/plain;charset=ISO-8859-1"))
                .AndExpect(BodyContains("Test Message"))
                .AndExpect(BodyContains("Content-Disposition: form-data; name=\"media\"; filename=\"Logo.png\""))
                .AndExpect(BodyContains("Content-Type: image/png"))
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
                .AndExpectUri("https://api.twitter.com/1/statuses/update.json")
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
		    mockServer.ExpectNewRequest().AndExpectUri("https://api.twitter.com/1/statuses/update.json")
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
                .AndExpectUri("https://api.twitter.com/1/statuses/update.json")
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
                .AndExpectUri("https://api.twitter.com/1/statuses/update.json")
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
                .AndExpectUri("https://upload.twitter.com/1/statuses/update_with_media.json")
                .AndExpectMethod(HttpMethod.POST)
                .AndExpectHeaderContains("Content-Type", MediaType.MULTIPART_FORM_DATA.ToString() + ";boundary=")
                .AndExpect(BodyContains("Content-Disposition: form-data; name=\"status\""))
                .AndExpect(BodyContains("Content-Type: text/plain;charset=ISO-8859-1"))
                .AndExpect(BodyContains("Test Message"))
                .AndExpect(BodyContains("Content-Disposition: form-data; name=\"lat\""))
                .AndExpect(BodyContains("123.1"))
                .AndExpect(BodyContains("Content-Disposition: form-data; name=\"long\""))
                .AndExpect(BodyContains("-111.2"))
                .AndExpect(BodyContains("Content-Disposition: form-data; name=\"media\"; filename=\"Logo.png\""))
                .AndExpect(BodyContains("Content-Type: image/png"))
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
        [ExpectedException(typeof(NotAuthorizedException),
            ExpectedMessage = "Authorization is required for the operation, but the API binding was created without authorization.")]
	    public void UpdateStatus_WithLocation_Unauthorized() 
        {
		    StatusDetails details = new StatusDetails();
            details.Latitude = 123.1f;
            details.Longitude = -111.2f;
#if NET_4_0 || SILVERLIGHT_5
            unauthorizedTwitter.TimelineOperations.UpdateStatusAsync("Test Message", details).Wait();
#else
            unauthorizedTwitter.TimelineOperations.UpdateStatus("Test Message", details);
#endif
        }

	    [Test]
#if NET_4_0 || SILVERLIGHT_5
#else
        [ExpectedException(typeof(OperationNotPermittedException), ExpectedMessage = "You already said that")]
#endif
	    public void UpdateStatus_DuplicateTweet() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/statuses/update.json")
				.AndExpectMethod(HttpMethod.POST)
				.AndExpectBody("status=Test+Message")
				.AndRespondWith("{\"error\":\"You already said that\"}", responseHeaders, HttpStatusCode.Forbidden, "");

#if NET_4_0 || SILVERLIGHT_5
            twitter.TimelineOperations.UpdateStatusAsync("Test Message")
                .ContinueWith(task =>
                {
                    AssertAggregateException(task.Exception, typeof(OperationNotPermittedException), "You already said that");
                })
                .Wait();
#else
            twitter.TimelineOperations.UpdateStatus("Test Message");
#endif
        }
	
	    [Test]
#if NET_4_0 || SILVERLIGHT_5
#else
        [ExpectedException(typeof(OperationNotPermittedException), ExpectedMessage = "Status is over 140 characters.")]
#endif
	    public void UpdateStatus_TweetTooLong() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/statuses/update.json")
			    .AndExpectMethod(HttpMethod.POST)
			    .AndExpectBody("status=Really+long+message")
			    .AndRespondWith("{\"error\":\"Status is over 140 characters.\"}", responseHeaders, HttpStatusCode.Forbidden, "");

#if NET_4_0 || SILVERLIGHT_5
            twitter.TimelineOperations.UpdateStatusAsync("Really long message")
                .ContinueWith(task =>
                {
                    AssertAggregateException(task.Exception, typeof(OperationNotPermittedException), "Status is over 140 characters.");
                })
                .Wait();
#else
            twitter.TimelineOperations.UpdateStatus("Really long message");
#endif
        }
	
	    [Test]
#if NET_4_0 || SILVERLIGHT_5
#else
        [ExpectedException(typeof(OperationNotPermittedException), ExpectedMessage = "Forbidden")]
#endif
	    public void UpdateStatus_Forbidden() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/statuses/update.json")
				.AndExpectMethod(HttpMethod.POST)
				.AndExpectBody("status=Test+Message")
				.AndRespondWith("{\"error\":\"Forbidden\"}", responseHeaders, HttpStatusCode.Forbidden, "");

#if NET_4_0 || SILVERLIGHT_5
            twitter.TimelineOperations.UpdateStatusAsync("Test Message")
                .ContinueWith(task =>
                {
                    AssertAggregateException(task.Exception, typeof(OperationNotPermittedException), "Forbidden");
                })
                .Wait();
#else
            twitter.TimelineOperations.UpdateStatus("Test Message");
#endif
        }

	    [Test]
	    public void DeleteStatus() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/statuses/destroy/12345.json")
			    .AndExpectMethod(HttpMethod.DELETE)
			    .AndRespondWith("{}", responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            twitter.TimelineOperations.DeleteStatusAsync(12345L).Wait();
#else
            twitter.TimelineOperations.DeleteStatus(12345L);
#endif
        }
	
	    [Test]
        [ExpectedException(typeof(NotAuthorizedException),
            ExpectedMessage = "Authorization is required for the operation, but the API binding was created without authorization.")]
	    public void DeleteStatus_Unauthorized()
        {
#if NET_4_0 || SILVERLIGHT_5
            unauthorizedTwitter.TimelineOperations.DeleteStatusAsync(12345L).Wait();
#else
            unauthorizedTwitter.TimelineOperations.DeleteStatus(12345L);
#endif
        }
	
	    [Test]
	    public void Retweet() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/statuses/retweet/12345.json")
				.AndExpectMethod(HttpMethod.POST)
				.AndRespondWith("{}", responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            twitter.TimelineOperations.RetweetAsync(12345).Wait();
#else
            twitter.TimelineOperations.Retweet(12345);
#endif
        }
	
	    [Test]
        [ExpectedException(typeof(NotAuthorizedException),
            ExpectedMessage = "Authorization is required for the operation, but the API binding was created without authorization.")]
	    public void Retweet_Unauthorized()
        {
#if NET_4_0 || SILVERLIGHT_5
            unauthorizedTwitter.TimelineOperations.RetweetAsync(12345L).Wait();
#else
            unauthorizedTwitter.TimelineOperations.Retweet(12345L);
#endif
        }

	    [Test]
#if NET_4_0 || SILVERLIGHT_5
#else
        [ExpectedException(typeof(OperationNotPermittedException), ExpectedMessage = "You already said that")]
#endif
	    public void Retweet_DuplicateTweet() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/statuses/retweet/12345.json")
				.AndExpectMethod(HttpMethod.POST)
				.AndRespondWith("{\"error\":\"You already said that\"}", responseHeaders, HttpStatusCode.Forbidden, "");

#if NET_4_0 || SILVERLIGHT_5
            twitter.TimelineOperations.RetweetAsync(12345)
                .ContinueWith(task =>
                {
                    AssertAggregateException(task.Exception, typeof(OperationNotPermittedException), "You already said that");
                })
                .Wait();
#else
            twitter.TimelineOperations.Retweet(12345);
#endif
        }

	    [Test]
#if NET_4_0 || SILVERLIGHT_5
#else
        [ExpectedException(typeof(OperationNotPermittedException), ExpectedMessage = "Forbidden")]
#endif
	    public void Retweet_Forbidden() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/statuses/retweet/12345.json")
				.AndExpectMethod(HttpMethod.POST)
				.AndRespondWith("{\"error\":\"Forbidden\"}", responseHeaders, HttpStatusCode.Forbidden, "");

#if NET_4_0 || SILVERLIGHT_5
            twitter.TimelineOperations.RetweetAsync(12345)
                .ContinueWith(task =>
                {
                    AssertAggregateException(task.Exception, typeof(OperationNotPermittedException), "Forbidden");
                })
                .Wait();
#else
            twitter.TimelineOperations.Retweet(12345);
#endif
        }

	    [Test]
#if NET_4_0 || SILVERLIGHT_5
#else
        [ExpectedException(typeof(OperationNotPermittedException), ExpectedMessage = "sharing is not permissable for this status (Share validations failed)\nsharing is not permissable for this status (Share validations failed)\nsharing is not permissable for this status (Share validations failed)")]
#endif
	    public void Retweet_SharingNotAllowed() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/statuses/retweet/12345.json")
				.AndExpectMethod(HttpMethod.POST)
                .AndRespondWith(JsonResource("Error_Sharing_NotAllowed"), responseHeaders, HttpStatusCode.Forbidden, "");

#if NET_4_0 || SILVERLIGHT_5
            twitter.TimelineOperations.RetweetAsync(12345)
                .ContinueWith(task =>
                {
                    AssertAggregateException(task.Exception, typeof(OperationNotPermittedException), "sharing is not permissable for this status (Share validations failed)\nsharing is not permissable for this status (Share validations failed)\nsharing is not permissable for this status (Share validations failed)");
                })
                .Wait();
#else
            twitter.TimelineOperations.Retweet(12345);
#endif
        }
	
	    [Test]
	    public void GetRetweets() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/statuses/retweets/42.json?count=100")
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
                .AndExpectUri("https://api.twitter.com/1/statuses/retweets/42.json?count=12")
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
	    public void GetRetweetedBy() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/statuses/42/retweeted_by.json?page=1&count=100")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Retweeted_By"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<TwitterProfile> retweetedBy = twitter.TimelineOperations.GetRetweetedByAsync(42L).Result;
#else
            IList<TwitterProfile> retweetedBy = twitter.TimelineOperations.GetRetweetedBy(42L);
#endif
            Assert.AreEqual(2, retweetedBy.Count);
		    Assert.AreEqual("royclarkson", retweetedBy[0].ScreenName);
		    Assert.AreEqual("kdonald", retweetedBy[1].ScreenName);
	    }

	    [Test]
	    public void GetRetweetedBy_Paged() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/statuses/42/retweeted_by.json?page=2&count=25")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Retweeted_By"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<TwitterProfile> retweetedBy = twitter.TimelineOperations.GetRetweetedByAsync(42L, 2, 25).Result;
#else
            IList<TwitterProfile> retweetedBy = twitter.TimelineOperations.GetRetweetedBy(42L, 2, 25);
#endif
            Assert.AreEqual(2, retweetedBy.Count);
		    Assert.AreEqual("royclarkson", retweetedBy[0].ScreenName);
		    Assert.AreEqual("kdonald", retweetedBy[1].ScreenName);
	    }

	    [Test]
	    public void GetRetweetedByIds() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/statuses/42/retweeted_by/ids.json?page=1&count=100")
			    .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("Retweeted_By_Ids"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<long> retweetedByIds = twitter.TimelineOperations.GetRetweetedByIdsAsync(42L).Result;
#else
            IList<long> retweetedByIds = twitter.TimelineOperations.GetRetweetedByIds(42L);
#endif
            Assert.AreEqual(3, retweetedByIds.Count);
		    Assert.AreEqual(12345, retweetedByIds[0]);
		    Assert.AreEqual(9223372036854775807L, retweetedByIds[1]);
		    Assert.AreEqual(34567, retweetedByIds[2]);
	    }

	    [Test]
	    public void GetRetweetedByIds_Paged() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/statuses/42/retweeted_by/ids.json?page=3&count=60")
			    .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("Retweeted_By_Ids"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<long> retweetedByIds = twitter.TimelineOperations.GetRetweetedByIdsAsync(42L, 3, 60).Result;
#else
            IList<long> retweetedByIds = twitter.TimelineOperations.GetRetweetedByIds(42L, 3, 60);
#endif
            Assert.AreEqual(3, retweetedByIds.Count);
		    Assert.AreEqual(12345, retweetedByIds[0]);
		    Assert.AreEqual(9223372036854775807L, retweetedByIds[1]);
		    Assert.AreEqual(34567, retweetedByIds[2]);
	    }

	    [Test]
        [ExpectedException(typeof(NotAuthorizedException),
            ExpectedMessage = "Authorization is required for the operation, but the API binding was created without authorization.")]
	    public void GetRetweetedByIds_Unauthorized()
        {
#if NET_4_0 || SILVERLIGHT_5
            unauthorizedTwitter.TimelineOperations.GetRetweetedByIdsAsync(12345L).Wait();
#else
            unauthorizedTwitter.TimelineOperations.GetRetweetedByIds(12345L);
#endif
        }
	
	    [Test]
	    public void GetFavorites() 
        {
		    // Note: The documentation for /favorites.json doesn't list the count parameter, but it works anyway.
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/favorites.json?page=1&count=20")
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
	    public void GetFavorites_Paged() 
        {
		    // Note: The documentation for /favorites.json doesn't list the count parameter, but it works anyway.
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/favorites.json?page=3&count=50")
				.AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("Favorite"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> timeline = twitter.TimelineOperations.GetFavoritesAsync(3, 50).Result;
#else
            IList<Tweet> timeline = twitter.TimelineOperations.GetFavorites(3, 50);
#endif
            AssertTimelineTweets(timeline);
	    }

	    [Test]
        [ExpectedException(typeof(NotAuthorizedException),
            ExpectedMessage = "Authorization is required for the operation, but the API binding was created without authorization.")]
	    public void GetFavorites_Unauthorized()
        {
#if NET_4_0 || SILVERLIGHT_5
            unauthorizedTwitter.TimelineOperations.GetFavoritesAsync().Wait();
#else
            unauthorizedTwitter.TimelineOperations.GetFavorites();
#endif
        }

	    [Test]
	    public void AddToFavorites() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/favorites/create/42.json")
			    .AndExpectMethod(HttpMethod.POST)
			    .AndRespondWith("{}", responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            twitter.TimelineOperations.AddToFavoritesAsync(42L).Wait();
#else
            twitter.TimelineOperations.AddToFavorites(42L);
#endif
        }
	
	    [Test]
        [ExpectedException(typeof(NotAuthorizedException),
            ExpectedMessage = "Authorization is required for the operation, but the API binding was created without authorization.")]
	    public void AddToFavorites_Unauthorized()
        {
#if NET_4_0 || SILVERLIGHT_5
            unauthorizedTwitter.TimelineOperations.AddToFavoritesAsync(12345L).Wait();
#else
            unauthorizedTwitter.TimelineOperations.AddToFavorites(12345L);
#endif
        }

	    [Test]
	    public void RemoveFromFavorites() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/favorites/destroy/71.json")
			    .AndExpectMethod(HttpMethod.POST)
			    .AndRespondWith("{}", responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            twitter.TimelineOperations.RemoveFromFavoritesAsync(71L).Wait();
#else
            twitter.TimelineOperations.RemoveFromFavorites(71L);
#endif
        }
	
	    [Test]
        [ExpectedException(typeof(NotAuthorizedException),
            ExpectedMessage = "Authorization is required for the operation, but the API binding was created without authorization.")]
	    public void RemoveFromFavorites_Unauthorized()
        {
#if NET_4_0 || SILVERLIGHT_5
            unauthorizedTwitter.TimelineOperations.RemoveFromFavoritesAsync(12345L).Wait();
#else
            unauthorizedTwitter.TimelineOperations.RemoveFromFavorites(12345L);
#endif
        }
    }
}
