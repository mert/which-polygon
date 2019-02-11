using System;
using Lib;
using Xunit;

namespace polygon_test
{
    public class UnitTestScenario
    {
        [Fact]
        public void Failing_Unknown()
        {
            var result = WhichPolygon.Find(0, 1);
            Assert.Null(result);
        }
        
        [Theory]
        [InlineData(39.9026, 38.7670, "Erzincan")]
        public void Passing_City_Erzincan(double x, double y, string z)
        {
            var result = WhichPolygon.Find(x, y);
            Assert.NotNull(result);
            Assert.Equal(z, result);
        }
        
        [Theory]
        [InlineData(39.9026, 38.7670, "İstanbul")]
        [InlineData(39.3513, 33.8928, "Erzincan")]
        public void Failing_City_Erzincan(double x, double y, string z)
        {
            var result = WhichPolygon.Find(x, y);
            Assert.NotNull(result);
            Assert.NotEqual(z, result);
        }
    }
}
