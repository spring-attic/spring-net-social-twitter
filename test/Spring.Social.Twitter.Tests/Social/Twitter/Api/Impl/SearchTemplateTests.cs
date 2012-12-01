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
                .AndExpectUri("https://api.twitter.com/1.1/search/tweets.json?q=%23spring")
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
	    public void Search_WithCount() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/search/tweets.json?q=%23spring&count=10")
				.AndExpectMethod(HttpMethod.GET)
				.AndRespondWith(JsonResource("Search"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    SearchResults searchResults = twitter.SearchOperations.SearchAsync("#spring", 10).Result;
#else
            SearchResults searchResults = twitter.SearchOperations.Search("#spring", 10);
#endif
		    Assert.AreEqual(10, searchResults.SinceId);
		    Assert.AreEqual(999, searchResults.MaxId);
		    IList<Tweet> tweets = searchResults.Tweets;
		    AssertSearchTweets(tweets);
	    }

	    [Test]
	    public void Search_WithSinceAndMaxId() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/search/tweets.json?q=%23spring&count=10&since_id=123&max_id=54321")
				.AndExpectMethod(HttpMethod.GET)
				.AndRespondWith(JsonResource("Search"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    SearchResults searchResults = twitter.SearchOperations.SearchAsync("#spring", 10, 123, 54321).Result;
#else
            SearchResults searchResults = twitter.SearchOperations.Search("#spring", 10, 123, 54321);
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
                .AndExpectUri("https://api.twitter.com/1.1/saved_searches/list.json")
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
	    public void GetSavedSearch() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/saved_searches/show/26897775.json")
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
	    public void CreateSavedSearch() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/saved_searches/create.json")
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
	    public void DeleteSavedSearch() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/saved_searches/destroy/26897775.json")
			    .AndExpectMethod(HttpMethod.POST)
                .AndRespondWith(JsonResource("Saved_Search"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            SavedSearch deletedSavedSearch = twitter.SearchOperations.DeleteSavedSearchAsync(26897775).Result;
#else
            SavedSearch deletedSavedSearch = twitter.SearchOperations.DeleteSavedSearch(26897775);
#endif
            Assert.AreEqual(26897775, deletedSavedSearch.ID);
            Assert.AreEqual("#springsocial", deletedSavedSearch.Query);
            Assert.AreEqual("#springsocial", deletedSavedSearch.Name);
            Assert.AreEqual(0, deletedSavedSearch.Position);
        }
	
	    [Test]
	    public void GetTrends() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/trends/place.json?id=2442047")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Trends"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    Trends localTrends = twitter.SearchOperations.GetTrendsAsync(2442047).Result;
#else
            Trends localTrends = twitter.SearchOperations.GetTrends(2442047);
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
	    public void GetTrends_excludeHashtags() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1.1/trends/place.json?id=2442047&exclude=hashtags")
			    .AndExpectMethod(HttpMethod.GET)
			    .AndRespondWith(JsonResource("Trends"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    Trends localTrends = twitter.SearchOperations.GetTrendsAsync(2442047, true).Result;
#else
            Trends localTrends = twitter.SearchOperations.GetTrends(2442047, true);
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
