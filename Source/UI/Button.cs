namespace GameOffJam;

public class Button : UI
{
    private Vector2 position;
    
    private readonly Sprite sprite;
    
    private Hitbox hitbox;

    private Action action;

    private string animationName;

    public Button(string animationName, Vector2 position, Action action)
    {
        sprite = AssetUtils.GetSprite("Button")!;

        this.action = action;
        this.position = position;
        this.animationName = animationName;
        
        hitbox = new(new Rect(position, 16 * Game.Scale/2, 16 * Game.Scale/2), Color.Red);
    }
    
    public override void Update()
    {
        if (hitbox.Contains(Game.Instance.Input.Mouse.Position) && Game.Instance.Input.Mouse.LeftPressed)
        {
            // Run the action if the button is clicked
            action();
        }
    }

    public override void Render(Batcher batcher)
    {
        // var anim = sprite.GetAnimation(animationName);
        // var frame = sprite.GetFrameAt(anim, 0, true);
        //
        // batcher.Image(frame.Subtexture, position, Vector2.Zero, Vector2.One * Game.Scale/2, 0, Color.White);
        //
        // hitbox.Render(batcher);
    }
}