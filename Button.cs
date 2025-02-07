using SDL2;
namespace Sharpy;

public class Button
{
    public Texture ButtonTexture;
    private SDL.SDL_Rect ButtonRect;
    private Action _onClickAction;
    public int X;
    public int Y;
    private bool IsVisible;

    public Button(int x, int y, int width, int height, Texture buttonTexture)
    {
        X = x;
        Y = y;
        ButtonRect = new SDL.SDL_Rect { x = x, y = y, w = width, h = height };
        ButtonTexture = buttonTexture;
    }
    public Button(int x, int y, int width, int height, string label)
    {
        ButtonRect = new SDL.SDL_Rect { x = x, y = y, w = width, h = height };

    }
    public void OnClick(Action action)
    {
        _onClickAction = action;
    }
    public void HandleClick(int mouseX, int mouseY)
    {
        var point = new SDL.SDL_Point { x = mouseX, y = mouseY };
        if ((SDL.SDL_PointInRect(ref point, ref ButtonRect) == SDL.SDL_bool.SDL_TRUE) && IsVisible)
        {
            _onClickAction.Invoke();
        }
    }

    public void Hide()
    {
        IsVisible = false;
    }

    public void Render()
    {
        ButtonTexture.Render(X, Y);
    }
}