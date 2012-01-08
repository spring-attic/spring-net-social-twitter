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
using System.Collections.Generic;
#if SILVERLIGHT
using Spring.Collections.Specialized;
#else
using System.Collections.Specialized;
#endif
#if NET_4_0 || SILVERLIGHT_5
using System.Threading.Tasks;
#else
using Spring.Http;
#endif

using Spring.IO;
using Spring.Rest.Client;

using Spring.Social.Twitter.Api.Impl.Json;

namespace Spring.Social.Twitter.Api.Impl
{
    /// <summary>
    /// Implementation of <see cref="ISearchOperations"/>, providing a binding to Twitter's search and trend-oriented REST resources.
    /// </summary>
    /// <author>Craig Walls</author>
    /// <author>Bruno Baia (.NET)</author>
    class SearchTemplate : AbstractTwitterOperations, ISearchOperations
    {
        private const int DEFAULT_RESULTS_PER_PAGE = 50;

        private RestTemplate restTemplate;

        public SearchTemplate(RestTemplate restTemplate, bool isAuthorized)
            : base(isAuthorized)
        {
            this.restTemplate = restTemplate;
        }

        #region ISearchOperations Members

#if NET_4_0 || SILVERLIGHT_5
        public Task<SearchResults> SearchAsync(string query) 
        {
            return this.SearchAsync(query, 1, DEFAULT_RESULTS_PER_PAGE, 0, 0);
	    }

        public Task<SearchResults> SearchAsync(string query, int page, int resultsPerPage) 
        {
            return this.SearchAsync(query, page, resultsPerPage, 0, 0);
	    }

        public Task<SearchResults> SearchAsync(string query, int page, int resultsPerPage, long sinceId, long maxId) 
        {
            NameValueCollection parameters = BuildSearchParameters(query, page, resultsPerPage, sinceId, maxId);
            return this.restTemplate.GetForObjectAsync<SearchResults>(this.BuildUrl("https://search.twitter.com/search.json", parameters));
	    }

        public Task<IList<SavedSearch>> GetSavedSearchesAsync() 
        {
		    this.EnsureIsAuthorized();
            return this.restTemplate.GetForObjectAsync<IList<SavedSearch>>("saved_searches.json");
	    }

        public Task<SavedSearch> GetSavedSearchAsync(long searchId) 
        {
		    this.EnsureIsAuthorized();
            return this.restTemplate.GetForObjectAsync<SavedSearch>("saved_searches/show/{searchId}.json", searchId);
	    }

        public Task<SavedSearch> CreateSavedSearchAsync(string query) 
        {		
		    this.EnsureIsAuthorized();
		    NameValueCollection request = new NameValueCollection();
		    request.Add("query", query);
            return this.restTemplate.PostForObjectAsync<SavedSearch>("saved_searches/create.json", request);
	    }

        public Task DeleteSavedSearchAsync(long searchId) 
        {
		    this.EnsureIsAuthorized();
            return this.restTemplate.DeleteAsync("saved_searches/destroy/{searchId}.json", searchId);
	    }

        public Task<IList<Trends>> GetDailyTrendsAsync() 
        {
            return this.GetDailyTrendsAsync(false, null);
	    }

        public Task<IList<Trends>> GetDailyTrendsAsync(bool excludeHashtags) 
        {
            return this.GetDailyTrendsAsync(excludeHashtags, null);
	    }

        public Task<IList<Trends>> GetDailyTrendsAsync(bool excludeHashtags, string startDate) 
        {
            NameValueCollection parameters = BuildTrendsParameters(excludeHashtags, startDate);
            return this.restTemplate.GetForObjectAsync<DailyTrendsList>(this.BuildUrl("trends/daily.json", parameters))
                .ContinueWith<IList<Trends>>(task =>
                {
                    return task.Result;
                });
	    }

        public Task<IList<Trends>> GetWeeklyTrendsAsync() 
        {
            return this.GetWeeklyTrendsAsync(false, null);
	    }

        public Task<IList<Trends>> GetWeeklyTrendsAsync(bool excludeHashtags) 
        {
            return this.GetWeeklyTrendsAsync(excludeHashtags, null);
	    }

        public Task<IList<Trends>> GetWeeklyTrendsAsync(bool excludeHashtags, string startDate) 
        {
            NameValueCollection parameters = BuildTrendsParameters(excludeHashtags, startDate);
            return this.restTemplate.GetForObjectAsync<WeeklyTrendsList>(this.BuildUrl("trends/weekly.json", parameters))
                .ContinueWith<IList<Trends>>(task =>
                {
                    return task.Result;
                });
	    }

        public Task<Trends> GetLocalTrendsAsync(long whereOnEarthId) 
        {
            return this.GetLocalTrendsAsync(whereOnEarthId, false);
	    }

        public Task<Trends> GetLocalTrendsAsync(long whereOnEarthId, bool excludeHashtags)
        {
            NameValueCollection parameters = new NameValueCollection();
            if (excludeHashtags)
            {
                parameters.Add("exclude", "hashtags");
            }
            return this.restTemplate.GetForObjectAsync<LocalTrends>(this.BuildUrl("trends/" + whereOnEarthId + ".json", parameters))
                .ContinueWith<Trends>(task =>
                {
                    return task.Result;
                });
        }
#else
#if !SILVERLIGHT
        public SearchResults Search(string query) 
        {
		    return this.Search(query, 1, DEFAULT_RESULTS_PER_PAGE, 0, 0);
	    }

