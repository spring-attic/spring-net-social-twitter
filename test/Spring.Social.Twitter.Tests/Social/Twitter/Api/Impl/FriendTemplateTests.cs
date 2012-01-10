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
                .AndExpectUri("https://api.twitter.com/1/friends/ids.json?cursor=-1")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Friend_Or_Follower_Ids"), responseHeaders);
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/users/lookup.json?user_id=14846645%2C14718006")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("List_Of_Profiles"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            System.Threading.Tasks.Task<CursoredList<TwitterProfile>> task = twitter.FriendOperations.GetFriendsAsync();
		    CursoredList<TwitterProfile> friends = task.Result;
#else
            CursoredList<TwitterProfile> friends = twitter.FriendOperations.GetFriends();
#endif
            AssertFriendsFollowers(friends);
	    }

	    [Test]
	    public void GetFriendsInCursor_CurrentUser() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/friends/ids.json?cursor=987654321")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Friend_Or_Follower_Ids"), responseHeaders);
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/users/lookup.json?user_id=14846645%2C14718006")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("List_Of_Profiles"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    CursoredList<TwitterProfile> friends = twitter.FriendOperations.GetFriendsInCursorAsync(987654321).Result;
#else
            CursoredList<TwitterProfile> friends = twitter.FriendOperations.GetFriendsInCursor(987654321);
#endif
            AssertFriendsFollowers(friends);
	    }

	    [Test]
        [ExpectedException(typeof(NotAuthorizedException),
            ExpectedMessage = "Authorization is required for the operation, but the API binding was created without authorization.")]
	    public void GetFriends_CurrentUser_Unauthorized() 
        {
#if NET_4_0 || SILVERLIGHT_5
		    unauthorizedTwitter.FriendOperations.GetFriendsAsync().Wait();
#else
            unauthorizedTwitter.FriendOperations.GetFriends();
#endif
        }
	
	    [Test]
	    public void GetFriends_ByUserId() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/friends/ids.json?cursor=-1&user_id=98765")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Friend_Or_Follower_Ids"), responseHeaders);
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/users/lookup.json?user_id=14846645%2C14718006")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("List_Of_Profiles"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    CursoredList<TwitterProfile> friends = twitter.FriendOperations.GetFriendsAsync(98765L).Result;
#else
            CursoredList<TwitterProfile> friends = twitter.FriendOperations.GetFriends(98765L);
#endif
            AssertFriendsFollowers(friends);
	    }

	    [Test]
	    public void GetFriendsInCursor_ByUserId() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/friends/ids.json?cursor=987654321&user_id=98765")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Friend_Or_Follower_Ids"), responseHeaders);
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/users/lookup.json?user_id=14846645%2C14718006")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("List_Of_Profiles"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    CursoredList<TwitterProfile> friends = twitter.FriendOperations.GetFriendsInCursorAsync(98765L, 987654321).Result;
#else
            CursoredList<TwitterProfile> friends = twitter.FriendOperations.GetFriendsInCursor(98765L, 987654321);
#endif
            AssertFriendsFollowers(friends);
	    }

	    [Test]
	    public void GetFriends_ByScreenName() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/friends/ids.json?cursor=-1&screen_name=habuma")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Friend_Or_Follower_Ids"), responseHeaders);
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/users/lookup.json?user_id=14846645%2C14718006")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("List_Of_Profiles"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    CursoredList<TwitterProfile> friends = twitter.FriendOperations.GetFriendsAsync("habuma").Result;
#else
            CursoredList<TwitterProfile> friends = twitter.FriendOperations.GetFriends("habuma");
#endif
            AssertFriendsFollowers(friends);
	    }

	    [Test]
	    public void GetFriendsInCursor_ByScreenName() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/friends/ids.json?cursor=987654321&screen_name=habuma")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Friend_Or_Follower_Ids"), responseHeaders);
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/users/lookup.json?user_id=14846645%2C14718006")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("List_Of_Profiles"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    CursoredList<TwitterProfile> friends = twitter.FriendOperations.GetFriendsInCursorAsync("habuma", 987654321).Result;
#else
            CursoredList<TwitterProfile> friends = twitter.FriendOperations.GetFriendsInCursor("habuma", 987654321);
#endif
            AssertFriendsFollowers(friends);
	    }

	    [Test]
	    public void GetFriends_CurrentUser_NoFriends() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/friends/ids.json?cursor=-1")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("No_Friend_Or_Follower_Ids"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    IList<TwitterProfile> friends = twitter.FriendOperations.GetFriendsAsync().Result;
#else
            IList<TwitterProfile> friends = twitter.FriendOperations.GetFriends();
