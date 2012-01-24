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
using System.IO;
using System.Collections.Generic;
#if NET_4_0 || SILVERLIGHT_5
using System.Threading.Tasks;
#else
using Spring.Rest.Client;
using Spring.Http;
#endif

namespace Spring.Social.Twitter.Api
{
    /// <summary>
    /// Interface defining the operations for searching Twitter and retrieving trending data.
    /// </summary>
    /// <author>Craig Walls</author>
    /// <author>Bruno Baia (.NET)</author>
    public interface ISearchOperations
    {
#if NET_4_0 || SILVERLIGHT_5
        /// <summary>
        /// Asynchronously searches Twitter, returning the first 50 matching <see cref="Tweet"/>s
        /// </summary>
        /// <param name="query">The search query string.</param>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// a <see cref="SearchResults"/> containing the search results metadata and a list of matching <see cref="Tweet"/>s.
        /// </returns>
        /// <exception cref="TwitterApiException">If there is an error while communicating with Twitter.</exception>
        Task<SearchResults> SearchAsync(string query);

        /// <summary>
        /// Asynchronously searches Twitter, returning a specific page out of the complete set of results.
        /// </summary>
        /// <param name="query">The search query string.</param>
        /// <param name="page">The page to return.</param>
        /// <param name="pageSize">The number of <see cref="Tweet"/>s per page.</param>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// a <see cref="SearchResults"/> containing the search results metadata and a list of matching <see cref="Tweet"/>s.
        /// </returns>
        /// <exception cref="TwitterApiException">If there is an error while communicating with Twitter.</exception>
        Task<SearchResults> SearchAsync(string query, int page, int pageSize);

        /// <summary>
        /// Asynchronously searches Twitter, returning a specific page out of the complete set of results. 
        /// Results are filtered to those whose ID falls between sinceId and maxId.
        /// </summary>
        /// <param name="query">The search query string.</param>
        /// <param name="page">The page to return.</param>
        /// <param name="pageSize">The number of <see cref="Tweet"/>s per page.</param>
        /// <param name="sinceId">The minimum <see cref="Tweet"/> ID to return in the results.</param>
        /// <param name="maxId">The maximum <see cref="Tweet"/> ID to return in the results.</param>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// a <see cref="SearchResults"/> containing the search results metadata and a list of matching <see cref="Tweet"/>s.
        /// </returns>
        /// <exception cref="TwitterApiException">If there is an error while communicating with Twitter.</exception>
        Task<SearchResults> SearchAsync(string query, int page, int pageSize, long sinceId, long maxId);

        /// <summary>
        /// Asynchronously retrieves the authenticating user's saved searches.
        /// </summary>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// a list of <see cref="SavedSearch"/> items.
        /// </returns>
        /// <exception cref="TwitterApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="TwitterApiException">If OAuth credentials was not provided.</exception>
        Task<IList<SavedSearch>> GetSavedSearchesAsync();

        /// <summary>
        /// Asynchronously retrieves a single saved search by the saved search's ID.
        /// </summary>
        /// <param name="searchId">The ID of the saved search.</param>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// a <see cref="SavedSearch"/>.
        /// </returns>
        /// <exception cref="TwitterApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="TwitterApiException">If OAuth credentials was not provided.</exception>
        Task<SavedSearch> GetSavedSearchAsync(long searchId);

        /// <summary>
        /// Asynchronously creates a new saved search for the authenticating user.
        /// </summary>
        /// <param name="query">The search query to save.</param>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// a <see cref="SavedSearch"/>.
        /// </returns>
        /// <exception cref="TwitterApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="TwitterApiException">If OAuth credentials was not provided.</exception>
        Task<SavedSearch> CreateSavedSearchAsync(string query);

        /// <summary>
        /// Asynchronously deletes a saved search.
        /// </summary>
        /// <param name="searchId">The ID of the saved search.</param>
        /// <returns>A <code>Task</code> that represents the asynchronous operation.</returns>
        /// <exception cref="TwitterApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="TwitterApiException">If OAuth credentials was not provided.</exception>
        Task DeleteSavedSearchAsync(long searchId);

