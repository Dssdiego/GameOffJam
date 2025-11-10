using System.Collections.Concurrent;
using System.Diagnostics;

using Path = System.IO.Path;

namespace GameOffJam;

public class Assets
{
    public static readonly Dictionary<string, Font> Fonts = new(StringComparer.OrdinalIgnoreCase);
    public static readonly Dictionary<string, Image> Textures = new(StringComparer.OrdinalIgnoreCase);
    public static readonly Dictionary<string, Subtexture> Subtextures = new(StringComparer.OrdinalIgnoreCase);
    public static readonly Dictionary<string, Tileset> Tilesets = new(StringComparer.OrdinalIgnoreCase);
    public static readonly Dictionary<string, Aseprite> Aseprites = new(StringComparer.OrdinalIgnoreCase);
    public static readonly Dictionary<string, Sprite> Sprites = new(StringComparer.OrdinalIgnoreCase);
    public static readonly Dictionary<string, SpriteFont> SpriteFonts = new(StringComparer.OrdinalIgnoreCase);
    public static readonly Dictionary<string, Model> Models = new(StringComparer.OrdinalIgnoreCase);
    public static readonly Dictionary<string, TiledMap> TiledMaps = new(StringComparer.OrdinalIgnoreCase);
    public static readonly Dictionary<string, LDTKMap> LDTKMaps = new(StringComparer.OrdinalIgnoreCase);
    public static readonly Dictionary<string, TrenchBroomMap> TrenchBroomMaps = new(StringComparer.OrdinalIgnoreCase);
    
    private static readonly bool exportAtlas = true; 
    
    public static void Init(GraphicsDevice graphicsDevice)
    {
        Log.Info("Loading assets...");
        
        Stopwatch timer = Stopwatch.StartNew();
        
        // clear lists before loading
        // Maps.Clear();
        Fonts.Clear();
        Textures.Clear();
        Subtextures.Clear();
        Tilesets.Clear();
        Aseprites.Clear();
        SpriteFonts.Clear();
        Models.Clear();
        TiledMaps.Clear();
        LDTKMaps.Clear();
        TrenchBroomMaps.Clear();
        
        // var maps = new ConcurrentBag<TiledMap>();
        List<Task> tasks = new List<Task>();

        tasks.Add(Task.Run(() =>
        {
            LoadFonts();
            LoadSpriteFonts(graphicsDevice);
            LoadAseprites();
            LoadTextures(graphicsDevice);
            LoadTilesets();
            LoadModels();
            
            LoadMaps();

            PackAtlas(graphicsDevice);
        }));

        // Wait for tasks to complete
        foreach (var task in tasks)
        {
            task.Wait();
        }
        
        // Construct models
        foreach (var model in Models)
        {
            model.Value.Construct(graphicsDevice);
        }
        
        Log.Info($"Loaded assets in {timer.ElapsedMilliseconds}ms");
    }
    
    #region Loaders
    private static void LoadFonts()
    {
        var fontsPath = Path.Join(AssetUtils.GetContentPath, "Fonts");
        foreach (var file in Directory.EnumerateFiles(fontsPath, "*.*", SearchOption.AllDirectories))
        {
            if (file.EndsWith(".ttf") || file.EndsWith(".otf"))
            {
                Fonts.Add(AssetUtils.GetResourceName(fontsPath, file), new Font(file));
            }
        }
    }

    private static void LoadSpriteFonts(GraphicsDevice graphicsDevice)
    {
        foreach (var font in Fonts)
        {
            SpriteFont spriteFont = new SpriteFont(graphicsDevice, font.Value, 24);
            SpriteFonts.Add(font.Key, spriteFont);
        }
    }

    private static void LoadTextures(GraphicsDevice graphicsDevice)
    {
        var texturesPath = Path.Join(AssetUtils.GetContentPath, "Textures");
        foreach (var file in Directory.EnumerateFiles(texturesPath, "*.png", SearchOption.AllDirectories))
        {
            Textures.Add(AssetUtils.GetResourceName(texturesPath, file), new Image(file));
        }
    }
    
    private static void LoadAseprites()
    {
        var asepritePath = Path.Join(AssetUtils.GetContentPath, "Aseprite");
        var asepriteExtensions = new[] { ".ase", ".aseprite" };

        foreach (var file in Directory.EnumerateFiles(asepritePath, "*.*", SearchOption.AllDirectories).Where(f => asepriteExtensions.Contains(Path.GetExtension(f), StringComparer.OrdinalIgnoreCase)))
        {
            var name = Path.ChangeExtension(Path.GetRelativePath(asepritePath, file), null);
            
            var ase = new Aseprite(file);
            if (ase.Frames.Length > 0)
            {
                Aseprites.Add(name, ase);
            }
        }
    }
    
