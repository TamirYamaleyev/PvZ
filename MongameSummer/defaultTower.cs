using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongameSummer
{
    internal class defaultTower : Tower
    {
        public defaultTower() : base("defaultTower")
        {
            Play(true, 12);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // Shooting logic
        }
    }
}
