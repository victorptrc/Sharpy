using SDL2;
namespace Sharpy;

public class Background
{
    public IntPtr Renderer;
    private int squareSize = 40;
    private int screenWidth = 600;
    private int screenHeight = 600;
    public IntPtr BackgroundTexture;

    public Background(IntPtr renderer)
    {
        Renderer = renderer;
    }
    public void Draw()
    {
        SDL.SDL_RenderCopy(Renderer, BackgroundTexture, IntPtr.Zero, IntPtr.Zero);
    }
    public void Create()
    {
        //Create background texture
        BackgroundTexture = SDL.SDL_CreateTexture(Renderer, SDL.SDL_PIXELFORMAT_RGBA8888, (int)SDL.SDL_TextureAccess.SDL_TEXTUREACCESS_TARGET, screenWidth, screenHeight);
        //Set the target for drawing
        SDL.SDL_SetRenderTarget(Renderer, BackgroundTexture);

        for (int i = 0; i < 15; i++)
        {
            for (int j = 0; j < 15; j++)
            {
                if (i % 2 == 0)
                {
                    if (j % 2 == 0)
                    {
                        SDL.SDL_SetRenderDrawColor(Renderer, 170, 215, 80, 255); // RGB for light green, last one is transperancy
                        SDL.SDL_Rect square = new SDL.SDL_Rect { x = j * squareSize, y = i * squareSize, w = 40, h = 40 };
                        SDL.SDL_RenderFillRect(Renderer, ref square);
                    }
                    else
                    {
                        SDL.SDL_SetRenderDrawColor(Renderer, 162, 209, 72, 255); // RGB for darker green, last one is transperancy
                        SDL.SDL_Rect square = new SDL.SDL_Rect { x = j * squareSize, y = i * squareSize, w = 40, h = 40 };
                        SDL.SDL_RenderFillRect(Renderer, ref square);
                    }
                }
                else
                {
                    if (j % 2 == 0)
                    {
                        SDL.SDL_SetRenderDrawColor(Renderer, 162, 209, 72, 255); // RGB for darker green, last one is transperancy
                        SDL.SDL_Rect square = new SDL.SDL_Rect { x = j * squareSize, y = i * squareSize, w = 40, h = 40 };
                        SDL.SDL_RenderFillRect(Renderer, ref square);
                    }
                    else
                    {
                        SDL.SDL_SetRenderDrawColor(Renderer, 170, 215, 80, 255); // RGB for light green, last one is transperancy
                        SDL.SDL_Rect square = new SDL.SDL_Rect { x = j * squareSize, y = i * squareSize, w = 40, h = 40 };
                        SDL.SDL_RenderFillRect(Renderer, ref square);
                    }
                }
            }
        }
        // Reset the render target back to the default (the screen)
        SDL.SDL_SetRenderTarget(Renderer, IntPtr.Zero);
        // Render the background texture to the screen
        SDL.SDL_RenderCopy(Renderer, BackgroundTexture, IntPtr.Zero, IntPtr.Zero);
    }

    public void DestroyTexture()
    {
        SDL.SDL_DestroyTexture(BackgroundTexture);
    }
}