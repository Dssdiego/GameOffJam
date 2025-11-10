global using Foster.Framework;
global using System.Numerics;

global using Matrix = System.Numerics.Matrix4x4;

global using Sledge.Formats.Map.Formats;
global using Sledge.Formats.Map.Objects;
global using SledgeSolid = Sledge.Formats.Map.Objects.Solid;
global using SledgeEntity = Sledge.Formats.Map.Objects.Entity;
global using SledgeFace = Sledge.Formats.Map.Objects.Face;
global using SledgeMap = Sledge.Formats.Map.Objects.MapFile;
global using SledgeMapObject = Sledge.Formats.Map.Objects.MapObject;

global using ModelRoot = SharpGLTF.Schema2.ModelRoot;
global using LogicalImage = SharpGLTF.Schema2.Image;
global using LogicalMesh = SharpGLTF.Schema2.Mesh;
global using MemoryImage = SharpGLTF.Memory.MemoryImage;

namespace GameOffJam
{
    public abstract class Globals
    {
        public static string GameName = "Game Off Jam 2025";
        
        public readonly struct Depth
        {
            public const int Player = 1;
        }
    }
}