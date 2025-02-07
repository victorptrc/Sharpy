using SDL2;
namespace Sharpy;

public class UI
{
    private Texture appleTexture;
    private Texture currentScoreTextTexture;
    private Texture highScoreTextTexture;
    private Texture trophyTexture;
    private IntPtr Renderer;
    private IntPtr Font;
    private SDL.SDL_Color textColor = new SDL.SDL_Color { r = 255, g = 255, b = 255, a = 255 };
    public int Score = 0;
    public int HighScore = 0;
    public Button Volume;




    public UI(IntPtr font, IntPtr renderer)
    {
        Renderer = renderer;
        Font = font;
        appleTexture = Game.Textures["apple"];
        trophyTexture = Game.Textures["trophy"];
        highScoreTextTexture = new(Renderer, Font);
        highScoreTextTexture.LoadFromText(HighScore.ToString(), textColor);
        currentScoreTextTexture = new(Renderer, Font);
        currentScoreTextTexture.LoadFromText(Score.ToString(), textColor);
        Volume = new(580, 10, 40, 40, Game.Textures["volume_on"]);

    }

    public void Update(int score, int highScore)
    {
        Score = score;
        HighScore = highScore;
        highScoreTextTexture.LoadFromText(HighScore.ToString(), textColor);
        currentScoreTextTexture.LoadFromText(Score.ToString(), textColor);
    }

    public void Render()
    {
        SDL.SDL_SetRenderDrawColor(Renderer, 74, 117, 44, 255);
        SDL.SDL_Rect topRect = new SDL.SDL_Rect { x = 0, y = 0, w = 660, h = 60 };
        SDL.SDL_RenderFillRect(Renderer, ref topRect);
        appleTexture.Render(20, 10);
        currentScoreTextTexture.Render(60, 20);
        trophyTexture.Render(90, 10);
        highScoreTextTexture.Render(130, 20);
        Volume.Render();
    }

}