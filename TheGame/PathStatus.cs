using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;

public static class PathEvaluator
{
    public const int ScoreFree = 4;
    public const int ScoreUnknown = 1;

    /// <summary>
    /// Use a basic flood fill to work out what direction is least likely to die in
    /// Returns +3 for every free square from that location
    /// Returns +1 for every unknown square accessible from that location
    /// 0 would be a dead end    
    /// </summary>        
    public static int GetScore(Vector2D startingPoint, MapObjects[,] map)
    {
        var score = 0;

        var sizeX = map.GetLength(0);
        var sizeY = map.GetLength(1);

        if(!IsNotBlocked(map[startingPoint.X, startingPoint.Y]))
            return 0;

        // track where we have already tested
        var done = new bool[sizeX, sizeY];

        // indicates points on the map to test for further flood
        var points = new Stack<Vector2D>();

        foreach (var direction in Enum.GetValues(typeof(Direction)).Cast<Direction>())
        {
            var testingPosition = startingPoint + direction.ToVector2d();

            if (testingPosition.X < 0) continue;
            if (testingPosition.X >= sizeX) continue;
            if (testingPosition.Y < 0) continue;
            if (testingPosition.Y >= sizeY) continue;

            if (!IsNotBlocked(map[testingPosition.X,testingPosition.Y]))
                continue;            
            points.Push(testingPosition);
        }

        while (points.Count > 0)
        {
            var point = points.Pop();
            
            // If we already checked this point, skip
            if (done[point.X, point.Y]) continue;
            done[point.X, point.Y] = true;

            foreach (var direction in Enum.GetValues(typeof(Direction)).Cast<Direction>())
            {
                var currentPoint = point + direction.ToVector2d();

                if (currentPoint.X < 0) continue;
                if (currentPoint.X >= sizeX) continue;
                if (currentPoint.Y < 0) continue;
                if (currentPoint.Y >= sizeY) continue;

                if (IsNotBlocked(map[currentPoint.X, currentPoint.Y]))
                {
                    points.Push(currentPoint);
                    score += Score(map[currentPoint.X, currentPoint.Y]);
                }                                
            }
        }
        return score;
    }

    private static bool IsNotBlocked(MapObjects input) =>
        input == MapObjects.FreeSquare ||
        input == MapObjects.Unknown;

    private static int Score(MapObjects input)
    {
        switch (input) {
            case MapObjects.FreeSquare: return ScoreFree;
            case MapObjects.Unknown: return ScoreUnknown;
            default:
                return 0;
        }
    }
}

public enum PathStatus
{
    Untested,
    Unknown,
    Free,
    Blocked
}