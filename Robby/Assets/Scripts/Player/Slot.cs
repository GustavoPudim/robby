using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public string itemId = "none";

    private int m_amount = 0;
    public int amount 
    {
        get { return m_amount; } 
        set
        {
            m_amount = value;
            if(m_amount == 0) itemId = "none";

            UpdateSlot();
        }
    }

    private bool active = false;

    [HideInInspector] public Image itemImage;
    [HideInInspector] public GameObject displayNamePanel;
    [HideInInspector] public Text displayNameText;
    [HideInInspector] public Text amountText;

    private void Awake()
    {
        itemImage = transform.GetChild(0).GetComponent<Image>();
        amountText = transform.GetChild(1).GetComponent<Text>();
        displayNamePanel = transform.GetChild(2).gameObject;
        displayNameText = displayNamePanel.GetComponentInChildren<Text>();

        ClearSlot();
    }

    public void ClearSlot()
    {
        itemImage.sprite = null;
        itemImage.gameObject.SetActive(false);
        amountText.text = "";
    }

    public void UpdateSlot()
    {
        ClearSlot();
        displayNamePanel.SetActive(!IsEmpty() && active);

        if (itemId.Equals("none")) return;

        Register.ItemRegister item = Manager.register.GetItemById(itemId);

        itemImage.sprite = item.texture;
        itemImage.gameObject.SetActive(true);
        
        displayNameText.text = item.displayName;
        amountText.text = amount > 1 ? amount.ToString() : "";
    }

    public void Activate()
    {
        GetComponent<Image>().color = Color.yellow;
        displayNamePanel.SetActive(!IsEmpty());

        active = true;
    }

    public void Deactivate()
    {
        GetComponent<Image>().color = Color.grey * (1.4f);
        displayNamePanel.SetActive(false);

        active = false;
    }

    public bool HasItem(string id)
    {
        return id.Equals(itemId);
    }

    public bool IsEmpty()
    {
        return amount == 0;
    }

    public bool IsFull()
    {
        return amount >= Manager.register.GetItemById(itemId).maxStack;
    }

    public bool CanHold(string id)
    {
        return (HasItem(id) && !IsFull()) || IsEmpty();
    }

    public bool AddItem(string id)
    {
        if (!CanHold(id)) return false;

        itemId = id;
        amount++;
        return true;
    }

    public void RemoveItem()
    {
        amount--;
    }
}
