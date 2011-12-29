#region License

/*
 * Copyright 2002-2011 the original author or authors.
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
#if NET_4_0 || SILVERLIGHT_5
using System.Threading.Tasks;
#endif
#if SILVERLIGHT
using Spring.Collections.Specialized;
#else
using System.Collections.Specialized;
#endif

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

        #region ITimelineOperations Membres

#if NET_4_0 || SILVERLIGHT_5
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

        public Tweet UpdateStatus(string status, FileInfo photo)
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

        public Tweet UpdateStatus(string status, FileInfo photo, StatusDetails details)
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