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

using System.Net;
using System.Collections.Generic;

using NUnit.Framework;
using Spring.Rest.Client.Testing;

using Spring.Http;

namespace Spring.Social.Twitter.Api.Impl
{
    /// <summary>
    /// Unit tests for the ListTemplate class.
    /// </summary>
    /// <author>Craig Walls</author>
    /// <author>Bruno Baia (.NET)</author>
    [TestFixture]
    public class ListTemplateTests : AbstractTwitterOperationsTests
    {
        [Test]
        public void GetLists_CurrentUser()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/lists/list.json")
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("List_Of_Lists"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            AssertListOfLists(twitter.ListOperations.GetListsAsync().Result);
#else
            AssertListOfLists(twitter.ListOperations.GetLists());
#endif
        }

        [Test]
        public void GetLists_ById()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/lists/list.json?user_id=161064614")
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("List_Of_Lists"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            AssertListOfLists(twitter.ListOperations.GetListsAsync(161064614).Result);
#else
            AssertListOfLists(twitter.ListOperations.GetLists(161064614));
#endif
        }

        [Test]
        public void GetLists_ByScreenName() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/lists/list.json?screen_name=habuma")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("List_Of_Lists"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            AssertListOfLists(twitter.ListOperations.GetListsAsync("habuma").Result);
#else
            AssertListOfLists(twitter.ListOperations.GetLists("habuma"));
#endif
        }

        [Test]
        public void GetList_ByListId()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/lists/show.json?list_id=40841803")
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("Single_List"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            AssertSingleList(twitter.ListOperations.GetListAsync(40841803).Result);
#else
            AssertSingleList(twitter.ListOperations.GetList(40841803));
#endif
        }

