using Microsoft.Xna.Framework;
using MongameSummer;

public class Tower : Animation
{
    public Tower(string spriteName) : base(spriteName)
    {
        // automatically start animation
        Play(true, 10); // looping, 10 FPS by default
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        // optional: implement tower logic (shooting, etc.) here
    }
}
