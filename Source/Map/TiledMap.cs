using TiledSharp;

namespace GameOffJam;

public class TiledMap : Map
{
    [Flags]
    public enum TileId
    {
        Unknown     = 0,
        Solid       = 1,
        PlayerSpawn = 2,
        EndArea     = 3
    }
    
    public TiledMap(string name, string file) : base(name, file) { }

    public override void LoadResources(in World world)
    {
        base.LoadResources(world);
        
        var mapData = new TmxMap(FilePath);
        
        // var jsonString = File.ReadAllText(FilePath);
        //
        // var ldtkData = LdtkJson.FromJson(File.ReadAllText(FilePath));
        
        LoadProperties(mapData);

        LoadSolidTiles(mapData);
        LoadSpawnTiles(mapData);
    }

    private void LoadProperties(in TmxMap mapData)
    {
        foreach (var prop in mapData.Properties)
        {
            switch (prop.Key)
            {
                case "WindDirection":
                {
                    // var windDirSplit = prop.Value.Split(',');
                    // var windDirection = new Vector2(
                    //     float.Parse(windDirSplit[0]),
                    //     float.Parse(windDirSplit[1])
                    // );
                    //
                    // World.WindDirection = windDirection;
                    break;
                }
            }
        }
    }

    private void LoadSolidTiles(in TmxMap mapData)
    {
        var solidLayer = mapData.Layers["Solids"];
        var solidTiles = solidLayer.Tiles;
        
        foreach (var solidTile in solidTiles)
        {
            if (solidTile.Gid == (int) TileId.Solid)
            {
                // World.Spawn<Wall>(new Vector2(solidTile.X * 16, solidTile.Y * 16));
            }
        }
    }

    private void LoadSpawnTiles(in TmxMap mapData)
    {
        var spawnLayer = mapData.Layers["Spawns"];
        var spawnTiles = spawnLayer.Tiles;
        
        foreach (var spawnTile in spawnTiles)
        {
            Vector2 spawnPosition = new Vector2(spawnTile.X * 16, spawnTile.Y * 16);
                
            switch (spawnTile.Gid)
            {
                case (int) TileId.PlayerSpawn:
                    // World.PlayerRespawnPosition = spawnPosition;
                    // World.Spawn<Player>(spawnPosition);
                    break;
                case (int) TileId.EndArea:
                    // World.Spawn<EndArea>(spawnPosition);
                    break;
            }
        }
    }

}