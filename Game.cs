using SDL2;
using System;
namespace Sharpy;

public class Game
{
    public IntPtr window = IntPtr.Zero;  // SDL_Window* equivalent in C#
    public IntPtr renderer = IntPtr.Zero; // Rendering context
    public bool Running;
    public Snake snake;
    public Apple apple;
    public IntPtr font;
    public Background background;
    private bool Pause = false;
    public static Dictionary<string, Texture> Textures = new Dictionary<string, Texture>();
    FPSCounter fps;




    public void Play()
    {

        Initialize();
        LoadMedia();

        snake = new Snake(5 * 40, 5 * 40);
        apple = new Apple();
        Running = true;
        Time.Start();
        fps = new(renderer, font);
        while (Running)
        {
            //Event Handler
            SDL.SDL_Event e;
            while (SDL.SDL_PollEvent(out e) != 0)
            {
                if (e.type == SDL.SDL_EventType.SDL_QUIT)
                {
                    Running = false;
                }
                else if (e.type == SDL.SDL_EventType.SDL_KEYDOWN)
                {
                    HandleKeyPress(e);
                }
            }
            if (!Pause)
            {
                fps.Update();
                snake.Update();
                Time.Update();
            }
            RenderObjects();


        }
        Clean();

    }
    public void RenderObjects()
    {
        SDL.SDL_RenderClear(renderer);
        background.Draw();
        snake.Render(renderer);
        fps.Display(500, 0);
        SDL.SDL_RenderPresent(renderer);
    }
    public void CheckCollisions()
    {

    }
    public void LoadMedia()
    {
        //Load font
        font = SDL_ttf.TTF_OpenFont("./fonts/MegamaxJonathanToo-YqOq2.ttf", 14);
        if (font == IntPtr.Zero)
        {
            Console.WriteLine("Failed to load font: " + SDL_ttf.TTF_GetError());
            return;
        }

        // Load textures for body parts
        Textures["body_horizontal"] = new Texture(renderer, "./images/body_horizontal.png");
        Textures["body_vertical"] = new Texture(renderer, "./images/body_vertical.png");
        Textures["body_bottomleft"] = new Texture(renderer, "./images/body_bottomleft.png");
        Textures["body_topleft"] = new Texture(renderer, "./images/body_topleft.png");
        Textures["body_bottomright"] = new Texture(renderer, "./images/body_bottomright.png");
        Textures["body_topright"] = new Texture(renderer, "./images/body_topright.png");

        // Load textures for head parts
        Textures["head_up"] = new Texture(renderer, "./images/head_up.png");
        Textures["head_down"] = new Texture(renderer, "./images/head_down.png");
        Textures["head_left"] = new Texture(renderer, "./images/head_left.png");
        Textures["head_right"] = new Texture(renderer, "./images/head_right.png");

        // Load textures for tail parts
        Textures["tail_up"] = new Texture(renderer, "./images/tail_up.png");
        Textures["tail_down"] = new Texture(renderer, "./images/tail_down.png");
        Textures["tail_left"] = new Texture(renderer, "./images/tail_left.png");
        Textures["tail_right"] = new Texture(renderer, "./images/tail_right.png");

        // Load texture for apple
        Textures["apple"] = new Texture(renderer, "./images/apple.png");

        // Check for failed loads
        foreach (var texture in Textures)
        {
            if (texture.Value.TexturePtr == IntPtr.Zero)
            {
                Console.WriteLine($"Failed to load texture: {texture.Key}");
                return;
            }
        }
    }
    public void Initialize()
    {
        // Initialize SDL
        if (SDL.SDL_Init(SDL.SDL_INIT_VIDEO) < 0)
        {
            Console.WriteLine($"SDL could not initialize! SDL_Error: {SDL.SDL_GetError()}");
            return;
        }
        // Initialize fonts
        if (SDL_ttf.TTF_Init() < 0)
        {
            Console.WriteLine($"Error initializing SDL_ttf: {SDL_ttf.TTF_GetError()}");
            return;
        }
        // Initialize SDL_Image
        if (SDL_image.IMG_Init(SDL_image.IMG_InitFlags.IMG_INIT_PNG) < 0)
        {
            Console.WriteLine($"SDL Image could not initialize! SDL_Error: {SDL.SDL_GetError()}");
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
    }

    public void HandleKeyPress(SDL.SDL_Event e)
    {
        switch (e.key.keysym.sym)
        {
            case SDL.SDL_Keycode.SDLK_w:
            case SDL.SDL_Keycode.SDLK_UP:
                snake.ProcessInput(SnakePiece.Direction.UP);
                break;
            case SDL.SDL_Keycode.SDLK_s:
            case SDL.SDL_Keycode.SDLK_DOWN:
                snake.ProcessInput(SnakePiece.Direction.DOWN);
                break;
            case SDL.SDL_Keycode.SDLK_a:
            case SDL.SDL_Keycode.SDLK_LEFT:
                snake.ProcessInput(SnakePiece.Direction.LEFT);
                break;
            case SDL.SDL_Keycode.SDLK_d:
            case SDL.SDL_Keycode.SDLK_RIGHT:
                snake.ProcessInput(SnakePiece.Direction.RIGHT);
                break;
            case SDL.SDL_Keycode.SDLK_SPACE:
                Pause = !Pause;
                break;


        }
    }

    public void Clean()
    {
        foreach (var texture in Textures)
        {
            texture.Value.Destroy();
        }
        background.DestroyTexture();
        SDL_ttf.TTF_CloseFont(font);
        SDL.SDL_DestroyRenderer(renderer);
        SDL.SDL_DestroyWindow(window);
        SDL.SDL_Quit();
    }



}