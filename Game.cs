using SDL2;
using System;
namespace Sharpy;

public class Game
{
    public IntPtr window = IntPtr.Zero;  // SDL_Window* equivalent in C#
    public IntPtr renderer = IntPtr.Zero; // Rendering context
    public bool Running;
    public Background? background; //I dont know how to fix this yet so ill leave it like this
    public static Dictionary<string, IntPtr> Textures = new Dictionary<string, IntPtr>();
    public List<Asset> Assets = new List<Asset>();

    public void Play()
    {
        Initialize();
        Snake snake = new Snake(5, 5);

        Running = true;
        while (Running)
        {
            SDL.SDL_Event e;
            while (SDL.SDL_PollEvent(out e) != 0)
            {
                if (e.type == SDL.SDL_EventType.SDL_QUIT)
                {
                    Running = false;
                }
            }
            // Clear the screen
            SDL.SDL_RenderClear(renderer);
            background.Draw();
            //Render all assets
            snake.Render(renderer);
            //snake.Move();
            // RenderAssets();
            // Present the renderer
            SDL.SDL_RenderPresent(renderer);

        }
        background.DestroyTexture();
        SDL.SDL_DestroyRenderer(renderer);
        SDL.SDL_DestroyWindow(window);
        SDL.SDL_Quit();

    }
    public void Initialize()
    {
        // Initialize SDL
        if (SDL.SDL_Init(SDL.SDL_INIT_VIDEO) < 0)
        {
            Console.WriteLine($"SDL could not initialize! SDL_Error: {SDL.SDL_GetError()}");
            return;
        }

        //Create Window
        window = SDL.SDL_CreateWindow("Sharpy", SDL.SDL_WINDOWPOS_CENTERED, SDL.SDL_WINDOWPOS_CENTERED, 600, 600, SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);
        if (window == IntPtr.Zero)
        {
            Console.WriteLine($"Window could not be created! SDL_Error: {SDL.SDL_GetError()}");
            SDL.SDL_Quit();
            return;
        }

        //Create renderer
        renderer = SDL.SDL_CreateRenderer(window, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED);

        //Create background
        background = new Background(renderer);
        background.Create();
        //Load all textures
        LoadTextures();
    }
    public void LoadTextures()
    {
        if (SDL_image.IMG_Init(SDL_image.IMG_InitFlags.IMG_INIT_PNG) < 0)
        {
            Console.WriteLine($"SDL Image could not initialize! SDL_Error: {SDL.SDL_GetError()}");
        }

        // Load surfaces for body parts
        IntPtr imageSurfaceBodyHorizontal = SDL_image.IMG_Load("./images/body_horizontal.png");
        IntPtr imageSurfaceBodyVertical = SDL_image.IMG_Load("./images/body_vertical.png");
        IntPtr imageSurfaceBodyBottomLeft = SDL_image.IMG_Load("./images/body_bottomleft.png");
        IntPtr imageSurfaceBodyTopLeft = SDL_image.IMG_Load("./images/body_topleft.png");
        IntPtr imageSurfaceBodyBottomRight = SDL_image.IMG_Load("./images/body_bottomright.png");
        IntPtr imageSurfaceBodyTopRight = SDL_image.IMG_Load("./images/body_topright.png");

        // Load surfaces for head parts
        IntPtr imageSurfaceHeadUp = SDL_image.IMG_Load("./images/head_up.png");
        IntPtr imageSurfaceHeadDown = SDL_image.IMG_Load("./images/head_down.png");
        IntPtr imageSurfaceHeadLeft = SDL_image.IMG_Load("./images/head_left.png");
        IntPtr imageSurfaceHeadRight = SDL_image.IMG_Load("./images/head_right.png");

        // Load surfaces for tail parts
        IntPtr imageSurfaceTailUp = SDL_image.IMG_Load("./images/tail_up.png");
        IntPtr imageSurfaceTailDown = SDL_image.IMG_Load("./images/tail_down.png");
        IntPtr imageSurfaceTailLeft = SDL_image.IMG_Load("./images/tail_left.png");
        IntPtr imageSurfaceTailRight = SDL_image.IMG_Load("./images/tail_right.png");

        // Load surface for apple
        IntPtr imageSurfaceApple = SDL_image.IMG_Load("./images/apple.png");

        // Check for failed loads
        if (imageSurfaceBodyHorizontal == IntPtr.Zero || imageSurfaceBodyVertical == IntPtr.Zero ||
            imageSurfaceBodyBottomLeft == IntPtr.Zero || imageSurfaceBodyTopLeft == IntPtr.Zero ||
            imageSurfaceBodyBottomRight == IntPtr.Zero || imageSurfaceBodyTopRight == IntPtr.Zero ||
            imageSurfaceHeadUp == IntPtr.Zero || imageSurfaceHeadDown == IntPtr.Zero ||
            imageSurfaceHeadLeft == IntPtr.Zero || imageSurfaceHeadRight == IntPtr.Zero ||
            imageSurfaceTailUp == IntPtr.Zero || imageSurfaceTailDown == IntPtr.Zero ||
            imageSurfaceTailLeft == IntPtr.Zero || imageSurfaceTailRight == IntPtr.Zero ||
            imageSurfaceApple == IntPtr.Zero)
        {
            Console.WriteLine("Failed to load one or more textures: " + SDL_image.IMG_GetError());
            return;
        }

        // Create textures from the surfaces and store them in the dictionary
        Textures["body_horizontal"] = SDL.SDL_CreateTextureFromSurface(renderer, imageSurfaceBodyHorizontal);
        Textures["body_vertical"] = SDL.SDL_CreateTextureFromSurface(renderer, imageSurfaceBodyVertical);
        Textures["body_bottomleft"] = SDL.SDL_CreateTextureFromSurface(renderer, imageSurfaceBodyBottomLeft);
        Textures["body_topleft"] = SDL.SDL_CreateTextureFromSurface(renderer, imageSurfaceBodyTopLeft);
        Textures["body_bottomright"] = SDL.SDL_CreateTextureFromSurface(renderer, imageSurfaceBodyBottomRight);
        Textures["body_topright"] = SDL.SDL_CreateTextureFromSurface(renderer, imageSurfaceBodyTopRight);

        Textures["head_up"] = SDL.SDL_CreateTextureFromSurface(renderer, imageSurfaceHeadUp);
        Textures["head_down"] = SDL.SDL_CreateTextureFromSurface(renderer, imageSurfaceHeadDown);
        Textures["head_left"] = SDL.SDL_CreateTextureFromSurface(renderer, imageSurfaceHeadLeft);
        Textures["head_right"] = SDL.SDL_CreateTextureFromSurface(renderer, imageSurfaceHeadRight);

        Textures["tail_up"] = SDL.SDL_CreateTextureFromSurface(renderer, imageSurfaceTailUp);
        Textures["tail_down"] = SDL.SDL_CreateTextureFromSurface(renderer, imageSurfaceTailDown);
        Textures["tail_left"] = SDL.SDL_CreateTextureFromSurface(renderer, imageSurfaceTailLeft);
        Textures["tail_right"] = SDL.SDL_CreateTextureFromSurface(renderer, imageSurfaceTailRight);

        Textures["apple"] = SDL.SDL_CreateTextureFromSurface(renderer, imageSurfaceApple);

        // Free the surfaces as they're no longer needed
        SDL.SDL_FreeSurface(imageSurfaceBodyHorizontal);
        SDL.SDL_FreeSurface(imageSurfaceBodyVertical);
        SDL.SDL_FreeSurface(imageSurfaceBodyBottomLeft);
        SDL.SDL_FreeSurface(imageSurfaceBodyTopLeft);
        SDL.SDL_FreeSurface(imageSurfaceBodyBottomRight);
        SDL.SDL_FreeSurface(imageSurfaceBodyTopRight);

        SDL.SDL_FreeSurface(imageSurfaceHeadUp);
        SDL.SDL_FreeSurface(imageSurfaceHeadDown);
        SDL.SDL_FreeSurface(imageSurfaceHeadLeft);
        SDL.SDL_FreeSurface(imageSurfaceHeadRight);

        SDL.SDL_FreeSurface(imageSurfaceTailUp);
        SDL.SDL_FreeSurface(imageSurfaceTailDown);
        SDL.SDL_FreeSurface(imageSurfaceTailLeft);
        SDL.SDL_FreeSurface(imageSurfaceTailRight);

        SDL.SDL_FreeSurface(imageSurfaceApple);

    }
    public void AddAsset(Asset asset)
    {
        Assets.Add(asset);
    }
    public void RenderAssets()
    {
        foreach (var asset in Assets)
        {
            Console.WriteLine("Rendering asset at position: (" + asset.PositionX + ", " + asset.PositionY + ")");
            asset.DisplayOnRenderer(renderer);
        }
    }
}