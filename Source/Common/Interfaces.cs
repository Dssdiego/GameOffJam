namespace GameOffJam;

public interface IHaveModels
{
    public void CollectModels(List<(Actor Actor, Model Model)> populate);
}