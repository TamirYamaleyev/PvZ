using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongameSummer
{
    internal class defaultTower : Tower
    {
        public defaultTower() : base("defaultTower", 100)
        {
            shootCooldown = 2f;
            bulletDamage = 25;
            PreviewSpriteName = "defaultTowerPreview";
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Shoot()
        {
            Vector2 spawnPos = new Vector2(DestRectangle.Right, DestRectangle.Center.Y);

            var bullet = new Bullet("arrow", spawnPos, Vector2.UnitX);
            bullet.damage = bulletDamage;
            bullet.effects = SpriteEffects.FlipHorizontally;
            SceneManager.Add(bullet);
        }
    }
}
