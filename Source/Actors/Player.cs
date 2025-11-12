namespace GameOffJam;

public class Player : Actor
{
    public Vector2 Velocity = Vector2.Zero;
    public Vector2 Direction = Vector2.Zero;
    
    private float size = 25f;
    private float rotationAngle;
    private float rotationSpeed = 3.5f;
    private float boostSpeed = 125f;
    
    private readonly Sprite sprite;
    
    public Player()
    {
        sprite = AssetUtils.GetSprite("Player")!;

        WarpInScreen = true;
    }

    public override void Update()
    {
        base.Update();

        // rotate to the left
        if (Controls.Move.IntValue.X < 0f)
        {
            rotationAngle -= rotationSpeed * Time.Delta;
        }

        // rotate to the right
        if (Controls.Move.IntValue.X > 0f)
        {
            rotationAngle += rotationSpeed * Time.Delta;
        }

        // "forward" direction calculation
        // FIXME: Perhaps rotation is in radians, and I'm trying to use degrees here. It works but it's wrong!
        // Log.Info("Rotation Angle: " + rotationAngle);
        Direction.X = (float)Math.Cos(rotationAngle + 30);
        Direction.Y = (float)Math.Sin(rotationAngle + 30);
        
        // boost
        if (Controls.Boost.IntValue > 0)
        {
            Velocity += Direction * boostSpeed * Time.Delta;
        }
        
        // shoot
        if (Controls.Shoot.Pressed)
        {
            var bullet = World.Spawn<Bullet>(Position + Direction * 10);
            bullet.Setup(Direction);
        }

        // move the player with velocity (w/ inertia)
        Position += Velocity * Time.Delta;
    }

    public override void Render(Batcher batcher)
    {
        base.Render(batcher);
        
        var anim = sprite.GetAnimation("Idle");
        var frame = sprite.GetFrameAt(anim, 0, true);
        
        batcher.Image(frame.Subtexture, Position, new Vector2(16,16), Vector2.One, rotationAngle, Color.White);
        
        // hitbox.Render(batcher);
        
        // batcher.Triangle(new Triangle(new Vector2(0, 0), new Vector2(size, -2*size), new Vector2(size*2, 0)), Color.Red);
    }

    public override void OnAddedToWorld()
    {
        base.OnAddedToWorld();
    }

    public override void OnDestroyed()
    {
        base.OnDestroyed();
    }
}