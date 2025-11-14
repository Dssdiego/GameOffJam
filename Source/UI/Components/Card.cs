namespace GameOffJam;

public class Card : UI
{
    public enum State
    {
        Normal,
        Selected
    }

    public Vector2 Position;
    public float Scale = 4f;

    public PowerUp PowerUp;

    private Sprite sprite;
    private State state = State.Normal;

    public Card(PowerUp powerUp)
    {
        PowerUp = powerUp;
        
        sprite = AssetUtils.GetSprite("Card")!;
    }

    public void SetState(State newState)
    {
        state = newState;
    }
        
    public override void Update() { }

    public override void Render(Batcher batcher)
    {
        // content
        {
            var anim = sprite.GetAnimation("Speed");
            var frame = sprite.GetFrameAt(anim, 0, false);
        
            batcher.Image(frame.Subtexture, Position, new Vector2(16,16), Vector2.One * Scale, 0, Color.White);
        }
        
        // border
        {
            var anim = sprite.GetAnimation(state == State.Normal ? "Border" : "BorderSelected");
            var frame = sprite.GetFrameAt(anim, 0, false);
        
            batcher.Image(frame.Subtexture, Position, new Vector2(16,16), Vector2.One * Scale, 0, Color.White);
        }
        
        // texts
        batcher.Text(Assets.SpriteFonts["Renogare"], PowerUp.Value.ToString(), Position + new Vector2(-20, 15), Color.White);
        batcher.Text(Assets.SpriteFonts["Renogare"], PowerUp.Type.ToString(), Position + new Vector2(-40, 80), Color.White);
    }
}