#endif
            Assert.AreEqual(0, friends.Count);
	    }

	    [Test]
	    public void GetFriends_CurrentUser_ManyFriends() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/friends/ids.json?cursor=-1")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Many_Friend_Or_Follower_Ids"), responseHeaders);
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/users/lookup.json?user_id=1%2C2%2C3%2C4%2C5%2C6%2C7%2C8%2C9%2C10%2C11%2C12%2C13%2C14%2C15%2C16%2C17%2C18%2C19%2C20%2C21%2C22%2C23%2C24%2C25%2C26%2C27%2C28%2C29%2C30%2C31%2C32%2C33%2C34%2C35%2C36%2C37%2C38%2C39%2C40%2C41%2C42%2C43%2C44%2C45%2C46%2C47%2C48%2C49%2C50%2C51%2C52%2C53%2C54%2C55%2C56%2C57%2C58%2C59%2C60%2C61%2C62%2C63%2C64%2C65%2C66%2C67%2C68%2C69%2C70%2C71%2C72%2C73%2C74%2C75%2C76%2C77%2C78%2C79%2C80%2C81%2C82%2C83%2C84%2C85%2C86%2C87%2C88%2C89%2C90%2C91%2C92%2C93%2C94%2C95%2C96%2C97%2C98%2C99%2C100")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("List_Of_Profiles"), responseHeaders);
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/users/lookup.json?user_id=101%2C102%2C103%2C104%2C105%2C106%2C107%2C108%2C109%2C110%2C111%2C112%2C113%2C114%2C115%2C116%2C117%2C118%2C119%2C120%2C121%2C122%2C123%2C124%2C125%2C126%2C127%2C128%2C129%2C130%2C131%2C132%2C133%2C134%2C135%2C136%2C137%2C138%2C139%2C140%2C141%2C142%2C143%2C144%2C145%2C146%2C147%2C148%2C149%2C150%2C151%2C152%2C153%2C154%2C155%2C156%2C157%2C158%2C159%2C160%2C161%2C162%2C163%2C164%2C165%2C166%2C167%2C168%2C169%2C170%2C171%2C172%2C173%2C174%2C175%2C176%2C177%2C178%2C179%2C180%2C181%2C182%2C183%2C184%2C185%2C186%2C187%2C188%2C189%2C190%2C191%2C192%2C193%2C194%2C195%2C196%2C197%2C198%2C199%2C200")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("List_Of_Profiles"), responseHeaders);
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/users/lookup.json?user_id=201%2C202%2C203%2C204%2C205%2C206%2C207%2C208%2C209%2C210%2C211%2C212%2C213%2C214%2C215%2C216%2C217%2C218%2C219%2C220%2C221%2C222%2C223%2C224%2C225%2C226%2C227%2C228%2C229%2C230%2C231%2C232%2C233%2C234%2C235%2C236%2C237%2C238%2C239%2C240%2C241%2C242")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("List_Of_Profiles"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    IList<TwitterProfile> friends = twitter.FriendOperations.GetFriendsAsync().Result;
#else
            IList<TwitterProfile> friends = twitter.FriendOperations.GetFriends();
#endif
            // what's important is that the IDs from friends/ids is chunked up correctly and that users/lookup was invoked the right number of times 
		    // and that each time its response was added to the big list; not that we actually get 242 profiles back
		    Assert.AreEqual(6, friends.Count);
		    Assert.AreEqual("royclarkson", friends[0].ScreenName);
		    Assert.AreEqual("kdonald", friends[1].ScreenName);
		    Assert.AreEqual("royclarkson", friends[2].ScreenName);
		    Assert.AreEqual("kdonald", friends[3].ScreenName);
		    Assert.AreEqual("royclarkson", friends[4].ScreenName);
		    Assert.AreEqual("kdonald", friends[5].ScreenName);
	    }

	    [Test]
	    public void GetFriendIds_CurrentUser() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/friends/ids.json?cursor=-1")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Friend_Or_Follower_Ids"), responseHeaders);
		
#if NET_4_0 || SILVERLIGHT_5
		    CursoredList<long> friendIds = twitter.FriendOperations.GetFriendIdsAsync().Result;
#else
            CursoredList<long> friendIds = twitter.FriendOperations.GetFriendIds();
#endif
            AssertFriendFollowerIdsList(friendIds);
	    }

	    [Test]
	    [ExpectedException(typeof(NotAuthorizedException), 
            ExpectedMessage = "Authorization is required for the operation, but the API binding was created without authorization.")]
	    public void GetFriendIds_CurrentUser_Unauthorized() 
        {
#if NET_4_0 || SILVERLIGHT_5
		    unauthorizedTwitter.FriendOperations.GetFriendIdsAsync().Wait();
#else
            unauthorizedTwitter.FriendOperations.GetFriendIds();
#endif
        }

	    [Test]
	    public void GetFriendIdsInCursor_CurrentUser() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/friends/ids.json?cursor=123456")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Friend_Or_Follower_Ids"), responseHeaders);
		
