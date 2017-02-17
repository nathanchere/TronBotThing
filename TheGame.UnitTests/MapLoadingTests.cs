using System.Collections.Generic;
using Xunit;

namespace TheGame.UnitTests
{
    public class MapLoadingTestsss
    {
        [Fact]
        public void InitialisesMapBoundariesCorrectly()
        {
            var target = new Competitor(5, 14, 'c');

            Assert.Equal(target.DiscoveredMap[0, 0], MapObjects.Wall);
            Assert.Equal(target.DiscoveredMap[49, 49], MapObjects.Wall);

            Assert.Equal(target.DiscoveredMap[10, 0], MapObjects.Wall);

            Assert.Equal(target.DiscoveredMap[0, 10], MapObjects.Wall);

            Assert.Equal(target.DiscoveredMap[10, 49], MapObjects.Wall);
            Assert.Equal(target.DiscoveredMap[49, 10], MapObjects.Wall);
        }
    }

    public class MapLoadingTests
    {
        [Fact]
        public void InitialisesMapBoundariesCorrectly()
        {
            var target = new Competitor(5, 14, 'c');

            Assert.Equal(target.DiscoveredMap[0, 0], MapObjects.Wall);
            Assert.Equal(target.DiscoveredMap[49, 49], MapObjects.Wall);

            Assert.Equal(target.DiscoveredMap[10, 0], MapObjects.Wall);

            Assert.Equal(target.DiscoveredMap[0, 10], MapObjects.Wall);

            Assert.Equal(target.DiscoveredMap[10, 49], MapObjects.Wall);
            Assert.Equal(target.DiscoveredMap[49, 10], MapObjects.Wall);
        }

        [Fact]
        public void InitialisesMapInsideCorrectly()
        {
            var target = new Competitor(5, 14, 'c');

            Assert.Equal(target.DiscoveredMap[1, 1], MapObjects.Unknown);
            Assert.Equal(target.DiscoveredMap[48, 48], MapObjects.Unknown);

            Assert.Equal(target.DiscoveredMap[1, 48], MapObjects.Unknown);

            Assert.Equal(target.DiscoveredMap[48, 1], MapObjects.Unknown);

            Assert.Equal(target.DiscoveredMap[25, 25], MapObjects.Unknown);
        }

        [Fact]
        public void UpdatesMapAfterTurnCorrectly()
        {
            var target = new Competitor(10, 20, 'c');

            char[][] input = new []{
                new char[] { 'X' , 'X' , 'X' , '-' , 'X' },
                new char[] { '-' , '-' , 'X' , '-' , 'X' },
                new char[] { 'X' , 'X' , 'J' , '-' , 'X' },
                new char[] { '-' , '-' , '-' , '-' , 'X' },
                new char[] { 'X' , 'X' , 'M' , '-' , 'M' },
                };

            var debugx = target.DumpMap();

            var result = target.MakeMove(input);

            Assert.Equal(target.DiscoveredMap[8, 18], MapObjects.Wall);
            Assert.Equal(target.DiscoveredMap[9, 18], MapObjects.FreeSquare);
            Assert.Equal(target.DiscoveredMap[10, 18], MapObjects.Wall);

            Assert.Equal(target.DiscoveredMap[8, 19], MapObjects.Wall);
            Assert.Equal(target.DiscoveredMap[8, 20], MapObjects.Wall);
            Assert.Equal(target.DiscoveredMap[8, 21], MapObjects.FreeSquare);
            Assert.Equal(target.DiscoveredMap[8, 22], MapObjects.Wall);
        }

        [Fact]
        public void ForcedMovesTest()
        {
            var target = new Competitor(30, 10, 'c');

            var results = new List<Move>();

            var debugx = target.DumpMap();

            results.Add(target.MakeMove(new[]{
                new char[] { 'X' , 'X' , 'X' , 'X' , 'X' },
                new char[] { 'X' , '-' , '-' , '-' , '-' },
                new char[] { 'X' , '-' , 'J' , '-' , '-' },
                new char[] { 'X' , '-' , '-' , '-' , 'X' },
                new char[] { 'X' , 'X' , 'X' , 'X' , 'M' },
                }));
            debugx = target.DumpMap();

            results.Add(target.MakeMove(new[]{
                new char[] { 'X' , 'X' , 'X' , '-' , 'X' },
                new char[] { 'X' , '-' , 'X' , '-' , 'X' },
                new char[] { 'X' , 'X' , 'J' , '-' , 'X' },
                new char[] { 'X' , 'X' , '-' , '-' , 'X' },
                new char[] { 'X' , 'X' , '-' , '-' , 'M' },
                }));
            debugx = target.DumpMap();

            results.Add(target.MakeMove(new[]{
                new char[] { 'X' , 'X' , 'X' , '-' , 'X' },
                new char[] { '-' , '-' , 'X' , '-' , 'X' },
                new char[] { '-' , '-' , 'J' , '-' , 'X' },
                new char[] { '-' , '-' , '-' , '-' , 'X' },
                new char[] { 'X' , 'X' , 'M' , '-' , 'M' },
                }));
            debugx = target.DumpMap();

            results.Add(target.MakeMove(new[]{
                new char[] { 'X' , 'X' , '-' , '-' , 'X' },
                new char[] { '-' , '-' , '-' , '-' , 'X' },
                new char[] { 'X' , 'X' , 'J' , '-' , 'X' },
                new char[] { '-' , '-' , '-' , '-' , 'X' },
                new char[] { 'X' , 'X' , 'M' , '-' , 'M' },
                }));
            debugx = target.DumpMap();

            Assert.Equal(results[0].DirectionEnum, Direction.East);
            Assert.Equal(results[1].DirectionEnum, Direction.South);
            Assert.Equal(results[2].DirectionEnum, Direction.West);
            Assert.Equal(results[3].DirectionEnum, Direction.North);
        }

        [Fact]
        public void SpecificTest()
        {
            var target = new Competitor(30, 30, 'c');

            var results = new List<Move>();

            var debugx = target.DumpMap();

            results.Add(target.MakeMove(new[]{
                new char[] { '-' , '-' , '-' , '-' , '-' },
                new char[] { '-' , '-' , '-' , '-' , '-' },
                new char[] { 'j' , 'j' , 'J' , '-' , '-' },
                new char[] { '-' , '-' , '-' , '-' , '-' },
                new char[] { '-' , '-' , '-' , '-' , '-' },
                }));
            debugx = target.DumpMap();
       
        }
    }
}