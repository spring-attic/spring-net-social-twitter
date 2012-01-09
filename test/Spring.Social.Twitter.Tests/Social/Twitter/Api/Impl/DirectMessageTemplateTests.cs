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

using System.Net;
using System.Collections.Generic;

using NUnit.Framework;
using Spring.Rest.Client.Testing;

using Spring.Http;
using Spring.IO;

namespace Spring.Social.Twitter.Api.Impl
{
    /// <summary>
    /// Unit tests for the DirectMessage class.
    /// </summary>
    /// <author>Craig Walls</author>
    /// <author>Bruno Baia (.NET)</author>
    [TestFixture]
    public class DirectMessageTests : AbstractTwitterOperationsTests 
    {    
	    [Test]
	    public void GetDirectMessagesReceived() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/direct_messages.json?page=1&count=20")
				.AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("Messages"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    IList<DirectMessage> messages = twitter.DirectMessageOperations.GetDirectMessagesReceivedAsync().Result;
#else
            IList<DirectMessage> messages = twitter.DirectMessageOperations.GetDirectMessagesReceived();
#endif
            AssertDirectMessageListContents(messages);
	    }
	
	    [Test]
	    public void GetDirectMessagesReceived_Paged() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/direct_messages.json?page=3&count=12")
				.AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("Messages"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    IList<DirectMessage> messages = twitter.DirectMessageOperations.GetDirectMessagesReceivedAsync(3, 12).Result;
#else
            IList<DirectMessage> messages = twitter.DirectMessageOperations.GetDirectMessagesReceived(3, 12);
#endif
            AssertDirectMessageListContents(messages);
	    }

	    [Test]
	    public void GetDirectMessagesReceived_Paged_WithSinceIdAndMaxId() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/direct_messages.json?page=3&count=12&since_id=112233&max_id=332211")
				.AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("Messages"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    IList<DirectMessage> messages = twitter.DirectMessageOperations.GetDirectMessagesReceivedAsync(3, 12, 112233, 332211).Result;
#else
            IList<DirectMessage> messages = twitter.DirectMessageOperations.GetDirectMessagesReceived(3, 12, 112233, 332211);
#endif
            AssertDirectMessageListContents(messages);
	    }

	    [Test]
	    [ExpectedException(typeof(NotAuthorizedException), 
            ExpectedMessage = "Authorization is required for the operation, but the API binding was created without authorization.")]
	    public void GetDirectMessagesReceived_Unauthorized() 
        {
#if NET_4_0 || SILVERLIGHT_5
		    unauthorizedTwitter.DirectMessageOperations.GetDirectMessagesReceivedAsync().Wait();
#else
            unauthorizedTwitter.DirectMessageOperations.GetDirectMessagesReceived();
#endif
        }

	    [Test]
	    public void GetDirectMessagesSent() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/direct_messages/sent.json?page=1&count=20")
				.AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("Messages"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    IList<DirectMessage> messages = twitter.DirectMessageOperations.GetDirectMessagesSentAsync().Result;
#else
            IList<DirectMessage> messages = twitter.DirectMessageOperations.GetDirectMessagesSent();
#endif
            AssertDirectMessageListContents(messages);
	    }

	    [Test]
	    public void GetDirectMessagesSent_Paged() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/direct_messages/sent.json?page=3&count=25")
				.AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("Messages"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    IList<DirectMessage> messages = twitter.DirectMessageOperations.GetDirectMessagesSentAsync(3, 25).Result;
#else
            IList<DirectMessage> messages = twitter.DirectMessageOperations.GetDirectMessagesSent(3, 25);
#endif
            AssertDirectMessageListContents(messages);
	    }

