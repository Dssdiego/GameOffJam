namespace GameOffJam;

public class Tileset
{
    public string Name;
    public string File;
    public int TileSize;

    private List<Packer.Entry> entries = [];
    private Image image;
    
    public Tileset(string name, string file, int tileSize)
    {
        Name = name;
        File = file;
        TileSize = tileSize;

        image = new Image(file);
    }

    // Cuts the tileset into individual tiles
    // Useful to be used with the LDTK tileset (each tile is an index)
    //  Example: if the tileset is 160x160 and tile size is 16, there will be 100 tiles (10x10)
    public void UnPack()
    {
        var rows = image.Width / TileSize;
        var cols = image.Height / TileSize;
        
        var indexMax = rows * cols;

        entries.EnsureCapacity(indexMax);

        // Create entries for each tile
        for (int i = 0; i < indexMax; i++)
        {
            // Calculate grid position
            var posX = (i % rows) * TileSize;
            var posY = (i / rows) * TileSize;
            
            var entry = new Packer.Entry(
                i,
                Name + "/" + i,
                0,
                new RectInt(posX, posY, 16, 16),
                new RectInt(16, 16)
            );
            
            entries.Add(entry);
        }
        
        // TODO: Now we have to make "subtextures" from these entries
        //       these subtextures can be used to render individual tiles (when LDTK gives us tile indices)
        
        Log.Info("Foobar");
    }
}