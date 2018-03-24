using NUnit.Framework;
using showTracker.BusinessLayer.Services;

namespace showTracker.BusinessLayer.Tests.Services
{
    [TestFixture]
    public class JsonSerializeTests
    {        
        [SetUp]
        public void Init()
        {            
        }

        [Test]
        public void SerializeObject_ValidObject_ReturnsValidString()
        {
            //Arrange
            var simpleObj = new
            {
                Name = "TestName",
                Array = new[]
                {
                    "One",
                    "Two"
                }
            };
            var jsonSerializeService = new JsonSerializeService();
            var expectedString = "{\"Name\":\"TestName\",\"Array\":[\"One\",\"Two\"]}";

            //Act
            var objString = jsonSerializeService.SerializeObject(simpleObj);

            //Assert
            Assert.AreEqual(expectedString, objString);
        }
    }
}
