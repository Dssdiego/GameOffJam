using System.Runtime.InteropServices;
using SharpGLTF.Schema2;
using Image = Foster.Framework.Image;
using Texture = Foster.Framework.Texture;

namespace GameOffJam;

public class Model
{
    public struct MeshPrimitive
    {
        public int Material;
        public int Index;
        public int Count;
    }
    
    private ModelRoot root;
    
    // only used while loading, cleared afterwards
    public Mesh<Vertex> Mesh = null!;
    private readonly Dictionary<MemoryImage, Image> images = [];
    public readonly List<Vertex> Vertices = [];
    public readonly List<int> Indices = [];
    public readonly List<MeshPrimitive>[] Parts = [];
    public readonly DefaultMaterial[] Materials;
    
    public Model(ModelRoot modelRoot)
    {
        root = modelRoot;
        Parts = new List<MeshPrimitive>[modelRoot.LogicalMeshes.Count];
        
        CreateImages(root.LogicalImages);
        
        // upon creation, all materials use the "default" material
        Materials = new DefaultMaterial[root.LogicalMaterials.Count];

        CreateDataArrays(root.LogicalMeshes);
        
        // TODO: Need to call "Construct" after the loading is done to create the Model "in the GPU"
        Log.Info("Model created");
    }

    // Constructs the model's resources and disposes of any temporary data (like loaded images)
    public void Construct(GraphicsDevice graphicsDevice)
    {
        // create all the textures and clear the list of images we loaded before
        var textures = new Dictionary<MemoryImage, Texture>();
        foreach (var image in images)
        {
            textures[image.Key] = new Texture(graphicsDevice, image.Value);
        }
        images.Clear();
        
        // create all the materials, find their textures
        for (int i = 0; i < root.LogicalMaterials.Count; i++)
        {
            var logicalMat = root.LogicalMaterials[i];

            Materials[i] = new DefaultMaterial
            {
                Name = logicalMat.Name
            };

            // figure out which texture to use by just using the first texture found
            foreach (var channel in logicalMat.Channels)
            {
                if (channel.Texture != null && 
                    channel.Texture.PrimaryImage != null && 
                    textures.TryGetValue(channel.Texture.PrimaryImage.Content, out var texture))
                {
                    Materials[i].Texture = texture;
                    break;
                }
            }
        }

        // upload vertices/indices to the mesh
        Mesh = new Mesh<Vertex>(graphicsDevice);
        Mesh.SetVertices(CollectionsMarshal.AsSpan(Vertices));
        Mesh.SetIndices(CollectionsMarshal.AsSpan(Indices));
    }

    public void Render(ref RenderState renderState)
    {
        // renderState.GraphicsDevice.Draw(new DrawCommand(renderState.GraphicsDevice, Mesh, Materials[0]));
        renderState.Calls++;
        // TODO: renderState.Triangles += 
    }
    
    private void CreateImages(IReadOnlyList<LogicalImage> logicalImages)
    {
        foreach (var logicalImage in logicalImages)
        {
            var stream = new MemoryStream(logicalImage.Content.Content.ToArray());
            var image = new Image(stream);
            images.Add(logicalImage.Content, image);
        }
    }

    // Creates vertices and indexes arrays
    // Needed to upload vertex/index data to the GPU (later on)
    private void CreateDataArrays(IReadOnlyList<LogicalMesh> logicalMeshes)
    {
        for (int i = 0; i < logicalMeshes.Count; i++)
        {
            var logicalMesh = root.LogicalMeshes[i];
            var vertexCount = Vertices.Count;
            var indexStart = Indices.Count;
            var part = Parts[i] = new List<MeshPrimitive>();

            foreach (var primitive in logicalMesh.Primitives)
            {
                var verts = primitive.GetVertexAccessor("POSITION").AsVector3Array();
                var uvs = primitive.GetVertexAccessor("TEXCOORD_0").AsVector2Array();
                var normals = primitive.GetVertexAccessor("NORMAL").AsVector3Array();
                var weights = primitive.GetVertexAccessor("WEIGHTS_0");
                var joints = primitive.GetVertexAccessor("JOINTS_0");
                
                // vertices
                
                // not all primitives have weights/joints
                if (weights != null && joints != null)
                {
                    // TODO: Implement me
                    // REVIEW: Do we need weights and joints right now?
                }
                else
                {
                    for (int j = 0; j < verts.Count; j++)
                    {
                        Vertices.Add( new Vertex( verts[j], uvs[j], Vector3.One, normals[j], new Vector4(), Vector4.One ) );
                    }
                }
                
                // indices
                foreach (var index in primitive.GetIndices())
                {
                    Indices.Add(vertexCount + (int) index);
                }
                
                // parts
                part.Add(new MeshPrimitive
                {
                    Material = primitive.Material?.LogicalIndex ?? 0,
                    Index = indexStart,
                    Count = Indices.Count - indexStart
                });
            }
        }
    }
}