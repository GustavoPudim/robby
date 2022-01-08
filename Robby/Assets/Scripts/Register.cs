using System;
using System.Collections.Generic;
using UnityEngine;

public class Register : MonoBehaviour
{
    [Serializable]
    public class ItemRegister
    {
        [SerializeField] private string m_id;
        [SerializeField] private string m_displayName;
        [SerializeField] private int m_maxStack = 100;
        [SerializeField] private Sprite m_texture;
        [SerializeField] private Item m_itemFunctionality;

        public ItemRegister(string id, string displayName, int maxStack, Sprite texture, Item itemFunctionality)
        {
            m_id = id;
            m_displayName = displayName;
            m_maxStack = maxStack;
            m_texture = texture;
            m_itemFunctionality = itemFunctionality;
        }

        public string id { get { return m_id; } }
        public string displayName { get { return m_displayName; } }
        public int maxStack { get { return m_maxStack; } }
        public Sprite texture { get { return m_texture; } }
        public Item functionality { get { return m_itemFunctionality; } }
    }

    [SerializeField] private List<ItemRegister> items;
    [SerializeField] private List<BlockRegister> blocks;

    [Serializable]
    public class BlockRegister
    {
        [SerializeField] private string m_id;
        [SerializeField] private string m_displayName;
        [SerializeField] Block m_prefab;
        [SerializeField] private bool m_generateItemFromBlock;

        public string id { get { return m_id; } }
        public string displayName { get { return m_displayName; } }
        public Sprite texture { get { return prefab.texture; } }
        public Block prefab { get { return m_prefab; } }

        public bool hasItem => m_generateItemFromBlock;

    }

    private void Awake() 
    {
        foreach(BlockRegister block in blocks)
        {
            if(block.hasItem) items.Add(new ItemRegister(block.id, block.displayName, 1, block.texture, new Item()));
        }
    }

    public ItemRegister GetItemById(string id)
    {
        foreach(ItemRegister item in items)
        {
            if(id.Equals(item.id))
            {
                return item;
            }
        }

        return null;
    }

    public BlockRegister GetBlockById(string id)
    {
        foreach (BlockRegister blockRegister in blocks)
        {
            if (id.Equals(blockRegister.id))
            {
                return blockRegister;
            }
        }
        
        return null;
    }
}
