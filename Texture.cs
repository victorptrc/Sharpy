using SDL2;
namespace Sharpy;

public class Texture
{
    private IntPtr Surface;
    private IntPtr Renderer;
    public IntPtr TexturePtr;
    private string Path = string.Empty;

    private IntPtr Font;
    public enum TexturePart { TopPart, BottomPart, LeftPart, RightPart }

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

    public void RenderPart(int x, int y, float part, TexturePart texturePart)
    {

        // Define source rectangle
        SDL.SDL_Rect srcRect = new SDL.SDL_Rect { x = 0, y = 0, w = GetWidth(), h = GetHeight() };

        // Adjust source rectangle based on part selection
        switch (texturePart)
        {
            case TexturePart.LeftPart:
                srcRect.w = (int)(srcRect.w * part);
                break;
            case TexturePart.RightPart:
                srcRect.x = (int)(GetWidth() - GetWidth() * part);
                srcRect.w = (int)(srcRect.w * part);
                break;
            case TexturePart.TopPart:
                srcRect.h = (int)(srcRect.h * part);
                break;
            case TexturePart.BottomPart:
                srcRect.y = (int)(GetWidth() - GetWidth() * part);
                srcRect.h = (int)(srcRect.h * part);
                break;
            default:
                return;
        }

        // Define destination rectangle (keeps the cropped size)
        SDL.SDL_Rect destRect = new SDL.SDL_Rect { x = x, y = y, w = srcRect.w, h = srcRect.h };

        // Render the cropped texture
        SDL.SDL_RenderCopy(Renderer, TexturePtr, ref srcRect, ref destRect);

    }



}