namespace Roguelike.Items
{
    public interface IItem
    { 
        public string Name { get; }
        public string Description { get; }
        public bool isStackable { get; }
        public ItemType Type { get; }
    }

    public enum ItemType
    {
        All,
        Weapon,
        Armour,
        Potion,
        Consumable
    }

    public class WeaponItem : IItem
    {
        public string Name { get => throw new NotImplementedException();  }
        public string Description { get => throw new NotImplementedException();  }
        public bool isStackable => false;
        public ItemType Type => ItemType.Weapon;

        public int Durability { get; }
        public int MinDamage { get; }
        public int MaxDamage { get; }

    }

    public class ArmourItem : IItem
    {
        public string Name { get => throw new NotImplementedException(); }
        public string Description => throw new NotImplementedException();
        public bool isStackable => false;
        public ItemType Type => ItemType.Armour;
    }

}
