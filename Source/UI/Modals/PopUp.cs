namespace GameOffJam;

public class PopUp : UI
{
    public Vector2 Position;

    public string Title;
    public string Description;
    public Color BackgroundColor = new(0, 0, 0, 0.75f);
    
    public PopUp() { }
    
    public override void Update() { }

    public override void Render(Batcher batcher)
    {
        batcher.RectRounded(new Rect(Position, 250, 150), 10, BackgroundColor);
        
        batcher.Text(Assets.SpriteFonts["Renogare"], Title, Position, Color.White);
        batcher.Text(Assets.SpriteFonts["Renogare"], Description, Position + new Vector2(0, 40), Color.White);
    }
}