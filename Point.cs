namespace everybody.codes_2025;

public class Point(int x, int y)
{
    public readonly int X = x;
    public readonly int Y = y;

    public Point(Point3D point3D):this(point3D.X, point3D.Y) { }

    public override bool Equals(object? obj)
    {
        return obj is Point point && X == point.X && Y == point.Y;
    }

    public bool Equals(Point other)
    {
        return X == other.X && Y == other.Y;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public override string ToString()
    {
        return $"{X},{Y}";
    }
}

public class Point3D(int x, int y, int z): Point(x,y)
{
    public readonly int Z = z;

    public override bool Equals(object? obj)
    {
        return obj is Point3D point && X == point.X && Y == point.Y && Z == point.Z;
    }

    public bool Equals(Point3D? other)
    {
        if(other == null) return false;
        return X == other.X && Y == other.Y && Z == other.Z;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Z);
    }

    public override string ToString()
    {
        return $"{X},{Y},{Z}";
    }
}

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}