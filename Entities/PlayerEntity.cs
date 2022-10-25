using Roguelike.Items;

namespace Roguelike.Entities
{
    public class PlayerEntity : BaseEntity
    {
        public Inventory Inventory { get; set; }
        public PlayerEntity()
        {
            Inventory = new Inventory(24, ItemType.All);
            DisplayString = "\u001b[101m \u001b[0m";
        }

        public void Init(int[,] Map, int hp)
        {
            base.Init(hp, "player", new EntityStats(1, 3, 1));
            base.Spawn(Map);
        }

        public override void Move(int deltaX, int deltaY, int[,] Map)
        {
            base.Move(deltaX, deltaY, Map);
        }
    }
}
