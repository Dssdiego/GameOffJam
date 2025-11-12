namespace GameOffJam;

public class Mine : Actor
{
    public Mine()
    {
        Sprite = AssetUtils.GetSprite("Mine")!;
    }
    
    public override void Update()
    {
        base.Update();
    }

    public override void Render(Batcher batcher)
    {
        base.Render(batcher);
        
        var anim = Sprite.GetAnimation("Idle");
        var frame = Sprite.GetFrameAt(anim, 0, true);
        
        batcher.Image(frame.Subtexture, Position, new Vector2(16,16), Vector2.One * 2, 0, Color.White);
    }
}