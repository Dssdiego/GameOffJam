namespace GameOffJam;

public class Card : UI
{
    public enum State
    {
        Normal,
        Selected
    }

    public Action Action = () => { };
    public string Title;
    public string Description;
    public Vector2 Position;
    public float Scale = 4f;

    private Sprite contentSprite;
    private Sprite borderSprite;
    private State state = State.Normal;

    public Card(string title, string description)
    {
        Title = title;
        Description = description;
        
        contentSprite = AssetUtils.GetSprite("Card")!;
        borderSprite = AssetUtils.GetSprite("CardBorder")!;
    }

    public void SetState(State newState)
    {
        state = newState;
    }
        
    public override void Update()
    {
        
    }

    public override void Render(Batcher batcher)
    {
        // card content
        {
            var contentAnim = contentSprite.GetAnimation("Speed");
            var contentFrame = contentSprite.GetFrameAt(contentAnim, 0, false);
        
            batcher.Image(contentFrame.Subtexture, Position, new Vector2(16,16), Vector2.One * Scale, 0, Color.White);
        }
        
        // card border
        {
            var borderAnim = borderSprite.GetAnimation(state == State.Normal ? "Normal" : "Selected");
            var borderFrame = borderSprite.GetFrameAt(borderAnim, 0, false);
        
            batcher.Image(borderFrame.Subtexture, Position, new Vector2(16,16), Vector2.One * Scale, 0, Color.White);
        }
        
        // texts
        batcher.Text(Assets.SpriteFonts["Renogare"], Description, Position + new Vector2(-20, 15), Color.White);
        batcher.Text(Assets.SpriteFonts["Renogare"], Title, Position + new Vector2(-40, 80), Color.White);
    }
}