#if NET_4_0 || SILVERLIGHT_5
		    CursoredList<long> friendIds = twitter.FriendOperations.GetFriendIdsInCursorAsync(123456).Result;
#else
            CursoredList<long> friendIds = twitter.FriendOperations.GetFriendIdsInCursor(123456);
#endif
            AssertFriendFollowerIdsList(friendIds);
	    }

	    [Test]
	    public void GetFriendIds_ByUserId() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/friends/ids.json?cursor=-1&user_id=98765")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Friend_Or_Follower_Ids"), responseHeaders);
		
#if NET_4_0 || SILVERLIGHT_5
		    CursoredList<long> friendIds = twitter.FriendOperations.GetFriendIdsAsync(98765L).Result;
#else
            CursoredList<long> friendIds = twitter.FriendOperations.GetFriendIds(98765L);
#endif
            AssertFriendFollowerIdsList(friendIds);
	    }

	    [Test]
	    public void GetFriendIdsInCursor_ByUserId() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/friends/ids.json?cursor=123456&user_id=98765")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Friend_Or_Follower_Ids"), responseHeaders);
		
#if NET_4_0 || SILVERLIGHT_5
		    CursoredList<long> friendIds = twitter.FriendOperations.GetFriendIdsInCursorAsync(98765L, 123456).Result;
#else
            CursoredList<long> friendIds = twitter.FriendOperations.GetFriendIdsInCursor(98765L, 123456);
#endif
            AssertFriendFollowerIdsList(friendIds);
	    }

	    [Test]
	    public void GetFriendIds_ByScreenName() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/friends/ids.json?cursor=-1&screen_name=habuma")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Friend_Or_Follower_Ids"), responseHeaders);
		
#if NET_4_0 || SILVERLIGHT_5
		    CursoredList<long> friendIds = twitter.FriendOperations.GetFriendIdsAsync("habuma").Result;
#else
            CursoredList<long> friendIds = twitter.FriendOperations.GetFriendIds("habuma");
#endif
            AssertFriendFollowerIdsList(friendIds);
	    }

	    [Test]
	    public void GetFriendIdsInCursor_ByScreenName() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/friends/ids.json?cursor=123456&screen_name=habuma")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Friend_Or_Follower_Ids"), responseHeaders);
		
#if NET_4_0 || SILVERLIGHT_5
		    CursoredList<long> friendIds = twitter.FriendOperations.GetFriendIdsInCursorAsync("habuma", 123456).Result;
#else
            CursoredList<long> friendIds = twitter.FriendOperations.GetFriendIdsInCursor("habuma", 123456);
#endif
            AssertFriendFollowerIdsList(friendIds);
	    }

	    [Test] 
	    public void GetFollowers_currentUser() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/followers/ids.json?cursor=-1")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Friend_Or_Follower_Ids"), responseHeaders);
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/users/lookup.json?user_id=14846645%2C14718006")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("List_Of_Profiles"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    IList<TwitterProfile> followers = twitter.FriendOperations.GetFollowersAsync().Result;
#else
            IList<TwitterProfile> followers = twitter.FriendOperations.GetFollowers();
#endif
            Assert.AreEqual(2, followers.Count);
		    Assert.AreEqual("royclarkson", followers[0].ScreenName);
		    Assert.AreEqual("kdonald", followers[1].ScreenName);
	    }

	    [Test] 
	    public void GetFollowersInCursor_CurrentUser() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/followers/ids.json?cursor=24680")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Friend_Or_Follower_Ids"), responseHeaders);
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/users/lookup.json?user_id=14846645%2C14718006")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("List_Of_Profiles"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    IList<TwitterProfile> followers = twitter.FriendOperations.GetFollowersInCursorAsync(24680).Result;
#else
            IList<TwitterProfile> followers = twitter.FriendOperations.GetFollowersInCursor(24680);
#endif
            Assert.AreEqual(2, followers.Count);
		    Assert.AreEqual("royclarkson", followers[0].ScreenName);
		    Assert.AreEqual("kdonald", followers[1].ScreenName);
	    }

	    [Test]
	    [ExpectedException(typeof(NotAuthorizedException), 
            ExpectedMessage = "Authorization is required for the operation, but the API binding was created without authorization.")]
	    public void GetFollowers_currentUser_Unauthorized() 
        {
#if NET_4_0 || SILVERLIGHT_5
		    unauthorizedTwitter.FriendOperations.GetFollowersAsync().Wait();
#else
            unauthorizedTwitter.FriendOperations.GetFollowers();
#endif
        }

	    [Test] 
	    public void GetFollowers_ByUserId() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/followers/ids.json?cursor=-1&user_id=98765")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Friend_Or_Follower_Ids"), responseHeaders);
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/users/lookup.json?user_id=14846645%2C14718006")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("List_Of_Profiles"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    IList<TwitterProfile> followers = twitter.FriendOperations.GetFollowersAsync(98765L).Result;
#else
            IList<TwitterProfile> followers = twitter.FriendOperations.GetFollowers(98765L);
