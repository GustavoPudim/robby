using UnityEngine;

public class Inventory : MonoBehaviour
{

    [Header("Attributes")]
    public int width = 6;
    public int height = 4;
    
    [Header("References")]
    public GameObject background;
    public Slot[] slots;

    private int m_activeSlotX = 0;
    private int m_activeSlotY = 0;
    public int activeSlotX 
    { 
        get { return m_activeSlotX; } 
        set 
        {
            if(value < 0 || value >= width) return;

            slots[activeSlot].Deactivate();
            m_activeSlotX = value;
            slots[activeSlot].Activate();
        } 
    }
    public int activeSlotY
    { 
        get { return m_activeSlotY; } 
        set 
        {
            if(value < 0 || value >= height) return;

            slots[activeSlot].Deactivate();
            m_activeSlotY = value;
            slots[activeSlot].Activate();
        } 
    }
    public int activeSlot
    {
        get { return activeSlotX + activeSlotY * width; }
    }

    void Start()
    {
        slots[activeSlot].Activate();
    }

    void OnEnable()
    {
        background.transform.localScale = Vector3.one * Menu.UIScale;
    }

    public void AddItem(string id)
    {
        foreach(Slot slot in slots)
        {
            if(slot.AddItem(id)) return;
        }
    }

    public bool AddItemToSlot(string id, int slotId)
    {
        return slots[slotId].AddItem(id);
    }

    public void RemoveItem(string id)
    {
        foreach (Slot slot in slots)
        {
            if (slot.HasItem(id))
            {
                slot.RemoveItem();
                return;
            }
        }
    }

    public void RemoveItemFromSlot(int slotId)
    {
        slots[slotId].RemoveItem();
    }

    public void moveActiveSlot(Enums.Direction direction)
    {
        switch(direction)
        {
            case Enums.Direction.Right:
                activeSlotX++;
                break;
            case Enums.Direction.Left:
                activeSlotX--;
                break;
            case Enums.Direction.Down:
                activeSlotY++;
                break;
            case Enums.Direction.Up:
                activeSlotY--;
                break;
            default:
                break;
        }
    }

    public void useItem()
    {
        if (slots[activeSlot].itemId.Equals("none")) return;
        
        Manager.register.GetItemById(slots[activeSlot].itemId).functionality.Use(this, activeSlot);
    }
}
