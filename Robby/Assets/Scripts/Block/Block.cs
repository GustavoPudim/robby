using UnityEngine;
using System;

public class Block : MonoBehaviour
{

    [Serializable]
    public struct Drop 
    {
        [SerializeField] public string itemID;
        [SerializeField] public double chance;
    }

    private float currentLife = 1;

    [SerializeField] private Drop[] drops;
    [SerializeField] private float hardness;
    [SerializeField] private Enums.Tool efficientTool;
    

    public Sprite texture { get { return GetComponent<SpriteRenderer>().sprite; } }

    public void Damage(float damage)
    {
        currentLife -= damage / hardness;
        
        if(currentLife <= 0)
        {
            Break();
        }
    }

    void Break()
    {
        foreach (Drop drop in drops)
        {
            if(Util.RandomDouble() > drop.chance) break;
            Instantiate(Manager.instance.getItemDrop(drop.itemID), transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
