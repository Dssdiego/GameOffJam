namespace GameOffJam;

public class Controls(Input input)
{
    #region Player
    public readonly VirtualStick Move = new(input, "Move", 
        new StickBindingSet()
            .AddArrowKeys()
            .AddWasd()
            .AddDPad()
            .Add(Axes.LeftX, 0.25f, Axes.LeftY, 0.50f, 0.25f)
    );
    
    public readonly VirtualAction Shoot = new(input, "Shoot", 
        new ActionBindingSet()
            .Add(Keys.Space)
            .Add(Buttons.South)
    );

    public readonly VirtualAxis Boost = new VirtualAxis(input, "Boost", 
        new AxisBindingSet()
            .Add(Axes.RightTrigger)
    );
    #endregion
    
    #region UI
    public readonly VirtualAction PauseGame = new(input, "PauseGame", 
        new ActionBindingSet()
            .Add(Keys.Escape)
            .Add(Buttons.Start)
    );
    
    public readonly VirtualAction PowerUpChooser = new(input, "PowerUpChooser", 
        new ActionBindingSet()
            .Add(Keys.Backspace)
            .Add(Buttons.North)
    );
    
    public readonly VirtualAction MenuNavUp = new(input, "MenuNavUp", 
        new ActionBindingSet()
            .Add(Keys.W)
            .Add(Keys.Up)
            .Add(Buttons.Up)
            .AddLeftJoystickUp(0.5f)
        // TODO: Add support for "axis up"
    );
    
    public readonly VirtualAction MenuNavDown = new(input, "MenuNavDown", 
        new ActionBindingSet()
            .Add(Keys.S)
            .Add(Keys.Down)
            .Add(Buttons.Down)
            .AddLeftJoystickDown(0.5f)
        // TODO: Add support for "axis down"
    );
    
    public readonly VirtualAction MenuNavLeft = new(input, "MenuNavLeft", 
        new ActionBindingSet()
            .Add(Keys.A)
            .Add(Keys.Left)
            .Add(Buttons.West)
            .AddLeftJoystickLeft(0.5f)
        // TODO: Add support for "axis left"
    );
    
    public readonly VirtualAction MenuNavRight = new(input, "MenuNavRight", 
        new ActionBindingSet()
            .Add(Keys.D)
            .Add(Keys.Right)
            .Add(Buttons.East)
            .AddLeftJoystickRight(0.5f)
        // TODO: Add support for "axis left"
    );
    
    public readonly VirtualAction MenuConfirm = new(input, "MenuConfirm", 
        new ActionBindingSet()
            .Add(Keys.Enter)
            .Add(Buttons.South)
    );
    
    public readonly VirtualAction MenuBack = new(input, "MenuBack", 
        new ActionBindingSet()
            .Add(Keys.Backspace)
            .Add(Buttons.East)
        // TODO: Add support for the "escape" key
        // REVIEW: What about the "escape" unpausing the game? Can we consume the input first?
    );
    #endregion
}