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
    /// Unit tests for the GeoTemplate class.
    /// </summary>
    /// <author>Craig Walls</author>
    /// <author>Bruno Baia (.NET)</author>
    [TestFixture]
    public class GeoTemplateTests : AbstractTwitterOperationsTests
    {
        [Test]
        public void GetPlace()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/geo/id/0bba15b36bd9e8cc.json")
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("Geo_Place"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            Place place = twitter.GeoOperations.GetPlaceAsync("0bba15b36bd9e8cc").Result;
#else
            Place place = twitter.GeoOperations.GetPlace("0bba15b36bd9e8cc");
#endif
            AssertPlace(place);
        }

        [Test]
        public void ReverseGeoCode_PointOnly()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/geo/reverse_geocode.json?lat=33.050278&long=-96.745833")
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("Places_List"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Place> places = twitter.GeoOperations.ReverseGeoCodeAsync(33.050278, -96.745833).Result;
#else
            IList<Place> places = twitter.GeoOperations.ReverseGeoCode(33.050278, -96.745833);
#endif
            AssertPlaces(places);
        }

        [Test]
        public void ReverseGeoCode_PointAndGranularity()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/geo/reverse_geocode.json?lat=33.050278&long=-96.745833&granularity=city")
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("Places_List"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Place> places = twitter.GeoOperations.ReverseGeoCodeAsync(33.050278, -96.745833, PlaceType.City, null).Result;
#else
            IList<Place> places = twitter.GeoOperations.ReverseGeoCode(33.050278, -96.745833, PlaceType.City, null);
#endif
            AssertPlaces(places);
        }

        [Test]
        public void ReverseGeoCode_PointAndPOIGranularity()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/geo/reverse_geocode.json?lat=33.050278&long=-96.745833&granularity=poi")
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("Places_List"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Place> places = twitter.GeoOperations.ReverseGeoCodeAsync(33.050278, -96.745833, PlaceType.POI, null).Result;
#else
            IList<Place> places = twitter.GeoOperations.ReverseGeoCode(33.050278, -96.745833, PlaceType.POI, null);
#endif
            AssertPlaces(places);
        }

        [Test]
        public void ReverseGeoCode_PointGranularityAndAccuracy()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/geo/reverse_geocode.json?lat=33.050278&long=-96.745833&granularity=city&accuracy=5280ft")
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("Places_List"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Place> places = twitter.GeoOperations.ReverseGeoCodeAsync(33.050278, -96.745833, PlaceType.City, "5280ft").Result;
#else
            IList<Place> places = twitter.GeoOperations.ReverseGeoCode(33.050278, -96.745833, PlaceType.City, "5280ft");
#endif
            AssertPlaces(places);
        }

        [Test]
        public void ReverseGeoCode_PointAndAccuracy()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/geo/reverse_geocode.json?lat=33.050278&long=-96.745833&accuracy=5280ft")
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("Places_List"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Place> places = twitter.GeoOperations.ReverseGeoCodeAsync(33.050278, -96.745833, null, "5280ft").Result;
#else
            IList<Place> places = twitter.GeoOperations.ReverseGeoCode(33.050278, -96.745833, null, "5280ft");
#endif
            AssertPlaces(places);
        }

        [Test]
        public void Search_PointOnly()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/geo/search.json?lat=33.050278&long=-96.745833")
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("Places_List"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Place> places = twitter.GeoOperations.SearchAsync(33.050278, -96.745833).Result;
#else
            IList<Place> places = twitter.GeoOperations.Search(33.050278, -96.745833);
#endif
            AssertPlaces(places);
        }

        [Test]
        public void Search_PointAndGranularity()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/geo/search.json?lat=33.050278&long=-96.745833&granularity=city")
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("Places_List"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Place> places = twitter.GeoOperations.SearchAsync(33.050278, -96.745833, PlaceType.City, null, null).Result;
#else
            IList<Place> places = twitter.GeoOperations.Search(33.050278, -96.745833, PlaceType.City, null, null);
#endif
            AssertPlaces(places);
        }

        [Test]
        public void Search_PointAndPOIGranularity()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/geo/search.json?lat=33.050278&long=-96.745833&granularity=poi")
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("Places_List"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Place> places = twitter.GeoOperations.SearchAsync(33.050278, -96.745833, PlaceType.POI, null, null).Result;
#else
            IList<Place> places = twitter.GeoOperations.Search(33.050278, -96.745833, PlaceType.POI, null, null);
#endif
            AssertPlaces(places);
        }

        [Test]
        public void Search_PointGranularityAndAccuracy()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/geo/search.json?lat=33.050278&long=-96.745833&granularity=city&accuracy=5280ft")
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("Places_List"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Place> places = twitter.GeoOperations.SearchAsync(33.050278, -96.745833, PlaceType.City, "5280ft", null).Result;
#else
            IList<Place> places = twitter.GeoOperations.Search(33.050278, -96.745833, PlaceType.City, "5280ft", null);
#endif
            AssertPlaces(places);
        }

        [Test]
        public void Search_PointAndAccuracy()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/geo/search.json?lat=33.050278&long=-96.745833&accuracy=5280ft")
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("Places_List"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Place> places = twitter.GeoOperations.SearchAsync(33.050278, -96.745833, null, "5280ft", null).Result;
#else
            IList<Place> places = twitter.GeoOperations.Search(33.050278, -96.745833, null, "5280ft", null);
#endif
            AssertPlaces(places);
        }

        [Test]
        public void Search_PointGranularityAccuracyAndQuery()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/geo/search.json?lat=33.050278&long=-96.745833&granularity=city&accuracy=5280ft&query=Public%20School")
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("Places_List"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            IList<Place> places = twitter.GeoOperations.SearchAsync(33.050278, -96.745833, PlaceType.City, "5280ft", "Public School").Result;
#else
            IList<Place> places = twitter.GeoOperations.Search(33.050278, -96.745833, PlaceType.City, "5280ft", "Public School");
#endif
            AssertPlaces(places);
        }

        [Test]
        public void FindSimilarPlaces()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/geo/similar_places.json?lat=37.7821120598956&long=-122.400612831116&name=Twitter%20HQ&attribute%3Astreet_address=795%20Folsom%20St&contained_within=2e056b6d9c0ff3cd")
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("Similar_Places"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            SimilarPlaces similarPlaces = twitter.GeoOperations.FindSimilarPlacesAsync(37.7821120598956, -122.400612831116, "Twitter HQ", "795 Folsom St", "2e056b6d9c0ff3cd").Result;
#else
            SimilarPlaces similarPlaces = twitter.GeoOperations.FindSimilarPlaces(37.7821120598956, -122.400612831116, "Twitter HQ", "795 Folsom St", "2e056b6d9c0ff3cd");
#endif
            Assert.AreEqual("9c8072b2a6788ee530e8c8cbb487107c", similarPlaces.PlacePrototype.CreateToken);
            Assert.AreEqual(37.7821120598956, similarPlaces.PlacePrototype.Latitude, 0.0000001);
            Assert.AreEqual(-122.400612831116, similarPlaces.PlacePrototype.Longitude, 0.0000001);
            Assert.AreEqual("Twitter HQ", similarPlaces.PlacePrototype.Name);
            Assert.AreEqual("2e056b6d9c0ff3cd", similarPlaces.PlacePrototype.ContainedWithin);
            Assert.AreEqual(2, similarPlaces.Count);
            Assert.AreEqual("851a1ba23cb8c6ee", similarPlaces[0].ID);
            Assert.AreEqual("twitter", similarPlaces[0].Name);
            Assert.AreEqual("twitter, Twitter HQ", similarPlaces[0].FullName);
            Assert.IsNull(similarPlaces[0].StreetAddress);
            Assert.AreEqual("United States", similarPlaces[0].Country);
            Assert.AreEqual("US", similarPlaces[0].CountryCode);
            Assert.AreEqual(PlaceType.POI, similarPlaces[0].PlaceType);
            Assert.AreEqual("247f43d441defc03", similarPlaces[1].ID);
            Assert.AreEqual("Twitter HQ", similarPlaces[1].Name);
            Assert.AreEqual("Twitter HQ, San Francisco", similarPlaces[1].FullName);
            Assert.AreEqual("795 Folsom St", similarPlaces[1].StreetAddress);
            Assert.AreEqual("United States", similarPlaces[1].Country);
            Assert.AreEqual("US", similarPlaces[1].CountryCode);
            Assert.AreEqual(PlaceType.POI, similarPlaces[1].PlaceType);
        }

        [Test]
        public void CreatePlace()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://api.twitter.com/1/geo/place.json")
                .AndExpectMethod(HttpMethod.POST)
                .AndExpectBody("lat=33.153661&long=-94.973045&name=Restaurant+Mexico&attribute%3Astreet_address=301+W+Ferguson+Rd&contained_within=2e056b6d9c0ff3cd&token=0b699bfda6514e84c7b69cf993c0c23e")
                .AndRespondWith(JsonResource("Geo_Place"), responseHeaders);

            PlacePrototype placePrototype = new PlacePrototype()
                {
                    CreateToken = "0b699bfda6514e84c7b69cf993c0c23e", 
                    Latitude = 33.153661, 
                    Longitude = -94.973045, 
                    Name = "Restaurant Mexico", 
                    StreetAddress = "301 W Ferguson Rd",
                    ContainedWithin = "2e056b6d9c0ff3cd"
                };                
#if NET_4_0 || SILVERLIGHT_5
            Place place = twitter.GeoOperations.CreatePlaceAsync(placePrototype).Result;
#else
            Place place = twitter.GeoOperations.CreatePlace(placePrototype);
#endif
            AssertPlace(place);
        }


        // test helpers

        private void AssertPlace(Place place)
        {
            Assert.AreEqual("0bba15b36bd9e8cc", place.ID);
            Assert.AreEqual("Restaurant Mexico", place.Name);
            Assert.AreEqual("Restaurant Mexico, Mount Pleasant", place.FullName);
            Assert.AreEqual("301 W Ferguson Rd", place.StreetAddress);
            Assert.AreEqual("United States", place.Country);
            Assert.AreEqual("US", place.CountryCode);
            Assert.AreEqual(PlaceType.POI, place.PlaceType);
        }

        private void AssertPlaces(IList<Place> places)
        {
            Assert.AreEqual(3, places.Count);
            Assert.AreEqual("488da0de4c92ac8e", places[0].ID);
            Assert.AreEqual("Plano", places[0].Name);
            Assert.AreEqual("Plano, TX", places[0].FullName);
            Assert.IsNull(places[0].StreetAddress);
            Assert.AreEqual("United States", places[0].Country);
            Assert.AreEqual("US", places[0].CountryCode);
            Assert.AreEqual(PlaceType.City, places[0].PlaceType);
            Assert.AreEqual("e0060cda70f5f341", places[1].ID);
            Assert.AreEqual("Texas", places[1].Name);
            Assert.IsNull(places[1].StreetAddress);
            Assert.AreEqual("Texas, US", places[1].FullName);
            Assert.AreEqual("United States", places[1].Country);
            Assert.AreEqual("US", places[1].CountryCode);
            Assert.AreEqual(PlaceType.Admin, places[1].PlaceType);
            Assert.AreEqual("96683cc9126741d1", places[2].ID);
            Assert.AreEqual("United States", places[2].Name);
            Assert.IsNull(places[2].StreetAddress);
            Assert.AreEqual("United States", places[2].FullName);
            Assert.AreEqual("United States", places[2].Country);
            Assert.AreEqual("US", places[2].CountryCode);
            Assert.AreEqual(PlaceType.Country, places[2].PlaceType);
        }
    }
}