        [Test]
        public void CreateList_PublicListForUserId()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/lists/create.json")
                .AndExpectMethod(HttpMethod.POST)
                .AndExpectBody("name=forfun&description=Just+for+Fun&mode=public")
                .AndRespondWith(JsonResource("Single_List"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            AssertSingleList(twitter.ListOperations.CreateListAsync("forfun", "Just for Fun", true).Result);
#else
            AssertSingleList(twitter.ListOperations.CreateList("forfun", "Just for Fun", true));
#endif
        }

        [Test]
        public void CreateList_PrivateListForUserId()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/lists/create.json")
                .AndExpectMethod(HttpMethod.POST)
                .AndExpectBody("name=forfun2&description=Just+for+Fun%2C+too&mode=private")
                .AndRespondWith(JsonResource("Single_List"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            AssertSingleList(twitter.ListOperations.CreateListAsync("forfun2", "Just for Fun, too", false).Result);
#else
            AssertSingleList(twitter.ListOperations.CreateList("forfun2", "Just for Fun, too", false));
#endif
        }

        [Test]
        public void UpdateList_PublicListForUserId()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/lists/update.json")
                .AndExpectMethod(HttpMethod.POST)
                .AndExpectBody("name=forfun&description=Just+for+Fun&mode=public&list_id=40841803")
                .AndRespondWith(JsonResource("Single_List"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            AssertSingleList(twitter.ListOperations.UpdateListAsync(40841803, "forfun", "Just for Fun", true).Result);
#else
            AssertSingleList(twitter.ListOperations.UpdateList(40841803, "forfun", "Just for Fun", true));
#endif
        }

        [Test]
        public void UpdateList_PrivateListForUserId()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/lists/update.json")
                .AndExpectMethod(HttpMethod.POST)
                .AndExpectBody("name=forfun2&description=Just+for+Fun%2C+too&mode=private&list_id=40841803")
                .AndRespondWith(JsonResource("Single_List"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            AssertSingleList(twitter.ListOperations.UpdateListAsync(40841803, "forfun2", "Just for Fun, too", false).Result);
#else
            AssertSingleList(twitter.ListOperations.UpdateList(40841803, "forfun2", "Just for Fun, too", false));
#endif
        }

        [Test]
        public void DeleteList_ForUserIdByListId()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/lists/destroy.json")
                .AndExpectMethod(HttpMethod.POST)
                .AndExpectBody("list_id=40841803")
                .AndRespondWith(JsonResource("Single_List"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            UserList deletedList = twitter.ListOperations.DeleteListAsync(40841803).Result;
#else
            UserList deletedList = twitter.ListOperations.DeleteList(40841803);
#endif
            AssertSingleList(deletedList);
        }

        [Test]
        public void GetListMembers_ByUserIdAndListId()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/lists/members.json?list_id=40841803")
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("List_Members"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            AssertListMembers(twitter.ListOperations.GetListMembersAsync(40841803).Result);
#else
            AssertListMembers(twitter.ListOperations.GetListMembers(40841803));
#endif
        }

        [Test]
        public void GetListMembers_ByScreenNameAndListSlug() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/lists/members.json?owner_screen_name=habuma&slug=forfun")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("List_Members"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    AssertListMembers(twitter.ListOperations.GetListMembersAsync("habuma", "forfun").Result);
#else
            AssertListMembers(twitter.ListOperations.GetListMembers("habuma", "forfun"));
#endif
        }

        [Test]
        public void AddToList_ForUserIdListIdSingle()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/lists/members/create_all.json")
                .AndExpectMethod(HttpMethod.POST)
                .AndExpectBody("user_id=123456&list_id=40841803")
                .AndRespondWith(JsonResource("Single_List"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            AssertSingleList(twitter.ListOperations.AddToListAsync(40841803, 123456).Result);
#else
            AssertSingleList(twitter.ListOperations.AddToList(40841803, 123456));
#endif
        }

        [Test]
        public void AddToList_ForUserIdListIdMultiple()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/lists/members/create_all.json")
                .AndExpectMethod(HttpMethod.POST)
                .AndExpectBody("user_id=123456%2C234567%2C345678&list_id=40841803")
                .AndRespondWith(JsonResource("Single_List"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            AssertSingleList(twitter.ListOperations.AddToListAsync(40841803, 123456, 234567, 345678).Result);
#else
            AssertSingleList(twitter.ListOperations.AddToList(40841803, 123456, 234567, 345678));
#endif
        }

        [Test]
        public void AddToList_ForScreenNameMultiple()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/lists/members/create_all.json")
                .AndExpectMethod(HttpMethod.POST)
                .AndExpectBody("screen_name=habuma%2Croyclarkson&list_id=40841803")
                .AndRespondWith(JsonResource("Single_List"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            AssertSingleList(twitter.ListOperations.AddToListAsync(40841803, "habuma", "royclarkson").Result);
#else
            AssertSingleList(twitter.ListOperations.AddToList(40841803, "habuma", "royclarkson"));
#endif
        }

        [Test]
        public void RemoveFromList_OwnerIdListIdMemberId()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/lists/members/destroy.json")
                .AndExpectMethod(HttpMethod.POST)
                .AndExpectBody("user_id=12345&list_id=40841803")
                .AndRespondWith("{}", responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            twitter.ListOperations.RemoveFromListAsync(40841803, 12345).Wait();
#else
            twitter.ListOperations.RemoveFromList(40841803, 12345);
#endif
        }

        [Test]
        public void RemoveFromList_ScreenName()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/lists/members/destroy.json")
                .AndExpectMethod(HttpMethod.POST)
                .AndExpectBody("screen_name=habuma&list_id=40841803")
                .AndRespondWith("{}", responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            twitter.ListOperations.RemoveFromListAsync(40841803, "habuma").Wait();
#else
            twitter.ListOperations.RemoveFromList(40841803, "habuma");
#endif
        }

        [Test]
        public void GetListSubscribers_ByUserIdAndListId()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/lists/subscribers.json?list_id=40841803")
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("List_Members"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            AssertListMembers(twitter.ListOperations.GetListSubscribersAsync(40841803).Result);
#else
            AssertListMembers(twitter.ListOperations.GetListSubscribers(40841803));
#endif
        }

        [Test]
        public void GetListSubscribers_ByScreenNameAndListSlug() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/lists/subscribers.json?owner_screen_name=habuma&slug=forfun")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("List_Members"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    AssertListMembers(twitter.ListOperations.GetListSubscribersAsync("habuma", "forfun").Result);
#else
            AssertListMembers(twitter.ListOperations.GetListSubscribers("habuma", "forfun"));
#endif
	    }

        [Test]
        public void GetMemberships_ForUserId()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/lists/memberships.json?user_id=161064614")
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("CursoredList_Of_Lists"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            AssertCursoredListOfLists(twitter.ListOperations.GetMembershipsAsync(161064614).Result);
#else
            AssertCursoredListOfLists(twitter.ListOperations.GetMemberships(161064614));
#endif
        }

        [Test]
        public void GetMemberships_ForScreenName() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/lists/memberships.json?screen_name=habuma")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("CursoredList_Of_Lists"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    AssertCursoredListOfLists(twitter.ListOperations.GetMembershipsAsync("habuma").Result);
#else
            AssertCursoredListOfLists(twitter.ListOperations.GetMemberships("habuma"));
#endif
        }

        [Test]
        public void GetSubscriptions_ForUserId()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/lists/subscriptions.json?user_id=161064614")
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("CursoredList_Of_Lists"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            AssertCursoredListOfLists(twitter.ListOperations.GetSubscriptionsAsync(161064614).Result);
#else
            AssertCursoredListOfLists(twitter.ListOperations.GetSubscriptions(161064614));
#endif
        }

        [Test]
        public void GetSubscriptions_ForScreenName() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/lists/subscriptions.json?screen_name=habuma")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("CursoredList_Of_Lists"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    AssertCursoredListOfLists(twitter.ListOperations.GetSubscriptionsAsync("habuma").Result);
#else
            AssertCursoredListOfLists(twitter.ListOperations.GetSubscriptions("habuma"));
#endif
        }

        [Test]
        public void IsMember_ByUserId()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/lists/members/show.json?list_id=40841803&user_id=123456")
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("Twitter_Profile"), responseHeaders);
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/lists/members/show.json?list_id=40841803&user_id=987654")
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith("{}", responseHeaders, HttpStatusCode.NotFound, "");

#if NET_4_0 || SILVERLIGHT_5
            Assert.IsTrue(twitter.ListOperations.IsMemberAsync(40841803, 123456).Result);
            Assert.IsFalse(twitter.ListOperations.IsMemberAsync(40841803, 987654).Result);
#else
            Assert.IsTrue(twitter.ListOperations.IsMember(40841803, 123456));
            Assert.IsFalse(twitter.ListOperations.IsMember(40841803, 987654));
#endif
        }

        [Test]
        public void IsMember_ByScreenName() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/lists/members/show.json?owner_screen_name=habuma&slug=forfun&screen_name=royclarkson")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Twitter_Profile"), responseHeaders);
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/lists/members/show.json?owner_screen_name=habuma&slug=forfun&screen_name=kdonald")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith("{}", responseHeaders, HttpStatusCode.NotFound, "");

#if NET_4_0 || SILVERLIGHT_5
		    Assert.IsTrue(twitter.ListOperations.IsMemberAsync("habuma", "forfun", "royclarkson").Result);
		    Assert.IsFalse(twitter.ListOperations.IsMemberAsync("habuma", "forfun", "kdonald").Result);
#else
            Assert.IsTrue(twitter.ListOperations.IsMember("habuma", "forfun", "royclarkson"));
		    Assert.IsFalse(twitter.ListOperations.IsMember("habuma", "forfun", "kdonald"));
#endif
	    }

        [Test]
        public void IsSubscriber_ByUserId()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/lists/subscribers/show.json?list_id=40841803&user_id=123456")
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("Twitter_Profile"), responseHeaders);
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/lists/subscribers/show.json?list_id=40841803&user_id=987654")
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith("{}", responseHeaders, HttpStatusCode.NotFound, "");

