namespace GameOffJam;

public class Game : App
{
    // public static readonly Version Version = typeof(Game).Assembly.GetName().Version!;
    // public static readonly string VersionString = $"v.{Version.Major}.{Version.Minor}.{Version.Build}";
    
    private static Game? instance;
    public static Game Instance => instance ?? throw new Exception("Game isn't running");

    public string[] Args;
    
    public static bool Paused = false;

    public World World;
    
    public readonly Controls Controls;
    
    private readonly Batcher batcher;

    private readonly HUD gameHUD = new();
    
    private PauseMenu pauseMenu;
    
    public static bool ShowHitboxes = true;
    public static float Scale = 4f;
    
    private static AppConfig appConfig = new()
    {
        ApplicationName = Globals.GameName,
        WindowTitle = Globals.GameName,
        Width = 1280,
        Height = 640,
        Fullscreen = false,
        // UpdateMode = UpdateMode.UnlockedStep()
    };

    public Game(string[] cliArgs) : base(appConfig)
    {
        Args = cliArgs;
        
        batcher = new Batcher(GraphicsDevice);
        // target = new Target(GraphicsDevice, Width, Height, "RenderTarget");
        
        Controls = new Controls(Input);
    }
    
    protected override void Startup()
    {
        instance = this;

        Assets.Init(GraphicsDevice);

        World = new World("Puzzles");
        World.Load("Test");

        pauseMenu = new PauseMenu(World);
        
        // GameState.Create<LandingPad>(new Vector2(400, 250));
        // World.Spawn<BlackHole>(new Vector2(400, 250));
        // var player = World.Spawn<Player>(new Vector2(100, 100));
        
        // var player = World.Spawn<Player>(new Vector2(200, 200));

        HUD.Init(World, Window);
    }

    protected override void Update()
    {
        if (Controls.PauseGame.Pressed)
        {
            Paused = !Paused;
            pauseMenu.ResetSelection();
        }

        if (Paused)
        {
            pauseMenu.Update();
        }
        
        if (!Paused)
        {
            Steam.Update();
            
            // GameState.Update();
            World.Update();

            gameHUD.Update();
            
            // actionSlotPopup.Update();
        }
        else
        {
            pauseMenu.Update();
        }
    }

    protected override void Render()
    {
        Window.Clear(0x150e22);

        World.Render(batcher);
       
        gameHUD.Render(batcher);

        if (Paused)
        {
            pauseMenu.Render(batcher);
        }
        
        batcher.Render(Window);
        batcher.Clear();
    }

    protected override void Shutdown()
    {
        Steam.Shutdown();
    }
}