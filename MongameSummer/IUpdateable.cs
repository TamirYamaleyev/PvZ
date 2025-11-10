using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MongameSummer;

public interface IUpdateable
{
    void Update(GameTime gameTime);
}