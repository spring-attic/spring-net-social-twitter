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
using System.Net;
using System.Collections.Generic;
#if SILVERLIGHT
using Spring.Collections.Specialized;
#else
using System.Collections.Specialized;
#endif
#if NET_4_0 || SILVERLIGHT_5
using System.Threading;
using System.Threading.Tasks;
#endif

using Spring.Http;
using Spring.Rest.Client;

namespace Spring.Social.Twitter.Api.Impl
{
    /// <summary>
    /// Implementation of <see cref="IListOperations"/>, providing a binding to Twitter's list-oriented REST resources.
    /// </summary>
    /// <author>Craig Walls</author>
    /// <author>Bruno Baia (.NET)</author>
    class ListTemplate : AbstractTwitterOperations, IListOperations
    {
        private RestTemplate restTemplate;

        public ListTemplate(RestTemplate restTemplate, bool isAuthorized)
            : base(isAuthorized)
        {
            this.restTemplate = restTemplate;
        }

        #region IListOperations Members

#if NET_4_0 || SILVERLIGHT_5
        public Task<CursoredList<UserList>> GetListsAsync() 
        {
            return this.GetListsInCursorAsync(-1);
	    }

        public Task<CursoredList<UserList>> GetListsInCursorAsync(long cursor) 
        {
		    this.EnsureIsAuthorized();
            return this.restTemplate.GetForObjectAsync<CursoredList<UserList>>(this.BuildUrl("lists.json", "cursor", cursor.ToString()));
	    }

        public Task<CursoredList<UserList>> GetListsAsync(long userId) 
        {
            return this.GetListsInCursorAsync(userId, -1);
	    }

        public Task<CursoredList<UserList>> GetListsInCursorAsync(long userId, long cursor) 
        {
		    NameValueCollection parameters = new NameValueCollection();
		    parameters.Add("user_id", userId.ToString());
		    parameters.Add("cursor", cursor.ToString());
            return this.restTemplate.GetForObjectAsync<CursoredList<UserList>>(this.BuildUrl("lists.json", parameters));
	    }

        public Task<CursoredList<UserList>> GetListsAsync(string screenName) 
        {
            return this.GetListsInCursorAsync(screenName, -1);
	    }

        public Task<CursoredList<UserList>> GetListsInCursorAsync(string screenName, long cursor) 
        {
		    NameValueCollection parameters = new NameValueCollection();
		    parameters.Add("screen_name", screenName);
		    parameters.Add("cursor", cursor.ToString());
            return this.restTemplate.GetForObjectAsync<CursoredList<UserList>>(this.BuildUrl("lists.json", parameters));
	    }

        public Task<UserList> GetListAsync(long listId) 
        {
            return this.restTemplate.GetForObjectAsync<UserList>(this.BuildUrl("lists/show.json", "list_id", listId.ToString()));
	    }

        public Task<UserList> GetListAsync(string screenName, string listSlug) 
        {
		    NameValueCollection parameters = new NameValueCollection();
		    parameters.Add("owner_screen_name", screenName);
		    parameters.Add("slug", listSlug);
            return this.restTemplate.GetForObjectAsync<UserList>(this.BuildUrl("lists/show.json", parameters));
	    }

        public Task<IList<Tweet>> GetListStatusesAsync(long listId) 
        {
            return this.GetListStatusesAsync(listId, 1, 20, 0, 0);
	    }

        public Task<IList<Tweet>> GetListStatusesAsync(long listId, int page, int pageSize) 
        {
            return this.GetListStatusesAsync(listId, page, pageSize, 0, 0);
	    }

        public Task<IList<Tweet>> GetListStatusesAsync(long listId, int page, int pageSize, long sinceId, long maxId) 
        {
		    NameValueCollection parameters = PagingUtils.BuildPagingParametersWithPerPage(page, pageSize, sinceId, maxId);
		    parameters.Add("list_id", listId.ToString());
            return this.restTemplate.GetForObjectAsync<IList<Tweet>>(this.BuildUrl("lists/statuses.json", parameters));
	    }

        public Task<IList<Tweet>> GetListStatusesAsync(string screenName, string listSlug) 
        {
            return this.GetListStatusesAsync(screenName, listSlug, 1, 20, 0, 0);
	    }

        public Task<IList<Tweet>> GetListStatusesAsync(string screenName, string listSlug, int page, int pageSize) 
        {
            return this.GetListStatusesAsync(screenName, listSlug, page, pageSize, 0, 0);
	    }

        public Task<IList<Tweet>> GetListStatusesAsync(string screenName, string listSlug, int page, int pageSize, long sinceId, long maxId) 
        {
		    NameValueCollection parameters = PagingUtils.BuildPagingParametersWithPerPage(page, pageSize, sinceId, maxId);
		    parameters.Add("owner_screen_name", screenName);
		    parameters.Add("slug", listSlug);
            return this.restTemplate.GetForObjectAsync<IList<Tweet>>(this.BuildUrl("lists/statuses.json", parameters));
	    }