#if NET_4_0 || SILVERLIGHT_5
            Assert.IsTrue(twitter.ListOperations.IsSubscriberAsync(40841803, 123456).Result);
            Assert.IsFalse(twitter.ListOperations.IsSubscriberAsync(40841803, 987654).Result);
#else
            Assert.IsTrue(twitter.ListOperations.IsSubscriber(40841803, 123456));
            Assert.IsFalse(twitter.ListOperations.IsSubscriber(40841803, 987654));
#endif
        }

        [Test]
        public void IsSubscriber_ByScreenName() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/lists/subscribers/show.json?owner_screen_name=habuma&slug=forfun&screen_name=royclarkson")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Twitter_Profile"), responseHeaders);
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/lists/subscribers/show.json?owner_screen_name=habuma&slug=forfun&screen_name=kdonald")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith("{}", responseHeaders, HttpStatusCode.NotFound, "");

#if NET_4_0 || SILVERLIGHT_5
		    Assert.IsTrue(twitter.ListOperations.IsSubscriberAsync("habuma", "forfun", "royclarkson").Result);
		    Assert.IsFalse(twitter.ListOperations.IsSubscriberAsync("habuma", "forfun", "kdonald").Result);
#else
            Assert.IsTrue(twitter.ListOperations.IsSubscriber("habuma", "forfun", "royclarkson"));
		    Assert.IsFalse(twitter.ListOperations.IsSubscriber("habuma", "forfun", "kdonald"));
