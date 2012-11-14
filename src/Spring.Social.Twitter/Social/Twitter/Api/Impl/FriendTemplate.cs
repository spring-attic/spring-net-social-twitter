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
#endif

using Spring.Json;
using Spring.Rest.Client;

namespace Spring.Social.Twitter.Api.Impl
{
    /// <summary>
    /// Implementation of <see cref="IFriendOperations"/>, providing a binding to Twitter's friends and followers-oriented REST resources.
    /// </summary>
    /// <author>Craig Walls</author>
    /// <author>Bruno Baia (.NET)</author>
    class FriendTemplate : AbstractTwitterOperations, IFriendOperations
    {
        private RestTemplate restTemplate;

        public FriendTemplate(RestTemplate restTemplate, bool isAuthorized)
            : base(isAuthorized)
        {
            this.restTemplate = restTemplate;
        }

        #region IFriendOperations Members

#if NET_4_0 || SILVERLIGHT_5
        public Task<CursoredList<TwitterProfile>> GetFriendsAsync() 
        {
            return this.GetFriendsInCursorAsync(-1);
	    }

        public Task<CursoredList<TwitterProfile>> GetFriendsInCursorAsync(long cursor)
        {
            return this.GetFriendIdsInCursorAsync(cursor)
                .ContinueWith<CursoredList<TwitterProfile>>(task =>
                {
                    return this.GetCursoredProfileListAsync(task.Result, task.Result.PreviousCursor, task.Result.NextCursor);
                });
        }

        public Task<CursoredList<TwitterProfile>> GetFriendsAsync(long userId) 
        {
            return this.GetFriendsInCursorAsync(userId, -1);
	    }

        public Task<CursoredList<TwitterProfile>> GetFriendsInCursorAsync(long userId, long cursor) 
        {
            return this.GetFriendIdsInCursorAsync(userId, cursor)
                .ContinueWith<CursoredList<TwitterProfile>>(task =>
                {
                    return this.GetCursoredProfileListAsync(task.Result, task.Result.PreviousCursor, task.Result.NextCursor);
                });
	    }

        public Task<CursoredList<TwitterProfile>> GetFriendsAsync(string screenName) 
        {
            return this.GetFriendsInCursorAsync(screenName, -1);
	    }

        public Task<CursoredList<TwitterProfile>> GetFriendsInCursorAsync(string screenName, long cursor) 
        {
            return this.GetFriendIdsInCursorAsync(screenName, cursor)
                .ContinueWith<CursoredList<TwitterProfile>>(task =>
                {
                    return this.GetCursoredProfileListAsync(task.Result, task.Result.PreviousCursor, task.Result.NextCursor);
                });
	    }

        public Task<CursoredList<long>> GetFriendIdsAsync() 
        {
            return this.GetFriendIdsInCursorAsync(-1);
	    }

        public Task<CursoredList<long>> GetFriendIdsInCursorAsync(long cursor) 
        {
		    this.EnsureIsAuthorized();
            return this.restTemplate.GetForObjectAsync<CursoredList<long>>(this.BuildUrl("friends/ids.json", "cursor", cursor.ToString()));
	    }

        public Task<CursoredList<long>> GetFriendIdsAsync(long userId) 
        {
            return this.GetFriendIdsInCursorAsync(userId, -1);
	    }

        public Task<CursoredList<long>> GetFriendIdsInCursorAsync(long userId, long cursor) 
        {
		    NameValueCollection parameters = new NameValueCollection();
		    parameters.Add("cursor", cursor.ToString());
		    parameters.Add("user_id", userId.ToString());
            return this.restTemplate.GetForObjectAsync<CursoredList<long>>(this.BuildUrl("friends/ids.json", parameters));
	    }

        public Task<CursoredList<long>> GetFriendIdsAsync(string screenName) 
        {
            return this.GetFriendIdsInCursorAsync(screenName, -1);
	    }

        public Task<CursoredList<long>> GetFriendIdsInCursorAsync(string screenName, long cursor) 
        {
		    NameValueCollection parameters = new NameValueCollection();
		    parameters.Add("cursor", cursor.ToString());
		    parameters.Add("screen_name", screenName);
            return this.restTemplate.GetForObjectAsync<CursoredList<long>>(this.BuildUrl("friends/ids.json", parameters));
	    }

        public Task<CursoredList<TwitterProfile>> GetFollowersAsync() 
        {
            return this.GetFollowersInCursorAsync(-1);
	    }

        public Task<CursoredList<TwitterProfile>> GetFollowersInCursorAsync(long cursor) 
        {
            return this.GetFollowerIdsInCursorAsync(cursor)
                .ContinueWith<CursoredList<TwitterProfile>>(task =>
                {
                    return this.GetCursoredProfileListAsync(task.Result, task.Result.PreviousCursor, task.Result.NextCursor);
                });
	    }

        public Task<CursoredList<TwitterProfile>> GetFollowersAsync(long userId) 
        {
            return this.GetFollowersInCursorAsync(userId, -1);
	    }

