using SDL2;
namespace Sharpy;

public class Texture
{
    private IntPtr Surface;
    private IntPtr Renderer;
    public IntPtr TexturePtr;
    private string Path = string.Empty;

    private IntPtr Font;

    public Texture(IntPtr renderer, string path)
    {
        Renderer = renderer;
        Path = path;
        LoadFromFile();

    }
    public Texture(IntPtr renderer, IntPtr font)
    {
        Renderer = renderer;
        Font = font;
        Path = string.Empty;
    }
    public void LoadFromText(string text, SDL.SDL_Color color)
    {
        if (Font == IntPtr.Zero)
        {
            throw new Exception("Font not initialized.");
        }

        // Ensure SDL_ttf is initialized
        if (SDL_ttf.TTF_WasInit() == 0)
        {
            throw new Exception("SDL_ttf not initialized.");
        }
        Surface = SDL_ttf.TTF_RenderText_Solid(Font, text, color);
        if (Surface == IntPtr.Zero)
        {
            Console.WriteLine("Failed to load texture: " + SDL_image.IMG_GetError());
            return;
        }

        TexturePtr = SDL.SDL_CreateTextureFromSurface(Renderer, Surface);
        if (TexturePtr == IntPtr.Zero)
        {
            Console.WriteLine("Unable to create texture from rendered text! SDL Error: %s\n", SDL.SDL_GetError());
        }

        //Get rid of old surface
        SDL.SDL_FreeSurface(Surface);
    }

    public void LoadFromFile()
    {
        Surface = SDL_image.IMG_Load(Path);
        if (Surface == IntPtr.Zero)
        {
            Console.WriteLine("Failed to load texture: " + SDL_image.IMG_GetError());
            return;
        }
        TexturePtr = SDL.SDL_CreateTextureFromSurface(Renderer, Surface);
        SDL.SDL_FreeSurface(Surface);


    }

    public int GetWidth()
    {
        SDL.SDL_QueryTexture(TexturePtr, out _, out _, out int width, out _);
        return width;
    }

    public int GetHeight()
    {
        SDL.SDL_QueryTexture(TexturePtr, out _, out _, out _, out int height);
        return height;
    }

    public void Render(int x, int y)
    {
        SDL.SDL_Rect dstRect = new SDL.SDL_Rect { x = x, y = y, w = GetWidth(), h = GetHeight() };
        SDL.SDL_RenderCopy(Renderer, TexturePtr, IntPtr.Zero, ref dstRect);
    }

    public void Destroy()
    {
        SDL.SDL_DestroyTexture(TexturePtr);
        TexturePtr = IntPtr.Zero;
    }



}