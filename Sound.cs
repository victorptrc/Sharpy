using SDL2;
namespace Sharpy;


public class Sound
{
    private IntPtr SoundPtr;

    public Sound(string path)
    {
        SoundPtr = SDL_mixer.Mix_LoadWAV(path); ;
        if (SoundPtr == IntPtr.Zero)
        {
            Console.WriteLine("Failed to load sound! Error: " + SDL.SDL_GetError());
            return;
        }
    }
    public void Play()
    {
        SDL_mixer.Mix_PlayChannel(-1, SoundPtr, 0);
    }
    public void Destroy()
    {
        SDL_mixer.Mix_FreeChunk(SoundPtr);
        SoundPtr = IntPtr.Zero;
    }
}