using SDL2;
namespace Sharpy;

public static class Time
{
    public static uint StartTime { get; private set; }
    public static uint TimeSinceLastFrame { get; private set; }
    public static uint LastMoveTime { get; private set; }
    public static uint CurrentMoveTime { get; private set; }

    public static uint MovementDelay { get; } = 135;


    public static void Start()
    {
        StartTime = SDL.SDL_GetTicks();

    }

    public static void Update()
    {
        CurrentMoveTime = SDL.SDL_GetTicks();
    }

    public static void ResetMoveTime()
    {
        LastMoveTime = SDL.SDL_GetTicks();
        CurrentMoveTime = SDL.SDL_GetTicks();
    }


}
