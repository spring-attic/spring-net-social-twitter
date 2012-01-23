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
    /// Unit tests for the SearchTemplate class.
    /// </summary>
    /// <author>Craig Walls</author>
    /// <author>Bruno Baia (.NET)</author>
    [TestFixture]
    public class SearchTemplateTests : AbstractTwitterOperationsTests 
    {    
        [Test]
	    public void Search_QueryOnly() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://search.twitter.com/search.json?q=%23spring&rpp=50&page=1")
				.AndExpectMethod(HttpMethod.GET)
				.AndRespondWith(JsonResource("Search"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            SearchResults searchResults = twitter.SearchOperations.SearchAsync("#spring").Result;
#else
		    SearchResults searchResults = twitter.SearchOperations.Search("#spring");
#endif
		    Assert.AreEqual(10, searchResults.SinceId);
		    Assert.AreEqual(999, searchResults.MaxId);
		    IList<Tweet> tweets = searchResults.Tweets;
		    AssertSearchTweets(tweets);
	    }

	    [Test]
	    public void Search_PageAndResultsPerPage() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://search.twitter.com/search.json?q=%23spring&rpp=10&page=2")
				.AndExpectMethod(HttpMethod.GET)
				.AndRespondWith(JsonResource("Search"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    SearchResults searchResults = twitter.SearchOperations.SearchAsync("#spring", 2, 10).Result;
#else
            SearchResults searchResults = twitter.SearchOperations.Search("#spring", 2, 10);
#endif
		    Assert.AreEqual(10, searchResults.SinceId);
		    Assert.AreEqual(999, searchResults.MaxId);
		    IList<Tweet> tweets = searchResults.Tweets;
		    AssertSearchTweets(tweets);
	    }

	    [Test]
	    public void Search_SinceAndMaxId() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://search.twitter.com/search.json?q=%23spring&rpp=10&page=2&since_id=123&max_id=54321")
				.AndExpectMethod(HttpMethod.GET)
				.AndRespondWith(JsonResource("Search"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    SearchResults searchResults = twitter.SearchOperations.SearchAsync("#spring", 2, 10, 123, 54321).Result;
#else
            SearchResults searchResults = twitter.SearchOperations.Search("#spring", 2, 10, 123, 54321);
#endif
		    Assert.AreEqual(10, searchResults.SinceId);
		    Assert.AreEqual(999, searchResults.MaxId);
		    IList<Tweet> tweets = searchResults.Tweets;
		    AssertSearchTweets(tweets);
	    }
	
	    [Test]
	    public void GetSavedSearches() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/saved_searches.json")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Saved_Searches"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    IList<SavedSearch> savedSearches = twitter.SearchOperations.GetSavedSearchesAsync().Result;
#else
            IList<SavedSearch> savedSearches = twitter.SearchOperations.GetSavedSearches();
#endif
		    Assert.AreEqual(2, savedSearches.Count);
		    SavedSearch search1 = savedSearches[0];
		    Assert.AreEqual(26897775, search1.ID);
		    Assert.AreEqual("#springsocial", search1.Query);
		    Assert.AreEqual("#springsocial", search1.Name);
		    Assert.AreEqual(0, search1.Position);
		    SavedSearch search2 = savedSearches[1];
		    Assert.AreEqual(56897772, search2.ID);
		    Assert.AreEqual("#twitter", search2.Query);
		    Assert.AreEqual("#twitter", search2.Name);
		    Assert.AreEqual(1, search2.Position);
	    }

        [Test]
	    [ExpectedException(typeof(TwitterApiException), 
            ExpectedMessage = "Authorization is required for the operation, but the API binding was created without authorization.")]
	    public void GetSavedSearches_Unauthorized() 
        {
#if NET_4_0 || SILVERLIGHT_5
		    unauthorizedTwitter.SearchOperations.GetSavedSearchesAsync().Wait();
#else
            unauthorizedTwitter.SearchOperations.GetSavedSearches();
#endif
	    }

	    [Test]
	    public void GetSavedSearch() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/saved_searches/show/26897775.json")
				.AndExpectMethod(HttpMethod.GET)
				.AndRespondWith(JsonResource("Saved_Search"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    SavedSearch savedSearch = twitter.SearchOperations.GetSavedSearchAsync(26897775).Result;
#else
            SavedSearch savedSearch = twitter.SearchOperations.GetSavedSearch(26897775);
#endif
		    Assert.AreEqual(26897775, savedSearch.ID);
		    Assert.AreEqual("#springsocial", savedSearch.Query);
		    Assert.AreEqual("#springsocial", savedSearch.Name);
		    Assert.AreEqual(0, savedSearch.Position);
	    }
	
        [Test]
	    [ExpectedException(typeof(TwitterApiException), 
            ExpectedMessage = "Authorization is required for the operation, but the API binding was created without authorization.")]
	    public void GetSavedSearch_Unauthorized() 
        {
#if NET_4_0 || SILVERLIGHT_5
		    unauthorizedTwitter.SearchOperations.GetSavedSearchAsync(26897775).Wait();
#else
            unauthorizedTwitter.SearchOperations.GetSavedSearch(26897775);
#endif
	    }

	    [Test]
	    public void CreateSavedSearch() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/saved_searches/create.json")
			    .AndExpectMethod(HttpMethod.POST)
			    .AndExpectBody("query=%23twitter")
			    .AndRespondWith(JsonResource("Saved_Search"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    SavedSearch savedSearch = twitter.SearchOperations.CreateSavedSearchAsync("#twitter").Result;
#else
            SavedSearch savedSearch = twitter.SearchOperations.CreateSavedSearch("#twitter");
#endif
		    Assert.AreEqual(26897775, savedSearch.ID);
		    Assert.AreEqual("#springsocial", savedSearch.Query);
		    Assert.AreEqual("#springsocial", savedSearch.Name);
		    Assert.AreEqual(0, savedSearch.Position);
	    }

        [Test]
        [ExpectedException(typeof(TwitterApiException), 
            ExpectedMessage = "Authorization is required for the operation, but the API binding was created without authorization.")]
	    public void CreateSavedSearch_Unauthorized() 
        {
#if NET_4_0 || SILVERLIGHT_5
		    unauthorizedTwitter.SearchOperations.CreateSavedSearchAsync("#twitter").Wait();
#else
            unauthorizedTwitter.SearchOperations.CreateSavedSearch("#twitter");
#endif
	    }

	    [Test]
	    public void DeleteSavedSearch() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/saved_searches/destroy/26897775.json")
			    .AndExpectMethod(HttpMethod.DELETE)
			    .AndRespondWith("{}", responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    twitter.SearchOperations.DeleteSavedSearchAsync(26897775).Wait();
#else
            twitter.SearchOperations.DeleteSavedSearch(26897775);
#endif
	    }

        [Test]
        [ExpectedException(typeof(TwitterApiException),
            ExpectedMessage = "Authorization is required for the operation, but the API binding was created without authorization.")]
	    public void DeleteSavedSearch_Unauthorized() 
        {
#if NET_4_0 || SILVERLIGHT_5
		    unauthorizedTwitter.SearchOperations.DeleteSavedSearchAsync(26897775).Wait();
#else
            unauthorizedTwitter.SearchOperations.DeleteSavedSearch(26897775);
#endif
        }
	
	    [Test]
	    public void GetDailyTrends() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/trends/daily.json")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Daily_Trends"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    IList<Trends> dailyTrends = twitter.SearchOperations.GetDailyTrendsAsync().Result;
#else
            IList<Trends> dailyTrends = twitter.SearchOperations.GetDailyTrends();
#endif
		    Assert.AreEqual(24, dailyTrends.Count);
		    int i = 0;
		    foreach(Trends currentTrends in dailyTrends) 
            {
			    IList<Trend> trends = currentTrends.Items;
			    Assert.AreEqual(2, trends.Count);
			    Assert.AreEqual("Cool Stuff" + i, trends[0].Name);
			    Assert.AreEqual("Cool Stuff" + i, trends[0].Query);
			    Assert.AreEqual("#springsocial" + i, trends[1].Name);
			    Assert.AreEqual("#springsocial" + i, trends[1].Query);
			    i++;
		    }
	    }
	
	    [Test]
	    public void GetDailyTrends_ExcludeHashtags() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/trends/daily.json?exclude=hashtags")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Daily_Trends"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    IList<Trends> dailyTrends = twitter.SearchOperations.GetDailyTrendsAsync(true).Result;
#else
            IList<Trends> dailyTrends = twitter.SearchOperations.GetDailyTrends(true);
#endif
            Assert.AreEqual(24, dailyTrends.Count);
	    }

	    [Test]
	    public void GetDailyTrends_WithStartDate() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/trends/daily.json?date=2011-03-17")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Daily_Trends"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    IList<Trends> dailyTrends = twitter.SearchOperations.GetDailyTrendsAsync(false, "2011-03-17").Result;
#else
            IList<Trends> dailyTrends = twitter.SearchOperations.GetDailyTrends(false, "2011-03-17");
#endif
            Assert.AreEqual(24, dailyTrends.Count);
	    }

	    [Test]
	    public void GetDailyTrends_WithStartDateAndExcludeHashtags() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/trends/daily.json?exclude=hashtags&date=2011-03-17")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Daily_Trends"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    IList<Trends> dailyTrends = twitter.SearchOperations.GetDailyTrendsAsync(true, "2011-03-17").Result;
#else
            IList<Trends> dailyTrends = twitter.SearchOperations.GetDailyTrends(true, "2011-03-17");
#endif
            Assert.AreEqual(24, dailyTrends.Count);
		    mockServer.Verify();
	    }

        [Test]
        public void GetWeeklyTrends()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/trends/weekly.json")
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("Weekly_Trends"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Trends> dailyTrends = twitter.SearchOperations.GetWeeklyTrendsAsync().Result;
#else
            IList<Trends> dailyTrends = twitter.SearchOperations.GetWeeklyTrends();
#endif
            Assert.AreEqual(7, dailyTrends.Count);
            int i = 0;
            foreach (Trends currentTrends in dailyTrends)
            {
                IList<Trend> trends = currentTrends.Items;
                Assert.AreEqual(2, trends.Count);
                Assert.AreEqual("Cool Stuff" + i, trends[0].Name);
                Assert.AreEqual("Cool Stuff" + i, trends[0].Query);
                Assert.AreEqual("#springsocial" + i, trends[1].Name);
                Assert.AreEqual("#springsocial" + i, trends[1].Query);
                i++;
            }
        }
	
	    [Test]
	    public void GetWeeklyTrends_ExcludeHashtags() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/trends/weekly.json?exclude=hashtags")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Weekly_Trends"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    IList<Trends> dailyTrends = twitter.SearchOperations.GetWeeklyTrendsAsync(true).Result;
#else
            IList<Trends> dailyTrends = twitter.SearchOperations.GetWeeklyTrends(true);
#endif
            Assert.AreEqual(7, dailyTrends.Count);
	    }
	
	    [Test]
	    public void GetWeeklyTrends_WithStartDate() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/trends/weekly.json?date=2011-03-18")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Weekly_Trends"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    IList<Trends> dailyTrends = twitter.SearchOperations.GetWeeklyTrendsAsync(false, "2011-03-18").Result;
