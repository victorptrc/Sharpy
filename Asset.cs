using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using SDL2;
namespace Sharpy;
public class Asset
{
    public IntPtr Texture { get; set; }
    public int Height { get; set; }
    public int Width { get; set; }
    public int PositionX { get; set; }
    public int PositionY { get; set; }

    public Asset(IntPtr texture, int height, int width, int positionX, int positionY)
    {
        Texture = texture;
        Height = height;
        Width = width;
        PositionX = positionX * 40;
        PositionY = positionY * 40;
    }
    public Asset(IntPtr texture, int positionX, int positionY)
    {
        Texture = texture;
        PositionX = positionX * 40;
        PositionY = positionY * 40;
    }

    //Display at a certain coordinate
    public virtual void DisplayOnRendererAt(IntPtr renderer, int x, int y)
    {
        PositionX = x;
        PositionY = y;
        //Create rectangle where to display
        SDL.SDL_Rect destRect = new SDL.SDL_Rect { x = x, y = y, w = Width, h = Height };
        //Display to the renderer on the rectangle
        SDL.SDL_RenderCopy(renderer, Texture, IntPtr.Zero, ref destRect);
    }
    //Display at position defined in constructor
    public virtual void DisplayOnRenderer(IntPtr renderer)
    {
        //Create rectangle where to display
        SDL.SDL_Rect destRect = new SDL.SDL_Rect { x = PositionX, y = PositionY, w = Width, h = Height };
        //Display to the renderer on the rectangle
        SDL.SDL_RenderCopy(renderer, Texture, IntPtr.Zero, ref destRect);
    }

}