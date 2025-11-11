using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongameSummer
{
    internal class GoldGenerator : IUpdateable
    {
        private Player player;

        private float timer;
        private float interval;
        private int generateAmount;

        public GoldGenerator(Player player, float intervalSeconds = 8f, int goldAmount = 25)
        {
            this.player = player;
            interval = intervalSeconds;
            generateAmount = goldAmount;
        }

        public void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timer >= interval)
            {
                player.AddGold(generateAmount);
                timer = 0f;
            }
        }
    }
}
