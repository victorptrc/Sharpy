namespace Sharpy;
public class Apple : GameObject
{
    public int SquareX;
    public int SquareY;

    public Apple()
    {
        Random random = new Random();
        X = random.Next(0, 15) * 40;
        Y = random.Next(0, 15) * 40;
        Texture = Game.Textures["apple"];
    }

    public override void Update()
    {

    }
    public void Regenerate()
    {
        Random random = new Random();
        X = random.Next(0, 15) * 40;
        Y = random.Next(0, 15) * 40;
    }
}