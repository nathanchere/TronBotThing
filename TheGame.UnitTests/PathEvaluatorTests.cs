using System.Linq;
using Xunit;

namespace TheGame.UnitTests
{
    public class PathEvaluatorTests
    {
        [Fact]
        public void CanGetAScore()
        {
            var map = new MapObjects[7, 7];

            map[2, 4] = MapObjects.Wall; map[2, 5] = MapObjects.Wall; map[2, 6] = MapObjects.Wall;

            map[3, 0] = MapObjects.Wall;
            map[3, 1] = MapObjects.Wall;
            map[3, 2] = MapObjects.Wall;
            map[3, 3] = MapObjects.FreeSquare;
            map[3, 4] = MapObjects.Wall;
            map[3, 5] = MapObjects.FreeSquare;

            map[4, 0] = MapObjects.Wall;
            map[4, 1] = MapObjects.FreeSquare;
            map[4, 2] = MapObjects.FreeSquare;
            map[4, 3] = MapObjects.Motorcycle;
            map[4, 4] = MapObjects.FreeSquare;
            map[4, 5] = MapObjects.FreeSquare;

            map[5, 0] = MapObjects.Wall;
            map[5, 1] = MapObjects.FreeSquare;
            map[5, 2] = MapObjects.FreeSquare;
            map[5, 3] = MapObjects.Wall;
            map[5, 4] = MapObjects.FreeSquare;
            map[5, 5] = MapObjects.FreeSquare;

            // sanity check
            Assert.Equal(map[4,3], MapObjects.Motorcycle);

            var result1 = PathEvaluator.GetScore(new Vector2D(4, 2), map);
            var result2 = PathEvaluator.GetScore(new Vector2D(3, 3), map);
            var result3 = PathEvaluator.GetScore(new Vector2D(4, 4), map);
            var result4 = PathEvaluator.GetScore(new Vector2D(5, 3), map);

        }

        public MapObjects[][] ParseMap(char[][] visableArea)
        {
            return visableArea.Select(row => row.Select(CharToMapObject).ToArray()).ToArray();
        }

        public MapObjects CharToMapObject(char mapObject)
        {
            switch (mapObject)
            {
                case 'X':
                    return MapObjects.Wall;
                case '-':
                    return MapObjects.FreeSquare;
                case 'C':
                    return MapObjects.Crash;
                default:
                    return char.IsUpper(mapObject) ? MapObjects.Motorcycle : MapObjects.Track;
            }
        }
    }
}