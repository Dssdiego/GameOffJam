namespace GameOffJam;

public class Player : Actor
{
    private float size = 25f;
    
    public override void Update()
    {
        base.Update();
    }

    public override void Render(Batcher batcher)
    {
        base.Render(batcher);
        
        batcher.Triangle(new Triangle(new Vector2(0, 0), new Vector2(size, -2*size), new Vector2(size*2, 0)), Color.Red);
        // batcher.Rect(new Rect(16, 16), Color.White);
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