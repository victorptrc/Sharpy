namespace Sharpy;
public class Apple : Asset
{
    public int SquareX;
    public int SquareY;
    public Apple(nint texture, int height, int width, int squareX, int squareY) : base(texture, height, width)
    {
        SquareX = squareX;
        SquareY = squareY;
        PositionX = squareX * 40;
        PositionY = squareY * 40;
    }
}