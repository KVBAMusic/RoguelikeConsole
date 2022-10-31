using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike.Entities
{
    public class EnemyEntity : BaseEntity
    {
        public EnemyEntity(EntityStats stats) : base (stats)
        {

        }

        public override void Tick()
        {
            // move in random direction

        }
    }
}
