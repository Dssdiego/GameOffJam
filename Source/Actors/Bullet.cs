namespace GameOffJam;

public class Bullet : Actor
{
    public float Speed = 50f;
    public Vector2 Velocity = Vector2.Zero;

    public Bullet()
    {
        WarpInScreen = true;
    }

    public void Setup(Vector2 velocity)
    {
        Velocity = velocity;
    }
    
    public override void Update()
    {
        base.Update();
        
        Position += Velocity * Speed * Time.Delta;
    }

    public override void Render(Batcher batcher)
    {
        base.Render(batcher);
        
        batcher.Circle(new Circle(Position, 6), 12, Color.LightGray);
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