	    public SearchResults Search(string query, int page, int resultsPerPage) 
        {
		    return this.Search(query, page, resultsPerPage, 0, 0);
	    }

	    public SearchResults Search(string query, int page, int resultsPerPage, long sinceId, long maxId) 
        {
            NameValueCollection parameters = BuildSearchParameters(query, page, resultsPerPage, sinceId, maxId);
            return this.restTemplate.GetForObject<SearchResults>(this.BuildUrl("https://search.twitter.com/search.json", parameters));
	    }

	    public IList<SavedSearch> GetSavedSearches() 
        {
		    this.EnsureIsAuthorized();
            return this.restTemplate.GetForObject<IList<SavedSearch>>("saved_searches.json");
	    }

	    public SavedSearch GetSavedSearch(long searchId) 
        {
		    this.EnsureIsAuthorized();
            return this.restTemplate.GetForObject<SavedSearch>("saved_searches/show/{searchId}.json", searchId);
	    }

	    public SavedSearch CreateSavedSearch(string query) 
        {		
		    this.EnsureIsAuthorized();
		    NameValueCollection request = new NameValueCollection();
		    request.Add("query", query);
            return this.restTemplate.PostForObject<SavedSearch>("saved_searches/create.json", request);
	    }

	    public void DeleteSavedSearch(long searchId) 
        {
		    this.EnsureIsAuthorized();
            this.restTemplate.Delete("saved_searches/destroy/{searchId}.json", searchId);
	    }

	    public IList<Trends> GetDailyTrends() 
        {
		    return this.GetDailyTrends(false, null);
	    }

	    public IList<Trends> GetDailyTrends(bool excludeHashtags) 
        {
		    return this.GetDailyTrends(excludeHashtags, null);
	    }

	    public IList<Trends> GetDailyTrends(bool excludeHashtags, string startDate) 
        {
            NameValueCollection parameters = BuildTrendsParameters(excludeHashtags, startDate);
            return this.restTemplate.GetForObject<DailyTrendsList>(this.BuildUrl("trends/daily.json", parameters));
	    }
	
	    public IList<Trends> GetWeeklyTrends() 
        {
		    return this.GetWeeklyTrends(false, null);
	    }
	
	    public IList<Trends> GetWeeklyTrends(bool excludeHashtags) 
        {
		    return this.GetWeeklyTrends(excludeHashtags, null);
	    }
	
	    public IList<Trends> GetWeeklyTrends(bool excludeHashtags, string startDate) 
        {
            NameValueCollection parameters = BuildTrendsParameters(excludeHashtags, startDate);
            return this.restTemplate.GetForObject<WeeklyTrendsList>(this.BuildUrl("trends/weekly.json", parameters));
	    }

	    public Trends GetLocalTrends(long whereOnEarthId) 
        {
		    return this.GetLocalTrends(whereOnEarthId, false);
	    }

	    public Trends GetLocalTrends(long whereOnEarthId, bool excludeHashtags) 
        {
            NameValueCollection parameters = new NameValueCollection();
            if (excludeHashtags)
            {
                parameters.Add("exclude", "hashtags");
            }
            return this.restTemplate.GetForObject<LocalTrends>(this.BuildUrl("trends/" + whereOnEarthId + ".json", parameters));
	    }
#endif

        public RestOperationCanceler SearchAsync(string query, Action<RestOperationCompletedEventArgs<SearchResults>> operationCompleted)
        {
            return this.SearchAsync(query, 1, DEFAULT_RESULTS_PER_PAGE, 0, 0, operationCompleted);
        }

        public RestOperationCanceler SearchAsync(string query, int page, int resultsPerPage, Action<RestOperationCompletedEventArgs<SearchResults>> operationCompleted)
        {
            return this.SearchAsync(query, page, resultsPerPage, 0, 0, operationCompleted);
        }

        public RestOperationCanceler SearchAsync(string query, int page, int resultsPerPage, long sinceId, long maxId, Action<RestOperationCompletedEventArgs<SearchResults>> operationCompleted)
        {
            NameValueCollection parameters = BuildSearchParameters(query, page, resultsPerPage, sinceId, maxId);
            return this.restTemplate.GetForObjectAsync<SearchResults>(this.BuildUrl("https://search.twitter.com/search.json", parameters), operationCompleted);
        }

        public RestOperationCanceler GetSavedSearchesAsync(Action<RestOperationCompletedEventArgs<IList<SavedSearch>>> operationCompleted)
        {
            this.EnsureIsAuthorized();
            return this.restTemplate.GetForObjectAsync<IList<SavedSearch>>("saved_searches.json", operationCompleted);
        }