        public Task<CursoredList<TwitterProfile>> GetFollowersInCursorAsync(long userId, long cursor) 
        {
            return this.GetFollowerIdsInCursorAsync(userId, cursor)
                .ContinueWith<CursoredList<TwitterProfile>>(task =>
                {
                    return this.GetCursoredProfileListAsync(task.Result, task.Result.PreviousCursor, task.Result.NextCursor);
                });
	    }

        public Task<CursoredList<TwitterProfile>> GetFollowersAsync(string screenName) 
        {
            return this.GetFollowersInCursorAsync(screenName, -1);
	    }

        public Task<CursoredList<TwitterProfile>> GetFollowersInCursorAsync(string screenName, long cursor) 
        {
            return this.GetFollowerIdsInCursorAsync(screenName, cursor)
                .ContinueWith<CursoredList<TwitterProfile>>(task =>
                {
                    return this.GetCursoredProfileListAsync(task.Result, task.Result.PreviousCursor, task.Result.NextCursor);
                });
	    }

        public Task<CursoredList<long>> GetFollowerIdsAsync() 
        {
            return this.GetFollowerIdsInCursorAsync(-1);
	    }

        public Task<CursoredList<long>> GetFollowerIdsInCursorAsync(long cursor) 
        {
		    this.EnsureIsAuthorized();
            return this.restTemplate.GetForObjectAsync<CursoredList<long>>(this.BuildUrl("followers/ids.json", "cursor", cursor.ToString()));
	    }

        public Task<CursoredList<long>> GetFollowerIdsAsync(long userId) 
        {
            return this.GetFollowerIdsInCursorAsync(userId, -1);
	    }

        public Task<CursoredList<long>> GetFollowerIdsInCursorAsync(long userId, long cursor) 
        {
		    NameValueCollection parameters = new NameValueCollection();
		    parameters.Add("cursor", cursor.ToString());
		    parameters.Add("user_id", userId.ToString());
            return this.restTemplate.GetForObjectAsync<CursoredList<long>>(this.BuildUrl("followers/ids.json", parameters));
	    }

        public Task<CursoredList<long>> GetFollowerIdsAsync(string screenName) 
        {
            return this.GetFollowerIdsInCursorAsync(screenName, -1);
	    }

        public Task<CursoredList<long>> GetFollowerIdsInCursorAsync(string screenName, long cursor) 
        {
		    NameValueCollection parameters = new NameValueCollection();
		    parameters.Add("cursor", cursor.ToString());
		    parameters.Add("screen_name", screenName);
            return this.restTemplate.GetForObjectAsync<CursoredList<long>>(this.BuildUrl("followers/ids.json", parameters));
	    }

        public Task<TwitterProfile> FollowAsync(long userId) 
        {
		    this.EnsureIsAuthorized();
            NameValueCollection request = new NameValueCollection();
            return this.restTemplate.PostForObjectAsync<TwitterProfile>(this.BuildUrl("friendships/create.json", "user_id", userId.ToString()), request);
	    }

        public Task<TwitterProfile> FollowAsync(string screenName) 
        {
		    this.EnsureIsAuthorized();
            NameValueCollection request = new NameValueCollection();
            return this.restTemplate.PostForObjectAsync<TwitterProfile>(this.BuildUrl("friendships/create.json", "screen_name", screenName), request);
	    }

        public Task<TwitterProfile> UnfollowAsync(long userId) 
        {
		    this.EnsureIsAuthorized();
            NameValueCollection request = new NameValueCollection();
            return this.restTemplate.PostForObjectAsync<TwitterProfile>(this.BuildUrl("friendships/destroy.json", "user_id", userId.ToString()), request);
	    }

        public Task<TwitterProfile> UnfollowAsync(string screenName) 
        {
		    this.EnsureIsAuthorized();
            NameValueCollection request = new NameValueCollection();
            return this.restTemplate.PostForObjectAsync<TwitterProfile>(this.BuildUrl("friendships/destroy.json", "screen_name", screenName), request);
	    }

        public Task<TwitterProfile> EnableNotificationsAsync(long userId) 
        {
		    this.EnsureIsAuthorized();
            NameValueCollection request = new NameValueCollection();
            return this.restTemplate.PostForObjectAsync<TwitterProfile>(this.BuildUrl("notifications/follow.json", "user_id", userId.ToString()), request);
	    }

        public Task<TwitterProfile> EnableNotificationsAsync(string screenName) 
        {
		    this.EnsureIsAuthorized();
            NameValueCollection request = new NameValueCollection();
            return this.restTemplate.PostForObjectAsync<TwitterProfile>(this.BuildUrl("notifications/follow.json", "screen_name", screenName), request);
	    }

        public Task<TwitterProfile> DisableNotificationsAsync(long userId) 
        {
		    this.EnsureIsAuthorized();
            NameValueCollection request = new NameValueCollection();
            return this.restTemplate.PostForObjectAsync<TwitterProfile>(this.BuildUrl("notifications/leave.json", "user_id", userId.ToString()), request);
	    }

