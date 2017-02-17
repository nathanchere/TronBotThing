using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TheGame.UnitTests
{
    public class Vector2DTests
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(-4, 0)]
        [InlineData(0, -4)]
        [InlineData(4, 0)]
        [InlineData(0, 4)]
        public void AddVectorWorks(int x, int y)
        {
            var target = new Vector2D(x, y);
            var vector = new Vector2D(2, 2);

            var result = vector + target;
            Assert.Equal(result.X, 2 + x);
            Assert.Equal(result.Y, 2 + y);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void MultiplyVectorBySpeedWorks(int speed)
        {
            var vector = new Vector2D(-1, 0);

            var result = vector * speed;
            Assert.Equal(result.X, vector.X * speed);
            Assert.Equal(result.Y, vector.Y * speed);
        }
    }
}