        public Task<UserList> CreateListAsync(string name, string description, bool isPublic) 
        {	
		    this.EnsureIsAuthorized();
		    NameValueCollection request = BuildListParameters(name, description, isPublic);
            return this.restTemplate.PostForObjectAsync<UserList>("lists/create.json", request);
	    }

        public Task<UserList> UpdateListAsync(long listId, string name, string description, bool isPublic) 
        {
		    this.EnsureIsAuthorized();
		    NameValueCollection request = BuildListParameters(name, description, isPublic);
		    request.Add("list_id", listId.ToString());
            return this.restTemplate.PostForObjectAsync<UserList>("lists/update.json", request);
	    }

        public Task DeleteListAsync(long listId) 
        {
		    this.EnsureIsAuthorized();
            return this.restTemplate.DeleteAsync(this.BuildUrl("lists/destroy.json", "list_id", listId.ToString()));
	    }

        public Task<IList<TwitterProfile>> GetListMembersAsync(long listId) 
        {
            return this.restTemplate.GetForObjectAsync<IList<TwitterProfile>>(this.BuildUrl("lists/members.json", "list_id", listId.ToString()));
	    }

        public Task<IList<TwitterProfile>> GetListMembersAsync(string screenName, string listSlug) 
        {
		    NameValueCollection parameters = new NameValueCollection();
		    parameters.Add("owner_screen_name", screenName);
		    parameters.Add("slug", listSlug);
            return this.restTemplate.GetForObjectAsync<IList<TwitterProfile>>(this.BuildUrl("lists/members.json", parameters));
	    }

        public Task<UserList> AddToListAsync(long listId, params long[] newMemberIds) 
        {
		    this.EnsureIsAuthorized();
		    NameValueCollection request = new NameValueCollection();
		    request.Add("user_id", ArrayUtils.Join(newMemberIds));
		    request.Add("list_id", listId.ToString());
            return this.restTemplate.PostForObjectAsync<UserList>("lists/members/create_all.json", request);
	    }

        public Task<UserList> AddToListAsync(long listId, params string[] newMemberScreenNames) 
        {
		    this.EnsureIsAuthorized();
		    NameValueCollection request = new NameValueCollection();
		    request.Add("screen_name", ArrayUtils.Join(newMemberScreenNames));
		    request.Add("list_id", listId.ToString());
            return this.restTemplate.PostForObjectAsync<UserList>("lists/members/create_all.json", request);
	    }

        public Task RemoveFromListAsync(long listId, long memberId) 
        {
		    this.EnsureIsAuthorized();
		    NameValueCollection request = new NameValueCollection();
		    request.Add("user_id", memberId.ToString()); 
		    request.Add("list_id", listId.ToString());
            return this.restTemplate.PostForMessageAsync("lists/members/destroy.json", request);
	    }

        public Task RemoveFromListAsync(long listId, string memberScreenName) 
        {
		    this.EnsureIsAuthorized();
		    NameValueCollection request = new NameValueCollection();
		    request.Add("screen_name", memberScreenName); 
		    request.Add("list_id", listId.ToString());
            return this.restTemplate.PostForMessageAsync("lists/members/destroy.json", request);
	    }

        public Task<IList<TwitterProfile>> GetListSubscribersAsync(long listId) 
        {
            return this.restTemplate.GetForObjectAsync<IList<TwitterProfile>>(this.BuildUrl("lists/subscribers.json", "list_id", listId.ToString()));
	    }

        public Task<IList<TwitterProfile>> GetListSubscribersAsync(string screenName, string listSlug) 
        {
		    NameValueCollection parameters = new NameValueCollection();
		    parameters.Add("owner_screen_name", screenName);
		    parameters.Add("slug", listSlug);
            return this.restTemplate.GetForObjectAsync<IList<TwitterProfile>>(this.BuildUrl("lists/subscribers.json", parameters));
	    }


        public Task<UserList> SubscribeAsync(long listId) 
        {
		    this.EnsureIsAuthorized();
		    NameValueCollection request = new NameValueCollection();
		    request.Add("list_id", listId.ToString());
            return this.restTemplate.PostForObjectAsync<UserList>("lists/subscribers/create.json", request);
	    }

        public Task<UserList> SubscribeAsync(string ownerScreenName, string listSlug) 
        {
		    this.EnsureIsAuthorized();
		    NameValueCollection request = new NameValueCollection();
		    request.Add("owner_screen_name", ownerScreenName);
		    request.Add("slug", listSlug);
            return this.restTemplate.PostForObjectAsync<UserList>("lists/subscribers/create.json", request);
	    }

        public Task<UserList> UnsubscribeAsync(long listId) 
        {
		    this.EnsureIsAuthorized();
		    NameValueCollection request = new NameValueCollection();
		    request.Add("list_id", listId.ToString());
            return this.restTemplate.PostForObjectAsync<UserList>("lists/subscribers/destroy.json", request);
	    }

        public Task<UserList> UnsubscribeAsync(string ownerScreenName, string listSlug) 
        {
		    this.EnsureIsAuthorized();
		    NameValueCollection request = new NameValueCollection();
		    request.Add("owner_screen_name", ownerScreenName);
		    request.Add("slug", listSlug);
            return this.restTemplate.PostForObjectAsync<UserList>("lists/subscribers/destroy.json", request);
	    }

