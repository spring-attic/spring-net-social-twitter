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
                    AssertTwitterApiException(task.Exception, "Authorization is required for the operation, but the API binding was created without authorization.", TwitterApiError.NotAuthorized);
                })
                .Wait();
#else
            try
            {
                twitter.UserOperations.GetUserProfile();
                Assert.Fail("Exception expected");
            }
            catch (Exception ex)
            {
                AssertTwitterApiException(ex, "Authorization is required for the operation, but the API binding was created without authorization.", TwitterApiError.NotAuthorized);
            }
#endif
        }

        [Test]
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
                    AssertTwitterApiException(task.Exception, "Invalid / expired Token", TwitterApiError.NotAuthorized);
                })
                .Wait();
#else
            try
            {
                twitter.UserOperations.GetUserProfile();
                Assert.Fail("Exception expected");
            }
            catch (Exception ex)
            {
                AssertTwitterApiException(ex, "Invalid / expired Token", TwitterApiError.NotAuthorized);
            }
#endif
        }

        [Test]
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
                    AssertTwitterApiException(task.Exception, "The authorization has been revoked.", TwitterApiError.NotAuthorized);
                })
                .Wait();
#else
            try
            {
                twitter.UserOperations.GetUserProfile();
                Assert.Fail("Exception expected");
            }
            catch (Exception ex)
            {
                AssertTwitterApiException(ex, "The authorization has been revoked.", TwitterApiError.NotAuthorized);
            }
#endif
        }

        [Test]
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
                    AssertTwitterApiException(task.Exception, "The rate limit has been exceeded.", TwitterApiError.RateLimitExceeded);
                })
                .Wait();
#else
            try
            {
                twitter.SearchOperations.Search("#spring");
                Assert.Fail("Exception expected");
            }
            catch (Exception ex)
            {
                AssertTwitterApiException(ex, "The rate limit has been exceeded.", TwitterApiError.RateLimitExceeded);
            }
#endif
        }

        [Test]
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
                    AssertTwitterApiException(task.Exception, "Something is broken at Twitter. Please see http://dev.twitter.com/pages/support to report the issue.", TwitterApiError.Server);
                })
                .Wait();
#else
            try
            {
                twitter.TimelineOperations.GetHomeTimeline();
                Assert.Fail("Exception expected");
            }
            catch (Exception ex)
            {
                AssertTwitterApiException(ex, "Something is broken at Twitter. Please see http://dev.twitter.com/pages/support to report the issue.", TwitterApiError.Server);
            }
#endif
        }

        [Test]
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
                    AssertTwitterApiException(task.Exception, "Twitter is down or is being upgraded.", TwitterApiError.ServerDown);
                })
                .Wait();
#else
            try
            {
                twitter.TimelineOperations.GetHomeTimeline();
                Assert.Fail("Exception expected");
            }
            catch (Exception ex)
            {
                AssertTwitterApiException(ex, "Twitter is down or is being upgraded.", TwitterApiError.ServerDown);
            }
#endif
        }

        [Test]
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
                    AssertTwitterApiException(task.Exception, "Twitter is overloaded with requests. Try again later.", TwitterApiError.ServerOverloaded);
                })
                .Wait();
#else
            try
            {
                twitter.TimelineOperations.GetHomeTimeline();
                Assert.Fail("Exception expected");
            }
            catch (Exception ex)
            {
                AssertTwitterApiException(ex, "Twitter is overloaded with requests. Try again later.", TwitterApiError.ServerOverloaded);
            }
#endif
        }

        [Test]
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
                    AssertTwitterApiException(task.Exception, "Error consuming Twitter REST API.", TwitterApiError.Unknown);
                })
                .Wait();
#else
            try
            {
                twitter.TimelineOperations.GetHomeTimeline();
                Assert.Fail("Exception expected");
            }
            catch (Exception ex)
            {
                AssertTwitterApiException(ex, "Error consuming Twitter REST API.", TwitterApiError.Unknown);
            }
#endif
        }
    }
}
