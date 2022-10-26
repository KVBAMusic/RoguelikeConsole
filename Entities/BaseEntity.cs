using System;

namespace Roguelike.Entities
{
    [Serializable]
    public struct EntityStats
    {
        public string name;
        public string mapDisplay;
        public int attackSpeed;
        public int damage;
        public int moveSpeed;
        public int hp;

        public EntityStats(string name, string display, int hp, int attackSpeed, int damage, int moveSpeed)
        {
            this.name = name;
            this.mapDisplay = display;
            this.attackSpeed = attackSpeed;
            this.damage = damage;
            this.moveSpeed = moveSpeed;
            this.hp = hp;
        }
    }

    public class BaseEntity
    {
        Random _random;
        public string Name  => stats.name;
        public string MapDisplay => stats.mapDisplay;
        public int PosX => posX;
        public int PosY => posY;
        public int HP => stats.hp;
        public int MaxHP => maxHp;

        public EntityStats stats;

        private int posX;
        private int posY;

        private int maxHp;
        public BaseEntity(EntityStats stats)
        {
            this.stats = stats;
            _random = new Random();
            maxHp = stats.hp;
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