namespace GameOffJam;

class Program
{
    public static void Main(string[] args)
    {
        try
        {
            // we must initialize Steam before the game starts (so that the overlay can "attach itself" to the game window)
            Steam.Init();
            
            Game game = new Game(args);
            game.Run();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }
}