        public Task<CursoredList<UserList>> GetMembershipsAsync(long userId) 
        {
            return this.restTemplate.GetForObjectAsync<CursoredList<UserList>>(this.BuildUrl("lists/memberships.json", "user_id", userId.ToString()));
	    }

        public Task<CursoredList<UserList>> GetMembershipsAsync(string screenName) 
        {
            return this.restTemplate.GetForObjectAsync<CursoredList<UserList>>(this.BuildUrl("lists/memberships.json", "screen_name", screenName));
	    }

        public Task<CursoredList<UserList>> GetSubscriptionsAsync(long userId) 
        {
            return this.restTemplate.GetForObjectAsync<CursoredList<UserList>>(this.BuildUrl("lists/subscriptions.json", "user_id", userId.ToString()));
	    }

        public Task<CursoredList<UserList>> GetSubscriptionsAsync(string screenName) 
        {
            return this.restTemplate.GetForObjectAsync<CursoredList<UserList>>(this.BuildUrl("lists/subscriptions.json", "screen_name", screenName));
	    }

        public Task<bool> IsMemberAsync(long listId, long memberId) 
        {
		    this.EnsureIsAuthorized();
		    NameValueCollection parameters = new NameValueCollection();
		    parameters.Add("list_id", listId.ToString());
		    parameters.Add("user_id", memberId.ToString());
            return this.CheckListConnectionAsync(this.BuildUrl("lists/members/show.json", parameters));
	    }

        public Task<bool> IsMemberAsync(string screenName, string listSlug, string memberScreenName) 
        {
		    this.EnsureIsAuthorized();
		    NameValueCollection parameters = new NameValueCollection();
		    parameters.Add("owner_screen_name", screenName);
		    parameters.Add("slug", listSlug);
		    parameters.Add("screen_name", memberScreenName);
            return this.CheckListConnectionAsync(this.BuildUrl("lists/members/show.json", parameters));
	    }

        public Task<bool> IsSubscriberAsync(long listId, long subscriberId) 
        {
		    NameValueCollection parameters = new NameValueCollection();
		    parameters.Add("list_id", listId.ToString());
		    parameters.Add("user_id", subscriberId.ToString());
            return this.CheckListConnectionAsync(this.BuildUrl("lists/subscribers/show.json", parameters));
	    }

        public Task<bool> IsSubscriberAsync(string screenName, string listSlug, string subscriberScreenName) 
        {
		    this.EnsureIsAuthorized();
		    NameValueCollection parameters = new NameValueCollection();
            parameters.Add("owner_screen_name", screenName);
            parameters.Add("slug", listSlug);
            parameters.Add("screen_name", subscriberScreenName);
		    return this.CheckListConnectionAsync(this.BuildUrl("lists/subscribers/show.json", parameters));
	    }
#else
#if !SILVERLIGHT
        public CursoredList<UserList> GetLists() 
        {
		    return this.GetListsInCursor(-1);
	    }

	    public CursoredList<UserList> GetListsInCursor(long cursor) 
        {
		    this.EnsureIsAuthorized();
		    return this.restTemplate.GetForObject<CursoredList<UserList>>(this.BuildUrl("lists.json", "cursor", cursor.ToString()));
	    }

	    public CursoredList<UserList> GetLists(long userId) 
        {
		    return this.GetListsInCursor(userId, -1);
	    }
	
	    public CursoredList<UserList> GetListsInCursor(long userId, long cursor) 
        {
		    NameValueCollection parameters = new NameValueCollection();
		    parameters.Add("user_id", userId.ToString());
		    parameters.Add("cursor", cursor.ToString());
		    return this.restTemplate.GetForObject<CursoredList<UserList>>(this.BuildUrl("lists.json", parameters));
	    }

	    public CursoredList<UserList> GetLists(string screenName) 
        {
		    return this.GetListsInCursor(screenName, -1);
	    }
	
	    public CursoredList<UserList> GetListsInCursor(string screenName, long cursor) 
        {
		    NameValueCollection parameters = new NameValueCollection();
		    parameters.Add("screen_name", screenName);
		    parameters.Add("cursor", cursor.ToString());
		    return this.restTemplate.GetForObject<CursoredList<UserList>>(this.BuildUrl("lists.json", parameters));
	    }

	    public UserList GetList(long listId) 
        {
		    return this.restTemplate.GetForObject<UserList>(this.BuildUrl("lists/show.json", "list_id", listId.ToString()));
	    }

	    public UserList GetList(string screenName, string listSlug) 
        {
		    NameValueCollection parameters = new NameValueCollection();
		    parameters.Add("owner_screen_name", screenName);
		    parameters.Add("slug", listSlug);
		    return this.restTemplate.GetForObject<UserList>(this.BuildUrl("lists/show.json", parameters));
	    }

	    public IList<Tweet> GetListStatuses(long listId) 
        {
		    return this.GetListStatuses(listId, 1, 20, 0, 0);
	    }

