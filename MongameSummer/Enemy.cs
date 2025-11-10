using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MongameSummer;

public class Enemy : Animation
{
    public Collider collider;

    public float defaultScale = 0.1f;

    public float speed = 50f;
    public int health = 100;
    public int attackPower = 10;

    private float attackCooldown = 1f;
    private float attackTimer = 0f;

    public int Lane;

    public Enemy() : base("zombie")
    {
        Play(true, 12);
        scale = new Vector2(defaultScale, defaultScale);

        collider = SceneManager.Create<Collider>();
        collider.isTrigger = true;
    }

    public override void Update(GameTime gameTime)
    {
        float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

        MoveEnemy(delta);

        collider.DestRectangle = DestRectangle;

        attackTimer += delta;

        CheckCollisions(delta);

        base.Update(gameTime);

        // Remove if off screen
        if (position.X + DestRectangle.Width < 0 || position.X > Game1.ScreenWidth ||
            position.Y + DestRectangle.Height < 0 || position.Y > Game1.ScreenHeight)
        {
            SceneManager.Remove(this);
        }
    }

    private void MoveEnemy(float delta)
    {
        position.X -= speed * delta;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
            SceneManager.Remove(this);
    }

    private void CheckCollisions(float delta)
    {
        var updatablesCopy = new List<IUpdateable>(SceneManager.Instance.GetAllUpdatables());

        foreach (var updatable in updatablesCopy)
        {
            if (updatable is Tower tower && tower.Tile.Row == Lane)
            {
                if (collider.Intersect(tower.collider))
                {
                    HandleCollisionWithTower(tower, delta);
                }
            }

            if (updatable is Bullet bullet && collider.Intersect(bullet.collider))
            {
                HandleCollisionWithBullet(bullet);
            }
        }
    }

    private void HandleCollisionWithTower(Tower tower, float delta)
    {
        position.X += speed * delta; // Pushback to prevent overlap

        if (attackTimer >= attackCooldown)
        {
            tower.TakeDamage(attackPower);
            attackTimer = 0f;
            // Add animations
        }
    }

    private void HandleCollisionWithBullet(Bullet bullet)
    {
        TakeDamage(bullet.damage);
        SceneManager.Remove(bullet);
    }
}