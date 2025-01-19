using SDL2;
namespace Sharpy;

public class Background
{
    public IntPtr Renderer;
    private int squareSize = 40;

    public Background(IntPtr renderer)
    {
        Renderer = renderer;
    }

    public void Draw()
    {
        // 170, 215, 80 light green
        // 162, 209, 72 darker green
        SDL.SDL_RenderClear(Renderer); // Clear entire screen
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
    }
}