        /// <summary>
        /// Asynchronously retrieves the top 20 trending topics, hourly for the past 24 hours. 
        /// This list includes hashtagged topics.
        /// </summary>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// a list of Trends objects, one for each hour in the past 24 hours, ordered with the most recent hour first.
        /// </returns>
        /// <exception cref="TwitterApiException">If there is an error while communicating with Twitter.</exception>
        Task<IList<Trends>> GetDailyTrendsAsync();

        /// <summary>
        /// Asynchronously retrieves the top 20 trending topics, hourly for the past 24 hours.
        /// </summary>
        /// <param name="excludeHashtags">If true, hashtagged topics will be excluded from the trends list.</param>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// a list of Trends objects, one for each hour in the past 24 hours, ordered with the most recent hour first.
        /// </returns>
        /// <exception cref="TwitterApiException">If there is an error while communicating with Twitter.</exception>
        Task<IList<Trends>> GetDailyTrendsAsync(bool excludeHashtags);

        /// <summary>
        /// Asynchronously retrieves the top 20 trending topics, hourly for a 24-hour period starting at the specified date.
        /// </summary>
        /// <param name="excludeHashtags">If true, hashtagged topics will be excluded from the trends list.</param>
        /// <param name="startDate">The date to start retrieving trending data for, in MM-DD-YYYY format.</param>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// a list of Trends objects, one for each hour in the given 24 hours, ordered with the most recent hour first.
        /// </returns>
        /// <exception cref="TwitterApiException">If there is an error while communicating with Twitter.</exception>
        Task<IList<Trends>> GetDailyTrendsAsync(bool excludeHashtags, string startDate);

        /// <summary>
        /// Asynchronously retrieves the top 30 trending topics for each day in the past week.
        /// This list includes hashtagged topics.
        /// </summary>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// a list of Trends objects, one for each day in the past week, ordered with the most recent day first.
        /// </returns>
        /// <exception cref="TwitterApiException">If there is an error while communicating with Twitter.</exception>
        Task<IList<Trends>> GetWeeklyTrendsAsync();

        /// <summary>
        /// Asynchronously retrieves the top 30 trending topics for each day in the past week.
        /// </summary>
        /// <param name="excludeHashtags">If true, hashtagged topics will be excluded from the trends list.</param>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// a list of Trends objects, one for each day in the past week, ordered with the most recent day first.
        /// </returns>
        /// <exception cref="TwitterApiException">If there is an error while communicating with Twitter.</exception>
        Task<IList<Trends>> GetWeeklyTrendsAsync(bool excludeHashtags);

        /// <summary>
        /// Asynchronously retrieves the top 30 trending topics for each day in a given week.
        /// </summary>
        /// <param name="excludeHashtags">If true, hashtagged topics will be excluded from the trends list.</param>
        /// <param name="startDate">The date to start retrieving trending data for, in MM-DD-YYYY format.</param>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// a list of Trends objects, one for each day in the given week, ordered with the most recent day first.
        /// </returns>
        /// <exception cref="TwitterApiException">If there is an error while communicating with Twitter.</exception>
        Task<IList<Trends>> GetWeeklyTrendsAsync(bool excludeHashtags, string startDate);

        /// <summary>
        /// Asynchronously retrieves the top 10 trending topics for a given location, identified by its "Where on Earth" (WOE) ID. 
        /// This includes hashtagged topics.
        /// </summary>
        /// <remarks>
        /// See http://developer.yahoo.com/geo/geoplanet/guide/concepts.html for more information on WOE.
        /// </remarks>
        /// <param name="whereOnEarthId">The Where on Earth ID for the location to retrieve trend data.</param>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// a Trends object with the top 10 trending topics for the location.
        /// </returns>
        /// <exception cref="TwitterApiException">If there is an error while communicating with Twitter.</exception>
        Task<Trends> GetLocalTrendsAsync(long whereOnEarthId);

