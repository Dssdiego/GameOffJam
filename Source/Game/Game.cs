namespace GameOffJam;

public class Game : App
{
    // public static readonly Version Version = typeof(Game).Assembly.GetName().Version!;
    // public static readonly string VersionString = $"v.{Version.Major}.{Version.Minor}.{Version.Build}";

    public enum GameState
    {
        Running,
        Paused,
        ChoosePowerUp
    }
    
    private static Game? instance;
    public static Game Instance => instance ?? throw new Exception("Game isn't running");

    public string[] Args;
    
    public World World;
    
    public readonly Controls Controls;
    
    private readonly Batcher batcher;

    private readonly HUD gameHUD = new();
    
    public static GameState State = GameState.Running;
    
    private PauseMenu pauseMenu;
    private PowerUpChooser powerUpChooser;
    
    public static bool ShowHitboxes = true;
    public static float Scale = 1f;
    
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

    public static void ChangeState(GameState newState)
    {
        State = newState;
    }
    
    protected override void Startup()
    {
        instance = this;

        Assets.Init(GraphicsDevice);

        World = new World("Puzzles");
        World.Load("Test");

        pauseMenu = new PauseMenu(World);
        powerUpChooser = new PowerUpChooser(Controls);
        
        // GameState.Create<LandingPad>(new Vector2(400, 250));
        // World.Spawn<BlackHole>(new Vector2(400, 250));
        // var player = World.Spawn<Player>(new Vector2(100, 100));

        var player = World.Spawn<Player>(new Vector2(300, 150));

        var enemyMine = World.Spawn<Mine>(new Vector2(450, 50));

        HUD.Init(World, Window);
    }

    protected override void Update()
    {
        if (Controls.PauseGame.Pressed && State != GameState.ChoosePowerUp)
        {
            ChangeState(State == GameState.Running ? GameState.Paused : GameState.Running);
            pauseMenu.ResetSelection();
        }

        if (Controls.PowerUpChooser.Pressed && State != GameState.Paused)
        {
            ChangeState(State == GameState.Running ? GameState.ChoosePowerUp : GameState.Running);
        }

        if (State == GameState.Paused)
        {
            pauseMenu.Update();
        }

        if (State == GameState.ChoosePowerUp)
        {
            powerUpChooser.Update();
        }
        
        if (State == GameState.Running)
        {
            Steam.Update();
            
            // GameState.Update();
            World.Update();

            gameHUD.Update();
        }
    }
    
    protected override void Render()
    {
        Window.Clear(Color.Black);

        World.Render(batcher);
       
        gameHUD.Render(batcher);

        if (State == GameState.Paused)
        {
            pauseMenu.Render(batcher);
        }

        if (State == GameState.ChoosePowerUp)
        {
            powerUpChooser.Render(batcher);
        }
        
        batcher.Render(Window);
        batcher.Clear();
    }

    protected override void Shutdown()
    {
        Steam.Shutdown();
    }
}