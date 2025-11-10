namespace GameOffJam;

public class AtlasPacker
{
    public bool ExportAtlas = true;
        
    public void Pack(Packer.Output packerOutput, GraphicsDevice graphicsDevice, string name, in Dictionary<string, Subtexture> subtexturesDict)
    {
        if (!ExportAtlas || packerOutput.Pages.Count <= 0)
        {
            return;
        }
        
        var atlas = new Texture(graphicsDevice, packerOutput.Pages[0], name);

        for (var i = 0; i < packerOutput.Pages.Count; i++)
        {
            packerOutput.Pages[i].WritePng(name + "_" + i + ".png");
        }
        
        // create subtextures
        foreach (var entry in packerOutput.Entries)
        {
            subtexturesDict.Add(entry.Name, new Subtexture(atlas, entry.Source, entry.Frame));
        }
    }
}