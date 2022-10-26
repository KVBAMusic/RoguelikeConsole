using Roguelike.Items;

namespace Roguelike.Entities
{
    public class PlayerEntity : BaseEntity
    {
        public Inventory Inventory { get; set; }
        public PlayerEntity(EntityStats stats) : base(stats)
        {
            Inventory = new Inventory(24, ItemType.All);
        }


        public override void Move(int deltaX, int deltaY, int[,] Map)
        {
            base.Move(deltaX, deltaY, Map);
        }
    }
}
