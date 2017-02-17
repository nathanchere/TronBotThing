public struct Vector2D
{
    public Vector2D(int x, int y)
    {
        X = x;
        Y = y;
    }
    public int X;
    public int Y;

    public static Vector2D operator +(Vector2D a, Vector2D b)
    {
        return new Vector2D(a.X + b.X, a.Y + b.Y);
    }

    public static Vector2D operator *(Vector2D a, int b)
    {
        return new Vector2D(a.X * b, a.Y * b);
    }

    public override string ToString()
    {
        return $"X:{X} Y:{Y}";
    }
}