        public Task<TwitterProfile> DisableNotificationsAsync(string screenName) 
        {
		    this.EnsureIsAuthorized();
            NameValueCollection request = new NameValueCollection();
            return this.restTemplate.PostForObjectAsync<TwitterProfile>(this.BuildUrl("notifications/leave.json", "screen_name", screenName), request);
	    }

        public Task<bool> FriendshipExistsAsync(string screenNameA, string screenNameB) 
        {
		    NameValueCollection parameters = new NameValueCollection();
		    parameters.Add("screen_name_a", screenNameA);
		    parameters.Add("screen_name_b", screenNameB);
            return this.restTemplate.GetForObjectAsync<JsonValue>(this.BuildUrl("friendships/exists.json", parameters))
                .ContinueWith<bool>(task =>
                {
                    return task.Result.GetValue<bool>();
                }, TaskContinuationOptions.ExecuteSynchronously);
	    }

        public Task<CursoredList<long>> GetIncomingFriendshipsAsync() 
        {
            return this.GetIncomingFriendshipsAsync(-1);
	    }

        public Task<CursoredList<long>> GetIncomingFriendshipsAsync(long cursor) 
        {
		    this.EnsureIsAuthorized();
            return this.restTemplate.GetForObjectAsync<CursoredList<long>>(this.BuildUrl("friendships/incoming.json", "cursor", cursor.ToString()));
	    }

        public Task<CursoredList<long>> GetOutgoingFriendshipsAsync() 
        {
            return this.GetOutgoingFriendshipsAsync(-1);
	    }

        public Task<CursoredList<long>> GetOutgoingFriendshipsAsync(long cursor) 
        {
		    this.EnsureIsAuthorized();
            return this.restTemplate.GetForObjectAsync<CursoredList<long>>(this.BuildUrl("friendships/outgoing.json", "cursor", cursor.ToString()));
	    }
#else
#if !SILVERLIGHT
        public CursoredList<TwitterProfile> GetFriends() 
        {
		    return this.GetFriendsInCursor(-1);
	    }

	    public CursoredList<TwitterProfile> GetFriendsInCursor(long cursor) 
        {
		    CursoredList<long> friendIds = GetFriendIdsInCursor(cursor);
		    return this.GetCursoredProfileList(friendIds, friendIds.PreviousCursor, friendIds.NextCursor);
	    }

	    public CursoredList<TwitterProfile> GetFriends(long userId) 
        {
		    return this.GetFriendsInCursor(userId, -1);
	    }

	    public CursoredList<TwitterProfile> GetFriendsInCursor(long userId, long cursor) 
        {
		    CursoredList<long> friendIds = this.GetFriendIdsInCursor(userId, cursor);
		    return this.GetCursoredProfileList(friendIds, friendIds.PreviousCursor, friendIds.NextCursor);
	    }

	    public CursoredList<TwitterProfile> GetFriends(string screenName) 
        {
		    return this.GetFriendsInCursor(screenName, -1);
	    }
	
	    public CursoredList<TwitterProfile> GetFriendsInCursor(string screenName, long cursor) 
        {
		    CursoredList<long> friendIds = this.GetFriendIdsInCursor(screenName, cursor);
		    return this.GetCursoredProfileList(friendIds, friendIds.PreviousCursor, friendIds.NextCursor);
	    }
	
	    public CursoredList<long> GetFriendIds() 
        {
		    return this.GetFriendIdsInCursor(-1);
	    }
	
	    public CursoredList<long> GetFriendIdsInCursor(long cursor) 
        {
		    this.EnsureIsAuthorized();
		    return this.restTemplate.GetForObject<CursoredList<long>>(this.BuildUrl("friends/ids.json", "cursor", cursor.ToString()));
	    }

	    public CursoredList<long> GetFriendIds(long userId) 
        {
		    return this.GetFriendIdsInCursor(userId, -1);
	    }
	
	    public CursoredList<long> GetFriendIdsInCursor(long userId, long cursor) 
        {
		    NameValueCollection parameters = new NameValueCollection();
		    parameters.Add("cursor", cursor.ToString());
		    parameters.Add("user_id", userId.ToString());
		    return this.restTemplate.GetForObject<CursoredList<long>>(this.BuildUrl("friends/ids.json", parameters));;
	    }

	    public CursoredList<long> GetFriendIds(string screenName) 
        {
		    return this.GetFriendIdsInCursor(screenName, -1);
	    }
	
	    public CursoredList<long> GetFriendIdsInCursor(string screenName, long cursor) 
        {
		    NameValueCollection parameters = new NameValueCollection();
		    parameters.Add("cursor", cursor.ToString());
		    parameters.Add("screen_name", screenName);
		    return this.restTemplate.GetForObject<CursoredList<long>>(this.BuildUrl("friends/ids.json", parameters));
	    }

