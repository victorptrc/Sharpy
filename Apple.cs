namespace Sharpy;
public class Apple : GameObject
{


    public Apple()
    {
        Random random = new Random();
        Texture = Game.Textures["apple"];
        X = random.Next(0, 16) * 40;
        Y = random.Next(0, 16) * 40;
    }

    public override void Update()
    {

    }
    public void Regenerate()
    {
        Random random = new Random();
        X = random.Next(0, 16) * 40;
        Y = random.Next(0, 16) * 40;
    }
}