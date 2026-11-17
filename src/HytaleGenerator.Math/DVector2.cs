namespace HytaleGenerator.Math;

/// <summary>
/// A two-dimensional <see langword="double"/> vector.
/// </summary>
public struct DVector2
{
    public double X;
    public double Y;

    public DVector2(double x, double y) { X = x; Y = y; }

    public static DVector2 Abs(DVector2 a) => new(double.Abs(a.X), double.Abs(a.Y));
    public static DVector2 Min(DVector2 a, DVector2 b) => new(double.Min(a.X, b.X), double.Min(a.Y, b.Y));
    public static DVector2 Max(DVector2 a, DVector2 b) => new(double.Max(a.X, b.X), double.Max(a.Y, b.Y));

    // Vector assignment ops.
    public void operator += (DVector2 b) { X += b.X; Y += b.Y; }
    public void operator -= (DVector2 b) { X -= b.X; Y -= b.Y; }
    public void operator *= (DVector2 b) { X *= b.X; Y *= b.Y; }
    public void operator /= (DVector2 b) { X /= b.X; Y /= b.Y; }
    public void operator %= (DVector2 b) { X %= b.X; Y %= b.Y; }

    // Scalar assignment ops.
    public void operator += (double b) { X += b; Y += b; }
    public void operator -= (double b) { X -= b; Y -= b; }
    public void operator *= (double b) { X *= b; Y *= b; }
    public void operator /= (double b) { X /= b; Y /= b; }
    public void operator %= (double b) { X %= b; Y %= b; }

    // Vector+Vector binary ops.
    public static DVector2 operator +(DVector2 a, DVector2 b) => new(a.X + b.X, a.Y + b.Y);
    public static DVector2 operator -(DVector2 a, DVector2 b) => new(a.X - b.X, a.Y - b.Y);
    public static DVector2 operator *(DVector2 a, DVector2 b) => new(a.X * b.X, a.Y * b.Y);
    public static DVector2 operator /(DVector2 a, DVector2 b) => new(a.X / b.X, a.Y / b.Y);
    public static DVector2 operator %(DVector2 a, DVector2 b) => new(a.X % b.X, a.Y % b.Y);

    // Vector+Scalar binary ops.
    public static DVector2 operator +(DVector2 a, double b) => new(a.X + b, a.Y + b);
    public static DVector2 operator -(DVector2 a, double b) => new(a.X - b, a.Y - b);
    public static DVector2 operator *(DVector2 a, double b) => new(a.X * b, a.Y * b);
    public static DVector2 operator /(DVector2 a, double b) => new(a.X / b, a.Y / b);
    public static DVector2 operator %(DVector2 a, double b) => new(a.X % b, a.Y % b);

    // Scalar+Vector binary ops.
    public static DVector2 operator +(double a, DVector2 b) => new(a + b.X, a + b.Y);
    public static DVector2 operator -(double a, DVector2 b) => new(a - b.X, a - b.Y);
    public static DVector2 operator *(double a, DVector2 b) => new(a * b.X, a * b.Y);
    public static DVector2 operator /(double a, DVector2 b) => new(a / b.X, a / b.Y);
    public static DVector2 operator %(double a, DVector2 b) => new(a % b.X, a % b.Y);
}