namespace GameOffJam;

public class Actor
{
    [Flags]
    public enum Masks
    {
        None      = 0,
        Player    = 1 << 0,
        CardArea  = 1 << 1
    }
    
    public Vector2 Position;
    public Hitbox? Hitbox; // REVIEW: Do we have actors that don't need hitboxes? Or all actors should have hitboxes ALWAYS?
    public Masks Mask;
    public int Depth = 0; // rendering depth, higher is rendered later (on top)
    public bool WarpInScreen = false;
    
    public Sprite Sprite;
    
    private World? world = null;
    
    /// <summary>
    /// The World we belong to - asserts if Destroyed
    /// </summary>
    public World World => world ?? throw new Exception("Actor not added to the World");
    
    public float Timer = 0f;
    
    public Game Game = null!;
    public Controls Controls = null!;
    public Time Time => Game.Instance.Time;
    
    #region World
    public void SetWorld(in World newWorld)
    {
        world = newWorld;
    }
    #endregion

    #region Game Loop Methods
    public virtual void Update()
    {
        Timer += Time.Delta;

        if (WarpInScreen)
        {
            // warp X
            if (Position.X < 0)
            {
                Position.X = Game.Window.WidthInPixels/2;
            }

            if (Position.X > Game.Window.WidthInPixels / 2)
            {
                Position.X = 0;
            }
        
            // warp Y
            if (Position.Y < 0)
            {
                Position.Y = Game.Window.HeightInPixels / 2;
            }

            if (Position.Y > Game.Window.HeightInPixels / 2)
            {
                Position.Y = 0;
            }
        }
    }

    public virtual void Render(Batcher batcher)
    {
        if (Hitbox != null)
        {
            Hitbox.Render(batcher, 2 / Game.Scale);
        }
    }
    #endregion
    
    #region Events
    /// <summary>
    /// Called when the Actor was added to the World
    /// </summary>
    public virtual void OnAddedToWorld() { }
    
    /// <summary>
    /// Called when the Actor was destroyed
    /// </summary>
    public virtual void OnDestroyed() { }
    
    #endregion
    
    #region Spatial | Overlaps
    // REVIEW: Should this be here instead of inside the World class?
    public bool OverlapsAny(Hitbox customHitbox, Vector2 offset, Masks mask)
    {
        foreach (var other in World.Actors)
        {
            if (other != this && other.Mask.Has(mask) && OverlapsAny(customHitbox, offset, other))
            {
                return true;
            }
        }

        return false;
    }
    public bool OverlapsAny(Vector2 offset, Masks mask)
    {
        foreach (var other in World.Actors)
        {
            if (other != this && other.Mask.Has(mask) && OverlapsAny(offset, other))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Checks to see if we overlap the given actor (with a custom hitbox)
    /// </summary>
    public bool OverlapsAny(Hitbox customHitbox, Vector2 offset, Actor other)
    {
        // REVIEW: Make sure our hitboxes are always "valid"?
        return other.Hitbox != null && customHitbox.Overlaps(Position + offset - other.Position, other.Hitbox);
    }
    
    /// <summary>
    /// Checks to see if we overlap the given actor
    /// </summary>
    public bool OverlapsAny(Vector2 offset, Actor other)
    {
        // REVIEW: Make sure our hitboxes are always "valid"?
        return Hitbox != null && other.Hitbox != null && Hitbox.Overlaps(Position + offset - other.Position, other.Hitbox);
    }
    #endregion
}