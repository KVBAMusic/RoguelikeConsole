using Roguelike.Items;

namespace Roguelike.Entities
{
    public class PlayerEntity : BaseEntity
    {
        Random random;

        public Inventory Inventory { get; set; }

        public int Hunger { get; set; }
        public PlayerEntity(EntityStats stats) : base(stats)
        {
            random = new Random();
            Inventory = new Inventory(24, ItemType.All);
            Hunger = 500;
        }


        public override void Move(int deltaX, int deltaY)
        {
            base.Move(deltaX, deltaY);
        }

        public override void Tick()
        {
            if (moved)
            {
                if (Hunger > 0)
                    Hunger--;
                else
                {
                    if (random.Next(50) <= 1)
                    {
                        stats.hp--;
                    }
                }
            }
        }

        public string GetCurrentStats()
        {
            return $"\nHP:\t{stats.hp}\t\tFOOD:\t{Hunger}";
        }
    }
}