	    public CursoredList<TwitterProfile> GetFollowers() 
        {
		    return this.GetFollowersInCursor(-1);
	    }
	
	    public CursoredList<TwitterProfile> GetFollowersInCursor(long cursor) 
        {
		    CursoredList<long> followerIds = this.GetFollowerIdsInCursor(cursor);
		    return this.GetCursoredProfileList(followerIds, followerIds.PreviousCursor, followerIds.NextCursor);
	    }

	    public CursoredList<TwitterProfile> GetFollowers(long userId) 
        {
		    return this.GetFollowersInCursor(userId, -1);
	    }
	
	    public CursoredList<TwitterProfile> GetFollowersInCursor(long userId, long cursor) 
        {
		    CursoredList<long> followerIds = this.GetFollowerIdsInCursor(userId, cursor);
		    return this.GetCursoredProfileList(followerIds, followerIds.PreviousCursor, followerIds.NextCursor);
	    }

	    public CursoredList<TwitterProfile> GetFollowers(string screenName) 
        {
		    return this.GetFollowersInCursor(screenName, -1);
	    }
	
	    public CursoredList<TwitterProfile> GetFollowersInCursor(string screenName, long cursor) 
        {
		    CursoredList<long> followerIds = this.GetFollowerIdsInCursor(screenName, cursor);
		    return this.GetCursoredProfileList(followerIds, followerIds.PreviousCursor, followerIds.NextCursor);
	    }

	    public CursoredList<long> GetFollowerIds() 
        {
		    return this.GetFollowerIdsInCursor(-1);
	    }
	
	    public CursoredList<long> GetFollowerIdsInCursor(long cursor) 
        {
		    this.EnsureIsAuthorized();
		    return this.restTemplate.GetForObject<CursoredList<long>>(this.BuildUrl("followers/ids.json", "cursor", cursor.ToString()));
	    }

	    public CursoredList<long> GetFollowerIds(long userId) 
        {
		    return this.GetFollowerIdsInCursor(userId, -1);
	    }
	
	    public CursoredList<long> GetFollowerIdsInCursor(long userId, long cursor) 
        {
		    NameValueCollection parameters = new NameValueCollection();
		    parameters.Add("cursor", cursor.ToString());
		    parameters.Add("user_id", userId.ToString());
		    return this.restTemplate.GetForObject<CursoredList<long>>(this.BuildUrl("followers/ids.json", parameters));
	    }

	    public CursoredList<long> GetFollowerIds(string screenName) 
        {
		    return this.GetFollowerIdsInCursor(screenName, -1);
	    }
	
	    public CursoredList<long> GetFollowerIdsInCursor(string screenName, long cursor) 
        {
		    NameValueCollection parameters = new NameValueCollection();
		    parameters.Add("cursor", cursor.ToString());
		    parameters.Add("screen_name", screenName);
		    return this.restTemplate.GetForObject<CursoredList<long>>(this.BuildUrl("followers/ids.json", parameters));
	    }

	    public TwitterProfile Follow(long userId) 
        {
		    this.EnsureIsAuthorized();
            NameValueCollection request = new NameValueCollection();
		    return this.restTemplate.PostForObject<TwitterProfile>(this.BuildUrl("friendships/create.json", "user_id", userId.ToString()), request);
	    }

	    public TwitterProfile Follow(string screenName) 
        {
		    this.EnsureIsAuthorized();
            NameValueCollection request = new NameValueCollection();
		    return this.restTemplate.PostForObject<TwitterProfile>(this.BuildUrl("friendships/create.json", "screen_name", screenName), request);
	    }
	
	    public TwitterProfile Unfollow(long userId) 
        {
		    this.EnsureIsAuthorized();
            NameValueCollection request = new NameValueCollection();
		    return this.restTemplate.PostForObject<TwitterProfile>(this.BuildUrl("friendships/destroy.json", "user_id", userId.ToString()), request);
	    }

	    public TwitterProfile Unfollow(string screenName) 
        {
		    this.EnsureIsAuthorized();
            NameValueCollection request = new NameValueCollection();
		    return this.restTemplate.PostForObject<TwitterProfile>(this.BuildUrl("friendships/destroy.json", "screen_name", screenName), request);
	    }
	
	    public TwitterProfile EnableNotifications(long userId) 
        {
		    this.EnsureIsAuthorized();
            NameValueCollection request = new NameValueCollection();
		    return this.restTemplate.PostForObject<TwitterProfile>(this.BuildUrl("notifications/follow.json", "user_id", userId.ToString()), request);
	    }
	
	    public TwitterProfile EnableNotifications(string screenName) 
        {
		    this.EnsureIsAuthorized();
            NameValueCollection request = new NameValueCollection();
		    return this.restTemplate.PostForObject<TwitterProfile>(this.BuildUrl("notifications/follow.json", "screen_name", screenName), request);
	    }

