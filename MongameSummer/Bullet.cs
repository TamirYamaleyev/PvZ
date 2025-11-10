using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MongameSummer
{
    internal class Bullet : Animation
    {
        protected float defaultScale = 0.3f;

        public Collider collider;
        public int damage = 25;
        public float speed = 500f;
        public Vector2 direction;

        public Bullet(string spriteName, Vector2 startPos, Vector2 direction) : base(spriteName)
        {
            Play(true, 12);
            scale = new Vector2(defaultScale, defaultScale);

            this.position = startPos;
            this.direction = direction;

            collider = SceneManager.Create<Collider>();
            collider.isTrigger = true;
        }

        public override void Update(GameTime gameTime)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            MoveBullet(delta);

            collider.DestRectangle = DestRectangle;

            base.Update(gameTime);

            if (position.X < 0 || position.X > Game1.ScreenWidth || 
                position.Y < 0 || position.Y > Game1.ScreenHeight)
            {
                SceneManager.Remove(this);
            }
        }

        private void MoveBullet(float delta)
        {
            position += direction * speed * delta;
        }
    }
}
