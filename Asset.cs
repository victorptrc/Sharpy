using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using SDL2;
namespace Sharpy;
public class Asset
{
    public IntPtr Texture { get; }
    public int Height { get; }
    public int Width { get; }
    public int PositionX { get; protected set; }
    public int PositionY { get; protected set; }

    public Asset(IntPtr texture, int height, int width, int positionX, int positionY)
    {
        Texture = texture;
        Height = height;
        Width = width;
        PositionX = positionX;
        PositionY = positionY;
    }

    public Asset(IntPtr texture, int height, int width)
    {
        Texture = texture;
        Height = height;
        Width = width;
    }
    //Display at a certain coordinate
    public void DisplayOnRendererAt(IntPtr renderer, int x, int y)
    {
        PositionX = x;
        PositionY = y;
        //Create rectangle where to display
        SDL.SDL_Rect destRect = new SDL.SDL_Rect { x = x, y = y, w = Width, h = Height };
        //Display to the renderer on the rectangle
        SDL.SDL_RenderCopy(renderer, Texture, IntPtr.Zero, ref destRect);
    }
    //Display at position defined in constructor
    public void DisplayOnRenderer(IntPtr renderer)
    {
        //Create rectangle where to display
        SDL.SDL_Rect destRect = new SDL.SDL_Rect { x = PositionX, y = PositionY, w = Width, h = Height };
        //Display to the renderer on the rectangle
        SDL.SDL_RenderCopy(renderer, Texture, IntPtr.Zero, ref destRect);
    }

}