#endif
            Assert.AreEqual(2, followers.Count);
		    Assert.AreEqual("royclarkson", followers[0].ScreenName);
		    Assert.AreEqual("kdonald", followers[1].ScreenName);
	    }

	    [Test] 
	    public void GetFollowersInCursor_ByUserId() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/followers/ids.json?cursor=13579&user_id=98765")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Friend_Or_Follower_Ids"), responseHeaders);
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/users/lookup.json?user_id=14846645%2C14718006")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("List_Of_Profiles"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    IList<TwitterProfile> followers = twitter.FriendOperations.GetFollowersInCursorAsync(98765L, 13579).Result;
#else
            IList<TwitterProfile> followers = twitter.FriendOperations.GetFollowersInCursor(98765L, 13579);
#endif
            Assert.AreEqual(2, followers.Count);
		    Assert.AreEqual("royclarkson", followers[0].ScreenName);
		    Assert.AreEqual("kdonald", followers[1].ScreenName);
	    }

	    [Test] 
	    public void GetFollowers_ByScreenName() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/followers/ids.json?cursor=-1&screen_name=oizik")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Friend_Or_Follower_Ids"), responseHeaders);
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/users/lookup.json?user_id=14846645%2C14718006")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("List_Of_Profiles"), responseHeaders);
	    
#if NET_4_0 || SILVERLIGHT_5
		    IList<TwitterProfile> followers = twitter.FriendOperations.GetFollowersAsync("oizik").Result;
#else
            IList<TwitterProfile> followers = twitter.FriendOperations.GetFollowers("oizik");
#endif
            Assert.AreEqual(2, followers.Count);
		    Assert.AreEqual("royclarkson", followers[0].ScreenName);
		    Assert.AreEqual("kdonald", followers[1].ScreenName);
	    }

	    [Test] 
	    public void GetFollowersInCursor_ByScreenName() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/followers/ids.json?cursor=12357&screen_name=oizik")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Friend_Or_Follower_Ids"), responseHeaders);
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/users/lookup.json?user_id=14846645%2C14718006")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("List_Of_Profiles"), responseHeaders);
	    
#if NET_4_0 || SILVERLIGHT_5
		    IList<TwitterProfile> followers = twitter.FriendOperations.GetFollowersInCursorAsync("oizik", 12357).Result;
#else
            IList<TwitterProfile> followers = twitter.FriendOperations.GetFollowersInCursor("oizik", 12357);
#endif
            Assert.AreEqual(2, followers.Count);
		    Assert.AreEqual("royclarkson", followers[0].ScreenName);
		    Assert.AreEqual("kdonald", followers[1].ScreenName);
	    }

	    [Test]
	    public void GetFriends_CurrentUser_NoFollowers() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/followers/ids.json?cursor=-1")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("No_Friend_Or_Follower_Ids"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    IList<TwitterProfile> friends = twitter.FriendOperations.GetFollowersAsync().Result;
#else
            IList<TwitterProfile> friends = twitter.FriendOperations.GetFollowers();
