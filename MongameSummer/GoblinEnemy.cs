using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongameSummer
{
    internal class GoblinEnemy : Enemy
    {
        public GoblinEnemy() : base("goblinEnemy")
        {
            health = 80;
            speed = 120f;
            attackPower = 60;
            attackCooldown = 1f;
        }
    }
}
