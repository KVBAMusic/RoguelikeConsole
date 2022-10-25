using System;

namespace Roguelike.Entities
{
    [Serializable]
    public struct EntityStats
    {
        public int attackSpeed;
        public int damage;
        public int moveSpeed;

        public EntityStats(int attSpeed, int damage, int moveSpeed)
        {
            this.attackSpeed = attSpeed;
            this.damage = damage;
            this.moveSpeed = moveSpeed;
        }
    }

    public class BaseEntity
    {
        Random _random;
        public string Name  { get; set; }
        public int PosX => posX;
        public int PosY => posY;
        public int HP => hp;

        public string DisplayString;
        public EntityStats stats;

        private int posX;
        private int posY;
        int hp;
        public BaseEntity()
        {
            _random = new Random();
        }

        public virtual void Init(int hp, string name, EntityStats stats)
        {
            this.hp = hp;
            Name = name;
            this.stats = stats;
        }

        public virtual void Spawn(int[,] Map)
        {
            do
            {
                posX = _random.Next(80);
                posY = _random.Next(24);
            } while (Map[posX, posY] != 1);
        }

        public virtual void Move(int deltaX, int deltaY, int[,] Map)
        {
            if (Map[PosX + deltaX, PosY + deltaY] != 2)
            {
                posX += deltaX;
                posY += deltaY;
            }
        }

        public virtual void Attack(BaseEntity target)
        {

        }
    }
}