#endif
            Assert.AreEqual(0, friends.Count);
	    }

	    [Test]
	    public void GetFollowers_CurrentUser_ManyFollowers() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/followers/ids.json?cursor=-1")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Many_Friend_Or_Follower_Ids"), responseHeaders);
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/users/lookup.json?user_id=1%2C2%2C3%2C4%2C5%2C6%2C7%2C8%2C9%2C10%2C11%2C12%2C13%2C14%2C15%2C16%2C17%2C18%2C19%2C20%2C21%2C22%2C23%2C24%2C25%2C26%2C27%2C28%2C29%2C30%2C31%2C32%2C33%2C34%2C35%2C36%2C37%2C38%2C39%2C40%2C41%2C42%2C43%2C44%2C45%2C46%2C47%2C48%2C49%2C50%2C51%2C52%2C53%2C54%2C55%2C56%2C57%2C58%2C59%2C60%2C61%2C62%2C63%2C64%2C65%2C66%2C67%2C68%2C69%2C70%2C71%2C72%2C73%2C74%2C75%2C76%2C77%2C78%2C79%2C80%2C81%2C82%2C83%2C84%2C85%2C86%2C87%2C88%2C89%2C90%2C91%2C92%2C93%2C94%2C95%2C96%2C97%2C98%2C99%2C100")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("List_Of_Profiles"), responseHeaders);
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/users/lookup.json?user_id=101%2C102%2C103%2C104%2C105%2C106%2C107%2C108%2C109%2C110%2C111%2C112%2C113%2C114%2C115%2C116%2C117%2C118%2C119%2C120%2C121%2C122%2C123%2C124%2C125%2C126%2C127%2C128%2C129%2C130%2C131%2C132%2C133%2C134%2C135%2C136%2C137%2C138%2C139%2C140%2C141%2C142%2C143%2C144%2C145%2C146%2C147%2C148%2C149%2C150%2C151%2C152%2C153%2C154%2C155%2C156%2C157%2C158%2C159%2C160%2C161%2C162%2C163%2C164%2C165%2C166%2C167%2C168%2C169%2C170%2C171%2C172%2C173%2C174%2C175%2C176%2C177%2C178%2C179%2C180%2C181%2C182%2C183%2C184%2C185%2C186%2C187%2C188%2C189%2C190%2C191%2C192%2C193%2C194%2C195%2C196%2C197%2C198%2C199%2C200")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("List_Of_Profiles"), responseHeaders);
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/users/lookup.json?user_id=201%2C202%2C203%2C204%2C205%2C206%2C207%2C208%2C209%2C210%2C211%2C212%2C213%2C214%2C215%2C216%2C217%2C218%2C219%2C220%2C221%2C222%2C223%2C224%2C225%2C226%2C227%2C228%2C229%2C230%2C231%2C232%2C233%2C234%2C235%2C236%2C237%2C238%2C239%2C240%2C241%2C242")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("List_Of_Profiles"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    IList<TwitterProfile> friends = twitter.FriendOperations.GetFollowersAsync().Result;
#else
            IList<TwitterProfile> friends = twitter.FriendOperations.GetFollowers();
#endif
            // what's important is that the IDs from friends/ids is chunked up correctly and that users/lookup was invoked the right number of times 
		    // and that each time its response was added to the big list; not that we actually get 242 profiles back
		    Assert.AreEqual(6, friends.Count);
		    Assert.AreEqual("royclarkson", friends[0].ScreenName);
		    Assert.AreEqual("kdonald", friends[1].ScreenName);
		    Assert.AreEqual("royclarkson", friends[2].ScreenName);
		    Assert.AreEqual("kdonald", friends[3].ScreenName);
		    Assert.AreEqual("royclarkson", friends[4].ScreenName);
		    Assert.AreEqual("kdonald", friends[5].ScreenName);
	    }

	    [Test]
	    public void GetFollowerIds_CurrentUser() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/followers/ids.json?cursor=-1")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Friend_Or_Follower_Ids"), responseHeaders);
		
#if NET_4_0 || SILVERLIGHT_5
		    CursoredList<long> followerIds = twitter.FriendOperations.GetFollowerIdsAsync().Result;
#else
            CursoredList<long> followerIds = twitter.FriendOperations.GetFollowerIds();
#endif
            AssertFriendFollowerIdsList(followerIds);
	    }

	    [Test]
	    public void GetFollowerIdsInCursor_CurrentUser() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/followers/ids.json?cursor=24680")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Friend_Or_Follower_Ids"), responseHeaders);
		
#if NET_4_0 || SILVERLIGHT_5
		    CursoredList<long> followerIds = twitter.FriendOperations.GetFollowerIdsInCursorAsync(24680).Result;
#else
            CursoredList<long> followerIds = twitter.FriendOperations.GetFollowerIdsInCursor(24680);
#endif
            AssertFriendFollowerIdsList(followerIds);
	    }

	    [Test]
	    [ExpectedException(typeof(NotAuthorizedException), 
            ExpectedMessage = "Authorization is required for the operation, but the API binding was created without authorization.")]
	    public void GetFollowerIds_CurrentUser_Unauthorized() 
        {
#if NET_4_0 || SILVERLIGHT_5
		    unauthorizedTwitter.FriendOperations.GetFollowerIdsAsync().Wait();
#else
            unauthorizedTwitter.FriendOperations.GetFollowerIds();
#endif
        }

	    [Test]
	    public void GetFollowerIds_ByUserId() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/followers/ids.json?cursor=-1&user_id=98765")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Friend_Or_Follower_Ids"), responseHeaders);
		
#if NET_4_0 || SILVERLIGHT_5
		    CursoredList<long> followerIds = twitter.FriendOperations.GetFollowerIdsAsync(98765L).Result;
#else
            CursoredList<long> followerIds = twitter.FriendOperations.GetFollowerIds(98765L);
