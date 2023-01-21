using System.Diagnostics.CodeAnalysis;

namespace BattleshipsKata
{
    public readonly struct Coordinate : IEquatable<Coordinate>
    {
        public int X { get; } = 0;
        public int Y { get; } = 0;        

        public Coordinate(int x, int y) 
        { 
            X= x;
            Y= y;
        }
        
        public static Coordinate? ConvertStringToValidCoord(string? stringCoord)
        {
            if (string.IsNullOrEmpty(stringCoord))
            {
                return null;
            }

            var array = stringCoord.Split(',');
           
            if (!int.TryParse(array[0], out int x))
            {
                return null;
            }

            if (!int.TryParse(array[1], out int y))
            {
                return null;
            }            

            return new Coordinate(x, y);
        }

        public override bool Equals(object? obj)
        {
            return obj is Coordinate coordinate && Equals(coordinate);
        }

        public bool Equals(Coordinate other)
        {
            return X == other.X &&
                   Y == other.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public static bool operator ==(Coordinate left, Coordinate right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Coordinate left, Coordinate right)
        {
            return !(left == right);
        }
    }
}
