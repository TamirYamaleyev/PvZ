using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MongameSummer;

public class Collider : Sprite
{
    public bool isTrigger = false;

    #region  Debug Boundaries
    
    int thickness = 5;
    Color color = Color.White;

    #endregion
    
    public Collider() : base("pixel")
    {
    }

    public delegate void DoSomething(object obj);
    public event DoSomething OnCollision;
    public event DoSomething OnTrigger;
    public bool Intersect(Collider other)
    {
        if (DestRectangle == null)
            return false;
        
        return DestRectangle.Intersects(other.DestRectangle);
    }
    
    public void Notify(object obj)
    {
        if (isTrigger)
            OnTrigger?.Invoke(obj);
        else
            OnCollision?.Invoke(obj);
    }

    public override void Draw(SpriteBatch _spriteBatch)
    {
 
        
#if DEBUG
    
        // Draw outline
         
        // top
        _spriteBatch.Draw(_texture,
            new Rectangle(
                DestRectangle.X,
                DestRectangle.Y,
                DestRectangle.Width,
                thickness
            ), 
            color);
        
        // left
        _spriteBatch.Draw(_texture,
            new Rectangle(
                DestRectangle.X,
                DestRectangle.Y,
                thickness,
                DestRectangle.Height
            ), 
            color);
        
        // right
        _spriteBatch.Draw(_texture,
            new Rectangle(
                DestRectangle.X + DestRectangle.Width - thickness,
                DestRectangle.Y,
                thickness,
                DestRectangle.Height
            ), 
            color);
        
        // bottom
        _spriteBatch.Draw(_texture,
            new Rectangle(
                DestRectangle.X,
                DestRectangle.Y + DestRectangle.Height - thickness,
                DestRectangle.Width,
                thickness
            ), 
            color);
    #endif
        
        
    }
}
    