	    public IList<Tweet> GetListStatuses(long listId, int page, int pageSize) 
        {
		    return this.GetListStatuses(listId, page, pageSize, 0, 0);
	    }

	    public IList<Tweet> GetListStatuses(long listId, int page, int pageSize, long sinceId, long maxId) 
        {
		    NameValueCollection parameters = PagingUtils.BuildPagingParametersWithPerPage(page, pageSize, sinceId, maxId);
		    parameters.Add("list_id", listId.ToString());
		    return this.restTemplate.GetForObject<IList<Tweet>>(this.BuildUrl("lists/statuses.json", parameters));
	    }

	    public IList<Tweet> GetListStatuses(string screenName, string listSlug) 
        {
		    return this.GetListStatuses(screenName, listSlug, 1, 20, 0, 0);
	    }

	    public IList<Tweet> GetListStatuses(string screenName, string listSlug, int page, int pageSize) 
        {
		    return this.GetListStatuses(screenName, listSlug, page, pageSize, 0, 0);
	    }

	    public IList<Tweet> GetListStatuses(string screenName, string listSlug, int page, int pageSize, long sinceId, long maxId) 
        {
		    NameValueCollection parameters = PagingUtils.BuildPagingParametersWithPerPage(page, pageSize, sinceId, maxId);
		    parameters.Add("owner_screen_name", screenName);
		    parameters.Add("slug", listSlug);
		    return this.restTemplate.GetForObject<IList<Tweet>>(this.BuildUrl("lists/statuses.json", parameters));
	    }

	    public UserList CreateList(string name, string description, bool isPublic) 
        {	
		    this.EnsureIsAuthorized();
		    NameValueCollection request = BuildListParameters(name, description, isPublic);
		    return this.restTemplate.PostForObject<UserList>("lists/create.json", request);
	    }

	    public UserList UpdateList(long listId, string name, string description, bool isPublic) 
        {
		    this.EnsureIsAuthorized();
		    NameValueCollection request = BuildListParameters(name, description, isPublic);
		    request.Add("list_id", listId.ToString());
		    return this.restTemplate.PostForObject<UserList>("lists/update.json", request);
	    }

	    public void DeleteList(long listId) 
        {
		    this.EnsureIsAuthorized();
		    this.restTemplate.Delete(this.BuildUrl("lists/destroy.json", "list_id", listId.ToString()));
	    }

	    public IList<TwitterProfile> GetListMembers(long listId) 
        {
		    return this.restTemplate.GetForObject<IList<TwitterProfile>>(this.BuildUrl("lists/members.json", "list_id", listId.ToString()));
	    }
	
	    public IList<TwitterProfile> GetListMembers(string screenName, string listSlug) 
        {
		    NameValueCollection parameters = new NameValueCollection();
		    parameters.Add("owner_screen_name", screenName);
            parameters.Add("slug", listSlug);
		    return this.restTemplate.GetForObject<IList<TwitterProfile>>(this.BuildUrl("lists/members.json", parameters));
	    }

	    public UserList AddToList(long listId, params long[] newMemberIds) 
        {
		    this.EnsureIsAuthorized();
		    NameValueCollection request = new NameValueCollection();
		    request.Add("user_id", ArrayUtils.Join(newMemberIds));
		    request.Add("list_id", listId.ToString());
		    return this.restTemplate.PostForObject<UserList>("lists/members/create_all.json", request);
	    }

	    public UserList AddToList(long listId, params string[] newMemberScreenNames) 
        {
		    this.EnsureIsAuthorized();
		    NameValueCollection request = new NameValueCollection();
		    request.Add("screen_name", ArrayUtils.Join(newMemberScreenNames));
		    request.Add("list_id", listId.ToString());
		    return this.restTemplate.PostForObject<UserList>("lists/members/create_all.json", request);
	    }

	    public void RemoveFromList(long listId, long memberId) 
        {
		    this.EnsureIsAuthorized();
		    NameValueCollection request = new NameValueCollection();
		    request.Add("user_id", memberId.ToString()); 
		    request.Add("list_id", listId.ToString());
		    this.restTemplate.PostForMessage("lists/members/destroy.json", request);
	    }

	    public void RemoveFromList(long listId, string memberScreenName) 
        {
		    this.EnsureIsAuthorized();
		    NameValueCollection request = new NameValueCollection();
		    request.Add("screen_name", memberScreenName); 
		    request.Add("list_id", listId.ToString());
		    this.restTemplate.PostForMessage("lists/members/destroy.json", request);
	    }

	    public IList<TwitterProfile> GetListSubscribers(long listId) 
        {
		    return this.restTemplate.GetForObject<IList<TwitterProfile>>(this.BuildUrl("lists/subscribers.json", "list_id", listId.ToString()));
	    }

	    public IList<TwitterProfile> GetListSubscribers(string screenName, string listSlug) 
        {
		    NameValueCollection parameters = new NameValueCollection();
		    parameters.Add("owner_screen_name", screenName);
		    parameters.Add("slug", listSlug);
		    return this.restTemplate.GetForObject<IList<TwitterProfile>>(this.BuildUrl("lists/subscribers.json", parameters));
	    }

	
	    public UserList Subscribe(long listId) 
        {
		    this.EnsureIsAuthorized();
		    NameValueCollection request = new NameValueCollection();
		    request.Add("list_id", listId.ToString());
		    return this.restTemplate.PostForObject<UserList>("lists/subscribers/create.json", request);
	    }

