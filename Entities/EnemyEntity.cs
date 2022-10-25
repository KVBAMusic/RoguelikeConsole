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
        public EnemyEntity(int hp, string name, int attSpeed, int damage, int moveSpeed, string display)
        {
            DisplayString = display;
            base.Init(hp, name, new EntityStats(attSpeed, damage, moveSpeed));
        }

        public EnemyEntity() {  }

    }
}
