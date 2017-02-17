public class Move
{
    public string Direction { get { return DirectionEnum.ToString(); } set { } }

    public Direction DirectionEnum;

    public int Speed { get; set; }

    public static Move South(int speed) => new Move { Direction = "South", Speed = speed };
    public static Move North(int speed) => new Move { Direction = "North", Speed = speed };
    public static Move East(int speed) => new Move { Direction = "East", Speed = speed };
    public static Move West(int speed) => new Move { Direction = "West", Speed = speed };

    public static Move Any(Direction direction, int speed) => new Move { Direction = direction.ToString(), Speed = speed };

    public override string ToString()
    {
        return $"{Direction} x {Speed} ({DirectionEnum.ToVector2d() * Speed})";
    }
}