	    public UserList Subscribe(string ownerScreenName, string listSlug) 
        {
		    this.EnsureIsAuthorized();
		    NameValueCollection request = new NameValueCollection();
		    request.Add("owner_screen_name", ownerScreenName);
		    request.Add("slug", listSlug);
		    return this.restTemplate.PostForObject<UserList>("lists/subscribers/create.json", request);
	    }

	    public UserList Unsubscribe(long listId) 
        {
		    this.EnsureIsAuthorized();
		    NameValueCollection request = new NameValueCollection();
		    request.Add("list_id", listId.ToString());
		    return this.restTemplate.PostForObject<UserList>("lists/subscribers/destroy.json", request);
	    }

	    public UserList Unsubscribe(string ownerScreenName, string listSlug) 
        {
		    this.EnsureIsAuthorized();
		    NameValueCollection request = new NameValueCollection();
		    request.Add("owner_screen_name", ownerScreenName);
		    request.Add("slug", listSlug);
		    return this.restTemplate.PostForObject<UserList>("lists/subscribers/destroy.json", request);
	    }

	    public CursoredList<UserList> GetMemberships(long userId) 
        {
		    return this.restTemplate.GetForObject<CursoredList<UserList>>(this.BuildUrl("lists/memberships.json", "user_id", userId.ToString()));
	    }

	    public CursoredList<UserList> GetMemberships(string screenName) 
        {
		    return this.restTemplate.GetForObject<CursoredList<UserList>>(this.BuildUrl("lists/memberships.json", "screen_name", screenName));
	    }

	    public CursoredList<UserList> GetSubscriptions(long userId) 
        {
		    return this.restTemplate.GetForObject<CursoredList<UserList>>(this.BuildUrl("lists/subscriptions.json", "user_id", userId.ToString()));
	    }

	    public CursoredList<UserList> GetSubscriptions(string screenName) 
        {
		    return this.restTemplate.GetForObject<CursoredList<UserList>>(this.BuildUrl("lists/subscriptions.json", "screen_name", screenName));
	    }

	    public bool IsMember(long listId, long memberId) 
        {
		    this.EnsureIsAuthorized();
		    NameValueCollection parameters = new NameValueCollection();
		    parameters.Add("list_id", listId.ToString());
		    parameters.Add("user_id", memberId.ToString());
		    return this.CheckListConnection(this.BuildUrl("lists/members/show.json", parameters));
	    }

	    public bool IsMember(string screenName, string listSlug, string memberScreenName) 
        {
		    this.EnsureIsAuthorized();
		    NameValueCollection parameters = new NameValueCollection();
		    parameters.Add("owner_screen_name", screenName);
		    parameters.Add("slug", listSlug);
		    parameters.Add("screen_name", memberScreenName);
		    return this.CheckListConnection(this.BuildUrl("lists/members/show.json", parameters));
	    }

	    public bool IsSubscriber(long listId, long subscriberId) 
        {
		    NameValueCollection parameters = new NameValueCollection();
		    parameters.Add("list_id", listId.ToString());
		    parameters.Add("user_id", subscriberId.ToString());
		    return this.CheckListConnection(this.BuildUrl("lists/subscribers/show.json", parameters));
	    }

	    public bool IsSubscriber(string screenName, string listSlug, string subscriberScreenName) 
        {
		    this.EnsureIsAuthorized();
		    NameValueCollection parameters = new NameValueCollection();
            parameters.Add("owner_screen_name", screenName);
            parameters.Add("slug", listSlug);
            parameters.Add("screen_name", subscriberScreenName);
		    return this.CheckListConnection(this.BuildUrl("lists/subscribers/show.json", parameters));
	    }
#endif

        public RestOperationCanceler GetListsAsync(Action<RestOperationCompletedEventArgs<CursoredList<UserList>>> operationCompleted)
        {
            return this.GetListsInCursorAsync(-1, operationCompleted);
        }

        public RestOperationCanceler GetListsInCursorAsync(long cursor, Action<RestOperationCompletedEventArgs<CursoredList<UserList>>> operationCompleted)
        {
            this.EnsureIsAuthorized();
            return this.restTemplate.GetForObjectAsync<CursoredList<UserList>>(this.BuildUrl("lists.json", "cursor", cursor.ToString()), operationCompleted);
        }

        public RestOperationCanceler GetListsAsync(long userId, Action<RestOperationCompletedEventArgs<CursoredList<UserList>>> operationCompleted)
        {
            return this.GetListsInCursorAsync(userId, -1, operationCompleted);
        }

        public RestOperationCanceler GetListsInCursorAsync(long userId, long cursor, Action<RestOperationCompletedEventArgs<CursoredList<UserList>>> operationCompleted)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("user_id", userId.ToString());
            parameters.Add("cursor", cursor.ToString());
            return this.restTemplate.GetForObjectAsync<CursoredList<UserList>>(this.BuildUrl("lists.json", parameters), operationCompleted);
        }