        /// <summary>
        /// Asynchronously retrieves the top 10 trending topics for a given location, identified by its "Where on Earth" (WOE) ID.
        /// </summary>
        /// <remarks>
        /// See http://developer.yahoo.com/geo/geoplanet/guide/concepts.html for more information on WOE.
        /// </remarks>
        /// <param name="whereOnEarthId">The Where on Earth ID for the location to retrieve trend data.</param>
        /// <param name="excludeHashtags">If true, hashtagged topics will be excluded from the trends list.</param>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// a Trends object with the top 10 trending topics for the given location.
        /// </returns>
        /// <exception cref="TwitterApiException">If there is an error while communicating with Twitter.</exception>
        Task<Trends> GetLocalTrendsAsync(long whereOnEarthId, bool excludeHashtags);
#else
#if !SILVERLIGHT
        /// <summary>
        /// Searches Twitter, returning the first 50 matching <see cref="Tweet"/>s
        /// </summary>
        /// <param name="query">The search query string.</param>
        /// <returns>
        /// A <see cref="SearchResults"/> containing the search results metadata and a list of matching <see cref="Tweet"/>s.
        /// </returns>
        /// <exception cref="TwitterApiException">If there is an error while communicating with Twitter.</exception>
        SearchResults Search(string query);

        /// <summary>
        /// Searches Twitter, returning a specific page out of the complete set of results.
        /// </summary>
        /// <param name="query">The search query string.</param>
        /// <param name="page">The page to return.</param>
        /// <param name="pageSize">The number of <see cref="Tweet"/>s per page.</param>
        /// <returns>
        /// A <see cref="SearchResults"/> containing the search results metadata and a list of matching <see cref="Tweet"/>s.
        /// </returns>
        /// <exception cref="TwitterApiException">If there is an error while communicating with Twitter.</exception>
        SearchResults Search(string query, int page, int pageSize);

        /// <summary>
        /// Searches Twitter, returning a specific page out of the complete set of results. 
        /// Results are filtered to those whose ID falls between sinceId and maxId.
        /// </summary>
        /// <param name="query">The search query string.</param>
        /// <param name="page">The page to return.</param>
        /// <param name="pageSize">The number of <see cref="Tweet"/>s per page.</param>
        /// <param name="sinceId">The minimum <see cref="Tweet"/> ID to return in the results.</param>
        /// <param name="maxId">The maximum <see cref="Tweet"/> ID to return in the results.</param>
        /// <returns>A <see cref="SearchResults"/> containing the search results metadata and a list of matching <see cref="Tweet"/>s</returns>
        /// <exception cref="TwitterApiException">If there is an error while communicating with Twitter.</exception>
        SearchResults Search(string query, int page, int pageSize, long sinceId, long maxId);

        /// <summary>
        /// Retrieves the authenticating user's saved searches.
        /// </summary>
        /// <returns>A list of <see cref="SavedSearch"/> items.</returns>
        /// <exception cref="TwitterApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="TwitterApiException">If OAuth credentials was not provided.</exception>
        IList<SavedSearch> GetSavedSearches();

        /// <summary>
        /// Retrieves a single saved search by the saved search's ID.
        /// </summary>
        /// <param name="searchId">The ID of the saved search.</param>
        /// <returns>A <see cref="SavedSearch"/>.</returns>
        /// <exception cref="TwitterApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="TwitterApiException">If OAuth credentials was not provided.</exception>
        SavedSearch GetSavedSearch(long searchId);

        /// <summary>
        /// Creates a new saved search for the authenticating user.
        /// </summary>
        /// <param name="query">The search query to save.</param>
        /// <returns>A <see cref="SavedSearch"/>.</returns>
        /// <exception cref="TwitterApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="TwitterApiException">If OAuth credentials was not provided.</exception>
        SavedSearch CreateSavedSearch(string query);

        /// <summary>
        /// Deletes a saved search.
        /// </summary>
        /// <param name="searchId">The ID of the saved search.</param>
        /// <exception cref="TwitterApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="TwitterApiException">If OAuth credentials was not provided.</exception>
        void DeleteSavedSearch(long searchId);

        /// <summary>
        /// Retrieves the top 20 trending topics, hourly for the past 24 hours. 
        /// This list includes hashtagged topics.
        /// </summary>
        /// <returns>
        /// A list of Trends objects, one for each hour in the past 24 hours, ordered with the most recent hour first.
        /// </returns>
        /// <exception cref="TwitterApiException">If there is an error while communicating with Twitter.</exception>
        IList<Trends> GetDailyTrends();

