using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using SDL2;
namespace Sharpy;
public abstract class GameObject
{
    public int X;
    public int Y;
    public Texture Texture { get; set; }

    public abstract void Update();
    public virtual void Render(IntPtr renderer)
    {
        Texture.Render(X, Y);
    }


}