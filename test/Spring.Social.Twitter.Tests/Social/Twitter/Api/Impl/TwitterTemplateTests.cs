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

using NUnit.Framework;

namespace Spring.Social.Twitter.Api.Impl
{
    /// <summary>
    /// Unit tests for the TwitterTemplate class.
    /// </summary>
    /// <author>Craig Walls</author>
    /// <author>Bruno Baia (.NET)</author>
    [TestFixture]
    public class TwitterTemplateTests
    {
        [Test]
	    public void IsAuthorizedForUser() 
        {
		    TwitterTemplate twitter = new TwitterTemplate("API_KEY", "API_SECRET", "ACCESS_TOKEN", "ACCESS_TOKEN_SECRET");
		    Assert.IsTrue(twitter.IsAuthorized);
	    }

	    [Test]
	    public void IsAuthorizedForUser_NotAuthorized() 
        {
		    TwitterTemplate twitter = new TwitterTemplate();
            Assert.IsFalse(twitter.IsAuthorized);
	    }
    }
}
