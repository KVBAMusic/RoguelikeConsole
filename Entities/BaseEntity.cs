using System;

namespace Roguelike.Entities
{
    public class MoveEventArgs : EventArgs
    {
        public BaseEntity Entity { get; private set; }
        public int deltaX { get; private set; }
        public int deltaY { get; private set; }

        public MoveEventArgs(BaseEntity entity, int deltaX, int deltaY)
        {
            Entity = entity;
            this.deltaX = deltaX;
            this.deltaY = deltaY;
        }
    }

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
        public event EventHandler<MoveEventArgs> OnMove;

        protected Random _random;
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

        protected bool moved;
        public BaseEntity(EntityStats stats)
        {
            this.stats = stats;
            _random = new Random();
            maxHp = stats.hp;
        }

        public virtual void Tick()
        {

        }

        public virtual void Spawn(int[,] Map)
        {
            do
            {
                posX = _random.Next(60);
                posY = _random.Next(40);
            } while (Map[posX, posY] != 1);
        }

        public virtual void Move(int deltaX, int deltaY)
        {
            moved = false;
            OnMove?.Invoke(this, new MoveEventArgs(this, deltaX, deltaY));
        }

        public void MoveSuccessful(int deltaX, int deltaY)
        {
            moved = true;
            posX += deltaX;
            posY += deltaY;
        }

        public virtual void Attack(BaseEntity target)
        {

        }
    }
}