    private static void LoadTilesets()
    {
        var tilesetsPath = Path.Join(AssetUtils.GetContentPath, "Tilesets");
        foreach (var file in Directory.EnumerateFiles(tilesetsPath, "*.png", SearchOption.AllDirectories))
        {
            var name = AssetUtils.GetResourceName(tilesetsPath, file);

            var tileset = new Tileset(name, file, 16);
            Tilesets.Add(name, tileset);
        }
    }
    
    private static void LoadModels()
    {
        var modelsPath = Path.Join(AssetUtils.GetContentPath, "Models");
        foreach (var file in Directory.EnumerateFiles(modelsPath, "*.glb", SearchOption.AllDirectories))
        {
            var name = AssetUtils.GetResourceName(modelsPath, file);

            var modelInput = ModelRoot.Load(file);
            var model = new Model(modelInput);
            Models.Add(name, model);
        }
    }
    
    private static void LoadMaps()
    {
        var mapsPath = Path.Join(AssetUtils.GetContentPath, "Maps");
        
        LoadTiledMaps(mapsPath);
        LoadLDTKMaps(mapsPath);
        LoadTrenchBroomMaps(mapsPath);
    }
    
    private static void LoadTiledMaps(string mapsPath)
    {
        foreach (var file in Directory.EnumerateFiles(mapsPath, "*.tmx", SearchOption.AllDirectories))
        {
            var name = AssetUtils.GetResourceName(mapsPath, file);
        
            var map = new TiledMap(name, file);
            TiledMaps.Add(name, map);
        }
    }

    private static void LoadLDTKMaps(string mapsPath)
    {
        foreach (var file in Directory.EnumerateFiles(mapsPath, "*.ldtk", SearchOption.AllDirectories))
        {
            var name = AssetUtils.GetResourceName(mapsPath, file);

            var map = new LDTKMap(name, file);
            LDTKMaps.Add(name, map);
        }
    }
    
    private static void LoadTrenchBroomMaps(string mapsPath)
    {
        foreach (var file in Directory.EnumerateFiles(mapsPath, "*.map", SearchOption.AllDirectories))
        {
            var name = AssetUtils.GetResourceName(mapsPath, file);
        
            var map = new TrenchBroomMap(name, file);
            TrenchBroomMaps.Add(name, map);
        }
    }
    #endregion
    
    #region Packing
    private static void PackAtlas(GraphicsDevice graphicsDevice)
    {
        var texturePackOutput = CreateTexturePackOutput();
        var asepritePackOutput = CreateAsepritePackOutput();
        // var outputTilesetPack = UnpackTilesets();
        
        var atlasPacker = new AtlasPacker();
        atlasPacker.ExportAtlas = exportAtlas;
        atlasPacker.Pack(texturePackOutput, graphicsDevice, "TextureAtlas", Subtextures);
        atlasPacker.Pack(asepritePackOutput, graphicsDevice, "AsepriteAtlas", Subtextures);

        // create sprite assets
        foreach (var (name, ase) in Aseprites)
        {
            // find origin
            Vector2 origin = Vector2.Zero;
            if (ase.Slices.Count > 0 && ase.Slices[0].Keys.Length > 0 && ase.Slices[0].Keys[0].Pivot.HasValue)
            {
                origin = ase.Slices[0].Keys[0].Pivot!.Value;
            }
        
            var sprite = new Sprite(name, origin);
        
            // add frames
            for (int i = 0; i < ase.Frames.Length; i++)
            {
                sprite.Frames.Add(new(AssetUtils.GetSubtexture($"{name}/{i}"), ase.Frames[i].Duration / 1000.0f));
            }
        
            // add animations
            foreach (var tag in ase.Tags)
            {
                if (!string.IsNullOrEmpty(tag.Name))
                {
                    sprite.AddAnimation(tag.Name, tag.From, tag.To - tag.From + 1);
                }
            }
            
            Sprites.Add(name, sprite);
        }
    }

    private static Packer.Output CreateTexturePackOutput()
    {
        var packer = new Packer();

        foreach (var (name, img) in Textures)
        {
            packer.Add($"{name}", img);
            // Log.Info($"Packing {name}...");
        }
    
        return packer.Pack();
    }
    
    private static Packer.Output CreateAsepritePackOutput()
    {
        var packer = new Packer();
        
        foreach (var (name, ase) in Aseprites)
        {
            var frames = ase.RenderAllFrames();
            for (int i = 0; i < frames.Length; i++)
            {
                packer.Add($"{name}/{i}", frames[i]);
            }
        }
    
        return packer.Pack();
    }

    private static Packer.Output UnpackTilesets()
    {
        var packer = new Packer();

        foreach (var (name, tileset) in Tilesets)
        {
            tileset.UnPack();
            
            // TODO: Need to separate tiles before packing. The goal here is to pack individual tiles
            // packer.Add($"{name}", img);
            // Log.Info($"Packing {name}...");
        }
        
        return packer.Pack();
    }
    
    #endregion // Packing
}
