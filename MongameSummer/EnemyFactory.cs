using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MongameSummer
{
    internal static class EnemyFactory
    {
        public static Enemy Create(string type)
        {
            return type switch
            {
                "goblinEnemy" => new GoblinEnemy(),
                "zombieEnemy" => new SlimeEnemy(),
                "houndEnemy" => new HoundEnemy(),
                _ => null
            };
        }
    }
}
