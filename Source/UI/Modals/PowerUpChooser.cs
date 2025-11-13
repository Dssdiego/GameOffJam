namespace GameOffJam;

public class PowerUpChooser : UI
{
    public Controls Controls;
    
    private List<String> powerUps = new();

    private Sprite sprite;

    public PowerUpChooser(Controls controls)
    {
        Controls = controls;
        
        sprite = AssetUtils.GetSprite("Card")!;
    }

    public override void Update()
    {
        if (Controls.MenuNavLeft.Pressed)
        {
            
        }

        if (Controls.MenuNavRight.Pressed)
        {
            
        }

        if (Controls.MenuConfirm.Pressed)
        {
            
        }
    }

    public override void Render(Batcher batcher)
    {
        // draw (semi-transparent) background
        batcher.Rect(new Rect(Vector2.Zero, Game.Instance.Window.Size), Color.Black * 0.5f);
        
        // foreach (var powerUp in powerUps)
        // {
        //     
        // }
        
        var anim = sprite.GetAnimation("Normal");
        var frame = sprite.GetFrameAt(anim, 0, true);
        
        batcher.Image(frame.Subtexture, new Vector2(200, 200), new Vector2(16,16), Vector2.One * 4, 0, Color.White);
    }
}