#endif
            AssertFriendFollowerIdsList(followerIds);
	    }

	    [Test]
	    public void GetFollowerIdsInCursor_ByUserId() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/followers/ids.json?cursor=24680&user_id=98765")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Friend_Or_Follower_Ids"), responseHeaders);
		
#if NET_4_0 || SILVERLIGHT_5
		    CursoredList<long> followerIds = twitter.FriendOperations.GetFollowerIdsInCursorAsync(98765L, 24680).Result;
#else
            CursoredList<long> followerIds = twitter.FriendOperations.GetFollowerIdsInCursor(98765L, 24680);
#endif
            AssertFriendFollowerIdsList(followerIds);
	    }

	    [Test]
	    public void GetFollowerIds_ByScreenName() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/followers/ids.json?cursor=-1&screen_name=habuma")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Friend_Or_Follower_Ids"), responseHeaders);
		
#if NET_4_0 || SILVERLIGHT_5
		    CursoredList<long> followerIds = twitter.FriendOperations.GetFollowerIdsAsync("habuma").Result;
#else
            CursoredList<long> followerIds = twitter.FriendOperations.GetFollowerIds("habuma");
#endif
		    AssertFriendFollowerIdsList(followerIds);
	    }

	    [Test]
	    public void GetFollowerIdsInCursor_ByScreenName() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/followers/ids.json?cursor=24680&screen_name=habuma")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Friend_Or_Follower_Ids"), responseHeaders);
		
#if NET_4_0 || SILVERLIGHT_5
		    CursoredList<long> followerIds = twitter.FriendOperations.GetFollowerIdsInCursorAsync("habuma", 24680).Result;
#else
            CursoredList<long> followerIds = twitter.FriendOperations.GetFollowerIdsInCursor("habuma", 24680);
#endif
            AssertFriendFollowerIdsList(followerIds);
	    }

	    [Test]
	    public void Follow_ByUserId() 
        {
	        mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/friendships/create.json?user_id=98765")
	            .AndExpectMethod(HttpMethod.POST)
	            .AndRespondWith(JsonResource("Follow"), responseHeaders);
	    
#if NET_4_0 || SILVERLIGHT_5
            TwitterProfile followedUser = twitter.FriendOperations.FollowAsync(98765).Result;
#else
            TwitterProfile followedUser = twitter.FriendOperations.Follow(98765);
#endif
            Assert.AreEqual("oizik2", followedUser.ScreenName);
	    }
	
	    [Test]
	    [ExpectedException(typeof(NotAuthorizedException), 
            ExpectedMessage = "Authorization is required for the operation, but the API binding was created without authorization.")]
	    public void Follow_ByUserId_Unauthorized() 
        {
#if NET_4_0 || SILVERLIGHT_5
		    unauthorizedTwitter.FriendOperations.FollowAsync(98765).Wait();
#else
            unauthorizedTwitter.FriendOperations.Follow(98765);
#endif
        }
	
	    [Test]
	    public void Follow_ByScreenName() 
        {
	        mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/friendships/create.json?screen_name=oizik2")
	            .AndExpectMethod(HttpMethod.POST)
	            .AndRespondWith(JsonResource("Follow"), responseHeaders);
	    
#if NET_4_0 || SILVERLIGHT_5
            TwitterProfile followedUser = twitter.FriendOperations.FollowAsync("oizik2").Result;
#else
            TwitterProfile followedUser = twitter.FriendOperations.Follow("oizik2");
#endif
            Assert.AreEqual("oizik2", followedUser.ScreenName);
	    }
	
	    [Test]
	    [ExpectedException(typeof(NotAuthorizedException), 
            ExpectedMessage = "Authorization is required for the operation, but the API binding was created without authorization.")]
	    public void Follow_ByScreenName_Unauthorized() 
        {
#if NET_4_0 || SILVERLIGHT_5
            unauthorizedTwitter.FriendOperations.FollowAsync("aizik2").Wait();
#else
            unauthorizedTwitter.FriendOperations.Follow("aizik2");
#endif
        }
	
	    [Test]
	    public void Unfollow_ByUserId() 
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/friendships/destroy.json?user_id=98765")
                .AndExpectMethod(HttpMethod.POST)
                .AndRespondWith(JsonResource("Unfollow"), responseHeaders);
        
#if NET_4_0 || SILVERLIGHT_5
            TwitterProfile unFollowedUser = twitter.FriendOperations.UnfollowAsync(98765).Result;
#else
            TwitterProfile unFollowedUser = twitter.FriendOperations.Unfollow(98765);
