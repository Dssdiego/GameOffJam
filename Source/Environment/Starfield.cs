namespace GameOffJam;

public class Star
{
    public enum ESize
    {
        Small,
        Medium,
        Medium2,
        Big
    }
    
    public Vector2 Position;
    public ESize Size;
    public Color Color;
    public float Transparency;

    public Star(Vector2 position, ESize size, Color color, float transparency)
    {
        Position = position;
        Size = size;
        Color = color;
        Transparency = transparency;
    }
}

public class Starfield
{
    public int NumOfStars = 150;
    public bool MoveStars = true;
    
    public Time Time => Game.Instance.Time;

    private Sprite sprite;
    private List<Star> Stars = [];

    private List<Color> starsColors =
    [
        Color.White,
        Color.Gray,
        Color.DarkGray,
        Color.Peachpuff,
        Color.LightYellow,
        Color.Wheat,
        Color.Beige,
    ];
    
    private Player? playerRef;

    public Starfield()
    {
        sprite = AssetUtils.GetSprite("Stars")!;
        
        Stars.EnsureCapacity(NumOfStars);
        
        for (int i = 0; i < NumOfStars; i++)
        {
            var rndX = new Random().Next(0, Game.Instance.Window.Width);
            var rndY = new Random().Next(0, Game.Instance.Window.Height);
            
            var rndSizeIdx = new Random().Next(0, Enum.GetValues(typeof(Star.ESize)).Length);
            var rndSize = (Star.ESize) rndSizeIdx;
            
            var rndTransparency = new Random().NextDouble();
            var rndTransparencyClamped = Calc.Clamp((float)rndTransparency, 0.15f, 0.85f);
            
            var rndColorIdx = new Random().Next(0, starsColors.Count);
            var rndColor = starsColors[rndColorIdx];
            
            var randomPosition = new Vector2(rndX, rndY);
            Stars.Add(new Star(randomPosition, rndSize, rndColor, rndTransparencyClamped));
        }
    }

    public void Update()
    {
        if (playerRef == null)
        {
            playerRef = Game.Instance.World.GetFirstActorWithMask(Actor.Masks.Player) as Player;
        }

        if (playerRef == null)
        {
            return;
        }

        if (!MoveStars)
        {
            return;
        }

        var window = Game.Instance.Window;

        // iterate the stars and move them the inverse of the player's velocity
        foreach (var star in Stars)
        {
            // warp screen X
            if (star.Position.X < 0)
            {
                star.Position.X = window.WidthInPixels;
            }

            if (star.Position.X > window.WidthInPixels)
            {
                star.Position.X = 0;
            }
        
            // warp screen Y
            if (star.Position.Y < 0)
            {
                star.Position.Y = window.HeightInPixels;
            }

            if (star.Position.Y > window.HeightInPixels)
            {
                star.Position.Y = 0;
            }
            
            star.Position += playerRef.Velocity * -0.5f * Time.Delta;
        }
    }
    
    public void Render(Batcher batcher)
    {
        foreach (var star in Stars)
        {
            var anim = sprite.GetAnimation(star.Size.ToString());
            var frame = sprite.GetFrameAt(anim, 0, false);

            batcher.Image(frame.Subtexture, star.Position, new Vector2(16, 16), Vector2.One, 0, star.Color * star.Transparency);
        }
    }
}