namespace GameOffJam;

public class PowerUpChooser : UI
{
    public Controls Controls;
    
    private List<Card> powerUpCards = [];
    
    private Vector2 basePosition = new(200, 200);

    private int offsetX = 200;

    public PowerUpChooser(Controls controls)
    {
        Controls = controls;
        
        powerUpCards.Add(new Card(true));
        powerUpCards.Add(new Card());
        powerUpCards.Add(new Card());
    }

    public override void Update()
    {
        // navigate left
        if (Controls.MenuNavLeft.ConsumePress())
        {
            
        }

        // navigate right
        if (Controls.MenuNavRight.ConsumePress())
        {
            
        }

        // confirm/press
        if (Controls.MenuConfirm.ConsumePress())
        {
            // TODO: Apply the buff in the game
        }
    }

    public override void Render(Batcher batcher)
    {
        // draw (semi-transparent) background
        batcher.Rect(new Rect(Vector2.Zero, Game.Instance.Window.Size), Color.Black * 0.5f);

        // draw title
        batcher.Text(Assets.SpriteFonts["Renogare"], "Level 1", new Vector2(Game.Instance.Window.Width/2 - 50, 50), Color.White);
        
        // draw cards
        {
            var offset = 0;
        
            foreach (var powerUp in powerUpCards)
            {
                powerUp.Position = basePosition with { X = basePosition.X + offset };
                powerUp.Render(batcher);

                offset += offsetX;
            }
            
        }
    }
}