        public RestOperationCanceler GetListsAsync(string screenName, Action<RestOperationCompletedEventArgs<CursoredList<UserList>>> operationCompleted)
        {
            return this.GetListsInCursorAsync(screenName, -1, operationCompleted);
        }

        public RestOperationCanceler GetListsInCursorAsync(string screenName, long cursor, Action<RestOperationCompletedEventArgs<CursoredList<UserList>>> operationCompleted)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("screen_name", screenName);
            parameters.Add("cursor", cursor.ToString());
            return this.restTemplate.GetForObjectAsync<CursoredList<UserList>>(this.BuildUrl("lists.json", parameters), operationCompleted);
        }

        public RestOperationCanceler GetListAsync(long listId, Action<RestOperationCompletedEventArgs<UserList>> operationCompleted)
        {
            return this.restTemplate.GetForObjectAsync<UserList>(this.BuildUrl("lists/show.json", "list_id", listId.ToString()), operationCompleted);
        }

        public RestOperationCanceler GetListAsync(string screenName, string listSlug, Action<RestOperationCompletedEventArgs<UserList>> operationCompleted)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("owner_screen_name", screenName);
            parameters.Add("slug", listSlug);
            return this.restTemplate.GetForObjectAsync<UserList>(this.BuildUrl("lists/show.json", parameters), operationCompleted);
        }

        public RestOperationCanceler GetListStatusesAsync(long listId, Action<RestOperationCompletedEventArgs<IList<Tweet>>> operationCompleted)
        {
            return this.GetListStatusesAsync(listId, 1, 20, 0, 0, operationCompleted);
        }

        public RestOperationCanceler GetListStatusesAsync(long listId, int page, int pageSize, Action<RestOperationCompletedEventArgs<IList<Tweet>>> operationCompleted)
        {
            return this.GetListStatusesAsync(listId, page, pageSize, 0, 0, operationCompleted);
        }

        public RestOperationCanceler GetListStatusesAsync(long listId, int page, int pageSize, long sinceId, long maxId, Action<RestOperationCompletedEventArgs<IList<Tweet>>> operationCompleted)
        {
            NameValueCollection parameters = PagingUtils.BuildPagingParametersWithPerPage(page, pageSize, sinceId, maxId);
            parameters.Add("list_id", listId.ToString());
            return this.restTemplate.GetForObjectAsync<IList<Tweet>>(this.BuildUrl("lists/statuses.json", parameters), operationCompleted);
        }

        public RestOperationCanceler GetListStatusesAsync(string screenName, string listSlug, Action<RestOperationCompletedEventArgs<IList<Tweet>>> operationCompleted)
        {
            return this.GetListStatusesAsync(screenName, listSlug, 1, 20, 0, 0, operationCompleted);
        }

        public RestOperationCanceler GetListStatusesAsync(string screenName, string listSlug, int page, int pageSize, Action<RestOperationCompletedEventArgs<IList<Tweet>>> operationCompleted)
        {
            return this.GetListStatusesAsync(screenName, listSlug, page, pageSize, 0, 0, operationCompleted);
        }

        public RestOperationCanceler GetListStatusesAsync(string screenName, string listSlug, int page, int pageSize, long sinceId, long maxId, Action<RestOperationCompletedEventArgs<IList<Tweet>>> operationCompleted)
        {
            NameValueCollection parameters = PagingUtils.BuildPagingParametersWithPerPage(page, pageSize, sinceId, maxId);
            parameters.Add("owner_screen_name", screenName);
            parameters.Add("slug", listSlug);
            return this.restTemplate.GetForObjectAsync<IList<Tweet>>(this.BuildUrl("lists/statuses.json", parameters), operationCompleted);
        }

        public RestOperationCanceler CreateListAsync(string name, string description, bool isPublic, Action<RestOperationCompletedEventArgs<UserList>> operationCompleted)
        {
            this.EnsureIsAuthorized();
            NameValueCollection request = BuildListParameters(name, description, isPublic);
            return this.restTemplate.PostForObjectAsync<UserList>("lists/create.json", request, operationCompleted);
        }

        public RestOperationCanceler UpdateListAsync(long listId, string name, string description, bool isPublic, Action<RestOperationCompletedEventArgs<UserList>> operationCompleted)
        {
            this.EnsureIsAuthorized();
            NameValueCollection request = BuildListParameters(name, description, isPublic);
            request.Add("list_id", listId.ToString());
            return this.restTemplate.PostForObjectAsync<UserList>("lists/update.json", request, operationCompleted);
        }

        public RestOperationCanceler DeleteListAsync(long listId, Action<RestOperationCompletedEventArgs<object>> operationCompleted)
        {
            this.EnsureIsAuthorized();
            return this.restTemplate.DeleteAsync(this.BuildUrl("lists/destroy.json", "list_id", listId.ToString()), operationCompleted);
        }

        public RestOperationCanceler GetListMembersAsync(long listId, Action<RestOperationCompletedEventArgs<IList<TwitterProfile>>> operationCompleted)
        {
            return this.restTemplate.GetForObjectAsync<IList<TwitterProfile>>(this.BuildUrl("lists/members.json", "list_id", listId.ToString()), operationCompleted);
        }

        public RestOperationCanceler GetListMembersAsync(string screenName, string listSlug, Action<RestOperationCompletedEventArgs<IList<TwitterProfile>>> operationCompleted)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("owner_screen_name", screenName);
            parameters.Add("slug", listSlug);
            return this.restTemplate.GetForObjectAsync<IList<TwitterProfile>>(this.BuildUrl("lists/members.json", parameters), operationCompleted);
        }

        public RestOperationCanceler AddToListAsync(long listId, long[] newMemberIds, Action<RestOperationCompletedEventArgs<UserList>> operationCompleted)
        {
            this.EnsureIsAuthorized();
            NameValueCollection request = new NameValueCollection();
            request.Add("user_id", ArrayUtils.Join(newMemberIds));
            request.Add("list_id", listId.ToString());
            return this.restTemplate.PostForObjectAsync<UserList>("lists/members/create_all.json", request, operationCompleted);
        }

        public RestOperationCanceler AddToListAsync(long listId, string[] newMemberScreenNames, Action<RestOperationCompletedEventArgs<UserList>> operationCompleted)
        {
            this.EnsureIsAuthorized();
            NameValueCollection request = new NameValueCollection();
            request.Add("screen_name", ArrayUtils.Join(newMemberScreenNames));
            request.Add("list_id", listId.ToString());
            return this.restTemplate.PostForObjectAsync<UserList>("lists/members/create_all.json", request, operationCompleted);
        }

        public RestOperationCanceler RemoveFromListAsync(long listId, long memberId, Action<RestOperationCompletedEventArgs<HttpResponseMessage>> operationCompleted)
        {
            this.EnsureIsAuthorized();
            NameValueCollection request = new NameValueCollection();
            request.Add("user_id", memberId.ToString());
            request.Add("list_id", listId.ToString());
            return this.restTemplate.PostForMessageAsync("lists/members/destroy.json", request, operationCompleted);
        }

        public RestOperationCanceler RemoveFromListAsync(long listId, string memberScreenName, Action<RestOperationCompletedEventArgs<HttpResponseMessage>> operationCompleted)
        {
            this.EnsureIsAuthorized();
            NameValueCollection request = new NameValueCollection();
            request.Add("screen_name", memberScreenName);
            request.Add("list_id", listId.ToString());
            return this.restTemplate.PostForMessageAsync("lists/members/destroy.json", request, operationCompleted);
        }

        public RestOperationCanceler GetListSubscribersAsync(long listId, Action<RestOperationCompletedEventArgs<IList<TwitterProfile>>> operationCompleted)
        {
            return this.restTemplate.GetForObjectAsync<IList<TwitterProfile>>(this.BuildUrl("lists/subscribers.json", "list_id", listId.ToString()), operationCompleted);
        }

        public RestOperationCanceler GetListSubscribersAsync(string screenName, string listSlug, Action<RestOperationCompletedEventArgs<IList<TwitterProfile>>> operationCompleted)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("owner_screen_name", screenName);
            parameters.Add("slug", listSlug);
            return this.restTemplate.GetForObjectAsync<IList<TwitterProfile>>(this.BuildUrl("lists/subscribers.json", parameters), operationCompleted);
        }

        public RestOperationCanceler SubscribeAsync(long listId, Action<RestOperationCompletedEventArgs<UserList>> operationCompleted)
        {
            this.EnsureIsAuthorized();
            NameValueCollection request = new NameValueCollection();
            request.Add("list_id", listId.ToString());
            return this.restTemplate.PostForObjectAsync<UserList>("lists/subscribers/create.json", request, operationCompleted);
        }

        public RestOperationCanceler SubscribeAsync(string ownerScreenName, string listSlug, Action<RestOperationCompletedEventArgs<UserList>> operationCompleted)
        {
            this.EnsureIsAuthorized();
            NameValueCollection request = new NameValueCollection();
            request.Add("owner_screen_name", ownerScreenName);
            request.Add("slug", listSlug);
            return this.restTemplate.PostForObjectAsync<UserList>("lists/subscribers/create.json", request, operationCompleted);
        }

        public RestOperationCanceler UnsubscribeAsync(long listId, Action<RestOperationCompletedEventArgs<UserList>> operationCompleted)
        {
            this.EnsureIsAuthorized();
            NameValueCollection request = new NameValueCollection();
            request.Add("list_id", listId.ToString());
            return this.restTemplate.PostForObjectAsync<UserList>("lists/subscribers/destroy.json", request, operationCompleted);
        }

        public RestOperationCanceler UnsubscribeAsync(string ownerScreenName, string listSlug, Action<RestOperationCompletedEventArgs<UserList>> operationCompleted)
        {
            this.EnsureIsAuthorized();
            NameValueCollection request = new NameValueCollection();
            request.Add("owner_screen_name", ownerScreenName);
            request.Add("slug", listSlug);
            return this.restTemplate.PostForObjectAsync<UserList>("lists/subscribers/destroy.json", request, operationCompleted);
        }

        public RestOperationCanceler GetMembershipsAsync(long userId, Action<RestOperationCompletedEventArgs<CursoredList<UserList>>> operationCompleted)
        {
            return this.restTemplate.GetForObjectAsync<CursoredList<UserList>>(this.BuildUrl("lists/memberships.json", "user_id", userId.ToString()), operationCompleted);
        }

        public RestOperationCanceler GetMembershipsAsync(string screenName, Action<RestOperationCompletedEventArgs<CursoredList<UserList>>> operationCompleted)
        {
            return this.restTemplate.GetForObjectAsync<CursoredList<UserList>>(this.BuildUrl("lists/memberships.json", "screen_name", screenName), operationCompleted);
        }

        public RestOperationCanceler GetSubscriptionsAsync(long userId, Action<RestOperationCompletedEventArgs<CursoredList<UserList>>> operationCompleted)
        {
            return this.restTemplate.GetForObjectAsync<CursoredList<UserList>>(this.BuildUrl("lists/subscriptions.json", "user_id", userId.ToString()), operationCompleted);
        }

        public RestOperationCanceler GetSubscriptionsAsync(string screenName, Action<RestOperationCompletedEventArgs<CursoredList<UserList>>> operationCompleted)
        {
            return this.restTemplate.GetForObjectAsync<CursoredList<UserList>>(this.BuildUrl("lists/subscriptions.json", "screen_name", screenName), operationCompleted);
        }

        public RestOperationCanceler IsMemberAsync(long listId, long memberId, Action<RestOperationCompletedEventArgs<bool>> operationCompleted)
        {
            this.EnsureIsAuthorized();
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("list_id", listId.ToString());
            parameters.Add("user_id", memberId.ToString());
            return this.CheckListConnectionAsync(this.BuildUrl("lists/members/show.json", parameters), operationCompleted);
        }

        public RestOperationCanceler IsMemberAsync(string screenName, string listSlug, string memberScreenName, Action<RestOperationCompletedEventArgs<bool>> operationCompleted)
        {
            this.EnsureIsAuthorized();
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("owner_screen_name", screenName);
            parameters.Add("slug", listSlug);
            parameters.Add("screen_name", memberScreenName);
            return this.CheckListConnectionAsync(this.BuildUrl("lists/members/show.json", parameters), operationCompleted);
        }

        public RestOperationCanceler IsSubscriberAsync(long listId, long subscriberId, Action<RestOperationCompletedEventArgs<bool>> operationCompleted)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("list_id", listId.ToString());
            parameters.Add("user_id", subscriberId.ToString());
            return this.CheckListConnectionAsync(this.BuildUrl("lists/subscribers/show.json", parameters), operationCompleted);
        }

        public RestOperationCanceler IsSubscriberAsync(string screenName, string listSlug, string subscriberScreenName, Action<RestOperationCompletedEventArgs<bool>> operationCompleted)
        {
            this.EnsureIsAuthorized();
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("owner_screen_name", screenName);
            parameters.Add("slug", listSlug);
            parameters.Add("screen_name", subscriberScreenName);
            return this.CheckListConnectionAsync(this.BuildUrl("lists/subscribers/show.json", parameters), operationCompleted);
        }
