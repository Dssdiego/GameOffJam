namespace GameOffJam;

public class BulletBomber : Actor
{
    public int NumOfBullets = 25;

    private bool bExploded;
    
    public override void Update()
    {
        base.Update();

        if (bExploded)
        {
            return;
        }

        // 1 second after spawn, explode into a lot of bullets
        if (Timer > 1f)
        {
            Explode();
        }
    }

    private void Explode()
    {
        bExploded = true;
        
        var angleOffset = 360f / NumOfBullets;
        var spawnAngle = 0f;
        
        for (int i = 0; i < NumOfBullets; i++)
        {
            spawnAngle += angleOffset;

            var direction = new Vector2
            {
                X = (float) Math.Cos(spawnAngle),
                Y = (float) Math.Sin(spawnAngle)
            };

            var bulletSpawned = World.Spawn<Bullet>(Position);
            bulletSpawned.Setup(direction);
        }
        
        World.DestroyActor(this);
    }

    public override void Render(Batcher batcher)
    {
        base.Render(batcher);

        if (!bExploded)
        {
            batcher.CircleDashed(new Circle(Position, 20), 2, 10, Color.Red, 2, 2);
        }
    }
}