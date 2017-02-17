using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TerribleAI
{
    public class Competitor
    {
        private MapObjects[][] _map;
        public Competitor(int x, int y, char color)
        {
        }


        public Move MakeMove(char[][] visableArea)
        { 
            _map = ParseMap(visableArea);

            // Try to make a legal move
            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                if (CanMoveDirection(direction))
                {
                    return new Move {Direction = direction.ToString(), Speed = 1};
                }
            }
            // If we can't make a legal move we will just go all out!
            return  new Move {Direction = "South", Speed = 4};
        }

        private bool CanMoveDirection(Direction direction)
        {
            var x = 2;
            var y = 2;
            switch (direction)
            {
                case Direction.West:
                     x -= 1;
                    break;
                case Direction.East:
                    x += 1;
                    break;
                case Direction.North:
                    y -= 1;
                    break;
                case Direction.South:
                    y += 1;
                    break;
            }
            return _map[y][x] == MapObjects.FreeSquare;
        }

        private MapObjects[][] ParseMap(char[][] visableArea)
        {
            return visableArea.Select(row => row.Select(CharToMapObject).ToArray()).ToArray();
        }

        private MapObjects CharToMapObject(char mapObject)
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

        enum MapObjects
        {
            FreeSquare,
            Wall,
            Crash,
            Motorcycle,
            Track
        }
    }


    public class Move
    {
        public string Direction { get; set; }
        public int Speed { get; set; }
    }

    internal enum Direction
    {
        West,
        East,
        North,
        South
    }
}

