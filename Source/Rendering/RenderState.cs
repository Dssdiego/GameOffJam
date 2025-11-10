namespace GameOffJam;

public struct RenderState(GraphicsDevice graphicsDevice)
{
    public readonly GraphicsDevice GraphicsDevice = graphicsDevice;
    
    public int Calls = 0;
    public int Triangles = 0;
}