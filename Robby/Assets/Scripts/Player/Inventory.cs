using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    
    [Header("References")]
    public GameObject background;
    public Slot[] slots;

    private GameObject lastSelect;

    public Slot activeSlot 
    { 
        get 
        { 
            if(EventSystem.current.currentSelectedGameObject == null) return null;

            EventSystem.current.currentSelectedGameObject.TryGetComponent<Slot>(out Slot slot);
            return slot;
        }
    }

    private Slot lastActiveSlot;

    void Update () 
    {
        // Workaround to prevent mouse clicks from deselecting objects        
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(lastSelect);
        }
        else
        {
            lastSelect = EventSystem.current.currentSelectedGameObject;
        }

        if(lastActiveSlot != activeSlot) 
        {
            if(lastActiveSlot) lastActiveSlot.Deactivate();
            if(activeSlot) activeSlot.Activate();
        }

        lastActiveSlot = activeSlot;
    }

    void OnEnable()
    {
        background.transform.localScale = Vector3.one * Menu.UIScale;

        if(!activeSlot) EventSystem.current.SetSelectedGameObject(slots[0].gameObject);
        activeSlot.GetComponent<Selectable>().OnSelect(null); // Makes sure the slot is highlighted
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

    public void useItem()
    {
        if (activeSlot.itemId.Equals("none")) return;
        
        Manager.register.GetItemById(activeSlot.itemId).functionality.Use(this, activeSlot);
    }
}
