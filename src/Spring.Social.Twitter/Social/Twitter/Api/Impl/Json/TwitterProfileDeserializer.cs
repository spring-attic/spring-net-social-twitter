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
            TwitterProfile twitterProfile = new TwitterProfile();
            twitterProfile.ID = value.GetValue<long>("id");
            twitterProfile.ScreenName = value.GetValue<string>("screen_name");
            twitterProfile.Name = value.GetValue<string>("name");
            twitterProfile.Url = value.GetValue<string>("url");
            twitterProfile.ProfileImageUrl = value.GetValue<string>("profile_image_url");
            twitterProfile.Description = value.GetValue<string>("description");
            twitterProfile.Location = value.GetValue<string>("location");
            twitterProfile.CreatedDate = JsonUtils.ToDateTime(value.GetValue<string>("created_at"), DATE_FORMAT);
            twitterProfile.IsNotificationsEnabled = value.GetValue<bool>("notifications");
            twitterProfile.Language = value.GetValue<string>("lang");
            twitterProfile.StatusesCount = value.GetValue<int>("statuses_count");
            twitterProfile.FriendsCount = value.GetValue<int>("friends_count");
            twitterProfile.FollowersCount = value.GetValue<int>("followers_count");
            twitterProfile.FavoritesCount = value.GetValue<int>("favourites_count");
            twitterProfile.IsFollowing = value.GetValue<bool>("following");
            twitterProfile.IsFollowRequestSent = value.GetValue<bool>("follow_request_sent");
            twitterProfile.IsProtected = value.GetValue<bool>("protected");
            twitterProfile.IsVerified = value.GetValue<bool>("verified");
            twitterProfile.IsGeoEnabled = value.GetValue<bool>("geo_enabled");
            twitterProfile.IsContributorsEnabled = value.GetValue<bool>("contributors_enabled");
            twitterProfile.IsTranslator = value.GetValue<bool>("is_translator");
            twitterProfile.TimeZone = value.GetValue<string>("time_zone");
            twitterProfile.UtcOffset = value.GetValue<int>("utc_offset");
            twitterProfile.UseBackgroundImage = value.GetValue<bool>("profile_use_background_image");
            twitterProfile.SidebarBorderColor = value.GetValue<string>("profile_sidebar_border_color");
            twitterProfile.SidebarFillColor = value.GetValue<string>("profile_sidebar_fill_color");
            twitterProfile.BackgroundColor = value.GetValue<string>("profile_background_color");
            twitterProfile.BackgroundImageUrl = value.GetValue<string>("profile_background_image_url");
            twitterProfile.IsBackgroundImageTiled = value.GetValue<bool>("profile_background_tile");
            twitterProfile.TextColor = value.GetValue<string>("profile_text_color");
            twitterProfile.LinkColor = value.GetValue<string>("profile_link_color");
            twitterProfile.ShowAllInlineMedia = value.GetValue<bool>("show_all_inline_media");
            return twitterProfile;
        }
    }
}