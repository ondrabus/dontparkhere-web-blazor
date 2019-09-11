using AspNetMonsters.Blazor.Geolocation;
using KenticoCloudModels;
using NUnit.Framework;
using System;

namespace DontParkHere.Tests
{
    [TestFixture]
    public class AreaTests
    {
        // Brno inner city
        private const string TEST_AREA = "[[49.198146, 16.603004], [49.190709, 16.605043], [49.189842, 16.608921], [49.191709, 16.613514], [49.195143, 16.614945], [49.197712, 16.612802], [49.200147, 16.607853], [49.198146, 16.603004]]";

        [Test]
        public void GetPolygon_InputValidArray_ReturnLocationsList()
        {
            var data = new Area
            {
                AreaData = TEST_AREA
            };

            var list = data.GetPolygon();
            Assert.AreEqual(8, list.Count);
            Assert.AreEqual((decimal)49.198146, list[0].Latitude);
            Assert.AreEqual((decimal)16.603004, list[0].Longitude);
        }

        [Test]
        public void GetPolygon_InputInvalid_ThrowException()
        {
            var data = new Area
            {
                AreaData = "Hello world"
            };

            Assert.Throws<System.Text.Json.JsonException>(() => data.GetPolygon());
        }

        [Test]
        public void GetPolygon_InputValidArrayLong_ReturnLocationsList()
        {
            var data = new Area
            {
                AreaData = "[[49.19727035543781,16.19727035543781]]"
            };

            var list = data.GetPolygon();
            Assert.AreEqual((decimal)49.19727035543781, list[0].Latitude);
            Assert.AreEqual((decimal)16.19727035543781, list[0].Longitude);
        }

        [Test]
        public void GetPolygon_InputValidStringArray_ReturnLocationsList()
        {
            var data = new Area
            {
                AreaData = "[[\"49.19727035543781\",\"16.19727035543781\"]]"
            };

            var list = data.GetPolygon();
            Assert.AreEqual((decimal)49.19727035543781, list[0].Latitude);
            Assert.AreEqual((decimal)16.19727035543781, list[0].Longitude);
        }

        [Test]
        public void IsPointInside_InputInside_ReturnTrue()
        {
            var data = new Area
            {
                AreaData = TEST_AREA
            };
            var point = new Location
            {
                Latitude = (decimal)49.195211,
                Longitude = (decimal)16.609586
            };

            Assert.IsTrue(data.IsPointInside(point));
        }

        [Test]
        public void IsPointInside_InputOutside_ReturnFalse()
        {
            var data = new Area
            {
                AreaData = TEST_AREA
            };
            var point = new Location
            {
                Latitude = (decimal)16.597084,
                Longitude = (decimal)49.201415
            };

            Assert.IsFalse(data.IsPointInside(point));
        }
    }
}