        public RestOperationCanceler GetSavedSearchAsync(long searchId, Action<RestOperationCompletedEventArgs<SavedSearch>> operationCompleted)
        {
            this.EnsureIsAuthorized();
            return this.restTemplate.GetForObjectAsync<SavedSearch>("saved_searches/show/{searchId}.json", operationCompleted, searchId);
        }

        public RestOperationCanceler CreateSavedSearchAsync(string query, Action<RestOperationCompletedEventArgs<SavedSearch>> operationCompleted)
        {
            this.EnsureIsAuthorized();
            NameValueCollection request = new NameValueCollection();
            request.Add("query", query);
            return this.restTemplate.PostForObjectAsync<SavedSearch>("saved_searches/create.json", request, operationCompleted);
        }

        public RestOperationCanceler DeleteSavedSearchAsync(long searchId, Action<RestOperationCompletedEventArgs<object>> operationCompleted)
        {
            this.EnsureIsAuthorized();
            return this.restTemplate.DeleteAsync("saved_searches/destroy/{searchId}.json", operationCompleted, searchId);
        }

        public RestOperationCanceler GetDailyTrendsAsync(Action<RestOperationCompletedEventArgs<IList<Trends>>> operationCompleted)
        {
            return this.GetDailyTrendsAsync(false, null, operationCompleted);
        }

        public RestOperationCanceler GetDailyTrendsAsync(bool excludeHashtags, Action<RestOperationCompletedEventArgs<IList<Trends>>> operationCompleted)
        {
            return this.GetDailyTrendsAsync(excludeHashtags, null, operationCompleted);
        }

        public RestOperationCanceler GetDailyTrendsAsync(bool excludeHashtags, string startDate, Action<RestOperationCompletedEventArgs<IList<Trends>>> operationCompleted)
        {
            NameValueCollection parameters = BuildTrendsParameters(excludeHashtags, startDate);
            return this.restTemplate.GetForObjectAsync<DailyTrendsList>(this.BuildUrl("trends/daily.json", parameters), 
                r =>
                {
                    operationCompleted(new RestOperationCompletedEventArgs<IList<Trends>>(
                        r.Error == null ? r.Response : null, 
                        r.Error, r.Cancelled, r.UserState));
                });
        }

        public RestOperationCanceler GetWeeklyTrendsAsync(Action<RestOperationCompletedEventArgs<IList<Trends>>> operationCompleted)
        {
            return this.GetWeeklyTrendsAsync(false, null, operationCompleted);
        }

        public RestOperationCanceler GetWeeklyTrendsAsync(bool excludeHashtags, Action<RestOperationCompletedEventArgs<IList<Trends>>> operationCompleted)
        {
            return this.GetWeeklyTrendsAsync(excludeHashtags, null, operationCompleted);
        }

        public RestOperationCanceler GetWeeklyTrendsAsync(bool excludeHashtags, string startDate, Action<RestOperationCompletedEventArgs<IList<Trends>>> operationCompleted)
        {
            NameValueCollection parameters = BuildTrendsParameters(excludeHashtags, startDate);
            return this.restTemplate.GetForObjectAsync<WeeklyTrendsList>(this.BuildUrl("trends/weekly.json", parameters), 
                r =>
                {
                    operationCompleted(new RestOperationCompletedEventArgs<IList<Trends>>(
                        r.Error == null ? r.Response : null, 
                        r.Error, r.Cancelled, r.UserState));
                });
        }

        public RestOperationCanceler GetLocalTrendsAsync(long whereOnEarthId, Action<RestOperationCompletedEventArgs<Trends>> operationCompleted)
        {
            return this.GetLocalTrendsAsync(whereOnEarthId, false, operationCompleted);
        }

        public RestOperationCanceler GetLocalTrendsAsync(long whereOnEarthId, bool excludeHashtags, Action<RestOperationCompletedEventArgs<Trends>> operationCompleted)
        {
            NameValueCollection parameters = new NameValueCollection();
            if (excludeHashtags)
            {
                parameters.Add("exclude", "hashtags");
            }
            return this.restTemplate.GetForObjectAsync<LocalTrends>(this.BuildUrl("trends/" + whereOnEarthId + ".json", parameters), 
                r =>
                {
                    operationCompleted(new RestOperationCompletedEventArgs<Trends>(
                        r.Error == null ? r.Response : null, 
                        r.Error, r.Cancelled, r.UserState));
                });
        }
#endif

        #endregion

        #region Private Methods

        private static NameValueCollection BuildSearchParameters(string query, int page, int resultsPerPage, long sinceId, long maxId)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("q", query);
            parameters.Add("rpp", resultsPerPage.ToString());
            parameters.Add("page", page.ToString());
            if (sinceId > 0)
            {
                parameters.Add("since_id", sinceId.ToString());
            }
            if (maxId > 0)
            {
                parameters.Add("max_id", maxId.ToString());
            }
            return parameters;
        }

        private static NameValueCollection BuildTrendsParameters(bool excludeHashtags, string startDate)
        {
            NameValueCollection parameters = new NameValueCollection();
            if (excludeHashtags)
            {
                parameters.Add("exclude", "hashtags");
            }
            if (startDate != null)
            {
                parameters.Add("date", startDate);
            }
            return parameters;
        }

        #endregion
    }
}