namespace Sharpy;
public class SnakePiece : GameObject
{
    public enum Direction { UP, DOWN, LEFT, RIGHT }
    public enum PieceTypes { Head, Body, Tail }
    public PieceTypes PieceType;
    public Direction currentDirection;
    public Direction lastDirection;
    public SnakePiece(Direction direction, PieceTypes type)
    {
        PieceType = type;
        currentDirection = direction;
        ChangeTexture(direction);
    }
    public SnakePiece(int startX, int startY, Direction direction, PieceTypes type)
    {
        X = startX;
        Y = startY;
        PieceType = type;
        currentDirection = direction;
        lastDirection = currentDirection;
        ChangeTexture(direction);
    }
    public void ChangeTexture(Direction direction)
    {
        switch (PieceType)
        {
            case PieceTypes.Body:
                if (currentDirection == lastDirection)
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
                else
                {
                    if ((lastDirection == Direction.RIGHT && currentDirection == Direction.UP) || (lastDirection == Direction.DOWN && currentDirection == Direction.LEFT))
                    {
                        Texture = Game.Textures["body_topleft"];

                    }
                    else if ((lastDirection == Direction.LEFT && currentDirection == Direction.UP) || (lastDirection == Direction.DOWN && currentDirection == Direction.RIGHT))
                    {
                        Texture = Game.Textures["body_topright"];

                    }
                    else if ((lastDirection == Direction.UP && currentDirection == Direction.RIGHT) || (lastDirection == Direction.LEFT && currentDirection == Direction.DOWN))
                    {
                        Texture = Game.Textures["body_bottomright"];

                    }
                    else if ((lastDirection == Direction.UP && currentDirection == Direction.LEFT) || (lastDirection == Direction.RIGHT && currentDirection == Direction.DOWN))
                    {
                        Texture = Game.Textures["body_bottomleft"];

                    }
                }
                break;
            case PieceTypes.Head:
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
                    default:
                        break;
                }
                break;
            case PieceTypes.Tail:
                switch (direction)
                {
                    case Direction.UP:
                        Texture = Game.Textures["tail_down"];
                        break;
                    case Direction.DOWN:
                        Texture = Game.Textures["tail_up"];
                        break;
                    case Direction.LEFT:
                        Texture = Game.Textures["tail_right"];
                        break;
                    case Direction.RIGHT:
                        Texture = Game.Textures["tail_left"];
                        break;
                    default:
                        break;
                }
                break;
        }
    }
    public override void Update()
    {
    }
}
