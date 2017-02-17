using System.Linq;

public abstract class CompetitorBase
{
    public MapObjects[][] _map;
    public CompetitorBase(int x, int y, char color)
    {
    }

    public abstract Move MakeMove(char[][] visableArea);

    public bool CanMoveDirection(Direction direction)
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