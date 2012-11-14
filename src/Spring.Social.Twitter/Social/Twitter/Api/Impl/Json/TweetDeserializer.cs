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
using System.Globalization;

using Spring.Json;

namespace Spring.Social.Twitter.Api.Impl.Json
{
    /// <summary>
    /// JSON deserializer for tweets. 
    /// The JSON structure varies between the search API and the timeline API. 
    /// This deserializer determine which structure is in play and creates a tweet from it.
    /// </summary>
    /// <author>Craig Walls</author>
    /// <author>Bruno Baia (.NET)</author>
    class TweetDeserializer : IJsonDeserializer
    {
        private const string SEARCH_DATE_FORMAT = "ddd, dd MMM yyyy HH:mm:ss zzz";
        private const string TIMELINE_DATE_FORMAT = "ddd MMM dd HH:mm:ss zzz yyyy";

        public object Deserialize(JsonValue value, JsonMapper mapper)
        {
            Tweet tweet = new Tweet();

            tweet.ID = value.GetValue<long>("id");
            tweet.Text = value.GetValue<string>("text");
            JsonValue fromUserValue = value.GetValue("user");
            string dateFormat;
            if (fromUserValue != null)
            {
                tweet.FromUser = fromUserValue.GetValue<string>("screen_name");
                tweet.FromUserId = fromUserValue.GetValue<long>("id");
                tweet.ProfileImageUrl = fromUserValue.GetValue<string>("profile_image_url");
                dateFormat = TIMELINE_DATE_FORMAT;
            }
            else
            {
                tweet.FromUser = value.GetValue<string>("from_user");
                tweet.FromUserId = value.GetValue<long>("from_user_id");
                tweet.ProfileImageUrl = value.GetValue<string>("profile_image_url");
                dateFormat = SEARCH_DATE_FORMAT;
            }
            tweet.CreatedAt = JsonUtils.ToDateTime(value.GetValue<string>("created_at"), dateFormat);
            tweet.Source = value.GetValue<string>("source");
            tweet.ToUserId = value.GetValueOrDefault<long?>("in_reply_to_user_id");
            tweet.LanguageCode = value.GetValueOrDefault<string>("iso_language_code");
            tweet.InReplyToStatusId = value.GetValueOrDefault<long?>("in_reply_to_status_id");

            return tweet;
        }
    }
}