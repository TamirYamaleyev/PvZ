using Microsoft.Xna.Framework;

namespace MongameSummer;

public class Enemy : Animation
{
    public Collider collider;

    public float speed = 50f;
    public int health = 100;

    public Enemy() : base("zombie")
    {
        Play(true, 12);
        scale = new Vector2(0.25f, 0.25f);

        collider = SceneManager.Create<Collider>();
        collider.isTrigger = true;
    }

    public override void Update(GameTime gameTime)
    {
        float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
        position.X -= speed * delta;

        collider.DestRectangle = DestRectangle;

        base.Update(gameTime);

        if (position.X < 0)
            SceneManager.Remove(this);        
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
            SceneManager.Remove(this);
    }
}