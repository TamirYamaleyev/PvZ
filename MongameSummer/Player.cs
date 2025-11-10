using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MongameSummer;

public class Player : Animation
{
    public Collider collider;
    
    Vector2 prevPosition;
    public Player() : base("egret")
    {
        collider = SceneManager.Create<Collider>();
    }

    public void OnCollision(object obj)
    {
        position = prevPosition;
    }
    
    public void OnTrigger(object obj)
    {
        if (obj == null) return;
        
        SceneManager.Remove(((Enemy)obj).collider);
        SceneManager.Remove((IUpdateable)obj);
    }

    private float speed = 0.2f;
    public override void Update(GameTime gameTime)
    {
        prevPosition = position;
        
        KeyboardState state = Keyboard.GetState();


        if (state.IsKeyDown(Keys.Right))
        {
            position += speed * Vector2.UnitX * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            effects = SpriteEffects.FlipHorizontally;
        }
        if (state.IsKeyDown(Keys.Left))
        {
            position += -(speed * Vector2.UnitX)* (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            effects = SpriteEffects.None;
        }
        
        if (state.IsKeyDown(Keys.Down))
        {
            position += speed * Vector2.UnitY * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }
        if (state.IsKeyDown(Keys.Up))
        {
            position += -(speed * Vector2.UnitY)* (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        base.Update(gameTime);

        collider.DestRectangle = DestRectangle;
    }
}