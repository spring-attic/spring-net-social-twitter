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
    /// Unit tests for the UserTemplate class.
    /// </summary>
    /// <author>Craig Walls</author>
    /// <author>Bruno Baia (.NET)</author>
    [TestFixture]
    public class UserTemplateTests : AbstractTwitterOperationsTests 
    {    
	    [Test]
	    public void GetUserProfile()
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/account/verify_credentials.json")
				.AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("Twitter_Profile"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    TwitterProfile profile = twitter.UserOperations.GetUserProfileAsync().Result;
#else
            TwitterProfile profile = twitter.UserOperations.GetUserProfile();
#endif
		    Assert.AreEqual(161064614, profile.ID);
		    Assert.AreEqual("artnames", profile.ScreenName);
		    Assert.AreEqual("Art Names", profile.Name);
		    Assert.AreEqual("I'm just a normal kinda guy", profile.Description);
		    Assert.AreEqual("Denton, TX", profile.Location);
		    Assert.AreEqual("https://www.springsource.org", profile.Url);
		    Assert.AreEqual("https://a1.twimg.com/sticky/default_profile_images/default_profile_4_normal.png", profile.ProfileImageUrl);
		    Assert.IsTrue(profile.IsNotificationsEnabled);
		    Assert.IsFalse(profile.IsVerified);
		    Assert.IsTrue(profile.IsGeoEnabled);
		    Assert.IsTrue(profile.IsContributorsEnabled);
		    Assert.IsTrue(profile.IsTranslator);
		    Assert.IsTrue(profile.IsFollowing);
		    Assert.IsTrue(profile.IsFollowRequestSent);
		    Assert.IsTrue(profile.IsProtected);
		    Assert.AreEqual("en", profile.Language);
		    Assert.AreEqual(125, profile.StatusesCount);
		    Assert.AreEqual(1001, profile.ListedCount);
		    Assert.AreEqual(14, profile.FollowersCount);
		    Assert.AreEqual(194, profile.FriendsCount);
		    Assert.AreEqual(4, profile.FavoritesCount);
		    Assert.AreEqual("Mountain Time (US & Canada)", profile.TimeZone);
		    Assert.AreEqual(-25200, profile.UtcOffset);
		    Assert.IsTrue(profile.UseBackgroundImage);
		    Assert.AreEqual("C0DEED", profile.SidebarBorderColor);
		    Assert.AreEqual("DDEEF6", profile.SidebarFillColor);
		    Assert.AreEqual("C0DEED", profile.BackgroundColor);
		    Assert.AreEqual("https://a3.twimg.com/a/1301419075/images/themes/theme1/bg.png", profile.BackgroundImageUrl);
		    Assert.IsFalse(profile.IsBackgroundImageTiled);
		    Assert.AreEqual("333333", profile.TextColor);
		    Assert.AreEqual("0084B4", profile.LinkColor);
	    }

	    [Test]
	    public void GetUserProfile_UserId() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/users/show.json?user_id=12345")
				.AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("Twitter_Profile"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    TwitterProfile profile = twitter.UserOperations.GetUserProfileAsync(12345).Result;
#else
            TwitterProfile profile = twitter.UserOperations.GetUserProfile(12345);
#endif
		    Assert.AreEqual(161064614, profile.ID);
		    Assert.AreEqual("artnames", profile.ScreenName);
		    Assert.AreEqual("Art Names", profile.Name);
		    Assert.AreEqual("I'm just a normal kinda guy", profile.Description);
		    Assert.AreEqual("Denton, TX", profile.Location);
		    Assert.AreEqual("https://www.springsource.org", profile.Url);
		    Assert.AreEqual("https://a1.twimg.com/sticky/default_profile_images/default_profile_4_normal.png", profile.ProfileImageUrl);
	    }

	    [Test]
	    public void GetUsers_ByUserId() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/users/lookup.json?user_id=14846645%2C14718006")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("List_Of_Profiles"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    IList<TwitterProfile> users = twitter.UserOperations.GetUsersAsync(14846645, 14718006).Result;
#else
            IList<TwitterProfile> users = twitter.UserOperations.GetUsers(14846645, 14718006);
#endif
            Assert.AreEqual(2, users.Count);
		    Assert.AreEqual("royclarkson", users[0].ScreenName);
		    Assert.AreEqual("kdonald", users[1].ScreenName);
	    }
	
	    [Test]
	    public void GetUsers_ByScreenName() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/users/lookup.json?screen_name=royclarkson%2Ckdonald")
			    .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("List_Of_Profiles"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    IList<TwitterProfile> users = twitter.UserOperations.GetUsersAsync("royclarkson", "kdonald").Result;
#else
            IList<TwitterProfile> users = twitter.UserOperations.GetUsers("royclarkson", "kdonald");
#endif
            Assert.AreEqual(2, users.Count);
		    Assert.AreEqual("royclarkson", users[0].ScreenName);
		    Assert.AreEqual("kdonald", users[1].ScreenName);
	    }
	
	    [Test]
	    public void SearchForUsers() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/users/search.json?page=1&per_page=20&q=some%20query")
			    .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("List_Of_Profiles"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    IList<TwitterProfile> users = twitter.UserOperations.SearchForUsersAsync("some query").Result;
#else
            IList<TwitterProfile> users = twitter.UserOperations.SearchForUsers("some query");
#endif
            Assert.AreEqual(2, users.Count);
		    Assert.AreEqual("royclarkson", users[0].ScreenName);
		    Assert.AreEqual("kdonald", users[1].ScreenName);
	    }

	    [Test]
	    public void SearchForUsers_Paged() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/users/search.json?page=3&per_page=35&q=some%20query")
			    .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("List_Of_Profiles"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    IList<TwitterProfile> users = twitter.UserOperations.SearchForUsersAsync("some query", 3, 35).Result;
#else
            IList<TwitterProfile> users = twitter.UserOperations.SearchForUsers("some query", 3, 35);
#endif
            Assert.AreEqual(2, users.Count);
		    Assert.AreEqual("royclarkson", users[0].ScreenName);
		    Assert.AreEqual("kdonald", users[1].ScreenName);
	    }
	
	    [Test]
	    public void GetSuggestionCategories() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/users/suggestions.json")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Suggestion_Categories"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    IList<SuggestionCategory> categories = twitter.UserOperations.GetSuggestionCategoriesAsync().Result;