#endif
	    }

        [Test]
        public void Subscribe()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/lists/subscribers/create.json")
                .AndExpectMethod(HttpMethod.POST)
                .AndExpectBody("list_id=54321")
                .AndRespondWith(JsonResource("Single_List"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            UserList list = twitter.ListOperations.SubscribeAsync(54321).Result;
#else
            UserList list = twitter.ListOperations.Subscribe(54321);
#endif
            AssertSingleList(list);
        }

        [Test]
        public void Subscribe_usernameAndSlug()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/lists/subscribers/create.json")
                .AndExpectMethod(HttpMethod.POST)
                .AndExpectBody("owner_screen_name=habuma&slug=somelist")
                .AndRespondWith(JsonResource("Single_List"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            UserList list = twitter.ListOperations.SubscribeAsync("habuma", "somelist").Result;
#else
            UserList list = twitter.ListOperations.Subscribe("habuma", "somelist");
#endif
            AssertSingleList(list);
        }

        [Test]
        public void Unsubscribe()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/lists/subscribers/destroy.json")
                .AndExpectMethod(HttpMethod.POST)
                .AndExpectBody("list_id=54321")
                .AndRespondWith(JsonResource("Single_List"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            UserList list = twitter.ListOperations.UnsubscribeAsync(54321).Result;
#else
            UserList list = twitter.ListOperations.Unsubscribe(54321);
#endif
            AssertSingleList(list);
        }

        [Test]
        public void Unsubscribe_UsernameAndSlug()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/lists/subscribers/destroy.json")
                .AndExpectMethod(HttpMethod.POST)
                .AndExpectBody("owner_screen_name=habuma&slug=somelist")
                .AndRespondWith(JsonResource("Single_List"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            UserList list = twitter.ListOperations.UnsubscribeAsync("habuma", "somelist").Result;
#else
            UserList list = twitter.ListOperations.Unsubscribe("habuma", "somelist");
#endif
            AssertSingleList(list);
        }

        [Test]
        public void GetListStatuses_ListId()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/lists/statuses.json?list_id=1234")
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> timeline = twitter.ListOperations.GetListStatusesAsync(1234).Result;
#else
            IList<Tweet> timeline = twitter.ListOperations.GetListStatuses(1234);
#endif
            AssertTimelineTweets(timeline);
        }

        [Test]
        public void GetListStatuses_ListId_WithCount()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/lists/statuses.json?count=30&list_id=1234")
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> timeline = twitter.ListOperations.GetListStatusesAsync(1234, 30).Result;
#else
            IList<Tweet> timeline = twitter.ListOperations.GetListStatuses(1234, 30);
#endif
            AssertTimelineTweets(timeline);
        }

        [Test]
        public void GetListStatuses_ListId_WithCountAndSinceIdAndMaxId()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/lists/statuses.json?count=30&since_id=12345&max_id=54321&list_id=1234")
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> timeline = twitter.ListOperations.GetListStatusesAsync(1234, 30, 12345, 54321).Result;
#else
            IList<Tweet> timeline = twitter.ListOperations.GetListStatuses(1234, 30, 12345, 54321);
#endif
            AssertTimelineTweets(timeline);
        }

        [Test]
        public void GetListStatuses_Slug()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/lists/statuses.json?owner_screen_name=habuma&slug=mylist")
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> timeline = twitter.ListOperations.GetListStatusesAsync("habuma", "mylist").Result;
#else
            IList<Tweet> timeline = twitter.ListOperations.GetListStatuses("habuma", "mylist");
#endif
            AssertTimelineTweets(timeline);
        }

        [Test]
        public void GetListStatuses_Slug_WithCount()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/lists/statuses.json?count=30&owner_screen_name=habuma&slug=mylist")
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> timeline = twitter.ListOperations.GetListStatusesAsync("habuma", "mylist", 30).Result;
#else
            IList<Tweet> timeline = twitter.ListOperations.GetListStatuses("habuma", "mylist", 30);
