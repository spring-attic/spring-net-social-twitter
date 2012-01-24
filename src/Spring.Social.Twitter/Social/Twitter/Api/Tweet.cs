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
    /// Represents a Twitter status update (e.g., a "tweet").
    /// </summary>
    /// <author>Craig Walls</author>
    /// <author>Bruno Baia (.NET)</author>
    public class Tweet
    {
        /// <summary>
        /// Gets or sets the tweet ID.
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// Gets or sets the tweet message.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the tweet created date.
        /// </summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the tweet author's screen name.
        /// </summary>
        public string FromUser { get; set; }

        /// <summary>
        /// Gets or sets the tweet author's profile image URL.
        /// </summary>
        public string ProfileImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the user ID when replying to a user.
        /// </summary>
        public long? ToUserId { get; set; }

        /// <summary>
        /// Gets or sets the tweet ID when replying to a tweet.
        /// </summary>
        public long? InReplyToStatusId { get; set; }

        /// <summary>
        /// Gets or sets the tweet author's ID.
        /// </summary>
        public long FromUserId { get; set; }

        /// <summary>
        /// Gets or sets the tweet's language code. May be null.
        /// </summary>
        public string LanguageCode { get; set; }

        /// <summary>
        /// Gets or sets the source from where the tweet was send.
        /// </summary>
        public string Source { get; set; }
    }
}