#endif
            Assert.AreEqual("oizik2", unFollowedUser.ScreenName);
        }

	    [Test]
	    [ExpectedException(typeof(NotAuthorizedException), 
            ExpectedMessage = "Authorization is required for the operation, but the API binding was created without authorization.")]
	    public void Unfollow_ByUserId_Unauthorized() 
        {
#if NET_4_0 || SILVERLIGHT_5
		    unauthorizedTwitter.FriendOperations.UnfollowAsync(98765).Wait();
#else
            unauthorizedTwitter.FriendOperations.Unfollow(98765);
#endif
        }

	    [Test]
	    public void Unfollow_ByScreenName() 
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/friendships/destroy.json?screen_name=oizik2")
                .AndExpectMethod(HttpMethod.POST)
                .AndRespondWith(JsonResource("Unfollow"), responseHeaders);
        
#if NET_4_0 || SILVERLIGHT_5
            TwitterProfile unFollowedUser = twitter.FriendOperations.UnfollowAsync("oizik2").Result;
#else
            TwitterProfile unFollowedUser = twitter.FriendOperations.Unfollow("oizik2");
#endif
            Assert.AreEqual("oizik2", unFollowedUser.ScreenName);
        }
	
	    [Test]
	    [ExpectedException(typeof(NotAuthorizedException), 
            ExpectedMessage = "Authorization is required for the operation, but the API binding was created without authorization.")]
	    public void Unfollow_ByScreenName_Unauthorized() 
        {
#if NET_4_0 || SILVERLIGHT_5
            unauthorizedTwitter.FriendOperations.UnfollowAsync("aizik2").Wait();
#else
            unauthorizedTwitter.FriendOperations.Unfollow("aizik2");
#endif
        }

	    [Test]
	    public void EnableNotifications_ByUserId() 
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/notifications/follow.json?user_id=98765")
                .AndExpectMethod(HttpMethod.POST)
                .AndRespondWith(JsonResource("Follow"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    TwitterProfile unFollowedUser = twitter.FriendOperations.EnableNotificationsAsync(98765).Result;
#else
            TwitterProfile unFollowedUser = twitter.FriendOperations.EnableNotifications(98765);
#endif
            Assert.AreEqual("oizik2", unFollowedUser.ScreenName);
        }

	    [Test]
	    [ExpectedException(typeof(NotAuthorizedException), 
            ExpectedMessage = "Authorization is required for the operation, but the API binding was created without authorization.")]
	    public void EnableNotifications_ByUserId_Unauthorized() 
        {
#if NET_4_0 || SILVERLIGHT_5
		    unauthorizedTwitter.FriendOperations.EnableNotificationsAsync(98765).Wait();
#else
            unauthorizedTwitter.FriendOperations.EnableNotifications(98765);
#endif
        }
	
	    [Test]
	    public void EnableNotifications_ByScreenName() 
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/notifications/follow.json?screen_name=oizik2")
                .AndExpectMethod(HttpMethod.POST)
                .AndRespondWith(JsonResource("Follow"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    TwitterProfile unFollowedUser = twitter.FriendOperations.EnableNotificationsAsync("oizik2").Result;
#else
            TwitterProfile unFollowedUser = twitter.FriendOperations.EnableNotifications("oizik2");
#endif
            Assert.AreEqual("oizik2", unFollowedUser.ScreenName);
        }

	    [Test]
	    [ExpectedException(typeof(NotAuthorizedException), 
            ExpectedMessage = "Authorization is required for the operation, but the API binding was created without authorization.")]
	    public void EnableNotifications_ByScreenName_Unauthorized() 
        {
#if NET_4_0 || SILVERLIGHT_5
		    unauthorizedTwitter.FriendOperations.EnableNotificationsAsync("oizik2").Wait();
#else
            unauthorizedTwitter.FriendOperations.EnableNotifications("oizik2");
#endif
        }

	    [Test]
	    public void DisableNotifications_ByUserId() 
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/notifications/leave.json?user_id=98765")
                .AndExpectMethod(HttpMethod.POST)
                .AndRespondWith(JsonResource("Unfollow"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    TwitterProfile unFollowedUser = twitter.FriendOperations.DisableNotificationsAsync(98765).Result;
#else
            TwitterProfile unFollowedUser = twitter.FriendOperations.DisableNotifications(98765);
#endif
            Assert.AreEqual("oizik2", unFollowedUser.ScreenName);
        }

	    [Test]
	    [ExpectedException(typeof(NotAuthorizedException), 
            ExpectedMessage = "Authorization is required for the operation, but the API binding was created without authorization.")]
	    public void DisableNotifications_ByUserId_Unauthorized() 
        {
#if NET_4_0 || SILVERLIGHT_5
		    unauthorizedTwitter.FriendOperations.DisableNotificationsAsync(98765).Wait();
#else
            unauthorizedTwitter.FriendOperations.DisableNotifications(98765);
#endif
        }
	
	    [Test]
	    public void DisableNotifications_ByScreenName() 
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/notifications/leave.json?screen_name=oizik2")
                .AndExpectMethod(HttpMethod.POST)
                .AndRespondWith(JsonResource("Unfollow"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    TwitterProfile unFollowedUser = twitter.FriendOperations.DisableNotificationsAsync("oizik2").Result;
#else
            TwitterProfile unFollowedUser = twitter.FriendOperations.DisableNotifications("oizik2");
#endif
            Assert.AreEqual("oizik2", unFollowedUser.ScreenName);
        }
	
	    [Test]
	    [ExpectedException(typeof(NotAuthorizedException), 
            ExpectedMessage = "Authorization is required for the operation, but the API binding was created without authorization.")]
	    public void disableNotifications_ByScreenName_Unauthorized() 
        {
#if NET_4_0 || SILVERLIGHT_5
		    unauthorizedTwitter.FriendOperations.DisableNotificationsAsync("oizik2").Wait();
#else
            unauthorizedTwitter.FriendOperations.DisableNotifications("oizik2");
#endif
        }
	
	    [Test]
	    public void Exists() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/friendships/exists.json?screen_name_a=kdonald&screen_name_b=tinyrod")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith("true", responseHeaders);
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/friendships/exists.json?screen_name_a=royclarkson&screen_name_b=charliesheen")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith("false", responseHeaders);
		
#if NET_4_0 || SILVERLIGHT_5
		    Assert.IsTrue(twitter.FriendOperations.FriendshipExistsAsync("kdonald", "tinyrod").Result);
		    Assert.IsFalse(twitter.FriendOperations.FriendshipExistsAsync("royclarkson", "charliesheen").Result);
#else
            Assert.IsTrue(twitter.FriendOperations.FriendshipExists("kdonald", "tinyrod"));
		    Assert.IsFalse(twitter.FriendOperations.FriendshipExists("royclarkson", "charliesheen"));
#endif
	    }
	
	    [Test]
	    public void GetIncomingFriendships() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/friendships/incoming.json?cursor=-1")
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
                .AndExpectUri("https://api.twitter.com/1/friendships/incoming.json?cursor=1234567")
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
	    [ExpectedException(typeof(NotAuthorizedException), 
            ExpectedMessage = "Authorization is required for the operation, but the API binding was created without authorization.")]
	    public void GetIncomingFriendships_Unauthorized() 
        {
#if NET_4_0 || SILVERLIGHT_5
		    unauthorizedTwitter.FriendOperations.GetIncomingFriendshipsAsync().Wait();
#else
            unauthorizedTwitter.FriendOperations.GetIncomingFriendships();
#endif
        }
	
	    [Test]
	    public void GetOutgoingFriendships() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/friendships/outgoing.json?cursor=-1")
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
                .AndExpectUri("https://api.twitter.com/1/friendships/outgoing.json?cursor=9876543")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Incoming_Or_Outgoing_Friendships"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    CursoredList<long> friendships = twitter.FriendOperations.GetOutgoingFriendshipsAsync(9876543).Result;
#else
            CursoredList<long> friendships = twitter.FriendOperations.GetOutgoingFriendships(9876543);
#endif
            AssertIncomingOutgoingFriendships(friendships);
	    }

	    [Test]
	    [ExpectedException(typeof(NotAuthorizedException), 
            ExpectedMessage = "Authorization is required for the operation, but the API binding was created without authorization.")]
	    public void GetOutgoingFriendships_Unauthorized() 
        {
#if NET_4_0 || SILVERLIGHT_5
		    unauthorizedTwitter.FriendOperations.GetOutgoingFriendshipsAsync().Wait();
#else
            unauthorizedTwitter.FriendOperations.GetOutgoingFriendships();
#endif
        }


        // test helpers
        
	    private void AssertFriendFollowerIdsList(CursoredList<long> friendIds) 
        {
		    Assert.AreEqual(2, friendIds.Count);
		    Assert.AreEqual(14846645L, friendIds[0]);
		    Assert.AreEqual(14718006L, friendIds[1]);
		    Assert.AreEqual(112233, friendIds.PreviousCursor);
		    Assert.AreEqual(332211, friendIds.NextCursor);
	    }

	    private void AssertFriendsFollowers(CursoredList<TwitterProfile> friends) 
        {
		    Assert.AreEqual(2, friends.Count);
		    Assert.AreEqual("royclarkson", friends[0].ScreenName);
		    Assert.AreEqual("kdonald", friends[1].ScreenName);
		    Assert.AreEqual(112233, friends.PreviousCursor);
		    Assert.AreEqual(332211, friends.NextCursor);
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
