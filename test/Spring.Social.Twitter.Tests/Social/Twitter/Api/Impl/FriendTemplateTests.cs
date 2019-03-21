#region License

/*
 * Copyright 2002-2012 the original author or authors.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      https://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

#endregion

using System.Collections.Generic;

using NUnit.Framework;
using Spring.Rest.Client.Testing;

using Spring.Http;
using Spring.IO;

namespace Spring.Social.Twitter.Api.Impl
{
    /// <summary>
    /// Unit tests for the FriendTemplate class.
    /// </summary>
    /// <author>Craig Walls</author>
    /// <author>Bruno Baia (.NET)</author>
    [TestFixture]
    public class FriendTemplateTests : AbstractTwitterOperationsTests 
    {    
	    [Test]
	    public void GetFriends_CurrentUser() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/friends/list.json?cursor=-1")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Friends_Or_Followers"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            System.Threading.Tasks.Task<CursoredList<TwitterProfile>> task = twitter.FriendOperations.GetFriendsAsync();
		    CursoredList<TwitterProfile> friends = task.Result;
#else
            CursoredList<TwitterProfile> friends = twitter.FriendOperations.GetFriends();
#endif
            AssertFriendsOrFollowers(friends);
	    }

	    [Test]
	    public void GetFriendsInCursor_CurrentUser() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/friends/list.json?cursor=987654321")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Friends_Or_Followers"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    CursoredList<TwitterProfile> friends = twitter.FriendOperations.GetFriendsInCursorAsync(987654321).Result;
#else
            CursoredList<TwitterProfile> friends = twitter.FriendOperations.GetFriendsInCursor(987654321);
#endif
            AssertFriendsOrFollowers(friends);
	    }
	
	    [Test]
	    public void GetFriends_ByUserId() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/friends/list.json?cursor=-1&user_id=98765")
			    .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("Friends_Or_Followers"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    CursoredList<TwitterProfile> friends = twitter.FriendOperations.GetFriendsAsync(98765L).Result;
#else
            CursoredList<TwitterProfile> friends = twitter.FriendOperations.GetFriends(98765L);
#endif
            AssertFriendsOrFollowers(friends);
	    }

	    [Test]
	    public void GetFriendsInCursor_ByUserId() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/friends/list.json?cursor=987654321&user_id=98765")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Friends_Or_Followers"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    CursoredList<TwitterProfile> friends = twitter.FriendOperations.GetFriendsInCursorAsync(98765L, 987654321).Result;
#else
            CursoredList<TwitterProfile> friends = twitter.FriendOperations.GetFriendsInCursor(98765L, 987654321);
#endif
            AssertFriendsOrFollowers(friends);
	    }

	    [Test]
	    public void GetFriends_ByScreenName() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/friends/list.json?cursor=-1&screen_name=habuma")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Friends_Or_Followers"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    CursoredList<TwitterProfile> friends = twitter.FriendOperations.GetFriendsAsync("habuma").Result;
#else
            CursoredList<TwitterProfile> friends = twitter.FriendOperations.GetFriends("habuma");
#endif
            AssertFriendsOrFollowers(friends);
	    }

	    [Test]
	    public void GetFriendsInCursor_ByScreenName() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/friends/list.json?cursor=987654321&screen_name=habuma")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Friends_Or_Followers"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    CursoredList<TwitterProfile> friends = twitter.FriendOperations.GetFriendsInCursorAsync("habuma", 987654321).Result;
#else
            CursoredList<TwitterProfile> friends = twitter.FriendOperations.GetFriendsInCursor("habuma", 987654321);
#endif
            AssertFriendsOrFollowers(friends);
	    }

	    [Test]
	    public void GetFriendIds_CurrentUser() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/friends/ids.json?cursor=-1")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Friend_Or_Follower_Ids"), responseHeaders);
		
#if NET_4_0 || SILVERLIGHT_5
		    CursoredList<long> friendIds = twitter.FriendOperations.GetFriendIdsAsync().Result;
#else
            CursoredList<long> friendIds = twitter.FriendOperations.GetFriendIds();
#endif
            AssertFriendOrFollowerIds(friendIds);
	    }

	    [Test]
	    public void GetFriendIdsInCursor_CurrentUser() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/friends/ids.json?cursor=123456")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Friend_Or_Follower_Ids"), responseHeaders);
		
#if NET_4_0 || SILVERLIGHT_5
		    CursoredList<long> friendIds = twitter.FriendOperations.GetFriendIdsInCursorAsync(123456).Result;
#else
            CursoredList<long> friendIds = twitter.FriendOperations.GetFriendIdsInCursor(123456);
#endif
            AssertFriendOrFollowerIds(friendIds);
	    }

	    [Test]
	    public void GetFriendIds_ByUserId() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/friends/ids.json?cursor=-1&user_id=98765")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Friend_Or_Follower_Ids"), responseHeaders);
		
#if NET_4_0 || SILVERLIGHT_5
		    CursoredList<long> friendIds = twitter.FriendOperations.GetFriendIdsAsync(98765L).Result;
#else
            CursoredList<long> friendIds = twitter.FriendOperations.GetFriendIds(98765L);
#endif
            AssertFriendOrFollowerIds(friendIds);
	    }

	    [Test]
	    public void GetFriendIdsInCursor_ByUserId() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/friends/ids.json?cursor=123456&user_id=98765")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Friend_Or_Follower_Ids"), responseHeaders);
		
#if NET_4_0 || SILVERLIGHT_5
		    CursoredList<long> friendIds = twitter.FriendOperations.GetFriendIdsInCursorAsync(98765L, 123456).Result;
#else
            CursoredList<long> friendIds = twitter.FriendOperations.GetFriendIdsInCursor(98765L, 123456);
#endif
            AssertFriendOrFollowerIds(friendIds);
	    }

	    [Test]
	    public void GetFriendIds_ByScreenName() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/friends/ids.json?cursor=-1&screen_name=habuma")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Friend_Or_Follower_Ids"), responseHeaders);
		
#if NET_4_0 || SILVERLIGHT_5
		    CursoredList<long> friendIds = twitter.FriendOperations.GetFriendIdsAsync("habuma").Result;
#else
            CursoredList<long> friendIds = twitter.FriendOperations.GetFriendIds("habuma");
#endif
            AssertFriendOrFollowerIds(friendIds);
	    }

	    [Test]
	    public void GetFriendIdsInCursor_ByScreenName() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/friends/ids.json?cursor=123456&screen_name=habuma")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Friend_Or_Follower_Ids"), responseHeaders);
		
#if NET_4_0 || SILVERLIGHT_5
		    CursoredList<long> friendIds = twitter.FriendOperations.GetFriendIdsInCursorAsync("habuma", 123456).Result;
#else
            CursoredList<long> friendIds = twitter.FriendOperations.GetFriendIdsInCursor("habuma", 123456);
#endif
            AssertFriendOrFollowerIds(friendIds);
	    }

	    [Test] 
	    public void GetFollowers_currentUser() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/followers/list.json?cursor=-1")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Friends_Or_Followers"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            CursoredList<TwitterProfile> followers = twitter.FriendOperations.GetFollowersAsync().Result;
#else
            CursoredList<TwitterProfile> followers = twitter.FriendOperations.GetFollowers();
#endif
            AssertFriendsOrFollowers(followers);
	    }

	    [Test] 
	    public void GetFollowersInCursor_CurrentUser() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/followers/list.json?cursor=24680")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Friends_Or_Followers"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            CursoredList<TwitterProfile> followers = twitter.FriendOperations.GetFollowersInCursorAsync(24680).Result;
#else
            CursoredList<TwitterProfile> followers = twitter.FriendOperations.GetFollowersInCursor(24680);
#endif
            AssertFriendsOrFollowers(followers);
	    }

	    [Test] 
	    public void GetFollowers_ByUserId() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/followers/list.json?cursor=-1&user_id=98765")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Friends_Or_Followers"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            CursoredList<TwitterProfile> followers = twitter.FriendOperations.GetFollowersAsync(98765L).Result;
#else
            CursoredList<TwitterProfile> followers = twitter.FriendOperations.GetFollowers(98765L);
#endif
            AssertFriendsOrFollowers(followers);
	    }

	    [Test] 
	    public void GetFollowersInCursor_ByUserId() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/followers/list.json?cursor=13579&user_id=98765")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Friends_Or_Followers"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            CursoredList<TwitterProfile> followers = twitter.FriendOperations.GetFollowersInCursorAsync(98765L, 13579).Result;
#else
            CursoredList<TwitterProfile> followers = twitter.FriendOperations.GetFollowersInCursor(98765L, 13579);
#endif
            AssertFriendsOrFollowers(followers);
	    }

	    [Test] 
	    public void GetFollowers_ByScreenName() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/followers/list.json?cursor=-1&screen_name=oizik")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Friends_Or_Followers"), responseHeaders);
	    
#if NET_4_0 || SILVERLIGHT_5
            CursoredList<TwitterProfile> followers = twitter.FriendOperations.GetFollowersAsync("oizik").Result;
#else
            CursoredList<TwitterProfile> followers = twitter.FriendOperations.GetFollowers("oizik");
#endif
            AssertFriendsOrFollowers(followers);
	    }

	    [Test] 
	    public void GetFollowersInCursor_ByScreenName() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/followers/list.json?cursor=12357&screen_name=oizik")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Friends_Or_Followers"), responseHeaders);
	    
#if NET_4_0 || SILVERLIGHT_5
            CursoredList<TwitterProfile> followers = twitter.FriendOperations.GetFollowersInCursorAsync("oizik", 12357).Result;
#else
            CursoredList<TwitterProfile> followers = twitter.FriendOperations.GetFollowersInCursor("oizik", 12357);
#endif
            AssertFriendsOrFollowers(followers);
	    }

	    [Test]
	    public void GetFollowerIds_CurrentUser() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/followers/ids.json?cursor=-1")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Friend_Or_Follower_Ids"), responseHeaders);
		
#if NET_4_0 || SILVERLIGHT_5
		    CursoredList<long> followerIds = twitter.FriendOperations.GetFollowerIdsAsync().Result;
#else
            CursoredList<long> followerIds = twitter.FriendOperations.GetFollowerIds();
#endif
            AssertFriendOrFollowerIds(followerIds);
	    }

	    [Test]
	    public void GetFollowerIdsInCursor_CurrentUser() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/followers/ids.json?cursor=24680")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Friend_Or_Follower_Ids"), responseHeaders);
		
#if NET_4_0 || SILVERLIGHT_5
		    CursoredList<long> followerIds = twitter.FriendOperations.GetFollowerIdsInCursorAsync(24680).Result;
#else
            CursoredList<long> followerIds = twitter.FriendOperations.GetFollowerIdsInCursor(24680);
#endif
            AssertFriendOrFollowerIds(followerIds);
	    }

	    [Test]
	    public void GetFollowerIds_ByUserId() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/followers/ids.json?cursor=-1&user_id=98765")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Friend_Or_Follower_Ids"), responseHeaders);
		
#if NET_4_0 || SILVERLIGHT_5
		    CursoredList<long> followerIds = twitter.FriendOperations.GetFollowerIdsAsync(98765L).Result;
#else
            CursoredList<long> followerIds = twitter.FriendOperations.GetFollowerIds(98765L);
#endif
            AssertFriendOrFollowerIds(followerIds);
	    }

	    [Test]
	    public void GetFollowerIdsInCursor_ByUserId() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/followers/ids.json?cursor=24680&user_id=98765")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Friend_Or_Follower_Ids"), responseHeaders);
		
#if NET_4_0 || SILVERLIGHT_5
		    CursoredList<long> followerIds = twitter.FriendOperations.GetFollowerIdsInCursorAsync(98765L, 24680).Result;
#else
            CursoredList<long> followerIds = twitter.FriendOperations.GetFollowerIdsInCursor(98765L, 24680);
#endif
            AssertFriendOrFollowerIds(followerIds);
	    }

	    [Test]
	    public void GetFollowerIds_ByScreenName() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/followers/ids.json?cursor=-1&screen_name=habuma")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Friend_Or_Follower_Ids"), responseHeaders);
		
#if NET_4_0 || SILVERLIGHT_5
		    CursoredList<long> followerIds = twitter.FriendOperations.GetFollowerIdsAsync("habuma").Result;
#else
            CursoredList<long> followerIds = twitter.FriendOperations.GetFollowerIds("habuma");
#endif
		    AssertFriendOrFollowerIds(followerIds);
	    }

	    [Test]
	    public void GetFollowerIdsInCursor_ByScreenName() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/followers/ids.json?cursor=24680&screen_name=habuma")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Friend_Or_Follower_Ids"), responseHeaders);
		
#if NET_4_0 || SILVERLIGHT_5
		    CursoredList<long> followerIds = twitter.FriendOperations.GetFollowerIdsInCursorAsync("habuma", 24680).Result;
#else
            CursoredList<long> followerIds = twitter.FriendOperations.GetFollowerIdsInCursor("habuma", 24680);
#endif
            AssertFriendOrFollowerIds(followerIds);
	    }

	    [Test]
	    public void Follow_ByUserId() 
        {
	        mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/friendships/create.json")
	            .AndExpectMethod(HttpMethod.POST)
                .AndExpectBody("user_id=98765")
	            .AndRespondWith(JsonResource("Follow"), responseHeaders);
	    
#if NET_4_0 || SILVERLIGHT_5
            TwitterProfile followedUser = twitter.FriendOperations.FollowAsync(98765).Result;
#else
            TwitterProfile followedUser = twitter.FriendOperations.Follow(98765);
#endif
            Assert.AreEqual("oizik2", followedUser.ScreenName);
	    }

	    [Test]
	    public void Follow_ByScreenName() 
        {
	        mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/friendships/create.json")
	            .AndExpectMethod(HttpMethod.POST)
                .AndExpectBody("screen_name=oizik2")
	            .AndRespondWith(JsonResource("Follow"), responseHeaders);
	    
#if NET_4_0 || SILVERLIGHT_5
            TwitterProfile followedUser = twitter.FriendOperations.FollowAsync("oizik2").Result;
#else
            TwitterProfile followedUser = twitter.FriendOperations.Follow("oizik2");
#endif
            Assert.AreEqual("oizik2", followedUser.ScreenName);
	    }
	
	    [Test]
	    public void Unfollow_ByUserId() 
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/friendships/destroy.json")
                .AndExpectMethod(HttpMethod.POST)
                .AndExpectBody("user_id=98765")
                .AndRespondWith(JsonResource("Unfollow"), responseHeaders);
        
#if NET_4_0 || SILVERLIGHT_5
            TwitterProfile unFollowedUser = twitter.FriendOperations.UnfollowAsync(98765).Result;
#else
            TwitterProfile unFollowedUser = twitter.FriendOperations.Unfollow(98765);
#endif
            Assert.AreEqual("oizik2", unFollowedUser.ScreenName);
        }

	    [Test]
	    public void Unfollow_ByScreenName() 
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/friendships/destroy.json")
                .AndExpectMethod(HttpMethod.POST)
                .AndExpectBody("screen_name=oizik2")
                .AndRespondWith(JsonResource("Unfollow"), responseHeaders);
        
#if NET_4_0 || SILVERLIGHT_5
            TwitterProfile unFollowedUser = twitter.FriendOperations.UnfollowAsync("oizik2").Result;
#else
            TwitterProfile unFollowedUser = twitter.FriendOperations.Unfollow("oizik2");
#endif
            Assert.AreEqual("oizik2", unFollowedUser.ScreenName);
        }

	    [Test]
	    public void EnableNotifications_ByUserId() 
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/friendships/update.json")
                .AndExpectMethod(HttpMethod.POST)
                .AndExpectBody("user_id=98765&device=true")
                .AndRespondWith(JsonResource("Follow"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    twitter.FriendOperations.EnableNotificationsAsync(98765).Wait();
#else
            twitter.FriendOperations.EnableNotifications(98765);
#endif
        }
	
	    [Test]
	    public void EnableNotifications_ByScreenName() 
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/friendships/update.json")
                .AndExpectMethod(HttpMethod.POST)
                .AndExpectBody("screen_name=oizik2&device=true")
                .AndRespondWith(JsonResource("Follow"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    twitter.FriendOperations.EnableNotificationsAsync("oizik2").Wait();
#else
            twitter.FriendOperations.EnableNotifications("oizik2");
#endif
        }

	    [Test]
	    public void DisableNotifications_ByUserId() 
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/friendships/update.json")
                .AndExpectMethod(HttpMethod.POST)
                .AndExpectBody("user_id=98765&device=false")
                .AndRespondWith(JsonResource("Unfollow"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    twitter.FriendOperations.DisableNotificationsAsync(98765).Wait();
#else
            twitter.FriendOperations.DisableNotifications(98765);
#endif
        }
	
	    [Test]
	    public void DisableNotifications_ByScreenName() 
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/friendships/update.json")
                .AndExpectMethod(HttpMethod.POST)
                .AndExpectBody("screen_name=oizik2&device=false")
                .AndRespondWith(JsonResource("Unfollow"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    twitter.FriendOperations.DisableNotificationsAsync("oizik2").Wait();
#else
            twitter.FriendOperations.DisableNotifications("oizik2");
#endif
        }
	
	    [Test]
	    public void GetIncomingFriendships() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/friendships/incoming.json?cursor=-1")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Incoming_Or_Outgoing_Friendships"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    CursoredList<long> friendships = twitter.FriendOperations.GetIncomingFriendshipsAsync().Result;
#else
            CursoredList<long> friendships = twitter.FriendOperations.GetIncomingFriendships();
#endif
            AssertIncomingOutgoingFriendships(friendships);
	    }

	    [Test]
	    public void GetIncomingFriendships_Cursored() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/friendships/incoming.json?cursor=1234567")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Incoming_Or_Outgoing_Friendships"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    CursoredList<long> friendships = twitter.FriendOperations.GetIncomingFriendshipsAsync(1234567).Result;
#else
            CursoredList<long> friendships = twitter.FriendOperations.GetIncomingFriendships(1234567);
#endif
            AssertIncomingOutgoingFriendships(friendships);
	    }
	
	    [Test]
	    public void GetOutgoingFriendships() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/friendships/outgoing.json?cursor=-1")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Incoming_Or_Outgoing_Friendships"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    CursoredList<long> friendships = twitter.FriendOperations.GetOutgoingFriendshipsAsync().Result;
#else
            CursoredList<long> friendships = twitter.FriendOperations.GetOutgoingFriendships();
#endif
            AssertIncomingOutgoingFriendships(friendships);
	    }

	    [Test]
	    public void GetOutgoingFriendships_Cursored() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/friendships/outgoing.json?cursor=9876543")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Incoming_Or_Outgoing_Friendships"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    CursoredList<long> friendships = twitter.FriendOperations.GetOutgoingFriendshipsAsync(9876543).Result;
#else
            CursoredList<long> friendships = twitter.FriendOperations.GetOutgoingFriendships(9876543);
#endif
            AssertIncomingOutgoingFriendships(friendships);
	    }


        // test helpers
        
	    private void AssertFriendOrFollowerIds(CursoredList<long> friendOrFollowerIds) 
        {
		    Assert.AreEqual(2, friendOrFollowerIds.Count);
		    Assert.AreEqual(14846645L, friendOrFollowerIds[0]);
		    Assert.AreEqual(14718006L, friendOrFollowerIds[1]);
		    Assert.AreEqual(112233, friendOrFollowerIds.PreviousCursor);
		    Assert.AreEqual(332211, friendOrFollowerIds.NextCursor);
	    }

	    private void AssertFriendsOrFollowers(CursoredList<TwitterProfile> friendsOrFollowers) 
        {
		    Assert.AreEqual(2, friendsOrFollowers.Count);
		    Assert.AreEqual("royclarkson", friendsOrFollowers[0].ScreenName);
		    Assert.AreEqual("kdonald", friendsOrFollowers[1].ScreenName);
		    Assert.AreEqual(112233, friendsOrFollowers.PreviousCursor);
		    Assert.AreEqual(332211, friendsOrFollowers.NextCursor);
	    }

	    private void AssertIncomingOutgoingFriendships(CursoredList<long> friendships) 
        {
		    Assert.AreEqual(3, friendships.Count);
		    Assert.AreEqual(12345, friendships[0]);
		    Assert.AreEqual(23456, friendships[1]);
		    Assert.AreEqual(34567, friendships[2]);
		    Assert.AreEqual(1234567890, friendships.PreviousCursor);
		    Assert.AreEqual(1357924680, friendships.NextCursor);
	    }
    }
}
