using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Spring.Social.OAuth1;
using Spring.Social.Twitter.Api;
using Spring.Social.Twitter.Connect;

namespace Spring.MvcQuickStart.Controllers
{
    public class TwitterController : Controller
    {
        // Register your own Twitter app at https://dev.twitter.com/apps/new with "Read & Write" access type
        // Configure the Callback URL with 'http://localhost/Twitter/Callback'
        // Set your consumer key & secret here
        private const string TwitterConsumerKey = TODO;
        private const string TwitterConsumerSecret = TODO;

        IOAuth1ServiceProvider<ITwitter> twitterProvider = 
            new TwitterServiceProvider(TwitterConsumerKey, TwitterConsumerSecret);

        // GET: /Twitter/Index
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Twitter/SignIn
        public ActionResult SignIn()
        {
            OAuthToken requestToken = twitterProvider.OAuthOperations.FetchRequestTokenAsync("http://localhost/Twitter/Callback", null).Result;
            Session["RequestToken"] = requestToken;

            return Redirect(twitterProvider.OAuthOperations.BuildAuthenticateUrl(requestToken.Value, null));
        }

        // GET: /Twitter/Callback
        public ActionResult Callback(string oauth_verifier)
        {
            OAuthToken requestToken = Session["RequestToken"] as OAuthToken;
            AuthorizedRequestToken authorizedRequestToken = new AuthorizedRequestToken(requestToken, oauth_verifier);
            OAuthToken token = twitterProvider.OAuthOperations.ExchangeForAccessTokenAsync(authorizedRequestToken, null).Result;

            Session["AccessToken"] = token;

            ITwitter twitterClient = twitterProvider.GetApi(token.Value, token.Secret);
            TwitterProfile profile = twitterClient.UserOperations.GetUserProfileAsync().Result;
            return View(profile);
        }

        // GET: /Twitter/UpdateStatus
        public ActionResult UpdateStatus()
        {
            OAuthToken token = Session["AccessToken"] as OAuthToken;
            ITwitter twitterClient = twitterProvider.GetApi(token.Value, token.Secret);

            IList<Tweet> tweets = twitterClient.TimelineOperations.GetHomeTimelineAsync().Result;
            return View(tweets);
        }

        // POST: /Twitter/UpdateStatus
        [HttpPost]
        public ActionResult UpdateStatus(string status)
        {
            OAuthToken token = Session["AccessToken"] as OAuthToken;
            ITwitter twitterClient = twitterProvider.GetApi(token.Value, token.Secret);

            twitterClient.TimelineOperations.UpdateStatusAsync(status);

            return View();
        }
    }
}
