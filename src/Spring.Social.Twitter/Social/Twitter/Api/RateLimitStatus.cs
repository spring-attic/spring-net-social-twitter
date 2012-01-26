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

namespace Spring.Social.Twitter.Api
{
    /// <summary>
    /// Carries data concerning the rate limit status.
    /// </summary>
    /// <author>Craig Walls</author>
    /// <author>Bruno Baia (.NET)</author>
#if !SILVERLIGHT
    [Serializable]
#endif
    public class RateLimitStatus
    {
        /// <summary>
        /// Gets or sets the limited number of calls per hour. 
        /// <para/>
        /// Unauthenticated calls are permitted 150 requests per hour. 
        /// <para/>
        /// OAuth calls are permitted 350 requests per hour.
        /// </summary>
	    public int HourlyLimit { get; set; }

        /// <summary>
        /// Gets or sets the remaining number of calls before being rate limited.
        /// </summary>
        public int RemainingHits { get; set; }

        /// <summary>
        /// Gets or sets the date when the limit number of calls will be reset.
        /// </summary>
        public DateTime? ResetTime { get; set; }
    }
}
