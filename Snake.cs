using SDL2;
using Sharpy;

public class Snake
{
    private List<SnakePiece> snakePieces = new List<SnakePiece>();
    private SnakeHead Head;

    public Snake(int x, int y)
    {
        Head = new(Game.Textures["head_up"], x, y, SnakePiece.Direction.LEFT);
        snakePieces.Add(Head);
        AddPiece();
    }
    public void AddPiece()
    {
        SnakePiece lastPiece = snakePieces[snakePieces.Count - 1];
        if (lastPiece is not SnakeHead)
        {
            lastPiece.ChangeTexture(lastPiece.MoveDirection);
        }
        IntPtr texture = IntPtr.Zero;
        switch (lastPiece.MoveDirection)
        {
            case SnakePiece.Direction.UP:
                texture = Game.Textures["tail_up"];
                break;
            case SnakePiece.Direction.DOWN:
                texture = Game.Textures["tail_down"];
                break;
            case SnakePiece.Direction.LEFT:
                texture = Game.Textures["tail_left"];
                break;
            case SnakePiece.Direction.RIGHT:
                texture = Game.Textures["tail_right"];
                break;
            default:
                break;
        }

        SnakePiece newPiece = new(texture, 1, 1, lastPiece.MoveDirection);
        snakePieces.Add(newPiece);
    }
    public void Move()
    {
        for (int i = snakePieces.Count - 1; i > 0; i--)
        {
            snakePieces[i].PositionX = snakePieces[i - 1].PositionX;
            snakePieces[i].PositionY = snakePieces[i - 1].PositionY;
            snakePieces[i].MoveDirection = snakePieces[i - 1].MoveDirection;
        }
        switch (Head.MoveDirection)
        {
            case SnakePiece.Direction.UP:
                Head.PositionY -= 40;
                break;
            case SnakePiece.Direction.DOWN:
                Head.PositionY += 40;
                break;
            case SnakePiece.Direction.LEFT:
                Head.PositionX -= 40;
                break;
            case SnakePiece.Direction.RIGHT:
                Head.PositionX += 40;
                break;
            default:
                break;
        }
    }
    public void Render(IntPtr renderer)
    {
        foreach (var piece in snakePieces)
        {
            Thread.Sleep(1000);
            Console.WriteLine("Rendering snake piece at position: (" + piece.PositionX + ", " + piece.PositionY + ")");
            piece.DisplayOnRenderer(renderer);
        }
    }
}

public class SnakePiece : Asset
{
    public enum Direction { UP, DOWN, LEFT, RIGHT }
    public Direction MoveDirection;
    public SnakePiece(nint texture, int positionX, int positionY, Direction direction) : base(texture, positionX, positionY)
    {
        MoveDirection = direction;
        Height = 40;
        Width = 40;

    }
    public virtual void ChangeTexture(Direction direction)
    {
        switch (direction)
        {
            case Direction.UP:
                Texture = Game.Textures["body_vertical"];
                break;
            case Direction.DOWN:
                Texture = Game.Textures["body_vertical"];
                break;
            case Direction.LEFT:
                Texture = Game.Textures["body_horizontal"];
                break;
            case Direction.RIGHT:
                Texture = Game.Textures["body_horizontal"];
                break;
            default:
                break;
        }
    }
}

public class SnakeHead : SnakePiece
{

    public SnakeHead(nint texture, int positionX, int positionY, Direction direction) : base(texture, positionX, positionY, direction)
    {
        switch (direction)
        {
            case Direction.UP:
                Texture = Game.Textures["head_up"];
                break;
            case Direction.DOWN:
                Texture = Game.Textures["head_down"];
                break;
            case Direction.LEFT:
                Texture = Game.Textures["head_left"];
                break;
            case Direction.RIGHT:
                Texture = Game.Textures["head_right"];
                break;
        }
    }

}