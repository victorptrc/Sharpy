namespace Sharpy;
public class Apple : GameObject
{
    public int SquareX;
    public int SquareY;
    private Snake snakeRef;

    public Apple(Snake snake)
    {
        snakeRef = snake;
        Regenerate();
        Texture = Game.Textures["apple"];
    }

    public override void Update()
    {

    }
    public void Regenerate()
    {
        Random random = new Random();
        X = 20 + random.Next(0, 15) * 40;
        Y = 80 + random.Next(0, 15) * 40;
        foreach (var piece in snakeRef.snakePieces)
        {
            if (piece.X == X && piece.Y == Y)
            {
                Regenerate();
            }

        }
    }
}