	    public TwitterProfile DisableNotifications(long userId) 
        {
		    this.EnsureIsAuthorized();
            NameValueCollection request = new NameValueCollection();
		    return this.restTemplate.PostForObject<TwitterProfile>(this.BuildUrl("notifications/leave.json", "user_id", userId.ToString()), request);
	    }
	
	    public TwitterProfile DisableNotifications(string screenName) 
        {
		    this.EnsureIsAuthorized();
            NameValueCollection request = new NameValueCollection();
		    return this.restTemplate.PostForObject<TwitterProfile>(this.BuildUrl("notifications/leave.json", "screen_name", screenName), request);
	    }
	
	    public bool FriendshipExists(string screenNameA, string screenNameB) 
        {
		    NameValueCollection parameters = new NameValueCollection();
		    parameters.Add("screen_name_a", screenNameA);
		    parameters.Add("screen_name_b", screenNameB);
		    return this.restTemplate.GetForObject<JsonValue>(this.BuildUrl("friendships/exists.json", parameters)).GetValue<bool>();
	    }

	    public CursoredList<long> GetIncomingFriendships() 
        {
		    return this.GetIncomingFriendships(-1);
	    }
	
	    public CursoredList<long> GetIncomingFriendships(long cursor) 
        {
		    this.EnsureIsAuthorized();
		    return this.restTemplate.GetForObject<CursoredList<long>>(this.BuildUrl("friendships/incoming.json", "cursor", cursor.ToString()));
	    }

	    public CursoredList<long> GetOutgoingFriendships() 
        {
		    return this.GetOutgoingFriendships(-1);
	    }
	
	    public CursoredList<long> GetOutgoingFriendships(long cursor) 
        {
		    this.EnsureIsAuthorized();
		    return this.restTemplate.GetForObject<CursoredList<long>>(this.BuildUrl("friendships/outgoing.json", "cursor", cursor.ToString()));
	    }
#endif

        public RestOperationCanceler GetFriendsAsync(Action<RestOperationCompletedEventArgs<CursoredList<TwitterProfile>>> operationCompleted)
        {
            return this.GetFriendsInCursorAsync(-1, operationCompleted);
        }

        public RestOperationCanceler GetFriendsInCursorAsync(long cursor, Action<RestOperationCompletedEventArgs<CursoredList<TwitterProfile>>> operationCompleted)
        {
            return this.GetFriendIdsInCursorAsync(cursor, 
                r => this.GetCursoredProfileListAsync(r, operationCompleted));
        }

        public RestOperationCanceler GetFriendsAsync(long userId, Action<RestOperationCompletedEventArgs<CursoredList<TwitterProfile>>> operationCompleted)
        {
            return this.GetFriendsInCursorAsync(userId, -1, operationCompleted);
        }

        public RestOperationCanceler GetFriendsInCursorAsync(long userId, long cursor, Action<RestOperationCompletedEventArgs<CursoredList<TwitterProfile>>> operationCompleted)
        {
            return this.GetFriendIdsInCursorAsync(userId, cursor, 
                r => this.GetCursoredProfileListAsync(r, operationCompleted));
        }

        public RestOperationCanceler GetFriendsAsync(string screenName, Action<RestOperationCompletedEventArgs<CursoredList<TwitterProfile>>> operationCompleted)
        {
            return this.GetFriendsInCursorAsync(screenName, -1, operationCompleted);
        }

        public RestOperationCanceler GetFriendsInCursorAsync(string screenName, long cursor, Action<RestOperationCompletedEventArgs<CursoredList<TwitterProfile>>> operationCompleted)
        {
            return this.GetFriendIdsInCursorAsync(screenName, cursor, 
                r => this.GetCursoredProfileListAsync(r, operationCompleted));
        }

        public RestOperationCanceler GetFriendIdsAsync(Action<RestOperationCompletedEventArgs<CursoredList<long>>> operationCompleted)
        {
            return this.GetFriendIdsInCursorAsync(-1, operationCompleted);
        }

        public RestOperationCanceler GetFriendIdsInCursorAsync(long cursor, Action<RestOperationCompletedEventArgs<CursoredList<long>>> operationCompleted)
        {
            this.EnsureIsAuthorized();
            return this.restTemplate.GetForObjectAsync<CursoredList<long>>(this.BuildUrl("friends/ids.json", "cursor", cursor.ToString()), operationCompleted);
        }

        public RestOperationCanceler GetFriendIdsAsync(long userId, Action<RestOperationCompletedEventArgs<CursoredList<long>>> operationCompleted)
        {
            return this.GetFriendIdsInCursorAsync(userId, -1, operationCompleted);
        }

        public RestOperationCanceler GetFriendIdsInCursorAsync(long userId, long cursor, Action<RestOperationCompletedEventArgs<CursoredList<long>>> operationCompleted)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("cursor", cursor.ToString());
            parameters.Add("user_id", userId.ToString());
            return this.restTemplate.GetForObjectAsync<CursoredList<long>>(this.BuildUrl("friends/ids.json", parameters), operationCompleted);
        }

