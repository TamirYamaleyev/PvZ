using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongameSummer
{
    internal class SlimeEnemy : Enemy
    {
        public SlimeEnemy() : base("slimeEnemy")
        {
            health = 150;
            speed = 60f;
            attackPower = 75;
            attackCooldown = 3f;
        }
    }
}
