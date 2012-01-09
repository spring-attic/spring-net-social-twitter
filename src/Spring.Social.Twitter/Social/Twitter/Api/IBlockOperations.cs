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
    /// Interface defining the operations for blocking and unblocking users
    /// </summary>
    /// <author>Craig Walls</author>
    /// <author>Bruno Baia (.NET)</author>
    public interface IBlockOperations
    {
#if NET_4_0 || SILVERLIGHT_5
        /// <summary>
        /// Asynchronously blocks a user. If a friendship exists with the user, it will be destroyed.
        /// </summary>
        /// <param name="userId">The ID of the user to block.</param>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// the <see cref="TwitterProfile"/> of the blocked user.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        Task<TwitterProfile> BlockAsync(long userId);

        /// <summary>
        /// Asynchronously blocks a user. If a friendship exists with the user, it will be destroyed.
        /// </summary>
        /// <param name="screenName">The screen name of the user to block.</param>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// the <see cref="TwitterProfile"/> of the blocked user.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        Task<TwitterProfile> BlockAsync(string screenName);

        /// <summary>
        /// Asynchronously unblocks a user.
        /// </summary>
        /// <param name="userId">The ID of the user to unblock.</param>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// the <see cref="TwitterProfile"/> of the unblocked user.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        Task<TwitterProfile> UnblockAsync(long userId);

        /// <summary>
        /// Asynchronously unblocks a user.
        /// </summary>
        /// <param name="screenName">The screen name of the user to unblock.</param>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// the <see cref="TwitterProfile"/> of the unblocked user.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        Task<TwitterProfile> UnblockAsync(string screenName);

        /// <summary>
        /// Asynchronously retrieves a list of users that the authenticating user has blocked.
        /// </summary>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// a list of <see cref="TwitterProfile"/>s for the users that are blocked.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        Task<IList<TwitterProfile>> GetBlockedUsersAsync();

        /// <summary>
        /// Asynchronously retrieves a list of users that the authenticating user has blocked.
        /// </summary>
        /// <param name="page">The page of blocked users to return.</param>
        /// <param name="pageSize">The number of users per page.</param>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// a list of <see cref="TwitterProfile"/>s for the users that are blocked.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        Task<IList<TwitterProfile>> GetBlockedUsersAsync(int page, int pageSize);

        /// <summary>
        /// Asynchronously retrieves a list of user IDs for the users that the authenticating user has blocked.
        /// </summary>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// a list of user IDs for the users that are blocked.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        Task<IList<long>> GetBlockedUserIdsAsync();

        /// <summary>
        /// Asynchronously determines if the user has blocked a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user to check for a block.</param>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// a value indicating whether or not the user is blocked.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        Task<bool> IsBlockingAsync(long userId);

        /// <summary>
        /// Asynchronously determines if the user has blocked a specific user.
        /// </summary>
        /// <param name="screenName">The screen name of the user to check for a block.</param>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// a value indicating whether or not the user is blocked.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        Task<bool> IsBlockingAsync(string screenName);
#else
#if !SILVERLIGHT
        /// <summary>
        /// Blocks a user. If a friendship exists with the user, it will be destroyed.
        /// </summary>
        /// <param name="userId">The ID of the user to block.</param>
        /// <returns>
        /// The <see cref="TwitterProfile"/> of the blocked user.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        TwitterProfile Block(long userId);

        /// <summary>
        /// Blocks a user. If a friendship exists with the user, it will be destroyed.
        /// </summary>
        /// <param name="screenName">The screen name of the user to block.</param>
        /// <returns>
        /// The <see cref="TwitterProfile"/> of the blocked user.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        TwitterProfile Block(string screenName);

        /// <summary>
        /// Unblocks a user.
        /// </summary>
        /// <param name="userId">The ID of the user to unblock.</param>
        /// <returns>
        /// The <see cref="TwitterProfile"/> of the unblocked user.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        TwitterProfile Unblock(long userId);

        /// <summary>
        /// Unblocks a user.
        /// </summary>
        /// <param name="screenName">The screen name of the user to unblock.</param>
        /// <returns>
        /// The <see cref="TwitterProfile"/> of the unblocked user.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        TwitterProfile Unblock(string screenName);

        /// <summary>
        /// Retrieves a list of users that the authenticating user has blocked.
        /// </summary>
        /// <returns>
        /// A list of <see cref="TwitterProfile"/>s for the users that are blocked.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        IList<TwitterProfile> GetBlockedUsers();

        /// <summary>
        /// Retrieves a list of users that the authenticating user has blocked.
        /// </summary>
        /// <param name="page">The page of blocked users to return.</param>
        /// <param name="pageSize">The number of users per page.</param>
        /// <returns>
        /// A list of <see cref="TwitterProfile"/>s for the users that are blocked.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        IList<TwitterProfile> GetBlockedUsers(int page, int pageSize);

        /// <summary>
        /// Retrieves a list of user IDs for the users that the authenticating user has blocked.
        /// </summary>
        /// <returns>A list of user IDs for the users that are blocked.</returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        IList<long> GetBlockedUserIds();

        /// <summary>
        /// Determines if the user has blocked a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user to check for a block.</param>
        /// <returns>
        /// <see langword="true"/> if the user is blocked; <see langword="false"/> otherwise.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        bool IsBlocking(long userId);

        /// <summary>
        /// Determines if the user has blocked a specific user.
        /// </summary>
        /// <param name="screenName">The screen name of the user to check for a block.</param>
        /// <returns>
        /// <see langword="true"/> if the user is blocked; <see langword="false"/> otherwise.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        bool IsBlocking(string screenName);
#endif

        /// <summary>
        /// Asynchronously blocks a user. If a friendship exists with the user, it will be destroyed.
        /// </summary>
        /// <param name="userId">The ID of the user to block.</param>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides the <see cref="TwitterProfile"/> of the blocked user.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        RestOperationCanceler BlockAsync(long userId, Action<RestOperationCompletedEventArgs<TwitterProfile>> operationCompleted);

        /// <summary>
        /// Asynchronously blocks a user. If a friendship exists with the user, it will be destroyed.
        /// </summary>
        /// <param name="screenName">The screen name of the user to block.</param>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides the <see cref="TwitterProfile"/> of the blocked user.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        RestOperationCanceler BlockAsync(string screenName, Action<RestOperationCompletedEventArgs<TwitterProfile>> operationCompleted);

        /// <summary>
        /// Asynchronously unblocks a user.
        /// </summary>
        /// <param name="userId">The ID of the user to unblock.</param>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides the <see cref="TwitterProfile"/> of the unblocked user.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        RestOperationCanceler UnblockAsync(long userId, Action<RestOperationCompletedEventArgs<TwitterProfile>> operationCompleted);

        /// <summary>
        /// Asynchronously unblocks a user.
        /// </summary>
        /// <param name="screenName">The screen name of the user to unblock.</param>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides the <see cref="TwitterProfile"/> of the unblocked user.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        RestOperationCanceler UnblockAsync(string screenName, Action<RestOperationCompletedEventArgs<TwitterProfile>> operationCompleted);

        /// <summary>
        /// Asynchronously retrieves a list of users that the authenticating user has blocked.
        /// </summary>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides a list of <see cref="TwitterProfile"/>s for the users that are blocked.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        RestOperationCanceler GetBlockedUsersAsync(Action<RestOperationCompletedEventArgs<IList<TwitterProfile>>> operationCompleted);

        /// <summary>
        /// Asynchronously retrieves a list of users that the authenticating user has blocked.
        /// </summary>
        /// <param name="page">The page of blocked users to return.</param>
        /// <param name="pageSize">The number of users per page.</param>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides a list of <see cref="TwitterProfile"/>s for the users that are blocked.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        RestOperationCanceler GetBlockedUsersAsync(int page, int pageSize, Action<RestOperationCompletedEventArgs<IList<TwitterProfile>>> operationCompleted);

        /// <summary>
        /// Asynchronously retrieves a list of user IDs for the users that the authenticating user has blocked.
        /// </summary>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides a list of user IDs for the users that are blocked.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        /// <exception cref="NotAuthorizedException">If OAuth credentials was not provided.</exception>
        RestOperationCanceler GetBlockedUserIdsAsync(Action<RestOperationCompletedEventArgs<IList<long>>> operationCompleted);
        /// <summary>
        /// Asynchronously determines if the user has blocked a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user to check for a block.</param>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides a value indicating whether or not the user is blocked.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        RestOperationCanceler IsBlockingAsync(long userId, Action<RestOperationCompletedEventArgs<bool>> operationCompleted);

        /// <summary>
        /// Asynchronously determines if the user has blocked a specific user.
        /// </summary>
        /// <param name="screenName">The screen name of the user to check for a block.</param>
        /// <param name="operationCompleted">
        /// The <code>Action&lt;&gt;</code> to perform when the asynchronous request completes. 
        /// Provides a value indicating whether or not the user is blocked.
        /// </param>
        /// <returns>
        /// A <see cref="RestOperationCanceler"/> instance that allows to cancel the asynchronous operation.
        /// </returns>
        /// <exception cref="ApiException">If there is an error while communicating with Twitter.</exception>
        RestOperationCanceler IsBlockingAsync(string screenName, Action<RestOperationCompletedEventArgs<bool>> operationCompleted);
#endif
    }
}