        public RestOperationCanceler GetFriendIdsAsync(string screenName, Action<RestOperationCompletedEventArgs<CursoredList<long>>> operationCompleted)
        {
            return this.GetFriendIdsInCursorAsync(screenName, -1, operationCompleted);
        }

        public RestOperationCanceler GetFriendIdsInCursorAsync(string screenName, long cursor, Action<RestOperationCompletedEventArgs<CursoredList<long>>> operationCompleted)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("cursor", cursor.ToString());
            parameters.Add("screen_name", screenName);
            return this.restTemplate.GetForObjectAsync<CursoredList<long>>(this.BuildUrl("friends/ids.json", parameters), operationCompleted);
        }

        public RestOperationCanceler GetFollowersAsync(Action<RestOperationCompletedEventArgs<CursoredList<TwitterProfile>>> operationCompleted)
        {
            return this.GetFollowersInCursorAsync(-1, operationCompleted);
        }

        public RestOperationCanceler GetFollowersInCursorAsync(long cursor, Action<RestOperationCompletedEventArgs<CursoredList<TwitterProfile>>> operationCompleted)
        {
            return this.GetFollowerIdsInCursorAsync(cursor, 
                r => this.GetCursoredProfileListAsync(r, operationCompleted));
        }

        public RestOperationCanceler GetFollowersAsync(long userId, Action<RestOperationCompletedEventArgs<CursoredList<TwitterProfile>>> operationCompleted)
        {
            return this.GetFollowersInCursorAsync(userId, -1, operationCompleted);
        }

        public RestOperationCanceler GetFollowersInCursorAsync(long userId, long cursor, Action<RestOperationCompletedEventArgs<CursoredList<TwitterProfile>>> operationCompleted)
        {
            return this.GetFollowerIdsInCursorAsync(userId, cursor, 
                r => this.GetCursoredProfileListAsync(r, operationCompleted));
        }

        public RestOperationCanceler GetFollowersAsync(string screenName, Action<RestOperationCompletedEventArgs<CursoredList<TwitterProfile>>> operationCompleted)
        {
            return this.GetFollowersInCursorAsync(screenName, -1, operationCompleted);
        }

        public RestOperationCanceler GetFollowersInCursorAsync(string screenName, long cursor, Action<RestOperationCompletedEventArgs<CursoredList<TwitterProfile>>> operationCompleted)
        {
            return this.GetFollowerIdsInCursorAsync(screenName, cursor, 
                r => this.GetCursoredProfileListAsync(r, operationCompleted));
        }

        public RestOperationCanceler GetFollowerIdsAsync(Action<RestOperationCompletedEventArgs<CursoredList<long>>> operationCompleted)
        {
            return this.GetFollowerIdsInCursorAsync(-1, operationCompleted);
        }

        public RestOperationCanceler GetFollowerIdsInCursorAsync(long cursor, Action<RestOperationCompletedEventArgs<CursoredList<long>>> operationCompleted)
        {
            this.EnsureIsAuthorized();
            return this.restTemplate.GetForObjectAsync<CursoredList<long>>(this.BuildUrl("followers/ids.json", "cursor", cursor.ToString()), operationCompleted);
        }

        public RestOperationCanceler GetFollowerIdsAsync(long userId, Action<RestOperationCompletedEventArgs<CursoredList<long>>> operationCompleted)
        {
            return this.GetFollowerIdsInCursorAsync(userId, -1, operationCompleted);
        }

        public RestOperationCanceler GetFollowerIdsInCursorAsync(long userId, long cursor, Action<RestOperationCompletedEventArgs<CursoredList<long>>> operationCompleted)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("cursor", cursor.ToString());
            parameters.Add("user_id", userId.ToString());
            return this.restTemplate.GetForObjectAsync<CursoredList<long>>(this.BuildUrl("followers/ids.json", parameters), operationCompleted);
        }

        public RestOperationCanceler GetFollowerIdsAsync(string screenName, Action<RestOperationCompletedEventArgs<CursoredList<long>>> operationCompleted)
        {
            return this.GetFollowerIdsInCursorAsync(screenName, -1, operationCompleted);
        }

        public RestOperationCanceler GetFollowerIdsInCursorAsync(string screenName, long cursor, Action<RestOperationCompletedEventArgs<CursoredList<long>>> operationCompleted)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("cursor", cursor.ToString());
            parameters.Add("screen_name", screenName);
            return this.restTemplate.GetForObjectAsync<CursoredList<long>>(this.BuildUrl("followers/ids.json", parameters), operationCompleted);
        }

        public RestOperationCanceler FollowAsync(long userId, Action<RestOperationCompletedEventArgs<TwitterProfile>> operationCompleted)
        {
            this.EnsureIsAuthorized();
            NameValueCollection request = new NameValueCollection();
            return this.restTemplate.PostForObjectAsync<TwitterProfile>(this.BuildUrl("friendships/create.json", "user_id", userId.ToString()), request, operationCompleted);
        }

        public RestOperationCanceler FollowAsync(string screenName, Action<RestOperationCompletedEventArgs<TwitterProfile>> operationCompleted)
        {
            this.EnsureIsAuthorized();
            NameValueCollection request = new NameValueCollection();
            return this.restTemplate.PostForObjectAsync<TwitterProfile>(this.BuildUrl("friendships/create.json", "screen_name", screenName), request, operationCompleted);
        }

        public RestOperationCanceler UnfollowAsync(long userId, Action<RestOperationCompletedEventArgs<TwitterProfile>> operationCompleted)
        {
            this.EnsureIsAuthorized();
            NameValueCollection request = new NameValueCollection();
            return this.restTemplate.PostForObjectAsync<TwitterProfile>(this.BuildUrl("friendships/destroy.json", "user_id", userId.ToString()), request, operationCompleted);
        }

        public RestOperationCanceler UnfollowAsync(string screenName, Action<RestOperationCompletedEventArgs<TwitterProfile>> operationCompleted)
        {
            this.EnsureIsAuthorized();
            NameValueCollection request = new NameValueCollection();
            return this.restTemplate.PostForObjectAsync<TwitterProfile>(this.BuildUrl("friendships/destroy.json", "screen_name", screenName), request, operationCompleted);
        }

        public RestOperationCanceler EnableNotificationsAsync(long userId, Action<RestOperationCompletedEventArgs<TwitterProfile>> operationCompleted)
        {
            this.EnsureIsAuthorized();
            NameValueCollection request = new NameValueCollection();
            return this.restTemplate.PostForObjectAsync<TwitterProfile>(this.BuildUrl("notifications/follow.json", "user_id", userId.ToString()), request, operationCompleted);
        }

        public RestOperationCanceler EnableNotificationsAsync(string screenName, Action<RestOperationCompletedEventArgs<TwitterProfile>> operationCompleted)
        {
            this.EnsureIsAuthorized();
            NameValueCollection request = new NameValueCollection();
            return this.restTemplate.PostForObjectAsync<TwitterProfile>(this.BuildUrl("notifications/follow.json", "screen_name", screenName), request, operationCompleted);
        }

        public RestOperationCanceler DisableNotificationsAsync(long userId, Action<RestOperationCompletedEventArgs<TwitterProfile>> operationCompleted)
        {
            this.EnsureIsAuthorized();
            NameValueCollection request = new NameValueCollection();
            return this.restTemplate.PostForObjectAsync<TwitterProfile>(this.BuildUrl("notifications/leave.json", "user_id", userId.ToString()), request, operationCompleted);
        }

        public RestOperationCanceler DisableNotificationsAsync(string screenName, Action<RestOperationCompletedEventArgs<TwitterProfile>> operationCompleted)
        {
            this.EnsureIsAuthorized();
            NameValueCollection request = new NameValueCollection();
            return this.restTemplate.PostForObjectAsync<TwitterProfile>(this.BuildUrl("notifications/leave.json", "screen_name", screenName), request, operationCompleted);
        }

        public RestOperationCanceler FriendshipExistsAsync(string screenNameA, string screenNameB, Action<RestOperationCompletedEventArgs<bool>> operationCompleted)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("screen_name_a", screenNameA);
            parameters.Add("screen_name_b", screenNameB);
            return this.restTemplate.GetForObjectAsync<JsonValue>(this.BuildUrl("friendships/exists.json", parameters), 
                r => operationCompleted(new RestOperationCompletedEventArgs<bool>(r.Error == null ? r.Response.GetValue<bool>() : false, r.Error, r.Cancelled, r.UserState)));
        }

        public RestOperationCanceler GetIncomingFriendshipsAsync(Action<RestOperationCompletedEventArgs<CursoredList<long>>> operationCompleted)
        {
            return this.GetIncomingFriendshipsAsync(-1, operationCompleted);
        }

        public RestOperationCanceler GetIncomingFriendshipsAsync(long cursor, Action<RestOperationCompletedEventArgs<CursoredList<long>>> operationCompleted)
        {
            this.EnsureIsAuthorized();
            return this.restTemplate.GetForObjectAsync<CursoredList<long>>(this.BuildUrl("friendships/incoming.json", "cursor", cursor.ToString()), operationCompleted);
        }

        public RestOperationCanceler GetOutgoingFriendshipsAsync(Action<RestOperationCompletedEventArgs<CursoredList<long>>> operationCompleted)
        {
            return this.GetOutgoingFriendshipsAsync(-1, operationCompleted);
        }

        public RestOperationCanceler GetOutgoingFriendshipsAsync(long cursor, Action<RestOperationCompletedEventArgs<CursoredList<long>>> operationCompleted)
        {
            this.EnsureIsAuthorized();
            return this.restTemplate.GetForObjectAsync<CursoredList<long>>(this.BuildUrl("friendships/outgoing.json", "cursor", cursor.ToString()), operationCompleted);
        }
