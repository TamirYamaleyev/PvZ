using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongameSummer
{
    internal class BombConsumable : Tower
    {
        private float fuseTime = 3f;
        private float explosionDuration = 1f; // For animation
        private float timer = 0f;
        private bool hasExploded = false;

        private float explosionRadius = 200f;
        private int explosionDamage = 999;
        public BombConsumable() : base("bombConsumable", 150)
        {
            scale = new Vector2(3f, 3f);
            PreviewSpriteName = "bombConsumablePreview";
        }

        public override void Update(GameTime gameTime)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timer += delta;

            if (!hasExploded && timer >= fuseTime)
            {
                Explode();
                timer = 0f; // reset to start counting for explosion duration
                hasExploded = true;
            }
            else if (hasExploded && timer >= explosionDuration)
            {
                SceneManager.Remove(this);
            }

                base.Update(gameTime);
        }

        private void Explode()
        {
            ChangeAnimation(SpriteManager.GetSprite("bombExplosion"), true, false, 12);
            scale = new Vector2(1f, 1f);

            var updatables = SceneManager.Instance.GetAllUpdatables().ToList();

            foreach (var obj in updatables)
            {
                if (obj is not Enemy enemy)
                    continue;

                Vector2 consumableCenter = new Vector2(DestRectangle.Center.X, DestRectangle.Center.Y);
                Vector2 enemyCenter = new Vector2(enemy.DestRectangle.Center.X, enemy.DestRectangle.Center.Y);

                float distance = Vector2.Distance(consumableCenter, enemyCenter);

                if (distance <= explosionRadius)
                {
                    enemy.TakeDamage(explosionDamage);
                }
            }
        }
    }
}
