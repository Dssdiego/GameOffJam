using Steamworks;

namespace GameOffJam;

public class Steam
{
    public static bool Initialized { get; private set; } = false;

    private static bool enabled = false;
    private static bool requiredForGameOpen = false;

    public static void Init()
    {
        if (!enabled)
        {
            return;
        }
        
        if (requiredForGameOpen && SteamAPI.RestartAppIfNecessary((AppId_t) 480))
        {
            Environment.Exit(1);
        }

        Log.Info("Initializing Steam...");

        Initialized = SteamAPI.Init();
        if (!Initialized)
        {
            Console.WriteLine("Steam failed to initialize.");
            return;
        }
        
        // CSteamID steamId = SteamUser.GetSteamID();

        Console.WriteLine("Steam initialized successfully.");
    }

    public static void Update()
    {
        if (!enabled)
        {
            return;
        }
        
        if (Initialized)
        {
            SteamAPI.RunCallbacks();
        }
    }
    
    public static void Shutdown()
    {
        if (!enabled)
        {
            return;
        }
        
        if (Initialized)
        {
            Log.Info("Shutting down Steam...");
            
            SteamAPI.Shutdown();
            Initialized = false;
            Console.WriteLine("Steam shut down.");
        }
    }
}