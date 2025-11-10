namespace GameOffJam;

public class MenuItem : UI
{
    public Vector2 Position = Vector2.Zero;
    public bool IsSelected = false;
    public string Text;
    public Action Action;

    public MenuItem(string text, Action action)
    {
        Text = text;
        Action = action;
    }

    public void Pressed()
    {
        Action();
    }
    
    public override void Update() {}

    public override void Render(Batcher batcher)
    {
        batcher.Text(Assets.SpriteFonts["Renogare"], Text, Position, IsSelected ? Color.Gold : Color.White);
    }
}