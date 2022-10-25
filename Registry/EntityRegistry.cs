using Roguelike.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike.Registry
{
    public class EntityRegistry
    {
        // just few lists of all entities
        public List<BaseEntity> Enemies;
        public List<BaseEntity> Npcs;

        public EntityRegistry()
        {
            Enemies = new List<BaseEntity>();
            Npcs = new List<BaseEntity>();
            Enemies.Add(new EnemyEntity(8, "Rat", 1, 2, 1, "\u001b[42mA\u001b[0m"));
        }
    }
}
