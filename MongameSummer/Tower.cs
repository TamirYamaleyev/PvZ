using Microsoft.Xna.Framework;
using MongameSummer;
using System;
using System.Linq;

public class Tower : Animation
{
    public Collider collider;

    protected int health = 100;
    protected float shootCooldown = 1f;
    protected float shootTimer = 0f;
    protected float rangeInPixels = 1000f;

    public Tile Tile { get; set; }

    public Tower(string spriteName) : base(spriteName)
    {
        Play(true, 12);

        scale = new Vector2(0.25f, 0.25f);

        collider = SceneManager.Create<Collider>();
        collider.isTrigger = false;
    }

    public override void Update(GameTime gameTime)
    {
        float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
        shootTimer += delta;

        // Keeps collider in sync
        collider.DestRectangle = DestRectangle;

        base.Update(gameTime);

        if (shootTimer >= shootCooldown && EnemyInRange())
        {
            Shoot();
            shootTimer = 0f;
        }
    }

    public virtual void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
            SceneManager.Remove(this);
    }

    protected virtual bool EnemyInRange()
    {
        var towerCenterX = position.X + DestRectangle.Width * 0.5f;

       var updatables = SceneManager.Instance.GetAllUpdatables();

        foreach (var obj in updatables)
        {
            if (obj is not Enemy enemy)
                continue;

            if (enemy.Lane != Tile.Row)
                continue;

            float enemyCenterX = enemy.position.X + enemy.DestRectangle.Width * 0.5f;
            float distance = enemyCenterX - towerCenterX;

            if (distance > 0 && distance <= rangeInPixels)
                return true;
        }

        return false;
    }

    protected virtual void Shoot()
    {
        // Overriden per tower type
    }
}
