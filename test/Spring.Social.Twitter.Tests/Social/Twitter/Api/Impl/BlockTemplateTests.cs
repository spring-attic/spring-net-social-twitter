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
    /// Unit tests for the BlockTemplate class.
    /// </summary>
    /// <author>Craig Walls</author>
    /// <author>Bruno Baia (.NET)</author>
    [TestFixture]
    public class BlockTemplateTests : AbstractTwitterOperationsTests
    {
        [Test]
        public void Block_UserId()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/blocks/create.json")
                .AndExpectMethod(HttpMethod.POST)
                .AndExpectBody("user_id=12345")
                .AndRespondWith(JsonResource("Twitter_Profile"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            TwitterProfile blockedUser = twitter.BlockOperations.BlockAsync(12345).Result;
#else
            TwitterProfile blockedUser = twitter.BlockOperations.Block(12345);
#endif
            AssertTwitterProfile(blockedUser);
        }

        [Test]
        [ExpectedException(typeof(TwitterApiException),
            ExpectedMessage = "Authorization is required for the operation, but the API binding was created without authorization.")]
        public void Block_UserId_Unauthorized()
        {
#if NET_4_0 || SILVERLIGHT_5
            unauthorizedTwitter.BlockOperations.BlockAsync(12345).Wait();
#else
            unauthorizedTwitter.BlockOperations.Block(12345);
#endif
        }

        [Test]
        public void Block_ScreenName()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/blocks/create.json")
                .AndExpectMethod(HttpMethod.POST)
                .AndExpectBody("screen_name=habuma")
                .AndRespondWith(JsonResource("Twitter_Profile"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            TwitterProfile blockedUser = twitter.BlockOperations.BlockAsync("habuma").Result;
#else
            TwitterProfile blockedUser = twitter.BlockOperations.Block("habuma");
#endif
            AssertTwitterProfile(blockedUser);
        }

        [Test]
        [ExpectedException(typeof(TwitterApiException),
            ExpectedMessage = "Authorization is required for the operation, but the API binding was created without authorization.")]
        public void Block_ScreenName_Unauthorized()
        {
#if NET_4_0 || SILVERLIGHT_5
            unauthorizedTwitter.BlockOperations.BlockAsync("habuma").Wait();
#else
            unauthorizedTwitter.BlockOperations.Block("habuma");
#endif
        }

        [Test]
        public void Unblock_UserId()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/blocks/destroy.json")
                .AndExpectMethod(HttpMethod.POST)
                .AndExpectBody("user_id=12345")
                .AndRespondWith(JsonResource("Twitter_Profile"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            TwitterProfile blockedUser = twitter.BlockOperations.UnblockAsync(12345).Result;
#else
            TwitterProfile blockedUser = twitter.BlockOperations.Unblock(12345);
#endif
            AssertTwitterProfile(blockedUser);
        }

        [Test]
        [ExpectedException(typeof(TwitterApiException),
            ExpectedMessage = "Authorization is required for the operation, but the API binding was created without authorization.")]
        public void Unblock_UserId_Unauthorized()
        {
#if NET_4_0 || SILVERLIGHT_5
            unauthorizedTwitter.BlockOperations.UnblockAsync(12345).Wait();
#else
            unauthorizedTwitter.BlockOperations.Unblock(12345);
#endif
        }

        [Test]
        public void Unblock_ScreenName()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/blocks/destroy.json")
                .AndExpectMethod(HttpMethod.POST)
                .AndExpectBody("screen_name=habuma")
                .AndRespondWith(JsonResource("Twitter_Profile"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            TwitterProfile blockedUser = twitter.BlockOperations.UnblockAsync("habuma").Result;
#else
            TwitterProfile blockedUser = twitter.BlockOperations.Unblock("habuma");
#endif
            AssertTwitterProfile(blockedUser);
        }

        [Test]
        [ExpectedException(typeof(TwitterApiException),
            ExpectedMessage = "Authorization is required for the operation, but the API binding was created without authorization.")]
        public void Unblock_ScreenName_Unauthorized()
        {
#if NET_4_0 || SILVERLIGHT_5
            unauthorizedTwitter.BlockOperations.UnblockAsync("habuma").Wait();
#else
            unauthorizedTwitter.BlockOperations.Unblock("habuma");
#endif
        }

        [Test]
        public void GetBlockedUsers()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/blocks/blocking.json?page=1&per_page=20")
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("List_Of_Profiles"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<TwitterProfile> blockedUsers = twitter.BlockOperations.GetBlockedUsersAsync().Result;
#else
            IList<TwitterProfile> blockedUsers = twitter.BlockOperations.GetBlockedUsers();
#endif
            Assert.AreEqual(2, blockedUsers.Count);
        }

        [Test]
        public void GetBlockedUsers_Paged()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/blocks/blocking.json?page=3&per_page=25")
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("List_Of_Profiles"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<TwitterProfile> blockedUsers = twitter.BlockOperations.GetBlockedUsersAsync(3, 25).Result;
#else
            IList<TwitterProfile> blockedUsers = twitter.BlockOperations.GetBlockedUsers(3, 25);
#endif
            Assert.AreEqual(2, blockedUsers.Count);
        }

        [Test]
        [ExpectedException(typeof(TwitterApiException),
            ExpectedMessage = "Authorization is required for the operation, but the API binding was created without authorization.")]
        public void GetBlockedUsers_Unauthorized()
        {
#if NET_4_0 || SILVERLIGHT_5
            unauthorizedTwitter.BlockOperations.GetBlockedUsersAsync().Wait();
#else
            unauthorizedTwitter.BlockOperations.GetBlockedUsers();
#endif
        }

        [Test]
        public void GetBlockedUserIds()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/blocks/blocking/ids.json")
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("List_Of_Ids"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<long> blockedUsers = twitter.BlockOperations.GetBlockedUserIdsAsync().Result;
#else
            IList<long> blockedUsers = twitter.BlockOperations.GetBlockedUserIds();
#endif
            Assert.AreEqual(4, blockedUsers.Count);
        }

        [Test]
        [ExpectedException(typeof(TwitterApiException),
            ExpectedMessage = "Authorization is required for the operation, but the API binding was created without authorization.")]
        public void GetBlockedUserIds_Unauthorized()
        {
#if NET_4_0 || SILVERLIGHT_5
            unauthorizedTwitter.BlockOperations.GetBlockedUserIdsAsync().Wait();
#else
            unauthorizedTwitter.BlockOperations.GetBlockedUserIds();
#endif
        }

        [Test]
        public void IsBlocking_UserId_True()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/blocks/exists.json?user_id=12345")
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("Twitter_Profile"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            Assert.IsTrue(twitter.BlockOperations.IsBlockingAsync(12345).Result);
#else
            Assert.IsTrue(twitter.BlockOperations.IsBlocking(12345));
#endif
        }

        [Test]
        public void IsBlocking_UserId_False()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/blocks/exists.json?user_id=12345")
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith("{\"request\": \"/1/blocks/exists.json?screen_name=episod\", \"error\": \"You are not blocking this user.\"}", responseHeaders, HttpStatusCode.NotFound, "Not Found");

#if NET_4_0 || SILVERLIGHT_5
            Assert.IsFalse(twitter.BlockOperations.IsBlockingAsync(12345).Result);
#else
            Assert.IsFalse(twitter.BlockOperations.IsBlocking(12345));
#endif
        }

        [Test]
        public void IsBlocking_ScreenName_True() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/blocks/exists.json?screen_name=habuma")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Twitter_Profile"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    Assert.IsTrue(twitter.BlockOperations.IsBlockingAsync("habuma").Result);
#else
            Assert.IsTrue(twitter.BlockOperations.IsBlocking("habuma"));
#endif
	    }

        [Test]
        public void IsBlocking_ScreenName_False() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/blocks/exists.json?screen_name=habuma")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith("{\"request\": \"/1/blocks/exists.json?screen_name=episod\", \"error\": \"You are not blocking this user.\"}", responseHeaders, HttpStatusCode.NotFound, "Not Found");

#if NET_4_0 || SILVERLIGHT_5
		    Assert.IsFalse(twitter.BlockOperations.IsBlockingAsync("habuma").Result);	
#else
            Assert.IsFalse(twitter.BlockOperations.IsBlocking("habuma"));	
#endif
        }

        // test helpers

        private void AssertTwitterProfile(TwitterProfile blockedUser)
        {
            Assert.AreEqual(161064614, blockedUser.ID);
            Assert.AreEqual("artnames", blockedUser.ScreenName);
            Assert.AreEqual("Art Names", blockedUser.Name);
            Assert.AreEqual("I'm just a normal kinda guy", blockedUser.Description);
            Assert.AreEqual("Denton, TX", blockedUser.Location);
            Assert.AreEqual("http://www.springsource.org", blockedUser.Url);
            Assert.AreEqual("http://a1.twimg.com/sticky/default_profile_images/default_profile_4_normal.png", blockedUser.ProfileImageUrl);
        }
    }
}
