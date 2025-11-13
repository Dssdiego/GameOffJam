namespace GameOffJam;

public class Card : UI
{
    public enum State
    {
        Normal,
        Selected
    }

    public Action Action = () => { };
    public Vector2 Position;
    public float Scale = 4f;

    private Sprite sprite;
    private State state = State.Normal;

    public Card()
    {
        sprite = AssetUtils.GetSprite("Card")!;
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
        var anim = sprite.GetAnimation(state == State.Normal ? "Normal" : "Selected");
        var frame = sprite.GetFrameAt(anim, 0, true);
        
        batcher.Image(frame.Subtexture, Position, new Vector2(16,16), Vector2.One * Scale, 0, Color.White);
    }
}