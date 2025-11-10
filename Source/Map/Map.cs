namespace GameOffJam;

public class Map
{
    public World World;
    public string Name;
    public string FilePath;
    
    public Map(string name, string filePath)
    {
        Name = name;
        FilePath = filePath;
    }
    
    public virtual void LoadResources(in World world)
    {
        World = world;
    }

    public virtual void LoadResources(in World world, string levelName)
    {
        World = world;
    }
}