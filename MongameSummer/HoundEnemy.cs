using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongameSummer
{
    internal class HoundEnemy : Enemy
    {
        public HoundEnemy() : base("houndEnemy")
        {
            health = 40;
            speed = 150f;
            attackPower = 12;
            attackCooldown = 3f;
        }
    }
}