#endif

        #endregion

        #region Private Methods

#if NET_4_0 || SILVERLIGHT_5
        private CursoredList<TwitterProfile> GetCursoredProfileListAsync(List<long> userIds, long previousCursor, long nextCursor)
        {
            // TODO: Would be good to figure out how to retrieve profiles in a tighter-than-cursor granularity.
            List<List<long>> chunks = this.ChunkList(userIds, 100);
            CursoredList<TwitterProfile> users = new CursoredList<TwitterProfile>();
            users.PreviousCursor = previousCursor;
            users.NextCursor = nextCursor;
            Task[] tasks = new Task[chunks.Count];
            for (int i = 0; i < chunks.Count; i++)
            {
                string joinedIds = ArrayUtils.Join(chunks[i].ToArray());
                tasks[i] = (this.restTemplate.GetForObjectAsync<IList<TwitterProfile>>(this.BuildUrl("users/lookup.json", "user_id", joinedIds))
                    .ContinueWith(task => users.AddRange(task.Result)));
            }
            Task.WaitAll(tasks);
            return users;
        }
#else
#if !SILVERLIGHT
        private CursoredList<TwitterProfile> GetCursoredProfileList(List<long> userIds, long previousCursor, long nextCursor)
        {
            // TODO: Would be good to figure out how to retrieve profiles in a tighter-than-cursor granularity.
            List<List<long>> chunks = this.ChunkList(userIds, 100);
            CursoredList<TwitterProfile> users = new CursoredList<TwitterProfile>();
            users.PreviousCursor = previousCursor;
            users.NextCursor = nextCursor;
            foreach (List<long> userIdChunk in chunks)
            {
                string joinedIds = ArrayUtils.Join(userIdChunk.ToArray());
                users.AddRange(this.restTemplate.GetForObject<IList<TwitterProfile>>(this.BuildUrl("users/lookup.json", "user_id", joinedIds)));
            }
            return users;
        }
