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

using NUnit.Framework;
using Spring.Rest.Client.Testing;

using Spring.Http;

namespace Spring.Social.Twitter.Api.Impl
{
    /// <summary>
    /// Unit tests for the TwitterErrorHandler class.
    /// </summary>
    /// <author>Craig Walls</author>
    /// <author>Bruno Baia (.NET)</author>
    [TestFixture]
    public class ErrorHandlerTests : AbstractTwitterOperationsTests 
    {    
	    [Test]
#if NET_4_0 || SILVERLIGHT_5
#else
        [ExpectedException(typeof(NotAuthorizedException),
            ExpectedMessage = "Authorization is required for the operation, but the API binding was created without authorization.")]
#endif
	    public void MissingAccessToken() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/account/verify_credentials.json")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Error_No_Token"), responseHeaders, HttpStatusCode.Unauthorized, "");

#if NET_4_0 || SILVERLIGHT_5
            twitter.UserOperations.GetUserProfileAsync()
                .ContinueWith(task =>
                {
                    AssertAggregateException(task.Exception, typeof(NotAuthorizedException), "Authorization is required for the operation, but the API binding was created without authorization.");
                })
                .Wait();
#else
            twitter.UserOperations.GetUserProfile();
#endif
        }

        [Test]
#if NET_4_0 || SILVERLIGHT_5
#else
        [ExpectedException(typeof(NotAuthorizedException), ExpectedMessage = "Invalid / expired Token")]
#endif
        public void BadAccessToken() 
        {
            // token is fabricated or fails signature validation
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/account/verify_credentials.json")
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("Error_Invalid_Token"), responseHeaders, HttpStatusCode.Unauthorized, "");
            
#if NET_4_0 || SILVERLIGHT_5
            twitter.UserOperations.GetUserProfileAsync()
                .ContinueWith(task =>
                {
                    AssertAggregateException(task.Exception, typeof(NotAuthorizedException), "Invalid / expired Token");
                })
                .Wait();
#else
            twitter.UserOperations.GetUserProfile();
#endif
        }

        [Test]
#if NET_4_0 || SILVERLIGHT_5
#else
        [ExpectedException(typeof(NotAuthorizedException), ExpectedMessage = "The authorization has been revoked.")]
#endif
        public void RevokedToken() 
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/account/verify_credentials.json")
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("Error_Revoked_Token"), responseHeaders, HttpStatusCode.Unauthorized, "");

#if NET_4_0 || SILVERLIGHT_5
            twitter.UserOperations.GetUserProfileAsync()
                .ContinueWith(task =>
                {
                    AssertAggregateException(task.Exception, typeof(NotAuthorizedException), "The authorization has been revoked.");
                })
                .Wait();
#else
            twitter.UserOperations.GetUserProfile();		
#endif
        }

        [Test]
#if NET_4_0 || SILVERLIGHT_5
#else
        [ExpectedException(typeof(ApiException), ExpectedMessage = "The rate limit has been exceeded.")]
#endif
        public void EnhanceYourCalm() 
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://search.twitter.com/search.json?q=%23spring&rpp=50&page=1")
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("Error_Rate_Limited"), responseHeaders, (HttpStatusCode)420, "");		
            
#if NET_4_0 || SILVERLIGHT_5
            twitter.SearchOperations.SearchAsync("#spring")
                .ContinueWith(task =>
                {
                    AssertAggregateException(task.Exception, typeof(ApiException), "The rate limit has been exceeded.");
                })
                .Wait();
#else
            twitter.SearchOperations.Search("#spring");
#endif
        }

        [Test]
#if NET_4_0 || SILVERLIGHT_5
#else
        [ExpectedException(typeof(ServerException), 
            ExpectedMessage = "Something is broken at Twitter. Please see http://dev.twitter.com/pages/support to report the issue.")]
#endif
        public void TwitterIsBroken() 
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/statuses/home_timeline.json?page=1&count=20")
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith("", responseHeaders, HttpStatusCode.InternalServerError, "");

#if NET_4_0 || SILVERLIGHT_5
            twitter.TimelineOperations.GetHomeTimelineAsync()
                .ContinueWith(task =>
                {
                    AssertAggregateException(task.Exception, typeof(ServerException), "Something is broken at Twitter. Please see http://dev.twitter.com/pages/support to report the issue.");
                })
                .Wait();
#else
            twitter.TimelineOperations.GetHomeTimeline();
#endif
        }

        [Test]
#if NET_4_0 || SILVERLIGHT_5
#else
        [ExpectedException(typeof(ServerException), ExpectedMessage = "Twitter is down or is being upgraded.")]
#endif
        public void TwitterIsDownOrBeingUpgraded()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/statuses/home_timeline.json?page=1&count=20")
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith("", responseHeaders, HttpStatusCode.BadGateway, "");

#if NET_4_0 || SILVERLIGHT_5
            twitter.TimelineOperations.GetHomeTimelineAsync()
                .ContinueWith(task =>
                {
                    AssertAggregateException(task.Exception, typeof(ServerException), "Twitter is down or is being upgraded.");
                })
                .Wait();
#else
            twitter.TimelineOperations.GetHomeTimeline();
#endif
        }

        [Test]
#if NET_4_0 || SILVERLIGHT_5
#else
        [ExpectedException(typeof(ServerException), ExpectedMessage = "Twitter is overloaded with requests. Try again later.")]
#endif
        public void TwitterIsOverloaded()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/statuses/home_timeline.json?page=1&count=20")
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith("", responseHeaders, HttpStatusCode.ServiceUnavailable, "");

#if NET_4_0 || SILVERLIGHT_5
            twitter.TimelineOperations.GetHomeTimelineAsync()
                .ContinueWith(task =>
                {
                    AssertAggregateException(task.Exception, typeof(ServerException), "Twitter is overloaded with requests. Try again later.");
                })
                .Wait();
#else
            twitter.TimelineOperations.GetHomeTimeline();
#endif
        }

        [Test]
#if NET_4_0 || SILVERLIGHT_5
#else
        [ExpectedException(typeof(ApiException), ExpectedMessage = "Error consuming Twitter REST API.")]
#endif
        public void NonJsonErrorResponse()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/statuses/home_timeline.json?page=1&count=20")
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith("<h1>HTML response</h1>", responseHeaders, HttpStatusCode.BadRequest, "");

#if NET_4_0 || SILVERLIGHT_5
            twitter.TimelineOperations.GetHomeTimelineAsync()
                .ContinueWith(task =>
                {
                    AssertAggregateException(task.Exception, typeof(ApiException), "Error consuming Twitter REST API.");
                })
                .Wait();
#else
            twitter.TimelineOperations.GetHomeTimeline();
#endif
        }

        [Test]
#if NET_4_0 || SILVERLIGHT_5
#else
        [ExpectedException(typeof(Spring.Json.JsonException), ExpectedMessage = "Could not parse JSON string 'Unparseable {text}'.")]
#endif
        [Ignore("Need to handle cases where there isn't an error, but the body is unparseable.")]
        public void UnparseableSuccessResponse()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/statuses/home_timeline.json?page=1&count=20")
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith("Unparseable {text}", responseHeaders, HttpStatusCode.OK, "");

#if NET_4_0 || SILVERLIGHT_5
            twitter.TimelineOperations.GetHomeTimelineAsync()
                .ContinueWith(task =>
                {
                    AssertAggregateException(task.Exception, typeof(Spring.Json.JsonException), "Could not parse JSON string 'Unparseable {text}'.");
                })
                .Wait();
#else
            twitter.TimelineOperations.GetHomeTimeline();
#endif
        }
    }
}
