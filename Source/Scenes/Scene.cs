namespace GameOffJam;

public abstract class Scene
{
    public virtual void Update() { }

    public virtual void Render(Batcher batcher) { }
}