namespace GameOffJam;

public class PauseMenu : Menu
{
    public PauseMenu(in World world)
    {
        World = world;
        
        MenuItems =
        [
            new MenuItem("[Debug] Restart Level", () =>
            {
                World.Reload();

                Game.ChangeState(Game.GameState.Running);
            }),

            new MenuItem("[Debug] Toggle Hitboxes", () =>
            {
                Game.ShowHitboxes = !Game.ShowHitboxes;

                Game.ChangeState(Game.GameState.Running);
            }),

            new MenuItem("Quit Game", () => { Environment.Exit(0); })
        ];

        // Position menu items vertically
        for (var i = 0; i < MenuItems.Count; i++)
        {
            MenuItems[i].Position = new Vector2(150, 150 + i * 50);
        }
        
        Construct();
    }

    public override void Render(Batcher batcher)
    {
        // draw (semi-transparent) background
        batcher.Rect(new Rect(Vector2.Zero, Game.Instance.Window.Size), Color.Black * 0.5f);
        
        batcher.Text(Assets.SpriteFonts["Renogare"], "Pause Menu", new Vector2(100, 100), Color.White);
        
        base.Render(batcher);
    }
}