#endif

        #endregion

        #region Private Methods

#if NET_4_0 || SILVERLIGHT_5
        private Task<bool> CheckListConnectionAsync(string url) 
        {
            return this.restTemplate.ExchangeAsync(url, HttpMethod.GET, null, CancellationToken.None)
                .ContinueWith<bool>(task =>
                {
                    return task.Result.StatusCode != HttpStatusCode.NotFound;
                });
        }
#else
#if !SILVERLIGHT
        private bool CheckListConnection(string url) 
        {
            HttpResponseMessage response = this.restTemplate.Exchange(url, HttpMethod.GET, null);
            return response.StatusCode != HttpStatusCode.NotFound;
        }
#endif

        private RestOperationCanceler CheckListConnectionAsync(string url, Action<RestOperationCompletedEventArgs<bool>> operationCompleted)
        {
            return this.restTemplate.ExchangeAsync(url, HttpMethod.GET, null, 
                r =>
                {
                    if (r.Error == null)
                    {
                        operationCompleted(new RestOperationCompletedEventArgs<bool>(
                            r.Response.StatusCode != HttpStatusCode.NotFound, r.Error, r.Cancelled, r.UserState));
                    }
                    else
                    {
                        operationCompleted(new RestOperationCompletedEventArgs<bool>(false, null, r.Cancelled, r.UserState));
                    }
                });
        }
#endif

        private static NameValueCollection BuildListParameters(string name, string description, bool isPublic) 
        {
		    NameValueCollection parameters = new NameValueCollection();
		    parameters.Add("name", name);
		    parameters.Add("description", description);
		    parameters.Add("mode", isPublic ? "public" : "private");
		    return parameters;
	    }

        #endregion
    }
}