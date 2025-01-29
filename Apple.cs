namespace Sharpy;
public class Apple : GameObject
{
    public int SquareX;
    public int SquareY;
    public Apple(nint texture, int height, int width, int squareX, int squareY)
    {
        SquareX = squareX;
        SquareY = squareY;
    }

    public override void Update()
    {

    }
}