	    [Test]
	    public void GetDirectMessagesSent_Paged_WithSinceIdAndMaxId() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/direct_messages/sent.json?page=3&count=25&since_id=2468&max_id=8642")
				.AndExpectMethod(HttpMethod.GET)
				.AndRespondWith(JsonResource("Messages"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    IList<DirectMessage> messages = twitter.DirectMessageOperations.GetDirectMessagesSentAsync(3, 25, 2468, 8642).Result;
#else
            IList<DirectMessage> messages = twitter.DirectMessageOperations.GetDirectMessagesSent(3, 25, 2468, 8642);
#endif
            AssertDirectMessageListContents(messages);
	    }

	    [Test]
	    [ExpectedException(typeof(NotAuthorizedException), 
            ExpectedMessage = "Authorization is required for the operation, but the API binding was created without authorization.")]
	    public void GetDirectMessagesSent_Unauthorized() 
        {
#if NET_4_0 || SILVERLIGHT_5
		    unauthorizedTwitter.DirectMessageOperations.GetDirectMessagesSentAsync().Wait();
#else
            unauthorizedTwitter.DirectMessageOperations.GetDirectMessagesSent();
#endif
        }

	    [Test]
	    public void GetDirectMessage() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/direct_messages/show/23456.json")
			    .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("Direct_Message"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    DirectMessage message = twitter.DirectMessageOperations.GetDirectMessageAsync(23456).Result;
#else
            DirectMessage message = twitter.DirectMessageOperations.GetDirectMessage(23456);
#endif
            AssertSingleDirectMessage(message);
	    }
	
	    [Test]
	    public void SendDirectMessage_ToScreenName() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/direct_messages/new.json").AndExpectMethod(HttpMethod.POST)
				.AndExpectBody("screen_name=habuma&text=Hello+there!")
                .AndRespondWith(JsonResource("Direct_Message"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    DirectMessage message = twitter.DirectMessageOperations.SendDirectMessageAsync("habuma", "Hello there!").Result;
#else
            DirectMessage message = twitter.DirectMessageOperations.SendDirectMessage("habuma", "Hello there!");
#endif
            AssertSingleDirectMessage(message);
	    }

	    [Test]
#if NET_4_0 || SILVERLIGHT_5
#else
        [ExpectedException(typeof(OperationNotPermittedException),
            ExpectedMessage = "There was an error sending your message: The text of your direct message is over 140 characters.")]
#endif
        public void SendDirectMessage_ToScreenName_TooLong() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/direct_messages/new.json")
                .AndExpectMethod(HttpMethod.POST)
				.AndExpectBody("screen_name=habuma&text=Really+long+message")
			    .AndRespondWith("{\"error\":\"There was an error sending your message: The text of your direct message is over 140 characters.\"}", responseHeaders, HttpStatusCode.Forbidden, "");		
		    
#if NET_4_0 || SILVERLIGHT_5
            twitter.DirectMessageOperations.SendDirectMessageAsync("habuma", "Really long message")
                .ContinueWith(task =>
                {
                    AssertAggregateException(task.Exception, typeof(OperationNotPermittedException), "There was an error sending your message: The text of your direct message is over 140 characters.");
                })
                .Wait();
#else
            twitter.DirectMessageOperations.SendDirectMessage("habuma", "Really long message");
#endif
        }

	    [Test]
	    [ExpectedException(typeof(NotAuthorizedException), 
            ExpectedMessage = "Authorization is required for the operation, but the API binding was created without authorization.")]
	    public void SendDirectMessaage_ToScreenName_Unauthorized() 
        {
#if NET_4_0 || SILVERLIGHT_5
		    unauthorizedTwitter.DirectMessageOperations.SendDirectMessageAsync("habuma", "Hello there!").Wait();
#else
            unauthorizedTwitter.DirectMessageOperations.SendDirectMessage("habuma", "Hello there!");
#endif
        }

	    [Test]
	    public void SendDirectMessage_ToUserId() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/direct_messages/new.json")
                .AndExpectMethod(HttpMethod.POST)
				.AndExpectBody("user_id=11223&text=Hello+there!")
                .AndRespondWith(JsonResource("Direct_Message"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    DirectMessage message = twitter.DirectMessageOperations.SendDirectMessageAsync(11223, "Hello there!").Result;
#else
            DirectMessage message = twitter.DirectMessageOperations.SendDirectMessage(11223, "Hello there!");
#endif
            AssertSingleDirectMessage(message);
	    }
	
	    [Test]
	    [ExpectedException(typeof(NotAuthorizedException), 
            ExpectedMessage = "Authorization is required for the operation, but the API binding was created without authorization.")]
	    public void SendDirectMessaage_ToUserId_Unauthorized() 
        {
#if NET_4_0 || SILVERLIGHT_5
		    unauthorizedTwitter.DirectMessageOperations.SendDirectMessageAsync(112233, "Hello there!").Wait();
#else
            unauthorizedTwitter.DirectMessageOperations.SendDirectMessage(112233, "Hello there!");
#endif
        }
	
	    [Test]
	    public void DeleteDirectMessage() 
        {
		    mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/direct_messages/destroy/42.json")
				.AndExpectMethod(HttpMethod.DELETE)
				.AndRespondWith(JsonResource("Direct_Message"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    twitter.DirectMessageOperations.DeleteDirectMessageAsync(42).Wait();
#else
            twitter.DirectMessageOperations.DeleteDirectMessage(42);
#endif
        }

        [Test]
        [ExpectedException(typeof(NotAuthorizedException),
            ExpectedMessage = "Authorization is required for the operation, but the API binding was created without authorization.")]
	    public void DeleteDirectMessage_Unauthorized() 
        {
#if NET_4_0 || SILVERLIGHT_5
		    unauthorizedTwitter.DirectMessageOperations.DeleteDirectMessageAsync(42).Wait();
#else
            unauthorizedTwitter.DirectMessageOperations.DeleteDirectMessage(42);
#endif
        }


        // test helpers
        	    
        private void AssertSingleDirectMessage(DirectMessage message) 
        {
		    Assert.AreEqual(23456, message.ID);
		    Assert.AreEqual("Back at ya", message.Text);
		    Assert.AreEqual(14718006, message.Sender.ID);
		    Assert.AreEqual("kdonald", message.Sender.ScreenName);
            Assert.AreEqual(255126476, message.Recipient.ID);
		    Assert.AreEqual("Rclarkson", message.Recipient.ScreenName);
	    }
	
	    private void AssertDirectMessageListContents(IList<DirectMessage> messages) 
        {
		    Assert.AreEqual(2, messages.Count);
		    Assert.AreEqual(12345, messages[0].ID);
		    Assert.AreEqual("Hello there", messages[0].Text);
            Assert.AreEqual(255126476, messages[0].Sender.ID);
		    Assert.AreEqual("Rclarkson", messages[0].Sender.ScreenName);
            Assert.AreEqual(14718006, messages[0].Recipient.ID);
		    Assert.AreEqual("kdonald", messages[0].Recipient.ScreenName);
		    // assertTimelineDateEquals("Tue Jul 13 17:38:21 +0000 2010", messages[0].CreatedAt);
		    Assert.AreEqual(23456, messages[1].ID);
		    Assert.AreEqual("Back at ya", messages[1].Text);
            Assert.AreEqual(14718006, messages[1].Sender.ID);
		    Assert.AreEqual("kdonald", messages[1].Sender.ScreenName);
            Assert.AreEqual(255126476, messages[1].Recipient.ID);
		    Assert.AreEqual("Rclarkson", messages[1].Recipient.ScreenName);
	    }
    }
}
