using ldtk;
using Newtonsoft.Json;

namespace GameOffJam;

public class LDTKMap : Map
{
    // [Flags]
    // public enum TileId
    // {
    //     Unknown     = 0,
    //     Wall        = 1
    // }
    
    public LDTKMap(string name, string file) : base(name, file) { }
    
    public override void LoadResources(in World world, string levelName)
    {
        base.LoadResources(world, levelName);
        
        var ldtkData = LdtkJson.FromJson(File.ReadAllText(FilePath));

        var levels = ldtkData.Levels;

        // iterate through all levels
        foreach (var level in levels)
        {
            if (level.Identifier != levelName)
            {
                continue;
            }
            
            var layerInstances = level.LayerInstances;
            
            // iterate through all layer instances
            foreach (var layerInstance in layerInstances)
            {
                switch (layerInstance.Identifier)
                {
                    case "Tiles":
                        LoadTiles(layerInstance.GridTiles);
                        break;
                    case "Entities":
                        LoadEntities(layerInstance.EntityInstances, layerInstance.GridSize);
                        break;
                }
            }
        }
    }

    private void LoadTiles(TileInstance[] tileInstances)
    {
        foreach (var tileInstance in tileInstances)
        {
            // World.Spawn<Tile>(new Vector2(tileInstance.Px[0], tileInstance.Px[1]));
        }
    }

    private void LoadEntities(EntityInstance[] entityInstances, long gridSize)
    {
        foreach (var entityInstance in entityInstances)
        {
            Vector2 gridPos = new Vector2(entityInstance.Grid[0], entityInstance.Grid[1]);
            Vector2 spawnPosition = new Vector2(gridPos.X * gridSize, gridPos.Y * gridSize); // TODO: Get the tile size from the map

            switch (entityInstance.Identifier)
            {
                case "Player":
                {
                    // World.PlayerRespawnPosition = spawnPosition;
                    // var player = World.Spawn<Player>(spawnPosition);
                    // player.SetOriginalPosition(spawnPosition);
                    break;
                }
            }
        }
    }
    
    private dynamic GetEntityProperty(EntityInstance entityInstance, string propertyName)
    {
        var fieldInstances = entityInstance.FieldInstances;

        foreach (var fieldInstance in fieldInstances)
        {
            if (fieldInstance.Identifier == propertyName)
            {
                return fieldInstance.Value;
            }
        }

        return 0;
    }
}