#else
            IList<Trends> dailyTrends = twitter.SearchOperations.GetWeeklyTrends(false, "2011-03-18");
#endif
            Assert.AreEqual(7, dailyTrends.Count);
	    }
	
	    [Test]
	    public void GetWeeklyTrends_WithStartDateAndExcludeHashtags() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/trends/weekly.json?exclude=hashtags&date=2011-03-18")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Weekly_Trends"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    IList<Trends> dailyTrends = twitter.SearchOperations.GetWeeklyTrendsAsync(true, "2011-03-18").Result;
#else
            IList<Trends> dailyTrends = twitter.SearchOperations.GetWeeklyTrends(true, "2011-03-18");
#endif
            Assert.AreEqual(7, dailyTrends.Count);
	    }
	
	    [Test]
	    public void GetLocalTrends() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/trends/2442047.json")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Local_Trends"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    Trends localTrends = twitter.SearchOperations.GetLocalTrendsAsync(2442047).Result;
#else
            Trends localTrends = twitter.SearchOperations.GetLocalTrends(2442047);
#endif
            IList<Trend> trends = localTrends.Items;
		    Assert.AreEqual(2, trends.Count);
		    Trend trend1 = trends[0];
		    Assert.AreEqual("Cool Stuff", trend1.Name);
		    Assert.AreEqual("Cool+Stuff", trend1.Query);
		    Trend trend2 = trends[1];
		    Assert.AreEqual("#springsocial", trend2.Name);
		    Assert.AreEqual("%23springsocial", trend2.Query);
	    }
	
	    [Test]
	    public void GetLocalTrends_excludeHashtags() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/trends/2442047.json?exclude=hashtags")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Local_Trends"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    Trends localTrends = twitter.SearchOperations.GetLocalTrendsAsync(2442047, true).Result;
#else
            Trends localTrends = twitter.SearchOperations.GetLocalTrends(2442047, true);
#endif
            IList<Trend> trends = localTrends.Items;
		    Assert.AreEqual(2, trends.Count);
	    }	
	

	    // test helpers

	    private void AssertSearchTweets(IList<Tweet> tweets) 
        {
		    AssertTimelineTweets(tweets);
		    Assert.AreEqual("en", tweets[0].LanguageCode);
		    Assert.AreEqual("de", tweets[1].LanguageCode);
	    }
    }
}
