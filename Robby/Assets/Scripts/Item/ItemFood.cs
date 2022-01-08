public class ItemFood : Item
{
    public override void Use(Inventory inventory, Slot slot)
    {
        slot.RemoveItem();
        // Unfinished
    }
}
