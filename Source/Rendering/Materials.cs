namespace GameOffJam;

public class DefaultMaterial : Material
{
    // TODO: Implement me
    public string Name = string.Empty;

    public Texture? Texture
    {
        get => Fragment.Samplers[0].Texture;
        set => Fragment.Samplers[0] = new(value, new TextureSampler(TextureFilter.Nearest, TextureWrap.Repeat, TextureWrap.Repeat));
    }
    
    
}