#else
            IList<SuggestionCategory> categories = twitter.UserOperations.GetSuggestionCategories();
#endif
            Assert.AreEqual(4, categories.Count);
		    Assert.AreEqual("Art & Design", categories[0].Name);
		    Assert.AreEqual("art-design", categories[0].Slug);
		    Assert.AreEqual(56, categories[0].Size);
		    Assert.AreEqual("Books", categories[1].Name);
		    Assert.AreEqual("books", categories[1].Slug);
		    Assert.AreEqual(72, categories[1].Size);
		    Assert.AreEqual("Business", categories[2].Name);
		    Assert.AreEqual("business", categories[2].Slug);
		    Assert.AreEqual(65, categories[2].Size);
		    Assert.AreEqual("Twitter", categories[3].Name);
		    Assert.AreEqual("twitter", categories[3].Slug);
		    Assert.AreEqual(16, categories[3].Size);
	    }
	
	    [Test]
	    public void GetSuggestions() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/users/suggestions/springsource.json")
			    .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("Suggestions"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    IList<TwitterProfile> users = twitter.UserOperations.GetSuggestionsAsync("springsource").Result;
#else
            IList<TwitterProfile> users = twitter.UserOperations.GetSuggestions("springsource");
#endif
            Assert.AreEqual(2, users.Count);
		    Assert.AreEqual("royclarkson", users[0].ScreenName);
		    Assert.AreEqual("kdonald", users[1].ScreenName);
	    }
	
	    [Test]
	    public void GetRateLimitStatus() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/application/rate_limit_status.json?resources=help%2Cusers%2Csearch%2Cstatuses")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Rate_Limit_Status"), responseHeaders);
		
#if NET_4_0 || SILVERLIGHT_5
		    IList<RateLimitStatus> limits = twitter.UserOperations.GetRateLimitStatusAsync("help", "users", "search", "statuses").Result;
#else
            IList<RateLimitStatus> limits = twitter.UserOperations.GetRateLimitStatus("help", "users", "search", "statuses");
#endif
            Assert.AreEqual(19, limits.Count);
            Assert.AreEqual("help", limits[0].ResourceFamily);
            Assert.AreEqual("/help/privacy", limits[0].ResourceEndpoint);
            Assert.AreEqual(15, limits[0].WindowLimit);
            Assert.AreEqual(15, limits[0].RemainingHits);
            Assert.AreEqual("31/08/2012 18:58:47", limits[0].ResetTime.ToUniversalTime().ToString("dd/MM/yyyy HH:mm:ss"));
	    }
    }
}
