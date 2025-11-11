using Microsoft.Xna.Framework;
using System;

namespace MongameSummer
{
    internal class EnemySpawner : IUpdateable
    {
        private TowerGrid grid;
        private Random random = new Random();

        private float spawnTimer = 0f;
        private float spawnIntervalSeconds = 4f;
        private int xSpawnOffset = 900;

        public EnemySpawner(TowerGrid grid)
        {
            this.grid = grid;
        }

        public void Update(GameTime gameTime)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            spawnTimer += delta;

            if (spawnTimer >= spawnIntervalSeconds)
            {
                spawnTimer = 0f;
                SpawnRandomEnemy();
            }
        }

        private void SpawnRandomEnemy()
        {
            int lane = random.Next(0, grid.Rows); // pick random lane

            // Randomly pick Goblin, Zombie, or Hound
            Enemy enemy;
            double choice = random.NextDouble();
            if (choice < 0.33)
                enemy = EnemyFactory.Create("goblinEnemy");
            else if (choice < 0.66)
                enemy = EnemyFactory.Create("zombieEnemy");
            else
                enemy = EnemyFactory.Create("houndEnemy");

            enemy.Lane = lane;
            enemy.position = new Vector2(Game1.ScreenCenterWidth + xSpawnOffset, grid[lane, 0].Bounds.Center.Y);

            SceneManager.Add(enemy);
        }
    }
}
