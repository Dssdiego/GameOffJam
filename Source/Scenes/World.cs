namespace GameOffJam;

public class World : Scene
{
    public readonly List<Actor> Actors = [];
    
    public LDTKMap LDTKMap;

    private string lastLevelLoadedName = "";
    
    private Time Time => Game.Instance.Time;
    
    private Vector2 cameraPos = Vector2.Zero;
    
    // NOTE: Mouse world position should take into account the game scale
    public Vector2 MousePos => Game.Instance.Input.Mouse.Position / Game.Scale;
    
    private readonly List<Actor> destroying = []; // Actors that are being destroyed this frame

    public World(string mapName)
    {
        // LDTKMap = Assets.LDTKMaps[mapName];
    }
    
    #region Load/Streaming
    public void Load(string levelName)
    {
        // TiledMap.LoadResources(this);
        // LDTKMap.LoadResources(this, levelName);
        
        // lastLevelLoadedName = levelName;
    }

    public void Reload()
    {
        // destroy all actors
        foreach (var actor in Actors)
        {
            DestroyActor(actor);
        }
        
        Actors.Clear();

        Load(lastLevelLoadedName);
    }
    #endregion
    
    #region Actor Handling 
    public T Spawn<T>(Vector2? position = null) where T : Actor, new()
    {
        var instance = new T
        {
            Game = Game.Instance,
            Controls = Game.Instance.Controls,
            Position = position ?? Point2.Zero
        };
        
        Actors.Add(instance);
        instance.SetWorld(this);
        instance.OnAddedToWorld();
        return instance;
    }
    public void DestroyActor(Actor actor)
    {
        if (!destroying.Contains(actor))
        {
            destroying.Add(actor);
        }
        // Actors[Actors.IndexOf(actor)] = null!; // Mark actor for destruction
    }
    #endregion
    
    #region Game Loop Methods
    public override void Update()
    {
        // clean up the actors that are being destroyed
        foreach (var actorBeingDestroyed in destroying)
        {
            // event that triggers when an actor is destroyed
            actorBeingDestroyed.OnDestroyed(); 
            
            // remove the actor from the game state
            Actors.Remove(actorBeingDestroyed);
        }
        destroying.Clear();
        
        // update actors
        // NOTE: Do not use a for-each loop here,
        //  as we are modifying the Actors list in realtime, and we need the list to be updated at all times
        for (int i = 0; i < Actors.Count; i++)
        {
            Actors[i].Update();
        }
    }

    public override void Render(Batcher batcher)
    {
        // Sorting actors by depth (before rendering them)
        Actors.Sort((a, b) => a.Depth.CompareTo(b.Depth));
        
        foreach (var actor in Actors)
        {
            // batcher.PushMatrix(actor.Position + cameraPos - new Vector2(500, 300));
            batcher.PushMatrix(Matrix3x2.CreateScale(Vector2.One) * Game.Scale); // apply scale
            
            // batcher.PushMatrix(actor.Position + cameraPos); // translate
            batcher.PushMatrix(actor.Position); // translate
            actor.Render(batcher);
            batcher.PopMatrix(); // pop translate
            
            batcher.PopMatrix(); // pop scale
        }
    }
    #endregion
    
    #region Getters
    public List<Actor> GetAllActorsWithMask(Actor.Masks mask)
    {
        return Actors.Where(actor => actor.Mask == mask && !destroying.Contains(actor)).ToList();
    }
    
    public Actor? GetFirstActorWithMask(Actor.Masks mask)
    {
        return Actors.FirstOrDefault(it => it.Mask.Has(mask) && !destroying.Contains(it));
    }
    #endregion
    
    #region Spatial | Overlaps
    // REVIEW: Should this be here instead of inside the Actor class?
    public Actor? OverlapsFirst(in Rect rect, Actor.Masks mask)
    {
        foreach (var actor in Actors)
        {
            var local = rect - actor.Position;
            if (actor.Mask.Has(mask) && actor.Hitbox.Overlaps(local))
            {
                return actor;
            }
        }

        return null;
    }
    #endregion
}