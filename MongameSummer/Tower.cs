using Microsoft.Xna.Framework;
using MongameSummer;

public class Tower : Animation
{
    protected float defaultScale = 0.25f;
    public Collider collider;
    public int health = 100;

    public Tile Tile { get; set; }

    public Tower(string spriteName) : base(spriteName)
    {
        Play(true, 12);

        scale = new Vector2(defaultScale, defaultScale);

        collider = SceneManager.Create<Collider>();
        collider.isTrigger = false;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        // Keeps collider in sync
        collider.DestRectangle = DestRectangle;
    }

    public virtual void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
            SceneManager.Remove(this);
    }
}
