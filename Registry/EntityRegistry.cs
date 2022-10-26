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
        public List<EntityStats> Enemies;
        public List<EntityStats> Npcs;

        public EntityRegistry()
        {
            Enemies = new List<EntityStats>();
            Npcs = new List<EntityStats>();

            Enemies.Add(new EntityStats("rat", "A", 8, 1, 2, 1));
        }
    }
}
