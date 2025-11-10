namespace GameOffJam;

public class Menu : UI
{
    public List<MenuItem> MenuItems;

    public World World;
    public Controls Controls => Game.Instance.Controls;

    private int selectedIndex = 0;

    private bool wrapAroundOptions = true;
    
    /// Calling this method will initialize the menu items and set the first item as selected
    public void Construct()
    {
        // Select the first item by default
        MenuItems[selectedIndex].IsSelected = true;
    }
    
    // Will reset the selection to the first item
    public void ResetSelection()
    {
        selectedIndex = 0;
        UpdateSelection();
    }
    
    private void UpdateSelection()
    {
        for (var i = 0; i < MenuItems.Count; i++)
        {
            MenuItems[i].IsSelected = (i == selectedIndex);
        }
    }

    public override void Update()
    {
        // navigation down
        if (Controls.MenuNavDown.ConsumePress())
        {
            if (selectedIndex < MenuItems.Count - 1)
            {
                selectedIndex++;
                UpdateSelection();
            }
            else
            {
                if (wrapAroundOptions)
                {
                    // wrap around to the first item
                    selectedIndex = 0; 
                    UpdateSelection();
                }
            }
        }
        
        // navigation up
        if (Controls.MenuNavUp.ConsumePress())
        {
            if (selectedIndex > 0)
            {
                selectedIndex--;
                UpdateSelection();
            }
            else
            {
                if (wrapAroundOptions)
                {
                    // wrap around to the last item
                    selectedIndex = MenuItems.Count - 1; 
                    UpdateSelection();
                }
            }
        }

        if (Controls.MenuConfirm.ConsumePress())
        {
            MenuItems[selectedIndex].Pressed();
        }
    }

    public override void Render(Batcher batcher)
    {
        // REVIEW: Render pushing matrix as the menu position (?)
        
        foreach (var menuItem in MenuItems)
        {
            menuItem.Render(batcher);
        }
    }
}