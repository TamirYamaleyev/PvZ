using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongameSummer
{
    internal class GoldRigTower : Tower
    {
        private float generateCooldown = 5f;
        private float generateTimer = 0f;
        private int goldPerTick = 25;

        public GoldRigTower() : base("goldTower", 50)
        {
            health = 200;
            PreviewSpriteName = "goldTowerPreview";
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            generateTimer += delta;

            if (generateTimer >= generateCooldown)
            {
                GenerateGold();
                generateTimer = 0f;
            }
        }

        private void GenerateGold()
        {
            Game1.player.AddGold(goldPerTick);
        }
    }
}