        /// <summary>
        /// Retrieves the top 20 trending topics, hourly for the past 24 hours.
        /// </summary>
        /// <param name="excludeHashtags">If true, hashtagged topics will be excluded from the trends list.</param>
        /// <returns>
        /// A list of Trends objects, one for each hour in the past 24 hours, ordered with the most recent hour first.
        /// </returns>
        /// <exception cref="TwitterApiException">If there is an error while communicating with Twitter.</exception>
        IList<Trends> GetDailyTrends(bool excludeHashtags);

        /// <summary>
        /// Retrieves the top 20 trending topics, hourly for a 24-hour period starting at the specified date.
        /// </summary>
        /// <param name="excludeHashtags">If true, hashtagged topics will be excluded from the trends list.</param>
        /// <param name="startDate">The date to start retrieving trending data for, in MM-DD-YYYY format.</param>
        /// <returns>
        /// A list of Trends objects, one for each hour in the given 24 hours, ordered with the most recent hour first.
        /// </returns>
        /// <exception cref="TwitterApiException">If there is an error while communicating with Twitter.</exception>
        IList<Trends> GetDailyTrends(bool excludeHashtags, string startDate);

        /// <summary>
        /// Retrieves the top 30 trending topics for each day in the past week.
        /// This list includes hashtagged topics.
        /// </summary>
        /// <returns>
        /// A list of Trends objects, one for each day in the past week, ordered with the most recent day first.
        /// </returns>
        /// <exception cref="TwitterApiException">If there is an error while communicating with Twitter.</exception>
        IList<Trends> GetWeeklyTrends();

        /// <summary>
        /// Retrieves the top 30 trending topics for each day in the past week.
        /// </summary>
        /// <param name="excludeHashtags">If true, hashtagged topics will be excluded from the trends list.</param>
        /// <returns>
        /// A list of Trends objects, one for each day in the past week, ordered with the most recent day first.
        /// </returns>
        /// <exception cref="TwitterApiException">If there is an error while communicating with Twitter.</exception>
        IList<Trends> GetWeeklyTrends(bool excludeHashtags);

        /// <summary>
        /// Retrieves the top 30 trending topics for each day in a given week.
        /// </summary>
        /// <param name="excludeHashtags">If true, hashtagged topics will be excluded from the trends list.</param>
        /// <param name="startDate">The date to start retrieving trending data for, in MM-DD-YYYY format.</param>
        /// <returns>
        /// A list of Trends objects, one for each day in the given week, ordered with the most recent day first.
        /// </returns>
        /// <exception cref="TwitterApiException">If there is an error while communicating with Twitter.</exception>
        IList<Trends> GetWeeklyTrends(bool excludeHashtags, string startDate);

        /// <summary>
        /// Retrieves the top 10 trending topics for a given location, identified by its "Where on Earth" (WOE) ID. 
        /// This includes hashtagged topics.
        /// </summary>
        /// <remarks>
        /// See http://developer.yahoo.com/geo/geoplanet/guide/concepts.html for more information on WOE.
        /// </remarks>
        /// <param name="whereOnEarthId">The Where on Earth ID for the location to retrieve trend data.</param>
        /// <returns>A Trends object with the top 10 trending topics for the location.</returns>
        /// <exception cref="TwitterApiException">If there is an error while communicating with Twitter.</exception>
        Trends GetLocalTrends(long whereOnEarthId);

        /// <summary>
        /// Retrieves the top 10 trending topics for a given location, identified by its "Where on Earth" (WOE) ID.
        /// </summary>
        /// <remarks>
        /// See http://developer.yahoo.com/geo/geoplanet/guide/concepts.html for more information on WOE.
        /// </remarks>
        /// <param name="whereOnEarthId">The Where on Earth ID for the location to retrieve trend data.</param>
        /// <param name="excludeHashtags">If true, hashtagged topics will be excluded from the trends list.</param>
        /// <returns>A Trends object with the top 10 trending topics for the given location.</returns>
        /// <exception cref="TwitterApiException">If there is an error while communicating with Twitter.</exception>
        Trends GetLocalTrends(long whereOnEarthId, bool excludeHashtags);
#endif

        /// <summary>
        /// Asynchronously searches Twitter, returning the first 50 matching <see cref="Tweet"/>s
        /// </summary>
        /// <param name="query">The search query string.</param>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides a <see cref="SearchResults"/> containing the search results metadata and a list of matching <see cref="Tweet"/>s.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="TwitterApiException">If there is an error while communicating with Twitter.</exception>
        RestOperationCanceler SearchAsync(string query, Action<RestOperationCompletedEventArgs<SearchResults>> operationCompleted);

