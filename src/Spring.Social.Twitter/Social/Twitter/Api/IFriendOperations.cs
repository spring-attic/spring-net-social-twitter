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
    /// Interface defining the operations for working with a user's friends and followers.
    /// </summary>
    /// <author>Craig Walls</author>
    /// <author>Bruno Baia (.NET)</author>
    public interface IFriendOperations
    {
#if NET_4_0 || SILVERLIGHT_5
        /// <summary>
        /// Asynchronously retrieves a list of up to 5000 users that the authenticated user follows.
        /// <para/>
        /// Note that this method make multiple calls to Twitter's REST API (one call to get a list of the friend IDs and one call for every 100 friends).
        /// <para/>
        /// If all you need is the friend IDs, consider calling GetFriendIds() instead.
        /// <para/>
        /// Or if you need only a subset of the user's friends, call UserOperations.GetUsers() passing in the list of friend IDs you need. 
        /// </summary>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// a cursored list of <see cref="TwitterProfile"/>s.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        Task<CursoredList<TwitterProfile>> GetFriendsAsync();

        /// <summary>
        /// Asynchronously retrieves a list of up to 5000 users that the authenticated user follows.
        /// <para/>
        /// Note that this method make multiple calls to Twitter's REST API (one call to get a list of the friend IDs and one call for every 100 friends).
        /// <para/>
        /// If all you need is the friend IDs, consider calling GetFriendIds() instead.
        /// <para/>
        /// Or if you need only a subset of the user's friends, call UserOperations.GetUsers() passing in the list of friend IDs you need. 
        /// </summary>
        /// <param name="cursor">The cursor used to fetch the friend IDs.</param>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// a cursored list of <see cref="TwitterProfile"/>s.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        Task<CursoredList<TwitterProfile>> GetFriendsInCursorAsync(long cursor);

        /// <summary>
        /// Asynchronously retrieves a list of up to 5000 users that the given user follows.
        /// <para/>
        /// Note that this method make multiple calls to Twitter's REST API (one call to get a list of the friend IDs and one call for every 100 friends).
        /// <para/>
        /// If all you need is the friend IDs, consider calling GetFriendIds() instead.
        /// <para/>
        /// Or if you need only a subset of the user's friends, call UserOperations.GetUsers() passing in the list of friend IDs you need. 
        /// </summary>
        /// <param name="userId">The user's Twitter ID.</param>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// a cursored list of <see cref="TwitterProfile"/>s.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        Task<CursoredList<TwitterProfile>> GetFriendsAsync(long userId);

        /// <summary>
        /// Asynchronously retrieves a list of up to 5000 users that the given user follows.
        /// <para/>
        /// Note that this method make multiple calls to Twitter's REST API (one call to get a list of the friend IDs and one call for every 100 friends).
        /// <para/>
        /// If all you need is the friend IDs, consider calling GetFriendIds() instead.
        /// <para/>
        /// Or if you need only a subset of the user's friends, call UserOperations.GetUsers() passing in the list of friend IDs you need. 
        /// </summary>
        /// <param name="userId">The user's Twitter ID.</param>
        /// <param name="cursor">The cursor used to fetch the friend IDs.</param>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// a cursored list of <see cref="TwitterProfile"/>s.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        Task<CursoredList<TwitterProfile>> GetFriendsInCursorAsync(long userId, long cursor);

        /// <summary>
        /// Asynchronously retrieves a list of up to 5000 users that the given user follows.
        /// <para/>
        /// Note that this method make multiple calls to Twitter's REST API (one call to get a list of the friend IDs and one call for every 100 friends).
        /// <para/>
        /// If all you need is the friend IDs, consider calling GetFriendIds() instead.
        /// <para/>
        /// Or if you need only a subset of the user's friends, call UserOperations.GetUsers() passing in the list of friend IDs you need. 
        /// </summary>
        /// <param name="screenName">The user's Twitter screen name.</param>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// a cursored list of <see cref="TwitterProfile"/>s.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        Task<CursoredList<TwitterProfile>> GetFriendsAsync(string screenName);

        /// <summary>
        /// Asynchronously retrieves a list of up to 5000 users that the given user follows.
        /// <para/>
        /// Note that this method make multiple calls to Twitter's REST API (one call to get a list of the friend IDs and one call for every 100 friends).
        /// <para/>
        /// If all you need is the friend IDs, consider calling GetFriendIds() instead.
        /// <para/>
        /// Or if you need only a subset of the user's friends, call UserOperations.GetUsers() passing in the list of friend IDs you need. 
        /// </summary>
        /// <param name="screenName">The user's Twitter screen name.</param>
        /// <param name="cursor">The cursor used to fetch the friend IDs.</param>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// a cursored list of <see cref="TwitterProfile"/>s.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        Task<CursoredList<TwitterProfile>> GetFriendsInCursorAsync(string screenName, long cursor);

        /// <summary>
        /// Asynchronously retrieves a list of up to 5000 IDs for the Twitter users that the authenticated user follows.
        /// <para/>
        /// Call GetFriendIdsForCursor() with a cursor value to get the next/previous page of entries.
        /// </summary>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// a cursored list of user IDs.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        Task<CursoredList<long>> GetFriendIdsAsync();

        /// <summary>
        /// Asynchronously retrieves a list of up to 5000 IDs for the Twitter users that the authenticated user follows.
        /// <para/>
        /// Call GetFriendIdsForCursor() with a cursor value to get the next/previous page of entries.
        /// </summary>
        /// <param name="cursor">
        /// The cursor value to fetch a specific page of entries. Use -1 for the first page of entries.
        /// </param>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// a cursored list of user IDs.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        Task<CursoredList<long>> GetFriendIdsInCursorAsync(long cursor);

        /// <summary>
        /// Asynchronously retrieves a list of up to 5000 IDs for the Twitter users that the given user follows.
        /// </summary>
        /// <param name="userId">The user's Twitter ID.</param>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// a cursored list of user IDs.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        Task<CursoredList<long>> GetFriendIdsAsync(long userId);

        /// <summary>
        /// Asynchronously retrieves a list of up to 5000 IDs for the Twitter users that the given user follows.
        /// </summary>
        /// <param name="userId">The user's Twitter ID.</param>
        /// <param name="cursor">The cursor value to fetch a specific page of entries. Use -1 for the first page of entries.</param>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// a cursored list of user IDs.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        Task<CursoredList<long>> GetFriendIdsInCursorAsync(long userId, long cursor);

        /// <summary>
        /// Asynchronously retrieves a list of up to 5000 IDs for the Twitter users that the given user follows.
        /// </summary>
        /// <param name="screenName">The user's Twitter screen name.</param>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// a cursored list of user IDs.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        Task<CursoredList<long>> GetFriendIdsAsync(string screenName);

        /// <summary>
        /// Asynchronously retrieves a list of up to 5000 IDs for the Twitter users that the given user follows.
        /// </summary>
        /// <param name="screenName">The user's Twitter screen name.</param>
        /// <param name="cursor">The cursor value to fetch a specific page of entries. Use -1 for the first page of entries.</param>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// a cursored list of user IDs.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        Task<CursoredList<long>> GetFriendIdsInCursorAsync(string screenName, long cursor);

        /// <summary>
        /// Asynchronously retrieves a list of up to 5000 users that the authenticated user is being followed by.
        /// <para/>
        /// Note that this method make multiple calls to Twitter's REST API (one call to get a list of the follower IDs and one call for every 100 followers).
        /// <para/>
        /// If all you need is the follower IDs, consider calling GetFollowerIds() instead.
        /// <para/>
        /// Or if you need only a subset of the user's followers, call UserOperations.GetUsers() passing in the list of follower IDs you need. 
        /// </summary>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// a cursored list of <see cref="TwitterProfile"/>s.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        Task<CursoredList<TwitterProfile>> GetFollowersAsync();

        /// <summary>
        /// Asynchronously retrieves a list of up to 5000 users that the authenticated user is being followed by.
        /// <para/>
        /// Note that this method make multiple calls to Twitter's REST API (one call to get a list of the follower IDs and one call for every 100 followers).
        /// <para/>
        /// If all you need is the follower IDs, consider calling GetFollowerIds() instead.
        /// <para/>
        /// Or if you need only a subset of the user's followers, call UserOperations.GetUsers() passing in the list of follower IDs you need. 
        /// </summary>
        /// <param name="cursor">The cursor used to fetch the follower IDs.</param>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// a cursored list of <see cref="TwitterProfile"/>s.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        Task<CursoredList<TwitterProfile>> GetFollowersInCursorAsync(long cursor);

        /// <summary>
        /// Asynchronously retrieves a list of up to 5000 users that the given user is being followed by.
        /// <para/>
        /// Note that this method make multiple calls to Twitter's REST API (one call to get a list of the follower IDs and one call for every 100 followers).
        /// <para/>
        /// If all you need is the follower IDs, consider calling GetFollowerIds() instead.
        /// <para/>
        /// Or if you need only a subset of the user's followers, call UserOperations.GetUsers() passing in the list of follower IDs you need. 
        /// </summary>
        /// <param name="userId">The user's Twitter ID.</param>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// a cursored list of <see cref="TwitterProfile"/>s.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        Task<CursoredList<TwitterProfile>> GetFollowersAsync(long userId);

        /// <summary>
        /// Asynchronously retrieves a list of up to 5000 users that the given user is being followed by.
        /// <para/>
        /// Note that this method make multiple calls to Twitter's REST API (one call to get a list of the follower IDs and one call for every 100 followers).
        /// <para/>
        /// If all you need is the follower IDs, consider calling GetFollowerIds() instead.
        /// <para/>
        /// Or if you need only a subset of the user's followers, call UserOperations.GetUsers() passing in the list of follower IDs you need. 
        /// </summary>
        /// <param name="userId">The user's Twitter ID.</param>
        /// <param name="cursor">The cursor used to fetch the follower IDs.</param>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// a cursored list of <see cref="TwitterProfile"/>s.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        Task<CursoredList<TwitterProfile>> GetFollowersInCursorAsync(long userId, long cursor);

        /// <summary>
        /// Asynchronously retrieves a list of up to 5000 users that the given user is being followed by.
        /// <para/>
        /// Note that this method make multiple calls to Twitter's REST API (one call to get a list of the follower IDs and one call for every 100 followers).
        /// <para/>
        /// If all you need is the follower IDs, consider calling GetFollowerIds() instead.
        /// <para/>
        /// Or if you need only a subset of the user's followers, call UserOperations.GetUsers() passing in the list of follower IDs you need. 
        /// </summary>
        /// <param name="screenName">The user's Twitter screen name.</param>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// a cursored list of <see cref="TwitterProfile"/>s.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        Task<CursoredList<TwitterProfile>> GetFollowersAsync(string screenName);

        /// <summary>
        /// Asynchronously retrieves a list of up to 5000 users that the given user is being followed by.
        /// <para/>
        /// Note that this method make multiple calls to Twitter's REST API (one call to get a list of the follower IDs and one call for every 100 followers).
        /// <para/>
        /// If all you need is the follower IDs, consider calling GetFollowerIds() instead.
        /// <para/>
        /// Or if you need only a subset of the user's followers, call UserOperations.GetUsers() passing in the list of follower IDs you need. 
        /// </summary>
        /// <param name="screenName">The user's Twitter screen name.</param>
        /// <param name="cursor">The cursor used to fetch the follower IDs.</param>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// a cursored list of <see cref="TwitterProfile"/>s.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        Task<CursoredList<TwitterProfile>> GetFollowersInCursorAsync(string screenName, long cursor);

        /// <summary>
        /// Asynchronously retrieves a list of up to 5000 IDs for the Twitter users that follow the authenticated user.
        /// </summary>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// a cursored list of user IDs.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        Task<CursoredList<long>> GetFollowerIdsAsync();

        /// <summary>
        /// Asynchronously retrieves a list of up to 5000 IDs for the Twitter users that follow the authenticated user.
        /// </summary>
        /// <param name="cursor">The cursor value to fetch a specific page of entries. Use -1 for the first page of entries.</param>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// a cursored list of user IDs.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        Task<CursoredList<long>> GetFollowerIdsInCursorAsync(long cursor);

        /// <summary>
        /// Asynchronously retrieves a list of up to 5000 IDs for the Twitter users that follow the given user.
        /// </summary>
        /// <param name="userId">The user's Twitter ID.</param>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// a cursored list of user IDs.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        Task<CursoredList<long>> GetFollowerIdsAsync(long userId);

        /// <summary>
        /// Asynchronously retrieves a list of up to 5000 IDs for the Twitter users that follow the given user.
        /// </summary>
        /// <param name="userId">The user's Twitter ID.</param>
        /// <param name="cursor">The cursor value to fetch a specific page of entries. Use -1 for the first page of entries.</param>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// a cursored list of user IDs.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        Task<CursoredList<long>> GetFollowerIdsInCursorAsync(long userId, long cursor);

        /// <summary>
        /// Asynchronously retrieves a list of up to 5000 IDs for the Twitter users that follow the given user.
        /// </summary>
        /// <param name="screenName">The user's Twitter screen name.</param>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// a cursored list of user IDs.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        Task<CursoredList<long>> GetFollowerIdsAsync(string screenName);

        /// <summary>
        /// Asynchronously retrieves a list of up to 5000 IDs for the Twitter users that follow the given user.
        /// </summary>
        /// <param name="screenName">The user's Twitter screen name.</param>
        /// <param name="cursor">The cursor value to fetch a specific page of entries. Use -1 for the first page of entries.</param>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// a cursored list of user IDs.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        Task<CursoredList<long>> GetFollowerIdsInCursorAsync(string screenName, long cursor);

        /// <summary>
        /// Asynchronously allows the authenticated user to follow (create a friendship) with another user.
        /// </summary>
        /// <param name="userId">The Twitter ID of the user to follow.</param>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// the <see cref="TwitterProfile"/> of the followed user if successful.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        Task<TwitterProfile> FollowAsync(long userId);

        /// <summary>
        /// Asynchronously allows the authenticated user to follow (create a friendship) with another user.
        /// </summary>
        /// <param name="screenName">The screen name of the user to follow.</param>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// the <see cref="TwitterProfile"/> of the followed user if successful.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        Task<TwitterProfile> FollowAsync(string screenName);

        /// <summary>
        /// Asynchronously allows the authenticated user to follow (create a friendship) with another user.
        /// </summary>
        /// <param name="userId">The Twitter ID of the user to unfollow.</param>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// the <see cref="TwitterProfile"/> of the unfollowed user if successful.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        Task<TwitterProfile> UnfollowAsync(long userId);

        /// <summary>
        /// Asynchronously allows the authenticated use to unfollow (destroy a friendship) with another user.
        /// </summary>
        /// <param name="screenName">The screen name of the user to unfollow.</param>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// the <see cref="TwitterProfile"/> of the unfollowed user if successful.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        Task<TwitterProfile> UnfollowAsync(string screenName);

        /// <summary>
        /// Asynchronously enables mobile device notifications from Twitter for the specified user.
        /// </summary>
        /// <param name="userId">The Twitter ID of the user to receive notifications for.</param>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// the <see cref="TwitterProfile"/> for the user.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        Task<TwitterProfile> EnableNotificationsAsync(long userId);

        /// <summary>
        /// Asynchronously enables mobile device notifications from Twitter for the specified user.
        /// </summary>
        /// <param name="screenName">The Twitter screen name of the user to receive notifications for.</param>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// the <see cref="TwitterProfile"/> for the user.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        Task<TwitterProfile> EnableNotificationsAsync(string screenName);

        /// <summary>
        /// Asynchronously disable mobile device notifications from Twitter for the specified user.
        /// </summary>
        /// <param name="userId">The Twitter ID of the user to stop notifications for.</param>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// the <see cref="TwitterProfile"/> for the user.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        Task<TwitterProfile> DisableNotificationsAsync(long userId);

        /// <summary>
        /// Asynchronously disable mobile device notifications from Twitter for the specified user.
        /// </summary>
        /// <param name="screenName">The Twitter screen name of the user to stop notifications for.</param>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// the <see cref="TwitterProfile"/> for the user.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        Task<TwitterProfile> DisableNotificationsAsync(string screenName);

        /// <summary>
        /// Asynchronously checks for a friendship between two users.
        /// </summary>
        /// <param name="screenNameA">The screen name of userA.</param>
        /// <param name="screenNameB">The screen name of userB.</param>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// a value indicating whether a user follows another user.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        Task<bool> FriendshipExistsAsync(string screenNameA, string screenNameB);

        /// <summary>
        /// Asynchronously returns an array of numeric IDs for every user who has a pending request to follow the authenticating user.
        /// </summary>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// a cursored list of user ids.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        Task<CursoredList<long>> GetIncomingFriendshipsAsync();

        /// <summary>
        /// Asynchronously returns an array of numeric IDs for every user who has a pending request to follow the authenticating user.
        /// </summary>
        /// <param name="cursor">The cursor of the page to retrieve.</param>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// a cursored list of user ids.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        Task<CursoredList<long>> GetIncomingFriendshipsAsync(long cursor);
     
        /// <summary>
        /// Asynchronously returns an array of numeric IDs for every protected user for whom the authenticating user has a pending follow request.
        /// </summary>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// a cursored list of user ids.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        Task<CursoredList<long>> GetOutgoingFriendshipsAsync();

        /// <summary>
        /// Asynchronously returns an array of numeric IDs for every protected user for whom the authenticating user has a pending follow request.
        /// </summary>
        /// <param name="cursor">The cursor of the page to retrieve.</param>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// a cursored list of user ids.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        Task<CursoredList<long>> GetOutgoingFriendshipsAsync(long cursor);
#else
#if !SILVERLIGHT
        /// <summary>
        /// Retrieves a list of up to 5000 users that the authenticated user follows.
        /// <para/>
        /// Note that this method make multiple calls to Twitter's REST API (one call to get a list of the friend IDs and one call for every 100 friends).
        /// <para/>
        /// If all you need is the friend IDs, consider calling GetFriendIds() instead.
        /// <para/>
        /// Or if you need only a subset of the user's friends, call UserOperations.GetUsers() passing in the list of friend IDs you need. 
        /// </summary>
        /// <returns>A cursored list of <see cref="TwitterProfile"/>s.</returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        CursoredList<TwitterProfile> GetFriends();

        /// <summary>
        /// Retrieves a list of up to 5000 users that the authenticated user follows.
        /// <para/>
        /// Note that this method make multiple calls to Twitter's REST API (one call to get a list of the friend IDs and one call for every 100 friends).
        /// <para/>
        /// If all you need is the friend IDs, consider calling GetFriendIds() instead.
        /// <para/>
        /// Or if you need only a subset of the user's friends, call UserOperations.GetUsers() passing in the list of friend IDs you need. 
        /// </summary>
        /// <param name="cursor">The cursor used to fetch the friend IDs.</param>
        /// <returns>A cursored list of <see cref="TwitterProfile"/>s.</returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        CursoredList<TwitterProfile> GetFriendsInCursor(long cursor);

        /// <summary>
        /// Retrieves a list of up to 5000 users that the given user follows.
        /// <para/>
        /// Note that this method make multiple calls to Twitter's REST API (one call to get a list of the friend IDs and one call for every 100 friends).
        /// <para/>
        /// If all you need is the friend IDs, consider calling GetFriendIds() instead.
        /// <para/>
        /// Or if you need only a subset of the user's friends, call UserOperations.GetUsers() passing in the list of friend IDs you need. 
        /// </summary>
        /// <param name="userId">The user's Twitter ID.</param>
        /// <returns>A cursored list of <see cref="TwitterProfile"/>s.</returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        CursoredList<TwitterProfile> GetFriends(long userId);

        /// <summary>
        /// Retrieves a list of up to 5000 users that the given user follows.
        /// <para/>
        /// Note that this method make multiple calls to Twitter's REST API (one call to get a list of the friend IDs and one call for every 100 friends).
        /// <para/>
        /// If all you need is the friend IDs, consider calling GetFriendIds() instead.
        /// <para/>
        /// Or if you need only a subset of the user's friends, call UserOperations.GetUsers() passing in the list of friend IDs you need. 
        /// </summary>
        /// <param name="userId">The user's Twitter ID.</param>
        /// <param name="cursor">The cursor used to fetch the friend IDs.</param>
        /// <returns>A cursored list of <see cref="TwitterProfile"/>s.</returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        CursoredList<TwitterProfile> GetFriendsInCursor(long userId, long cursor);

        /// <summary>
        /// Retrieves a list of up to 5000 users that the given user follows.
        /// <para/>
        /// Note that this method make multiple calls to Twitter's REST API (one call to get a list of the friend IDs and one call for every 100 friends).
        /// <para/>
        /// If all you need is the friend IDs, consider calling GetFriendIds() instead.
        /// <para/>
        /// Or if you need only a subset of the user's friends, call UserOperations.GetUsers() passing in the list of friend IDs you need. 
        /// </summary>
        /// <param name="screenName">The user's Twitter screen name.</param>
        /// <returns>A cursored list of <see cref="TwitterProfile"/>s.</returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        CursoredList<TwitterProfile> GetFriends(string screenName);

        /// <summary>
        /// Retrieves a list of up to 5000 users that the given user follows.
        /// <para/>
        /// Note that this method make multiple calls to Twitter's REST API (one call to get a list of the friend IDs and one call for every 100 friends).
        /// <para/>
        /// If all you need is the friend IDs, consider calling GetFriendIds() instead.
        /// <para/>
        /// Or if you need only a subset of the user's friends, call UserOperations.GetUsers() passing in the list of friend IDs you need. 
        /// </summary>
        /// <param name="screenName">The user's Twitter screen name.</param>
        /// <param name="cursor">The cursor used to fetch the friend IDs.</param>
        /// <returns>A cursored list of <see cref="TwitterProfile"/>s.</returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        CursoredList<TwitterProfile> GetFriendsInCursor(string screenName, long cursor);

        /// <summary>
        /// Retrieves a list of up to 5000 IDs for the Twitter users that the authenticated user follows.
        /// <para/>
        /// Call GetFriendIdsForCursor() with a cursor value to get the next/previous page of entries.
        /// </summary>
        /// <returns>A cursored list of user IDs.</returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        CursoredList<long> GetFriendIds();

        /// <summary>
        /// Retrieves a list of up to 5000 IDs for the Twitter users that the authenticated user follows.
        /// <para/>
        /// Call GetFriendIdsForCursor() with a cursor value to get the next/previous page of entries.
        /// </summary>
        /// <param name="cursor">
        /// The cursor value to fetch a specific page of entries. Use -1 for the first page of entries.
        /// </param>
        /// <returns>A cursored list of user IDs.</returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        CursoredList<long> GetFriendIdsInCursor(long cursor);

        /// <summary>
        /// Retrieves a list of up to 5000 IDs for the Twitter users that the given user follows.
        /// </summary>
        /// <param name="userId">The user's Twitter ID.</param>
        /// <returns>A cursored list of user IDs.</returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        CursoredList<long> GetFriendIds(long userId);

        /// <summary>
        /// Retrieves a list of up to 5000 IDs for the Twitter users that the given user follows.
        /// </summary>
        /// <param name="userId">The user's Twitter ID.</param>
        /// <param name="cursor">The cursor value to fetch a specific page of entries. Use -1 for the first page of entries.</param>
        /// <returns>A cursored list of user IDs.</returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        CursoredList<long> GetFriendIdsInCursor(long userId, long cursor);

        /// <summary>
        /// Retrieves a list of up to 5000 IDs for the Twitter users that the given user follows.
        /// </summary>
        /// <param name="screenName">The user's Twitter screen name.</param>
        /// <returns>A cursored list of user IDs.</returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        CursoredList<long> GetFriendIds(string screenName);

        /// <summary>
        /// Retrieves a list of up to 5000 IDs for the Twitter users that the given user follows.
        /// </summary>
        /// <param name="screenName">The user's Twitter screen name.</param>
        /// <param name="cursor">The cursor value to fetch a specific page of entries. Use -1 for the first page of entries.</param>
        /// <returns>A cursored list of user IDs.</returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        CursoredList<long> GetFriendIdsInCursor(string screenName, long cursor);

        /// <summary>
        /// Retrieves a list of up to 5000 users that the authenticated user is being followed by.
        /// <para/>
        /// Note that this method make multiple calls to Twitter's REST API (one call to get a list of the follower IDs and one call for every 100 followers).
        /// <para/>
        /// If all you need is the follower IDs, consider calling GetFollowerIds() instead.
        /// <para/>
        /// Or if you need only a subset of the user's followers, call UserOperations.GetUsers() passing in the list of follower IDs you need. 
        /// </summary>
        /// <returns>A cursored list of <see cref="TwitterProfile"/>s.</returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        CursoredList<TwitterProfile> GetFollowers();

        /// <summary>
        /// Retrieves a list of up to 5000 users that the authenticated user is being followed by.
        /// <para/>
        /// Note that this method make multiple calls to Twitter's REST API (one call to get a list of the follower IDs and one call for every 100 followers).
        /// <para/>
        /// If all you need is the follower IDs, consider calling GetFollowerIds() instead.
        /// <para/>
        /// Or if you need only a subset of the user's followers, call UserOperations.GetUsers() passing in the list of follower IDs you need. 
        /// </summary>
        /// <param name="cursor">The cursor used to fetch the follower IDs.</param>
        /// <returns>A cursored list of <see cref="TwitterProfile"/>s.</returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        CursoredList<TwitterProfile> GetFollowersInCursor(long cursor);

        /// <summary>
        /// Retrieves a list of up to 5000 users that the given user is being followed by.
        /// <para/>
        /// Note that this method make multiple calls to Twitter's REST API (one call to get a list of the follower IDs and one call for every 100 followers).
        /// <para/>
        /// If all you need is the follower IDs, consider calling GetFollowerIds() instead.
        /// <para/>
        /// Or if you need only a subset of the user's followers, call UserOperations.GetUsers() passing in the list of follower IDs you need. 
        /// </summary>
        /// <param name="userId">The user's Twitter ID.</param>
        /// <returns>A cursored list of <see cref="TwitterProfile"/>s.</returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        CursoredList<TwitterProfile> GetFollowers(long userId);

        /// <summary>
        /// Retrieves a list of up to 5000 users that the given user is being followed by.
        /// <para/>
        /// Note that this method make multiple calls to Twitter's REST API (one call to get a list of the follower IDs and one call for every 100 followers).
        /// <para/>
        /// If all you need is the follower IDs, consider calling GetFollowerIds() instead.
        /// <para/>
        /// Or if you need only a subset of the user's followers, call UserOperations.GetUsers() passing in the list of follower IDs you need. 
        /// </summary>
        /// <param name="userId">The user's Twitter ID.</param>
        /// <param name="cursor">The cursor used to fetch the follower IDs.</param>
        /// <returns>A cursored list of <see cref="TwitterProfile"/>s.</returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        CursoredList<TwitterProfile> GetFollowersInCursor(long userId, long cursor);

        /// <summary>
        /// Retrieves a list of up to 5000 users that the given user is being followed by.
        /// <para/>
        /// Note that this method make multiple calls to Twitter's REST API (one call to get a list of the follower IDs and one call for every 100 followers).
        /// <para/>
        /// If all you need is the follower IDs, consider calling GetFollowerIds() instead.
        /// <para/>
        /// Or if you need only a subset of the user's followers, call UserOperations.GetUsers() passing in the list of follower IDs you need. 
        /// </summary>
        /// <param name="screenName">The user's Twitter screen name.</param>
        /// <returns>A cursored list of <see cref="TwitterProfile"/>s.</returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        CursoredList<TwitterProfile> GetFollowers(string screenName);

        /// <summary>
        /// Retrieves a list of up to 5000 users that the given user is being followed by.
        /// <para/>
        /// Note that this method make multiple calls to Twitter's REST API (one call to get a list of the follower IDs and one call for every 100 followers).
        /// <para/>
        /// If all you need is the follower IDs, consider calling GetFollowerIds() instead.
        /// <para/>
        /// Or if you need only a subset of the user's followers, call UserOperations.GetUsers() passing in the list of follower IDs you need. 
        /// </summary>
        /// <param name="screenName">The user's Twitter screen name.</param>
        /// <param name="cursor">The cursor used to fetch the follower IDs.</param>
        /// <returns>A cursored list of <see cref="TwitterProfile"/>s.</returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        CursoredList<TwitterProfile> GetFollowersInCursor(string screenName, long cursor);

        /// <summary>
        /// Retrieves a list of up to 5000 IDs for the Twitter users that follow the authenticated user.
        /// </summary>
        /// <returns>A cursored list of user IDs.</returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        CursoredList<long> GetFollowerIds();

        /// <summary>
        /// Retrieves a list of up to 5000 IDs for the Twitter users that follow the authenticated user.
        /// </summary>
        /// <param name="cursor">The cursor value to fetch a specific page of entries. Use -1 for the first page of entries.</param>
        /// <returns>A cursored list of user IDs.</returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        CursoredList<long> GetFollowerIdsInCursor(long cursor);

        /// <summary>
        /// Retrieves a list of up to 5000 IDs for the Twitter users that follow the given user.
        /// </summary>
        /// <param name="userId">The user's Twitter ID.</param>
        /// <returns>A cursored list of user IDs.</returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        CursoredList<long> GetFollowerIds(long userId);

        /// <summary>
        /// Retrieves a list of up to 5000 IDs for the Twitter users that follow the given user.
        /// </summary>
        /// <param name="userId">The user's Twitter ID.</param>
        /// <param name="cursor">The cursor value to fetch a specific page of entries. Use -1 for the first page of entries.</param>
        /// <returns>A cursored list of user IDs.</returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        CursoredList<long> GetFollowerIdsInCursor(long userId, long cursor);

        /// <summary>
        /// Retrieves a list of up to 5000 IDs for the Twitter users that follow the given user.
        /// </summary>
        /// <param name="screenName">The user's Twitter screen name.</param>
        /// <returns>A cursored list of user IDs.</returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        CursoredList<long> GetFollowerIds(string screenName);

        /// <summary>
        /// Retrieves a list of up to 5000 IDs for the Twitter users that follow the given user.
        /// </summary>
        /// <param name="screenName">The user's Twitter screen name.</param>
        /// <param name="cursor">The cursor value to fetch a specific page of entries. Use -1 for the first page of entries.</param>
        /// <returns>A cursored list of user IDs.</returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        CursoredList<long> GetFollowerIdsInCursor(string screenName, long cursor);

        /// <summary>
        /// Allows the authenticated user to follow (create a friendship) with another user.
        /// </summary>
        /// <param name="userId">The Twitter ID of the user to follow.</param>
        /// <returns>The <see cref="TwitterProfile"/> of the followed user if successful.</returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        TwitterProfile Follow(long userId);

        /// <summary>
        /// Allows the authenticated user to follow (create a friendship) with another user.
        /// </summary>
        /// <param name="screenName">The screen name of the user to follow.</param>
        /// <returns>The <see cref="TwitterProfile"/> of the followed user if successful</returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        TwitterProfile Follow(string screenName);

        /// <summary>
        /// Allows the authenticated user to follow (create a friendship) with another user.
        /// </summary>
        /// <param name="userId">The Twitter ID of the user to unfollow.</param>
        /// <returns>The <see cref="TwitterProfile"/> of the unfollowed user if successful.</returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        TwitterProfile Unfollow(long userId);

        /// <summary>
        /// Allows the authenticated use to unfollow (destroy a friendship) with another user.
        /// </summary>
        /// <param name="screenName">The screen name of the user to unfollow.</param>
        /// <returns>The <see cref="TwitterProfile"/> of the unfollowed user if successful.</returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        TwitterProfile Unfollow(string screenName);

        /// <summary>
        /// Enables mobile device notifications from Twitter for the specified user.
        /// </summary>
        /// <param name="userId">The Twitter ID of the user to receive notifications for.</param>
        /// <returns>The <see cref="TwitterProfile"/> for the user.</returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        TwitterProfile EnableNotifications(long userId);

        /// <summary>
        /// Enables mobile device notifications from Twitter for the specified user.
        /// </summary>
        /// <param name="screenName">The Twitter screen name of the user to receive notifications for.</param>
        /// <returns>The <see cref="TwitterProfile"/> for the user.</returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        TwitterProfile EnableNotifications(string screenName);

        /// <summary>
        /// Disable mobile device notifications from Twitter for the specified user.
        /// </summary>
        /// <param name="userId">The Twitter ID of the user to stop notifications for.</param>
        /// <returns>The <see cref="TwitterProfile"/> for the user.</returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        TwitterProfile DisableNotifications(long userId);

        /// <summary>
        /// Disable mobile device notifications from Twitter for the specified user.
        /// </summary>
        /// <param name="screenName">The Twitter screen name of the user to stop notifications for.</param>
        /// <returns>The <see cref="TwitterProfile"/> for the user.</returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        TwitterProfile DisableNotifications(string screenName);

        /// <summary>
        /// Checks for a friendship between two users.
        /// </summary>
        /// <param name="screenNameA">The screen name of userA.</param>
        /// <param name="screenNameB">The screen name of userB.</param>
        /// <returns>Returns true if userA follows userB.</returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        bool FriendshipExists(string screenNameA, string screenNameB);

        /// <summary>
        /// Returns an array of numeric IDs for every user who has a pending request to follow the authenticating user.
        /// </summary>
        /// <returns>A cursored list of user ids.</returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        CursoredList<long> GetIncomingFriendships();

        /// <summary>
        /// Returns an array of numeric IDs for every user who has a pending request to follow the authenticating user.
        /// </summary>
        /// <param name="cursor">The cursor of the page to retrieve.</param>
        /// <returns>A cursored list of user ids.</returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        CursoredList<long> GetIncomingFriendships(long cursor);
     
        /// <summary>
        /// Returns an array of numeric IDs for every protected user for whom the authenticating user has a pending follow request.
        /// </summary>
        /// <returns>A cursored list of user ids.</returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        CursoredList<long> GetOutgoingFriendships();

        /// <summary>
        /// Returns an array of numeric IDs for every protected user for whom the authenticating user has a pending follow request.
        /// </summary>
        /// <param name="cursor">The cursor of the page to retrieve.</param>
        /// <returns>A cursored list of user ids.</returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        CursoredList<long> GetOutgoingFriendships(long cursor);
#endif

        /// <summary>
        /// Asynchronously retrieves a list of up to 5000 users that the authenticated user follows.
        /// <para/>
        /// Note that this method make multiple calls to Twitter's REST API (one call to get a list of the friend IDs and one call for every 100 friends).
        /// <para/>
        /// If all you need is the friend IDs, consider calling GetFriendIds() instead.
        /// <para/>
        /// Or if you need only a subset of the user's friends, call UserOperations.GetUsers() passing in the list of friend IDs you need. 
        /// </summary>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides a cursored list of <see cref="TwitterProfile"/>s.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        RestOperationCanceler GetFriendsAsync(Action<RestOperationCompletedEventArgs<CursoredList<TwitterProfile>>> operationCompleted);

        /// <summary>
        /// Asynchronously retrieves a list of up to 5000 users that the authenticated user follows.
        /// <para/>
        /// Note that this method make multiple calls to Twitter's REST API (one call to get a list of the friend IDs and one call for every 100 friends).
        /// <para/>
        /// If all you need is the friend IDs, consider calling GetFriendIds() instead.
        /// <para/>
        /// Or if you need only a subset of the user's friends, call UserOperations.GetUsers() passing in the list of friend IDs you need. 
        /// </summary>
        /// <param name="cursor">The cursor used to fetch the friend IDs.</param>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides a cursored list of <see cref="TwitterProfile"/>s.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        RestOperationCanceler GetFriendsInCursorAsync(long cursor, Action<RestOperationCompletedEventArgs<CursoredList<TwitterProfile>>> operationCompleted);

        /// <summary>
        /// Asynchronously retrieves a list of up to 5000 users that the given user follows.
        /// <para/>
        /// Note that this method make multiple calls to Twitter's REST API (one call to get a list of the friend IDs and one call for every 100 friends).
        /// <para/>
        /// If all you need is the friend IDs, consider calling GetFriendIds() instead.
        /// <para/>
        /// Or if you need only a subset of the user's friends, call UserOperations.GetUsers() passing in the list of friend IDs you need. 
        /// </summary>
        /// <param name="userId">The user's Twitter ID.</param>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides a cursored list of <see cref="TwitterProfile"/>s.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        RestOperationCanceler GetFriendsAsync(long userId, Action<RestOperationCompletedEventArgs<CursoredList<TwitterProfile>>> operationCompleted);

        /// <summary>
        /// Asynchronously retrieves a list of up to 5000 users that the given user follows.
        /// <para/>
        /// Note that this method make multiple calls to Twitter's REST API (one call to get a list of the friend IDs and one call for every 100 friends).
        /// <para/>
        /// If all you need is the friend IDs, consider calling GetFriendIds() instead.
        /// <para/>
        /// Or if you need only a subset of the user's friends, call UserOperations.GetUsers() passing in the list of friend IDs you need. 
        /// </summary>
        /// <param name="userId">The user's Twitter ID.</param>
        /// <param name="cursor">The cursor used to fetch the friend IDs.</param>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides a cursored list of <see cref="TwitterProfile"/>s.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        RestOperationCanceler GetFriendsInCursorAsync(long userId, long cursor, Action<RestOperationCompletedEventArgs<CursoredList<TwitterProfile>>> operationCompleted);

        /// <summary>
        /// Asynchronously retrieves a list of up to 5000 users that the given user follows.
        /// <para/>
        /// Note that this method make multiple calls to Twitter's REST API (one call to get a list of the friend IDs and one call for every 100 friends).
        /// <para/>
        /// If all you need is the friend IDs, consider calling GetFriendIds() instead.
        /// <para/>
        /// Or if you need only a subset of the user's friends, call UserOperations.GetUsers() passing in the list of friend IDs you need. 
        /// </summary>
        /// <param name="screenName">The user's Twitter screen name.</param>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides a cursored list of <see cref="TwitterProfile"/>s.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        RestOperationCanceler GetFriendsAsync(string screenName, Action<RestOperationCompletedEventArgs<CursoredList<TwitterProfile>>> operationCompleted);

        /// <summary>
        /// Asynchronously retrieves a list of up to 5000 users that the given user follows.
        /// <para/>
        /// Note that this method make multiple calls to Twitter's REST API (one call to get a list of the friend IDs and one call for every 100 friends).
        /// <para/>
        /// If all you need is the friend IDs, consider calling GetFriendIds() instead.
        /// <para/>
        /// Or if you need only a subset of the user's friends, call UserOperations.GetUsers() passing in the list of friend IDs you need. 
        /// </summary>
        /// <param name="screenName">The user's Twitter screen name.</param>
        /// <param name="cursor">The cursor used to fetch the friend IDs.</param>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides a cursored list of <see cref="TwitterProfile"/>s.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        RestOperationCanceler GetFriendsInCursorAsync(string screenName, long cursor, Action<RestOperationCompletedEventArgs<CursoredList<TwitterProfile>>> operationCompleted);

        /// <summary>
        /// Asynchronously retrieves a list of up to 5000 IDs for the Twitter users that the authenticated user follows.
        /// <para/>
        /// Call GetFriendIdsForCursor() with a cursor value to get the next/previous page of entries.
        /// </summary>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides a cursored list of user IDs.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        RestOperationCanceler GetFriendIdsAsync(Action<RestOperationCompletedEventArgs<CursoredList<long>>> operationCompleted);

        /// <summary>
        /// Asynchronously retrieves a list of up to 5000 IDs for the Twitter users that the authenticated user follows.
        /// <para/>
        /// Call GetFriendIdsForCursor() with a cursor value to get the next/previous page of entries.
        /// </summary>
        /// <param name="cursor">
        /// The cursor value to fetch a specific page of entries. Use -1 for the first page of entries.
        /// </param>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides a cursored list of user IDs.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        RestOperationCanceler GetFriendIdsInCursorAsync(long cursor, Action<RestOperationCompletedEventArgs<CursoredList<long>>> operationCompleted);

        /// <summary>
        /// Asynchronously retrieves a list of up to 5000 IDs for the Twitter users that the given user follows.
        /// </summary>
        /// <param name="userId">The user's Twitter ID.</param>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides a cursored list of user IDs.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        RestOperationCanceler GetFriendIdsAsync(long userId, Action<RestOperationCompletedEventArgs<CursoredList<long>>> operationCompleted);

        /// <summary>
        /// Asynchronously retrieves a list of up to 5000 IDs for the Twitter users that the given user follows.
        /// </summary>
        /// <param name="userId">The user's Twitter ID.</param>
        /// <param name="cursor">The cursor value to fetch a specific page of entries. Use -1 for the first page of entries.</param>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides a cursored list of user IDs.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        RestOperationCanceler GetFriendIdsInCursorAsync(long userId, long cursor, Action<RestOperationCompletedEventArgs<CursoredList<long>>> operationCompleted);

        /// <summary>
        /// Asynchronously retrieves a list of up to 5000 IDs for the Twitter users that the given user follows.
        /// </summary>
        /// <param name="screenName">The user's Twitter screen name.</param>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides a cursored list of user IDs.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        RestOperationCanceler GetFriendIdsAsync(string screenName, Action<RestOperationCompletedEventArgs<CursoredList<long>>> operationCompleted);

        /// <summary>
        /// Asynchronously retrieves a list of up to 5000 IDs for the Twitter users that the given user follows.
        /// </summary>
        /// <param name="screenName">The user's Twitter screen name.</param>
        /// <param name="cursor">The cursor value to fetch a specific page of entries. Use -1 for the first page of entries.</param>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides a cursored list of user IDs.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        RestOperationCanceler GetFriendIdsInCursorAsync(string screenName, long cursor, Action<RestOperationCompletedEventArgs<CursoredList<long>>> operationCompleted);

        /// <summary>
        /// Asynchronously retrieves a list of up to 5000 users that the authenticated user is being followed by.
        /// <para/>
        /// Note that this method make multiple calls to Twitter's REST API (one call to get a list of the follower IDs and one call for every 100 followers).
        /// <para/>
        /// If all you need is the follower IDs, consider calling GetFollowerIds() instead.
        /// <para/>
        /// Or if you need only a subset of the user's followers, call UserOperations.GetUsers() passing in the list of follower IDs you need. 
        /// </summary>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides a cursored list of <see cref="TwitterProfile"/>s.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        RestOperationCanceler GetFollowersAsync(Action<RestOperationCompletedEventArgs<CursoredList<TwitterProfile>>> operationCompleted);

        /// <summary>
        /// Asynchronously retrieves a list of up to 5000 users that the authenticated user is being followed by.
        /// <para/>
        /// Note that this method make multiple calls to Twitter's REST API (one call to get a list of the follower IDs and one call for every 100 followers).
        /// <para/>
        /// If all you need is the follower IDs, consider calling GetFollowerIds() instead.
        /// <para/>
        /// Or if you need only a subset of the user's followers, call UserOperations.GetUsers() passing in the list of follower IDs you need. 
        /// </summary>
        /// <param name="cursor">The cursor used to fetch the follower IDs.</param>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides a cursored list of <see cref="TwitterProfile"/>s.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        RestOperationCanceler GetFollowersInCursorAsync(long cursor, Action<RestOperationCompletedEventArgs<CursoredList<TwitterProfile>>> operationCompleted);

        /// <summary>
        /// Asynchronously retrieves a list of up to 5000 users that the given user is being followed by.
        /// <para/>
        /// Note that this method make multiple calls to Twitter's REST API (one call to get a list of the follower IDs and one call for every 100 followers).
        /// <para/>
        /// If all you need is the follower IDs, consider calling GetFollowerIds() instead.
        /// <para/>
        /// Or if you need only a subset of the user's followers, call UserOperations.GetUsers() passing in the list of follower IDs you need. 
        /// </summary>
        /// <param name="userId">The user's Twitter ID.</param>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides a cursored list of <see cref="TwitterProfile"/>s.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        RestOperationCanceler GetFollowersAsync(long userId, Action<RestOperationCompletedEventArgs<CursoredList<TwitterProfile>>> operationCompleted);

        /// <summary>
        /// Asynchronously retrieves a list of up to 5000 users that the given user is being followed by.
        /// <para/>
        /// Note that this method make multiple calls to Twitter's REST API (one call to get a list of the follower IDs and one call for every 100 followers).
        /// <para/>
        /// If all you need is the follower IDs, consider calling GetFollowerIds() instead.
        /// <para/>
        /// Or if you need only a subset of the user's followers, call UserOperations.GetUsers() passing in the list of follower IDs you need. 
        /// </summary>
        /// <param name="userId">The user's Twitter ID.</param>
        /// <param name="cursor">The cursor used to fetch the follower IDs.</param>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides a cursored list of <see cref="TwitterProfile"/>s.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        RestOperationCanceler GetFollowersInCursorAsync(long userId, long cursor, Action<RestOperationCompletedEventArgs<CursoredList<TwitterProfile>>> operationCompleted);

        /// <summary>
        /// Asynchronously retrieves a list of up to 5000 users that the given user is being followed by.
        /// <para/>
        /// Note that this method make multiple calls to Twitter's REST API (one call to get a list of the follower IDs and one call for every 100 followers).
        /// <para/>
        /// If all you need is the follower IDs, consider calling GetFollowerIds() instead.
        /// <para/>
        /// Or if you need only a subset of the user's followers, call UserOperations.GetUsers() passing in the list of follower IDs you need. 
        /// </summary>
        /// <param name="screenName">The user's Twitter screen name.</param>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides a cursored list of <see cref="TwitterProfile"/>s.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        RestOperationCanceler GetFollowersAsync(string screenName, Action<RestOperationCompletedEventArgs<CursoredList<TwitterProfile>>> operationCompleted);

        /// <summary>
        /// Asynchronously retrieves a list of up to 5000 users that the given user is being followed by.
        /// <para/>
        /// Note that this method make multiple calls to Twitter's REST API (one call to get a list of the follower IDs and one call for every 100 followers).
        /// <para/>
        /// If all you need is the follower IDs, consider calling GetFollowerIds() instead.
        /// <para/>
        /// Or if you need only a subset of the user's followers, call UserOperations.GetUsers() passing in the list of follower IDs you need. 
        /// </summary>
        /// <param name="screenName">The user's Twitter screen name.</param>
        /// <param name="cursor">The cursor used to fetch the follower IDs.</param>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides a cursored list of <see cref="TwitterProfile"/>s.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        RestOperationCanceler GetFollowersInCursorAsync(string screenName, long cursor, Action<RestOperationCompletedEventArgs<CursoredList<TwitterProfile>>> operationCompleted);

        /// <summary>
        /// Asynchronously retrieves a list of up to 5000 IDs for the Twitter users that follow the authenticated user.
        /// </summary>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides a cursored list of user IDs.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        RestOperationCanceler GetFollowerIdsAsync(Action<RestOperationCompletedEventArgs<CursoredList<long>>> operationCompleted);

        /// <summary>
        /// Asynchronously retrieves a list of up to 5000 IDs for the Twitter users that follow the authenticated user.
        /// </summary>
        /// <param name="cursor">The cursor value to fetch a specific page of entries. Use -1 for the first page of entries.</param>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides a cursored list of user IDs.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        RestOperationCanceler GetFollowerIdsInCursorAsync(long cursor, Action<RestOperationCompletedEventArgs<CursoredList<long>>> operationCompleted);

        /// <summary>
        /// Asynchronously retrieves a list of up to 5000 IDs for the Twitter users that follow the given user.
        /// </summary>
        /// <param name="userId">The user's Twitter ID.</param>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides a cursored list of user IDs.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        RestOperationCanceler GetFollowerIdsAsync(long userId, Action<RestOperationCompletedEventArgs<CursoredList<long>>> operationCompleted);

        /// <summary>
        /// Asynchronously retrieves a list of up to 5000 IDs for the Twitter users that follow the given user.
        /// </summary>
        /// <param name="userId">The user's Twitter ID.</param>
        /// <param name="cursor">The cursor value to fetch a specific page of entries. Use -1 for the first page of entries.</param>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides a cursored list of user IDs.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        RestOperationCanceler GetFollowerIdsInCursorAsync(long userId, long cursor, Action<RestOperationCompletedEventArgs<CursoredList<long>>> operationCompleted);

        /// <summary>
        /// Asynchronously retrieves a list of up to 5000 IDs for the Twitter users that follow the given user.
        /// </summary>
        /// <param name="screenName">The user's Twitter screen name.</param>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides a cursored list of user IDs.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        RestOperationCanceler GetFollowerIdsAsync(string screenName, Action<RestOperationCompletedEventArgs<CursoredList<long>>> operationCompleted);

        /// <summary>
        /// Asynchronously retrieves a list of up to 5000 IDs for the Twitter users that follow the given user.
        /// </summary>
        /// <param name="screenName">The user's Twitter screen name.</param>
        /// <param name="cursor">The cursor value to fetch a specific page of entries. Use -1 for the first page of entries.</param>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides a cursored list of user IDs.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        RestOperationCanceler GetFollowerIdsInCursorAsync(string screenName, long cursor, Action<RestOperationCompletedEventArgs<CursoredList<long>>> operationCompleted);

        /// <summary>
        /// Asynchronously allows the authenticated user to follow (create a friendship) with another user.
        /// </summary>
        /// <param name="userId">The Twitter ID of the user to follow.</param>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides the <see cref="TwitterProfile"/> of the followed user if successful.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        RestOperationCanceler FollowAsync(long userId, Action<RestOperationCompletedEventArgs<TwitterProfile>> operationCompleted);

        /// <summary>
        /// Asynchronously allows the authenticated user to follow (create a friendship) with another user.
        /// </summary>
        /// <param name="screenName">The screen name of the user to follow.</param>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides the <see cref="TwitterProfile"/> of the followed user if successful.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        RestOperationCanceler FollowAsync(string screenName, Action<RestOperationCompletedEventArgs<TwitterProfile>> operationCompleted);

        /// <summary>
        /// Asynchronously allows the authenticated user to follow (create a friendship) with another user.
        /// </summary>
        /// <param name="userId">The Twitter ID of the user to unfollow.</param>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides the <see cref="TwitterProfile"/> of the unfollowed user if successful.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        RestOperationCanceler UnfollowAsync(long userId, Action<RestOperationCompletedEventArgs<TwitterProfile>> operationCompleted);

        /// <summary>
        /// Asynchronously allows the authenticated use to unfollow (destroy a friendship) with another user.
        /// </summary>
        /// <param name="screenName">The screen name of the user to unfollow.</param>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides the <see cref="TwitterProfile"/> of the unfollowed user if successful.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        RestOperationCanceler UnfollowAsync(string screenName, Action<RestOperationCompletedEventArgs<TwitterProfile>> operationCompleted);

        /// <summary>
        /// Asynchronously enables mobile device notifications from Twitter for the specified user.
        /// </summary>
        /// <param name="userId">The Twitter ID of the user to receive notifications for.</param>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides the <see cref="TwitterProfile"/> for the user.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        RestOperationCanceler EnableNotificationsAsync(long userId, Action<RestOperationCompletedEventArgs<TwitterProfile>> operationCompleted);

        /// <summary>
        /// Asynchronously enables mobile device notifications from Twitter for the specified user.
        /// </summary>
        /// <param name="screenName">The Twitter screen name of the user to receive notifications for.</param>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides the <see cref="TwitterProfile"/> for the user.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        RestOperationCanceler EnableNotificationsAsync(string screenName, Action<RestOperationCompletedEventArgs<TwitterProfile>> operationCompleted);

        /// <summary>
        /// Asynchronously disable mobile device notifications from Twitter for the specified user.
        /// </summary>
        /// <param name="userId">The Twitter ID of the user to stop notifications for.</param>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides the <see cref="TwitterProfile"/> for the user.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        RestOperationCanceler DisableNotificationsAsync(long userId, Action<RestOperationCompletedEventArgs<TwitterProfile>> operationCompleted);

        /// <summary>
        /// Asynchronously disable mobile device notifications from Twitter for the specified user.
        /// </summary>
        /// <param name="screenName">The Twitter screen name of the user to stop notifications for.</param>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides the <see cref="TwitterProfile"/> for the user.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        RestOperationCanceler DisableNotificationsAsync(string screenName, Action<RestOperationCompletedEventArgs<TwitterProfile>> operationCompleted);

        /// <summary>
        /// Asynchronously checks for a friendship between two users.
        /// </summary>
        /// <param name="screenNameA">The screen name of userA.</param>
        /// <param name="screenNameB">The screen name of userB.</param>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides a value indicating whether a user follows another user.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        RestOperationCanceler FriendshipExistsAsync(string screenNameA, string screenNameB, Action<RestOperationCompletedEventArgs<bool>> operationCompleted);

        /// <summary>
        /// Asynchronously returns an array of numeric IDs for every user who has a pending request to follow the authenticating user.
        /// </summary>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides a cursored list of user ids.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        RestOperationCanceler GetIncomingFriendshipsAsync(Action<RestOperationCompletedEventArgs<CursoredList<long>>> operationCompleted);

        /// <summary>
        /// Asynchronously returns an array of numeric IDs for every user who has a pending request to follow the authenticating user.
        /// </summary>
        /// <param name="cursor">The cursor of the page to retrieve.</param>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides a cursored list of user ids.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        RestOperationCanceler GetIncomingFriendshipsAsync(long cursor, Action<RestOperationCompletedEventArgs<CursoredList<long>>> operationCompleted);

        /// <summary>
        /// Asynchronously returns an array of numeric IDs for every protected user for whom the authenticating user has a pending follow request.
        /// </summary>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides a cursored list of user ids.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        RestOperationCanceler GetOutgoingFriendshipsAsync(Action<RestOperationCompletedEventArgs<CursoredList<long>>> operationCompleted);

        /// <summary>
        /// Asynchronously returns an array of numeric IDs for every protected user for whom the authenticating user has a pending follow request.
        /// </summary>
        /// <param name="cursor">The cursor of the page to retrieve.</param>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides a cursored list of user ids.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        RestOperationCanceler GetOutgoingFriendshipsAsync(long cursor, Action<RestOperationCompletedEventArgs<CursoredList<long>>> operationCompleted);
#endif
    }
}
