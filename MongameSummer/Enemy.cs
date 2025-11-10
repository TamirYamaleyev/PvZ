using Microsoft.Xna.Framework;

namespace MongameSummer;

public class Enemy : Animation
{
    public Collider collider;
    private Player player;
    public Enemy() : base("duck")
    {
        collider = SceneManager.Create<Collider>();
        collider.isTrigger = true;
    }


    public void LinkWithPlayer(Player player)
    {
        this.player = player;

        if (collider.isTrigger)
            collider.OnTrigger += player.OnTrigger;
        else
            collider.OnCollision += player.OnCollision;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        
        collider.DestRectangle = DestRectangle;

        if (collider.Intersect(player.collider))
            collider.Notify(this);
        
    }
}