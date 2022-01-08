public class ItemFood : Item
{
    public override void Use(Inventory inventory, int slot)
    {
        inventory.RemoveItemFromSlot(slot);
        // Unfinished
    }
}
