namespace GameOffJam;

public class Hitbox
{
    public Rect Rect;
    public Color Color;
    
    public Hitbox(in Rect rectVal)
    {
        Color = Color.White;
        Rect = rectVal;
    }
    
    public Hitbox(in Rect rectVal, in Color color)
    {
        Color = color;
        Rect = rectVal;
    }

    public bool Overlaps(Rect rectVal) => Overlaps(Point2.Zero, new Hitbox(rectVal));

    // Checks if this hitbox overlaps with another hitbox
    public bool Overlaps(in Vector2 offset, in Hitbox other)
    {
        return RectToRect(Rect + offset, other.Rect);
    }
    
    // Checks if this hitbox contains a point
    public bool Contains(in Vector2 point)
    {
        return Rect.Contains(point);
    }
    
    // Checks overlap between two rectangles
    private bool RectToRect(Rect a, Rect b)
    {
        return a.Overlaps(b);
    }

    public void Render(Batcher batcher)
    {
        if (Game.ShowHitboxes)
        {
            batcher.RectDashed(Rect, 2, Color, 2, 0);
        }
    }
    
    public void Render(Batcher batcher, float lineWeight)
    {
        if (Game.ShowHitboxes)
        {
            batcher.RectDashed(Rect, lineWeight, Color, 2, 0);
        }
    }
}