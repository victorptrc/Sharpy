using SDL2;
using System;
namespace Sharpy;
class Program
{
    static IntPtr screen = IntPtr.Zero;  // SDL_Window* equivalent in C#
    static IntPtr renderer = IntPtr.Zero; // Rendering context

    static void Main(string[] args)
    {
        // Initialize SDL
        if (SDL.SDL_Init(SDL.SDL_INIT_VIDEO) < 0)
        {
            Console.WriteLine($"SDL could not initialize! SDL_Error: {SDL.SDL_GetError()}");
            return;
        }
        if (SDL_image.IMG_Init(SDL_image.IMG_InitFlags.IMG_INIT_PNG) < 0)
        {
            Console.WriteLine($"SDL Image could not initialize! SDL_Error: {SDL.SDL_GetError()}");
        }

        // Create the SDL window (equivalent to the screen surface in C)
        screen = SDL.SDL_CreateWindow("Sharpy", SDL.SDL_WINDOWPOS_CENTERED, SDL.SDL_WINDOWPOS_CENTERED, 600, 600, SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);
        if (screen == IntPtr.Zero)
        {
            Console.WriteLine($"Window could not be created! SDL_Error: {SDL.SDL_GetError()}");
            SDL.SDL_Quit();
            return;
        }
        //Create renderer
        renderer = SDL.SDL_CreateRenderer(screen, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED);

        IntPtr imageSurface = SDL_image.IMG_Load("./images/apple.png");
        if (imageSurface == IntPtr.Zero)
        {
            Console.WriteLine("Failed to load image: " + SDL_image.IMG_GetError());
            return;
        }
        // Create a texture from the surface
        IntPtr appleTexture = SDL.SDL_CreateTextureFromSurface(renderer, imageSurface);
        // Free the surface as it's no longer needed
        SDL.SDL_FreeSurface(imageSurface);
        Background background = new Background(renderer);
        background.Create();
        Asset apple = new Apple(appleTexture, 40, 40, 14, 14);


        // Wait for user to close the window
        bool running = true;
        while (running)
        {
            SDL.SDL_Event e;
            while (SDL.SDL_PollEvent(out e) != 0)
            {
                if (e.type == SDL.SDL_EventType.SDL_QUIT)
                {
                    running = false;
                }
            }

            // Clear the screen
            SDL.SDL_RenderClear(renderer);
            background.Draw();
            apple.DisplayOnRenderer(renderer);
            // Present the renderer
            SDL.SDL_RenderPresent(renderer);
        }

        // Clean up
        background.DestroyTexture();
        SDL.SDL_DestroyTexture(appleTexture);
        SDL.SDL_DestroyRenderer(renderer);
        SDL.SDL_DestroyWindow(screen);
        SDL.SDL_Quit();
    }

    static void display_bmp(string fileName)
    {
        // Load the BMP image
        IntPtr image = SDL.SDL_LoadBMP(fileName);
        if (image == IntPtr.Zero)
        {
            Console.WriteLine($"Couldn't load {fileName}: {SDL.SDL_GetError()}");
            return;
        }

        // Get the screen's surface (SDL_Surface*) for blitting
        IntPtr screenSurface = SDL.SDL_GetWindowSurface(screen);

        // Blit the loaded image onto the screen
        if (SDL.SDL_BlitSurface(image, IntPtr.Zero, screenSurface, IntPtr.Zero) < 0)
        {
            Console.WriteLine($"BlitSurface error: {SDL.SDL_GetError()}");
        }

        // Update the window to reflect the changes
        SDL.SDL_UpdateWindowSurface(screen);

        // Free the loaded image
        SDL.SDL_FreeSurface(image);
    }
}
