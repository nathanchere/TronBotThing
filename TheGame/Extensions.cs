using System;
using System.Collections.Generic;
using System.Linq;

public static class Extensions
{
    public static Vector2D ToVector2d(this Direction direction)
    {
        switch (direction)
        {
            case Direction.East:
                return new Vector2D(1, 0);

            case Direction.West:
                return new Vector2D(-1, 0);

            case Direction.North:
                return new Vector2D(0, -1);

            case Direction.South:
                return new Vector2D(0, 1);

            default:
                throw new Exception();
        }
    }

    public static MapObjects[][] AddTrail(this MapObjects[][] input, Vector2D position)
    {
        var result = new MapObjects[][] { };

        for (var row = 0; row <= 4; row++)
            for (var col = 0; col <= 4; col++)
                result[row][col] = input[row][col];

        result[position.X][position.Y] = MapObjects.Track;

        return result;
    }
}

public class MoveScore
{
    public List<Move> Moves = new List<Move>();
    public int Score = 0;
}