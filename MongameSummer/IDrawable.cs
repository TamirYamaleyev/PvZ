using Microsoft.Xna.Framework.Graphics;

namespace MongameSummer;

public interface IDrawable : IUpdateable
{
    void Draw(SpriteBatch spriteBatch);
}