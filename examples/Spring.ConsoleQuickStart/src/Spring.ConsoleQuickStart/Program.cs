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
        // Register your own Twitter app at https://dev.twitter.com/apps/new with "Read & Write" access type.
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
                //ITwitter twitter = new TwitterTemplate();

                twitter.UserOperations.GetUserProfileAsync("brbaia")
                    .ContinueWith(task => Console.WriteLine("brbaia is " + task.Result.Name));

                // Use step by step debugging
/*
                Tweet tweet = twitter.TimelineOperations.UpdateStatusAsync(
                    "Hi from #Spring.NET Social Twitter! http://bit.ly/x2rvlC", new AssemblyResource("Image.png", typeof(Program))).Result;
                twitter.UserOperations.GetUserProfileImageAsync("twitter", ImageSize.Original)
                    .ContinueWith(task =>
                    {
                        // Save file to "C:\twitter.jpg"
                        using (FileStream fileStream = new FileStream(@"C:\twitter.jpg", FileMode.Create))
                        {
                            fileStream.Write(task.Result, 0, task.Result.Length);
                        }
                    });
                SearchResults searchResults = twitter.SearchOperations.SearchAsync("Portugal").Result;
                CursoredList<UserList> s2Lists = twitter.ListOperations.GetListsAsync("SpringSource").Result;
                IList<Place> places = twitter.GeoOperations.SearchAsync(33.050278, -96.745833).Result;
                bool friendshipExists = twitter.FriendOperations.FriendshipExistsAsync("brbaia", "sbohlen").Result;
                IList<DirectMessage> myDmReceived = twitter.DirectMessageOperations.GetDirectMessagesReceivedAsync().Result;
                bool isBlocking = twitter.BlockOperations.IsBlockingAsync("brbaia").Result;
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
                //ITwitter twitter = new TwitterTemplate();

                TwitterProfile profile = twitter.UserOperations.GetUserProfile("brbaia");
                Console.WriteLine("brbaia is " + profile.Name);

                // Use step by step debugging
/*
                Tweet tweet = twitter.TimelineOperations.UpdateStatus(
                    "Hi from #Spring.NET Social Twitter! http://bit.ly/x2rvlC", new AssemblyResource("Image.png", typeof(Program)));
                twitter.UserOperations.GetUserProfileImageAsync("twitter", ImageSize.Original, 
                    r =>
                    {
                        // Save file to "C:\twitter.jpg"
                        using (FileStream fileStream = new FileStream(@"C:\twitter.jpg", FileMode.Create))
                        {
                            fileStream.Write(r.Response, 0, r.Response.Length);
                        }
                    });
                SearchResults searchResults = twitter.SearchOperations.Search("Portugal");
                CursoredList<UserList> s2Lists = twitter.ListOperations.GetLists("SpringSource");
                IList<Place> places = twitter.GeoOperations.Search(33.050278, -96.745833);
                bool friendshipExists = twitter.FriendOperations.FriendshipExists("brbaia", "sbohlen");
                IList<DirectMessage> myDmReceived = twitter.DirectMessageOperations.GetDirectMessagesReceived();
                bool isBlocking = twitter.BlockOperations.IsBlocking("brbaia");
*/
            }
            catch (TwitterApiException ex)
            {
                Console.WriteLine(ex);
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