        /// <summary>
        /// Asynchronously searches Twitter, returning a specific page out of the complete set of results.
        /// </summary>
        /// <param name="query">The search query string.</param>
        /// <param name="page">The page to return.</param>
        /// <param name="pageSize">The number of <see cref="Tweet"/>s per page.</param>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides a <see cref="SearchResults"/> containing the search results metadata and a list of matching <see cref="Tweet"/>s.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="TwitterApiException">If there is an error while communicating with Twitter.</exception>
        RestOperationCanceler SearchAsync(string query, int page, int pageSize, Action<RestOperationCompletedEventArgs<SearchResults>> operationCompleted);

        /// <summary>
        /// Asynchronously searches Twitter, returning a specific page out of the complete set of results. 
        /// Results are filtered to those whose ID falls between sinceId and maxId.
        /// </summary>
        /// <param name="query">The search query string.</param>
        /// <param name="page">The page to return.</param>
        /// <param name="pageSize">The number of <see cref="Tweet"/>s per page.</param>
        /// <param name="sinceId">The minimum <see cref="Tweet"/> ID to return in the results.</param>
        /// <param name="maxId">The maximum <see cref="Tweet"/> ID to return in the results.</param>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides a <see cref="SearchResults"/> containing the search results metadata and a list of matching <see cref="Tweet"/>s.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="TwitterApiException">If there is an error while communicating with Twitter.</exception>
        RestOperationCanceler SearchAsync(string query, int page, int pageSize, long sinceId, long maxId, Action<RestOperationCompletedEventArgs<SearchResults>> operationCompleted);

        /// <summary>
        /// Asynchronously retrieves the authenticating user's saved searches.
        /// </summary>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides a list of <see cref="SavedSearch"/> items.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="TwitterApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="TwitterApiException">If OAuth credentials was not provided.</exception>
        RestOperationCanceler GetSavedSearchesAsync(Action<RestOperationCompletedEventArgs<IList<SavedSearch>>> operationCompleted);

        /// <summary>
        /// Asynchronously retrieves a single saved search by the saved search's ID.
        /// </summary>
        /// <param name="searchId">The ID of the saved search.</param>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides a <see cref="SavedSearch"/>.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="TwitterApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="TwitterApiException">If OAuth credentials was not provided.</exception>
        RestOperationCanceler GetSavedSearchAsync(long searchId, Action<RestOperationCompletedEventArgs<SavedSearch>> operationCompleted);

        /// <summary>
        /// Asynchronously creates a new saved search for the authenticating user.
        /// </summary>
        /// <param name="query">The search query to save.</param>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides a <see cref="SavedSearch"/>.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="TwitterApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="TwitterApiException">If OAuth credentials was not provided.</exception>
        RestOperationCanceler CreateSavedSearchAsync(string query, Action<RestOperationCompletedEventArgs<SavedSearch>> operationCompleted);

        /// <summary>
        /// Asynchronously deletes a saved search.
        /// </summary>
        /// <param name="searchId">The ID of the saved search.</param>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="TwitterApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="TwitterApiException">If OAuth credentials was not provided.</exception>
        RestOperationCanceler DeleteSavedSearchAsync(long searchId, Action<RestOperationCompletedEventArgs<object>> operationCompleted);

        /// <summary>
        /// Asynchronously retrieves the top 20 trending topics, hourly for the past 24 hours. 
        /// This list includes hashtagged topics.
        /// </summary>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides a list of Trends objects, one for each hour in the past 24 hours, ordered with the most recent hour first.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="TwitterApiException">If there is an error while communicating with Twitter.</exception>
        RestOperationCanceler GetDailyTrendsAsync(Action<RestOperationCompletedEventArgs<IList<Trends>>> operationCompleted);

        /// <summary>
        /// Asynchronously retrieves the top 20 trending topics, hourly for the past 24 hours.
        /// </summary>
        /// <param name="excludeHashtags">If true, hashtagged topics will be excluded from the trends list.</param>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides a list of Trends objects, one for each hour in the past 24 hours, ordered with the most recent hour first.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="TwitterApiException">If there is an error while communicating with Twitter.</exception>
        RestOperationCanceler GetDailyTrendsAsync(bool excludeHashtags, Action<RestOperationCompletedEventArgs<IList<Trends>>> operationCompleted);

