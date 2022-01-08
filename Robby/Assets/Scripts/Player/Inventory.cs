using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : UIScreen
{
    
    [Header("References")]
    public GameObject background;
    public Selectable firstSelect;
    public GameObject[] sections;
    public Slot[] slots;

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

        if(!EventSystem.current.currentSelectedGameObject) firstSelect.Select();
        EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>().OnSelect(null); // Makes sure the object is highlighted
    }

    public void OpenSection(int index)
    {
        for (int i = 0; i < sections.Length; i++)
        {
            sections[i].SetActive(i == index);
        }
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
