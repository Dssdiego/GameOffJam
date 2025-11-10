using System.Runtime.InteropServices;

namespace GameOffJam;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly struct Vertex(Vector3 position, Vector2 uvs, Vector3 color, Vector3 normal, Vector4 joint, Vector4 weight) : IVertex
{
    public readonly Vector3 Position = position;
    public readonly Vector2 UVs = uvs;
    public readonly Vector3 Color = color;
    public readonly Vector3 Normal = normal;
    public readonly Vector4 Joint = joint;
    public readonly Vector4 Weight = weight;
    
    public Vertex(Vector3 position, Vector2 uvs, Vector3 color, Vector3 normal) : 
        this(position, uvs, color, normal, Vector4.Zero, Vector4.One) {}

    public VertexFormat Format => VertexFormat;
    
    public static readonly VertexFormat VertexFormat = VertexFormat.Create<Vertex>(
    [
            new (0, VertexType.Float3, Normalized: false),
            new (1, VertexType.Float2, Normalized: false),
            new (2, VertexType.Float3, Normalized: true),
            new (3, VertexType.Float3, Normalized: false),
            new (4, VertexType.Float4, Normalized: false),
            new (5, VertexType.Float4, Normalized: false)
    ]);
}