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
                .AndExpectUri("https://api.twitter.com/1.1/blocks/create.json")
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
                .AndExpectUri("https://api.twitter.com/1.1/blocks/create.json")
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
                .AndExpectUri("https://api.twitter.com/1.1/blocks/destroy.json")
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
                .AndExpectUri("https://api.twitter.com/1.1/blocks/destroy.json")
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
                .AndExpectUri("https://api.twitter.com/1.1/blocks/list.json?skip_status=true&cursor=-1")
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("CursoredList_Of_Profiles"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            CursoredList<TwitterProfile> blockedUsers = twitter.BlockOperations.GetBlockedUsersAsync().Result;
#else
            CursoredList<TwitterProfile> blockedUsers = twitter.BlockOperations.GetBlockedUsers();
#endif
            Assert.AreEqual(2, blockedUsers.Count);
            Assert.AreEqual(12, blockedUsers.PreviousCursor);
            Assert.AreEqual(65, blockedUsers.NextCursor);
        }

        [Test]
        public void GetBlockedUsers_Cursored()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/blocks/list.json?skip_status=true&cursor=25")
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("CursoredList_Of_Profiles"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            CursoredList<TwitterProfile> blockedUsers = twitter.BlockOperations.GetBlockedUsersAsync(25).Result;
#else
            CursoredList<TwitterProfile> blockedUsers = twitter.BlockOperations.GetBlockedUsers(25);
#endif
            Assert.AreEqual(2, blockedUsers.Count);
            Assert.AreEqual(12, blockedUsers.PreviousCursor);
            Assert.AreEqual(65, blockedUsers.NextCursor);
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
                .AndExpectUri("https://api.twitter.com/1.1/blocks/ids.json?cursor=-1")
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("CursoredList_Of_Ids"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            CursoredList<long> blockedUsers = twitter.BlockOperations.GetBlockedUserIdsAsync().Result;
#else
            CursoredList<long> blockedUsers = twitter.BlockOperations.GetBlockedUserIds();
#endif
            Assert.AreEqual(4, blockedUsers.Count);
            Assert.AreEqual(12, blockedUsers.PreviousCursor);
            Assert.AreEqual(65, blockedUsers.NextCursor);
        }

        [Test]
        public void GetBlockedUserIds_Cursored()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/blocks/ids.json?cursor=21")
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("CursoredList_Of_Ids"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            CursoredList<long> blockedUsers = twitter.BlockOperations.GetBlockedUserIdsAsync(21).Result;
#else
            CursoredList<long> blockedUsers = twitter.BlockOperations.GetBlockedUserIds(21);
#endif
            Assert.AreEqual(4, blockedUsers.Count);
            Assert.AreEqual(12, blockedUsers.PreviousCursor);
            Assert.AreEqual(65, blockedUsers.NextCursor);
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
