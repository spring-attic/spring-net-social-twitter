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
    /// JSON deserializer for Twitter user's profile.
    /// </summary>
    /// <author>Craig Walls</author>
    /// <author>Bruno Baia (.NET)</author>
    class TwitterProfileDeserializer : IJsonDeserializer
    {
        private const string DATE_FORMAT = "ddd MMM dd HH:mm:ss zzz yyyy";

        public object Deserialize(JsonValue value, JsonMapper mapper)
        {
            return new TwitterProfile()
            {
                ID = value.GetValue<long>("id"),
                ScreenName = value.GetValue<string>("screen_name"),
                Name = value.GetValue<string>("name"),
                Url = value.GetValue<string>("url"),
                ProfileImageUrl = value.GetValue<string>("profile_image_url"),
                Description = value.GetValue<string>("description"),
                Location = value.GetValue<string>("location"),
                CreatedDate = JsonUtils.ToDateTime(value.GetValue<string>("created_at"), DATE_FORMAT),
                IsNotificationsEnabled = value.GetValueOrDefault<bool>("notifications"),
                Language = value.GetValue<string>("lang"),
                StatusesCount = value.GetValue<int>("statuses_count"),
                FriendsCount = value.GetValue<int>("friends_count"),
                FollowersCount = value.GetValue<int>("followers_count"),
                FavoritesCount = value.GetValue<int>("favourites_count"),
                ListedCount = value.GetValue<int>("listed_count"),
                IsFollowing = value.GetValueOrDefault<bool>("following"),
                IsFollowRequestSent = value.GetValueOrDefault<bool>("follow_request_sent"),
                IsProtected = value.GetValueOrDefault<bool>("protected"),
                IsVerified = value.GetValueOrDefault<bool>("verified"),
                IsGeoEnabled = value.GetValueOrDefault<bool>("geo_enabled"),
                IsContributorsEnabled = value.GetValueOrDefault<bool>("contributors_enabled"),
                IsTranslator = value.GetValueOrDefault<bool>("is_translator"),
                TimeZone = value.GetValue<string>("time_zone"),
                UtcOffset = value.GetValue<int>("utc_offset"),
                UseBackgroundImage = value.GetValueOrDefault<bool>("profile_use_background_image"),
                SidebarBorderColor = value.GetValue<string>("profile_sidebar_border_color"),
                SidebarFillColor = value.GetValue<string>("profile_sidebar_fill_color"),
                BackgroundColor = value.GetValue<string>("profile_background_color"),
                BackgroundImageUrl = value.GetValue<string>("profile_background_image_url"),
                IsBackgroundImageTiled = value.GetValueOrDefault<bool>("profile_background_tile"),
                TextColor = value.GetValue<string>("profile_text_color"),
                LinkColor = value.GetValue<string>("profile_link_color"),
                ShowAllInlineMedia = value.GetValueOrDefault<bool>("show_all_inline_media"),
            };
        }
    }
}