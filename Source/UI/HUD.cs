namespace GameOffJam;

public class HUD : UI
{
    public static World World;
    
    private static Vector2 position;
    
    // private static Button playButton;
    private static List<Button> controlButtons = [];
    
    public static void Init(World world, Window window)
    {
        World = world;
        
        // position = new Vector2(10, window.Height - 100);
        position = Vector2.Zero;
        
        var hudHeightBase = window.Height - 50;
        var centerWidth = window.Width / 2;
        var widthOffset = 40;
        
        var playButton = new Button("Play", new Vector2(centerWidth - widthOffset, hudHeightBase), () =>
        {
            Log.Info("TODO: Clicked the PLAY button");
        });
        
        var stepButton = new Button("Step", new Vector2(centerWidth, hudHeightBase), () =>
        {
            Log.Info("TODO: Clicked the STEP button");
        });
        
        var stopButton = new Button("Stop", new Vector2(centerWidth + widthOffset, hudHeightBase), () =>
        {
            Log.Info("TODO: Clicked the STOP button");
        });
        
        controlButtons.Add(playButton);
        controlButtons.Add(stepButton);
        controlButtons.Add(stopButton);
    }

    public override void Update()
    {
        // Update control buttons
        foreach (var button in controlButtons)
        {
            button.Update();
        }
    }
    
    public override void Render(Batcher batcher)
    {
        var windowWidth = Game.Instance.Window.Width;
        
        // batcher.Text(Assets.SpriteFonts["Renogare"], "HUD stuff goes here", position, Color.White);
        batcher.Text(Assets.SpriteFonts["Renogare"], $"FPS: {Math.Round(1/Game.Instance.Time.Delta)}", new Vector2(windowWidth - 120, position.Y + 20), Color.White);
        
        // Render control buttons
        foreach (var button in controlButtons)
        {
            button.Render(batcher);
        }
    }
}