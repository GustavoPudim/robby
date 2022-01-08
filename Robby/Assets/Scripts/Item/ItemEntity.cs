using UnityEngine;

public class ItemEntity : MonoBehaviour {

    public string id;
    public Register.ItemRegister item { get { return Manager.register.GetItemById(id); } }

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = item.texture;
        
        Spread();
    }

    void Update()
    {
        spriteRenderer.material.SetInt("_Glow", Manager.instance.player.GetClosestDrop() == gameObject ? 1 : 0);
    }

    public void Spread()
    {
        Vector2 direction = UnityEngine.Random.insideUnitCircle.normalized;

        GetComponent<Rigidbody2D>().AddForce(direction*4, ForceMode2D.Impulse);
    }
}
