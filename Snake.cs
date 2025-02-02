using System.Net.Http.Headers;
using System.Transactions;
using System.Text;
using SDL2;
using Sharpy;

public class Snake
{
    private List<SnakePiece> snakePieces = new List<SnakePiece>();
    public SnakePiece Head;
    public List<SnakePiece.Direction> MovementQueue = new();
    public int timeSinceLastMove;

    public Snake(int startX, int startY)
    {
        Head = new(startX, startY, SnakePiece.Direction.RIGHT, SnakePiece.PieceTypes.Head);
        snakePieces.Add(Head);
        AddPiece();
        AddPiece();
        AddPiece();
        AddPiece();
        AddPiece();
        AddPiece();
        AddPiece();
        AddPiece();
        AddPiece();
        AddPiece();

    }
    public void AddPiece()
    {
        SnakePiece lastPiece = snakePieces[snakePieces.Count - 1];
        if (lastPiece.PieceType != SnakePiece.PieceTypes.Head)
        {
            lastPiece.PieceType = SnakePiece.PieceTypes.Body;
            lastPiece.ChangeTexture(lastPiece.currentDirection);
        }
        int newX = 0;
        int newY = 0;
        switch (lastPiece.currentDirection)
        {
            case SnakePiece.Direction.UP:
                newX = lastPiece.X;
                newY = lastPiece.Y + 40;
                break;
            case SnakePiece.Direction.DOWN:
                newX = lastPiece.X;
                newY = lastPiece.Y - 40;
                break;
            case SnakePiece.Direction.RIGHT:
                newX = lastPiece.X - 40;
                newY = lastPiece.Y;
                break;
            case SnakePiece.Direction.LEFT:
                newX = lastPiece.X + 40;
                newY = lastPiece.Y;
                break;
        }
        SnakePiece newPiece = new(newX, newY, lastPiece.currentDirection, SnakePiece.PieceTypes.Tail);
        snakePieces.Add(newPiece);
    }
    public void Move()
    {
        if (MovementQueue.Count > 0)
        {
            switch (MovementQueue[0])
            {
                case SnakePiece.Direction.UP:
                    Head.currentDirection = SnakePiece.Direction.UP;
                    break;

                case SnakePiece.Direction.DOWN:
                    Head.currentDirection = SnakePiece.Direction.DOWN;
                    break;

                case SnakePiece.Direction.RIGHT:
                    Head.currentDirection = SnakePiece.Direction.RIGHT;
                    break;

                case SnakePiece.Direction.LEFT:
                    Head.currentDirection = SnakePiece.Direction.LEFT;
                    break;
            }
            MovementQueue.RemoveAt(0);

        }
        if (Head.X >= 600)
        {
            Head.X = 0;
        }
        else if (Head.X < 0)
        {
            Head.X = 600;
        }
        else if (Head.Y >= 600)
        {
            Head.Y = 0;
        }
        else if (Head.Y < 0)
        {
            Head.Y = 600;
        }

        for (int i = snakePieces.Count - 1; i > 0; i--)
        {
            snakePieces[i].previousX = snakePieces[i].X;
            snakePieces[i].previousY = snakePieces[i].Y;
            snakePieces[i].targetX = snakePieces[i - 1].X;
            snakePieces[i].targetY = snakePieces[i - 1].Y;
            snakePieces[i].lastDirection = snakePieces[i].currentDirection;
            snakePieces[i].currentDirection = snakePieces[i - 1].currentDirection;
        }

        switch (Head.currentDirection)
        {
            case SnakePiece.Direction.UP:
                Head.targetY -= 40;
                break;
            case SnakePiece.Direction.DOWN:
                Head.targetY += 40;
                break;
            case SnakePiece.Direction.LEFT:
                Head.targetX -= 40;
                break;
            case SnakePiece.Direction.RIGHT:
                Head.targetX += 40;
                break;
            default:
                break;
        }
        snakePieces[0].ChangeTexture(snakePieces[0].currentDirection);


    }
    public void Render(IntPtr renderer)
    {
        foreach (var piece in snakePieces)
        {
            piece.Render(renderer);
        }
    }
    public void PrintMovementQueue()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Movement Queue: ");
        foreach (var direction in MovementQueue)
        {
            sb.Append(direction.ToString() + " ");
        }
        Console.WriteLine(sb.ToString());
    }
    public void ProcessInput(SnakePiece.Direction direction)
    {
        SnakePiece.Direction lastQueueDirection;
        if (MovementQueue.Count > 0)
        {
            lastQueueDirection = MovementQueue[MovementQueue.Count - 1];
        }
        else
        {
            lastQueueDirection = Head.currentDirection;
        }

        if (lastQueueDirection == direction || lastQueueDirection == GetOppositeDirection(direction))
        {
            return;
        }
        else
        {
            MovementQueue.Add(direction);
        }
    }
    public void Update()
    {
        uint timeElapsed = Time.CurrentMoveTime - Time.LastMoveTime;
        if (timeElapsed >= Time.MovementDelay)
        {

            foreach (var piece in snakePieces)
            {
                piece.X = piece.targetX;
                piece.Y = piece.targetY;
                piece.previousX = piece.X;
                piece.previousY = piece.Y;
                piece.ChangeTexture(piece.currentDirection);

            }
            Move();

            Time.ResetMoveTime(); // Update LastMoveTime properly
        }
        else
        {
            // Smooth movement
            float factor = timeElapsed / (float)Time.MovementDelay;
            foreach (var piece in snakePieces)
            {
                piece.X = (int)(piece.previousX + (piece.targetX - piece.previousX) * factor);
                piece.Y = (int)(piece.previousY + (piece.targetY - piece.previousY) * factor);
            }
        }
    }



    public SnakePiece.Direction GetOppositeDirection(SnakePiece.Direction direction)
    {
        switch (direction)
        {
            case SnakePiece.Direction.UP:
                return SnakePiece.Direction.DOWN;
            case SnakePiece.Direction.DOWN:
                return SnakePiece.Direction.UP;
            case SnakePiece.Direction.LEFT:
                return SnakePiece.Direction.RIGHT;
            case SnakePiece.Direction.RIGHT:
                return SnakePiece.Direction.LEFT;
            default:
                throw new ArgumentException("Invalid direction");
        }

    }
}

