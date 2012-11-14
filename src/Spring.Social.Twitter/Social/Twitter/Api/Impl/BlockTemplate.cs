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
    /// Implementation of <see cref="IBlockOperations"/>, providing a binding to Twitter's block REST resources.
    /// </summary>
    /// <author>Craig Walls</author>
    /// <author>Bruno Baia (.NET)</author>
    class BlockTemplate : AbstractTwitterOperations, IBlockOperations
    {
        private RestTemplate restTemplate;

        public BlockTemplate(RestTemplate restTemplate, bool isAuthorized)
            : base(isAuthorized)
        {
            this.restTemplate = restTemplate;
        }

        #region IBlockOperations Members

#if NET_4_0 || SILVERLIGHT_5
        public Task<TwitterProfile> BlockAsync(long userId) 
        {
		    this.EnsureIsAuthorized();
		    NameValueCollection request = new NameValueCollection();
		    request.Add("user_id", userId.ToString());
		    return this.restTemplate.PostForObjectAsync<TwitterProfile>("blocks/create.json", request);
	    }

        public Task<TwitterProfile> BlockAsync(string screenName) 
        {
		    this.EnsureIsAuthorized();
		    NameValueCollection request = new NameValueCollection();
		    request.Add("screen_name", screenName);
		    return this.restTemplate.PostForObjectAsync<TwitterProfile>("blocks/create.json", request);
	    }

        public Task<TwitterProfile> UnblockAsync(long userId) 
        {
		    this.EnsureIsAuthorized();
		    NameValueCollection request = new NameValueCollection();
		    request.Add("user_id", userId.ToString());
		    return this.restTemplate.PostForObjectAsync<TwitterProfile>("blocks/destroy.json", request);
	    }

        public Task<TwitterProfile> UnblockAsync(String screenName) 
        {
		    this.EnsureIsAuthorized();
		    NameValueCollection request = new NameValueCollection();
		    request.Add("screen_name", screenName);
		    return this.restTemplate.PostForObjectAsync<TwitterProfile>("blocks/destroy.json", request);
	    }

        public Task<IList<TwitterProfile>> GetBlockedUsersAsync() 
        {
		    return this.GetBlockedUsersAsync(1, 20);
	    }

        public Task<IList<TwitterProfile>> GetBlockedUsersAsync(int page, int pageSize) 
        {
		    this.EnsureIsAuthorized();
		    NameValueCollection parameters = PagingUtils.BuildPagingParametersWithPerPage(page, pageSize, 0, 0);
		    return this.restTemplate.GetForObjectAsync<IList<TwitterProfile>>(this.BuildUrl("blocks/blocking.json", parameters));
	    }

        public Task<IList<long>> GetBlockedUserIdsAsync() 
        {
		    this.EnsureIsAuthorized();
		    return this.restTemplate.GetForObjectAsync<IList<long>>("blocks/blocking/ids.json");
	    }

        public Task<bool> IsBlockingAsync(long userId) 
        {
            return this.InternalIsBlockingAsync(this.BuildUrl("blocks/exists.json", "user_id", userId.ToString()));
	    }

        public Task<bool> IsBlockingAsync(string screenName) 
        {
            return this.InternalIsBlockingAsync(this.BuildUrl("blocks/exists.json", "screen_name", screenName));
	    }
#else
#if !SILVERLIGHT
        public TwitterProfile Block(long userId) 
        {
		    this.EnsureIsAuthorized();
		    NameValueCollection request = new NameValueCollection();
		    request.Add("user_id", userId.ToString());
		    return this.restTemplate.PostForObject<TwitterProfile>("blocks/create.json", request);
	    }
	
	    public TwitterProfile Block(string screenName) 
        {
		    this.EnsureIsAuthorized();
		    NameValueCollection request = new NameValueCollection();
		    request.Add("screen_name", screenName);
		    return this.restTemplate.PostForObject<TwitterProfile>("blocks/create.json", request);
	    }
	
	    public TwitterProfile Unblock(long userId) 
        {
		    this.EnsureIsAuthorized();
		    NameValueCollection request = new NameValueCollection();
		    request.Add("user_id", userId.ToString());
		    return this.restTemplate.PostForObject<TwitterProfile>("blocks/destroy.json", request);
	    }
	
	    public TwitterProfile Unblock(String screenName) 
        {
		    this.EnsureIsAuthorized();
		    NameValueCollection request = new NameValueCollection();
		    request.Add("screen_name", screenName);
		    return this.restTemplate.PostForObject<TwitterProfile>("blocks/destroy.json", request);
	    }
	
	    public IList<TwitterProfile> GetBlockedUsers() 
        {
		    return this.GetBlockedUsers(1, 20);
	    }
	
	    public IList<TwitterProfile> GetBlockedUsers(int page, int pageSize) 
        {
		    this.EnsureIsAuthorized();
		    NameValueCollection parameters = PagingUtils.BuildPagingParametersWithPerPage(page, pageSize, 0, 0);
		    return this.restTemplate.GetForObject<IList<TwitterProfile>>(this.BuildUrl("blocks/blocking.json", parameters));
	    }

	    public IList<long> GetBlockedUserIds() 
        {
		    this.EnsureIsAuthorized();
		    return this.restTemplate.GetForObject<IList<long>>("blocks/blocking/ids.json");
	    }
	
	    public bool IsBlocking(long userId) 
        {
		    return this.InternalIsBlocking(this.BuildUrl("blocks/exists.json", "user_id", userId.ToString()));
	    }

	    public bool IsBlocking(string screenName) 
        {
		    return this.InternalIsBlocking(this.BuildUrl("blocks/exists.json", "screen_name", screenName));
	    }
#endif

        public RestOperationCanceler BlockAsync(long userId, Action<RestOperationCompletedEventArgs<TwitterProfile>> operationCompleted)
        {
            this.EnsureIsAuthorized();
            NameValueCollection request = new NameValueCollection();
            request.Add("user_id", userId.ToString());
            return this.restTemplate.PostForObjectAsync<TwitterProfile>("blocks/create.json", request, operationCompleted);
        }

        public RestOperationCanceler BlockAsync(string screenName, Action<RestOperationCompletedEventArgs<TwitterProfile>> operationCompleted)
        {
            this.EnsureIsAuthorized();
            NameValueCollection request = new NameValueCollection();
            request.Add("screen_name", screenName);
            return this.restTemplate.PostForObjectAsync<TwitterProfile>("blocks/create.json", request, operationCompleted);
        }

        public RestOperationCanceler UnblockAsync(long userId, Action<RestOperationCompletedEventArgs<TwitterProfile>> operationCompleted)
        {
            this.EnsureIsAuthorized();
            NameValueCollection request = new NameValueCollection();
            request.Add("user_id", userId.ToString());
            return this.restTemplate.PostForObjectAsync<TwitterProfile>("blocks/destroy.json", request, operationCompleted);
        }

        public RestOperationCanceler UnblockAsync(String screenName, Action<RestOperationCompletedEventArgs<TwitterProfile>> operationCompleted)
        {
            this.EnsureIsAuthorized();
            NameValueCollection request = new NameValueCollection();
            request.Add("screen_name", screenName);
            return this.restTemplate.PostForObjectAsync<TwitterProfile>("blocks/destroy.json", request, operationCompleted);
        }

        public RestOperationCanceler GetBlockedUsersAsync(Action<RestOperationCompletedEventArgs<IList<TwitterProfile>>> operationCompleted)
        {
            return this.GetBlockedUsersAsync(1, 20, operationCompleted);
        }

        public RestOperationCanceler GetBlockedUsersAsync(int page, int pageSize, Action<RestOperationCompletedEventArgs<IList<TwitterProfile>>> operationCompleted)
        {
            this.EnsureIsAuthorized();
            NameValueCollection parameters = PagingUtils.BuildPagingParametersWithPerPage(page, pageSize, 0, 0);
            return this.restTemplate.GetForObjectAsync<IList<TwitterProfile>>(this.BuildUrl("blocks/blocking.json", parameters), operationCompleted);
        }

        public RestOperationCanceler GetBlockedUserIdsAsync(Action<RestOperationCompletedEventArgs<IList<long>>> operationCompleted)
        {
            this.EnsureIsAuthorized();
            return this.restTemplate.GetForObjectAsync<IList<long>>("blocks/blocking/ids.json", operationCompleted);
        }

        public RestOperationCanceler IsBlockingAsync(long userId, Action<RestOperationCompletedEventArgs<bool>> operationCompleted)
        {
            return this.InternalIsBlockingAsync(this.BuildUrl("blocks/exists.json", "user_id", userId.ToString()), operationCompleted);
        }

        public RestOperationCanceler IsBlockingAsync(string screenName, Action<RestOperationCompletedEventArgs<bool>> operationCompleted)
        {
            return this.InternalIsBlockingAsync(this.BuildUrl("blocks/exists.json", "screen_name", screenName), operationCompleted);
        }
#endif

        #endregion

        #region Private Methods

#if NET_4_0 || SILVERLIGHT_5
        private Task<bool> InternalIsBlockingAsync(string blockingExistsUrl) 
        {
            return this.restTemplate.ExchangeAsync(blockingExistsUrl, HttpMethod.GET, null, CancellationToken.None)
                .ContinueWith<bool>(task =>
                {
                    return task.Result.StatusCode != HttpStatusCode.NotFound;
                }, TaskContinuationOptions.ExecuteSynchronously);
        }
#else
#if !SILVERLIGHT
        private bool InternalIsBlocking(string blockingExistsUrl) 
        {
            HttpResponseMessage response = this.restTemplate.Exchange(blockingExistsUrl, HttpMethod.GET, null);
            return response.StatusCode != HttpStatusCode.NotFound;
        }
#endif

        private RestOperationCanceler InternalIsBlockingAsync(string blockingExistsUrl, Action<RestOperationCompletedEventArgs<bool>> operationCompleted)
        {
            return this.restTemplate.ExchangeAsync(blockingExistsUrl, HttpMethod.GET, null, 
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

        #endregion
    }
}