        /// <summary>
        /// Asynchronously retrieves the top 20 trending topics, hourly for a 24-hour period starting at the specified date.
        /// </summary>
        /// <param name="excludeHashtags">If true, hashtagged topics will be excluded from the trends list.</param>
        /// <param name="startDate">The date to start retrieving trending data for, in MM-DD-YYYY format.</param>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides a list of Trends objects, one for each hour in the given 24 hours, ordered with the most recent hour first.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="TwitterApiException">If there is an error while communicating with Twitter.</exception>
        RestOperationCanceler GetDailyTrendsAsync(bool excludeHashtags, string startDate, Action<RestOperationCompletedEventArgs<IList<Trends>>> operationCompleted);

        /// <summary>
        /// Asynchronously retrieves the top 30 trending topics for each day in the past week.
        /// This list includes hashtagged topics.
        /// </summary>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides a list of Trends objects, one for each day in the past week, ordered with the most recent day first.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="TwitterApiException">If there is an error while communicating with Twitter.</exception>
        RestOperationCanceler GetWeeklyTrendsAsync(Action<RestOperationCompletedEventArgs<IList<Trends>>> operationCompleted);

        /// <summary>
        /// Asynchronously retrieves the top 30 trending topics for each day in the past week.
        /// </summary>
        /// <param name="excludeHashtags">If true, hashtagged topics will be excluded from the trends list.</param>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides a list of Trends objects, one for each day in the past week, ordered with the most recent day first.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="TwitterApiException">If there is an error while communicating with Twitter.</exception>
        RestOperationCanceler GetWeeklyTrendsAsync(bool excludeHashtags, Action<RestOperationCompletedEventArgs<IList<Trends>>> operationCompleted);

        /// <summary>
        /// Asynchronously retrieves the top 30 trending topics for each day in a given week.
        /// </summary>
        /// <param name="excludeHashtags">If true, hashtagged topics will be excluded from the trends list.</param>
        /// <param name="startDate">The date to start retrieving trending data for, in MM-DD-YYYY format.</param>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides a list of Trends objects, one for each day in the given week, ordered with the most recent day first.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="TwitterApiException">If there is an error while communicating with Twitter.</exception>
        RestOperationCanceler GetWeeklyTrendsAsync(bool excludeHashtags, string startDate, Action<RestOperationCompletedEventArgs<IList<Trends>>> operationCompleted);

        /// <summary>
        /// Asynchronously retrieves the top 10 trending topics for a given location, identified by its "Where on Earth" (WOE) ID. 
        /// This includes hashtagged topics.
        /// </summary>
        /// <remarks>
        /// See http://developer.yahoo.com/geo/geoplanet/guide/concepts.html for more information on WOE.
        /// </remarks>
        /// <param name="whereOnEarthId">The Where on Earth ID for the location to retrieve trend data.</param>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides a Trends object with the top 10 trending topics for the location.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="TwitterApiException">If there is an error while communicating with Twitter.</exception>
        RestOperationCanceler GetLocalTrendsAsync(long whereOnEarthId, Action<RestOperationCompletedEventArgs<Trends>> operationCompleted);

        /// <summary>
        /// Asynchronously retrieves the top 10 trending topics for a given location, identified by its "Where on Earth" (WOE) ID.
        /// </summary>
        /// <remarks>
        /// See http://developer.yahoo.com/geo/geoplanet/guide/concepts.html for more information on WOE.
        /// </remarks>
        /// <param name="whereOnEarthId">The Where on Earth ID for the location to retrieve trend data.</param>
        /// <param name="excludeHashtags">If true, hashtagged topics will be excluded from the trends list.</param>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides a Trends object with the top 10 trending topics for the given location.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="TwitterApiException">If there is an error while communicating with Twitter.</exception>
        RestOperationCanceler GetLocalTrendsAsync(long whereOnEarthId, bool excludeHashtags, Action<RestOperationCompletedEventArgs<Trends>> operationCompleted);
#endif
    }
}
