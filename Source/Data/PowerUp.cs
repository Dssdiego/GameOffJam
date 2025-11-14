namespace GameOffJam;

public class PowerUp
{
    public enum EType
    {
        None,
        BoostSpeed,
        ShootSpeed,
        Invincibility,
    }

    public enum EOperation
    {
        Increase,
        Multiply,
        Decrease,
        Divide
    }
    
    public EType Type = EType.None;
    public EOperation Operation;
    public float Value;

    public PowerUp(EType type, float value)
    {
        Type = type;
        Value = value;
    }
    
    public void ApplyToPlayer()
    {
        var player = Game.Instance.World.GetFirstActorWithMask(Actor.Masks.Player) as Player;
        if (player == null)
        {
            return; 
        }

        if (Type == EType.BoostSpeed)
        {
            player.BoostSpeed += Value;
        }
    }
}