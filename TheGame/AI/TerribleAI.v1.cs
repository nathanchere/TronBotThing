using System;
using System.Linq;
using System.Text;

public class CompetitorV1 : CompetitorBase
{
    public int MapSizeX;
    public int MapSizeY;

    // How many squares away do we see with each update
    public const int FieldOfVision = 2;

    // How many iterations deep to traverse - this will get ugly quickly
    public const int MaxSearchDepth = 4;

    // State
    public Vector2D PlayerPosition;

    // TODO: track other player positions and confidence / how long ago last seen

    public MapObjects[,] DiscoveredMap;

    public Competitor(int x, int y, char color) : base(x, y, color)
    {
        MapSizeX = 50;
        MapSizeY = 50;
        DiscoveredMap = new MapObjects[MapSizeX, MapSizeY];

        // Initialise entire map as unknown
        for (var i = 0; i < MapSizeX; i++)
            for (var j = 0; j < MapSizeY; j++)
                DiscoveredMap[i, j] = MapObjects.Unknown;

        // Initialise known walls
        for (var i = 0; i < MapSizeX; i++)
        {
            DiscoveredMap[i, 0] = MapObjects.Wall;
            DiscoveredMap[i, MapSizeY - 1] = MapObjects.Wall;
        }
        for (var i = 0; i < MapSizeY; i++)
        {
            DiscoveredMap[0, i] = MapObjects.Wall;
            DiscoveredMap[MapSizeX - 1, i] = MapObjects.Wall;
        }

        // Initialise player starting point - important, otherwise discovered map is useless
        PlayerPosition = new Vector2D(x, y);
    }

    public static Random Random = new Random();

    /// <summary>
    /// Returns 1 for every viable square from that location
    /// 0 would be a dead end
    /// </summary>    
    //public MoveScore GetScore(MoveScore accumulator, MapObjects[][] projectedMap)
    //{
    //    return null;
    //}

    public int GetScore(Direction direction)
    {
        var score = 0;
        var testPosition = PlayerPosition + direction.ToVector2d();

        while (score <= 4 && DiscoveredMap[testPosition.X, testPosition.Y] == MapObjects.FreeSquare)
        {
            score += 1;
            testPosition = testPosition + direction.ToVector2d();
        }
        return score;
    }

    public override Move MakeMove(char[][] visableArea)
    {
        UpdateKnownMap(ParseMap(visableArea));
        var result = new Move();

        var moves = Enum.GetValues(typeof(Direction))
            .Cast<Direction>()
            .Select(d => new { D = d, Score = GetScore(d) })
            .ToList();

        var maxScore = moves.Max(m => m.Score);

        if (maxScore == 0) return new Move { DirectionEnum = Direction.East, Speed = 4 };

        var bestMoves = moves.Where(m => m.Score == maxScore)
            .ToList();

        //if (bestMoves.Count == 1)        
        result = new Move
        {
            DirectionEnum = bestMoves[0].D,
            Speed = bestMoves[0].Score
        };

        // Update player position
        for (int i = 1; i <= result.Speed; i++)
        {
            var newTrail = PlayerPosition + result.DirectionEnum.ToVector2d() * i;
            DiscoveredMap[newTrail.X, newTrail.Y] = MapObjects.Track;
        }
        var speed = 1;// result.Speed;
        PlayerPosition += (result.DirectionEnum.ToVector2d() * speed);

        return result;
    }

    public void UpdateKnownMap(MapObjects[][] currentVisibleMap)
    {
        for (int i = 0-FieldOfVision; i <= FieldOfVision; i++)
        {
            for (int j = 0-FieldOfVision; j <= FieldOfVision; j++)
            {
                var x = PlayerPosition.X + i;
                var y = PlayerPosition.Y + j;

                if (x < 0) continue;
                if (y < 0) continue;
                if (x >= MapSizeX) continue;
                if (y >= MapSizeY) continue;

                DiscoveredMap[x, y] = currentVisibleMap[j + FieldOfVision][i + FieldOfVision];
            }
        }
    }

    public string DumpMap()
    {
        var result = new StringBuilder();
        for (int j = 0; j < MapSizeY; j++)
        {
            for (int i = 0; i < MapSizeX; i++)
            {
                char value = '.';
                switch (DiscoveredMap[i, j])
                {
                    case MapObjects.Crash:
                        value = '*';
                        break;
                    case MapObjects.Wall:
                        value = 'X';
                        break;
                    case MapObjects.FreeSquare:
                        value = ' ';
                        break;
                    case MapObjects.Unknown:
                        value = '?';
                        break;
                    default:
                        value = '~';
                        break;
                }
                result.Append(value);
            }
            result.Append(Environment.NewLine);
        }

        return result.ToString();
    }
}