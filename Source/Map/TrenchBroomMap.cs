
namespace GameOffJam;

/// <summary>
/// Represents a map loaded from a TrenchBroom file (Quake Map).
/// </summary>
public class TrenchBroomMap : Map
{
    public TrenchBroomMap(string name, string filePath) : base(name, filePath) { }

    public override void LoadResources(in World world)
    {
        base.LoadResources(in world);
        
        var format = new QuakeMapFormat();
        var mapFile = format.ReadFromFile(FilePath);
        
        // Load the child objects of the 'Worldspawn'
        LoadObjects(mapFile.Worldspawn);
    }

    private void LoadObjects(SledgeMapObject obj)
    {
        foreach (SledgeMapObject childObj in obj.Children)
        {
            switch (childObj)
            {
                // entities
                case SledgeEntity entity:
                    LoadEntity(entity);
                    break;
                // solids
                case SledgeSolid solid:
                    LoadSolid(solid);
                    break;
                // everything else
                default:
                    LoadObjects(childObj);
                    break;
            }
        }
    }
    
    private void LoadEntity(SledgeEntity entity)
    {
        // TODO: Implement me
        if (entity.ClassName == "PlayerSpawn")
        {
        }
        
        if (entity.ClassName == " Cone")
        {
            // TODO: Create a 'Cone' actor at given position
        }
    }

    private void LoadSolid(SledgeSolid solid)
    {
        // TODO: Implement me
        // TODO: Load solid geometry (walls, grounds, etc)
    }
}