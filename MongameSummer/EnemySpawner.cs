using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongameSummer
{
    internal class EnemySpawner : IUpdateable
    {
        private TowerGrid grid;

        int xSpawnOffset = 900;
        int laneCount = 5;

        private float spawnTimer = 0f;
        private float spawnIntervalSeconds;
        private Random random = new Random();

        public EnemySpawner(TowerGrid grid, float interval = 3)
        {
            this.grid = grid;
            spawnIntervalSeconds = interval;
        }

        public void Update(GameTime gameTime)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            spawnTimer += delta;

            if (spawnTimer >= spawnIntervalSeconds)
            {
                spawnTimer = 0f;
                SpawnEnemy();
            }
        }

        private void SpawnEnemy()
        {
            int lane = random.Next(0, laneCount);

            var enemy = SceneManager.Create<Enemy>();
            enemy.Lane = lane;

            // Adjust Y to lock onto a lane(Row)
            enemy.position = new Vector2(Game1.ScreenCenterWidth + xSpawnOffset, grid[lane, 0].Bounds.Center.Y);
        }
    }
}
