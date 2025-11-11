using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongameSummer
{
    internal static class TowerFactory
    {
        public static Tower CreateTower(string towerType)
        {
            return (towerType) switch
            {
                "defaultTower" => new defaultTower(),
                "goldTower" => new GoldRigTower(),
                //"sniperTower" => new defaultTower(),
                _ => null
            };
        }
    }
}
