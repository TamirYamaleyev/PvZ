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
            shootCooldown = 1.2f;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (shootTimer >= shootCooldown)
            {
                if (EnemyInRange())
                {
                    Shoot();
                    shootTimer = 0f;
                }
            }
        }

        protected override void Shoot()
        {
            Vector2 spawnPos = new Vector2(DestRectangle.Right, DestRectangle.Center.Y);

            var bullet = new Bullet("bull", spawnPos, Vector2.UnitX);
            SceneManager.Add(bullet);
        }
    }
}