#endif
            AssertTimelineTweets(timeline);
        }

        [Test]
        public void GetListStatuses_Slug_WithCountAndSinceIdAndMaxId()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/lists/statuses.json?count=30&since_id=12345&max_id=54321&owner_screen_name=habuma&slug=mylist")
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("Timeline"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Tweet> timeline = twitter.ListOperations.GetListStatusesAsync("habuma", "mylist", 30, 12345, 54321).Result;
#else
            IList<Tweet> timeline = twitter.ListOperations.GetListStatuses("habuma", "mylist", 30, 12345, 54321);
#endif
            AssertTimelineTweets(timeline);
        }


        // test helpers

        private void AssertSingleList(UserList list)
        {
            Assert.AreEqual(40841803, list.ID);
            Assert.AreEqual("forFun", list.Name);
            Assert.AreEqual("@habuma/forfun", list.FullName);
            Assert.AreEqual("forfun", list.Slug);
            Assert.AreEqual("Just for fun", list.Description);
            Assert.AreEqual(22, list.MemberCount);
            Assert.AreEqual(100, list.SubscriberCount);
            Assert.AreEqual("/habuma/forfun", list.UriPath);
            Assert.IsFalse(list.IsPublic);
        }

        private void AssertCursoredListOfLists(CursoredList<UserList> lists)
        {
            AssertListOfLists(lists);
            Assert.AreEqual(123456, lists.PreviousCursor);
            Assert.AreEqual(234567, lists.NextCursor);
        }

        private void AssertListOfLists(IList<UserList> lists)
        {
            Assert.AreEqual(2, lists.Count);
            UserList list1 = lists[0];
            Assert.AreEqual(40842137, list1.ID);
            Assert.AreEqual("forFun2", list1.Name);
            Assert.AreEqual("@habuma/forfun2", list1.FullName);
            Assert.AreEqual("forfun2", list1.Slug);
            Assert.AreEqual("Just for fun, too", list1.Description);
            Assert.AreEqual(3, list1.MemberCount);
            Assert.AreEqual(0, list1.SubscriberCount);
            Assert.AreEqual("/habuma/forfun2", list1.UriPath);
            Assert.IsTrue(list1.IsPublic);
            UserList list2 = lists[1];
            Assert.AreEqual(40841803, list2.ID);
            Assert.AreEqual("forFun", list2.Name);
            Assert.AreEqual("@habuma/forfun", list2.FullName);
            Assert.AreEqual("forfun", list2.Slug);
            Assert.AreEqual("Just for fun", list2.Description);
            Assert.AreEqual(22, list2.MemberCount);
            Assert.AreEqual(100, list2.SubscriberCount);
            Assert.AreEqual("/habuma/forfun", list2.UriPath);
            Assert.IsFalse(list2.IsPublic);
        }

        private void AssertListMembers(IList<TwitterProfile> members)
        {
            Assert.AreEqual(2, members.Count);
            TwitterProfile profile1 = members[0];
            Assert.AreEqual(14846645, profile1.ID);
            Assert.AreEqual("royclarkson", profile1.ScreenName);
            Assert.AreEqual("Roy Clarkson", profile1.Name);
            Assert.AreEqual("Follower of mobile, social, and web technology trends. I write lots of code, and work at SpringSource.", profile1.Description);
            Assert.AreEqual("Atlanta, GA, USA", profile1.Location);
            TwitterProfile profile2 = members[1];
            Assert.AreEqual(14718006, profile2.ID);
            Assert.AreEqual("kdonald", profile2.ScreenName);
            Assert.AreEqual("Keith Donald", profile2.Name);
            Assert.AreEqual("SpringSource co-founder", profile2.Description);
            Assert.AreEqual("Melbourne, Fl", profile2.Location);
        }
    }
}
