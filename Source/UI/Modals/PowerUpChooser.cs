namespace GameOffJam;

public class PowerUpChooser : UI
{
    public Controls Controls;
    
    private List<Card> powerUpCards = [];
    
    private Vector2 basePosition = new(200, 200);

    private int offsetX = 200;
    private int selectedCardIdx;
    
    public List<(int, PowerUp)> Progression =
    [
        // wave 1
        new ValueTuple<int, PowerUp>(1, new PowerUp( PowerUp.EType.BoostSpeed, 10)),
        new ValueTuple<int, PowerUp>(1, new PowerUp( PowerUp.EType.BoostSpeed, 30)),
        new ValueTuple<int, PowerUp>(1, new PowerUp( PowerUp.EType.BoostSpeed, 50)),
        
        // wave 2
        new ValueTuple<int, PowerUp>(2, new PowerUp( PowerUp.EType.ShootSpeed, 45)),
        new ValueTuple<int, PowerUp>(2, new PowerUp( PowerUp.EType.ShootSpeed, 25)),
        new ValueTuple<int, PowerUp>(2, new PowerUp( PowerUp.EType.ShootSpeed, 15))
    ];

    public PowerUpChooser(Controls controls)
    {
        Controls = controls;

        powerUpCards.Add(new Card(Progression[0].Item2));
        powerUpCards.Add(new Card(Progression[1].Item2));
        powerUpCards.Add(new Card(Progression[2].Item2));
        
        UpdateSelection();
    }

    private void UpdateSelection()
    {
        foreach (var card in powerUpCards)
        {
            card.SetState(Card.State.Normal);
        }
        
        powerUpCards[selectedCardIdx].SetState(Card.State.Selected);
    }

    public override void Update()
    {
        // navigate right
        if (Controls.MenuNavRight.ConsumePress())
        {
            if (selectedCardIdx < powerUpCards.Count - 1)
            {
                selectedCardIdx++;
            }
            else
            {
                selectedCardIdx = 0;
            }

            UpdateSelection();
        }

        // navigate left
        if (Controls.MenuNavLeft.ConsumePress())
        {
            if (selectedCardIdx > 0)
            {
                selectedCardIdx--;
            }
            else
            {
                selectedCardIdx = powerUpCards.Count - 1;
            }
            
            UpdateSelection();
        }

        // confirm/press
        if (Controls.MenuConfirm.ConsumePress())
        {
            // execute the action of the "power up" card
            powerUpCards[selectedCardIdx].PowerUp.ApplyToPlayer();
            
            // make the game "run" again
            Game.ChangeState(Game.GameState.Running);
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