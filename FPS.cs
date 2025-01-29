using SDL2;

namespace Sharpy;

public class FPSCounter
{
    public uint startTime;
    public uint currentTime;
    public uint frameStartTime;
    public uint frameCurrentTime;
    private float currentFPS;
    public Texture fpsText;
    public int frameCount;
    public IntPtr Renderer;
    const int targetFPS = 60;
    const int frameDelay = 1000 / targetFPS;


    public FPSCounter(IntPtr renderer, IntPtr font)
    {
        startTime = SDL.SDL_GetTicks();
        frameCount = 0;
        Renderer = renderer;
        fpsText = new Texture(renderer, font);
    }

    public void Update()
    {
        frameStartTime = SDL.SDL_GetTicks();
        frameCount++;
        currentTime = SDL.SDL_GetTicks();
        uint elapsedTime = currentTime - startTime;

        if (elapsedTime > 1000)
        {
            currentFPS = frameCount / (elapsedTime / 1000.0f);
            startTime = currentTime;
            frameCount = 0;
        }

        fpsText.LoadFromText($"FPS: {Get()} ", new SDL.SDL_Color { r = 255, g = 255, b = 255, a = 255 });
        uint frameDuration = SDL.SDL_GetTicks() - frameStartTime;
        if (frameDelay > frameDuration)
        {
            SDL.SDL_Delay(frameDelay - frameDuration);  // Delay to maintain target FPS
        }
    }

    public void Display(int x, int y)
    {
        fpsText.Render(x, y);
    }

    public string Get()
    {
        return currentFPS.ToString("F2");
    }

}