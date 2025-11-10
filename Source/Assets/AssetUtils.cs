using Path = System.IO.Path;

namespace GameOffJam;

public class AssetUtils
{
    public const string AssetFolder = "Content";
    
    private static string? contentPath = null;
    
    public static string GetContentPath
    {
        get
        {
            if (contentPath == null)
            {
                var baseFolder = AppContext.BaseDirectory;
                var searchUpPath = "";
                int up = 0;
                
                while (!Directory.Exists(Path.Join(baseFolder, searchUpPath, AssetFolder)) && up++ < 5)
                {
                    searchUpPath = Path.Join(searchUpPath, "..");
                }

                if (!Directory.Exists(Path.Join(baseFolder, searchUpPath, AssetFolder)))
                {
                    throw new Exception($"Unable to find {AssetFolder} Directory from '{baseFolder}'");
                }
                
                contentPath = Path.Join(baseFolder, searchUpPath, AssetFolder);
            }

            return contentPath;
        }
    } 
    public static string GetResourceName(string contentFolder, string path)
    {
        var fullname = Path.Join(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path));
        var relative = Path.GetRelativePath(contentFolder, fullname);
        var normalized = relative.Replace("\\", "/");
        return normalized;
    }
    
    #region Getters
    public static Sprite? GetSprite(string name)
    {
        return Assets.Sprites.GetValueOrDefault(name);
    }
    
    public static Subtexture GetSubtexture(string name)
    {
        return Assets.Subtextures.TryGetValue(name, out var value) ? value : new Subtexture();
    }
    #endregion
}