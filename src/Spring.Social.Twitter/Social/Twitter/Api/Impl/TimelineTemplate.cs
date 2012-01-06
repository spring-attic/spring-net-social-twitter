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
using System.Globalization;
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
    /// Implementation of <see cref="ITimelineOperations"/>, providing a binding to Twitter's tweet and timeline-oriented REST resources.
    /// </summary>
    /// <author>Craig Walls</author>
    /// <author>Bruno Baia (.NET)</author>
    class TimelineTemplate : AbstractTwitterOperations, ITimelineOperations
    {
        private RestTemplate restTemplate;

        public TimelineTemplate(RestTemplate restTemplate, bool isAuthorized)
            : base(isAuthorized)
        {
            this.restTemplate = restTemplate;
        }

        #region ITimelineOperations Members

#if NET_4_0 || SILVERLIGHT_5
        public Task<IList<Tweet>> GetPublicTimelineAsync()
        {
            return this.restTemplate.GetForObjectAsync<IList<Tweet>>("statuses/public_timeline.json");
        }

        public Task<IList<Tweet>> GetHomeTimelineAsync()
        {
            return this.GetHomeTimelineAsync(1, 20, 0, 0);
        }

        public Task<IList<Tweet>> GetHomeTimelineAsync(int page, int pageSize)
        {
            return this.GetHomeTimelineAsync(page, pageSize, 0, 0);
        }

        public Task<IList<Tweet>> GetHomeTimelineAsync(int page, int pageSize, long sinceId, long maxId)
        {
            this.EnsureIsAuthorized();
            NameValueCollection parameters = PagingUtils.BuildPagingParametersWithCount(page, pageSize, sinceId, maxId);
            return this.restTemplate.GetForObjectAsync<IList<Tweet>>(this.BuildUri("statuses/home_timeline.json", parameters));
        }

        public Task<IList<Tweet>> GetUserTimelineAsync()
        {
            return this.GetUserTimelineAsync(1, 20, 0, 0);
        }

        public Task<IList<Tweet>> GetUserTimelineAsync(int page, int pageSize)
        {
            return this.GetUserTimelineAsync(page, pageSize, 0, 0);
        }

        public Task<IList<Tweet>> GetUserTimelineAsync(int page, int pageSize, long sinceId, long maxId)
        {
            this.EnsureIsAuthorized();
            NameValueCollection parameters = PagingUtils.BuildPagingParametersWithCount(page, pageSize, sinceId, maxId);
            return this.restTemplate.GetForObjectAsync<IList<Tweet>>(this.BuildUri("statuses/user_timeline.json", parameters));
        }

        public Task<IList<Tweet>> GetUserTimelineAsync(string screenName)
        {
            return this.GetUserTimelineAsync(screenName, 1, 20, 0, 0);
        }

        public Task<IList<Tweet>> GetUserTimelineAsync(string screenName, int page, int pageSize)
        {
            return this.GetUserTimelineAsync(screenName, page, pageSize, 0, 0);
        }

        public Task<IList<Tweet>> GetUserTimelineAsync(string screenName, int page, int pageSize, long sinceId, long maxId)
        {
            NameValueCollection parameters = PagingUtils.BuildPagingParametersWithCount(page, pageSize, sinceId, maxId);
            parameters.Add("screen_name", screenName);
            return this.restTemplate.GetForObjectAsync<IList<Tweet>>(this.BuildUri("statuses/user_timeline.json", parameters));
        }

        public Task<IList<Tweet>> GetUserTimelineAsync(long userId)
        {
            return this.GetUserTimelineAsync(userId, 1, 20, 0, 0);
        }

        public Task<IList<Tweet>> GetUserTimelineAsync(long userId, int page, int pageSize)
        {
            return this.GetUserTimelineAsync(userId, page, pageSize, 0, 0);
        }

        public Task<IList<Tweet>> GetUserTimelineAsync(long userId, int page, int pageSize, long sinceId, long maxId)
        {
            NameValueCollection parameters = PagingUtils.BuildPagingParametersWithCount(page, pageSize, sinceId, maxId);
            parameters.Add("user_id", userId.ToString());
            return this.restTemplate.GetForObjectAsync<IList<Tweet>>(this.BuildUri("statuses/user_timeline.json", parameters));
        }

        public Task<IList<Tweet>> GetMentionsAsync()
        {
            return this.GetMentionsAsync(1, 20, 0, 0);
        }

        public Task<IList<Tweet>> GetMentionsAsync(int page, int pageSize)
        {
            return this.GetMentionsAsync(page, pageSize, 0, 0);
        }

        public Task<IList<Tweet>> GetMentionsAsync(int page, int pageSize, long sinceId, long maxId)
        {
            this.EnsureIsAuthorized();
            NameValueCollection parameters = PagingUtils.BuildPagingParametersWithCount(page, pageSize, sinceId, maxId);
            return this.restTemplate.GetForObjectAsync<IList<Tweet>>(this.BuildUri("statuses/mentions.json", parameters));
        }

        public Task<IList<Tweet>> GetRetweetedByMeAsync()
        {
            return this.GetRetweetedByMeAsync(1, 20, 0, 0);
        }

        public Task<IList<Tweet>> GetRetweetedByMeAsync(int page, int pageSize)
        {
            return this.GetRetweetedByMeAsync(page, pageSize, 0, 0);
        }

        public Task<IList<Tweet>> GetRetweetedByMeAsync(int page, int pageSize, long sinceId, long maxId)
        {
            this.EnsureIsAuthorized();
            NameValueCollection parameters = PagingUtils.BuildPagingParametersWithCount(page, pageSize, sinceId, maxId);
            return this.restTemplate.GetForObjectAsync<IList<Tweet>>(this.BuildUri("statuses/retweeted_by_me.json", parameters));
        }

        public Task<IList<Tweet>> GetRetweetedByUserAsync(long userId)
        {
            return this.GetRetweetedByUserAsync(userId, 1, 20, 0, 0);
        }

        public Task<IList<Tweet>> GetRetweetedByUserAsync(long userId, int page, int pageSize)
        {
            return this.GetRetweetedByUserAsync(userId, page, pageSize, 0, 0);
        }

        public Task<IList<Tweet>> GetRetweetedByUserAsync(long userId, int page, int pageSize, long sinceId, long maxId)
        {
            this.EnsureIsAuthorized();
            NameValueCollection parameters = PagingUtils.BuildPagingParametersWithCount(page, pageSize, sinceId, maxId);
            parameters.Add("user_id", userId.ToString());
            return this.restTemplate.GetForObjectAsync<IList<Tweet>>(this.BuildUri("statuses/retweeted_by_user.json", parameters));
        }

        public Task<IList<Tweet>> GetRetweetedByUserAsync(string screenName)
        {
            return this.GetRetweetedByUserAsync(screenName, 1, 20, 0, 0);
        }

        public Task<IList<Tweet>> GetRetweetedByUserAsync(string screenName, int page, int pageSize)
        {
            return this.GetRetweetedByUserAsync(screenName, page, pageSize, 0, 0);
        }

        public Task<IList<Tweet>> GetRetweetedByUserAsync(string screenName, int page, int pageSize, long sinceId, long maxId)
        {
            this.EnsureIsAuthorized();
            NameValueCollection parameters = PagingUtils.BuildPagingParametersWithCount(page, pageSize, sinceId, maxId);
            parameters.Add("screen_name", screenName);
            return this.restTemplate.GetForObjectAsync<IList<Tweet>>(this.BuildUri("statuses/retweeted_by_user.json", parameters));
        }

        public Task<IList<Tweet>> GetRetweetedToMeAsync()
        {
            return this.GetRetweetedToMeAsync(1, 20, 0, 0);
        }

        public Task<IList<Tweet>> GetRetweetedToMeAsync(int page, int pageSize)
        {
            return this.GetRetweetedToMeAsync(page, pageSize, 0, 0);
        }

        public Task<IList<Tweet>> GetRetweetedToMeAsync(int page, int pageSize, long sinceId, long maxId)
        {
            this.EnsureIsAuthorized();
            NameValueCollection parameters = PagingUtils.BuildPagingParametersWithCount(page, pageSize, sinceId, maxId);
            return this.restTemplate.GetForObjectAsync<IList<Tweet>>(this.BuildUri("statuses/retweeted_to_me.json", parameters));
        }

        public Task<IList<Tweet>> GetRetweetedToUserAsync(long userId)
        {
            return this.GetRetweetedToUserAsync(userId, 1, 20, 0, 0);
        }

        public Task<IList<Tweet>> GetRetweetedToUserAsync(long userId, int page, int pageSize)
        {
            return this.GetRetweetedToUserAsync(userId, page, pageSize, 0, 0);
        }

        public Task<IList<Tweet>> GetRetweetedToUserAsync(long userId, int page, int pageSize, long sinceId, long maxId)
        {
            this.EnsureIsAuthorized();
            NameValueCollection parameters = PagingUtils.BuildPagingParametersWithCount(page, pageSize, sinceId, maxId);
            parameters.Add("user_id", userId.ToString());
            return this.restTemplate.GetForObjectAsync<IList<Tweet>>(this.BuildUri("statuses/retweeted_to_user.json", parameters));
        }

        public Task<IList<Tweet>> GetRetweetedToUserAsync(string screenName)
        {
            return this.GetRetweetedToUserAsync(screenName, 1, 20, 0, 0);
        }

        public Task<IList<Tweet>> GetRetweetedToUserAsync(string screenName, int page, int pageSize)
        {
            return this.GetRetweetedToUserAsync(screenName, page, pageSize, 0, 0);
        }

        public Task<IList<Tweet>> GetRetweetedToUserAsync(string screenName, int page, int pageSize, long sinceId, long maxId)
        {
            this.EnsureIsAuthorized();
            NameValueCollection parameters = PagingUtils.BuildPagingParametersWithCount(page, pageSize, sinceId, maxId);
            parameters.Add("screen_name", screenName);
            return this.restTemplate.GetForObjectAsync<IList<Tweet>>(this.BuildUri("statuses/retweeted_to_user.json", parameters));
        }

        public Task<IList<Tweet>> GetRetweetsOfMeAsync()
        {
            return this.GetRetweetsOfMeAsync(1, 20, 0, 0);
        }

        public Task<IList<Tweet>> GetRetweetsOfMeAsync(int page, int pageSize)
        {
            return this.GetRetweetsOfMeAsync(page, pageSize, 0, 0);
        }

        public Task<IList<Tweet>> GetRetweetsOfMeAsync(int page, int pageSize, long sinceId, long maxId)
        {
            this.EnsureIsAuthorized();
            NameValueCollection parameters = PagingUtils.BuildPagingParametersWithCount(page, pageSize, sinceId, maxId);
            return this.restTemplate.GetForObjectAsync<IList<Tweet>>(this.BuildUri("statuses/retweets_of_me.json", parameters));
        }

        public Task<Tweet> GetStatusAsync(long tweetId)
        {
            return this.restTemplate.GetForObjectAsync<Tweet>("statuses/show/{tweetId}.json", tweetId);
        }

        public Task<Tweet> UpdateStatusAsync(string status)
        {
            return this.UpdateStatusAsync(status, new StatusDetails());
        }

        public Task<Tweet> UpdateStatusAsync(string status, IResource photo)
        {
            return this.UpdateStatusAsync(status, photo, new StatusDetails());
        }

        public Task<Tweet> UpdateStatusAsync(string status, StatusDetails details)
        {
            this.EnsureIsAuthorized();
            NameValueCollection request = new NameValueCollection();
            request.Add("status", status);
            AddStatusDetailsTo(request, details);
            return this.restTemplate.PostForObjectAsync<Tweet>("statuses/update.json", request);
        }

        public Task<Tweet> UpdateStatusAsync(string status, IResource photo, StatusDetails details)
        {
            this.EnsureIsAuthorized();
            IDictionary<string, object> request = new Dictionary<string, object>();
            request.Add("status", status);
            request.Add("media", photo);
            AddStatusDetailsTo(request, details);
            return this.restTemplate.PostForObjectAsync<Tweet>("https://upload.twitter.com/1/statuses/update_with_media.json", request);
        }

        public Task DeleteStatusAsync(long tweetId)
        {
            this.EnsureIsAuthorized();
            return this.restTemplate.DeleteAsync("statuses/destroy/{tweetId}.json", tweetId);
        }

        public Task RetweetAsync(long tweetId)
        {
            this.EnsureIsAuthorized();
            NameValueCollection request = new NameValueCollection();
            return this.restTemplate.PostForMessageAsync("statuses/retweet/{tweetId}.json", request, tweetId);
        }

        public Task<IList<Tweet>> GetRetweetsAsync(long tweetId)
        {
            return this.GetRetweetsAsync(tweetId, 100);
        }

        public Task<IList<Tweet>> GetRetweetsAsync(long tweetId, int count)
        {
            return this.restTemplate.GetForObjectAsync<IList<Tweet>>("statuses/retweets/{tweetId}.json?count={count}", tweetId, count);
        }

        public Task<IList<TwitterProfile>> GetRetweetedByAsync(long tweetId)
        {
            return this.GetRetweetedByAsync(tweetId, 1, 100);
        }

        public Task<IList<TwitterProfile>> GetRetweetedByAsync(long tweetId, int page, int pageSize)
        {
            NameValueCollection parameters = PagingUtils.BuildPagingParametersWithCount(page, pageSize, 0, 0);
            return this.restTemplate.GetForObjectAsync<IList<TwitterProfile>>(this.BuildUri("statuses/" + tweetId + "/retweeted_by.json", parameters));
        }

        public Task<IList<long>> GetRetweetedByIdsAsync(long tweetId)
        {
            return this.GetRetweetedByIdsAsync(tweetId, 1, 100);
        }

        public Task<IList<long>> GetRetweetedByIdsAsync(long tweetId, int page, int pageSize)
        {
            this.EnsureIsAuthorized(); // requires authentication, even though getRetweetedBy() does not.
            NameValueCollection parameters = PagingUtils.BuildPagingParametersWithCount(page, pageSize, 0, 0);
            return this.restTemplate.GetForObjectAsync<IList<long>>(this.BuildUri("statuses/" + tweetId + "/retweeted_by/ids.json", parameters));
        }

        public Task<IList<Tweet>> GetFavoritesAsync()
        {
            return this.GetFavoritesAsync(1, 20);
        }

        public Task<IList<Tweet>> GetFavoritesAsync(int page, int pageSize)
        {
            this.EnsureIsAuthorized();
            // Note: The documentation for /favorites.json doesn't list the count parameter, but it works anyway.
            NameValueCollection parameters = PagingUtils.BuildPagingParametersWithCount(page, pageSize, 0, 0);
            return this.restTemplate.GetForObjectAsync<IList<Tweet>>(this.BuildUri("favorites.json", parameters));
        }

        public Task AddToFavoritesAsync(long tweetId)
        {
            this.EnsureIsAuthorized();
            NameValueCollection request = new NameValueCollection();
            return this.restTemplate.PostForMessageAsync("favorites/create/{tweetId}.json", request, tweetId);
        }

        public Task RemoveFromFavoritesAsync(long tweetId)
        {
            this.EnsureIsAuthorized();
            NameValueCollection request = new NameValueCollection();
            return this.restTemplate.PostForMessageAsync("favorites/destroy/{tweetId}.json", request, tweetId);
        }
#else
#if !SILVERLIGHT
        public IList<Tweet> GetPublicTimeline()
        {
            return this.restTemplate.GetForObject<IList<Tweet>>("statuses/public_timeline.json");
        }

        public IList<Tweet> GetHomeTimeline()
        {
            return this.GetHomeTimeline(1, 20, 0, 0);
        }

        public IList<Tweet> GetHomeTimeline(int page, int pageSize)
        {
            return this.GetHomeTimeline(page, pageSize, 0, 0);
        }

        public IList<Tweet> GetHomeTimeline(int page, int pageSize, long sinceId, long maxId)
        {
            this.EnsureIsAuthorized();
            NameValueCollection parameters = PagingUtils.BuildPagingParametersWithCount(page, pageSize, sinceId, maxId);
            return this.restTemplate.GetForObject<IList<Tweet>>(this.BuildUri("statuses/home_timeline.json", parameters));
        }

        public IList<Tweet> GetUserTimeline()
        {
            return this.GetUserTimeline(1, 20, 0, 0);
        }

        public IList<Tweet> GetUserTimeline(int page, int pageSize)
        {
            return this.GetUserTimeline(page, pageSize, 0, 0);
        }

        public IList<Tweet> GetUserTimeline(int page, int pageSize, long sinceId, long maxId)
        {
            this.EnsureIsAuthorized();
            NameValueCollection parameters = PagingUtils.BuildPagingParametersWithCount(page, pageSize, sinceId, maxId);
            return this.restTemplate.GetForObject<IList<Tweet>>(this.BuildUri("statuses/user_timeline.json", parameters));
        }

        public IList<Tweet> GetUserTimeline(string screenName)
        {
            return this.GetUserTimeline(screenName, 1, 20, 0, 0);
        }

        public IList<Tweet> GetUserTimeline(string screenName, int page, int pageSize)
        {
            return this.GetUserTimeline(screenName, page, pageSize, 0, 0);
        }

        public IList<Tweet> GetUserTimeline(string screenName, int page, int pageSize, long sinceId, long maxId)
        {
            NameValueCollection parameters = PagingUtils.BuildPagingParametersWithCount(page, pageSize, sinceId, maxId);
            parameters.Add("screen_name", screenName);
            return this.restTemplate.GetForObject<IList<Tweet>>(this.BuildUri("statuses/user_timeline.json", parameters));
        }

        public IList<Tweet> GetUserTimeline(long userId)
        {
            return this.GetUserTimeline(userId, 1, 20, 0, 0);
        }

        public IList<Tweet> GetUserTimeline(long userId, int page, int pageSize)
        {
            return this.GetUserTimeline(userId, page, pageSize, 0, 0);
        }

        public IList<Tweet> GetUserTimeline(long userId, int page, int pageSize, long sinceId, long maxId)
        {
            NameValueCollection parameters = PagingUtils.BuildPagingParametersWithCount(page, pageSize, sinceId, maxId);
            parameters.Add("user_id", userId.ToString());
            return this.restTemplate.GetForObject<IList<Tweet>>(this.BuildUri("statuses/user_timeline.json", parameters));
        }

        public IList<Tweet> GetMentions()
        {
            return this.GetMentions(1, 20, 0, 0);
        }

        public IList<Tweet> GetMentions(int page, int pageSize)
        {
            return this.GetMentions(page, pageSize, 0, 0);
        }

        public IList<Tweet> GetMentions(int page, int pageSize, long sinceId, long maxId)
        {
            this.EnsureIsAuthorized();
            NameValueCollection parameters = PagingUtils.BuildPagingParametersWithCount(page, pageSize, sinceId, maxId);
            return this.restTemplate.GetForObject<IList<Tweet>>(this.BuildUri("statuses/mentions.json", parameters));
        }

        public IList<Tweet> GetRetweetedByMe()
        {
            return this.GetRetweetedByMe(1, 20, 0, 0);
        }

        public IList<Tweet> GetRetweetedByMe(int page, int pageSize)
        {
            return this.GetRetweetedByMe(page, pageSize, 0, 0);
        }

        public IList<Tweet> GetRetweetedByMe(int page, int pageSize, long sinceId, long maxId)
        {
            this.EnsureIsAuthorized();
            NameValueCollection parameters = PagingUtils.BuildPagingParametersWithCount(page, pageSize, sinceId, maxId);
            return this.restTemplate.GetForObject<IList<Tweet>>(this.BuildUri("statuses/retweeted_by_me.json", parameters));
        }

        public IList<Tweet> GetRetweetedByUser(long userId)
        {
            return this.GetRetweetedByUser(userId, 1, 20, 0, 0);
        }

        public IList<Tweet> GetRetweetedByUser(long userId, int page, int pageSize)
        {
            return this.GetRetweetedByUser(userId, page, pageSize, 0, 0);
        }

        public IList<Tweet> GetRetweetedByUser(long userId, int page, int pageSize, long sinceId, long maxId)
        {
            this.EnsureIsAuthorized();
            NameValueCollection parameters = PagingUtils.BuildPagingParametersWithCount(page, pageSize, sinceId, maxId);
            parameters.Add("user_id", userId.ToString());
            return this.restTemplate.GetForObject<IList<Tweet>>(this.BuildUri("statuses/retweeted_by_user.json", parameters));
        }

        public IList<Tweet> GetRetweetedByUser(string screenName)
        {
            return this.GetRetweetedByUser(screenName, 1, 20, 0, 0);
        }

        public IList<Tweet> GetRetweetedByUser(string screenName, int page, int pageSize)
        {
            return this.GetRetweetedByUser(screenName, page, pageSize, 0, 0);
        }

        public IList<Tweet> GetRetweetedByUser(string screenName, int page, int pageSize, long sinceId, long maxId)
        {
            this.EnsureIsAuthorized();
            NameValueCollection parameters = PagingUtils.BuildPagingParametersWithCount(page, pageSize, sinceId, maxId);
            parameters.Add("screen_name", screenName);
            return this.restTemplate.GetForObject<IList<Tweet>>(this.BuildUri("statuses/retweeted_by_user.json", parameters));
        }

        public IList<Tweet> GetRetweetedToMe()
        {
            return this.GetRetweetedToMe(1, 20, 0, 0);
        }

        public IList<Tweet> GetRetweetedToMe(int page, int pageSize)
        {
            return this.GetRetweetedToMe(page, pageSize, 0, 0);
        }

        public IList<Tweet> GetRetweetedToMe(int page, int pageSize, long sinceId, long maxId)
        {
            this.EnsureIsAuthorized();
            NameValueCollection parameters = PagingUtils.BuildPagingParametersWithCount(page, pageSize, sinceId, maxId);
            return this.restTemplate.GetForObject<IList<Tweet>>(this.BuildUri("statuses/retweeted_to_me.json", parameters));
        }

        public IList<Tweet> GetRetweetedToUser(long userId)
        {
            return this.GetRetweetedToUser(userId, 1, 20, 0, 0);
        }

        public IList<Tweet> GetRetweetedToUser(long userId, int page, int pageSize)
        {
            return this.GetRetweetedToUser(userId, page, pageSize, 0, 0);
        }

        public IList<Tweet> GetRetweetedToUser(long userId, int page, int pageSize, long sinceId, long maxId)
        {
            this.EnsureIsAuthorized();
            NameValueCollection parameters = PagingUtils.BuildPagingParametersWithCount(page, pageSize, sinceId, maxId);
            parameters.Add("user_id", userId.ToString());
            return this.restTemplate.GetForObject<IList<Tweet>>(this.BuildUri("statuses/retweeted_to_user.json", parameters));
        }

        public IList<Tweet> GetRetweetedToUser(string screenName)
        {
            return this.GetRetweetedToUser(screenName, 1, 20, 0, 0);
        }

        public IList<Tweet> GetRetweetedToUser(string screenName, int page, int pageSize)
        {
            return this.GetRetweetedToUser(screenName, page, pageSize, 0, 0);
        }

        public IList<Tweet> GetRetweetedToUser(string screenName, int page, int pageSize, long sinceId, long maxId)
        {
            this.EnsureIsAuthorized();
            NameValueCollection parameters = PagingUtils.BuildPagingParametersWithCount(page, pageSize, sinceId, maxId);
            parameters.Add("screen_name", screenName);
            return this.restTemplate.GetForObject<IList<Tweet>>(this.BuildUri("statuses/retweeted_to_user.json", parameters));
        }

        public IList<Tweet> GetRetweetsOfMe()
        {
            return this.GetRetweetsOfMe(1, 20, 0, 0);
        }

        public IList<Tweet> GetRetweetsOfMe(int page, int pageSize)
        {
            return this.GetRetweetsOfMe(page, pageSize, 0, 0);
        }

        public IList<Tweet> GetRetweetsOfMe(int page, int pageSize, long sinceId, long maxId)
        {
            this.EnsureIsAuthorized();
            NameValueCollection parameters = PagingUtils.BuildPagingParametersWithCount(page, pageSize, sinceId, maxId);
            return this.restTemplate.GetForObject<IList<Tweet>>(this.BuildUri("statuses/retweets_of_me.json", parameters));
        }

        public Tweet GetStatus(long tweetId)
        {
            return this.restTemplate.GetForObject<Tweet>("statuses/show/{tweetId}.json", tweetId);
        }

        public Tweet UpdateStatus(string status)
        {
            return this.UpdateStatus(status, new StatusDetails());
        }

        public Tweet UpdateStatus(string status, IResource photo)
        {
            return this.UpdateStatus(status, photo, new StatusDetails());
        }

        public Tweet UpdateStatus(string status, StatusDetails details)
        {
            this.EnsureIsAuthorized();
		    NameValueCollection request = new NameValueCollection();
		    request.Add("status", status);
		    AddStatusDetailsTo(request, details);
		    return this.restTemplate.PostForObject<Tweet>("statuses/update.json", request);
        }

        public Tweet UpdateStatus(string status, IResource photo, StatusDetails details)
        {
            this.EnsureIsAuthorized();
		    IDictionary<string, object> request = new Dictionary<string, object>();
		    request.Add("status", status);
		    request.Add("media", photo);
            AddStatusDetailsTo(request, details);
		    return this.restTemplate.PostForObject<Tweet>("https://upload.twitter.com/1/statuses/update_with_media.json", request);
        }

        public void DeleteStatus(long tweetId)
        {
            this.EnsureIsAuthorized();
		    this.restTemplate.Delete("statuses/destroy/{tweetId}.json", tweetId);
        }

        public void Retweet(long tweetId)
        {
            this.EnsureIsAuthorized();
		    NameValueCollection request = new NameValueCollection();
		    this.restTemplate.PostForMessage("statuses/retweet/{tweetId}.json", request, tweetId);
        }

        public IList<Tweet> GetRetweets(long tweetId)
        {
            return this.GetRetweets(tweetId, 100);
        }

        public IList<Tweet> GetRetweets(long tweetId, int count)
        {
		    return this.restTemplate.GetForObject<IList<Tweet>>("statuses/retweets/{tweetId}.json?count={count}", tweetId, count);
        }

        public IList<TwitterProfile> GetRetweetedBy(long tweetId)
        {
            return this.GetRetweetedBy(tweetId, 1, 100);
        }

        public IList<TwitterProfile> GetRetweetedBy(long tweetId, int page, int pageSize)
        {
            NameValueCollection parameters = PagingUtils.BuildPagingParametersWithCount(page, pageSize, 0, 0);
		    return this.restTemplate.GetForObject<IList<TwitterProfile>>(this.BuildUri("statuses/" + tweetId + "/retweeted_by.json", parameters));
        }

        public IList<long> GetRetweetedByIds(long tweetId)
        {
            return this.GetRetweetedByIds(tweetId, 1, 100);
        }

        public IList<long> GetRetweetedByIds(long tweetId, int page, int pageSize)
        {
            this.EnsureIsAuthorized(); // requires authentication, even though getRetweetedBy() does not.
		    NameValueCollection parameters = PagingUtils.BuildPagingParametersWithCount(page, pageSize, 0, 0);
		    return this.restTemplate.GetForObject<IList<long>>(this.BuildUri("statuses/" + tweetId + "/retweeted_by/ids.json", parameters));
        }

        public IList<Tweet> GetFavorites()
        {
            return this.GetFavorites(1, 20);
        }

        public IList<Tweet> GetFavorites(int page, int pageSize)
        {
            this.EnsureIsAuthorized();
		    // Note: The documentation for /favorites.json doesn't list the count parameter, but it works anyway.
		    NameValueCollection parameters = PagingUtils.BuildPagingParametersWithCount(page, pageSize, 0, 0);
		    return this.restTemplate.GetForObject<IList<Tweet>>(this.BuildUri("favorites.json", parameters));
        }

        public void AddToFavorites(long tweetId)
        {
            this.EnsureIsAuthorized();
		    NameValueCollection request = new NameValueCollection();
		    this.restTemplate.PostForMessage("favorites/create/{tweetId}.json", request, tweetId);
        }

        public void RemoveFromFavorites(long tweetId)
        {
            this.EnsureIsAuthorized();
		    NameValueCollection request = new NameValueCollection();
            this.restTemplate.PostForMessage("favorites/destroy/{tweetId}.json", request, tweetId);
        }
#endif

        public RestOperationCanceler GetPublicTimelineAsync(Action<RestOperationCompletedEventArgs<IList<Tweet>>> operationCompleted)
        {
            return this.restTemplate.GetForObjectAsync<IList<Tweet>>("statuses/public_timeline.json", operationCompleted);
        }
        public RestOperationCanceler GetHomeTimelineAsync(Action<RestOperationCompletedEventArgs<IList<Tweet>>> operationCompleted)
        {
            return this.GetHomeTimelineAsync(1, 20, 0, 0, operationCompleted);
        }

        public RestOperationCanceler GetHomeTimelineAsync(int page, int pageSize, Action<RestOperationCompletedEventArgs<IList<Tweet>>> operationCompleted)
        {
            return this.GetHomeTimelineAsync(page, pageSize, 0, 0, operationCompleted);
        }

        public RestOperationCanceler GetHomeTimelineAsync(int page, int pageSize, long sinceId, long maxId, Action<RestOperationCompletedEventArgs<IList<Tweet>>> operationCompleted)
        {
            this.EnsureIsAuthorized();
            NameValueCollection parameters = PagingUtils.BuildPagingParametersWithCount(page, pageSize, sinceId, maxId);
            return this.restTemplate.GetForObjectAsync<IList<Tweet>>(this.BuildUri("statuses/home_timeline.json", parameters), operationCompleted);
        }

        public RestOperationCanceler GetUserTimelineAsync(Action<RestOperationCompletedEventArgs<IList<Tweet>>> operationCompleted)
        {
            return this.GetUserTimelineAsync(1, 20, 0, 0, operationCompleted);
        }

        public RestOperationCanceler GetUserTimelineAsync(int page, int pageSize, Action<RestOperationCompletedEventArgs<IList<Tweet>>> operationCompleted)
        {
            return this.GetUserTimelineAsync(page, pageSize, 0, 0, operationCompleted);
        }

        public RestOperationCanceler GetUserTimelineAsync(int page, int pageSize, long sinceId, long maxId, Action<RestOperationCompletedEventArgs<IList<Tweet>>> operationCompleted)
        {
            this.EnsureIsAuthorized();
            NameValueCollection parameters = PagingUtils.BuildPagingParametersWithCount(page, pageSize, sinceId, maxId);
            return this.restTemplate.GetForObjectAsync<IList<Tweet>>(this.BuildUri("statuses/user_timeline.json", parameters), operationCompleted);
        }

        public RestOperationCanceler GetUserTimelineAsync(string screenName, Action<RestOperationCompletedEventArgs<IList<Tweet>>> operationCompleted)
        {
            return this.GetUserTimelineAsync(screenName, 1, 20, 0, 0, operationCompleted);
        }

        public RestOperationCanceler GetUserTimelineAsync(string screenName, int page, int pageSize, Action<RestOperationCompletedEventArgs<IList<Tweet>>> operationCompleted)
        {
            return this.GetUserTimelineAsync(screenName, page, pageSize, 0, 0, operationCompleted);
        }

        public RestOperationCanceler GetUserTimelineAsync(string screenName, int page, int pageSize, long sinceId, long maxId, Action<RestOperationCompletedEventArgs<IList<Tweet>>> operationCompleted)
        {
            NameValueCollection parameters = PagingUtils.BuildPagingParametersWithCount(page, pageSize, sinceId, maxId);
            parameters.Add("screen_name", screenName);
            return this.restTemplate.GetForObjectAsync<IList<Tweet>>(this.BuildUri("statuses/user_timeline.json", parameters), operationCompleted);
        }

        public RestOperationCanceler GetUserTimelineAsync(long userId, Action<RestOperationCompletedEventArgs<IList<Tweet>>> operationCompleted)
        {
            return this.GetUserTimelineAsync(userId, 1, 20, 0, 0, operationCompleted);
        }

        public RestOperationCanceler GetUserTimelineAsync(long userId, int page, int pageSize, Action<RestOperationCompletedEventArgs<IList<Tweet>>> operationCompleted)
        {
            return this.GetUserTimelineAsync(userId, page, pageSize, 0, 0, operationCompleted);
        }

        public RestOperationCanceler GetUserTimelineAsync(long userId, int page, int pageSize, long sinceId, long maxId, Action<RestOperationCompletedEventArgs<IList<Tweet>>> operationCompleted)
        {
            NameValueCollection parameters = PagingUtils.BuildPagingParametersWithCount(page, pageSize, sinceId, maxId);
            parameters.Add("user_id", userId.ToString());
            return this.restTemplate.GetForObjectAsync<IList<Tweet>>(this.BuildUri("statuses/user_timeline.json", parameters), operationCompleted);
        }

        public RestOperationCanceler GetMentionsAsync(Action<RestOperationCompletedEventArgs<IList<Tweet>>> operationCompleted)
        {
            return this.GetMentionsAsync(1, 20, 0, 0, operationCompleted);
        }

        public RestOperationCanceler GetMentionsAsync(int page, int pageSize, Action<RestOperationCompletedEventArgs<IList<Tweet>>> operationCompleted)
        {
            return this.GetMentionsAsync(page, pageSize, 0, 0, operationCompleted);
        }

        public RestOperationCanceler GetMentionsAsync(int page, int pageSize, long sinceId, long maxId, Action<RestOperationCompletedEventArgs<IList<Tweet>>> operationCompleted)
        {
            this.EnsureIsAuthorized();
            NameValueCollection parameters = PagingUtils.BuildPagingParametersWithCount(page, pageSize, sinceId, maxId);
            return this.restTemplate.GetForObjectAsync<IList<Tweet>>(this.BuildUri("statuses/mentions.json", parameters), operationCompleted);
        }

        public RestOperationCanceler GetRetweetedByMeAsync(Action<RestOperationCompletedEventArgs<IList<Tweet>>> operationCompleted)
        {
            return this.GetRetweetedByMeAsync(1, 20, 0, 0, operationCompleted);
        }

        public RestOperationCanceler GetRetweetedByMeAsync(int page, int pageSize, Action<RestOperationCompletedEventArgs<IList<Tweet>>> operationCompleted)
        {
            return this.GetRetweetedByMeAsync(page, pageSize, 0, 0, operationCompleted);
        }

        public RestOperationCanceler GetRetweetedByMeAsync(int page, int pageSize, long sinceId, long maxId, Action<RestOperationCompletedEventArgs<IList<Tweet>>> operationCompleted)
        {
            this.EnsureIsAuthorized();
            NameValueCollection parameters = PagingUtils.BuildPagingParametersWithCount(page, pageSize, sinceId, maxId);
            return this.restTemplate.GetForObjectAsync<IList<Tweet>>(this.BuildUri("statuses/retweeted_by_me.json", parameters), operationCompleted);
        }

        public RestOperationCanceler GetRetweetedByUserAsync(long userId, Action<RestOperationCompletedEventArgs<IList<Tweet>>> operationCompleted)
        {
            return this.GetRetweetedByUserAsync(userId, 1, 20, 0, 0, operationCompleted);
        }

        public RestOperationCanceler GetRetweetedByUserAsync(long userId, int page, int pageSize, Action<RestOperationCompletedEventArgs<IList<Tweet>>> operationCompleted)
        {
            return this.GetRetweetedByUserAsync(userId, page, pageSize, 0, 0, operationCompleted);
        }

        public RestOperationCanceler GetRetweetedByUserAsync(long userId, int page, int pageSize, long sinceId, long maxId, Action<RestOperationCompletedEventArgs<IList<Tweet>>> operationCompleted)
        {
            this.EnsureIsAuthorized();
            NameValueCollection parameters = PagingUtils.BuildPagingParametersWithCount(page, pageSize, sinceId, maxId);
            parameters.Add("user_id", userId.ToString());
            return this.restTemplate.GetForObjectAsync<IList<Tweet>>(this.BuildUri("statuses/retweeted_by_user.json", parameters), operationCompleted);
        }

        public RestOperationCanceler GetRetweetedByUserAsync(string screenName, Action<RestOperationCompletedEventArgs<IList<Tweet>>> operationCompleted)
        {
            return this.GetRetweetedByUserAsync(screenName, 1, 20, 0, 0, operationCompleted);
        }

        public RestOperationCanceler GetRetweetedByUserAsync(string screenName, int page, int pageSize, Action<RestOperationCompletedEventArgs<IList<Tweet>>> operationCompleted)
        {
            return this.GetRetweetedByUserAsync(screenName, page, pageSize, 0, 0, operationCompleted);
        }

        public RestOperationCanceler GetRetweetedByUserAsync(string screenName, int page, int pageSize, long sinceId, long maxId, Action<RestOperationCompletedEventArgs<IList<Tweet>>> operationCompleted)
        {
            this.EnsureIsAuthorized();
            NameValueCollection parameters = PagingUtils.BuildPagingParametersWithCount(page, pageSize, sinceId, maxId);
            parameters.Add("screen_name", screenName);
            return this.restTemplate.GetForObjectAsync<IList<Tweet>>(this.BuildUri("statuses/retweeted_by_user.json", parameters), operationCompleted);
        }

        public RestOperationCanceler GetRetweetedToMeAsync(Action<RestOperationCompletedEventArgs<IList<Tweet>>> operationCompleted)
        {
            return this.GetRetweetedToMeAsync(1, 20, 0, 0, operationCompleted);
        }

        public RestOperationCanceler GetRetweetedToMeAsync(int page, int pageSize, Action<RestOperationCompletedEventArgs<IList<Tweet>>> operationCompleted)
        {
            return this.GetRetweetedToMeAsync(page, pageSize, 0, 0, operationCompleted);
        }

        public RestOperationCanceler GetRetweetedToMeAsync(int page, int pageSize, long sinceId, long maxId, Action<RestOperationCompletedEventArgs<IList<Tweet>>> operationCompleted)
        {
            this.EnsureIsAuthorized();
            NameValueCollection parameters = PagingUtils.BuildPagingParametersWithCount(page, pageSize, sinceId, maxId);
            return this.restTemplate.GetForObjectAsync<IList<Tweet>>(this.BuildUri("statuses/retweeted_to_me.json", parameters), operationCompleted);
        }

        public RestOperationCanceler GetRetweetedToUserAsync(long userId, Action<RestOperationCompletedEventArgs<IList<Tweet>>> operationCompleted)
        {
            return this.GetRetweetedToUserAsync(userId, 1, 20, 0, 0, operationCompleted);
        }

        public RestOperationCanceler GetRetweetedToUserAsync(long userId, int page, int pageSize, Action<RestOperationCompletedEventArgs<IList<Tweet>>> operationCompleted)
        {
            return this.GetRetweetedToUserAsync(userId, page, pageSize, 0, 0, operationCompleted);
        }

        public RestOperationCanceler GetRetweetedToUserAsync(long userId, int page, int pageSize, long sinceId, long maxId, Action<RestOperationCompletedEventArgs<IList<Tweet>>> operationCompleted)
        {
            this.EnsureIsAuthorized();
            NameValueCollection parameters = PagingUtils.BuildPagingParametersWithCount(page, pageSize, sinceId, maxId);
            parameters.Add("user_id", userId.ToString());
            return this.restTemplate.GetForObjectAsync<IList<Tweet>>(this.BuildUri("statuses/retweeted_to_user.json", parameters), operationCompleted);
        }

        public RestOperationCanceler GetRetweetedToUserAsync(string screenName, Action<RestOperationCompletedEventArgs<IList<Tweet>>> operationCompleted)
        {
            return this.GetRetweetedToUserAsync(screenName, 1, 20, 0, 0, operationCompleted);
        }

        public RestOperationCanceler GetRetweetedToUserAsync(string screenName, int page, int pageSize, Action<RestOperationCompletedEventArgs<IList<Tweet>>> operationCompleted)
        {
            return this.GetRetweetedToUserAsync(screenName, page, pageSize, 0, 0, operationCompleted);
        }

        public RestOperationCanceler GetRetweetedToUserAsync(string screenName, int page, int pageSize, long sinceId, long maxId, Action<RestOperationCompletedEventArgs<IList<Tweet>>> operationCompleted)
        {
            this.EnsureIsAuthorized();
            NameValueCollection parameters = PagingUtils.BuildPagingParametersWithCount(page, pageSize, sinceId, maxId);
            parameters.Add("screen_name", screenName);
            return this.restTemplate.GetForObjectAsync<IList<Tweet>>(this.BuildUri("statuses/retweeted_to_user.json", parameters), operationCompleted);
        }

        public RestOperationCanceler GetRetweetsOfMeAsync(Action<RestOperationCompletedEventArgs<IList<Tweet>>> operationCompleted)
        {
            return this.GetRetweetsOfMeAsync(1, 20, 0, 0, operationCompleted);
        }

        public RestOperationCanceler GetRetweetsOfMeAsync(int page, int pageSize, Action<RestOperationCompletedEventArgs<IList<Tweet>>> operationCompleted)
        {
            return this.GetRetweetsOfMeAsync(page, pageSize, 0, 0, operationCompleted);
        }

        public RestOperationCanceler GetRetweetsOfMeAsync(int page, int pageSize, long sinceId, long maxId, Action<RestOperationCompletedEventArgs<IList<Tweet>>> operationCompleted)
        {
            this.EnsureIsAuthorized();
            NameValueCollection parameters = PagingUtils.BuildPagingParametersWithCount(page, pageSize, sinceId, maxId);
            return this.restTemplate.GetForObjectAsync<IList<Tweet>>(this.BuildUri("statuses/retweets_of_me.json", parameters), operationCompleted);
        }

        public RestOperationCanceler GetStatusAsync(long tweetId, Action<RestOperationCompletedEventArgs<Tweet>> operationCompleted)
        {
            return this.restTemplate.GetForObjectAsync<Tweet>("statuses/show/{tweetId}.json", operationCompleted, tweetId);
        }

        public RestOperationCanceler UpdateStatusAsync(string status, Action<RestOperationCompletedEventArgs<Tweet>> operationCompleted)
        {
            return this.UpdateStatusAsync(status, new StatusDetails(), operationCompleted);
        }

        public RestOperationCanceler UpdateStatusAsync(string status, IResource photo, Action<RestOperationCompletedEventArgs<Tweet>> operationCompleted)
        {
            return this.UpdateStatusAsync(status, photo, new StatusDetails(), operationCompleted);
        }

        public RestOperationCanceler UpdateStatusAsync(string status, StatusDetails details, Action<RestOperationCompletedEventArgs<Tweet>> operationCompleted)
        {
            this.EnsureIsAuthorized();
            NameValueCollection request = new NameValueCollection();
            request.Add("status", status);
            AddStatusDetailsTo(request, details);
            return this.restTemplate.PostForObjectAsync<Tweet>("statuses/update.json", request, operationCompleted);
        }

        public RestOperationCanceler UpdateStatusAsync(string status, IResource photo, StatusDetails details, Action<RestOperationCompletedEventArgs<Tweet>> operationCompleted)
        {
            this.EnsureIsAuthorized();
            IDictionary<string, object> request = new Dictionary<string, object>();
            request.Add("status", status);
            request.Add("media", photo);
            AddStatusDetailsTo(request, details);
            return this.restTemplate.PostForObjectAsync<Tweet>("https://upload.twitter.com/1/statuses/update_with_media.json", request, operationCompleted);
        }

        public RestOperationCanceler DeleteStatusAsync(long tweetId, Action<RestOperationCompletedEventArgs<object>> operationCompleted)
        {
            this.EnsureIsAuthorized();
            return this.restTemplate.DeleteAsync("statuses/destroy/{tweetId}.json", operationCompleted, tweetId);
        }

        public RestOperationCanceler RetweetAsync(long tweetId, Action<RestOperationCompletedEventArgs<HttpResponseMessage>> operationCompleted)
        {
            this.EnsureIsAuthorized();
            NameValueCollection request = new NameValueCollection();
            return this.restTemplate.PostForMessageAsync("statuses/retweet/{tweetId}.json", request, operationCompleted, tweetId);
        }

        public RestOperationCanceler GetRetweetsAsync(long tweetId, Action<RestOperationCompletedEventArgs<IList<Tweet>>> operationCompleted)
        {
            return this.GetRetweetsAsync(tweetId, 100, operationCompleted);
        }

        public RestOperationCanceler GetRetweetsAsync(long tweetId, int count, Action<RestOperationCompletedEventArgs<IList<Tweet>>> operationCompleted)
        {
            return this.restTemplate.GetForObjectAsync<IList<Tweet>>("statuses/retweets/{tweetId}.json?count={count}", operationCompleted, tweetId, count);
        }

        public RestOperationCanceler GetRetweetedByAsync(long tweetId, Action<RestOperationCompletedEventArgs<IList<TwitterProfile>>> operationCompleted)
        {
            return this.GetRetweetedByAsync(tweetId, 1, 100, operationCompleted);
        }

        public RestOperationCanceler GetRetweetedByAsync(long tweetId, int page, int pageSize, Action<RestOperationCompletedEventArgs<IList<TwitterProfile>>> operationCompleted)
        {
            NameValueCollection parameters = PagingUtils.BuildPagingParametersWithCount(page, pageSize, 0, 0);
            return this.restTemplate.GetForObjectAsync<IList<TwitterProfile>>(this.BuildUri("statuses/" + tweetId + "/retweeted_by.json", parameters), operationCompleted);
        }

        public RestOperationCanceler GetRetweetedByIdsAsync(long tweetId, Action<RestOperationCompletedEventArgs<IList<long>>> operationCompleted)
        {
            return this.GetRetweetedByIdsAsync(tweetId, 1, 100, operationCompleted);
        }

        public RestOperationCanceler GetRetweetedByIdsAsync(long tweetId, int page, int pageSize, Action<RestOperationCompletedEventArgs<IList<long>>> operationCompleted)
        {
            this.EnsureIsAuthorized(); // requires authentication, even though getRetweetedBy() does not.
            NameValueCollection parameters = PagingUtils.BuildPagingParametersWithCount(page, pageSize, 0, 0);
            return this.restTemplate.GetForObjectAsync<IList<long>>(this.BuildUri("statuses/" + tweetId + "/retweeted_by/ids.json", parameters), operationCompleted);
        }

        public RestOperationCanceler GetFavoritesAsync(Action<RestOperationCompletedEventArgs<IList<Tweet>>> operationCompleted)
        {
            return this.GetFavoritesAsync(1, 20, operationCompleted);
        }

        public RestOperationCanceler GetFavoritesAsync(int page, int pageSize, Action<RestOperationCompletedEventArgs<IList<Tweet>>> operationCompleted)
        {
            this.EnsureIsAuthorized();
            // Note: The documentation for /favorites.json doesn't list the count parameter, but it works anyway.
            NameValueCollection parameters = PagingUtils.BuildPagingParametersWithCount(page, pageSize, 0, 0);
            return this.restTemplate.GetForObjectAsync<IList<Tweet>>(this.BuildUri("favorites.json", parameters), operationCompleted);
        }

        public RestOperationCanceler AddToFavoritesAsync(long tweetId, Action<RestOperationCompletedEventArgs<HttpResponseMessage>> operationCompleted)
        {
            this.EnsureIsAuthorized();
            NameValueCollection request = new NameValueCollection();
            return this.restTemplate.PostForMessageAsync("favorites/create/{tweetId}.json", request, operationCompleted, tweetId);
        }

        public RestOperationCanceler RemoveFromFavoritesAsync(long tweetId, Action<RestOperationCompletedEventArgs<HttpResponseMessage>> operationCompleted)
        {
            this.EnsureIsAuthorized();
            NameValueCollection request = new NameValueCollection();
            return this.restTemplate.PostForMessageAsync("favorites/destroy/{tweetId}.json", request, operationCompleted, tweetId);
        }
#endif

        #endregion

        #region Private Methods

        private static void AddStatusDetailsTo(NameValueCollection parameters, StatusDetails details)
        {
            if (details.Latitude.HasValue && details.Longitude.HasValue)
            {
                parameters.Add("lat", details.Latitude.Value.ToString(CultureInfo.InvariantCulture));
                parameters.Add("long", details.Longitude.Value.ToString(CultureInfo.InvariantCulture));
            }
            if (details.DisplayCoordinates)
            {
                parameters.Add("display_coordinates", "true");
            }
            if (details.InReplyToStatusId.HasValue)
            {
                parameters.Add("in_reply_to_status_id", details.InReplyToStatusId.Value.ToString());
            }
            if (details.WrapLinks)
            {
                parameters.Add("wrap_links", "true");
            }
        }

        private static void AddStatusDetailsTo(IDictionary<string, object> parameters, StatusDetails details)
        {
            if (details.Latitude.HasValue && details.Longitude.HasValue)
            {
                parameters.Add("lat", details.Latitude.Value.ToString(CultureInfo.InvariantCulture));
                parameters.Add("long", details.Longitude.Value.ToString(CultureInfo.InvariantCulture));
            }
            if (details.DisplayCoordinates)
            {
                parameters.Add("display_coordinates", "true");
            }
            if (details.InReplyToStatusId.HasValue)
            {
                parameters.Add("in_reply_to_status_id", details.InReplyToStatusId.Value.ToString());
            }
            if (details.WrapLinks)
            {
                parameters.Add("wrap_links", "true");
            }
        }

        #endregion
    }
}