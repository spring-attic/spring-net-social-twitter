using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;

using Spring.IO;
using Spring.Social.OAuth1;
using Spring.Social.Twitter.Api;
using Spring.Social.Twitter.Api.Impl;
using Spring.Social.Twitter.Connect;

namespace Spring.ConsoleQuickStart
{
    class Program
    {
        // Register your own Twitter app at https://dev.twitter.com/apps/new with "Read, Write and Access direct messages" access type.
        // Set your consumer key & secret here
        private const string TwitterConsumerKey = TODO;
        private const string TwitterConsumerSecret = TODO;

        static void Main(string[] args)
        {
            try
            {
                TwitterServiceProvider twitterServiceProvider = new TwitterServiceProvider(TwitterConsumerKey, TwitterConsumerSecret);

#if NET_4_0
                /* OAuth 'dance' */

                // Authentication using Out-of-band/PIN Code Authentication
                Console.Write("Getting request token...");
                OAuthToken oauthToken = twitterServiceProvider.OAuthOperations.FetchRequestTokenAsync("oob", null).Result;
                Console.WriteLine("Done");

                string authenticateUrl = twitterServiceProvider.OAuthOperations.BuildAuthorizeUrl(oauthToken.Value, null);
                Console.WriteLine("Redirect user for authentication: " + authenticateUrl);
                Process.Start(authenticateUrl);
                Console.WriteLine("Enter PIN Code from Twitter authorization page:");
                string pinCode = Console.ReadLine();

                Console.Write("Getting access token...");
                AuthorizedRequestToken requestToken = new AuthorizedRequestToken(oauthToken, pinCode);
                OAuthToken oauthAccessToken = twitterServiceProvider.OAuthOperations.ExchangeForAccessTokenAsync(requestToken, null).Result;
                Console.WriteLine("Done");

                /* API */

                ITwitter twitter = twitterServiceProvider.GetApi(oauthAccessToken.Value, oauthAccessToken.Secret);

                twitter.UserOperations.GetUserProfileAsync("brbaia")
                    .ContinueWith(task => Console.WriteLine("brbaia is " + task.Result.Name));

                // Use step by step debugging
/*
                // IBlockOperations
                TwitterProfile blockedProfile = twitter.BlockOperations.BlockAsync("brbaia").Result;
                CursoredList<TwitterProfile> blockedIserProfiles = twitter.BlockOperations.GetBlockedUsersAsync().Result;
                CursoredList<long> blockedUserIds = twitter.BlockOperations.GetBlockedUserIdsAsync().Result;
                blockedProfile = twitter.BlockOperations.UnblockAsync(blockedProfile.ID).Result;

                // IDirectMessageOperations
                IList<DirectMessage> directMessagesSent = twitter.DirectMessageOperations.GetDirectMessagesSentAsync().Result;
                IList<DirectMessage> directMessagesReceived = twitter.DirectMessageOperations.GetDirectMessagesReceivedAsync().Result;
                DirectMessage directMessage = twitter.DirectMessageOperations.SendDirectMessageAsync("brbaia", "Hi from #Spring.NET Social Twitter!").Result;
                directMessage = twitter.DirectMessageOperations.GetDirectMessageAsync(directMessage.ID).Result;
                directMessage = twitter.DirectMessageOperations.DeleteDirectMessageAsync(directMessage.ID).Result;

                // IFriendOperations
                CursoredList<TwitterProfile> followers = twitter.FriendOperations.GetFollowersAsync().Result;
                CursoredList<TwitterProfile> friends = twitter.FriendOperations.GetFriendsAsync().Result;
                TwitterProfile newFriend = twitter.FriendOperations.FollowAsync("brbaia").Result;
                twitter.FriendOperations.EnableNotificationsAsync("brbaia").Wait();

                // IGeoOperations
                IList<Place> places = twitter.GeoOperations.SearchAsync(37.7821120598956, -122.400612831116).Result;

                // IListOperations
                IList<UserList> myLists = twitter.ListOperations.GetListsAsync().Result;
                IList<UserList> s2Lists = twitter.ListOperations.GetListsAsync("SpringSource").Result;
                IList<TwitterProfile> listMembers = twitter.ListOperations.GetListMembersAsync(s2Lists[0].ID).Result;
                IList<TwitterProfile> listSubscribers = twitter.ListOperations.GetListSubscribersAsync(s2Lists[0].ID).Result;
                IList<Tweet> listTweets = twitter.ListOperations.GetListStatusesAsync(s2Lists[0].ID).Result;
                CursoredList<UserList> listMemberships = twitter.ListOperations.GetMembershipsAsync(listMembers[0].ID).Result;
                UserList myNewList = twitter.ListOperations.CreateListAsync("My list", "List description", true).Result;
                twitter.ListOperations.AddToListAsync(myNewList.ID, "brbaia").Wait();
                twitter.ListOperations.DeleteListAsync(myNewList.ID).Wait();

                // ISearchOperations
                SearchResults searchResults = twitter.SearchOperations.SearchAsync("#spring").Result;
                SavedSearch savedSearch = twitter.SearchOperations.CreateSavedSearchAsync("@brbaia").Result;
                IList<SavedSearch> savedSearches = twitter.SearchOperations.GetSavedSearchesAsync().Result;
                twitter.SearchOperations.DeleteSavedSearchAsync(savedSearch.ID).Wait();
                Trends globalTrends = twitter.SearchOperations.GetTrendsAsync(1).Result;

                // ITimelineOperations
                Tweet tweet = twitter.TimelineOperations.UpdateStatusAsync(
                    "Hi from #Spring.NET Social Twitter! http://bit.ly/x2rvlC", new AssemblyResource("Image.png", typeof(Program))).Result;
                twitter.TimelineOperations.DeleteStatusAsync(tweet.ID).Wait();
                IList<Tweet> homeTimeline = twitter.TimelineOperations.GetHomeTimelineAsync().Result;
                IList<Tweet> userTimeline = twitter.TimelineOperations.GetUserTimelineAsync().Result;
                IList<Tweet> mentions = twitter.TimelineOperations.GetMentionsAsync().Result;
                IList<Tweet> retweetsOfMe = twitter.TimelineOperations.GetRetweetsOfMeAsync().Result;
                IList<Tweet> retweets = twitter.TimelineOperations.GetRetweetsAsync(homeTimeline[0].ID).Result;
                IList<Tweet> favorites = twitter.TimelineOperations.GetFavoritesAsync().Result;

                // IUserOperations
                TwitterProfile userProfile = twitter.UserOperations.GetUserProfileAsync().Result;
                IList<TwitterProfile> searchProfiles = twitter.UserOperations.SearchForUsersAsync("spring").Result;
                IList<RateLimitStatus> limits = twitter.UserOperations.GetRateLimitStatusAsync("users", "search", "statuses").Result;
*/
            }
            catch (AggregateException ae)
            {
                ae.Handle(ex =>
                    {
                        if (ex is TwitterApiException)
                        {
                            Console.WriteLine(ex.Message);
                            return true;
                        }
                        return false;
                    });
            }
#else
                /* OAuth 'dance' */

                // Authentication using Out-of-band/PIN Code Authentication
                Console.Write("Getting request token...");
                OAuthToken oauthToken = twitterServiceProvider.OAuthOperations.FetchRequestToken("oob", null);
                Console.WriteLine("Done");

                string authenticateUrl = twitterServiceProvider.OAuthOperations.BuildAuthorizeUrl(oauthToken.Value, null);
                Console.WriteLine("Redirect user for authentication: " + authenticateUrl);
                Process.Start(authenticateUrl);
                Console.WriteLine("Enter PIN Code from Twitter authorization page:");
                string pinCode = Console.ReadLine();

                Console.Write("Getting access token...");
                AuthorizedRequestToken requestToken = new AuthorizedRequestToken(oauthToken, pinCode);
                OAuthToken oauthAccessToken = twitterServiceProvider.OAuthOperations.ExchangeForAccessToken(requestToken, null);
                Console.WriteLine("Done");

                /* API */

                ITwitter twitter = twitterServiceProvider.GetApi(oauthAccessToken.Value, oauthAccessToken.Secret);

                TwitterProfile profile = twitter.UserOperations.GetUserProfile("brbaia");
                Console.WriteLine("brbaia is " + profile.Name);

                // Use step by step debugging
/*
                // IBlockOperations
                TwitterProfile blockedProfile = twitter.BlockOperations.Block("brbaia");
                CursoredList<TwitterProfile> blockedIserProfiles = twitter.BlockOperations.GetBlockedUsers();
                CursoredList<long> blockedUserIds = twitter.BlockOperations.GetBlockedUserIds();
                blockedProfile = twitter.BlockOperations.Unblock(blockedProfile.ID);

                // IDirectMessageOperations
                IList<DirectMessage> directMessagesSent = twitter.DirectMessageOperations.GetDirectMessagesSent();
                IList<DirectMessage> directMessagesReceived = twitter.DirectMessageOperations.GetDirectMessagesReceived();
                DirectMessage directMessage = twitter.DirectMessageOperations.SendDirectMessage("brbaia", "Hi from #Spring.NET Social Twitter!");
                directMessage = twitter.DirectMessageOperations.GetDirectMessage(directMessage.ID);
                directMessage = twitter.DirectMessageOperations.DeleteDirectMessage(directMessage.ID);

                // IFriendOperations
                CursoredList<TwitterProfile> followers = twitter.FriendOperations.GetFollowers();
                CursoredList<TwitterProfile> friends = twitter.FriendOperations.GetFriends();
                TwitterProfile newFriend = twitter.FriendOperations.Follow("brbaia");
                twitter.FriendOperations.EnableNotifications("brbaia");

                // IGeoOperations
                IList<Place> places = twitter.GeoOperations.Search(37.7821120598956, -122.400612831116);

                // IListOperations
                IList<UserList> myLists = twitter.ListOperations.GetLists();
                IList<UserList> s2Lists = twitter.ListOperations.GetLists("SpringSource");
                IList<TwitterProfile> listMembers = twitter.ListOperations.GetListMembers(s2Lists[0].ID);
                IList<TwitterProfile> listSubscribers = twitter.ListOperations.GetListSubscribers(s2Lists[0].ID);
                IList<Tweet> listTweets = twitter.ListOperations.GetListStatuses(s2Lists[0].ID);
                CursoredList<UserList> listMemberships = twitter.ListOperations.GetMemberships(listMembers[0].ID);
                UserList myNewList = twitter.ListOperations.CreateList("My list", "List description", true);
                twitter.ListOperations.AddToList(myNewList.ID, "brbaia");
                twitter.ListOperations.DeleteList(myNewList.ID);

                // ISearchOperations
                SearchResults searchResults = twitter.SearchOperations.Search("#spring");
                SavedSearch savedSearch = twitter.SearchOperations.CreateSavedSearch("@brbaia");
                IList<SavedSearch> savedSearches = twitter.SearchOperations.GetSavedSearches();
                twitter.SearchOperations.DeleteSavedSearch(savedSearch.ID);
                Trends globalTrends = twitter.SearchOperations.GetTrends(1);

                // ITimelineOperations
                Tweet tweet = twitter.TimelineOperations.UpdateStatus(
                    "Hi from #Spring.NET Social Twitter! http://bit.ly/x2rvlC", new AssemblyResource("Image.png", typeof(Program)));
                twitter.TimelineOperations.DeleteStatus(tweet.ID);
                IList<Tweet> homeTimeline = twitter.TimelineOperations.GetHomeTimeline();
                IList<Tweet> userTimeline = twitter.TimelineOperations.GetUserTimeline();
                IList<Tweet> mentions = twitter.TimelineOperations.GetMentions();
                IList<Tweet> retweetsOfMe = twitter.TimelineOperations.GetRetweetsOfMe();
                IList<Tweet> retweets = twitter.TimelineOperations.GetRetweets(homeTimeline[0].ID);
                IList<Tweet> favorites = twitter.TimelineOperations.GetFavorites();

                // IUserOperations
                TwitterProfile userProfile = twitter.UserOperations.GetUserProfile();
                IList<TwitterProfile> searchProfiles = twitter.UserOperations.SearchForUsers("spring");
                IList<RateLimitStatus> limits = twitter.UserOperations.GetRateLimitStatus("users", "search", "statuses");
*/
            }
            catch (TwitterApiException ex)
            {
                Console.WriteLine(ex.Message);
            }
#endif
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                Console.WriteLine("--- hit <return> to quit ---");
                Console.ReadLine();
            }
        }
    }
}