#endif

        private void GetCursoredProfileListAsync(
            RestOperationCompletedEventArgs<CursoredList<long>> userIdsResult, 
            Action<RestOperationCompletedEventArgs<CursoredList<TwitterProfile>>> operationCompleted)
        {
            if (userIdsResult.Error == null)
            {
                // TODO: Would be good to figure out how to retrieve profiles in a tighter-than-cursor granularity.
                List<List<long>> chunks = this.ChunkList(userIdsResult.Response, 100);
                CursoredList<TwitterProfile> users = new CursoredList<TwitterProfile>();
                users.PreviousCursor = userIdsResult.Response.PreviousCursor;
                users.NextCursor = userIdsResult.Response.NextCursor;
                IEnumerator<List<long>> chunkEnumerator = chunks.GetEnumerator();
                this.GetCursoredProfileListAsyncRecursive(userIdsResult, users, chunkEnumerator, operationCompleted);
            }
            else
            {
                operationCompleted(new RestOperationCompletedEventArgs<CursoredList<TwitterProfile>>(null, userIdsResult.Error, userIdsResult.Cancelled, userIdsResult.UserState));
            }
        }

        private void GetCursoredProfileListAsyncRecursive(
            RestOperationCompletedEventArgs<CursoredList<long>> userIdsResult, 
            CursoredList<TwitterProfile> users, 
            IEnumerator<List<long>> userIdChunk, 
            Action<RestOperationCompletedEventArgs<CursoredList<TwitterProfile>>> operationCompleted)
        {
            if (userIdChunk.MoveNext())
            {
                string joinedIds = ArrayUtils.Join(userIdChunk.Current.ToArray());
                this.restTemplate.GetForObjectAsync<IList<TwitterProfile>>(this.BuildUrl("users/lookup.json", "user_id", joinedIds),
                    r =>
                    {
                        if (r.Error == null)
                        {
                            users.AddRange(r.Response);
                            this.GetCursoredProfileListAsyncRecursive(userIdsResult, users, userIdChunk, operationCompleted);
                        }
                        else
                        {
                            operationCompleted(new RestOperationCompletedEventArgs<CursoredList<TwitterProfile>>(null, r.Error, r.Cancelled, r.UserState));
                        }
                    });
            }
            else
            {
                operationCompleted(new RestOperationCompletedEventArgs<CursoredList<TwitterProfile>>(users, userIdsResult.Error, userIdsResult.Cancelled, userIdsResult.UserState));
            }
        }

#endif

        private List<List<long>> ChunkList(List<long> list, int chunkSize)
        {
            List<List<long>> chunkedList = new List<List<long>>();
		    int start = 0;
		    while (start < list.Count) 
            {
			    int end = Math.Min(chunkSize + start, list.Count);
			    chunkedList.Add(list.GetRange(start, end - start));
			    start = end;
		    }
		    return chunkedList;
        }

        #endregion
    }
}